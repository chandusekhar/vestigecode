using System.Configuration;
using AutoMapper;
using WSS.Data;
using WSS.Email.Service;
using WSS.Logging.Service;
using WSS.User.Service;
using WSS.Common.Utilities.ActionLink;
using WSS.InternalApplication.Infrastructure;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WSS.InternalApplication.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WSS.InternalApplication.App_Start.NinjectWebCommon), "Stop")]

namespace WSS.InternalApplication.App_Start
{
    using AuditTransaction.Interfaces;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<UtilityBilling.Data.IUnitOfWork>().To<UtilityBilling.Data.UnitOfWork>();
            kernel.Bind<ILogger>().To<Logger>();
            kernel.Bind<IAuditTransaction>().To<AuditTransaction.Implementation.AuditTransaction>();
            kernel.Bind<WWDCommon.Data.IUnitOfWork>().To<WWDCommon.Data.UnitOfWork>();
            kernel.Bind<ISendEmail>().To<SendEmail>();
            kernel.Bind<IWssUserManager>().To<WssUserManager>().InSingletonScope();
            kernel.Bind<IActionLinkManager>().To<ActionLinkManager>().WithConstructorArgument("baseUrl", ConfigurationManager.AppSettings["baseUrl"]);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperProfile>();
            });

            var mapper = config.CreateMapper();
            kernel.Bind<IMapper>().ToConstant(mapper);
        }
    }
}
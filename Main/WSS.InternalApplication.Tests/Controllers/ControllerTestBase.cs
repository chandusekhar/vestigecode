using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSS.Data;
using WSS.InternalApplication.Infrastructure;
using Moq;

namespace WSS.InternalApplication.Controllers.Tests
{
    public class ControllerTestBase
    {
        public IUnitOfWork MockUnitOfWork;
        public UtilityBilling.Data.IUnitOfWork MockUtilityUnitOfWork;
        public Mock<IUnitOfWork> _mockUnitOfWork;
        public Mock<UtilityBilling.Data.IUnitOfWork> _mockUtilityUnitOfWork;
        protected Mock<IDataRepository<UtilityBilling.Data.UtilityAccount>> _mockUtilityAccountRepository;
        protected Mock<IDataRepository<UtilityBilling.Data.DocumentHeader>> _mockDocumentHeaderRepository;
        protected Mock<IDataRepository<UtilityBilling.Data.DocumentDetail>> _mockDocumentDetailRepository;
        protected Mock<IDataRepository<UtilityBilling.Data.MeterServiceAgreement>> _mockMeterServiceAgreementRepository;
        protected Mock<IDataRepository<Status>> _mockStatus;
        public ControllerTestBase()
        {
            BaseTestInitialize();
        }
        public void BaseTestInitialize()
        {

        }
        public void BaseTestCleanUp()
        {

        }
    }

}

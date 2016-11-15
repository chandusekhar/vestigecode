using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TransfersComponent.BusinessObjects
{
    public interface ITransferOrder
    {

        #region Property
        System.Int32 SourceLocationId
        {
            get;
            set;
        }

        System.Int32 StatusId
        {
            get;
            set;
        }

         System.Int32 DestinationLocationId
        {
            get;
            set;
        }

         string StatusName
        {
            get;
            set;
        }

         string DestinationAddress
        {
            get;
            set;
        }

         string SourceAddress
        {
            get;
            set;
        }

         string TNumber
        {
            get;
            set;
        }

        #endregion
    }
}

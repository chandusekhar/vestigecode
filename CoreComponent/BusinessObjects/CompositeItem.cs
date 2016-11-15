using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.MasterData.BusinessObjects;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.MasterData.BusinessObjects
{
    [Serializable]
    public class CompositeItem : ItemMaster, IItemCompositeBOM, IUOMHeader
    {
        #region Variables
        Int32 m_compItemId = Common.INT_DBNULL;
        Int32 m_quantity = Common.INT_DBNULL;
        Int32 m_uomId = Common.INT_DBNULL;
        String m_uomName = String.Empty;

        public const String GRID_ITEMID = "ItemId";
        public const String GRID_ITEMCODE = "ItemCode";
        public const String GRID_COMPITEMID = "CompositeItemId";
        public const String GRID_UOMID = "UOMId";
        public const String GRID_STATUS = "Status";
        public const String GRID_QTY = "Quantity";
        #endregion

        #region CTors
        public CompositeItem()
        { }
        #endregion

        #region Properties

        public Int32 CompositeItemId
        {
            get { return m_compItemId; }
            set { m_compItemId = value; }
        }

        public Int32 Quantity
        {
            get { return m_quantity; }
            set { m_quantity = value; }
        }

        public Int32 UOMId
        {
            get { return m_uomId; }
            set { m_uomId = value; }
        }

        public String UOMName
        {
            get { return m_uomName; }
            set { m_uomName = value; }
        }

        public bool IsTradable { get; set; }

        #endregion
    }
}

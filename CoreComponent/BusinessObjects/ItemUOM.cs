using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.MasterData.BusinessObjects
{
    [Serializable]
    public class ItemUOMHeader : ItemMaster, IUOMHeader
    {
        private Int32 m_itemUOM = Common.INT_DBNULL;
        private String m_itemUOMName = String.Empty;

        public Int32 UOMId
        {
            get { return m_itemUOM; }
            set { m_itemUOM = value; }
        }

        public String UOMName
        {
            get { return m_itemUOMName; }
            set { m_itemUOMName = value; }
        }
    }
    [Serializable]
    public class ItemUOMDetails : ItemUOMHeader, IItemUOMLink
    {
        #region Const & ReadOnly Variables
        public const String UOM_VALUE_MEM = "UOMId";
        public const String UOM_TEXT_MEM = "UOMName";

        public const String GRID_ITEM_COL = "ItemId";
        public const String GRID_UOM_COL = "UOMId";
        public const String GRID_TOM_COL = "TOMId";
        public const String GRID_PRIM_COL = "IsPrimary";
        public const String GRID_ITEMUOM_COL = "ItemUOMId";
        #endregion

        #region Variables
        private Int32 m_itemUOMId = Common.INT_DBNULL;
        private Int32 m_uomId = Common.INT_DBNULL;
        private String m_uomName = String.Empty;
        private Int32 m_tomId = Common.INT_DBNULL;
        private String m_tomName = String.Empty;
        private Boolean m_isPrimary = false;
        private Int32 m_convFactor = Common.INT_DBNULL;
        #endregion

        public Int32 ItemUOMId
        {
            get { return m_itemUOMId; }
            set { m_itemUOMId = value; }
        }

        public Int32 TOMId
        {
            get { return m_tomId; }
            set { m_tomId = value; }
        }

        public Int32 ConversionFactor
        {
            get { return m_convFactor; }
            set { m_convFactor = value; }
        }

        public String TOMName
        {
            get { return m_tomName; }
            set { m_tomName = value; }
        }

        public Boolean IsPrimary
        {
            get { return m_isPrimary; }
            set { m_isPrimary = value; }
        }
    }
}

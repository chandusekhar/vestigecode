using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;

namespace CoreComponent.MasterData.BusinessObjects
{
    public interface IItemHeader
    {
        System.Int32 ItemId { get; set; }
        System.String ItemCode { get; set; }
        System.String ItemName { get; set; }
    }

    public interface IItemDetails
    {
        System.String ShortName { get; set; }
        System.String DisplayName { get; set; }
        System.String ReceiptName { get; set; }
        System.String PrintName { get; set; }
        System.String ItemBarCode { get; set; }
        System.Int32 IsKit { get; set; }
        System.Int32 IsComposite { get; set; }
        System.Int32 IsPromoPart { get; set; }
        System.Int32 ExpiryDuration { get; set; }
        System.Int32 SubCategoryId { get; set; }
    }

    public interface IItemPrice
    {
        System.Decimal PrimaryCost { get; set; }
        System.Decimal MRP { get; set; }
        System.Decimal DistributorPrice { get; set; }
        /*System.Decimal TransferPrice { get; set; }*/
        System.Decimal BusinessVolume { get; set; }
        System.Decimal PointValue { get; set; }
        System.Int32 TaxCategoryId { get; set; }
    }

    public interface IItemLocation: ILocation
    {
        String LocationDisplayName { get; }
        Decimal ReorderLevel { get; set; }
    }

    public interface IItemUOMLink
    {
        Int32 ItemUOMId { get; set; }
        Int32 TOMId { get; set; }
        Int32 ConversionFactor { get; set; }
        Boolean IsPrimary { get; set; }
        String TOMName { get; set; }
    }

    public interface IItemCompositeBOM
    {
        Int32 CompositeItemId { get; set; }
        //Int32 CompositePurchaseUOM { get; set; }
    }

    public interface IItemDimensions
    {
        System.Int32 ItemLength { get; set; }
        System.Int32 ItemWidth { get; set; }
        System.Int32 ItemHeight { get; set; }
        //System.Int32 ItemWeight { get; set; }
        System.Int32 StackLimit { get; set; }
        System.String BayNumber { get; set; }
 
    }

    public interface IItemMaster : IItemHeader, IStatus, IModifierDetails
    {
        Boolean Save(String xmlDoc, String spName, ref String errorMessage);
    }
}

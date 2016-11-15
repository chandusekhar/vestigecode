using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionsComponent.BusinessLayer
{
    public interface IPromotion
    {
        bool Save(string xmlDoc, string spName,ref Int32 promotionId, ref string errorMessage);
        Promotion  Search(string xmlDoc, string spName, ref string errorMessage);
    }
}

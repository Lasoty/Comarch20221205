using Restauracja.Common.Model.Dto;

namespace Restauracja.Web.Models
{
    public class CouponDto : BaseDtoEntity
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
    }
}

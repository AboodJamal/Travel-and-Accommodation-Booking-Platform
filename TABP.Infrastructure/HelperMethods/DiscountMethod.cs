using TABP.Domain.Entities;

namespace Infrastructure.HelperMethods.DiscountMethod;

public static class DiscountMethod
{
    public static float GetDiscount(IEnumerable<Discount> roomType)
    {
        return roomType
            .FirstOrDefault(discount =>
                discount.FromDate.Date <= DateTime.Today.Date && 
                discount.ToDate.Date >= DateTime.Today.Date)
            ?.DiscountPercentage ?? 0.0f;
    }
}
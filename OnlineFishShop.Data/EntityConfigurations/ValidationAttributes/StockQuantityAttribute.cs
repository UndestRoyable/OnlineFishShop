using System.ComponentModel.DataAnnotations;
using OnlineFishShop.Common;

namespace OnlineFishShop.Data.EntityConfigurations.ValidationAttributes
{
    public class StockQuantityAttribute : ValidationAttribute
    {
        private const byte minValue = 0;
        private const byte maxValue = byte.MaxValue;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            byte quantity = (byte)value;

            if (!(quantity >= minValue && quantity <= maxValue))
            {
                return new ValidationResult(CommonConstants.WrongStockAmount);
            }

            return ValidationResult.Success;
        }
    }
}

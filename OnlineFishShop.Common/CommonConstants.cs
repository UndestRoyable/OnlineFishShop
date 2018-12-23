using System;

namespace OnlineFishShop.Common
{
    public class CommonConstants
    {
        public const string WrongStockAmount = "Wrong stock amount provided";

        public const string StockAmountExceededError =
                "You have requested quantities for a product that exceed our stock level. Please reduce the amount or remove the item and try again.";

        public const string StockAmountExceededForError =
            "You have requested quantities for {0} that exceed our stock level. Please reduce the amount or remove the item and try again.";

        public const string AddingAmountWillExceedOurStock = "Adding this amount to your current bag amount will exceed our stock for this item";
        public const string DeletedCartItemSuccessfully = "Successfully removed item from your bag!";
        public const string UpdatedCartItemSuccessfully = "Successfully updated your bag!";
        public const string SuccessfullyAddedCartItem = "Successfully added this product to the cart.";
        public const string SuccessfullyAddedMoreOfThisItem = "Successfully added more of this product to the cart.";
    }
}

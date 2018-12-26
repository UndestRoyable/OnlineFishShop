namespace OnlineFishShop.Web.Infrastructure.Constants
{
    public class WebConstants
    {
        public class Routes
        {
            public const string ProductDetails = "product_details";
            public const string ProductEdit = "product_edit";
            public const string ProductsPaginatedListing = "products_paginated_listing";
        }

        public class RoleNames
        {
            public const string AdminRole = "Admin";
            public const string ManagerRole = "Manager";
        }


        public enum OrderBy
        {
            NameAsc,
            NameDesc,
            PriceAsc,
            PriceDesc
        }

        public class Areas
        {
            public const string ProductsArea = "Products";
            public const string ApiArea = "Api";
            public const string AdminArea = "Admin";
            public const string ShoppingArea = "Shopping";
        }


        public const decimal TaxPercent = 0.2m;
        public const int CommentsPerPage = 7;
        public const string SuchCategoryExists = "Such category already exists.";
        public const string CategoryCreated = "Category {0} successfully created.";
        public const string CheckoutSecured = "Payment successfull. Your shipment is on its way.";
        public const string ProductSuccessfullyDeleted = "{0} successfully deleted.";
        public const string ProductAlreadyExists = "Such product already exists.";
        public const string InvalidDataSupplied = "Invalid data supplied";
        public const string SuccessfullyCreatedProduct = "Product was successfully created.";
    }
}

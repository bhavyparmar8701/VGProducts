using System.Runtime.CompilerServices;

namespace VGProducts.API.Extentions
{
    public class Permissions
    {
        public const string CreateCategory = "CreateCategory";
        public const string UpdateCategory = "UpdateCategory";
        public const string DeleteCategory = "DeleteCategory";
        public const string ViewCategory = "ViewCategory";

        public const string CreateSubCategory = "CreateSubCategory";
        public const string UpdateSubCategory = "UpdateSubCategory";
        public const string DeleteSubCategory = "DeleteSubCategory";
        public const string ViewSubCategory = "ViewSubCategory";

        public const string CreateProduct = "CreateProduct";
        public const string UpdateProduct = "UpdateProduct";
        public const string DeleteProduct = "DeleteProduct";
        public const string ViewProduct = "ViewProduct";
        public const string GetProductById = "GetProductById";

        public const string CreateFavourites = "CreateFavourites";
        public const string DeleteFavourites = "DeleteFavourites";
        public const string ViewFavourites = "ViewFavourites";

        public const string CreateCartItem = "CreateCartItem";
        public const string ViewCartItem = "ViewCartItem";
        public const string DeleteCartItemById = "DeleteCartItemById";
        public const string DeleteAllCartItemAsync = "DeleteAllCartItemAsync";
        public const string AddByIdCartItemAsync = "AddByIdCartItemAsync";

        public const string CreateCountry = "CreateCountry";
        public const string GetCountry = "GetCountry";
        public const string GetCountryById = "GetCountryById";
        public const string DeleteCountry = "DeleteCountry";

        public const string CreateState = "CreateState";
        public const string GetState = "GetState";
        public const string GetStateById = "GetStateById";
        public const string DeleteState = "DeleteState";

        public const string CreateCity = "CreateCity";
        public const string GetCity = "GetCity";
        public const string GetCityById = "GetCityById";
        public const string DeleteCity = "DeleteCity";

        public const string CreateAddress = "CreateAddress";
        public const string GetAddress = "GetAddress";
        public const string GetAddressById = "GetAddressById";
        public const string DeleteAddress = "DeleteAddress";
        public const string UpdateAddress = "UpdateAddress";

        public const string CreateOrder = "CreateOrder";
        public const string GetOrder = "GetOrder";
        public const string DeleteOrder = "DeleteOrder";
        public const string SelectPaymentMethod = "SelectPaymentMethod";
        public const string GetPaymentQr = "GetPaymentQr";

        public const string GetInvoice = "GetInvoice";

        public const string GetAllUser = "GetAllUser";
        public const string UpdateUser = "UpdateUser";
        public const string ChangePassword = "ChangePassword";
        public const string CreateOrUpdateReview = "CreateOrUpdateReview";

    }
}

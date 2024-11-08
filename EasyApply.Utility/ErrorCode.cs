namespace EasyApply.Utility
{
    public class ErrorCode
    {
        public const string ERROR_0 = "";
        public const string ERROR_1 = "UNKNOWN";
        public const string ERROR_2 = "USERNAME_TAKEN";
        public const string ERROR_3 = "INVALID_ROLE";
        public const string ERROR_4 = "BAD_INPUT";
        public const string ERROR_5 = "NOT_FOUND";
        public const string ERROR_6 = "MODEL_VALIDATION_ERROR";
        public const string ERROR_7 = "CREATE_USER_ERROR";
        public const string ERROR_8 = "RANDOM_DOMAIN_ERROR";
        public const string ERROR_9 = "CUSTOM_DOMAIN_ERROR";
        public const string ERROR_10 = "SLUG_OR_URL_TAKEN";
        public const string ERROR_11 = "PASSWORD_UPDATE_FAILED";
        public const string ERROR_12 = "PASSWORD_TOKEN_ERROR";
        public const string ERROR_13 = "FEEDBACK_ALREADY_GIVEN";
        public const string ERROR_14 = "COUPON_NAME_TAKEN";
    }

    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data)
        {
            Success = true;
            Message = string.Empty;
            ErrorCode = Utility.ErrorCode.ERROR_0;
            Errors = null;
            Data = data;

        }

        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public T Data { get; set; }
    }

    public class UserRoles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string User = "User";
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WebCommon.Constants
{
    public class Constants
    {
        public class AppSettingKeys
        {
            public const string DEFAULT_CONNECTION = "DefaultConnection";
            public const string JWT_VALIDAUDIENCE = "JWT:ValidAudience";
            public const string JWT_VALIDISSUER = "JWT:ValidIssuer";
            public const string JWT_SECRET = "JWT:Secret";
            public const string JWT_EXPIREMINUTES = "JWT:RefreshExpireMinute";

        }

        public class Controller
        {
            public const string DEFAULT_ROUTE_CONTROLLER = "api/[controller]/[action]";
        }
        public class Role
        {
            public const string ADMIN = "Admin";
            public const string USER = "User";
        }
        public class Commons
        {
            public const string EMAIL_INVALID = "Email invalid !";
            public const string FIELD_REQUIRED = "This field is required";
            public const string USER_ALREADY_EXIST = "User already exist";
            public const string USER_NOT_EXIST = "User not exist";
            public const string ITEM_NOT_EXIST = "{0} not exist";
        }
    }
}

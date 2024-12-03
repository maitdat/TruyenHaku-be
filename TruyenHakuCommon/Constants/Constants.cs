using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace TruyenHakuCommon.Constants
{
    public partial class Constants
    {
        public class AppSettingKeys
        {
            public const string DEFAULT_CONNECTION = "DefaultConnection";
            public const string JWT_VALIDAUDIENCE = "JWT:ValidAudience";
            public const string JWT_VALIDISSUER = "JWT:ValidIssuer";
            public const string JWT_SECRET = "JWT:Secret";
            public const string JWT_EXPIREMINUTES = "JWT:RefreshExpireMinute";
            public const string DEFAULT_ROOT_DIRECTORY = "DefaultRootDirectory";
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
        public class PathFile
        {
            public const string DEFAULT_ROOT_DIRECTORY = "E:\\Data Manga";
        }

        public class Pagination
        {
            public const int PAGE_NO_DEFAULT = 1;
            public const int PAGE_SIZE_DEFAULT = 20;
            public const int PAGE_SIZE_MANGA = 36;
        }
    }
}

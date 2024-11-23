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
            public const string PATH_FILE_MANGA = "E:\\Data Manga";
        }

        public class QuerySelectorNetTruyenViet
        {
            public const string MANGA_NAME = ".title-detail";
            public const string ANOTHER_MANGA_NAME = ".other-name";
            public const string AUTHOR = ".author col-xs-8";
            public const string IMAGETHUMB = ".image-thumb";
            public const string LIST_CHAPTER = ".list-chapter > nav #desc li .chapter a ";
            public const string IMGAGE = ".reading-detail .page-chapter img";
            public const string IMAGE_ATTRIBUTE = "data-src";
        }

        public class QuerySelectorTruyenQQ
        {
            public const string MANGA_NAME = ".book_other h1";
            public const string ANOTHER_MANGA_NAME = "";
            public const string AUTHOR = "";
            public const string IMAGETHUMB = ".book_avatar img";
            public const string LIST_CHAPTER = ".works-chapter-list .name-chap a";
            public const string IMGAGE = "#list_image .page-chapter img ";
            public const string IMAGE_ATTRIBUTE = "src";
            public const string HTTPS = "";
        }
    }
}

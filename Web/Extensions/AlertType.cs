using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Helpers
{
    public static class AlertType
    {
        public static class Alerts
        {
            /// <summary>
            /// 成功
            /// </summary>
            public const string Success = "success";
            /// <summary>
            /// 警告
            /// </summary>
            public const string Attention = "attention";
            /// <summary>
            /// 错误
            /// </summary>
            public const string Warning = "warning";
            /// <summary>
            /// 信息
            /// </summary>
            public const string Information = "info";

            public static string[] All => new[] { Success, Attention, Information, Warning };
        }
    }
}

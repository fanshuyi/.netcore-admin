using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Api.Models
{
    /// <summary>
    /// api 操作返回状态
    /// </summary>
    public class ResInfo : IResInfo
    {
        /// <summary>
        /// </summary>
        public ResInfo()
        {
            Code = ResInfoCode.Success;
        }

        private string msg = "操作成功";

        private object data;

        /// <summary>
        /// 状态码 0成功 -1失败
        /// </summary>
        public ResInfoCode Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg
        {
            get
            {
                return msg;
            }
            set
            {
                msg = value;
                Code = ResInfoCode.Fail;
            }
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data
        {
            get { return data; }
            set
            {
                data = value;
                Code = ResInfoCode.Success;
            }
        }
    }

    /// <summary>
    /// </summary>
    public interface IResInfo
    {
        /// <summary>
        /// 状态码 0成功 -1失败
        /// </summary>
        ResInfoCode Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        string Msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        object Data { get; set; }
    }

    /// <summary>
    /// 返回信息状态
    /// </summary>
    public enum ResInfoCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 失败
        /// </summary>
        Fail = -1,
    }
}
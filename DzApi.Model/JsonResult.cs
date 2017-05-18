using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzApi.Model
{

    /// <summary>
    /// Json返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonResult<T>
    {

        /// <summary>
        /// 操作码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 是否拉取成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 消息说明
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public T Result { get; set; }
    }
}

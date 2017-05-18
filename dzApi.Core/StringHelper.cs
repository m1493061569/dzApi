using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dzApi.Core
{
    public static class StringHelper
    {

        /// <summary>
        /// 字符串转换为List
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        public static List<string> toList(this string str, char? split = ',')
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            List<string> result = new List<string>();
            if (!str.Contains(split.Value))
            {
                result.Add(str);
            }
            else
            {
                try
                {
                    result.AddRange(str.Split(split.Value));
                }
                catch
                {
                    return null;
                }
            }

            return result;
        }
    }
}

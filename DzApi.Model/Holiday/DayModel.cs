using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzApi.Model.Holiday
{
    public class DayModel
    {
        public string date { get; set; }

        /// <summary>
        /// 工作日对应结果为2, 休息日对应结果为 1, 节假日对应的结果为0
        /// </summary>
        public int type { get; set; }
    }
}

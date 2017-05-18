using DzApi.Model;
using dzApi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dzApi.Areas.Service.Controllers
{

    /// <summary>
    /// 节假日API
    /// </summary>
    [RoutePrefix("api")]
    public class HolidayController : ApiController
    {

        /// <summary>
        /// 获取节假日
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [Route("v1/GetHoliday")]
        public JsonResult<List<string>> GetHoliday(string d)
        {
            if (string.IsNullOrEmpty(d))
            {
                return new JsonResult<List<string>>() { Code = "0001", IsSuccess = false, Message = "缺少日期" };
            }
            List<string> dateList = d.toList(',');
            if (dateList == null || dateList.Count <= 0)
            {
                return new JsonResult<List<string>>() { Code = "0001", IsSuccess = false, Message = "缺少日期,日期不合法" };
            }

            List<string> result = new List<string>();
            foreach (var item in dateList)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }

                if (item.Length == 4) //只传了年份
                {
                    DateTime startDate = new DateTime(int.Parse(item), 1, 1);
                    DateTime endDate = new DateTime(int.Parse(item), 12, 31);

                    while (startDate <= endDate)
                    {
                        #region 判断是否为节假日
                        if (startDate.IsHoliday())
                        {
                            result.Add(startDate.ToString("yyyyMMdd"));
                        }
                        #endregion

                        startDate = startDate.AddDays(1);
                    }
                }
                else if (item.Length == 6) //只带年月
                {
                    DateTime startDate = new DateTime(int.Parse(item.Substring(0, 4)), int.Parse(item.Substring(4)), 1);
                    DateTime endDate = new DateTime(int.Parse(item.Substring(0, 4)), int.Parse(item.Substring(4)) + 1, 1).AddDays(-1);

                    while (startDate <= endDate)
                    {
                        #region 判断是否为节假日
                        if (startDate.IsHoliday())
                        {
                            result.Add(startDate.ToString("yyyyMMdd"));
                        }
                        #endregion

                        startDate = startDate.AddDays(1);
                    }

                }
            }

            return new JsonResult<List<string>>() { Code = "0000", IsSuccess = true, Message = "", Result = result };
        }

        /// <summary>
        /// 获取周末
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [Route("v1/GetWeekend")]
        public JsonResult<List<string>> GetWeekend(string d)
        {
            if (string.IsNullOrEmpty(d))
            {
                return new JsonResult<List<string>>() { Code = "0001", IsSuccess = false, Message = "缺少日期" };
            }
            List<string> dateList = d.toList(',');
            if (dateList == null || dateList.Count <= 0)
            {
                return new JsonResult<List<string>>() { Code = "0001", IsSuccess = false, Message = "缺少日期,日期不合法" };
            }

            List<string> result = new List<string>();
            foreach (var item in dateList)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }

                if (item.Length == 4) //只传了年份
                {
                    DateTime startDate = new DateTime(int.Parse(item), 1, 1);
                    DateTime endDate = new DateTime(int.Parse(item), 12, 31);

                    while (startDate <= endDate)
                    {
                        if (startDate.IsWeekend())
                        {
                            result.Add(startDate.ToString("yyyyMMdd")); //周末
                        }

                        startDate = startDate.AddDays(1);
                    }
                }
                else if (item.Length == 6) //只带年月
                {
                    DateTime startDate = new DateTime(int.Parse(item.Substring(0, 4)), int.Parse(item.Substring(4)), 1);
                    DateTime endDate = new DateTime(int.Parse(item.Substring(0, 4)), int.Parse(item.Substring(4)) + 1, 1).AddDays(-1);

                    while (startDate <= endDate)
                    {
                        if (startDate.IsWeekend())
                        {
                            result.Add(startDate.ToString("yyyyMMdd")); //周末
                        }

                        startDate = startDate.AddDays(1);
                    }
                }
            }

            return new JsonResult<List<string>>() { Code = "0000", IsSuccess = true, Message = "", Result = result };
        }
        
        /// <summary>
        /// 获取工作日
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [Route("v1/GetWeekday")]
        public JsonResult<List<string>> GetWeekday(string d)
        {
            if (string.IsNullOrEmpty(d))
            {
                return new JsonResult<List<string>>() { Code = "0001", IsSuccess = false, Message = "缺少日期" };
            }
            List<string> dateList = d.toList(',');
            if (dateList == null || dateList.Count <= 0)
            {
                return new JsonResult<List<string>>() { Code = "0001", IsSuccess = false, Message = "缺少日期,日期不合法" };
            }

            List<string> result = new List<string>();
            foreach (var item in dateList)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }

                if (item.Length == 4) //只传了年份
                {
                    DateTime startDate = new DateTime(int.Parse(item), 1, 1);
                    DateTime endDate = new DateTime(int.Parse(item), 12, 31);

                    while (startDate <= endDate)
                    {
                        if (!startDate.IsHoliday() && !startDate.IsWeekend())
                        {
                            result.Add(startDate.ToString("yyyyMMdd")); //
                        }

                        startDate = startDate.AddDays(1);
                    }
                }
                else if (item.Length == 6) //只带年月
                {
                    DateTime startDate = new DateTime(int.Parse(item.Substring(0, 4)), int.Parse(item.Substring(4)), 1);
                    DateTime endDate = new DateTime(int.Parse(item.Substring(0, 4)), int.Parse(item.Substring(4)) + 1, 1).AddDays(-1);

                    while (startDate <= endDate)
                    {
                        if (!startDate.IsHoliday() && !startDate.IsWeekend())
                        {
                            result.Add(startDate.ToString("yyyyMMdd")); //
                        }

                        startDate = startDate.AddDays(1);
                    }
                }
            }

            return new JsonResult<List<string>>() { Code = "0000", IsSuccess = true, Message = "", Result = result };
        }

        /// <summary>
        /// 判断对应日期是节假日、周末还是工作日
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [Route("v1/Get")]
        public JsonResult<Dictionary<string, int>> Get(string d)
        {
            if (string.IsNullOrEmpty(d))
            {
                return new JsonResult<Dictionary<string, int>>() { Code = "0001", IsSuccess = false, Message = "缺少日期" };
            }
            List<string> dateList = d.toList(',');
            if (dateList == null || dateList.Count <= 0 || dateList.Where(t => t.Length == 8).Count() <= 0)
            {
                return new JsonResult<Dictionary<string, int>>() { Code = "0001", IsSuccess = false, Message = "缺少日期,日期不合法" };
            }

            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach(var item in dateList)
            {
                DateTime? date = item.ToDateTime();
                if (!date.HasValue)
                {
                    continue;
                }
                if (date.Value.IsHoliday())
                {
                    dict.Add(item, 0); //节假日
                }
                else if (date.Value.IsWeekend())
                {
                    dict.Add(item, 1); //周末
                }
                else
                {
                    dict.Add(item, 2); //工作日
                }
            }
            return new JsonResult<Dictionary<string, int>>() { Code = "0000", IsSuccess = true, Message = "", Result = dict };
        }
    }
}

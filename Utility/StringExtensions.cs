using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Extensions.Primitives;


namespace RoadFlow.Utility
{
    /// <summary>
    /// 字符串操作扩展类
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 将字符串转换为整数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns></returns>
        public static int ToInt(this string str, int defaultValue = int.MinValue)
        {
            return int.TryParse(str, out int i) ? i : defaultValue;
        }

    }
}
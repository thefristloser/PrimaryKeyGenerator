using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RoadFlow.Utility
{
    /// <summary>
    /// 系统配置类
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static string DatabaseType { get; set; } = "sqlserver";

        /// <summary>
        /// 雪花算法，workerid
        /// </summary>
        public static int SnowFlakeWorkerId { get; set; } = 0;

        /// <summary>
        /// 在生成guid时的偏移值
        /// </summary>
        public static int PianYi { get; set; } = 0;

        /// <summary>
        /// 在生成guid时的byte数组
        /// </summary>
        public static byte[] PianYiBytes { get; set; }
    }
}

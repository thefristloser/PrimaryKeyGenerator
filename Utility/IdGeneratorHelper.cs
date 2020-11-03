using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RoadFlow.Utility
{
    /// <summary>
    /// 生成数据库主键Id
    /// </summary>
    public class IdGeneratorHelper
    {
        private int SnowFlakeWorkerId = RoadFlow.Utility.Config.SnowFlakeWorkerId;

        private Snowflake snowflake;

        private static readonly IdGeneratorHelper instance = new IdGeneratorHelper();

        private IdGeneratorHelper()
        {
            snowflake = new Snowflake(SnowFlakeWorkerId, 0, 0);
        }
        public static IdGeneratorHelper Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// long型。如果要存字符串，请调用GetIdStr()函数
        /// </summary>
        /// <returns></returns>
        public long GetId()
        {
            return snowflake.NextId();
        }

        /// <summary>
        /// 19位数字
        /// </summary>
        /// <returns></returns>
        public string GetIdStr()
        {
            return snowflake.NextId().ToString().PadLeft(19,'0');
        }
    }
}

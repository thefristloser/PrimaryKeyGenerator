using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RoadFlow.Utility
{
    public static class GuidExtensions
    {
        /// <summary>
        /// 生成新的GUID（有的需要生成连续的Guid,所以统一调用这里的方法）
        /// 以下升序（数据库排序规则）
        /// -----------------升序-低1-------低2--低3---高4-------高5
        /// new Guid(new Byte[] {0,0,0,1,  0,0, 0,0  ,0,0,  0,0,0,0,0,0 }) 01000000-0000-0000-0000-000000000000
        /// new Guid(new Byte[] {0,0,0,0,  0,1,  0,0  ,0,0,  0,0,0,0,0,0 }) 00000000-0100-0000-0000-000000000000
        /// new Guid(new Byte[] {0,0,0,0, 0,0, 0,1  ,0,0,0,0,0,0,0,0 }) [00000000-0000-0100-0000-000000000000]
        /// new Guid(new Byte[] {0,0,0,0, 0,0, 0,0  ,0,1, 0,0,0,0,0,0 })[00000000-0000-0000-0001-000000000000]
        /// new Guid(new Byte[] {0,0,0,0, 0,0, 0,0  ,0,0, 0,0,0,0,0,1 })[00000000-0000-0000-0000-000000000001]
        /// new Guid(new Byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,   16, 0, 0, 0, 0, 0 }) [00000000-0000-0000-0000-100000000000]
        /// long转byte数组 低索引存低位 
        /// new Guid(new Byte[] {0,0,0,0, 0,0, 0,0  ,0,0, 0,0,0,0,0,1 })
        /// </summary>
        /// <returns></returns>
        public static Guid NewGuid()
        {
            long id= IdGeneratorHelper.Instance.GetId();
            var idBytes=BitConverter.GetBytes(id);
            var pianYiBytes = Config.PianYiBytes;
            Byte[] idByte = new Byte[16] { idBytes[3], idBytes[2], idBytes[1], idBytes[0],     idBytes[5], idBytes[4],   idBytes[7],idBytes[6],
                pianYiBytes[1],pianYiBytes[0],   255,255,255,255,pianYiBytes[3],pianYiBytes[2],
            };
            return new Guid(idByte);
        }

        /// <summary>
        /// 判断为空GUID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsEmptyGuid(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        /// <summary>
        /// 判断为空GUID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsEmptyGuid(this Guid? guid)
        {
            return !guid.HasValue || guid.Value == Guid.Empty;
        }

        /// <summary>
        /// 判断不为空GUID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsNotEmptyGuid(this Guid guid)
        {
            return guid != Guid.Empty;
        }

        /// <summary>
        /// 判断不为NULL和空GUID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsNotEmptyGuid(this Guid? guid)
        {
            return guid.HasValue && guid.Value != Guid.Empty;
        }

        /// <summary>
        /// 将GUID转换为整数
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static int ToInt(this Guid guid)
        {
            return Math.Abs(guid.GetHashCode());
        }

        /// <summary>
        /// 转换GUID为大写字符串
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string ToUpperString(this Guid guid)
        {
            return guid.ToString().ToUpper();
        }
        /// <summary>
        /// 转换guid为小写字符串
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string ToLowerString(this Guid guid)
        {
            return guid.ToString().ToLower();
        }

        /// <summary>
        /// 没有分隔线的字符串
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string ToNString(this Guid guid)
        {
            return guid.ToString("N");
        }

        /// <summary>
        /// 没有分隔线的小写字符串
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string ToLowerNString(this Guid guid)
        {
            return guid.ToString("N").ToLower();
        }

        /// <summary>
        /// 没有分隔线的大写字符串
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string ToUpperNString(this Guid guid)
        {
            return guid.ToString("N").ToUpper();
        }

        #region 生成顺序GUID
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        /// <summary>
        /// 创建顺序GUID
        /// </summary>
        /// <returns></returns>
        public static Guid NewSequentialGuid()
        {
            SequentialGuidType sequentialGuidType;
            switch (Config.DatabaseType)
            {
                case "sqlserver":
                    sequentialGuidType = SequentialGuidType.SequentialAtEnd;
                    break;
                case "oracle":
                    sequentialGuidType = SequentialGuidType.SequentialAsBinary;
                    break;
                case "mySql":
                    sequentialGuidType = SequentialGuidType.SequentialAsString;
                    break;
                default:
                    sequentialGuidType = SequentialGuidType.SequentialAsString;
                    break;
            }

            var randomBytes = new byte[10];
            Rng.Locking(r => r.GetBytes(randomBytes));
            long timestamp = DateTime.UtcNow.Ticks / 10000L;
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }
            byte[] guidBytes = new byte[16];
            switch (sequentialGuidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);
                    if (sequentialGuidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }
                    break;
                case SequentialGuidType.SequentialAtEnd:
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }
            return new Guid(guidBytes);
        }

        private enum SequentialGuidType
        {
            /// <summary>
            /// The GUID should be sequential when formatted using the
            /// <see cref="Guid.ToString()" /> method.
            /// </summary>
            SequentialAsString,

            /// <summary>
            /// The GUID should be sequential when formatted using the
            /// <see cref="Guid.ToByteArray" /> method.
            /// </summary>
            SequentialAsBinary,

            /// <summary>
            /// The sequential portion of the GUID should be located at the end
            /// of the Data4 block.
            /// </summary>
            SequentialAtEnd
        }
        #endregion
    }
}

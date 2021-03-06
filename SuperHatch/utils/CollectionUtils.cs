// @author Zereao
// @date 2021-03-06 20:41
// @Steam https://steamcommunity/id/hexaiolun/

using System.Collections;

namespace SuperHatch.utils
{
    public class CollectionUtils
    {
        private CollectionUtils()
        {
        }

        /// <summary>
        /// 空ArrayList，全局统一，节省内存
        /// </summary>
        public static readonly ArrayList EmptyList = new ArrayList();

        /// <summary>
        /// 空Hashtable，全局统一，节省内存
        /// </summary>
        public static readonly Hashtable EmptyMap = new Hashtable();

        /// <summary>判断一个数组是否为空</summary>
        /// <param name="args">数组</param>
        /// <returns>true OR false</returns>
        public static bool IsEmpty(object[] args)
        {
            return args == null || args.Length == 0;
        }

        /// <summary>判断一个ArrayList是否为空</summary>
        /// <param name="list">数组</param>
        /// <returns>true OR false</returns>
        public static bool IsEmpty(ArrayList list)
        {
            return list == null || list.Count == 0;
        }
    }
}
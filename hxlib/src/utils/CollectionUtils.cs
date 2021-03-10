// @author Zereao
// @date 2021-03-10 21:56
// @Steam https://steamcommunity/id/hexaiolun/

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HXLib.utils
{
    public class CollectionUtils
    {
        /// <summary> 私有构造函数</summary>
        private CollectionUtils()
        {
        }

        /// <summary>空 List ，全局统一，节省内存</summary>
        public static List<T> EmptyList<T>()
        {
            return Enumerable.Empty<T>().ToList();
        }

        /// <summary>空Hashtable，全局统一，节省内存</summary>
        public static readonly Hashtable EmptyMap = new Hashtable();

        /// <summary>判断一个数组是否为空</summary>
        /// <param name="args">数组</param>
        /// <returns>true OR false</returns>
        public static bool IsEmpty(object[] args)
        {
            return args == null || args.Length == 0;
        }

        /// <summary>判断一个ICollection是否为空</summary>
        /// <param name="list">数组</param>
        /// <returns>true OR false</returns>
        public static bool IsEmpty(ICollection list)
        {
            return list == null || list.Count == 0;
        }

        /// <summary>判断一个 List 是否为空</summary>
        /// <param name="list">数组</param>
        /// <returns>true OR false</returns>
        public static bool IsEmpty<T>(List<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
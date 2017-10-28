﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace P2P
{
    public class JsonOperation<T> where T : new()
    {
        /// <summary>
        /// 多组Json反序列化
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static IList<T> DeserializeList(string strJson)
        {
            return JsonConvert.DeserializeObject<IList<T>>(strJson);
        }
        /// <summary>
        /// 单组Json反序列化
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T Deserialize(string strJson)
        {
            return JsonConvert.DeserializeObject<T>(strJson);
        }
        /// <summary>
        /// 序列化Json
        /// </summary>
        /// <param name="objJsong"></param>
        /// <returns></returns>
        public static string Serialize(T objJsong)
        {
            return JsonConvert.SerializeObject(objJsong);
        }
    }
    public class JsonOperation
    {
        /// <summary>
        /// 单组Json反序列化
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static object Deserialize(string strJson)
        {
            return JsonConvert.DeserializeObject<object>(strJson);
        }
        /// <summary>
        /// 多组Json反序列化
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static IList<object> DeserializeList(string strJson)
        {
            return JsonConvert.DeserializeObject<IList<object>>(strJson);
        }
        /// <summary>
        /// 序列化Json
        /// </summary>
        /// <param name="objJsong"></param>
        /// <returns></returns>
        public static string Serialize(object objJsong)
        {
            return JsonConvert.SerializeObject(objJsong);
        }
    }
}

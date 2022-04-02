using Newtonsoft.Json;

namespace BF1.ServerAdminTools.Common.Utils
{
    public static class JsonUtil
    {
        /// <summary>
        /// 反序列化，将json字符串转换成json类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static T JsonDese<T>(string result)
        {
            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// 序列化，将json类转换成json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonClass"></param>
        /// <returns></returns>
        public static string JsonSeri(object jsonClass)
        {
            return JsonConvert.SerializeObject(jsonClass);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace FubuMVC.ActivityStream
{
    public static class JsonUtil
    {
#pragma warning disable 618,612
        public static string ToJson(object objectToSerialize)
        {
            return new JavaScriptSerializer().Serialize(objectToSerialize);
        }

        /// <summary>
        ///   Allows you to use function names (via <see cref = "javascript.function" />) in the value of a property, which is against the JSON spec
        /// </summary>
        /// <param name = "objectToSerialize"></param>
        /// <returns></returns>
        public static string ToUnsafeJson(object objectToSerialize)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new JavascriptFunctionConverter() });
            var output = serializer.Serialize(objectToSerialize);
            const string pattern = @"\{""__jsfunction"":""(?<function>\w+)""}";
            return Regex.Replace(output, pattern, m => m.Groups["function"].Value);
        }

        public static T Get<T>(string rawJson)
        {
            return new JavaScriptSerializer().Deserialize<T>(rawJson);
        }

        public static object Get(Type type, string rawJson)
        {
            return new JavaScriptSerializer().Deserialize(rawJson, type);
        }

        public static T Get<T>(byte[] rawJson)
        {
            var jsonString = Encoding.Default.GetString(rawJson);
            return Get<T>(jsonString);
        }

        public static object Get(string rawJson)
        {
            return new JavaScriptSerializer().DeserializeObject(rawJson);
        }

        public class JavascriptFunctionConverter : JavaScriptConverter
        {
            public override IEnumerable<Type> SupportedTypes
            {
                get { return new[] { typeof(javascript.JavascriptFunction) }; }
            }

            public override object Deserialize(IDictionary<string, object> dictionary, Type type,
                                               JavaScriptSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                var dictionary = new Dictionary<string, object>();
                dictionary["__jsfunction"] = obj.ToString();
                return dictionary;
            }
        }

#pragma warning restore 618,612
    }
}
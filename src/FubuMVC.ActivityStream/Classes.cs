using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using FubuCore.Util;
using FubuCore.Reflection;
using System.Linq;
using FubuCore;

namespace FubuMVC.ActivityStream
{
    /// <summary>
    /// Just a marker interface
    /// </summary>
    public interface Activity{}


    // TODO -- probably want date on this later
    public interface IActivityItem
    {
        string Type { get; set; }
        string Json { get; set; }
    }

    public class ActivityItem : IActivityItem
    {
        public string Type { get; set; }

        public string Json { get; set; }
    }

    public static class ActivityTypes
    {
        private static readonly Cache<string, Type> _types = new Cache<string, Type>();

        public static void Register(Type activityType)
        {
            if (_types.GetAll().Contains(activityType)) return;

            var activityName = GetActivityName(activityType);
            if (_types.Has(activityName))
            {
                var message = "Trying to register ActivityType {0} with name {1} would cause a duplicate activity name".ToFormat(activityType.FullName, activityName);
                throw new ApplicationException(message);
            }

            _types[activityName] = activityType;
        }

        public static Type TypeForActivityName(string name)
        {
            return _types[name];
        }

        public static string GetActivityName(Type activityType)
        {
            var name = activityType.Name;
            activityType.ForAttribute<ActivityTypeNameAttribute>(att => name = att.Name);

            return name;
        }

        public static void Dehydrate<T>(ActivityItem item, T activity) where T : Activity
        {
            if (!_types.GetAll().Contains(typeof(T)))
            {
                Register(typeof(T));
            }

            var activityName = GetActivityName(typeof (T));
            item.Type = activityName;
            item.Json = JsonUtil.ToJson(activity);
        }


        public static object Hydrate(IActivityItem item)
        {
            var type = TypeForActivityName(item.Type);
            return JsonUtil.Get(type, item.Json);
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ActivityTypeNameAttribute : Attribute
    {
        private readonly string _name;

        public ActivityTypeNameAttribute(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }

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

    public static class javascript
    {
        public static JavascriptFunction function(string functionName)
        {
            return new JavascriptFunction(functionName);
        }

        #region Nested type: JavascriptFunction

        public class JavascriptFunction
        {
            private readonly string _functionName;

            public JavascriptFunction(string functionName)
            {
                _functionName = functionName;
            }

            public string javascriptFunction
            {
                get { return "You must use JsonUtil.ToUnsafeJson() to serialize this properly"; }
            }

            public override string ToString()
            {
                return _functionName;
            }
        }

        #endregion
    }
}
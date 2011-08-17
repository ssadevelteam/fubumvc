using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuCore.Reflection;
using FubuCore.Util;

namespace FubuMVC.ActivityStream
{
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

        public static IEnumerable<Type> AllActivityTypes
        {
            get
            {
                return _types.GetAll();
            }
        }
    }
}
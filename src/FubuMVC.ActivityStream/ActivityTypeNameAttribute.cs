using System;

namespace FubuMVC.ActivityStream
{
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
}
using FubuCore;
using NUnit.Framework;
using FubuTestingSupport;

namespace FubuMVC.ActivityStream.Testing
{
    [TestFixture]
    public class ActivityTypesTester
    {
        [Test]
        public void get_activity_name_for_a_type_with_no_attribute()
        {
            ActivityTypes.GetActivityName(typeof (FirstActivity)).ShouldEqual("FirstActivity");
        }

        [Test]
        public void get_activity_name_for_a_type_with_an_attribute()
        {
            ActivityTypes.GetActivityName(typeof (ThirdActivity)).ShouldEqual("Third");
        }

        [Test]
        public void register_and_fetch_activity_type()
        {
            ActivityTypes.Register(typeof(FirstActivity));
            ActivityTypes.TypeForActivityName("FirstActivity").ShouldEqual(typeof (FirstActivity));
        }

        [Test]
        public void dehydrate_and_hydrate_an_activity_item()
        {
            var item = new ActivityItem();
            var activity1 = new FirstActivity(){
                Name = "Max",
                Age = 7
            };

            ActivityTypes.Dehydrate(item, activity1);

            var activity2 = ActivityTypes.Hydrate(item).As<FirstActivity>();

            activity1.ShouldEqual(activity2);
        }
    }

    public class FirstActivity : Activity
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public bool Equals(FirstActivity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name) && other.Age == Age;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (FirstActivity)) return false;
            return Equals((FirstActivity) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ Age;
            }
        }
    }
    public class SecondActivity : Activity{}

    [ActivityTypeName("Third")]
    public class ThirdActivity : Activity{}

    
}
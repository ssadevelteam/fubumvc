using FubuCore;
using FubuTestingSupport;
using NUnit.Framework;

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
}
using FubuCore.Reflection;
using HtmlTags;
using NUnit.Framework;
using FubuTestingSupport;

namespace FubuMVC.ActivityStream.Testing
{
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

    public class NotAnActivity{}
    
    [TestFixture]
    public class ActivityStreamConventionTester
    {
        [Test]
        public void is_visualizer_method_positive_case()
        {
            ActivityStreamConvention.IsVisualizerMethod(ReflectionHelper.GetMethod<FirstActivityVisualizer>(x => x.Visualize(null)))
                .ShouldBeTrue();
        }

        [Test]
        public void is_visualizer_method_negative_with_too_many_inputs()
        {
            ActivityStreamConvention.IsVisualizerMethod(ReflectionHelper.GetMethod<FirstActivityVisualizer>(x => x.WrongWithTooManyParameters(null, 0)))
                .ShouldBeFalse();
        }

        [Test]
        public void is_visualizer_method_negative_with_not_an_activity_input()
        {
            ActivityStreamConvention.IsVisualizerMethod(ReflectionHelper.GetMethod<FirstActivityVisualizer>(x => x.WrongBecauseTheInputIsNotAnActivity(null)))
                .ShouldBeFalse();
        }

        [Test]
        public void is_visualizer_method_negative_when_there_is_no_output()
        {
            ActivityStreamConvention.IsVisualizerMethod(ReflectionHelper.GetMethod<FirstActivityVisualizer>(x => x.WrongBecauseThereIsNotOutput(null)))
                .ShouldBeFalse();  
        }
    }
    
    public class FirstActivityVisualizer
    {
        public HtmlTag Visualize(FirstActivity activity)
        {
            return new HtmlTag("div");
        }

        public object WrongWithTooManyParameters(FirstActivity activity, int count)
        {
            return null;
        }

        public object WrongBecauseTheInputIsNotAnActivity(NotAnActivity not)
        {
            return null;
        }

        public void WrongBecauseThereIsNotOutput(FirstActivity activity)
        {
            
        }
    }
}
using FubuCore.Reflection;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.ActivityStream.Testing
{
    [TestFixture]
    public class ActivityVisualizerActionSourceTester
    {
        [Test]
        public void is_visualizer_method_positive_case()
        {
            ActivityVisualizerActionSource.IsVisualizerMethod(ReflectionHelper.GetMethod<FirstActivityVisualizer>(x => x.Visualize(null)))
                .ShouldBeTrue();
        }

        [Test]
        public void is_visualizer_method_negative_with_too_many_inputs()
        {
            ActivityVisualizerActionSource.IsVisualizerMethod(ReflectionHelper.GetMethod<FirstActivityVisualizer>(x => x.WrongWithTooManyParameters(null, 0)))
                .ShouldBeFalse();
        }

        [Test]
        public void is_visualizer_method_negative_with_not_an_activity_input()
        {
            ActivityVisualizerActionSource.IsVisualizerMethod(ReflectionHelper.GetMethod<FirstActivityVisualizer>(x => x.WrongBecauseTheInputIsNotAnActivity(null)))
                .ShouldBeFalse();
        }

        [Test]
        public void is_visualizer_method_negative_when_there_is_no_output()
        {
            ActivityVisualizerActionSource.IsVisualizerMethod(ReflectionHelper.GetMethod<FirstActivityVisualizer>(x => x.WrongBecauseThereIsNotOutput(null)))
                .ShouldBeFalse();  
        }

        [Test]
        public void is_visualizer_class_positive()
        {
            ActivityVisualizerActionSource.IsVisualizerType(typeof(FirstActivityVisualizer))
                .ShouldBeTrue();
        }

        [Test]
        public void is_visualizer_type_negative_when_its_abstract()
        {
            ActivityVisualizerActionSource.IsVisualizerType(typeof(AbstractVisualizer))
                .ShouldBeFalse();
        }


    }

    public interface AbstractVisualizer{}
}
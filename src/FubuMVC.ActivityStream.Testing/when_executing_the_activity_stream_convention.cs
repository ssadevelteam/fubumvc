using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;
using SpecificationExtensions = FubuTestingSupport.SpecificationExtensions;

namespace FubuMVC.ActivityStream.Testing
{
    [TestFixture]
    public class when_executing_the_activity_stream_convention
    {
        private BehaviorGraph theGraph;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Applies.ToThisAssembly();
            registry.RegisterActivityStreamHandlers();
            theGraph = registry.BuildGraph();
        }

        [Test]
        public void got_all_the_visualizer_actions()
        {
            theGraph.Behaviors.Count().ShouldEqual(3);
            theGraph.BehaviorFor<FirstActivityVisualizer>(x => x.Visualize(null))
                .ShouldNotBeNull();

            theGraph.BehaviorFor<SecondVisualizer>(x => x.Show(null)).ShouldNotBeNull();
            theGraph.BehaviorFor<SecondVisualizer>(x => x.Visualize(null)).ShouldNotBeNull();
        }

        [Test]
        public void the_visualizers_found_get_output_nodes()
        {
            theGraph.BehaviorFor<FirstActivityVisualizer>(x => x.Visualize(null))
                .Outputs.Any().ShouldBeTrue();
        }

        [Test]
        public void should_register_all_the_activity_types_it_finds()
        {
            ActivityTypes.AllActivityTypes
                .OrderBy(x => x.Name).ShouldHaveTheSameElementsAs(typeof(FirstActivity), typeof(SecondActivity), typeof(ThirdActivity));
        }

        [Test]
        public void should_register_all_the_possible_activity_streams()
        {
            theGraph.Services.ServicesFor(typeof(IActivityStream<Site>))
                .Select(x => x.Type).ShouldHaveTheSameElementsAs(typeof(SiteStream));

            theGraph.Services.ServicesFor(typeof(IActivityStream<Case>))
                .Select(x => x.Type).ShouldHaveTheSameElementsAs(typeof(CaseStream));

            theGraph.Services.ServicesFor(typeof(IActivityStream<Contact>))
                .Select(x => x.Type).ShouldHaveTheSameElementsAs(typeof(ContactStream));
        }
    }
}
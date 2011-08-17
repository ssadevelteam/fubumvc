using System;
using System.Collections.Generic;
using FubuCore;
using FubuMVC.Core.Registration;
using TypeExtensions = FubuCore.TypeExtensions;

namespace FubuMVC.ActivityStream
{
    public class ActivityStreamConvention : IConfigurationAction
    {
        private readonly Lazy<TypePool> _types;

        public ActivityStreamConvention(Lazy<TypePool> types)
        {
            _types = types;
        }

        public void Configure(BehaviorGraph graph)
        {
            var pool = _types.Value;
            findAllActivityTypes(pool);
            findAllActivityStreams(pool, graph);
        }

        private static void findAllActivityStreams(TypePool pool, BehaviorGraph registry)
        {
            pool.TypesMatching(t => t.Closes(typeof(IActivityStream<>))).Each(t =>
            {
                var @interface = TypeExtensions.FindInterfaceThatCloses(t, typeof(IActivityStream<>));
                registry.Services.SetServiceIfNone(@interface, t);
            });
        }

        private static void findAllActivityTypes(TypePool pool)
        {
            pool.TypesMatching(t => t.IsConcreteTypeOf<Activity>()).Each(ActivityTypes.Register);
        }
    }
}
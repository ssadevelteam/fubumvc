using System;
using Bottles;
using FubuMVC.Core;
using FubuMVC.Core.Registration;

namespace FubuMVC.ActivityStream
{

    public static class ActivityStreamFubuRegistryExtension
    {
        public static void RegisterActivityStreamHandlers(this FubuRegistry registry)
        {
            var pool = new Lazy<TypePool>(() => BuildTypePool(registry.Types));
            registry.Actions.FindWith(new ActivityVisualizerActionSource(pool));
            registry.ApplyConvention(new ActivityStreamConvention(pool));
        }

        public static TypePool BuildTypePool(TypePool original)
        {
            var pool = new TypePool(null)
            {
                ShouldScanAssemblies = true
            };
            pool.AddAssemblies(original.Assemblies);
            pool.AddAssemblies(PackageRegistry.PackageAssemblies);
            return pool;
        }
    }


    
}
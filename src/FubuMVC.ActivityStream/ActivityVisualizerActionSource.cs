using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FubuCore;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using TypeExtensions = FubuCore.TypeExtensions;

namespace FubuMVC.ActivityStream
{
    public class ActivityVisualizerActionSource : IActionSource
    {
        private readonly Lazy<TypePool> _pool;

        public ActivityVisualizerActionSource(Lazy<TypePool> pool)
        {
            _pool = pool;
        }

        public static bool IsVisualizerMethod(MethodInfo method)
        {
            if (method.GetParameters().Length != 1) return false;
            if (method.ReturnType == typeof(void)) return false;

            return TypeExtensions.CanBeCastTo<Activity>(method.GetParameters().Single().ParameterType);
        }

        public static bool IsVisualizerType(Type type)
        {
            return type.IsConcrete() && type.Name.EndsWith("Visualizer");
        }

        public IEnumerable<ActionCall> FindActions(TypePool types)
        {
            foreach (var type in _pool.Value.TypesMatching(IsVisualizerType))
            {
                foreach (var method in type.GetMethods().Where(IsVisualizerMethod))
                {
                    yield return new ActionCall(type, method);
                }
            }
        }

    }
}
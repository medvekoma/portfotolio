using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace Portfotolio.DependencyInjection.Unity
{
    public class UnityDecoratorBuildStrategy : BuilderStrategy
    {
        private readonly Dictionary<Type, List<Type>> _typeStacks;

        public UnityDecoratorBuildStrategy(Dictionary<Type, List<Type>> typeStacks)
        {
            _typeStacks = typeStacks;
        }

        public override void PreBuildUp(IBuilderContext context)
        {
            NamedTypeBuildKey key = context.OriginalBuildKey;

            if (!(key.Type.IsInterface && _typeStacks.ContainsKey(key.Type)))
            {
                return;
            }

            if (null != context.GetOverriddenResolver(key.Type))
            {
                return;
            }

            var stack = new Stack<Type>(_typeStacks[key.Type]);
            object value = null;
            stack.ForEach(type =>
            {
                value = context.NewBuildUp(new NamedTypeBuildKey(type, key.Name));
                var overrides = new DependencyOverride(key.Type, value);
                context.AddResolverOverrides(overrides);
            }
                );

            context.Existing = value;
            context.BuildComplete = true;
        }
    }
}

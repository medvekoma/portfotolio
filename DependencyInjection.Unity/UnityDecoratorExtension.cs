using System;
using System.Collections.Generic;
using Unity.Extension;
using System.Linq;
using Unity.Events;
using Unity.Builder;

namespace Portfotolio.DependencyInjection.Unity
{
    public sealed class UnityDecoratorExtension : UnityContainerExtension
    {
        private Dictionary<Type, List<Type>> _typeStacks;

        protected override void Initialize()
        {
            _typeStacks = new Dictionary<Type, List<Type>>();
            Context.Registering += ContextRegistering;
            Context.Strategies.Add(new UnityDecoratorBuildStrategy(_typeStacks), UnityBuildStage.PreCreation);
        }

        private void ContextRegistering(object sender, RegisterEventArgs e)
        {
            if (e.TypeFrom == null || !e.TypeFrom.IsInterface)
            {
                return;
            }

            List<Type> stack;
            if (!_typeStacks.ContainsKey(e.TypeFrom))
            {
                stack = new List<Type>();
                _typeStacks.Add(e.TypeFrom, stack);
            }
            else
            {
                stack = _typeStacks[e.TypeFrom];
            }

            stack.Add(e.TypeTo);
        }

        public void Replace<TService, TImplementation>()
              where TImplementation : class, TService
        {
            var serviceType = typeof(TService);
            var mapping = _typeStacks.FirstOrDefault(type => type.Key == serviceType);
            if (mapping.Value != null)
            {
                mapping.Value.Clear();
                mapping.Value.Add(typeof(TImplementation));
            }
            else
            {
                _typeStacks[serviceType] = new List<Type> { typeof(TImplementation) };
            }
        }
    }
}
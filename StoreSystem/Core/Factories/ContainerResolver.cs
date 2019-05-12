using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using Autofac;

namespace WIMSystem.Core.Factories
{
    public class ContainerResolver<T>:IContainerResolver<T>
    {
        private readonly IComponentContext container;

        public ContainerResolver(IComponentContext container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public T GetService(string namedService)
        {
            return container.ResolveNamed<T>(namedService);
        }
    }
}

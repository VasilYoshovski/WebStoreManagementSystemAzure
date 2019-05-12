using Autofac;
using System;
using WIMSystem.ContainerModules;
using WIMSystem.Core;
using WIMSystem.Core.Factories;
using WIMSystem.Menu;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem
{
    internal class Program
    {
        private static void Main()
        {
            

            var builder = new ContainerBuilder();

            builder.RegisterModule(new MainModule());
            builder.RegisterModule(new CommandsModule());


            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var engine = scope.Resolve<Engine>();
                engine.Start();
            }
        }
    }
}

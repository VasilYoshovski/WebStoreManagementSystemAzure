using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Core;
using WIMSystem.Core.Contracts;
using WIMSystem.Core.Factories;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Menu;
using WIMSystem.Menu.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.ContainerModules
{
    class MainModule : Module

    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ContainerResolver<>)).As(typeof(IContainerResolver<>));

            builder.RegisterType<ComponentsFactory>().As<IComponentsFactory>().SingleInstance();
            builder.RegisterType<WIMTeams>().As<IWIMTeams>().SingleInstance();
            builder.RegisterType<PersonsCollection>().As<IPersonsCollection>().SingleInstance();
            builder.RegisterType<HistoryItemsCollection>().As<IHistoryItemsCollection>().SingleInstance();
            builder.RegisterType<HistoryEventWriter>().As<IHistoryEventWriter>().SingleInstance();
           // builder.RegisterType<WIMEngine>().As<IWIMEngine>().SingleInstance();
            builder.RegisterType<MainMenu>().As<IMainMenu>().SingleInstance();
           // builder.RegisterType<ConsoleCommandParser>().As<ICommandParser>().SingleInstance();
            builder.RegisterType<PrintReports>().As<IPrintReports>().SingleInstance();
            builder.RegisterType<ConsoleWriter>().As<IWriter>().SingleInstance();
            builder.RegisterType<CommandParser>().As<ICommandParser>().SingleInstance();
            builder.RegisterType<MenuReader>().As<IReader>().SingleInstance();
            builder.RegisterType<Engine>().AsSelf();
        }
    }
}

using System;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Contracts;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Menu.Contracts;
using WIMSystem.Utils;

namespace WIMSystem.Core
{
    internal class Engine
    {
        private readonly ICommandsFactory commandsFactory;
        private readonly IPrintReports printReports;
        private readonly IMainMenu mainMenu;
        private readonly IReader reader;
        private readonly ICommandParser parser;

        public IReader ConsoleReader { get; }

        public Engine(ICommandsFactory commandsFactory,
            IPrintReports printReports,
            IMainMenu mainMenu,
            ICommandParser parser,
            IReader reader)
        {
            this.commandsFactory = commandsFactory ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(commandsFactory)));
            this.printReports = printReports ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(printReports)));
            this.mainMenu = mainMenu ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(mainMenu)));
            this.reader = reader;
            this.parser = parser;
        }

        public void Start()
        {
            this.mainMenu.ShowLogo();

            while (true)
            {

                var myReader = this.mainMenu.InputTypeChooser();
                if (myReader == null)
                {
                    this.mainMenu.ShowCredits();
                    return;
                }
                try
                {
                    var inputStringList = myReader.Read();

                    foreach (var inputString in inputStringList)
                    {
                        if (inputString.ToLower() == CommandsConsts.TerminationAppCommand)
                        {
                            this.mainMenu.ShowCredits();
                            return;
                        }

                        if (inputString == CommandsConsts.ConsoleExitCommand)
                        {
                            this.printReports.Print();
                            break;
                        }

                        var command = this.parser.Parse(inputString);
                        var engineCommand = this.commandsFactory.GetCommand(command.Name);
                        var report = engineCommand.Execute(command.Parameters);
                        this.printReports.Reports.Add(report);
                    }
                }
                catch (Exception ex)
                {
                    this.printReports.Reports.Add(ex.Message);
                    this.printReports.Print();
                }
            }
        }
    }
}

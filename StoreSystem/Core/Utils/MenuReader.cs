using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Contracts;
using WIMSystem.Menu;
using WIMSystem.Menu.Contracts;

namespace WIMSystem.Core.Utils
{
    internal class MenuReader : IReader
    {
        private readonly IMainMenu mainMenu;

        public MenuReader(IMainMenu mainMenu)
        {
            this.mainMenu = mainMenu;
        }
        public ICollection<string> Read()
        {
            return new List<string>()
            {
                this.mainMenu.ShowMenu(MainMenuItems.mainMenuItems),
                CommandsConsts.ConsoleExitCommand
            };
        }
    }
}

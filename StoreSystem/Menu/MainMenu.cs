using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Core;
using WIMSystem.Core.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Menu.Contracts;

namespace WIMSystem.Menu
{
    public class MainMenu : IMainMenu
    {
        public MainMenu()
        {
        }

        public void ShowLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(MainMenuLogo.logo);
            Console.WriteLine("Press any key for main menu...");
            Console.ResetColor();

            Console.ReadKey();

        }

        public void ShowCredits()
        {
            Console.Clear();
            Console.WriteLine(MainMenuLogo.logo);
            Console.WriteLine("                 Thank you!");
        }

        public string ShowMenu(IList<MenuItem> mainMenuItems)
        {
            int curItem = 0;
            int c = 0;

            ConsoleKeyInfo key;

            var titleString = mainMenuItems[0].MenuText;
            string[] menuItems = mainMenuItems.Skip(1).Select(x => x.MenuText).ToArray();
            do
            {
                Console.Clear();

                Console.WriteLine("Work Item Management (WIM) Console Application");
                Console.WriteLine(titleString);
                Console.WriteLine();

                for (c = 0; c < menuItems.Length; c++)
                {
                    if (curItem == c)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(menuItems[c]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuItems[c]);
                    }
                }

                Console.Write("Select your choice with the arrow keys.");
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow)
                {
                    curItem++;
                    if (curItem > menuItems.Length - 1) curItem = menuItems.Length - 1;
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    curItem--;
                    if (curItem < 0) curItem = 0;
                }

            } while (key.Key != ConsoleKey.Enter);

            if (curItem != menuItems.Length - 1)
            {
                   return  this.ConsoleParameters(curItem+1, mainMenuItems);
            }

            return mainMenuItems[mainMenuItems.Count-1].CommandText;
        }
        
        public string ConsoleParameters(int indexOfItem, IList<MenuItem> mainMenuItems)
        {
            Console.Clear();

            if (!string.IsNullOrEmpty(mainMenuItems[indexOfItem].ParamsText))
            {
                Console.WriteLine(mainMenuItems[indexOfItem].ParamsText);

                var parameters = Console.ReadLine();

                return string.Concat(
                    mainMenuItems[indexOfItem].CommandText,
                    "\" ",
                    parameters);
            }
            else
            {
                return mainMenuItems[indexOfItem].CommandText;
            }
        }

        public IReader InputTypeChooser()
        {
            var result = ShowMenu(MainMenuItems.InputTypeItems);
            switch (result)
            {
                case "MenuCommands": return new MenuReader(this);
                case "BatchCommands": return new ConsoleReader();
                case "AppExit": return null;
                default:
                    throw new ArgumentException("No such type");
            }
        }
    }
}

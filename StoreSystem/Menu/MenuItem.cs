using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Menu.Contracts;

namespace WIMSystem.Menu
{
    public class MenuItem : IMenuItem
    {
        string commandText;
        string menuText;
        string paramsText;

        public MenuItem(string commandText, string menuText, string paramsText)
        {
            this.CommandText = commandText;
            this.MenuText = menuText;
            this.ParamsText = paramsText;
        }

        public string CommandText
        {
            get
            {
                return this.commandText;
            }

            set
            {
                this.commandText = value;
            }
        }

        public string MenuText
        {
            get
            {
                return this.menuText;
            }

            set
            {
                this.menuText = value;
            }
        }

        public string ParamsText
        {
            get
            {
                return this.paramsText;
            }

            set
            {
                this.paramsText = value;
            }
        }
    }
}

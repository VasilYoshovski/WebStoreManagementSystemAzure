using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Commands.Utils
{
    internal class PrintReports : IPrintReports
    {
        private readonly IWriter writer;
        private IList<string> reports;

        public PrintReports(IWriter writer)
        {
            this.writer = writer ?? throw new ArgumentNullException(string.Format(CommandsConsts.NULL_OBJECT, nameof(writer)));
            reports = new List<string>();
        }

        public IList<string> Reports
        {
            get
            {
                return this.reports;
            }

            set
            {
                this.reports = value;
            }
        }

        public void Print()
        {
            if (this.Reports.Count != 0)
            {
                var output = new StringBuilder();

                foreach (var report in this.Reports)
                {
                    output.AppendLine(report);
                }

                this.writer.Write(output.ToString());
                this.Reports.Clear();
            }
        }
    }
}

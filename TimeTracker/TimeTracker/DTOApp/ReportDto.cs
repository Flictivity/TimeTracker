using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DTOApp
{
    public class ReportDto
    {
        public string CategoryName { get; set; }
        public TimeSpan Time { get; set; }
    }
}

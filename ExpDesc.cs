using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMyCourseWork
{
    class ExpDesc
    {
        public string typeOfAction { get; set; }
        public string note { get; set; }
        public double countOfAction { get; set; }
        public string actionStringCount { get; set; }
        public DateTime expDate = DateTime.Now;

        public string[] months = {"Январь", "Февраль", "Март", "Апрель", "Май",
                    "Июнь", "Июль","Август",  "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"};

        public string getAString()
        {
            return "-" + countOfAction + " Br";
        }

        public string getAStringInc()
        {
            return "+" + countOfAction + " Br";
        }

        public string getAStringDate()
        {
            return expDate.Day + " " + months[expDate.Month - 1] + " " + expDate.Year;
        }

    }
}

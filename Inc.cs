using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMyCourseWork
{
    class Inc
    {
        public double allInc;
        public double salary { get; set; }
        public double freelance { get; set; }
        public double sell { get; set; }
        public double rent { get; set; }
        
        public DateTime dateOfCreation = DateTime.Now;

        public string sumAll()
        {
            allInc = salary + freelance + sell + rent;
            return allInc.ToString() + " Br";
        }

        private void NullTheObj()
        {
            salary = 0;
            freelance = 0;
            sell = 0;
            rent = 0;
            
        }

        /*CarExpLabel, HouseExpLabel, sportExpLabel, EatExpLabel, RestaurantExpLabel, HobbiesExpLabel, BuyExpLabel, HealthExpLabel, allExpValue*/

        public void showIncOnMain(System.Windows.Controls.Label a, System.Windows.Controls.Label b,
            System.Windows.Controls.Label c, System.Windows.Controls.Label d, System.Windows.Controls.Label e, System.Windows.Controls.Label f)
        {
            a.Content = salary + " Br";
            b.Content = freelance + " Br";
            c.Content = sell + " Br";
            d.Content = rent + " Br";
            e.Content = sumAll();
            f.Content = sumAll();
        }
    }
}

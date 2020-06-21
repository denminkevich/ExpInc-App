using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfMyCourseWork
{
    class Exp
    {
        /*public double car, house, sport, food, restautant, hobbie, things, health;*/

        public double allExp;
        public double car { get; set; }
        public double house { get; set; }
        public double sport { get; set; }
        public double eat { get; set; }
        public double restaurant { get; set; }
        public double hobbies { get; set; }
        public double buy { get; set; }
        public double health { get; set; }
        public DateTime dateOfCreation = DateTime.Now;

        public string sumAll()
        {
            allExp = car + house + sport + eat + restaurant + hobbies + buy + health;
            return allExp.ToString() + " Br";
        }

        private void NullTheObj()
        {
            car = 0;
            house = 0;
            sport = 0;
            eat = 0;
            restaurant = 0;
            hobbies = 0;
            buy = 0;
            health = 0;
        }

        /*CarExpLabel, HouseExpLabel, sportExpLabel, EatExpLabel, RestaurantExpLabel, HobbiesExpLabel, BuyExpLabel, HealthExpLabel, allExpValue*/

        public void showExpsOnMain(System.Windows.Controls.Label a, System.Windows.Controls.Label b,
            System.Windows.Controls.Label c, System.Windows.Controls.Label d, System.Windows.Controls.Label e,
            System.Windows.Controls.Label f, System.Windows.Controls.Label g, System.Windows.Controls.Label h,
            System.Windows.Controls.Label k, System.Windows.Controls.Label m)
        {
            a.Content = car + " Br";
            b.Content = house + " Br";
            c.Content = sport + " Br";
            d.Content = eat + " Br";
            e.Content = restaurant + " Br";
            f.Content = hobbies + " Br";
            g.Content = buy + " Br";
            h.Content = health + " Br";
            k.Content = sumAll();
            m.Content = sumAll();
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMyCourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        monthsExp AllMonthExp = File.Exists("MonthsExp.json") ? JsonConvert.DeserializeObject<monthsExp>(File.ReadAllText("MonthsExp.json")) : new monthsExp();
        monthsInc AllMonthInc = File.Exists("MonthsInc.json") ? JsonConvert.DeserializeObject<monthsInc>(File.ReadAllText("MonthsInc.json")) : new monthsInc();
        /*10 последних действий*/
        object[] LastActions = File.Exists("Actions.json") ? JsonConvert.DeserializeObject<object[]>(File.ReadAllText("Actions.json")) : new object[7];

        Exp AllExp = new Exp();
        public string[] months = {"Январь", "Февраль", "Март", "Апрель", "Май",
                    "Июнь", "Июль","Август",  "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"};

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try {
                DateTime now = DateTime.Now;

                if (Convert.ToInt32(AllMonthExp.currentMonth) != now.Month)
                {
                    AllMonthExp.currentMonth = now.Month;

                    AllMonthExp.monthCount += 1;

                    AllMonthExp.monthObjectsList.Add(AllExp);
                    AllMonthInc.monthObjectsList.Add(AllInc);

                    CurMonth.Content = months[now.Month - 1].ToString() + ' ' + now.Year;
                    AllExp.showExpsOnMain(CarExpLabel, HouseExpLabel, sportExpLabel, EatExpLabel,
                        RestaurantExpLabel, HobbiesExpLabel, BuyExpLabel, HealthExpLabel, allExpValue,
                        allExpValueIncExp);
                    AllInc.showIncOnMain(SalaryIncLabel, FreelanceIncLabel, SellIncLabel, RentIncLabel,
                        allExpValueIncomes, allIncValue);

                    File.WriteAllText("MonthsExp.json", JsonConvert.SerializeObject(AllMonthExp));
                    File.WriteAllText("MonthsInc.json", JsonConvert.SerializeObject(AllMonthInc));
                }
                else
                {
                    AllExp = JsonConvert.DeserializeObject<Exp>(AllMonthExp.monthObjectsList.Last().ToString());
                    AllInc = JsonConvert.DeserializeObject<Inc>(AllMonthInc.monthObjectsList.Last().ToString());

                    AllExp.showExpsOnMain(CarExpLabel, HouseExpLabel, sportExpLabel, EatExpLabel,
                        RestaurantExpLabel, HobbiesExpLabel, BuyExpLabel, HealthExpLabel, allExpValue,
                        allExpValueIncExp);
                    AllInc.showIncOnMain(SalaryIncLabel, FreelanceIncLabel, SellIncLabel, RentIncLabel,
                        allExpValueIncomes, allIncValue);

                    CurMonth.Content = months[AllExp.dateOfCreation.Month - 1].ToString() + ' ' + AllExp.dateOfCreation.Year;
                }
                LightArrows();
            } catch
            {
                MessageBox.Show("Сбой работы программы");
            }
            
        }

        private void LightArrows()
        {
            if (AllMonthExp.monthCount == AllMonthExp.monthObjectsList.Count() - 1)
            {
                NextMonth.Opacity = 0.5;
            }
            else
            {
                NextMonth.Opacity = 1;
            }

            if (AllMonthExp.monthCount == 0)
            {
                PrevMonth.Opacity = 0.5;
            }
            else
            {
                PrevMonth.Opacity = 1;
            }
        }

        /*Переключение между доходами и расходами*/

        private void Expenses_Click(object sender, RoutedEventArgs e)
        {
            AllMonthExp.monthObjectsList[AllMonthExp.monthCount] = AllExp;
            AllMonthInc.monthObjectsList[AllMonthExp.monthCount] = AllInc;
            File.WriteAllText("MonthsInc.json", JsonConvert.SerializeObject(AllMonthInc));

            IncDiagram.Visibility = Visibility.Hidden;
            ExpDiagram.Visibility = Visibility.Visible;

            IncCatagories.Visibility = Visibility.Hidden;
            ExpCatagories.Visibility = Visibility.Visible;
        }

        private void Incomes_Click(object sender, RoutedEventArgs e)
        {
            AllMonthExp.monthObjectsList[AllMonthExp.monthCount] = AllExp;
            AllMonthInc.monthObjectsList[AllMonthExp.monthCount] = AllInc;
            File.WriteAllText("MonthsExp.json", JsonConvert.SerializeObject(AllMonthExp));

            ExpDiagram.Visibility = Visibility.Hidden;
            IncDiagram.Visibility = Visibility.Visible;

            ExpCatagories.Visibility = Visibility.Hidden;
            IncCatagories.Visibility = Visibility.Visible;
        }

        /*Стрелочки переключения месяца*/

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            if (AllMonthExp.monthCount < AllMonthExp.monthObjectsList.Count() - 1)
            {
                AllMonthExp.monthObjectsList[AllMonthExp.monthCount] = AllExp;
                AllMonthInc.monthObjectsList[AllMonthExp.monthCount] = AllInc;

                File.WriteAllText("MonthsExp.json", JsonConvert.SerializeObject(AllMonthExp));
                File.WriteAllText("MonthsInc.json", JsonConvert.SerializeObject(AllMonthInc));

                AllMonthExp = JsonConvert.DeserializeObject<monthsExp>(File.ReadAllText("MonthsExp.json"));
                AllMonthInc = JsonConvert.DeserializeObject<monthsInc>(File.ReadAllText("MonthsInc.json"));
                AllMonthExp.monthCount++;

                AllExp = JsonConvert.DeserializeObject<Exp>(AllMonthExp.monthObjectsList[AllMonthExp.monthCount].ToString());
                AllInc = JsonConvert.DeserializeObject<Inc>(AllMonthInc.monthObjectsList[AllMonthExp.monthCount].ToString());

                AllExp.showExpsOnMain(CarExpLabel, HouseExpLabel, sportExpLabel, EatExpLabel,
                    RestaurantExpLabel, HobbiesExpLabel, BuyExpLabel, HealthExpLabel, allExpValue,
                    allExpValueIncExp);
                AllInc.showIncOnMain(SalaryIncLabel, FreelanceIncLabel, SellIncLabel, RentIncLabel,
                    allExpValueIncomes, allIncValue);

                CurMonth.Content = months[AllExp.dateOfCreation.Month - 1].ToString() + ' ' + AllExp.dateOfCreation.Year;

                File.WriteAllText("MonthsExp.json", JsonConvert.SerializeObject(AllMonthExp));

                LightArrows();
            }

        }

        private void PrevMonth_Click(object sender, RoutedEventArgs e)
        {
            if (AllMonthExp.monthCount > 0)
            {
                AllMonthExp.monthObjectsList[AllMonthExp.monthCount] = AllExp;
                AllMonthInc.monthObjectsList[AllMonthExp.monthCount] = AllInc;

                File.WriteAllText("MonthsExp.json", JsonConvert.SerializeObject(AllMonthExp));
                File.WriteAllText("MonthsInc.json", JsonConvert.SerializeObject(AllMonthInc));

                AllMonthExp = JsonConvert.DeserializeObject<monthsExp>(File.ReadAllText("MonthsExp.json"));
                AllMonthInc = JsonConvert.DeserializeObject<monthsInc>(File.ReadAllText("MonthsInc.json"));
                AllMonthExp.monthCount--;

                AllExp = JsonConvert.DeserializeObject<Exp>(AllMonthExp.monthObjectsList[AllMonthExp.monthCount].ToString());
                AllInc = JsonConvert.DeserializeObject<Inc>(AllMonthInc.monthObjectsList[AllMonthExp.monthCount].ToString());

                AllExp.showExpsOnMain(CarExpLabel, HouseExpLabel, sportExpLabel, EatExpLabel,
                    RestaurantExpLabel, HobbiesExpLabel, BuyExpLabel, HealthExpLabel, allExpValue,
                    allExpValueIncExp);
                AllInc.showIncOnMain(SalaryIncLabel, FreelanceIncLabel, SellIncLabel, RentIncLabel,
                    allExpValueIncomes, allIncValue);

                CurMonth.Content = months[AllExp.dateOfCreation.Month - 1].ToString() + ' ' + AllExp.dateOfCreation.Year;
                File.WriteAllText("MonthsExp.json", JsonConvert.SerializeObject(AllMonthExp));

                LightArrows();
            }

        }

        private void CloseMainWind(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AllMonthExp.monthObjectsList[AllMonthExp.monthCount] = AllExp;
            AllMonthInc.monthObjectsList[AllMonthExp.monthCount] = AllInc;

            AllMonthExp.monthCount = AllMonthExp.monthObjectsList.Count - 1;
            File.WriteAllText("MonthsExp.json", JsonConvert.SerializeObject(AllMonthExp));
            File.WriteAllText("MonthsInc.json", JsonConvert.SerializeObject(AllMonthInc));
            File.WriteAllText("Actions.json", JsonConvert.SerializeObject(LastActions));
        }

        private void LastOperBtn_Click(object sender, RoutedEventArgs e)
        {
            lastOper.IsSelected = true;
            showActions();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddExpWindow.Visibility = Visibility.Hidden;
        }

        private void ShowAddExpWindow()
        {
            makeFalseBtn();
            makeOpacityColor();
            expFormField.Text = "Расход Br";
            noteFormField.Text = "Примечание";
            AddExpWindow.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowAddExpWindow();
        }


        /* Выбор категории расхода */
        private bool[] radioBtn = new bool[8];

        public void makeFalseBtn()
        {
            for (int i = 0; i < radioBtn.Length; i++)
            {
                if (radioBtn[i] == true)
                {
                    radioBtn[i] = false;
                }
            }
        }

        public void makeOpacityColor()
        {
            RadioBtnCar.Opacity = 1;
            RadioBtnHouse.Opacity = 1;
            RadioBtnSport.Opacity = 1;
            RadioBtnEat.Opacity = 1;
            RadioBtnBuy.Opacity = 1;
            RadioBtnHealth.Opacity = 1;
            RadioBtnRestaurant.Opacity = 1;
            RadioBtnHobbies.Opacity = 1;
        }

        private void RadioBtnCar_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtn();
            radioBtn[0] = true;
            makeOpacityColor();
            RadioBtnCar.Opacity = 0.55;
        }

        private void RadioBtnHouse_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtn();
            radioBtn[1] = true;
            makeOpacityColor();
            RadioBtnHouse.Opacity = 0.55;
        }

        private void RadioBtnSport_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtn();
            radioBtn[2] = true;
            makeOpacityColor();
            RadioBtnSport.Opacity = 0.55;
        }

        private void RadioBtnEat_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtn();
            radioBtn[3] = true;
            makeOpacityColor();
            RadioBtnEat.Opacity = 0.55;
        }

        private void RadioBtnBuy_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtn();
            radioBtn[4] = true;
            makeOpacityColor();
            RadioBtnBuy.Opacity = 0.55;
        }

        private void RadioBtnHealth_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtn();
            radioBtn[5] = true;
            makeOpacityColor();
            RadioBtnHealth.Opacity = 0.55;
        }

        private void RadioBtnRestaurant_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtn();
            radioBtn[6] = true;
            makeOpacityColor();
            RadioBtnRestaurant.Opacity = 0.55;
        }

        private void RadioBtnHobbies_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtn();
            radioBtn[7] = true;
            makeOpacityColor();
            RadioBtnHobbies.Opacity = 0.55;
        }

        /*Продолжение формы*/

        private void isMouseEnter(object sender, RoutedEventArgs e)
        {
            if (expFormField.Text == "Расход Br")
            {
                expFormField.Text = "";
            }

        }

        private void CheckIsANum()
        {
            double n;
            bool isNumeric = double.TryParse(expFormField.Text, out n);
            if (expFormField.Text == "")
            {
                expFormField.Text = "Расход Br";
                MessageBox.Show("Введите число");
            }
            else if (isNumeric == false)
            {
                expFormField.Text = "";
                MessageBox.Show("Введите число");
            }
        }

        private void isMouseOut(object sender, RoutedEventArgs e)
        {
            CheckIsANum();
        }

        private void isMouseOnNote(object sender, RoutedEventArgs e)
        {
            if (noteFormField.Text == "Примечание")
            {
                noteFormField.Text = "";
            }

        }

        private void isMouseOutNote(object sender, RoutedEventArgs e)
        {
            if (noteFormField.Text == "")
            {
                noteFormField.Text = "Примечание";
            }

        }

        public void addingNewAction()
        {
            for (int i = 0; i < LastActions.Length; i++)
            {
                int temp = i + 1;
                if (temp == 7)
                {
                    break;
                }
                if (LastActions[temp] != null)
                {
                    LastActions[i] = LastActions[temp];
                }

            }
        }

        public void showActions()
        {
            if (LastActions[LastActions.Length - 1] == null)
            {
                Action1.Visibility = Visibility.Hidden;
            } else
            {
                ExpDesc newAction = new ExpDesc();
                try
                {
                    newAction = (ExpDesc)LastActions[LastActions.Length - 1];
                } catch
                {
                    newAction = JsonConvert.DeserializeObject<ExpDesc>(LastActions[LastActions.Length - 1].ToString());
                }

                NoteOfAction1.Content = newAction.note;
                DateOfAction1.Content = newAction.getAStringDate();
                ValueOfAction1.Content = newAction.actionStringCount;
                if (newAction.typeOfAction == "Inc")
                {
                    ValueOfAction1.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EA8BC"));
                } else
                {
                    ValueOfAction1.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#DB71BD"));
                }

                Action1.Visibility = Visibility.Visible;
            }

            if (LastActions[5] == null)
            {
                Action2.Visibility = Visibility.Hidden;
            } else
            {
                ExpDesc newAction = new ExpDesc();
                try
                {
                    newAction = (ExpDesc)LastActions[5];
                }
                catch
                {
                    newAction = JsonConvert.DeserializeObject<ExpDesc>(LastActions[5].ToString());
                }

                NoteOfAction2.Content = newAction.note;
                DateOfAction2.Content = newAction.getAStringDate();
                ValueOfAction2.Content = newAction.actionStringCount;
                if (newAction.typeOfAction == "Inc")
                {
                    ValueOfAction2.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EA8BC"));
                }
                else
                {
                    ValueOfAction2.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#DB71BD"));
                }

                Action2.Visibility = Visibility.Visible;
            }

            if (LastActions[4] == null)
            {
                Action3.Visibility = Visibility.Hidden;
            } else
            {
                ExpDesc newAction = new ExpDesc();
                try
                {
                    newAction = (ExpDesc)LastActions[4];
                }
                catch
                {
                    newAction = JsonConvert.DeserializeObject<ExpDesc>(LastActions[4].ToString());
                }

                NoteOfAction3.Content = newAction.note;
                DateOfAction3.Content = newAction.getAStringDate();
                ValueOfAction3.Content = newAction.actionStringCount;
                if (newAction.typeOfAction == "Inc")
                {
                    ValueOfAction3.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EA8BC"));
                }
                else
                {
                    ValueOfAction3.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#DB71BD"));
                }

                Action3.Visibility = Visibility.Visible;
            }

            if (LastActions[3] == null)
            {
                Action4.Visibility = Visibility.Hidden;
            } else
            {
                ExpDesc newAction = new ExpDesc();
                try
                {
                    newAction = (ExpDesc)LastActions[3];
                }
                catch
                {
                    newAction = JsonConvert.DeserializeObject<ExpDesc>(LastActions[3].ToString());
                }

                NoteOfAction4.Content = newAction.note;
                DateOfAction4.Content = newAction.getAStringDate();
                ValueOfAction4.Content = newAction.actionStringCount;
                if (newAction.typeOfAction == "Inc")
                {
                    ValueOfAction4.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EA8BC"));
                }
                else
                {
                    ValueOfAction4.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#DB71BD"));
                }

                Action4.Visibility = Visibility.Visible;
            }

            if (LastActions[2] == null)
            {
                Action5.Visibility = Visibility.Hidden;
            }
            else
            {
                ExpDesc newAction = new ExpDesc();
                try
                {
                    newAction = (ExpDesc)LastActions[2];
                }
                catch
                {
                    newAction = JsonConvert.DeserializeObject<ExpDesc>(LastActions[2].ToString());
                }

                NoteOfAction5.Content = newAction.note;
                DateOfAction5.Content = newAction.getAStringDate();
                ValueOfAction5.Content = newAction.actionStringCount;
                if (newAction.typeOfAction == "Inc")
                {
                    ValueOfAction5.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EA8BC"));
                }
                else
                {
                    ValueOfAction5.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#DB71BD"));
                }

                Action5.Visibility = Visibility.Visible;
            }

            if (LastActions[1] == null)
            {
                Action6.Visibility = Visibility.Hidden;
            }
            else
            {
                ExpDesc newAction = new ExpDesc();
                try
                {
                    newAction = (ExpDesc)LastActions[1];
                }
                catch
                {
                    newAction = JsonConvert.DeserializeObject<ExpDesc>(LastActions[1].ToString());
                }

                NoteOfAction6.Content = newAction.note;
                DateOfAction6.Content = newAction.getAStringDate();
                ValueOfAction6.Content = newAction.actionStringCount;
                if (newAction.typeOfAction == "Inc")
                {
                    ValueOfAction6.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EA8BC"));
                }
                else
                {
                    ValueOfAction6.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#DB71BD"));
                }

                Action6.Visibility = Visibility.Visible;
            }

            if (LastActions[0] == null)
            {
                Action7.Visibility = Visibility.Hidden;
            }
            else
            {
                ExpDesc newAction = new ExpDesc();
                try
                {
                    newAction = (ExpDesc)LastActions[0];
                }
                catch
                {
                    newAction = JsonConvert.DeserializeObject<ExpDesc>(LastActions[0].ToString());
                }

                NoteOfAction7.Content = newAction.note;
                DateOfAction7.Content = newAction.getAStringDate();
                ValueOfAction7.Content = newAction.actionStringCount;
                if (newAction.typeOfAction == "Inc")
                {
                    ValueOfAction7.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#6EA8BC"));
                }
                else
                {
                    ValueOfAction7.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#DB71BD"));
                }

                Action7.Visibility = Visibility.Visible;
            }

        }

        private void FocusOnLastActions(object sender, RoutedEventArgs e)
        {
            showActions();
        }

        private void MainFormButton_Click(object sender, RoutedEventArgs e)
        {
            CheckIsANum();

            ExpDesc newAction = new ExpDesc();
            newAction.typeOfAction = "Exp";
            newAction.note = noteFormField.Text;
            newAction.countOfAction = Convert.ToDouble(expFormField.Text);
            newAction.actionStringCount = newAction.getAString();

            int temp = -1;
            for (int i = 0; i < radioBtn.Length; i++)
            {
                if (radioBtn[i] == true)
                {
                    temp = i;
                    break;
                }
            }

            switch (temp)
            {
                case 0:
                    AllExp.car += Convert.ToDouble(expFormField.Text);
                    break;
                case 1:
                    AllExp.house += Convert.ToDouble(expFormField.Text);
                    break;
                case 2:
                    AllExp.sport += Convert.ToDouble(expFormField.Text);
                    break;
                case 3:
                    AllExp.eat += Convert.ToDouble(expFormField.Text);
                    break;
                case 4:
                    AllExp.buy += Convert.ToDouble(expFormField.Text);
                    break;
                case 5:
                    AllExp.health += Convert.ToDouble(expFormField.Text);
                    break;
                case 6:
                    AllExp.restaurant += Convert.ToDouble(expFormField.Text);
                    break;
                case 7:
                    AllExp.hobbies += Convert.ToDouble(expFormField.Text);
                    break;
                default:
                    MessageBox.Show("Выберите категорию траты");
                    break;
            }

            addingNewAction();
            LastActions[LastActions.Length - 1] = newAction;

            AddExpWindow.Visibility = Visibility.Hidden;
            AllExp.showExpsOnMain(CarExpLabel, HouseExpLabel, sportExpLabel, EatExpLabel,
                    RestaurantExpLabel, HobbiesExpLabel, BuyExpLabel, HealthExpLabel, allExpValue,
                    allExpValueIncExp);
        }

        /*Вызов окна расходов с разных конпок*/

        private void ShowCarExpWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddExpWindow();
            radioBtn[0] = true;
            RadioBtnCar.Opacity = 0.55;
        }

        private void ShowHouseExpWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddExpWindow();
            radioBtn[1] = true;
            RadioBtnHouse.Opacity = 0.55;
        }

        private void ShowSportExpWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddExpWindow();
            radioBtn[2] = true;
            RadioBtnSport.Opacity = 0.55;
        }

        private void ShowEatExpWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddExpWindow();
            radioBtn[3] = true;
            RadioBtnEat.Opacity = 0.55;
        }

        private void ShowRestaurantExpWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddExpWindow();
            radioBtn[6] = true;
            RadioBtnRestaurant.Opacity = 0.55;
        }

        private void ShowHobbiesExpWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddExpWindow();
            radioBtn[7] = true;
            RadioBtnHobbies.Opacity = 0.55;
        }

        private void ShowBuyExpWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddExpWindow();
            radioBtn[4] = true;
            RadioBtnBuy.Opacity = 0.55;
        }

        private void ShowHealthExpWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddExpWindow();
            radioBtn[5] = true;
            RadioBtnHealth.Opacity = 0.55;
        }






        /* О Доходах*/

        Inc AllInc = new Inc();

        private void ShowAddIncWindow()
        {
            makeFalseBtnInc();
            makeOpacityColorInc();
            IncFormField.Text = "Доход Br";
            noteFormFieldInc.Text = "Примечание";
            AddIncWindow.Visibility = Visibility.Visible;
        }

        /*Форма*/
        /*Крестик*/
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddIncWindow.Visibility = Visibility.Hidden;
        }

        private bool[] radioBtnInc = new bool[4];

        public void makeFalseBtnInc()
        {
            for (int i = 0; i < radioBtnInc.Length; i++)
            {
                if (radioBtnInc[i] == true)
                {
                    radioBtnInc[i] = false;
                }
            }
        }

        public void makeOpacityColorInc()
        {
            RadioBtnSalary.Opacity = 1;
            RadioBtnFreelance.Opacity = 1;
            RadioBtnSell.Opacity = 1;
            RadioBtnRent.Opacity = 1;
        }

        private void RadioBtnSalary_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtnInc();
            radioBtnInc[0] = true;
            makeOpacityColorInc();
            RadioBtnSalary.Opacity = 0.55;
        }

        private void RadioBtnFreelance_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtnInc();
            radioBtnInc[1] = true;
            makeOpacityColorInc();
            RadioBtnFreelance.Opacity = 0.55;
        }

        private void RadioBtnSell_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtnInc();
            radioBtnInc[2] = true;
            makeOpacityColorInc();
            RadioBtnSell.Opacity = 0.55;
        }

        private void RadioBtnRent_Click(object sender, RoutedEventArgs e)
        {
            makeFalseBtnInc();
            radioBtnInc[3] = true;
            makeOpacityColorInc();
            RadioBtnRent.Opacity = 0.55;
        }

        /*Продолжение формы*/

        private void isMouseOutInc(object sender, RoutedEventArgs e)
        {
            CheckIsANumInc();
        }

        private void isMouseInInc(object sender, RoutedEventArgs e)
        {
            if (IncFormField.Text == "Доход Br")
            {
                IncFormField.Text = "";
            }
        }

        private void isMouseInIncNote(object sender, RoutedEventArgs e)
        {
            if (noteFormFieldInc.Text == "Примечание")
            {
                noteFormFieldInc.Text = "";
            }
        }

        private void isMouseOutIncNote(object sender, RoutedEventArgs e)
        {
            if (noteFormFieldInc.Text == "")
            {
                noteFormFieldInc.Text = "Примечание";
            }
        }

        private void CheckIsANumInc()
        {
            bool isNumeric = double.TryParse(IncFormField.Text, out _);
            if (IncFormField.Text == "")
            {
                IncFormField.Text = "Расход Br";
                MessageBox.Show("Введите число");
            }
            else if (isNumeric == false)
            {
                IncFormField.Text = "";
                MessageBox.Show("Введите число");
            }
        }


        private void MainIncFormButton_Click(object sender, RoutedEventArgs e)
        {
            CheckIsANumInc();

            ExpDesc newAction = new ExpDesc();
            newAction.typeOfAction = "Inc";
            newAction.note = noteFormFieldInc.Text;
            newAction.countOfAction = Convert.ToDouble(IncFormField.Text);
            newAction.actionStringCount = newAction.getAStringInc();

            int temp = -1;
            for (int i = 0; i < radioBtnInc.Length; i++)
            {
                if (radioBtnInc[i] == true)
                {
                    temp = i;
                    break;
                }
            }

            switch (temp)
            {
                case 0:
                    AllInc.salary += Convert.ToDouble(IncFormField.Text);
                    break;
                case 1:
                    AllInc.freelance += Convert.ToDouble(IncFormField.Text);
                    break;
                case 2:
                    AllInc.sell += Convert.ToDouble(IncFormField.Text);
                    break;
                case 3:
                    AllInc.rent += Convert.ToDouble(IncFormField.Text);
                    break;
                default:
                    MessageBox.Show("Выберите категорию траты");
                    break;
            }

            addingNewAction();
            LastActions[LastActions.Length - 1] = newAction;

            AddIncWindow.Visibility = Visibility.Hidden;
            AllInc.showIncOnMain(SalaryIncLabel, FreelanceIncLabel, SellIncLabel, RentIncLabel,
                    allExpValueIncomes, allIncValue);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ShowAddIncWindow();
        }

        private void ShowSalaryIncWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddIncWindow();
            radioBtnInc[0] = true;
            RadioBtnSalary.Opacity = 0.55;
        }

        private void ShowFreelanceIncWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddIncWindow();
            radioBtnInc[1] = true;
            RadioBtnFreelance.Opacity = 0.55;
        }

        private void ShowSellIncWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddIncWindow();
            radioBtnInc[2] = true;
            RadioBtnSell.Opacity = 0.55;
        }

        private void ShowRentIncWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowAddIncWindow();
            radioBtnInc[3] = true;
            RadioBtnRent.Opacity = 0.55;
        }

    
    }

}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Lib_11;

namespace Practice8_var11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int jobLevel = 0;
        private float money = 0f;
        private float epc = 0.69f;
        private int pivo = 0;
        private int candies = 0;
        private int pivoBought = 0;
        private int candiesBought = 0;

        Batya batya = new("Валера");
        SinaKorzina? son;
        public MainWindow()
        {
            InitializeComponent();
            son = new("Валерик", batya, "Валерьивич");
            UpdateStats();
        }

        private void BatyaTalkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (pivo >= 1)
            {
                pivo--;
                UpdateStats();
                MessageBox.Show(batya.Talk());
            }
            else
            {
                MessageBox.Show("А ГДЕ ПИВО??");
            }

        }

        public void UpdateStats()
        {
            int pivoCount = (int)(10 + MathF.Round(jobLevel * 2.694201488f));
            int candiesCount = (int)(20 + MathF.Round(jobLevel * 3.8008135f));
            Stats.Content = $"Деняг: {(int)money} руб. {(int)(money * 100 % 100)} кокпеек.\n" +
                            $"Заработок: {(int)epc} руб. {(int)(epc * 100 % 100)} кокпеек.\n" +
                            $"Пива в холодосе: {pivo}\n" +
                            $"Конфет: {candies}\n";


            PivoBuyBtn.Content = $"Купить пиво ({GetPivoPrice()} рублей)";
            KulekBuyBtn.Content = $"Купить кулёк конфет ({GetCandyPrice()} рублей)";
            MakeMoreMoney.Content = $"Подкупить насяйника ({pivoCount} пива и {candiesCount} конфет)";
        }

        private void PivoBuyBtn_Click(object sender, RoutedEventArgs e)
        {
            float currentPivoPrice = GetPivoPrice();
            if (money >= currentPivoPrice)
            {
                money -= currentPivoPrice;
                pivoBought++;
                pivo++;
                UpdateStats();
            }
        }

        private float GetPivoPrice() => MathF.Round(50f + jobLevel * 2.257532069f + pivoBought * 1.7369f);
        private float GetCandyPrice() => MathF.Round(100f + jobLevel * 2.199982069f + candiesBought * 3.9569f);

        private void KulekBuyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (money >= GetCandyPrice())
            {
                money -= GetCandyPrice();
                candiesBought++;
                candies += 10;
                UpdateStats();
            }
        }

        private void WorkBtn_Click(object sender, RoutedEventArgs e)
        {
            money += epc;
            UpdateStats();
        }

        private void KorzinaTalkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (candies >= 2)
            {
                candies -= 2;
                MessageBox.Show(son.Talk());
                UpdateStats();
            }
            else
            {
                MessageBox.Show("А ГДЕ КОНФЕТЫ??");
            }
        }

        private void MakeMoreMoney_Click(object sender, RoutedEventArgs e)
        {
            int pivoCount = (int)(10 + MathF.Round(jobLevel * 2.694201488f));
            int candiesCount = (int)(20 + MathF.Round(jobLevel * 3.8008135f));

            if (pivo >= pivoCount && candies >= candiesCount)
            {
                pivo -= pivoCount;
                candies -= candiesCount;

                jobLevel++;

                pivoCount = (int)(10 + MathF.Round(jobLevel * 2.694201488f));
                candiesCount = (int)(20 + MathF.Round(jobLevel * 5.8008135f));

                epc += 0.42f + 0.0069f * jobLevel;
            }
            UpdateStats();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (son == null)
                return;

            TextBox sndr = sender as TextBox;

            switch (sndr.Name)
            {
                case "BatyaName":
                    batya.Name = sndr.Text;
                    break;
                case "KorzinaName":
                    son.Name = sndr.Text;
                    break;
                case "KorzinaMiddleName":
                    son.MidName = sndr.Text;
                    break;
                default:
                    MessageBox.Show("ШО?");
                    break;
            }
        }

        private void MDB(object sender, MouseButtonEventArgs e) => (sender as TextBox).SelectAll();

        // сбербанк онлайн мод много денег
        private void DoCheat_Click(object sender, RoutedEventArgs e)
        {
            epc += 69420;
            money += 228339;
            jobLevel = -3;
            pivo += 8008135;
            candies += 8008135;
            pivoBought = 0;
            candiesBought = 0;
            UpdateStats();
        }
    }
}

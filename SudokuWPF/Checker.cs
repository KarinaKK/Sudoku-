using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SudokuWPF
{
    public static class Checker
    {
        public static bool check_text(string text, string message)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show(message, "Некорректный ввод", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        public static bool check_price(string text, string message)
        {
            double res = 0;
            if (String.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show(message, "Некорректный ввод", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            if (!double.TryParse(text, out res))
            {
                MessageBox.Show(message, "Некорректный ввод", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }
            return true;

        }


    }
}
using System;
using System.Collections.Generic;
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

namespace talyanu.View
{
    /// <summary>
    /// Логика взаимодействия для StartView.xaml
    /// </summary>
    public partial class StartView : UserControl
    {
        public StartView()
        {
            InitializeComponent();
            rb.IsChecked = true;

        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (cb1 != null && cb1.Items.Count > 0)
            {
                cb1.SelectedIndex = 0;
            }
        }

        private void ComboBox_Loaded_Sec(object sender, RoutedEventArgs e)
        {
            if (cb2 != null && cb2.Items.Count > 0)
            {
                cb2.SelectedIndex = 1;
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ((cb1.SelectedItem != null) && (cb2.SelectedItem != null) && (cb2.SelectedItem.ToString() == cb1.SelectedItem.ToString()))
            {
                // если равен, то обнуляем выбранный элемент
                cb2.SelectedItem = null;
            }

        }
        private void ComboBox_SelectionChanged_Sec(object sender, SelectionChangedEventArgs e)
        {

            if ((cb2.SelectedItem != null) && (cb1.SelectedItem != null) && (cb2.SelectedItem.ToString() == cb1.SelectedItem.ToString()))
            {
                // если равен, то обнуляем выбранный элемент
                cb1.SelectedItem = null;
            }

        }
    }
}


using gibdd.Models;
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
using System.Windows.Shapes;

namespace gibdd.Windows
{
    /// <summary>
    /// Логика взаимодействия для Inspector.xaml
    /// </summary>
    public partial class Inspector : Window
    {
        GaiEntities gaiEntities = new GaiEntities();
        public Inspector()
        {
            InitializeComponent();
            LoadFines();
        }
        private void LoadFines()
        {
            FinesDataGrid.ItemsSource = gaiEntities.Fines.ToList();
        }
        private void FinesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

using gibdd.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    /// Логика взаимодействия для Nachalnik.xaml
    /// </summary>
    public partial class Nachalnik : Window
    {
        private GaiEntities gaiEntities = new GaiEntities();
        public Nachalnik()
        {
            InitializeComponent();
            LoadDrivers();
            LoadWorkplace();
            LoadAdress();
            LoadLicences();
        }

        private void LoadDrivers()
        {
            DriversDataGrid.ItemsSource = gaiEntities.Drivers.ToList();
        }
        private void LoadWorkplace()
        {
            WorkplaceDataGrid.ItemsSource = gaiEntities.Workplace.ToList();
        }
        private void LoadAdress()
        {
            AddressDataGrid.ItemsSource = gaiEntities.Address.ToList();
        }
        private void LoadLicences()
        {
            LicencesDataGrid.ItemsSource = gaiEntities.Licences.ToList();
        }
        private int ParseIntField(string value, string fieldName)
        {
            if (int.TryParse(value, out int result))
                return result;

            throw new FormatException($"Поле '{fieldName}' должно содержать целое число.");
        }

        private void AddDriversButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(DriversNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DriversMiddlenameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DriversPassportSerialTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DriversPassportNumberTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DriversIDTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Drivers newDriver = new Drivers
                {
                    id = int.Parse(DriversIDTextBox.Text),
                    Name = DriversNameTextBox.Text,
                    Middlename = DriversMiddlenameTextBox.Text,
                    PassportSerial = int.Parse(DriversPassportSerialTextBox.Text),
                    PassportNumber = int.Parse(DriversPassportNumberTextBox.Text),
                    Postcode = int.Parse(DriversPostcodeTextBox.Text),
                    Phone = DriversPhoneTextBox.Text,
                    Email = DriversEmailTextBox.Text,
                    Photo = DriversPhotoTextBox.Text,
                    Description = DriversDescriptionTextBox.Text,
                    id_Workplace = ParseIntField(IDWorkplaceTextBox.Text, "ID Workplace"),
                    id_Adress = ParseIntField(IDAddressTextBox.Text, "ID Address"),
                    id_Licence = ParseIntField(IDLicenceTextBox.Text, "ID Licence")
                };

                // Добавление в базу данных
                gaiEntities.Drivers.Add(newDriver);
                gaiEntities.SaveChanges();

                // Обновление DataGrid
                LoadDrivers();

                MessageBox.Show("Водитель успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Ошибка формата данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.InnerException;
                MessageBox.Show($"Ошибка при обновлении базы данных: {innerException?.Message ?? ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void UpdateDriversButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DriversDataGrid.SelectedItem is Drivers selectedDriver)
                {
                    // Обновление свойств выбранного водителя
                    selectedDriver.Name = DriversNameTextBox.Text;
                    selectedDriver.Middlename = DriversMiddlenameTextBox.Text;
                    selectedDriver.PassportSerial = int.Parse(DriversPassportSerialTextBox.Text);
                    selectedDriver.PassportNumber = int.Parse(DriversPassportNumberTextBox.Text);
                    selectedDriver.Postcode = int.Parse(DriversPostcodeTextBox.Text);
                    selectedDriver.Phone = DriversPhoneTextBox.Text;
                    selectedDriver.Email = DriversEmailTextBox.Text;
                    selectedDriver.Photo = DriversPhotoTextBox.Text;
                    selectedDriver.Description = DriversDescriptionTextBox.Text;
                    selectedDriver.id_Workplace = int.Parse(IDWorkplaceTextBox.Text);
                    selectedDriver.id_Adress = int.Parse(IDAddressTextBox.Text);
                    selectedDriver.id_Licence = int.Parse(IDLicenceTextBox.Text);

                    // Сохранение изменений
                    gaiEntities.SaveChanges();

                    // Обновление DataGrid
                    LoadDrivers();

                    MessageBox.Show("Водитель успешно обновлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Выберите водителя для изменения.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении водителя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DeleteDriversButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DriversDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DriversDataGrid.SelectedItem is Drivers selectedDriver)
            {
                DriversIDTextBox.Text = selectedDriver.id.ToString();
                DriversNameTextBox.Text = selectedDriver.Name;
                DriversMiddlenameTextBox.Text = selectedDriver.Middlename;
                DriversPassportSerialTextBox.Text = selectedDriver.PassportSerial.ToString();
                DriversPassportNumberTextBox.Text = selectedDriver.PassportNumber.ToString();
                DriversPostcodeTextBox.Text = selectedDriver.Postcode.ToString();
                DriversPhoneTextBox.Text = selectedDriver.Phone;
                DriversEmailTextBox.Text = selectedDriver.Email;
                DriversPhotoTextBox.Text = selectedDriver.Photo;
                DriversDescriptionTextBox.Text = selectedDriver.Description;
                IDWorkplaceTextBox.Text = selectedDriver.id_Workplace.ToString();
                IDAddressTextBox.Text = selectedDriver.id_Adress.ToString();
                IDLicenceTextBox.Text = selectedDriver.id_Licence.ToString();
            }
        }

        private void AddWorkplaceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(WorkplaceCompanyTextBox.Text) ||
                    string.IsNullOrEmpty(WorkplaceJobNameTextBox.Text))
                {
                    MessageBox.Show("Заполните все поля");
                }
                Workplace newWorkplace = new Workplace()
                {
                    Company = WorkplaceCompanyTextBox.Text,
                    JobName = WorkplaceJobNameTextBox.Text
                };
                gaiEntities.Workplace.Add(newWorkplace);
                gaiEntities.SaveChanges();
                LoadWorkplace();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error", ex.Message);
            }
        }

        private void UpdateWorkplaceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(WorkplaceDataGrid.SelectedItem is Workplace selectedWorkplace)
                {
                    selectedWorkplace.id = int.Parse(WorkplaceIdTextBox.Text);
                    selectedWorkplace.Company = WorkplaceCompanyTextBox.Text;
                    selectedWorkplace.JobName = WorkplaceJobNameTextBox.Text;
                    gaiEntities.SaveChanges();
                    LoadWorkplace();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Ошибка формата данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.InnerException;
                MessageBox.Show($"Ошибка при обновлении базы данных: {innerException?.Message ?? ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            };

        }

        private void WorkplaceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                if(WorkplaceDataGrid.SelectedItem is Workplace selectedWorkplace)
                {
                    WorkplaceIdTextBox.Text = selectedWorkplace.id.ToString();
                    WorkplaceCompanyTextBox.Text = selectedWorkplace.Company.ToString();
                    WorkplaceJobNameTextBox.Text = selectedWorkplace.JobName.ToString();

                }
        }

        private void AdressDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddAddressButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateAddressButton_Click(object sender, RoutedEventArgs e)
        {

        }
        public bool AddDriver(Drivers newDriver)
        {
            if (newDriver == null ||
                string.IsNullOrWhiteSpace(newDriver.Name) ||
                string.IsNullOrWhiteSpace(newDriver.Middlename) ||
                newDriver.PassportSerial <= 0 ||
                newDriver.PassportNumber <= 0 ||
                newDriver.id_Workplace <= 0 ||
                newDriver.id_Adress <= 0 ||
                newDriver.id_Licence <= 0)
            {
                return false;
            }

            try
            {
                gaiEntities.Drivers.Add(newDriver);
                gaiEntities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

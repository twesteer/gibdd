using gibdd.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;
using System.Windows.Threading;


namespace gibdd.Windows
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private GaiEntities gaiEntities = new GaiEntities();
        private const string LockInfoFile = "lockInfo.txt"; // Файл для хранения информации о блокировке
        private int failedAttempts = 0;
        private DateTime? lockUntil = null;
        private DispatcherTimer inactivityTimer;
        public Login()
        {
            InitializeComponent();
            LoadLockInfo();
            SetupInactivityTimer();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string Login = LoginTextBox.Text;
            string Password = PasswordTextBox.Password;

            if (lockUntil.HasValue && DateTime.Now < lockUntil.Value)
            {
                MessageBox.Show($"Ввод заблокирован до {lockUntil.Value}.", "Блокировка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = gaiEntities.User.FirstOrDefault(u => u.Login == Login && u.Password == Password);

            if (user != null) {
                if (user.Role.Equals("nachalnik"))
                {
                    failedAttempts = 0;
                    SaveLockInfo();
                    Nachalnik nachalnik = new Nachalnik();
                    nachalnik.Show();
                    this.Close();
                }
                else if (user.Role.Equals("inspector"))
                {
                    failedAttempts = 0;
                    SaveLockInfo();
                    Inspector inspector = new Inspector();
                    inspector.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неизвестная роль!");
                }
            }
            else
            {
                failedAttempts++;
                MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                if (failedAttempts >= 3)
                {
                    lockUntil = DateTime.Now.AddMinutes(1);
                    SaveLockInfo();
                    MessageBox.Show("Вы превысили лимит попыток. Ввод заблокирован на 1 минуту.", "Блокировка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void SetupInactivityTimer()
        {
            inactivityTimer = new DispatcherTimer();
            inactivityTimer.Interval = TimeSpan.FromMinutes(1);
            inactivityTimer.Tick += (s, e) =>
            {
                MessageBox.Show("Сессия завершена из-за бездействия.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown(); // Закрытие приложения
            };
            this.MouseMove += ResetInactivityTimer;
            this.KeyDown += ResetInactivityTimer;
            inactivityTimer.Start();
        }

        private void ResetInactivityTimer(object sender, EventArgs e)
        {
            inactivityTimer.Stop();
            inactivityTimer.Start();
        }

        private void SaveLockInfo()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(LockInfoFile))
                {
                    writer.WriteLine(failedAttempts);
                    writer.WriteLine(lockUntil.HasValue ? lockUntil.Value.ToString("o") : "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных блокировки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadLockInfo()
        {
            try
            {
                if (File.Exists(LockInfoFile))
                {
                    using (StreamReader reader = new StreamReader(LockInfoFile))
                    {
                        if (int.TryParse(reader.ReadLine(), out int attempts))
                        {
                            failedAttempts = attempts;
                        }

                        string lockUntilString = reader.ReadLine();
                        if (DateTime.TryParse(lockUntilString, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime lockTime))
                        {
                            lockUntil = lockTime;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных блокировки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public bool AuthenticateUser(string login, string password, out string role)
        {
            role = null;

            var user = gaiEntities.User.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user != null)
            {
                role = user.Role;
                return true;
            }

            return false;
        }

    }
}

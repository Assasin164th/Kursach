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
using FishFirm164;
using FishFirm164.classes;

namespace FishFirm164
{
    public partial class MainWindow : Window
    {
        private int userId;
        private string userRole;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(int id, string name, string log, string role)
        {
            InitializeComponent();
            userId = id;
            userRole = role;
            txtUserInfo.Text = $"{name} ({role})";

            // Настройка видимости кнопок в зависимости от роли
            bool isAdmin = (role == "Admin");
            bool isManager = (role == "Manager");
            bool isDirector = (role == "Director");
            bool isStorekeeper = (role == "Storekeeper");
            bool isCashier = (role == "Cashier");
            bool isGuest = (role == "Guest");

            btnProducts.Visibility = Visibility.Visible;

            btnSales.Visibility = (isCashier || isManager || isAdmin || isDirector) ? Visibility.Visible : Visibility.Collapsed;

            btnPurchases.Visibility = (isStorekeeper || isManager || isAdmin || isDirector) ? Visibility.Visible : Visibility.Collapsed;

            btnCustomers.Visibility = (!isGuest) ? Visibility.Visible : Visibility.Collapsed;

            btnSuppliers.Visibility = (isManager || isAdmin || isDirector || isStorekeeper) ? Visibility.Visible : Visibility.Collapsed;

            btnUsers.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;

            btnReports.Visibility = (isDirector || isAdmin || isManager) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void OpenTab(string header, UserControl content)
        {
            foreach (TabItem item in mainTabControl.Items)
            {
                if (item.Header.ToString() == header)
                {
                    item.IsSelected = true;
                    return;
                }
            }
            TabItem newTab = new TabItem
            {
                Header = header,
                Content = content
            };
            mainTabControl.Items.Add(newTab);
            newTab.IsSelected = true;
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            var control = new ProductsControl(userId, userRole);
            OpenTab("Товары", control);
        }

        private void BtnSales_Click(object sender, RoutedEventArgs e)
        {
            var control = new SalesControl(userId, userRole);
            OpenTab("Продажи", control);
        }

        private void BtnPurchases_Click(object sender, RoutedEventArgs e)
        {
            var control = new PurchasesControl(userId, userRole);
            OpenTab("Закупки", control);
        }

        private void BtnCustomers_Click(object sender, RoutedEventArgs e)
        {
            var control = new CustomersControl(userId, userRole);
            OpenTab("Клиенты", control);
        }

        private void BtnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var control = new SuppliersControl(userId, userRole);
            OpenTab("Поставщики", control);
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            var control = new UsersControl(userId, userRole);
            OpenTab("Пользователи", control);
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            var control = new ReportsControl(userId, userRole);
            OpenTab("Отчёты", control);
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarApp
{
    public partial class CarsSection : Page
    {
        private List<Car> _allCars;
        private List<Car> _filteredCars;

        public CarsSection()
        {
            InitializeComponent();
            LoadData();
            InitializeFilters();
        }

        private void LoadData()
        {
            // Завантаження даних (з файлів або іншого джерела)
            _allCars = DataManager.LoadCars();
            _filteredCars = new List<Car>(_allCars);
            UpdateCarList();
        }

        private void InitializeFilters()
        {
            var brands = _allCars.Select(car => car.Brand).Distinct().ToList();
            BrandBox.ItemsSource = brands;
            BrandBox.SelectedIndex = -1;

            var models = _allCars.Select(car => car.Model).Distinct().ToList();
            ModelBox.ItemsSource = models;
            ModelBox.SelectedIndex = -1;

            var bodyTypes = _allCars.Select(car => car.BodyType).Distinct().ToList();
            BodyTypeBox.ItemsSource = bodyTypes;
            BodyTypeBox.SelectedIndex = -1;

            var productionYears = _allCars.Select(car => car.ProductionYears).Distinct().ToList();
            ProductionYearsBox.ItemsSource = productionYears;
            ProductionYearsBox.SelectedIndex = -1;
        }

        private void FilterCars(object sender, SelectionChangedEventArgs e)
        {
            // Отримання обраних значень з ComboBox-ів
            var selectedBrand = BrandBox.SelectedItem as string;
            var selectedModel = ModelBox.SelectedItem as string;
            var selectedBodyType = BodyTypeBox.SelectedItem as string;
            var selectedProductionYear = ProductionYearsBox.SelectedItem as string;

            // Фільтрація з урахуванням часткового збігу
            _filteredCars = _allCars.Where(car =>
                (string.IsNullOrEmpty(selectedBrand) || car.Brand.Contains(selectedBrand, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(selectedModel) || car.Model.Contains(selectedModel, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(selectedBodyType) || car.BodyType.Contains(selectedBodyType, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(selectedProductionYear) || car.ProductionYears.Contains(selectedProductionYear, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            // Оновлення залежних фільтрів
            UpdateDependentFilters();

            // Оновлення списку автомобілів
            UpdateCarList();
        }

        private void UpdateDependentFilters()
        {
            // Оновлення моделей, типів кузовів і років виробництва залежно від обраного бренду
            var availableModels = _filteredCars.Select(car => car.Model).Distinct().ToList();
            ModelBox.ItemsSource = availableModels;

            var availableBodyTypes = _filteredCars.Select(car => car.BodyType).Distinct().ToList();
            BodyTypeBox.ItemsSource = availableBodyTypes;

            var availableProductionYears = _filteredCars.Select(car => car.ProductionYears).Distinct().ToList();
            ProductionYearsBox.ItemsSource = availableProductionYears;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FilterCars(null, null);
        }

        private void GoToMainMenu(object sender, RoutedEventArgs e)
        {
            // Перехід до головного меню
            NavigationService.Navigate(new MainMenu()); // Замість `MainMenuPage` вкажіть правильну сторінку
        }

        private void GoToCarDetails(object sender, SelectionChangedEventArgs e)
        {
            if (CarsListBox.SelectedItem is Car selectedCar)
            {
                // Перехід до сторінки деталей автомобіля
                NavigationService.Navigate(new CarDetailsPage(selectedCar));
            }
        }
        private void ResetFilters(object sender, RoutedEventArgs e)
        {
            // Скидання обраних елементів у ComboBox
            BrandBox.SelectedItem = null;
            ModelBox.SelectedItem = null;
            BodyTypeBox.SelectedItem = null;
            ProductionYearsBox.SelectedItem = null;

            // Повернення початкового списку автомобілів
            _filteredCars = new List<Car>(_allCars);
            UpdateCarList();
        }
        private void UpdateCarList()
        {
            CarsListBox.ItemsSource = _filteredCars;
        }
    }
}
using PMU_APP.Models;
using PMU_APP.Services;

namespace PMU_APP.Pages;

public partial class CarListPage : ContentPage
{
    private readonly CarDatabaseService _databaseService;

    public CarListPage(CarDatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
        LoadCars();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadCars();
    }

    private async Task LoadCars()
    {
        var cars = await _databaseService.GetCarsAsync();
        CarsCollectionView.ItemsSource = cars;
    }

    private async void OnCarSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Car selectedCar)
        {
            await Navigation.PushAsync(new CarDetailPage(selectedCar));
            CarsCollectionView.SelectedItem = null;
        }
    }
}
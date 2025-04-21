using PMU_APP.Models;
using PMU_APP.Services;
using System.Diagnostics;

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
        foreach (var car in cars)
        {
            Debug.WriteLine($"Car ID: {car.Id}, Brand: {car.Brand}");
        }
        CarsCollectionView.ItemsSource = cars;
    }

    /*private async void OnCarSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Car selectedCar)
        {
            await Navigation.PushAsync(new CarDetailPage(selectedCar));
            CarsCollectionView.SelectedItem = null; // Reset selection
        }
    } */
    /*private async void OnCarSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Car selectedCar)
        {
            // Add slight delay to ensure button clicks are processed first
            await Task.Delay(50);

            // Only navigate if no button was clicked
            if (!_buttonWasClicked)
            {
                await Navigation.PushAsync(new CarDetailPage(selectedCar));
            }
            _buttonWasClicked = false;
            CarsCollectionView.SelectedItem = null;
        }
    } */
    private async void OnCarSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Car selectedCar)
        {
            // Avoid navigating if a button was clicked
            if (_buttonWasClicked)
            {
                _buttonWasClicked = false;
                return;
            }

            // Navigate to the CarDetailPage
            await Navigation.PushAsync(new CarDetailPage(selectedCar));
            CarsCollectionView.SelectedItem = null; // Reset selection
        }
    }



    private bool _buttonWasClicked = false;
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        _buttonWasClicked = true;
        if (sender is Button button && button.BindingContext is Car car)
        {
            
                try
                {
                    // Debug output to verify the car object
                    Debug.WriteLine($"Attempting to delete car ID: {car.Id}, Brand: {car.Brand}");

                    if (car.Id <= 0)
                    {
                        await DisplayAlert("Error", "Invalid car ID - cannot delete", "OK");
                        return;
                    }

                    bool confirm = await DisplayAlert("Confirm Delete",
                        $"Delete {car.Brand} {car.Model} permanently?",
                        "Delete", "Cancel");

                    if (confirm)
                    {
                        // Get fresh copy from database to ensure validity
                        var freshCar = await _databaseService.GetCarAsync(car.Id);
                        if (freshCar == null)
                        {
                            await DisplayAlert("Error", "Car not found in database", "OK");
                            return;
                        }

                        int result = await _databaseService.DeleteCarAsync(freshCar);

                        if (result > 0) // Success
                        {
                            await DisplayAlert("Success", "Car deleted", "OK");
                            await LoadCars(); // Refresh list
                        }
                        else
                        {
                            await DisplayAlert("Error", "Delete operation failed", "OK");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Delete failed: {ex.Message}", "OK");
                    Debug.WriteLine($"Delete error: {ex}");
                }
            
        }
    }

    private void CarsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private async void EditButton_Clicked(object sender, EventArgs e)
    {
        
    
        
        if (sender is Button button && button.BindingContext is Car selectedCar)
        {
            
            await Navigation.PushAsync(new CarDetailPage(selectedCar));
        }
    

}
}
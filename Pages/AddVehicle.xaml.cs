using PMU_APP.Models;
using PMU_APP.Services;

namespace PMU_APP.Pages;

public partial class AddVehicle : ContentPage
{
    private readonly CarDatabaseService _databaseService;
    private FileResult selectedImage;

    public AddVehicle(CarDatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string brand = BrandPicker.SelectedItem?.ToString() ?? "N/A";
        string model = ModelEntry.Text;
        string price = PriceEntry.Text;
        string info = InfoEditor.Text;

        if (string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(price))
        {
            await DisplayAlert("Error", "Please fill in all required fields", "OK");
            return;
        }

        
        string imagePath = null;
        if (selectedImage != null)
        {
            var localFilePath = Path.Combine(FileSystem.AppDataDirectory, Guid.NewGuid().ToString() + ".jpg");

            using (var stream = await selectedImage.OpenReadAsync())
            using (var newStream = File.OpenWrite(localFilePath))
            {
                await stream.CopyToAsync(newStream);
            }

            imagePath = localFilePath;
        }

        var car = new Car
        {
            Brand = brand,
            Model = model,
            Price = price,
            Info = info,
            ImagePath = imagePath
        };

        await _databaseService.AddCarAsync(car);
        await DisplayAlert("Success", "Car added successfully", "OK");

        // Clear the form
        BrandPicker.SelectedIndex = -1;
        ModelEntry.Text = string.Empty;
        PriceEntry.Text = string.Empty;
        InfoEditor.Text = string.Empty;
        CarImage.Source = null;
        selectedImage = null;
    }

    private async void OnChoosePhotoClicked(object sender, EventArgs e)
    {
        try
        {
            selectedImage = await MediaPicker.PickPhotoAsync();

            if (selectedImage != null)
            {
                var stream = await selectedImage.OpenReadAsync();
                CarImage.Source = ImageSource.FromStream(() => stream);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Unable to load image: " + ex.Message, "OK");
        }
    }
}
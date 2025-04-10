namespace PMU_APP.Pages;

public partial class AddVehicle : ContentPage
{
	public AddVehicle()
	{
		InitializeComponent();
	}
    private void OnSubmitClicked(object sender, EventArgs e)
    {
        string brand = BrandPicker.SelectedItem?.ToString() ?? "N/A";
        string model = ModelEntry.Text;
        string price = PriceEntry.Text;
        string info = InfoEditor.Text;

        DisplayAlert("Car Info",
            $"Brand: {brand}\nModel: {model}\nPrice: {price}\nInfo: {info}",
            "OK");
    }
    private FileResult selectedImage;

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
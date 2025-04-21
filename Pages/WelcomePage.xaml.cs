using System.Collections.ObjectModel;
using System.Timers;
using PMU_APP.Pages;
using PMU_APP.Services;


namespace PMU_APP.Pages;

public partial class WelcomePage : ContentPage
{
    private System.Timers.Timer _carouselTimer;
    private int _currentIndex = 0;

    public ObservableCollection<string> Images { get; set; }
    public WelcomePage()
	{
		InitializeComponent();
        Images = new ObservableCollection<string>
        {
            "logo.png",
            "cars1.jpg",
            "cars2.jpg"


        };

        BindingContext = this;

        StartCarouselTimer();
    }

    private void StartCarouselTimer()
    {
        _carouselTimer = new System.Timers.Timer(3000);
        _carouselTimer.Elapsed += OnTimerElapsed;
        _carouselTimer.AutoReset = true;
        _carouselTimer.Enabled = true;
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (Images.Count == 0) return;

            _currentIndex = (_currentIndex + 1) % Images.Count;
            ImageCarousel.Position = _currentIndex;
        });
    }
	

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        var databaseService = App.Current.Handler.MauiContext.Services.GetService<CarDatabaseService>();
        await Navigation.PushAsync(new AddVehicle(databaseService));
    }

    private async void ViewCarsButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            
            var databaseService = App.Current.Handler.MauiContext.Services.GetService<CarDatabaseService>();

            
            await Navigation.PushAsync(new CarListPage(databaseService));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Couldn't open car list: {ex.Message}", "OK");
        }
    }
}
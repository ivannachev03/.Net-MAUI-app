using PMU_APP.Models;

namespace PMU_APP.Pages;

public partial class CarDetailPage : ContentPage
{
    public CarDetailPage(Car car)
    {
        InitializeComponent();
        BindingContext = car;
    }
}
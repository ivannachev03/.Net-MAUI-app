using PMU_APP.Models;
using System.Diagnostics;
using PMU_APP.Models;
using PMU_APP.Services;

namespace PMU_APP.Pages;

public partial class CarDetailPage : ContentPage
{
    public CarDetailPage(Car car)
    {
        InitializeComponent();
        BindingContext = car;
        
    }

}
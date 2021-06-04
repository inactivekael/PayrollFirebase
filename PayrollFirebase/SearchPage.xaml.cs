using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PayrollFirebase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public SearchPage()
        {
            InitializeComponent();
        }
        public void homePage_(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        private async void searchClicked(object sender, EventArgs e)
        {
            var data = await firebaseHelper.SearchData(int.Parse(empnum.Text));
            if (data != null)
            {
                emplnum.Text = data.EmployeeNum.ToString();
                name.Text = data.Name;
                hoursw.Text = data.HoursWorked.ToString();
                empstat.Text = data.EmployeeStatus;
                civilstat.Text = data.CivilStatus;
                gross.Text = data.Gross.ToString();
                deduc.Text = data.Deduction.ToString();
                net.Text = data.NetIncome.ToString();

            }
            else
            {
                await DisplayAlert("Error", "No Employee Found", "OK");
            }
        }
    }
}
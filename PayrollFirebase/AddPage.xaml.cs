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
    public partial class AddPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public AddPage()
        {
            InitializeComponent();
            
        }
        public void homePage_(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        public async void addClicked(object sender, EventArgs e)
        {
            double hwork = double.Parse(hoursw.Text);
            double rateperday, rateperhour, basic, overtimehour,
                overtime, gross, wtax, phil, pagibig, net, deduction;

            if (empstat.Text == "R" || empstat.Text == "r")
            {
                rateperday = 800;
            }
            else if (empstat.Text == "P" || empstat.Text == "p")
            {
                rateperday = 600;
            }
            else if (empstat.Text == "C" || empstat.Text == "c")
            {
                rateperday = 500;
            }
            else if (empstat.Text == "T" || empstat.Text == "t")
            {
                rateperday = 450;
            }
            else
            {
                rateperday = 400;
            }

            rateperhour = rateperday / 8;
            basic = hwork * rateperday;
            overtimehour = hwork - 120;

            if (hwork > 120)
            {
                overtime = overtimehour * (rateperhour * 1.5);
            }

            else
            {
                overtime = 0;
            }
            gross = Math.Round((basic + overtime), 2);

            if (gross > 10000)
            {
                wtax = 0.1 * gross;
            }
            else if (gross > 5000)
            {
                wtax = 0.08 * gross;
            }
            else
            {
                wtax = 0.05 * gross;
            }

            if (civilstat.Text == "S" || civilstat.Text == "s")
            {
                phil = 500;
            }
            else if (civilstat.Text == "M" || civilstat.Text == "m")
            {
                phil = 300;
            }
            else
            {
                phil = 400;
            }

            if (gross > 10000)
            {
                pagibig = 0.05 * gross;
            }
            else if (gross > 5000)
            {
                pagibig = 0.03 * gross;
            }
            else
            {
                pagibig = 0.02 * gross;
            }

            deduction = Math.Round((wtax + phil + pagibig), 2);
            net = Math.Round((gross - deduction), 2);

            await firebaseHelper.AddData(int.Parse(empnum.Text), name.Text, double.Parse(hoursw.Text), empstat.Text, civilstat.Text, gross,deduction,net);
            await DisplayAlert("Success", "Data Saved Successfully", "OK");
            empnum.Text = string.Empty;
            name.Text = string.Empty;
            hoursw.Text = string.Empty;
            empstat.Text = string.Empty;
            civilstat.Text = string.Empty;

        }
    }
}
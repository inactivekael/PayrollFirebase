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
    public partial class UpdatePage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public UpdatePage()
        {
            InitializeComponent();
        }
        public void homePage_(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        public async void updateClicked(object sender, EventArgs e)
        {
            var data = await firebaseHelper.SearchData(int.Parse(empnum.Text));
            if (data != null)
            {
                string name = data.Name;
                string empstat = data.EmployeeStatus;
                string civilstat = data.CivilStatus;
                double hwork = double.Parse(hoursw.Text);
                double rateperday, rateperhour, basic, overtimehour,
                    overtime, gross, wtax, phil, pagibig, net, deduction;

                if (empstat == "R")
                {
                    rateperday = 800;
                }
                else if (empstat == "P")
                {
                    rateperday = 600;
                }
                else if (empstat == "C")
                {
                    rateperday = 500;
                }
                else if (empstat == "T")
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
                gross = basic + overtime;

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

                if (civilstat == "S")
                {
                    phil = 500;
                }
                else if (civilstat == "M")
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

                deduction = wtax + phil + pagibig;
                net = gross - deduction;

                await firebaseHelper.UpdateData(
                    int.Parse(empnum.Text),
                    name,
                    double.Parse(hoursw.Text),
                    empstat,
                    civilstat,
                    gross,
                    deduction,
                    net);

                await DisplayAlert("Success", "Data Updated Successfully", "OK");
                empnum.Text = string.Empty;
                hoursw.Text = string.Empty;

            }
            else
            {
                await DisplayAlert("Error", "No Employee Found", "OK");
            }
        }
    }
}
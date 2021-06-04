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
    public partial class DeletePage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public DeletePage()
        {
            InitializeComponent();
        }
        public void homePage_(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        public async void deleteClicked(object sender, EventArgs e)
        {           
            await firebaseHelper.DeleteData(int.Parse(empnum.Text));
            await DisplayAlert("Success", "Data Deleted Successfully", "OK");
            empnum.Text = string.Empty;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PayrollFirebase
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }
        public async void addPage_(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddPage()));
        }
        public async void updatePage_(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new UpdatePage()));
        }
        public async void searchPage_(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new SearchPage()));
        }
        public async void deletePage_(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new DeletePage()));
        }
    }
}

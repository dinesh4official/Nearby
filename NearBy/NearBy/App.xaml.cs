using System;
using NearBy.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NearBy
{
    [Preserve(AllMembers = true)]
    public partial class App : Application
    {
        #region Constructor

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new DashboardPage());
        }

        #endregion

        #region Override Methods

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }

        #endregion
    }
}

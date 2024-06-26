using System;
using TestWork.ViewModel;
using Xamarin.Forms;

namespace TestWork
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            MainPageVM viewModel = new MainPageVM();
            BindingContext = viewModel;
            viewModel.TitleCheckedChanged += ViewModel_TitleCheckedChanged;
            viewModel.ActorCheckedChanged += ViewModel_ActorCheckedChanged;

        }

        private void ViewModel_TitleCheckedChanged(object sender, EventArgs e)
        {
            
            titleSwitch.IsToggled = ((MainPageVM)BindingContext).TitleChecked;
        }
        private void ViewModel_ActorCheckedChanged(object sender, EventArgs e)
        {
            
            actorSwitch.IsToggled = ((MainPageVM)BindingContext).ActorChecked;
        }

        private void moviesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Внимание", "Инфо по фильму", "OK");
        }
    }
}

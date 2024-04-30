using System.ComponentModel;
using Xamarin.Forms;

namespace StudentPlannerXamarin
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Title = "Planner";
            InitializeComponent();
        }

        void TermBtnClicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new TermListPage());
        }
    }
}

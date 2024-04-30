using StudentPlannerXamarin.DataModels;
using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentPlannerXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTermPage : ContentPage
    {
        public AddTermPage()
        {
            InitializeComponent();
        }

        private void AddTermBtn_Clicked(object sender, EventArgs e)
        {
            string termName = TermName.Text;
            DateTime startDate = StartDatePicker.Date;
            DateTime endDate = EndDatePicker.Date;

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(dbPath);

            Term newTerm = new Term
            {
                Name = termName,
                StartDate = startDate,
                EndDate = endDate
            };
            db.Insert(newTerm);

            //Remove open pages and show new term view page
            Navigation.PopAsync();
            Navigation.PopAsync();
            Navigation.PushAsync(new TermListPage());
        }
    }
}
using StudentPlannerXamarin.DataModels;
using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentPlannerXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTermPage : ContentPage
    {
        Term termBeingEdited;
        public EditTermPage(Term term)
        {
            termBeingEdited = term;
            InitializeComponent();
            TermName.Text = term.Name;
            StartDatePicker.Date = term.StartDate;
            EndDatePicker.Date = term.EndDate;
        }

        private void SaveChangesBtn_Clicked(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(dbPath);

            termBeingEdited.Name = TermName.Text;
            termBeingEdited.StartDate = StartDatePicker.Date;
            termBeingEdited.EndDate = EndDatePicker.Date;

            db.Update(termBeingEdited);

            Navigation.PopAsync();
            Navigation.PopAsync();
            Navigation.PushAsync(new TermDetailPage(termBeingEdited));
        }
    }
}
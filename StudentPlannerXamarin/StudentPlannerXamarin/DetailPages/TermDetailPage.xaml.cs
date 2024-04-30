using StudentPlannerXamarin.DataModels;
using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentPlannerXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDetailPage : ContentPage
    {
        Term termViewed;
        public TermDetailPage(Term term)
        {
            termViewed = term;
            Title = term.Name;
            InitializeComponent();
            StartDateLabel.Text = "Start Date: " + term.StartDate.ToString("MM/dd/yy");
            EndDateLabel.Text = "End Date: " + term.EndDate.ToString("MM/dd/yy");

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(dbPath);
            db.CreateTable<Course>();

            var courseTable = db.Table<Course>().Where(v => v.TermId.Equals(term.Id));
            this.BindingContext = courseTable;
        }

        private void ClassesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event
            var selectedCourse = (Course)((ListView)sender).SelectedItem;
            Navigation.PushAsync(new CourseDetailPage(termViewed, selectedCourse));
        }

        async void DeleteTermBtn_Clicked(object sender, EventArgs e)
        {
            bool confirmedDelete = await DisplayAlert("Delete?", "Are you sure you want to delete this term, its courses, and its assessments?", "Yes", "No");
            if (confirmedDelete)
            {
                DeleteTerm();
            }
        }

        private void DeleteTerm()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(dbPath);

            //Cascading Delete
            db.Table<Assessment>().Where(v => v.TermId.Equals(termViewed.Id)).Delete();//Delete Assessments associated with term
            db.Table<Course>().Where(v => v.TermId.Equals(termViewed.Id)).Delete();//Delete courses associated with term
            db.Delete(termViewed);//Delete Term

            Navigation.PopAsync();
            Navigation.PopAsync();
            Navigation.PushAsync(new TermListPage());
        }

        private void EditTermBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditTermPage(termViewed));
        }

        private void AddCourseBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCoursePage(termViewed));
        }
    }
}
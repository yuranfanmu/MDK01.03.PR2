using StudentPlannerXamarin.DataModels;
using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentPlannerXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailPage : ContentPage
    {
        Term associatedTerm;
        Course courseViewed;
        public CourseDetailPage(Term term, Course course)
        {
            associatedTerm = term;
            courseViewed = course;
            InitializeComponent();
            Title = course.Name;
            StartDateLabel.Text = "Start Date: " + course.StartDate.ToString("MM/dd/yy");
            EndDateLabel.Text = "End Date: " + course.EndDate.ToString("MM/dd/yy");
            StatusLabel.Text = "Status: " + course.Status;
            InstructorNameLabel.Text = "Instructor Name: " + course.InstructorName;
            InstructorPhoneLabel.Text = "Instructor Phone: " + course.InstructorPhone;
            InstructorEmailLabel.Text = "Instructor Name: " + course.InstructorEmail;
            NotesLabel.Text = "Notes: " + course.Notes;


            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(dbPath);
            db.CreateTable<Assessment>();

            var courseTable = db.Table<Assessment>().Where(v => v.CourseId.Equals(course.Id));
            this.BindingContext = courseTable;
        }

        private void AddAssessmentBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddAssessmentPage(associatedTerm, courseViewed));
        }

        private void AssessmentListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event
            var selectedAssessment = (Assessment)((ListView)sender).SelectedItem;
            Navigation.PushAsync(new AssessmentDetailPage(associatedTerm, courseViewed, selectedAssessment));
        }
        async void DeleteCourseBtn_Clicked(object sender, EventArgs e)
        {
            bool confirmedDelete = await DisplayAlert("Delete?", "Are you sure you want to delete this course and its assessments?", "Yes", "No");
            if (confirmedDelete)
            {
                DeleteCourse();
            }
        }
        private void DeleteCourse()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(dbPath);

            db.Table<Assessment>().Where(v => v.CourseId.Equals(courseViewed.Id)).Delete();//Delete Assessments associated with Course
            db.Delete(courseViewed);

            Navigation.PopAsync();
            Navigation.PopAsync();
            Navigation.PushAsync(new TermDetailPage(associatedTerm));
        }
        private void EditCourseBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditCoursePage(associatedTerm, courseViewed));
        }
    }
}
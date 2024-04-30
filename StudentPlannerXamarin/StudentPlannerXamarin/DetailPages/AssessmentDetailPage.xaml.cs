using StudentPlannerXamarin.DataModels;
using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentPlannerXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentDetailPage : ContentPage
    {
        Term term;
        Course course;
        Assessment assessmentViewed;
        public AssessmentDetailPage(Term term, Course course, Assessment assessment)
        {
            this.term = term;
            this.course = course;
            assessmentViewed = assessment;
            Title = assessment.Name;
            InitializeComponent();
            AssessmentTypeLabel.Text = "Assessment Type: " + assessment.AssessmentType;
            DueDateLabel.Text = "Due Date: " + assessment.DueDate.ToString("MM/dd/yy");
            Description.Text = "Description: " + assessment.Description;
        }

        async void DeleteAssessmentBtn_Clicked(object sender, EventArgs e)
        {
            bool confirmedDelete = await DisplayAlert("Delete?", "Are you sure you want to delete this assessment?", "Yes", "No");
            if (confirmedDelete)
            {
                DeleteAssessment();
            }
        }
        private void DeleteAssessment()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(dbPath);
            db.Delete(assessmentViewed);

            Navigation.PopAsync();
            Navigation.PopAsync();
            Navigation.PushAsync(new CourseDetailPage(term, course));
        }

        private void EditAssessmentBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditAssessmentPage(term, course, assessmentViewed));
        }
    }
}
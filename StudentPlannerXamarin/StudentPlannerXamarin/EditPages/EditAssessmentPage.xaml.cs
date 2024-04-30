using Plugin.LocalNotifications;
using StudentPlannerXamarin.DataModels;
using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentPlannerXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAssessmentPage : ContentPage
    {
        Term term;
        Course course;
        Assessment assessmentBeingEdited;
        public EditAssessmentPage(Term term, Course course, Assessment assessment)
        {
            this.term = term;
            this.course = course;
            assessmentBeingEdited = assessment;
            InitializeComponent();

            AssessmentName.Text = assessment.Name;
            AssessmentTypePicker.SelectedItem = assessment.AssessmentType;
            DueDatePicker.Date = assessment.DueDate;
            Description.Text = assessment.Description;
        }

        private void SaveChangesBtn_Clicked(object sender, EventArgs e)
        {
            //Deleting existing notifications on assessment.
            CrossLocalNotifications.Current.Cancel(assessmentBeingEdited.Id);

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(dbPath);

            assessmentBeingEdited.Name = AssessmentName.Text;
            assessmentBeingEdited.AssessmentType = AssessmentTypePicker.SelectedItem.ToString();
            assessmentBeingEdited.DueDate = DueDatePicker.Date;
            assessmentBeingEdited.Description = Description.Text;

            db.Update(assessmentBeingEdited);

            //Setting notifications for anticipated due dates
            CrossLocalNotifications.Current.Show(AssessmentName.Text, "Assessment is due tomorrow", assessmentBeingEdited.Id, DueDatePicker.Date.AddDays(-1));//Adding reminder for day before
            CrossLocalNotifications.Current.Show(AssessmentName.Text, "Assessment is due", assessmentBeingEdited.Id, DueDatePicker.Date);

            Navigation.PopAsync();
            Navigation.PopAsync();
            Navigation.PushAsync(new AssessmentDetailPage(term, course, assessmentBeingEdited));
        }
    }
}
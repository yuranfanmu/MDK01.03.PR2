using Plugin.LocalNotifications;
using StudentPlannerXamarin.DataModels;
using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentPlannerXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAssessmentPage : ContentPage
    {
        Term associatedTerm;
        Course courseAddedTo;
        public AddAssessmentPage(Term term, Course course)
        {
            associatedTerm = term;
            courseAddedTo = course;
            InitializeComponent();
            AssessmentTypePicker.Items.Add("Objective Assessment");
            AssessmentTypePicker.Items.Add("Performance Assessment");
        }

        private void AddAssessment_Clicked(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(dbPath);
            Assessment newAssessment = new Assessment {
                TermId = associatedTerm.Id,
                CourseId = courseAddedTo.Id,
                Name = AssessmentName.Text,
                AssessmentType = AssessmentTypePicker.SelectedItem.ToString(),
                DueDate = DueDatePicker.Date,
                Description = Description.Text
            };
            db.Insert(newAssessment);

            //Setting notifications for anticipated due dates
            CrossLocalNotifications.Current.Show(AssessmentName.Text, "Assessment is due tomorrow", newAssessment.Id, DueDatePicker.Date.AddDays(-1));//Adding reminder for day before
            CrossLocalNotifications.Current.Show(AssessmentName.Text, "Assessment is due", newAssessment.Id, DueDatePicker.Date);

            Navigation.PopAsync();
            Navigation.PopAsync();
            Navigation.PushAsync(new CourseDetailPage(associatedTerm, courseAddedTo));
        }
    }
}
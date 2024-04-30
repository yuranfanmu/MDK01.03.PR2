using Plugin.LocalNotifications;
using StudentPlannerXamarin.DataModels;
using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentPlannerXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCoursePage : ContentPage
    {
        Term associatedTerm;
        Course courseBeingEdited;
        public EditCoursePage(Term term, Course course)
        {
            associatedTerm = term;
            courseBeingEdited = course;
            InitializeComponent();
            CourseStatusPicker.Items.Add("Completed");
            CourseStatusPicker.Items.Add("Dropped");
            CourseStatusPicker.Items.Add("In Progress");
            CourseStatusPicker.Items.Add("Planned");

            CourseName.Text = course.Name;
            StartDatePicker.Date = course.StartDate;
            EndDatePicker.Date = course.EndDate;
            CourseStatusPicker.SelectedItem = course.Status;
            InstructorName.Text = course.InstructorName;
            InstructorPhone.Text = course.InstructorPhone;
            InstructorEmail.Text = course.InstructorEmail;
            Notes.Text = course.Notes;
        }

        private void SaveChangesBtn_Clicked(object sender, EventArgs e)
        {
            //Deleting existing notifications on course.
            CrossLocalNotifications.Current.Cancel(courseBeingEdited.Id);

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(dbPath);

            courseBeingEdited.Name = CourseName.Text;
            courseBeingEdited.StartDate = StartDatePicker.Date;
            courseBeingEdited.EndDate = EndDatePicker.Date;
            courseBeingEdited.Status = CourseStatusPicker.SelectedItem.ToString();
            courseBeingEdited.InstructorName = InstructorName.Text;
            courseBeingEdited.InstructorPhone = InstructorPhone.Text;
            courseBeingEdited.InstructorEmail = InstructorEmail.Text;
            courseBeingEdited.Notes = Notes.Text;

            db.Update(courseBeingEdited);

            //Resetting alerts for the start and end date of the course
            CrossLocalNotifications.Current.Show(CourseName.Text, "Course Started", courseBeingEdited.Id, StartDatePicker.Date);
            CrossLocalNotifications.Current.Show(CourseName.Text, "Course Ended", courseBeingEdited.Id, EndDatePicker.Date);

            Navigation.PopAsync();
            Navigation.PopAsync();
            Navigation.PushAsync(new CourseDetailPage(associatedTerm, courseBeingEdited));
        }
    }
}
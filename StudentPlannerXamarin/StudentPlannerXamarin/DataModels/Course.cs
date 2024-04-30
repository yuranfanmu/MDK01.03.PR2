using SQLite;
using System;

namespace StudentPlannerXamarin.DataModels
{
    [Table("Course")]
    public class Course
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int TermId { get; set; }//Foreign Key

        [MaxLength(8)]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }

        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

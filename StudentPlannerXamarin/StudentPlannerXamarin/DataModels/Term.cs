using System;
using SQLite;

namespace StudentPlannerXamarin.DataModels
{
    [Table("Term")]
    public class Term
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

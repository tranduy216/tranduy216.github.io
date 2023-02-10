using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendApi.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class EnrollmentModel
    {
        [Key]
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public CourseModel Course { get; set; }

        public StudentModel Student { get; set; }
    }
}

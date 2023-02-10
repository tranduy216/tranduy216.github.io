using System.ComponentModel.DataAnnotations;

namespace BackendApi.Models
{
    public class StudentModel
    {
        public int ID { get; set; }

        public string FirstMidName { get; set; }

        public string LastName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public ICollection<EnrollmentModel> Enrollments { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}

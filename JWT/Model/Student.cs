using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace JWT.Model
{
        public enum MemberType
        {
            Student,
            Faculty,
            Staff
        }

        public enum MemberStatus
        {
            Active,
            Inactive,
            Suspended
        }
        public class Student
        {
            [Key]
            public int Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public string contact { get; set; }
            public DateTime JoinDate { get; set; } = DateTime.Now;

            [Required]
            public MemberStatus Status { get; set; } = MemberStatus.Active;

            [Required]
            public MemberType Type { get; set; } = MemberType.Student;

    }
    
}

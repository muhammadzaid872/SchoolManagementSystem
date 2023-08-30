using System.ComponentModel.DataAnnotations;

namespace Presentation.Domain
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public int TeacherId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}

using System.Collections.Generic;

namespace Presentation.Models
{
    public class EnrollmentDTO
    {
        public int EnrollmentId { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
    }

    public class EnrollmentViewDTO
    {
        public List<EnrollmentDTO> enrollmentList { get; set; }
        public List<IdNameDTO> couresList { get; set; }
        public List<IdNameDTO> studentList { get; set; }
        public List<IdNameDTO> teacherList { get; set; }

    }
    public class EnrollmentModel
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
    }
}

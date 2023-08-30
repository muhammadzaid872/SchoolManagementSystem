using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Domain
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseName { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public int Credits { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual Enrollment Enrollment { get; set; }
        public ICollection<Enrollment> Enrollment { get; set; }

    }
}

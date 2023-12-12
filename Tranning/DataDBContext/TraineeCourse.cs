using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Tranning.DataDBContext
{
    public class TraineeCourse
    {
        [Key]
        [ForeignKey("course")]
        public int course_id { get; set; }

        [ForeignKey("user")]
        public int trainee_id { get; set; }

        public virtual Course Course { get; set; }
        public virtual User User { get; set; }

        [AllowNull]
        public DateTime? created_at { get; set; }

        [AllowNull]
        public DateTime? updated_at { get; set; }

        [AllowNull]
        public DateTime? deleted_at { get; set; }
    }
}

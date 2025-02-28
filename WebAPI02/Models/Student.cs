using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI02.Models;

public partial class Student
{
    public int StId { get; set; }

    public string? StFname { get; set; }

    public string? StLname { get; set; }

    public string? StAddress { get; set; }

    public int? StAge { get; set; }

    public int? DeptId { get; set; }

    public int? StSuper { get; set; }
 
    public virtual Department? Dept { get; set; }

    public virtual ICollection<Student> InverseStSuperNavigation { get; set; } = new List<Student>(); // Self-referencing navigation property (List of students manage by this student)

    public virtual Student? StSuperNavigation { get; set; }  // Self-referencing navigation property (Super student)

    public virtual ICollection<StudCourse> StudCourses { get; set; } = new List<StudCourse>();
}

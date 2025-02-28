using System;
using System.Collections.Generic;

namespace WebAPI02.Models;

public partial class StudentCourseWithGradesView
{
    public string StudentFullName { get; set; } = null!;

    public string? CourseName { get; set; }
}

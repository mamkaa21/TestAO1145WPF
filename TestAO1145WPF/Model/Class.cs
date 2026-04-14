using System;
using System.Collections.Generic;

namespace TestAO1145WPF.Model;

public partial class Class
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public int? IdStudent { get; set; }

    public int? IdTeacher { get; set; }

    public virtual Student? IdStudentNavigation { get; set; }

    public virtual Teacher? IdTeacherNavigation { get; set; }

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}

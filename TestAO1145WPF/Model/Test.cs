using System;
using System.Collections.Generic;

namespace TestAO1145WPF.Model;

public partial class Test
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdSubject { get; set; }

    public int? IdQuestion { get; set; }

    public int? IdAnswer { get; set; }

    public int? IdMark { get; set; }

    public int? IdTeacher { get; set; }

    public int? IdStudent { get; set; }

    public virtual Aswer? IdAnswerNavigation { get; set; }

    public virtual Mark? IdMarkNavigation { get; set; }

    public virtual Question? IdQuestionNavigation { get; set; }

    public virtual Student? IdStudentNavigation { get; set; }

    public virtual Subject? IdSubjectNavigation { get; set; }

    public virtual Teacher? IdTeacherNavigation { get; set; }
}

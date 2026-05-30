using System;
using System.Collections.Generic;

namespace TestAO1145WPF;

public partial class Studentanswer
{
    public int Id { get; set; }

    public int? IdStudent { get; set; }

    public int? IdTest { get; set; }

    public DateTime? DateTime { get; set; }

    public virtual Student? IdStudentNavigation { get; set; }

    public virtual Test? IdTestNavigation { get; set; }
}

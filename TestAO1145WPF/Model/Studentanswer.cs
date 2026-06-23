using System;
using System.Collections.Generic;

namespace TestAO1145WPF.Model;

public partial class Studentanswer
{
    public int Id { get; set; }

    public int? IdStudent { get; set; }

    public int? IdTest { get; set; }

    public DateTime? DateTime { get; set; }

    public int? IdMark { get; set; }
    public int? Mark { get; set; }

    public string? Name { get; set; }
    public string? StudentName { get; set; }
    public string? StudentLastName { get; set; }
    public string? Test { get; set; }

    public virtual ICollection<Testcrossquestion> Testcrossquestions { get; set; } = new List<Testcrossquestion>();
}

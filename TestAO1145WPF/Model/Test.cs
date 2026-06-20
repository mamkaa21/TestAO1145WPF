using System;
using System.Collections.Generic;

namespace TestAO1145WPF.Model;
public partial class Test
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdSubject { get; set; }

    public int? IdTeacher { get; set; }

    public string? Teacher { get; set; }

    public int? CountQuestionTest { get; set; }
    public string? Subject { get; set; }
}

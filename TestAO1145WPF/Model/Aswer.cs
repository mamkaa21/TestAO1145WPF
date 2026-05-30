using System;
using System.Collections.Generic;

namespace TestAO1145WPF;

public partial class Aswer
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public int? IdQuestion { get; set; }

    public virtual Question? IdQuestionNavigation { get; set; }
}

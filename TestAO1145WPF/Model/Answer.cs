using System;
using System.Collections.Generic;

namespace TestAO1145WPF.Model;

public partial class Answer
{
    public int Id { get; set; }
    public string? Text { get; set; }

    public int? IdQuestion { get; set; }

    public bool? RightAnswer { get; set; }
}

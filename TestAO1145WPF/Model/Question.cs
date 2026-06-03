using System;
using System.Collections.Generic;

namespace TestAO1145WPF.Model;

public partial class Question // сделать вопросы и ответы чисто модеькаи и все
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdTest { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
}

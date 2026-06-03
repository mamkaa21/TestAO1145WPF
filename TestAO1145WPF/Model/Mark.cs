using System;
using System.Collections.Generic;

namespace TestAO1145WPF.Model;

public partial class Mark
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public int? CountQ { get; set; }

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}

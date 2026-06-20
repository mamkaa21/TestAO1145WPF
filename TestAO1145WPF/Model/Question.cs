using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace TestAO1145WPF.Model;

public partial class Question
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdTest { get; set; }

    public virtual ObservableCollection<Answer> Answers { get; set; } = new ObservableCollection<Answer>();

    public virtual Test? IdTestNavigation { get; set; }

    public int? Type { get; set; }

    [JsonIgnore]
    public string NameSlice
    {
        get
        {
            if (Name.Length > 20)
                return Name.Substring(0, 20) + "...";
            return Name;
        }
    }
    [JsonIgnore]
    public string StudentAnswer { get; set; }
}

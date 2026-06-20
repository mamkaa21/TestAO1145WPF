using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows;
using TestAO1145WPF.Model;
using TestAO1145WPF.ViewModel;

namespace TestAO1145WPF.Model;

public partial class Answer : BaseVM
{
    private bool? rightAnswer;

    public int Id { get; set; }

    public string? Text { get; set; }

    public int? IdQuestion { get; set; }

    public bool? RightAnswer { get => rightAnswer; set
        {
            rightAnswer = value;
            Signal(nameof(RightAnswer));
        }
    }
    [JsonIgnore]
    public virtual Question? IdQuestionNavigation { get; set; }

    public bool IsChecked { get; set; }

    [JsonIgnore]
    public Command<Answer> RemoveAnswer { get; private set; }
    public Answer()
    {
        RemoveAnswer = new Command<Answer>(async s =>
        {
            if (IdQuestionNavigation == null)
                return;

            if (s.Id == 0)
            {
                IdQuestionNavigation.Answers.Remove(s);
                return;
            }
            var response = await HttpClients.HttpClient.GetAsync($"Teacher/RemoveAnswer?answer=" + s.Id);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(await response.Content.ReadAsStringAsync());
                return;
            }
            else
                IdQuestionNavigation.Answers.Remove(s);
        });
    }
}

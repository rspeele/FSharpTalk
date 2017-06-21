using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows;

namespace CSharpConsoleApp2
{
    class ConsoleQuestionAsker : IQuestionable
    {
        public string[] Ask(string[] questions)
        {
            var answers = new List<string>();
            foreach (var question in questions)
            {
                Console.WriteLine(question);
                answers.Add(Console.ReadLine() ?? "");
            }
            return answers.ToArray();
        }
    }
    class FormQuestionAsker : IQuestionable
    {
        public string[] Ask(string[] questions)
        {
            var stackPanel = new StackPanel();
            var form = new Window
            {
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
            };

            var textboxes = new List<TextBox>();
            foreach (var question in questions)
            {
                stackPanel.Children.Add(new Label { Content = question });
                var textbox = new TextBox();
                textboxes.Add(textbox);
                stackPanel.Children.Add(textbox);
            }

            var button = new Button { Content = "Submit" };
            button.Click += (_, __) => form.Close();
            stackPanel.Children.Add(button);
            form.Content = stackPanel;
            var dialogResult = form.ShowDialog();
            return textboxes.Select(t => t.Text).ToArray();
        }
    }
}

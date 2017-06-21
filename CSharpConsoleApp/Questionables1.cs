using System;
using System.Windows.Controls;
using System.Windows;

namespace CSharpConsoleApp
{
    class ConsoleQuestionAsker : IQuestionable
    {
        public string Ask(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine() ?? "";
        }
    }
    class FormQuestionAsker : IQuestionable
    {
        public string Ask(string question)
        {
            var stackPanel = new StackPanel();
            var form = new Window
            {
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
            };
            var label = new Label { Content = question };
            var textbox = new TextBox { TabIndex = 0 };
            var button = new Button { Content = "Submit" };
            textbox.KeyDown += (_, evt) =>
            {
                if (evt.Key == System.Windows.Input.Key.Enter) form.Close();
            };
            button.Click += (_, __) => form.Close();
            stackPanel.Children.Add(label);
            stackPanel.Children.Add(textbox);
            stackPanel.Children.Add(button);
            form.Content = stackPanel;
            var dialogResult = form.ShowDialog();
            return textbox.Text;
        }
    }
}

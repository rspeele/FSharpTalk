using System;

namespace CSharpConsoleApp2
{
    public interface IQuestionable
    {
        string[] Ask(string[] questions);
    }
    public static class QuestionableExtensions
    {
        public static string Ask(this IQuestionable questionable, string question)
        {
            var answers = questionable.Ask(new[] { question });
            return answers[0];
        }
        public static (string, string) Ask(this IQuestionable questionable, string question1, string question2)
        {
            var answers = questionable.Ask(new[] { question1, question2 });
            return (answers[0], answers[1]);
        }
    }
    class ConsoleApp2
    {
        static double CalculateArea(IQuestionable subject)
        {
            var shape = subject.Ask("What shape is it?");
            switch (shape.ToLowerInvariant())
            {
                case "rectangle":
                    var (width, height) = subject.Ask("How wide is the rectangle?", "How tall is the rectangle?");
                    return double.Parse(width) * double.Parse(height);
                case "circle":
                    var radius = double.Parse(subject.Ask("What is the circle's radius?"));
                    return radius * radius * Math.PI;
                default:
                    throw new Exception("Unsupported shape");
            }

        }
        [STAThread]
        static void Main(string[] args)
        {
            var asker = new FormQuestionAsker();
            try
            {
                var area = CalculateArea(asker);
                Console.WriteLine($"Result: {area}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}

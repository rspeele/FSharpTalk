using System;

namespace CSharpConsoleApp
{
    public interface IQuestionable
    {
        string Ask(string question);
    }

    class ConsoleApp1
    {
        static double CalculateArea(IQuestionable subject)
        {
            var shape = subject.Ask("What shape is it?");
            switch (shape.ToLowerInvariant())
            {
                case "rectangle":
                    var width = subject.Ask("How wide is the rectangle?");
                    var height = subject.Ask("How tall is the rectangle?");
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

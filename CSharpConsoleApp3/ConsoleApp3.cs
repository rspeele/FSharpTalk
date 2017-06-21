using System;
using System.Linq;

namespace CSharpConsoleApp3
{
    class ConsoleApp3
    {
        static Workflow<double> CalculateArea()
            => Workflow.Ask("What shape is it?").AndThen(shape =>
            {
                switch (shape.ToLowerInvariant())
                {
                    case "rectangle":
                        return Workflow.Ask("How wide is the rectangle?")
                            .Also(Workflow.Ask("How tall is the rectangle?"))
                            .AndThen(pair =>
                            {
                                var (width, height) = pair;
                                return Workflow.Done(double.Parse(width) * double.Parse(height));
                            });
                    case "circle":
                        return Workflow.Ask("What is the circle's radius?").AndThen(radiusInput =>
                        {
                            var radius = double.Parse(radiusInput);
                            return Workflow.Done(radius * radius * Math.PI);
                        });
                    default:
                        throw new Exception("Unsupported shape");
                }
            });

        [STAThread]
        static void Main(string[] args)
        {
            var asker = new FormQuestionAsker();
            try
            {
                var area = CalculateArea().Run(asker);
                Console.WriteLine($"Result: {area}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        [STAThread]
        static void Main2(string[] args)
        {
            var asker = new FormQuestionAsker();
            try
            {
                var (area1, area2) = CalculateArea().Also(CalculateArea()).Run(asker);
                Console.WriteLine($"Shape 1's area is: {area1}, shape 2's area is {area2}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}

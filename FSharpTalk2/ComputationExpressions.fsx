(*

The last feature I want to talk about is a big one that lets you create really expressive abstractions hiding
things that we usually accept as facts of programming life.

I know I said this talk was going to be about Real Serious Business Applications, but let's go back to square one for a
few minutes. Please bear with me because my goal is to demonstrate a big idea, so I want the example to be as simple
as possible so we don't lose the forest for the trees. So let's cut our teeth on the simple case, then I will show how it
applies to a real world problem I bet most of you have encountered.

Most programmers took their baby steps on little console applications
that prompt the user for text input, like this one to calculate area:

*)

(*csharp

public static double GetArea()
{
    Console.WriteLine("What kind of shape is it? Circle or rectangle?");
    var shapeType = Console.ReadLine();
    switch (shapeType.ToUpperInvariant())
    {
        case "CIRCLE":
            Console.WriteLine("What's the radius?");
            var radius = double.Parse(Console.ReadLine());
            return radius * radius * Math.PI;
        case "RECTANGLE":
            Console.WriteLine("What's the width?");
            var width = double.Parse(Console.ReadLine());
            Console.WriteLine("What's the height?");
            var height = double.Parse(Console.ReadLine());
            return width * height;
        default:
            throw new InvalidDataException("Don't know that shape, sorry");
    }
}

public static void Main()
{
    var area = GetArea();
    Console.WriteLine($"The area of the shape you described is {area:0.##}.");
}

*)

(*

Now, the above code is not very good. For one, it mixes the implementation of calculating area with the user interface
of a console application. Please ignore that for now because it's not the point and it would only clutter the example
to break that functionality out.

I assert that every console application written using Console.ReadLine() and Console.WriteLine() could be turned into
a web application, which displays a form with a paragraph of text, an input textbox, and a submit button. Here's one
for the above program:

// demo web app here (hide URL if possible)

For those of you who have worked on web applications, consider: how would you build this one? It's a very simple
one, so it won't take long with any technology, but will the code bear much resemblance to the console app?

I suspect it won't, since web apps are supposed to be stateless.
Suppose you use ASP.NET MVC. You'll need a controller action and a view for each step:

* Asking the initial question about what type of shape to work on
* Displaying the prompt for radius
* Displaying the prompt for width
* Displaying the prompt for height
* Displaying the result

The worst part is that when you get to this page (show height prompt) you need to explicitly hold onto the previously
entered width input to pass forward when the form is submitted. This is a dead simple example -- what if it got more
complicated? Introduce a new input at the start of the process, and have fun passing it forward to each subsequent step
until it's no longer needed.

The fact is that if you were placed in a competition with the console app developer and expected to respond to updated
requirements in the same time frame, you'd be at an unfair disadvantage. The console app code describes the workflow
directly, while the web app code splits it over the series of pages displayed to the user, and must explicitly pass
forward any state to subsequent pages.

Now we all know that if you want to decouple code from its dependencies, you are supposed to use an interface.
But I think introducing the most obvious interface here doesn't help us too much.

*)

(*csharp

public interface IQuestionAsker
{
    string Ask(string question);
}

public static double GetArea(IQuestionAsker asker)
{
    var shapeType = asker.Ask("What kind of shape is it? Circle or rectangle?");
    switch (shapeType.ToUpperInvariant())
    {
        case "CIRCLE":
            var radius = double.Parse(asker.Ask("What's the radius?"));
            return radius * radius * Math.PI;
        case "RECTANGLE":
            var width = double.Parse(asker.Ask("What's the width?"));
            var height = double.Parse(asker.Ask("What's the height?"));
            return width * height;
        default:
            throw new InvalidDataException("Don't know that shape, sorry");
    }
}

*)

(*

You can implement IQuestionAsker based on Console.ReadLine and Console.WriteLine, no problem. You can also probably
make it work for a desktop GUI by displaying a series of modal dialogs. But good luck implementing
it for HTML forms, because you're running on the server, and you can't tell the client to
do anything -- all you can do is respond to the request you're given. So the combination of this interface and
our code written against it don't really describe this thing as universally as we would like.

Is this just a fact of life? The web frameworks I'm aware of would suggest that it is. Stateless HTTP
server-side programming is just not the same thing as writing a console app in your second week of CS101.

At the same time, intuition tells us that this console app and this web app are the same thing with a different
skin on top. So what's missing?

Well, in a web application, each time we display a form, we don't really know how long it will take for the user
to submit that form. They might even close the tab and never submit it, or submit it multiple times by refreshing the
page.

*)


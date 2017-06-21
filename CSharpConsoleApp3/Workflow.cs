using System;
using System.Linq;

namespace CSharpConsoleApp3
{
    public class Workflow<T>
    {
        public bool Done { get; }

        // if done == true
        public T Result { get; }

        // if done == false
        public string[] Questions { get; } // questions to ask
        public Func<string[], Workflow<T>> OnResponses { get; } // how to proceed when we get responses

        public Workflow(T result) // make a workflow that has a result
        {
            Done = true;
            Result = result;
        }

        // make a workflow that has pending questions
        public Workflow(string[] questions, Func<string[], Workflow<T>> onResponses)
        {
            Done = false;
            Questions = questions;
            OnResponses = onResponses;
        }
    }

    public static class Workflow
    {
        // running a workflow is easy if we have a way of answering the questions
        public static T Run<T>(this Workflow<T> workflow, IQuestionable questionable)
        {
            if (workflow.Done) return workflow.Result;

            var responses = questionable.Ask(workflow.Questions);
            var nextWorkflow = workflow.OnResponses(responses);

            return nextWorkflow.Run(questionable);
        }

        public static Workflow<T> Done<T>(T result) => new Workflow<T>(result);

        public static Workflow<string> Ask(string question)
            => new Workflow<string>(new[] { question }, responses => Done(responses[0]));

        public static Workflow<T2> AndThen<T1, T2>(this Workflow<T1> first, Func<T1, Workflow<T2>> next)
        {
            if (first.Done) return next(first.Result);

            return new Workflow<T2>(first.Questions, responses =>
            {
                var firstResumed = first.OnResponses(responses);
                return firstResumed.AndThen(next);
            });
        }

        public static Workflow<(T1, T2)> Also<T1, T2>(this Workflow<T1> first, Workflow<T2> second)
        {
            if (first.Done) return second.AndThen(snd => Done((first.Result, snd)));

            if (second.Done) return first.AndThen(fst => Done((fst, second.Result)));

            var combinedQuestions =
                first.Questions.Concat(second.Questions).ToArray();

            return new Workflow<(T1, T2)>(combinedQuestions, responses =>
            {
                var firstResponses = responses.Take(first.Questions.Length).ToArray();
                var firstResumed = first.OnResponses(firstResponses);

                var secondResponses = responses.Skip(first.Questions.Length).ToArray();
                var secondResumed = second.OnResponses(secondResponses);

                return firstResumed.Also(secondResumed);
            });
        }
    }
}

using AspectInjector.Broker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kinder_app.Aspects
{
    [Aspect(Scope.Global)]
    [Injection(typeof(LogAspect))]
    public class LogAspect : Attribute
    {
        [Advice(Kind.Before)]
        public void LogEnter([Argument(Source.Name)] string name, [Argument(Source.Arguments)] object[] args)
        {
            Console.WriteLine($"Calling '{name}' method with {args[0]}");
        }
    }
}

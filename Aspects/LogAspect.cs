using AspectInjector.Broker;
using kinder_app.Models;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            Log.Information("Calling \"{name}\" method", name);

            string argumentsStr = "";
            foreach(var arg in args)
            {
                Type type = arg.GetType();
                if (typeof(List<ItemDTO>) == type)
                {
                    List<ItemDTO> temp = (List<ItemDTO>)arg;

                    foreach(var element in temp)
                    {
                        argumentsStr += element.ToString() + ";  ";
                    }

                    argumentsStr += "| ";
                }
                else
                {
                    argumentsStr += arg.ToString() + "| ";

                }

            }

            Log.Information("Arguments: {args}", argumentsStr);
        }

        [Advice(Kind.After)]
        public void LogEnd([Argument(Source.ReturnValue)] object returnVal)
        {
            string returnStr = "";
            Type type = returnVal.GetType();

            if (typeof(List<ItemDTO>) == type)
            {
                List<ItemDTO> temp = (List<ItemDTO>)returnVal;

                foreach (var element in temp)
                {
                    returnStr += element.ToString() + ";  ";
                }

                returnStr += "| ";
            }
            else
            {
                returnStr += returnVal.ToString() + "| ";

            }

            Log.Information("Return: {args}", returnStr);
        }
    }
}

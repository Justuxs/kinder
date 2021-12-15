using AspectInjector.Broker;
using kinder_app.Controllers;
using kinder_app.Data;
using kinder_app.Models;
using Microsoft.AspNetCore.Identity;
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

                    if(typeof(List<Item>) == type)
                    {
                        List<Item> temp = (List<Item>)arg;

                        foreach (var element in temp)
                        {
                            argumentsStr += element.ToString() + ";  ";
                        }

                        argumentsStr += "| ";
                    }
                    else
                    {
                        if (typeof(ApplicationDbContext) == type)
                        {
                            argumentsStr += "DB requested| ";
                        }
                        else
                        {
                            if (typeof(List<TransferModel>) == type)
                            {
                                List<TransferModel> temp = (List<TransferModel>)arg;

                                foreach (var element in temp)
                                {
                                    argumentsStr += element.ToString() + ";  ";
                                }

                                argumentsStr += "| ";
                            }
                            else
                            {
                                if (typeof(List<IdentityUser>) == type)
                                {
                                    List<IdentityUser> temp = (List<IdentityUser>)arg;

                                    foreach (var element in temp)
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
                        }
                        
                    }                    
                }
            }

            Log.Information("Arguments: {args}", argumentsStr);
        }

        [Advice(Kind.After)]
        public void LogEnd([Argument(Source.ReturnValue)] object returnVal, [Argument(Source.ReturnType)] Type type)
        {
            string returnStr = "";

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
                if (typeof(List<ApplicationUser>) == type)
                {
                    List<ApplicationUser> temp = (List<ApplicationUser>)returnVal;

                    foreach (var element in temp)
                    {
                        returnStr += element.ToString() + ";  ";
                    }

                    returnStr += "| ";
                }
                else 
                {
                    if (typeof(void) == type)
                    {
                        returnStr += "void return| ";
                    }
                    else
                    {

                        if (typeof(List<TransferModel>) == type)
                        {
                            List<TransferModel> temp = (List<TransferModel>)returnVal;

                            foreach (var element in temp)
                            {
                                returnStr += element.ToString() + ";  ";
                            }

                            returnStr += "| ";
                        }
                        else
                        {
                            if (typeof(List<Item>) == type)
                            {
                                List<Item> temp = (List<Item>)returnVal;

                                foreach (var element in temp)
                                {
                                    returnStr += element.ToString() + ";  ";
                                }

                                returnStr += "| ";
                            }
                            else
                            {
                                if (typeof(List<int>) == type)
                                {
                                    List<int> temp = (List<int>)returnVal;

                                    foreach (var element in temp)
                                    {
                                        returnStr += element.ToString() + ";  ";
                                    }

                                    returnStr += "| ";
                                }
                                else
                                {
                                    if(returnVal==null || type == null)
                                    {
                                        returnStr += "null |";
                                    }
                                    else
                                    {
                                        returnStr += returnVal.ToString() + "| ";
                                    }
                                        
                                }
                            }
                        }
                    }
                   
                }
            }

            Log.Information("Return: {args}", returnStr);
        }
    }
}

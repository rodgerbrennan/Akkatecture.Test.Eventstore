using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Akkatecture.Aggregates;
using System.Collections.Concurrent;
using System.Reflection;
using Akkatecture.Extensions;

namespace Akkatecture.Demo.Tests
{
    
    internal static class ExtensionMethods
    {   
        public static string ToEventCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i == 0 ? char.ToLower(x) : x));
        }
       
    }
    
}

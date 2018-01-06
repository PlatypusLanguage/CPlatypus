using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CPlatypus.Core;
using CPlatypus.Execution.StandardLibrary;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Object
{
    public abstract class PlatypusClass
    {
        public string Name;

        public PlatypusClass(string name)
        {
            Name = name;
        }

        public abstract PlatypusInstance Constructor(PlatypusContext currentContext, PlatypusSymbol currentSymbol,
            params object[] args);

        public abstract PlatypusInstance Addition(PlatypusContext currentContext, PlatypusSymbol currentSymbol,
            params object[] args);
        
        public abstract PlatypusStringInstance ToStringInstance(PlatypusContext currentContext, PlatypusSymbol currentSymbol,
            params object[] args);

        public PlatypusClassSymbol ToSymbol(PlatypusSymbol parent)
        {
            var classSymbol = new PlatypusClassSymbol(this, parent);

            foreach (var method in GetType().GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(PlatypusFunctionAttribute), false).Length > 0).ToArray())
            {
                var attribute =
                    method.GetCustomAttribute(typeof(PlatypusFunctionAttribute)) as PlatypusFunctionAttribute;

                classSymbol.Add(
                    new PlatypusFunction(attribute.Name, method.CreateDelegate(this)).ToSymbol(classSymbol));
            }

            return classSymbol;
        }

    }
}
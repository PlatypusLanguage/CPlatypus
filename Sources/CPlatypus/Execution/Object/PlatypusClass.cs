/*
 * Copyright (c) 2017 Platypus Language http://platypus.vfrz.fr/
 *  This file is part of CPlatypus.
 *
 *     CPlatypus is free software: you can redistribute it and/or modify
 *     it under the terms of the GNU General Public License as published by
 *     the Free Software Foundation, either version 3 of the License, or
 *     (at your option) any later version.
 *
 *     CPlatypus is distributed in the hope that it will be useful,
 *     but WITHOUT ANY WARRANTY; without even the implied warranty of
 *     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *     GNU General Public License for more details.
 *
 *     You should have received a copy of the GNU General Public License
 *     along with CPlatypus.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CPlatypus.Core;
using CPlatypus.Framework.Execution;
using CPlatypus.Framework.Semantic;
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

        public abstract PlatypusInstance Create(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);

        public abstract PlatypusInstance Constructor(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);

        public abstract PlatypusInstance PlusOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);

        public abstract PlatypusInstance MinusOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);

        public abstract PlatypusInstance DivideOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);

        public abstract PlatypusInstance MultiplyOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);

        public abstract PlatypusInstance EqualOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);
        
        public abstract PlatypusInstance GreaterOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);
        
        public abstract PlatypusInstance GreaterEqualOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);
        
        public abstract PlatypusInstance LessOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);

        public abstract PlatypusInstance LessEqualOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);

        public abstract PlatypusInstance ToStringInstance(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args);

        public PlatypusClassSymbol ToSymbol(Symbol parent)
        {
            var classSymbol = new PlatypusClassSymbol(this, parent);

            foreach (var method in GetType().GetMethods()
                .Where(m => m.GetCustomAttribute<PlatypusFunctionAttribute>() != null).ToArray())
            {
                var attribute = method.GetCustomAttribute<PlatypusFunctionAttribute>();

                classSymbol.Add(
                    new PlatypusFunction(attribute.Name, attribute.Parameters, method.CreateDelegate(this)).ToSymbol(
                        classSymbol));
            }

            var methods = new List<(string RealName, string Name, List<string> Parameters)>
            {
                ("PlusOperator", "_plusoperator", new List<string> {"right"}),
                ("MinusOperator", "_minusoperator", new List<string> {"right"}),
                ("DivideOperator", "_divideoperator", new List<string> {"right"}),
                ("MultiplyOperator", "_multiplyoperator", new List<string> {"right"}),
                ("EqualOperator", "_equaloperator", new List<string> {"right"}),
                ("GreaterOperator", "_greateroperator", new List<string> {"right"}),
                ("GreaterEqualOperator", "_greaterequaloperator", new List<string> {"right"}),
                ("LessOperator", "_lessoperator", new List<string> {"right"}),
                ("LessEqualOperator", "_lessequaloperator", new List<string> {"right"}),
                ("ToStringInstance", "tostring", new List<string>())
            };

            foreach (var method in methods)
            {
                classSymbol.Add(
                    new PlatypusFunction(method.Name, method.Parameters,
                        GetType().GetMethod(method.RealName).CreateDelegate(this)).ToSymbol(classSymbol));
            }

            return classSymbol;
        }
    }
}
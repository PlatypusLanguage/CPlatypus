/*
 * Copyright (c) 2018 Platypus Language http://platypus.vfrz.fr/
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
using CPlatypus.Framework.Semantic;
using CPlatypus.Parser;
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

        public abstract PlatypusInstance Constructor(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args);

        public abstract PlatypusInstance PlusOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args);

        public abstract PlatypusInstance MinusOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args);

        public abstract PlatypusInstance DivideOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args);

        public abstract PlatypusInstance MultiplyOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args);

        public abstract PlatypusInstance EqualOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args);
        
        public abstract PlatypusInstance GreaterOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args);
        
        public abstract PlatypusInstance GreaterEqualOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args);
        
        public abstract PlatypusInstance LessOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args);

        public abstract PlatypusInstance LessEqualOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args);

        public abstract PlatypusInstance ToStringInstance(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args);

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

            var methods = new List<(string MethodName, string Name, List<string> Parameters)>
            {
                (nameof(PlusOperator), BinaryOperator.Plus.ToOperatorCode(), new List<string> {"right"}),
                (nameof(MinusOperator), BinaryOperator.Minus.ToOperatorCode(), new List<string> {"right"}),
                (nameof(DivideOperator), BinaryOperator.Divide.ToOperatorCode(), new List<string> {"right"}),
                (nameof(MultiplyOperator), BinaryOperator.Multiply.ToOperatorCode(), new List<string> {"right"}),
                (nameof(EqualOperator), BinaryOperator.Equal.ToOperatorCode(), new List<string> {"right"}),
                (nameof(GreaterOperator), BinaryOperator.Greater.ToOperatorCode(), new List<string> {"right"}),
                (nameof(GreaterEqualOperator), BinaryOperator.GreaterEqual.ToOperatorCode(), new List<string> {"right"}),
                (nameof(LessOperator), BinaryOperator.Less.ToOperatorCode(), new List<string> {"right"}),
                (nameof(LessEqualOperator), BinaryOperator.LessEqual.ToOperatorCode(), new List<string> {"right"}),
                (nameof(ToStringInstance), "tostring", new List<string>())
            };

            foreach (var method in methods)
            {
                classSymbol.Add(new PlatypusFunction(method.Name, method.Parameters, GetType().GetMethod(method.MethodName).CreateDelegate(this)).ToSymbol(classSymbol));
            }

            return classSymbol;
        }
    }
}
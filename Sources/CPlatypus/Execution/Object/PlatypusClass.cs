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

using System.Linq;
using System.Reflection;
using CPlatypus.Core;
using CPlatypus.Execution.StandardLibrary.Types;
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

        public abstract PlatypusInstance Constructor(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args);

        public abstract PlatypusInstance Addition(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args);
        
        public abstract PlatypusStringInstance ToStringInstance(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args);

        public PlatypusClassSymbol ToSymbol(Symbol parent)
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
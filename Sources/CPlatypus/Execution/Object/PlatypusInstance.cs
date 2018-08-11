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

using System;
using CPlatypus.Execution.Executors;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Framework.Execution;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Object
{
    public class PlatypusInstance
    {
        public PlatypusClassSymbol Symbol { get; }

        public Context Context { get; }

        public PlatypusInstance(PlatypusClassSymbol symbol, Context parentContext)
        {
            Symbol = symbol;
            Context = new PlatypusContext(symbol is null ? PlatypusContextType.Null : PlatypusContextType.ObjectInstance, parentContext);
        }

        public bool HasValue => Context.ContainsLocal("_value");

        public void SetValue(object value)
        {
            Context.Set("_value", value);
        }

        public object GetValue()
        {
            if (HasValue)
            {
                return Context.GetLocal("_value");
            }

            throw new Exception("IMPOSSIBLE EXCEPTION");
        }

        public T GetValue<T>()
        {
            if (HasValue)
            {
                return Context.GetLocal<T>("_value");
            }

            throw new Exception("IMPOSSIBLE EXCEPTION");
        }

        public override string ToString()
        {
            return new FunctionCallExecutor()
                .Execute(Symbol.GetLocal<PlatypusFunctionSymbol>("tostring"), Context, new object[0], this)
                .Context.GetLocal<string>("_value");
        }
    }
}
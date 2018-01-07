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

using CPlatypus.Core;
using CPlatypus.Execution.Object;
using CPlatypus.Framework.Semantic;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.StandardLibrary.Types
{
    public class PlatypusString : PlatypusClass
    {        
        public static PlatypusString Singleton { get; } = new PlatypusString();
        
        private PlatypusString() : base("String")
        {
        }

        public override PlatypusInstance Create(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args)
        {
            var instance = new PlatypusInstance(currentSymbol.TopSymbol.Get<PlatypusClassSymbol>(Name),
                currentContext);
            instance.Context.Add("_value", args[0] as string ?? "");
            return instance;
        }

        [PlatypusFunction("_constructor")]
        public override PlatypusInstance Constructor(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args)
        {
            return Create(currentContext, currentSymbol, args.JoinToString());
        }

        [PlatypusFunction("_plusoperator")]
        public override PlatypusInstance PlusOperator(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args)
        {
            var left = (PlatypusInstance) args[0];
            var right = (PlatypusInstance) args[1];

            return Create(
                currentContext, currentSymbol,
                (left.Symbol.Get<PlatypusFunctionSymbol>("_tostring")
                    .Execute(currentContext, currentSymbol, left)).Context.GetLocal<string>("_value") +
                (right.Symbol.Get<PlatypusFunctionSymbol>("_tostring")
                    .Execute(currentContext, currentSymbol, right)).Context.GetLocal<string>("_value")
            );
        }

        [PlatypusFunction("_tostring")]
        public override PlatypusInstance ToStringInstance(PlatypusContext currentContext,
            Symbol currentSymbol,
            params object[] args)
        {
            var arg = (PlatypusInstance) args[0];

            if (arg.Symbol.Name == Name)
            {
                return arg;
            }

            return Create(currentContext, currentSymbol);
        }
    }
}
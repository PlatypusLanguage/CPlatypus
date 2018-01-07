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
        public PlatypusString() : base("String")
        {
        }

        [PlatypusFunction("_constructor")]
        public override PlatypusInstance Constructor(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args)
        {
            var value = args.Join();
            return new PlatypusStringInstance(value, currentSymbol, currentContext);
        }

        [PlatypusFunction("_plusoperator")]
        public override PlatypusInstance PlusOperator(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args)
        {
            var left = (PlatypusInstance) args[0];
            var right = (PlatypusInstance) args[1];

            return new PlatypusStringInstance(
                ((PlatypusStringInstance) left.Symbol.Get<PlatypusFunctionSymbol>("_tostring")
                    .Execute(currentContext, currentSymbol, left)).Value +
                ((PlatypusStringInstance) right.Symbol.Get<PlatypusFunctionSymbol>("_tostring")
                    .Execute(currentContext, currentSymbol, right)).Value,
                currentSymbol, currentContext);
        }

        [PlatypusFunction("_tostring")]
        public override PlatypusStringInstance ToStringInstance(PlatypusContext currentContext,
            Symbol currentSymbol,
            params object[] args)
        {
            if (args[0] is PlatypusStringInstance platypusStringInstance)
            {
                return platypusStringInstance;
            }

            return new PlatypusStringInstance("", currentSymbol, currentContext);
        }
    }
}
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

using System;
using CPlatypus.Execution.Object;
using CPlatypus.Framework.Semantic;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.StandardLibrary.Types
{
    public class PlatypusInteger : PlatypusClass
    {
        public static PlatypusInteger Singleton { get; } = new PlatypusInteger();
        
        private PlatypusInteger() : base("Integer")
        {
        }

        public override PlatypusInstance Create(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args)
        {
            var instance = new PlatypusInstance(currentSymbol.TopSymbol.Get<PlatypusClassSymbol>(Name),
                currentContext);
            instance.SetValue(Convert.ToInt32(args[0]));
            return instance;
        }

        [PlatypusFunction("_constructor")]
        public override PlatypusInstance Constructor(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args)
        {
            var value = args.Length > 0 ? args[0] : 0;
            return Create(currentContext, currentSymbol, value);
        }

        [PlatypusFunction("_plusoperator")]
        public override PlatypusInstance PlusOperator(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args)
        {
            var left = (PlatypusInstance) args[0];
            var right = (PlatypusInstance) args[1];

            if (left.Symbol.Name == Name && right.Symbol.Name == Name)
            {
                return Create(currentContext, currentSymbol,
                    Convert.ToInt32(left.GetValue()) +
                    Convert.ToInt32(right.GetValue()));
            }

            return PlatypusString.Singleton.Create(
                currentContext, currentSymbol,
                (left.Symbol.Get<PlatypusFunctionSymbol>("_tostring")
                    .Execute(currentContext, currentSymbol, left)).GetValue<string>() +
                (right.Symbol.Get<PlatypusFunctionSymbol>("_tostring")
                    .Execute(currentContext, currentSymbol, right)).GetValue<string>()
            );
        }

        [PlatypusFunction("_minusoperator")]
        public override PlatypusInstance MinusOperator(PlatypusContext currentContext, Symbol currentSymbol, params object[] args)
        {
            throw new NotImplementedException();
        }

        [PlatypusFunction("_divideoperator")]
        public override PlatypusInstance DivideOperator(PlatypusContext currentContext, Symbol currentSymbol, params object[] args)
        {
            throw new NotImplementedException();
        }

        [PlatypusFunction("_multiplyoperator")]
        public override PlatypusInstance MultiplyOperator(PlatypusContext currentContext, Symbol currentSymbol, params object[] args)
        {
            throw new NotImplementedException();
        }

        [PlatypusFunction("_tostring")]
        public override PlatypusInstance ToStringInstance(PlatypusContext currentContext,
            Symbol currentSymbol,
            params object[] args)
        {
            var arg = (PlatypusInstance) args[0];

            if (arg.Symbol.Name == Name)
            {
                return PlatypusString.Singleton.Create(currentContext, currentSymbol, arg.GetValue().ToString());
            }

            return PlatypusString.Singleton.Create(currentContext, currentSymbol);
        }
    }
}
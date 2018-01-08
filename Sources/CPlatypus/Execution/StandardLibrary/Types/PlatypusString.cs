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
using CPlatypus.Core;
using CPlatypus.Execution.Executors;
using CPlatypus.Execution.Object;
using CPlatypus.Framework.Execution;
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

        public override PlatypusInstance Create(Context currentContext, Symbol currentSymbol,
            params object[] args)
        {
            var instance = new PlatypusInstance(currentSymbol.TopSymbol.Get<PlatypusClassSymbol>(Name),
                currentContext);
            instance.SetValue(args[0] as string ?? "");
            return instance;
        }

        [PlatypusFunction("_constructor")]
        public override PlatypusInstance Constructor(Context currentContext, Symbol currentSymbol,
            params object[] args)
        {
            return Create(currentContext, currentSymbol, args.JoinToString());
        }

        [PlatypusFunction("_plusoperator")]
        public override PlatypusInstance PlusOperator(Context currentContext, Symbol currentSymbol,
            params object[] args)
        {
            var left = (PlatypusInstance) args[0];
            var right = (PlatypusInstance) args[1];

            return Create(
                currentContext, currentSymbol,
                FunctionCallExecutor.Instance.Execute(left.Symbol.Get<PlatypusFunctionSymbol>("_tostring"),
                    currentContext, left).GetValue<string>() +
                FunctionCallExecutor.Instance.Execute(right.Symbol.Get<PlatypusFunctionSymbol>("_tostring"),
                    currentContext, right).GetValue<string>()
            );
        }

        [PlatypusFunction("_minusoperator")]
        public override PlatypusInstance MinusOperator(Context currentContext, Symbol currentSymbol, params object[] args)
        {
            throw new NotImplementedException();
        }

        [PlatypusFunction("_divideoperator")]
        public override PlatypusInstance DivideOperator(Context currentContext, Symbol currentSymbol, params object[] args)
        {
            throw new NotImplementedException();
        }

        [PlatypusFunction("_multiplyoperator")]
        public override PlatypusInstance MultiplyOperator(Context currentContext, Symbol currentSymbol, params object[] args)
        {
            throw new NotImplementedException();
        }
        
        [PlatypusFunction("_tostring")]
        public override PlatypusInstance ToStringInstance(Context currentContext,
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
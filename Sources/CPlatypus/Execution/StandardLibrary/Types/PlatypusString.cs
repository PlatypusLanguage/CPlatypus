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
using System.Collections.Generic;
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

        private PlatypusString() : base("string")
        {
        }

        public override PlatypusInstance Create(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args)
        {
            var instance = new PlatypusInstance(currentSymbol.Get<PlatypusClassSymbol>(Name),
                currentContext);
            if (args["value"] is PlatypusInstance platypusInstance)
            {
                instance.SetValue(platypusInstance.GetValue());
            }
            else
            {
                instance.SetValue(args["value"]);
            }
            return instance;
        }

        [PlatypusFunction("_constructor", "value")]
        public override PlatypusInstance Constructor(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args)
        {
            return Create(currentContext, currentSymbol, args);
        }

        public override PlatypusInstance PlusOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args)
        {
            var left = (PlatypusInstance) args["this"];
            var right = (PlatypusInstance) args["right"];

            return Create(
                currentContext, currentSymbol,
                new Dictionary<string, object>
                {
                    {
                        "value", new FunctionCallExecutor().Execute(
                                     left.Symbol.Get<PlatypusFunctionSymbol>("tostring"),
                                     currentContext, new object[0], left).GetValue<string>() +
                                 new FunctionCallExecutor().Execute(
                                     right.Symbol.Get<PlatypusFunctionSymbol>("tostring"),
                                     currentContext, new object[0], right).GetValue<string>()
                    }
                }
            );
        }

        public override PlatypusInstance MinusOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args)
        {
            throw new NotImplementedException();
        }

        public override PlatypusInstance DivideOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args)
        {
            throw new NotImplementedException();
        }

        public override PlatypusInstance MultiplyOperator(Context currentContext, Symbol currentSymbol,
            Dictionary<string, object> args)
        {
            throw new NotImplementedException();
        }

        public override PlatypusInstance EqualOperator(Context currentContext, Symbol currentSymbol, Dictionary<string, object> args)
        {
            throw new NotImplementedException();
        }

        public override PlatypusInstance GreaterOperator(Context currentContext, Symbol currentSymbol, Dictionary<string, object> args)
        {
            throw new NotImplementedException();
        }

        public override PlatypusInstance GreaterEqualOperator(Context currentContext, Symbol currentSymbol, Dictionary<string, object> args)
        {
            throw new NotImplementedException();
        }

        public override PlatypusInstance LessOperator(Context currentContext, Symbol currentSymbol, Dictionary<string, object> args)
        {
            throw new NotImplementedException();
        }

        public override PlatypusInstance LessEqualOperator(Context currentContext, Symbol currentSymbol, Dictionary<string, object> args)
        {
            throw new NotImplementedException();
        }

        public override PlatypusInstance ToStringInstance(Context currentContext,
            Symbol currentSymbol, Dictionary<string, object> args)
        {
            var arg = (PlatypusInstance) args["this"];

            if (arg.Symbol.Name == Name)
            {
                return arg;
            }

            return Create(currentContext, currentSymbol, new Dictionary<string, object>());
        }
    }
}
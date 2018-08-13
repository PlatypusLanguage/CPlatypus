﻿/*
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
using CPlatypus.Framework.Semantic;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.StandardLibrary.Types
{
    public class PlatypusInteger : PlatypusClass, IPlatypusBuiltInType<int>
    {
        public static PlatypusInteger Singleton { get; } = new PlatypusInteger();

        private PlatypusInteger() : base("integer")
        {
        }

        public PlatypusInstance Create(PlatypusContext currentContext, Symbol currentSymbol, int value)
        {
            var classSymbol = currentSymbol.Get<PlatypusClassSymbol>(Name);     
            var instance = new PlatypusInstance(classSymbol, currentContext);
            instance.SetValue(value);
            return instance;
        }
        
        [PlatypusFunction("_constructor", "value")]
        public override PlatypusInstance Constructor(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            if (args["value"].Symbol.Name != Name)
                throw new InvalidOperationException();
            currentContext.GetFirstParentContextOfType(PlatypusContextType.ObjectInstance).Set("_value", args["value"].GetValue());
            return PlatypusNullInstance.Instance;
        }

        public override PlatypusInstance PlusOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            var left = args["this"];
            var right = args["right"];

            if (left.Symbol.Name == Name && right.Symbol.Name == Name)
            {
                return Create(currentContext, currentSymbol, Convert.ToInt32(left.GetValue()) + Convert.ToInt32(right.GetValue()));
            }

            return PlatypusString.Singleton.Create(
                currentContext, currentSymbol,
                new FunctionCallExecutor().Execute(
                    left.Symbol.Get<PlatypusFunctionSymbol>("tostring"),
                    currentContext, new PlatypusInstance[0], left).GetValue<string>() +
                new FunctionCallExecutor().Execute(
                    right.Symbol.Get<PlatypusFunctionSymbol>("tostring"),
                    currentContext, new PlatypusInstance[0], right).GetValue<string>()
            );
        }

        public override PlatypusInstance MinusOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            var left = args["this"];
            var right = args["right"];

            if (left.Symbol.Name == Name && right.Symbol.Name == Name)
            {
                return Create(currentContext, currentSymbol, Convert.ToInt32(left.GetValue()) - Convert.ToInt32(right.GetValue()));
            }

            return PlatypusNullInstance.Instance;
        }

        public override PlatypusInstance DivideOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            var left = args["this"];
            var right = args["right"];

            if (left.Symbol.Name == Name && right.Symbol.Name == Name)
            {
                return Create(currentContext, currentSymbol, Convert.ToInt32(left.GetValue()) / Convert.ToInt32(right.GetValue()));
            }

            return PlatypusNullInstance.Instance;
        }

        public override PlatypusInstance MultiplyOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            var left = args["this"];
            var right = args["right"];

            if (left.Symbol.Name == Name && right.Symbol.Name == Name)
            {
                return Create(currentContext, currentSymbol, Convert.ToInt32(left.GetValue()) * Convert.ToInt32(right.GetValue()));
            }

            return PlatypusNullInstance.Instance;
        }

        public override PlatypusInstance EqualOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            var left = args["this"];
            var right = args["right"];

            if (left.Symbol.Name == Name && right.Symbol.Name == Name)
            {
                return PlatypusBoolean.Singleton.Create(currentContext, currentSymbol,
                    Convert.ToInt32(left.GetValue()).Equals(Convert.ToInt32(right.GetValue())));
            }

            return PlatypusNullInstance.Instance;
        }

        public override PlatypusInstance GreaterOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            var left = args["this"];
            var right = args["right"];

            if (left.Symbol.Name == Name && right.Symbol.Name == Name)
            {
                return PlatypusBoolean.Singleton.Create(currentContext, currentSymbol,
                    Convert.ToInt32(left.GetValue()) > Convert.ToInt32(right.GetValue()));
            }

            return PlatypusNullInstance.Instance;
        }

        public override PlatypusInstance GreaterEqualOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            var left = args["this"];
            var right = args["right"];

            if (left.Symbol.Name == Name && right.Symbol.Name == Name)
            {
                return PlatypusBoolean.Singleton.Create(currentContext, currentSymbol,
                    Convert.ToInt32(left.GetValue()) >= Convert.ToInt32(right.GetValue()));
            }

            return PlatypusNullInstance.Instance;
        }

        public override PlatypusInstance LessOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            var left = args["this"];
            var right = args["right"];

            if (left.Symbol.Name == Name && right.Symbol.Name == Name)
            {
                return PlatypusBoolean.Singleton.Create(currentContext, currentSymbol,
                    Convert.ToInt32(left.GetValue()) < Convert.ToInt32(right.GetValue()));
            }

            return PlatypusNullInstance.Instance;
        }

        public override PlatypusInstance LessEqualOperator(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            var left = args["this"];
            var right = args["right"];

            if (left.Symbol.Name == Name && right.Symbol.Name == Name)
            {
                return PlatypusBoolean.Singleton.Create(currentContext, currentSymbol,
                    Convert.ToInt32(left.GetValue()) <= Convert.ToInt32(right.GetValue()));
            }

            return PlatypusNullInstance.Instance;
        }

        public override PlatypusInstance ToStringInstance(PlatypusContext currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            var arg = args["this"];

            if (arg.Symbol.Name == Name)
            {
                return PlatypusString.Singleton.Create(currentContext, currentSymbol, arg.GetValue().ToString());
            }

            return PlatypusString.Singleton.Create(currentContext, currentSymbol, string.Empty);
        }
    }
}
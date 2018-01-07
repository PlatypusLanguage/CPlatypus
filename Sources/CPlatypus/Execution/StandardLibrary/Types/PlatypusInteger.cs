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

namespace CPlatypus.Execution.StandardLibrary.Types
{
    public class PlatypusInteger : PlatypusClass
    {
        public PlatypusInteger() : base("Integer")
        {
        }

        [PlatypusFunction("_constructor")]
        public override PlatypusInstance Constructor(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args)
        {
            var value = args.Length > 0 ? Convert.ToInt32(args[0]) : 0;
            return new PlatypusIntegerInstance(value, currentSymbol, currentContext);
        }

        [PlatypusFunction("_addition")]
        public override PlatypusInstance Addition(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args)
        {
            var left = args[0];
            var right = args[1];

            if (left is PlatypusIntegerInstance leftInteger && right is PlatypusIntegerInstance rightInteger)
            {
                return new PlatypusIntegerInstance(leftInteger.Value + rightInteger.Value,
                    currentSymbol, currentContext);
            }

            return PlatypusNullInstance.Instance;
        }

        [PlatypusFunction("_tostring")]
        public override PlatypusStringInstance ToStringInstance(PlatypusContext currentContext,
            Symbol currentSymbol,
            params object[] args)
        {
            if (args[0] is PlatypusIntegerInstance platypusIntegerInstance)
            {
                return new PlatypusStringInstance(platypusIntegerInstance.ToString(),
                    currentSymbol, currentContext);
            }

            return new PlatypusStringInstance("", currentSymbol, currentContext);
        }
    }
}
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
using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Framework.Execution;
using CPlatypus.Framework.Semantic;

namespace CPlatypus.Execution.StandardLibrary.IO
{
    public class PlatypusPrintFunction : PlatypusFunction
    {
        public static PlatypusPrintFunction Singleton { get; } = new PlatypusPrintFunction();

        private PlatypusPrintFunction() : base("print", new List<string> {"value"}, "Print")
        {
        }

        public PlatypusInstance Print(Context currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {
            Console.WriteLine(args["value"]);
            return PlatypusNullInstance.Instance;
        }
    }
}
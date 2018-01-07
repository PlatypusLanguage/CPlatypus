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
using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Framework.Semantic;

namespace CPlatypus.Execution.StandardLibrary.IO
{
    public class PlatypusPrintFunction : PlatypusFunction
    {
        public PlatypusPrintFunction() : base("print", "Print")
        {
        }
        
        public PlatypusInstance Print(PlatypusContext currentContext, Symbol currentSymbol,
            params object[] args)
        {
            Console.WriteLine(args.Join());
            return PlatypusNullInstance.Instance;
        }
    }
}
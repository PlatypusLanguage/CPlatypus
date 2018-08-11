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
using CPlatypus.Core;
using CPlatypus.Framework.Execution;
using CPlatypus.Framework.Semantic;
using CPlatypus.Semantic;

namespace CPlatypus.Execution.Object
{
    public class PlatypusFunction
    {
        public string Name { get; }

        public List<string> Parameters { get; }
        
        private readonly Delegate _delegateFunction;
        
        public PlatypusFunction(string name, List<string> parameters, Delegate delegateFunction)
        {
            Name = name;
            Parameters = parameters;
            _delegateFunction = delegateFunction;
        }
        
        public PlatypusFunction(string name, List<string> parameters, string realName)
        {
            Name = name;
            Parameters = parameters;
            _delegateFunction = GetType().GetMethod(realName).CreateDelegate(this);
        }

        public PlatypusInstance Execute(Context currentContext, Symbol currentSymbol, Dictionary<string, PlatypusInstance> args)
        {          
            return (PlatypusInstance) _delegateFunction.DynamicInvoke(currentContext, currentSymbol, args);
        }
        
        public PlatypusFunctionSymbol ToSymbol(Symbol parent)
        {
            return new PlatypusFunctionSymbol(this, parent);
        }
    }
}
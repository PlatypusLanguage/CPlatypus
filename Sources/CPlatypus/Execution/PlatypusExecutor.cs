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
using CPlatypus.Execution.Executors;
using CPlatypus.Framework.Execution;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution
{
    public class PlatypusExecutor
    {
        private readonly Context _context;
        
        public PlatypusExecutor()
        {
            _context = new PlatypusContext(PlatypusContextType.Global, null);
        }

        public void Execute(PlatypusNode ast, PlatypusModuleSymbol moduleSymbol = null)
        {
            moduleSymbol = moduleSymbol ?? PlatypusModuleSymbol.CreateGlobalModule();
            
            if (!(ast is CodeNode))
            {
                throw new ArgumentException();
            }

            new BodyExecutor().Execute(ast, _context, moduleSymbol);
        }
    }
}
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

using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary.Core;
using CPlatypus.Execution.StandardLibrary.IO;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Framework.Semantic;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Semantic
{
    public class PlatypusModuleSymbol : Symbol
    {        
        public ModuleNode ModuleNode { get; }
        
        public PlatypusModuleSymbol(ModuleNode moduleNode, Symbol parent) : base(parent)
        {
            Name = moduleNode.Name.Value;
            ModuleNode = moduleNode;
        }

        public static PlatypusModuleSymbol CreateGlobalModule(string name = "Global Module")
        {
            var globalModule = new PlatypusModuleSymbol(name);
            
            // Inject built in types, standard library classes and functions
            globalModule.InjectClass(PlatypusBoolean.Singleton);
            globalModule.InjectClass(PlatypusInteger.Singleton);
            globalModule.InjectClass(PlatypusString.Singleton);
            
            globalModule.InjectFunction(PlatypusPrintFunction.Singleton);
            globalModule.InjectFunction(PlatypusReadFunction.Singleton);
            globalModule.InjectFunction(PlatypusExitFunction.Singleton);
            
            return globalModule;
        }
        
        public void InjectClass(PlatypusClass clazz)
        {
            Add(clazz.ToSymbol(this));
        }

        public void InjectFunction(PlatypusFunction function)
        {
            Add(function.ToSymbol(this));
        }
        
        private PlatypusModuleSymbol(string name) : base(null)
        {
            Name = name;
        }
    }
}
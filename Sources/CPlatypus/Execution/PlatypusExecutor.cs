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

using CPlatypus.Core;
using CPlatypus.Execution.Executors;
using CPlatypus.Execution.Object;
using CPlatypus.Execution.StandardLibrary.IO;
using CPlatypus.Execution.StandardLibrary.Types;
using CPlatypus.Framework.Execution;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using CPlatypus.Semantic;

namespace CPlatypus.Execution
{
    public class PlatypusExecutor : IPlatypusVisitor
    {
        private PlatypusModuleSymbol _moduleSymbol;

        private Context _context;
        
        public PlatypusExecutor(PlatypusModuleSymbol globalModuleSymbol = null)
        {
            _moduleSymbol = globalModuleSymbol ?? PlatypusModuleSymbol.CreateGlobalModule();
            
            _context = new PlatypusContext(PlatypusContextType.Global, null);

            // Inject standard library classes and functions
            
            InjectClass(PlatypusBoolean.Singleton);
            InjectClass(PlatypusInteger.Singleton);
            InjectClass(PlatypusString.Singleton);
            
            InjectFunction(PlatypusPrintFunction.Singleton);
            InjectFunction(PlatypusReadFunction.Singleton);
        }

        public void InjectClass(PlatypusClass clazz)
        {
            _moduleSymbol.Add(clazz.ToSymbol(_moduleSymbol));
        }

        public void InjectFunction(PlatypusFunction function)
        {
            _moduleSymbol.Add(function.ToSymbol(_moduleSymbol));
        }

        public void Execute(PlatypusNode ast)
        {
            ast.Accept(this, null);
        }

        public void Visit(ArgumentListNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(ArrayAccessNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(AttributeAccessNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(BinaryOperationNode node, PlatypusNode parent)
        {
            new BinaryOperationExecutor().Execute(node, _context, _moduleSymbol);
        }

        public void Visit(BooleanNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(CharNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(ClassNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(CodeNode node, PlatypusNode parent)
        {
            new BodyExecutor().Execute(node, _context, _moduleSymbol);
        }

        public void Visit(ConstructorNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(IdentifierNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(IfNode node, PlatypusNode parent)
        {
            new IfExecutor().Execute(node, _context, _moduleSymbol);
        }

        public void Visit(IntegerNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(FloatNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(ForNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(FunctionCallNode node, PlatypusNode parent)
        {
            new FunctionCallExecutor().Execute(node, _context, _moduleSymbol);
        }

        public void Visit(FunctionNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(ModuleNode node, PlatypusNode parent)
        {
            node.AcceptChildren(this, node);
        }

        public void Visit(NewNode node, PlatypusNode parent)
        {
            new NewExecutor().Execute(node, _context, _moduleSymbol);
        }

        public void Visit(ParameterListNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(ReturnNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(ThisNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(UnaryOperationNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(VariableDeclarationNode node, PlatypusNode parent)
        {
            new VariableDeclarationExecutor().Execute(node, _context, _moduleSymbol);
        }

        public void Visit(StringNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(WhileNode node, PlatypusNode parent)
        {
            new WhileExecutor().Execute(node, _context, _moduleSymbol);
        }
    }
}
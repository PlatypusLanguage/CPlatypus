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

using CPlatypus.Core;
using CPlatypus.Framework.Semantic;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Semantic
{
    public class PlatypusAnalyzer : IPlatypusVisitor
    {
        private readonly PlatypusNode _ast;

        private Symbol _currentSymbol;

        public PlatypusAnalyzer(PlatypusNode ast)
        {
            _ast = ast;
        }

        public PlatypusModuleSymbol Analyze()
        {
            var globalModule = PlatypusModuleSymbol.CreateGlobalModule();

            _currentSymbol = globalModule;
            
            _ast.Accept(this, _ast);

            return globalModule;
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
            
        }

        public void Visit(BooleanNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(CharNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(ClassNode node, PlatypusNode parent)
        {
            var clazz = new PlatypusClassSymbol(node, _currentSymbol);
            _currentSymbol.Add(clazz);
            _currentSymbol = clazz;
            
            node.AcceptChildren(this, node);

            _currentSymbol = clazz.Parent;
        }

        public void Visit(CodeNode node, PlatypusNode parent)
        {
            node.AcceptChildren(this, node);
        }

        public void Visit(ConstructorNode node, PlatypusNode parent)
        {
            var constructor = new PlatypusFunctionSymbol(node, _currentSymbol);
            _currentSymbol.Add(constructor);
            _currentSymbol = constructor;
            
            node.AcceptChildren(this, node);

            _currentSymbol = constructor.Parent;
        }

        public void Visit(IdentifierNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(IfNode node, PlatypusNode parent)
        {
            
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
            
        }

        public void Visit(FunctionNode node, PlatypusNode parent)
        {
            var function = new PlatypusFunctionSymbol(node, _currentSymbol);
            _currentSymbol.Add(function);
            _currentSymbol = function;
            
            node.AcceptChildren(this, node);

            _currentSymbol = function.Parent;
        }

        public void Visit(ModuleNode node, PlatypusNode parent)
        {
            var module = new PlatypusModuleSymbol(node, _currentSymbol);
            _currentSymbol.Add(module);
            _currentSymbol = module;
            
            node.AcceptChildren(this, node);

            _currentSymbol = module.Parent;
        }

        public void Visit(NewNode node, PlatypusNode parent)
        {
            
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
            
        }

        public void Visit(StringNode node, PlatypusNode parent)
        {
            
        }

        public void Visit(WhileNode node, PlatypusNode parent)
        {
            
        }
    }
}
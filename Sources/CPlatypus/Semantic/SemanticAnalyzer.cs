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

using CPlatypus.Framework.Semantic;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Semantic
{
    public class SemanticAnalyzer : IPlatypusVisitor
    {
        private readonly PlatypusNode _tree;

        private SymbolTable _symbolTable;

        public SemanticAnalyzer(PlatypusNode tree)
        {
            _tree = tree;
        }

        public SymbolTable Analyze()
        {
            _symbolTable = new SymbolTable();

            _tree.Accept(this, _tree);

            return _symbolTable;
        }

        public void Visit(ArgumentListNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(ArrayAccessNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(AttributeAccessNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(BinaryOperationNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(BooleanNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(CharNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(ClassNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            var classSymbol = new PlatypusClassSymbol(node, _symbolTable.CurrentScope);
            _symbolTable.CurrentScope.Insert(classSymbol);
            _symbolTable.PushScope(classSymbol);

            node.AcceptChildren(this, node);

            _symbolTable.PopScope();
        }

        public void Visit(CodeNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(ConstructorNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            var constructorSymbol = new PlatypusConstructorSymbol(node, _symbolTable.CurrentScope);
            _symbolTable.CurrentScope.Insert(constructorSymbol);
            _symbolTable.PushScope(constructorSymbol);

            node.AcceptChildren(this, node);

            _symbolTable.PopScope();
        }

        public void Visit(IdentifierNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(IfNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(IntegerNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(FloatNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(ForNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(FunctionCallNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(FunctionNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            var functionSymbol = new PlatypusFunctionSymbol(node, _symbolTable.CurrentScope);
            _symbolTable.CurrentScope.Insert(functionSymbol);
            _symbolTable.PushScope(functionSymbol);

            node.AcceptChildren(this, node);

            _symbolTable.PopScope();
        }

        public void Visit(NewNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(ParameterListNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            foreach (var argument in node.Arguments)
            {
                _symbolTable.CurrentScope.Insert(new PlatypusParameterSymbol(node, argument,
                    _symbolTable.CurrentScope));
            }
            node.AcceptChildren(this, node);
        }

        public void Visit(ReturnNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(ThisNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(UnaryOperationNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(VariableDeclarationNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            var variableSymbol = new PlatypusVariableSymbol(node, _symbolTable.CurrentScope);
            _symbolTable.CurrentScope.Insert(variableSymbol);
            node.AcceptChildren(this, node);
        }

        public void Visit(StringNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }

        public void Visit(WhileNode node, PlatypusNode parent)
        {
            node.Scope = _symbolTable.CurrentScope;
            node.AcceptChildren(this, node);
        }
    }
}
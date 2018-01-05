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

using System.IO;
using CPlatypus.Parser;
using CPlatypus.Parser.Nodes;
using DotNetGraph;

namespace CPlatypus.Graphviz
{
    public class DotCompiler : IPlatypusVisitor
    {
        private readonly PlatypusNode _tree;

        private readonly DotGraph _graph;

        public DotCompiler(PlatypusNode tree)
        {
            _tree = tree;
            _graph = new DotGraph("g", true);
        }

        public void Compile(string fileName)
        {
            _tree.Accept(this, null);

            File.WriteAllText(fileName, _graph.Compile(false));
        }

        public void Visit(ArgumentListNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Argument List" + " [" + node.Scope.GetName() + "]"
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(ArrayAccessNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Array Access" + " [" + node.Scope.GetName() + "]"
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(AttributeAccessNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Attribute Access" + " [" + node.Scope.GetName() + "]"
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(BinaryOperationNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Binary Operation : " + node.OperationType + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Red3,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(BooleanNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Boolean : " + node.Value + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Orange,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(CharNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Char : " + node.Value + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Orange,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(ClassNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Class : " + node.Name.Value + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Cyan3,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(CodeNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Code (Body)" + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Cornflowerblue,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(ConstructorNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Constructor" + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Cyan3,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(IdentifierNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Identifier : " + node.Value + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Steelblue,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(IfNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "If" + " [" + node.Scope.GetName() + "]",
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(IntegerNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Integer : " + node.Value + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Orange,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(FloatNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Float : " + node.Value + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Orange,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(ForNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "For" + " [" + node.Scope.GetName() + "]"
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(FunctionCallNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Function Call" + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Orange,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(FunctionNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Function" + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Mediumseagreen,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(NewNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "New" + " [" + node.Scope.GetName() + "]"
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(ParameterListNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Parameters" + " [" + node.Scope.GetName() + "]"
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(ReturnNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Return" + " [" + node.Scope.GetName() + "]"
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(ThisNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "This" + " [" + node.Scope.GetName() + "]"
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(UnaryOperationNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Unary Operation : " + node.OperationType + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Red3,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(VariableDeclarationNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "Variable Declaration" + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Limegreen,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(StringNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "String : " + node.Value + " [" + node.Scope.GetName() + "]",
                FontColor = DotColor.White,
                FillColor = DotColor.Orange,
                Style = DotNodeStyle.Filled
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        public void Visit(WhileNode node, PlatypusNode parent)
        {
            _graph.Add(new DotNode("node" + node.Id)
            {
                Label = "While" + " [" + node.Scope.GetName() + "]"
            });
            CreateLink(parent, node);
            node.AcceptChildren(this, node);
        }

        private void CreateLink(PlatypusNode first, PlatypusNode second)
        {
            if (first != null && second != null && first != second)
            {
                _graph.Add(new DotArrow("node" + first.Id, "node" + second.Id));
            }
        }
    }
}
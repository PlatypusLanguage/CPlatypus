using System;
using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class WhileNode : PlatypusNode
    {
        public PlatypusNode Condition => Children[0];

        public CodeNode Body => Children[1] as CodeNode;

        public WhileNode(int id, PlatypusNode condition, CodeNode body, SourceLocation sourceLocation) : base(id,
            sourceLocation)
        {
            Children.Add(condition);
            Children.Add(body);
        }

        public override void Accept(IPlatypusVisitor visitor, PlatypusNode parent)
        {
            visitor.Visit(this, parent);
        }

        public override void AcceptChildren(IPlatypusVisitor visitor, PlatypusNode parent)
        {
            foreach (var child in Children)
            {
                child?.Accept(visitor, parent);
            }
        }
    }
}
using System.Collections.Generic;
using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class ArgumentListNode : PlatypusNode
    {
        public List<PlatypusNode> Arguments => Children;

        public ArgumentListNode(SourceLocation sourceLocation) : base(sourceLocation)
        {
        }

        public override void Accept(IPlatypusVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void AcceptChildren(IPlatypusVisitor visitor)
        {
            foreach (var child in Children)
            {
                child.Accept(visitor);
            }
        }
    }
}
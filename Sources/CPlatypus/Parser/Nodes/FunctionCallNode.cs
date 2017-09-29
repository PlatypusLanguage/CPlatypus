using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class FunctionCallNode : PlatypusNode
    {
        public PlatypusNode Target => Children[0];

        public ArgumentListNode Arguments => Children[1] as ArgumentListNode;

        public FunctionCallNode(PlatypusNode target, ArgumentListNode arguments, SourceLocation sourceLocation) : base(sourceLocation)
        {
            Children.Add(target);
            Children.Add(arguments);
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
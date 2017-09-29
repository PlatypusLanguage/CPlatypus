using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class ArrayAccessNode : PlatypusNode
    {
        public PlatypusNode Target => Children[0];

        public PlatypusNode Expression => Children[1];

        public ArrayAccessNode(PlatypusNode target, PlatypusNode expression, SourceLocation sourceLocation) : base(sourceLocation)
        {
            Children.Add(target);
            Children.Add(expression);
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
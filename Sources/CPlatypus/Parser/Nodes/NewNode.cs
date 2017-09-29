using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class NewNode : PlatypusNode
    {
        public PlatypusNode Target => Children[0];

        public NewNode(PlatypusNode target, SourceLocation sourceLocation) : base(sourceLocation)
        {
            Children.Add(target);
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
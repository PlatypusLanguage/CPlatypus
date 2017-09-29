using CPlatypus.Framework;

namespace CPlatypus.Parser.Nodes
{
    public class BinaryOperationNode : PlatypusNode
    {
        public readonly BinaryOperation OperationType;

        public PlatypusNode Left => Children[0];

        public PlatypusNode Right => Children[1];

        public BinaryOperationNode(BinaryOperation operationType, PlatypusNode left, PlatypusNode right,
            SourceLocation sourceLocation) : base(sourceLocation)
        {
            OperationType = operationType;
            Children.Add(left);
            Children.Add(right);
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
using CPlatypus.Framework;
using CPlatypus.Lexer;

namespace CPlatypus.Parser.Nodes
{
    public class UnaryOperationNode : PlatypusNode
    {
        public readonly UnaryOperation OperationType;

        public PlatypusNode Body => Children[0];

        public UnaryOperationNode(UnaryOperation operationType, PlatypusNode body, SourceLocation sourceLocation) : base(sourceLocation)
        {
            OperationType = operationType;
            Children.Add(body);
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
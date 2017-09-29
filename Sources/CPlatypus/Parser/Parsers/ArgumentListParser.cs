using System;
using CPlatypus.Lexer;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Parser.Parsers
{
    public class ArgumentListParser : NodeParser
    {
        public static ArgumentListParser Instance { get; } = new ArgumentListParser();

        private ArgumentListParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            throw new Exception("This function shouldn't be used ever !");
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            var argumentListNode = new ArgumentListNode(parser.Peek().SourceLocation);
            parser.ConsumeType(PlatypusTokenType.OpenParen);
            while (!parser.AcceptType(PlatypusTokenType.CloseParen))
            {
                argumentListNode.Children.Add(ExpressionParser.Instance.Parse(parser));
                if (!parser.AcceptType(PlatypusTokenType.Comma)) break;
            }
            return argumentListNode;
        }
    }
}
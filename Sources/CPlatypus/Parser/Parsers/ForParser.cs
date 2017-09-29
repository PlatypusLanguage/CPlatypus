using CPlatypus.Lexer;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Parser.Parsers
{
    public class ForParser : NodeParser
    {
        public static ForParser Instance { get; } = new ForParser();

        private ForParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            return parser.MatchType(PlatypusTokenType.ForKeyword);
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            var forKeywordToken = parser.ConsumeType(PlatypusTokenType.ForKeyword);

            parser.ConsumeType(PlatypusTokenType.OpenParen);
            var identifier = IdentifierParser.Instance.Parse(parser) as IdentifierNode;
            parser.ConsumeType(PlatypusTokenType.InKeyword);
            var target = ExpressionParser.Instance.Parse(parser);
            parser.ConsumeType(PlatypusTokenType.CloseParen);

            var body = CodeParser.Instance.ParseTill(parser);

            return new ForNode(parser.NextId(), identifier, target, body, forKeywordToken.SourceLocation);
        }
    }
}
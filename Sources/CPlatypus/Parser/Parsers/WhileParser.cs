using CPlatypus.Lexer;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Parser.Parsers
{
    public class WhileParser : NodeParser
    { 
        public static WhileParser Instance { get; } = new WhileParser();

        private WhileParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            return parser.MatchType(PlatypusTokenType.WhileKeyword);
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            var whileKeywordToken = parser.ConsumeType(PlatypusTokenType.WhileKeyword);

            parser.ConsumeType(PlatypusTokenType.OpenParen);
            var condition = ExpressionParser.Instance.Parse(parser);
            parser.ConsumeType(PlatypusTokenType.CloseParen);

            var body = CodeParser.Instance.ParseTill(parser);

            return new WhileNode(parser.NextId(), condition, body, whileKeywordToken.SourceLocation);
        }
    }
}
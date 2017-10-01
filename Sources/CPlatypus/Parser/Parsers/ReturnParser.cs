using CPlatypus.Lexer;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Parser.Parsers
{
    public class ReturnParser : NodeParser
    {
        public static ReturnParser Instance { get; } = new ReturnParser();

        private ReturnParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            return parser.MatchType(PlatypusTokenType.ReturnKeyword);
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            var returnKeywordToken = parser.ConsumeType(PlatypusTokenType.ReturnKeyword);
            var expression = ExpressionParser.Instance.Parse(parser);
            return new ReturnNode(parser.NextId(), expression, returnKeywordToken.SourceLocation);
        }
    }
}
using System.Collections.Generic;
using CPlatypus.Lexer;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Parser.Parsers
{
    public class IfParser : NodeParser
    {
        public static IfParser Instance { get; } = new IfParser();

        private IfParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            return parser.MatchType(PlatypusTokenType.IfKeyword);
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            var ifKeywordToken = parser.ConsumeType(PlatypusTokenType.IfKeyword);

            parser.ConsumeType(PlatypusTokenType.OpenParen);
            var condition = ExpressionParser.Instance.Parse(parser);
            parser.ConsumeType(PlatypusTokenType.CloseParen);

            PlatypusTokenType outType;

            var body = CodeParser.Instance.ParseTill(parser,
                new List<PlatypusTokenType> {PlatypusTokenType.EndKeyword, PlatypusTokenType.ElseKeyword}, out outType);

            CodeNode elseBody = null;
            IfNode elseIfNode = null;

            if (outType == PlatypusTokenType.ElseKeyword)
            {
                if (parser.MatchType(PlatypusTokenType.IfKeyword))
                {
                    elseIfNode = Instance.Parse(parser) as IfNode;
                }
                else
                {
                    elseBody = CodeParser.Instance.ParseTill(parser);
                }
            }

            return new IfNode(parser.NextId(), condition, body, elseIfNode, elseBody, ifKeywordToken.SourceLocation);
        }
    }
}
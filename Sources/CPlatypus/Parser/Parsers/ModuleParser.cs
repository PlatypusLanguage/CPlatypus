using CPlatypus.Lexer;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Parser.Parsers
{
    public class ModuleParser : NodeParser
    {
        public static ModuleParser Instance { get; } = new ModuleParser();

        private ModuleParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            return parser.MatchType(PlatypusTokenType.ModuleKeyword) &&
                   parser.MatchType(1, PlatypusTokenType.Identifier);
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            var moduleKeywordToken = parser.ConsumeType(PlatypusTokenType.ModuleKeyword);
            var nameNode = IdentifierParser.Instance.Parse(parser) as IdentifierNode;
            var bodyNode = CodeParser.Instance.ParseTill(parser);
            return new ModuleNode(parser.NextId(), nameNode, bodyNode, moduleKeywordToken.SourceLocation);
        }
    }
}
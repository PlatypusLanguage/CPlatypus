/*
 * Copyright (c) 2017 Platypus Language http://platypus.vfrz.fr/
 *  This file is part of CPlatypus.
 *
 *     CPlatypus is free software: you can redistribute it and/or modify
 *     it under the terms of the GNU General Public License as published by
 *     the Free Software Foundation, either version 3 of the License, or
 *     (at your option) any later version.
 *
 *     CPlatypus is distributed in the hope that it will be useful,
 *     but WITHOUT ANY WARRANTY; without even the implied warranty of
 *     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *     GNU General Public License for more details.
 *
 *     You should have received a copy of the GNU General Public License
 *     along with CPlatypus.  If not, see <http://www.gnu.org/licenses/>.
 */

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
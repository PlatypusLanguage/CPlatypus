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
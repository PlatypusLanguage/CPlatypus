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

using System;
using System.Collections.Generic;
using CPlatypus.Lexer;
using CPlatypus.Parser.Nodes;

namespace CPlatypus.Parser.Parsers
{
    public class ConstructorParser : NodeParser
    {
        public static ConstructorParser Instance { get; } = new ConstructorParser();

        private ConstructorParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            return parser.MatchType(PlatypusTokenType.ConstructorKeyword) &&
                   parser.MatchType(1, PlatypusTokenType.OpenParen);
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            var constructorKeywordToken = parser.ConsumeType(PlatypusTokenType.ConstructorKeyword);

            parser.ConsumeType(PlatypusTokenType.OpenParen);

            var parameters = new List<IdentifierNode>();

            while (!parser.MatchType(PlatypusTokenType.CloseParen))
            {
                if (!IdentifierParser.Instance.Match(parser))
                {
                    throw new Exception("Debug error # 1");
                }
                parameters.Add(IdentifierParser.Instance.Parse(parser) as IdentifierNode);
                if (parser.MatchType(PlatypusTokenType.Comma))
                {
                    parser.ConsumeType(PlatypusTokenType.Comma);
                }
                else
                {
                    break;
                }
            }

            parser.ConsumeType(PlatypusTokenType.CloseParen);

            var bodyNode = CodeParser.Instance.ParseTill(parser);

            return new ConstructorNode(bodyNode, parameters, constructorKeywordToken.SourceLocation);
        }
    }
}
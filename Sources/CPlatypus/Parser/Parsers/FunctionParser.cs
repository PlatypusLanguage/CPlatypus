﻿/*
 * Copyright (c) 2018 Platypus Language http://platypus.vfrz.fr/
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
    public class FunctionParser : NodeParser
    {
        public static FunctionParser Instance { get; } = new FunctionParser();

        private FunctionParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            return parser.MatchType(PlatypusTokenType.FunctionKeyword) &&
                   parser.MatchType(1, PlatypusTokenType.Identifier) &&
                   parser.MatchType(2, PlatypusTokenType.OpenParen);
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            var functionKeywordToken = parser.ConsumeType(PlatypusTokenType.FunctionKeyword);
            var nameNode = IdentifierParser.Instance.Parse(parser) as IdentifierNode;

            var parameters = ParameterListParser.Instance.Parse(parser) as ParameterListNode;

            var bodyNode = CodeParser.Instance.ParseTill(parser);

            return new FunctionNode(parser.NextId(), nameNode, parameters, bodyNode, functionKeywordToken.SourceLocation);
        }
    }
}
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
    public class VariableDeclarationParser : PlatypusNodeParser
    {
        public static VariableDeclarationParser Instance { get; } = new VariableDeclarationParser();

        private VariableDeclarationParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            return parser.MatchType(PlatypusTokenType.VarKeyword) && parser.MatchType(1, PlatypusTokenType.Identifier);
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            var varKeywordToken = parser.ConsumeType(PlatypusTokenType.VarKeyword);
            var nameNode = IdentifierParser.Instance.ParseWithoutConsume(parser);

            // If the identifier is not followed by a assign operator, it consume the identifier
            if (!parser.MatchType(1, PlatypusTokenType.EqualAssignOperator))
            {
                parser.ConsumeType(PlatypusTokenType.Identifier);
            }

            return new VariableDeclarationNode(parser.NextId(), nameNode, varKeywordToken.SourceLocation);
        }
    }
}
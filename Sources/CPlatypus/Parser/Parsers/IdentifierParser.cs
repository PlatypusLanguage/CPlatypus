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
    public class IdentifierParser : PlatypusNodeParser
    {
        public static IdentifierParser Instance { get; } = new IdentifierParser();

        private IdentifierParser()
        {
        }

        public override bool Match(PlatypusParser parser)
        {
            return parser.MatchType(PlatypusTokenType.Identifier);
        }

        public override PlatypusNode Parse(PlatypusParser parser)
        {
            var identifierToken = parser.ConsumeType(PlatypusTokenType.Identifier);
            return new IdentifierNode(parser.NextId(), identifierToken.Value, identifierToken.SourceLocation);
        }

        public IdentifierNode ParseWithoutConsume(PlatypusParser parser)
        {
            var identifierToken = parser.PeekType(PlatypusTokenType.Identifier);
            return new IdentifierNode(parser.NextId(), identifierToken.Value, identifierToken.SourceLocation);
        }
    }
}
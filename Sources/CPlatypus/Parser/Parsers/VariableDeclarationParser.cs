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
    public class VariableDeclarationParser : IPlatypusParser
    {
        public bool Match(PlatypusParser parser)
        {
            return parser.Match(PlatypusTokenType.VarKeyword) && parser.Match(1, PlatypusTokenType.Identifier);
        }

        public PlatypusNode Parse(PlatypusParser parser)
        {
            var varKeywordToken = parser.Consume(PlatypusTokenType.VarKeyword);
            var nameToken = parser.Consume(PlatypusTokenType.Identifier);

            if (parser.Match(PlatypusTokenType.EqualAssignOperator))
            {
                //TODO if assignment right after variable declaration
            }

            return new VariableDeclarationNode(nameToken.Value, varKeywordToken.SourceLocation);
        }
    }
}
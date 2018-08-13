/*
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

using CPlatypus.Framework;
using CPlatypus.Framework.Lexer;

namespace CPlatypus.Lexer
{
    public class PlatypusToken : Token
    {
        public PlatypusToken(PlatypusTokenType tokenType, string value, SourceLocation sourceLocation) : base(tokenType, value, sourceLocation)
        {
        }

        public new PlatypusTokenType TokenType
        {
            get
            {
                var type = base.TokenType;
                if (type is PlatypusTokenType t)
                    return t;
                return PlatypusTokenType.Unknown;
            }
        }

        public override string ToString()
        {
            return $"{nameof(TokenType)}: {TokenType}, {nameof(Value)}: {Value.Replace("\n", "\\n").Replace("\r", "\\r")}, {nameof(SourceLocation)}: {SourceLocation}";
        }
    }
}
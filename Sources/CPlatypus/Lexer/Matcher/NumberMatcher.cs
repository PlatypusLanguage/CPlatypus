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

using System.Text;
using CPlatypus.Framework;

namespace CPlatypus.Lexer.Matcher
{
    public class NumberMatcher : IMatcher
    {
        public PlatypusToken Match(SourceLocation location, Source source)
        {
            if (!char.IsDigit(source.PeekChar()))
            {
                return null;
            }

            var number = new StringBuilder();
            var currentChar = source.PeekChar();

            while (char.IsDigit(currentChar) || currentChar == '.')
            {
                if (currentChar == '.' && number.ToString().Contains("."))
                    break;

                number.Append(currentChar);
                currentChar = source.NextChar();
            }

            return number.ToString().Contains(".")
                ? new PlatypusToken(PlatypusTokenType.RealLiteral, number.ToString(), location)
                : new PlatypusToken(PlatypusTokenType.IntegerLiteral, number.ToString(), location);
        }
    }
}
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

using System.Text;
using CPlatypus.Core;
using CPlatypus.Framework;

namespace CPlatypus.Lexer.Matcher
{
    public class IdentifierMatcher : IMatcher
    {
        private PlatypusLanguage _language;

        public IdentifierMatcher(PlatypusLanguage language)
        {
            _language = language;
        }

        public PlatypusToken Match(SourceLocation location, Source source)
        {
            if (!char.IsLetter(source.PeekChar()))
            {
                return null;
            }

            var identifier = new StringBuilder();
            var currentChar = source.PeekChar();
            while (char.IsLetterOrDigit(currentChar))
            {
                identifier.Append(currentChar);
                currentChar = source.NextChar();
            }

            foreach (var key in _language.Keys)
            {
                if (_language[key].Equals(identifier.ToString()))
                {
                    return new PlatypusToken(key.ToPlatypusTokenType(), identifier.ToString(), location);
                }
            }

            return new PlatypusToken(PlatypusTokenType.Identifier, identifier.ToString(), location);
        }
    }
}
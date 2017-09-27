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
using CPlatypus.Core;
using CPlatypus.Framework;
using CPlatypus.Framework.Lexer;
using CPlatypus.Framework.Parser;
using CPlatypus.Lexer.Matcher;

namespace CPlatypus.Lexer
{
    public class PlatypusLexer : Lexer<PlatypusToken>
    {
        private readonly List<IMatcher> _matchers;
        private PlatypusLexerConfig _config;

        public PlatypusLexer(Source source, PlatypusLanguage language, PlatypusLexerConfig config) : base(source, config.BufferLookahead)
        {
            _config = config;
            _matchers = new List<IMatcher>
            {
                new WhiteSpaceMatcher(),

                new IdentifierMatcher(language),
                new NumberMatcher(),
                new TextLiteralMatcher(),
                new CharLiteralMatcher(),

                new CommentMatcher(),

                new OperatorMatcher(),

                new CharMatcher(PlatypusTokenType.Comma, ','),
                new CharMatcher(PlatypusTokenType.Dot, '.'),
                new CharMatcher(PlatypusTokenType.Colon, ':'),
                new CharMatcher(PlatypusTokenType.OpenParen, '('),
                new CharMatcher(PlatypusTokenType.CloseParen, ')'),
                new CharMatcher(PlatypusTokenType.OpenSquare, '['),
                new CharMatcher(PlatypusTokenType.CloseSquare, ']'),
                new CharMatcher(PlatypusTokenType.OpenBracket, '{'),
                new CharMatcher(PlatypusTokenType.CloseBracket, '}'),

                new EosMatcher()
            };
        }

        protected override PlatypusToken ExtractToken(TokenBuffer<PlatypusToken> buffer)
        {
            var location = Source.CurrentSourceLocation.Clone();

            foreach (var matcher in _matchers)
            {
                PlatypusToken token;
                if ((token = matcher.Match(location, Source)) == null) continue;

                if (token.TokenType == PlatypusTokenType.WhiteSpace && _config.IgnoreWhiteSpaces ||
                    token.TokenType == PlatypusTokenType.Comment && _config.IgnoreComments)
                {
                    return ExtractToken(buffer);
                }

                buffer.Push(token);
                return token;
            }

            var unknown = Source.PopChar().ToString();

            if (_config.IgnoreUnknownTokens)
            {
                return ExtractToken(buffer);
            }

            var unknownToken = new PlatypusToken(PlatypusTokenType.Unknown, unknown, location);

            buffer.Push(unknownToken);

            return unknownToken;
        }
    }
}
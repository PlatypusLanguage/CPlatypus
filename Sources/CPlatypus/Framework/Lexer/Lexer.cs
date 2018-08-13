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

using CPlatypus.Framework.Parser;

namespace CPlatypus.Framework.Lexer
{
    public abstract class Lexer<TToken> where TToken : Token
    {
        protected Source Source;

        private TokenBuffer<TToken> _buffer;

        public TToken CurrentToken => _buffer[0];

        protected Lexer(Source source, byte capacity = 1)
        {
            Source = source;
            _buffer = new TokenBuffer<TToken>(capacity);
        }

        //TODO Refactor buffer access to remove this method
        protected void InitializeBuffer()
        {
            for (var i = 0; i < _buffer.Capacity; i++)
            {
                ExtractToken(_buffer);
            }
        }

        public TToken NextToken()
        {
            return ExtractToken(_buffer);
        }

        public TToken ConsumeToken()
        {
            var token = _buffer.Peek();
            ExtractToken(_buffer);
            return token;
        }

        public TToken this[int index] => _buffer[index];

        protected abstract TToken ExtractToken(TokenBuffer<TToken> buffer);

        public bool EndOfLine => Source.EndOfLine;

        public bool EndOfStream => Source.EndOfStream;
    }
}
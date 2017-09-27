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

using CPlatypus.Framework.Parser;

namespace CPlatypus.Framework.Lexer
{
    public abstract class Lexer<TToken> where TToken : Token
    {
        protected Source Source;

        protected TokenBuffer<TToken> Buffer;

        public TToken CurrentToken => Buffer[0];

        protected Lexer(Source source, byte capacity = 1)
        {
            Source = source;
            Buffer = new TokenBuffer<TToken>(capacity);
        }

        public Lexer<TToken> InitializeBuffer()
        {
            for (var i = 0; i < Buffer.Capacity; i++)
            {
                ExtractToken(Buffer);
            }
            return this;
        }

        public TToken NextToken()
        {
            return ExtractToken(Buffer);
        }

        public TToken ConsumeToken()
        {
            var token = Buffer.Peek();
            ExtractToken(Buffer);
            return token;
        }

        public TToken this[int index] => Buffer[index];

        protected abstract TToken ExtractToken(TokenBuffer<TToken> buffer);

        public bool EndOfLine => Source.EndOfLine;

        public bool EndOfStream => Source.EndOfStream;
    }
}
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

using System.Collections;
using System.Collections.Generic;
using CPlatypus.Framework.Lexer;

namespace CPlatypus.Framework.Parser
{
    public class TokenBuffer<TToken> : IEnumerable<TToken> where TToken : Token
    {

        private readonly TToken[] _buffer;

        private int _top;

        public readonly int Capacity;

        public TokenBuffer(int capacity)
        {
            Capacity = capacity;
            _buffer = new TToken[Capacity];
        }

        public void Push(TToken item)
        {
            _buffer[_top] = item;
            _top = (_top + 1) % _buffer.Length;
        }

        public TToken Pop()
        {
            _top = (_buffer.Length + _top - 1) % _buffer.Length;
            return _buffer[_top];
        }

        public TToken Peek()
        {
            return _buffer[(_buffer.Length + _top - 1) % _buffer.Length];
        }

        public TToken this[int index]
        {
            get
            {
                if (index < 0) index = 0;
                if (index > Capacity) index = Capacity - 1;
                return _buffer [index];
            }
        }

        public IEnumerator<TToken> GetEnumerator()
        {
            for (var i = 0; i < Capacity; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
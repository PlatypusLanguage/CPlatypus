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

using System;
using System.IO;
using System.Text;

namespace CPlatypus.Framework
{
    public class Source : IDisposable
    {
        public static char CarriageReturn { get; } = '\r';
        public static char LineFeed { get; } = '\n';
        public static char Eos { get; } = '\0';

        public bool EndOfStream => _reader.EndOfStream;

        public bool EndOfLine => _currentChar == LineFeed || _currentChar == CarriageReturn;

        private AdvancedReader _reader;
        public SourceLocation CurrentSourceLocation => _reader.SourceLocation;
        private char _currentChar;

        public Source(Stream stream) : this(stream, Encoding.UTF8)
        {
        }

        public Source(Stream stream, Encoding encoding)
        {
            _reader = new AdvancedReader(stream, encoding);
            _currentChar = default(char);
        }

        public Source(string content) : this(content, Encoding.UTF8)
        {
        }

        public Source(string content, Encoding encoding) : this(new MemoryStream(encoding.GetBytes(content)), encoding)
        {
        }

        public static Source FromFile(string file, FileMode fileMode = FileMode.Open)
        {
            return new Source(new FileStream(file, fileMode));
        }

        public char PeekChar()
        {
            var charAsInt = _reader.Peek();

            if (charAsInt == -1)
            {
                return _currentChar = Eos;
            }

            return _currentChar = (char) charAsInt;
        }

        public char PopChar()
        {
            var charAsInt = _reader.Read();

            if (charAsInt == -1)
            {
                return _currentChar = Eos;
            }

            return _currentChar = (char) charAsInt;
        }

        public char NextChar()
        {
            PopChar();
            return PeekChar();
        }

        public string PeekFirstLine()
        {
            return _reader.PeekFirstLine();
        }

        public long GetPosition()
        {
            return _reader.BaseStream.Position;
        }

        public void Dispose()
        {
            _reader?.Dispose();
            _reader?.DiscardBufferedData();
        }
    }
}
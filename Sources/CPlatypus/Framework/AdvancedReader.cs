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
    public class AdvancedReader : StreamReader
    {
        public AdvancedReader(Stream stream) : base(stream)
        {
        }

        public AdvancedReader(Stream stream, bool detectEncodingFromByteOrderMarks) : base(stream,
            detectEncodingFromByteOrderMarks)
        {
        }

        public AdvancedReader(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }

        public AdvancedReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) : base(stream,
            encoding, detectEncodingFromByteOrderMarks)
        {
        }

        public AdvancedReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks,
            int bufferSize) : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize)
        {
        }

        public AdvancedReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks,
            int bufferSize, bool leaveOpen) : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize,
            leaveOpen)
        {
        }

        public override int Read()
        {
            var c = base.Read();
            if (c >= 0)
                AdvancePosition((char) c);
            return c;
        }

        public string PeekFirstLine()
        {
            var line = ReadLine();
            DiscardBufferedData();
            BaseStream.Position = 0;
            return line;
        }

        public SourceLocation SourceLocation { get; } = new SourceLocation(1, 1);

        private int _matched;

        private void AdvancePosition(char c)
        {
            if (Environment.NewLine[_matched] == c)
            {
                _matched++;
                if (_matched != Environment.NewLine.Length) return;
                SourceLocation.Line++;
                SourceLocation.Column = 1;
                _matched = 0;
            }
            else
            {
                _matched = 0;
                SourceLocation.Column++;
            }
        }
    }
}
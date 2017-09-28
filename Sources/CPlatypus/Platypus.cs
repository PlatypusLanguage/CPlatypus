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

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CPlatypus.Core;
using CPlatypus.Framework;
using CPlatypus.Lexer;
using CPlatypus.Parser;

namespace CPlatypus
{
    public class Platypus
    {
        public Platypus()
        {
        }

        public void InterpretFile(string file, FileMode fileMode = FileMode.Open,
            PlatypusLexerConfig lexerConfig = null)
        {
            InterpretSource(Source.FromFile(file, fileMode), lexerConfig);
        }

        public void InterpretString(string content, PlatypusLexerConfig lexerConfig = null)
        {
            InterpretSource(new Source(content), lexerConfig);
        }

        public void InterpretString(string content, Encoding encoding, PlatypusLexerConfig lexerConfig = null)
        {
            InterpretSource(new Source(content, encoding), lexerConfig);
        }

        public void InterpretSource(Source source, PlatypusLexerConfig lexerConfig = null)
        {
            if (lexerConfig == null)
                lexerConfig = new PlatypusLexerConfig();

            using (source)
            {
                var lexer = new PlatypusLexer(source, GetLanguageFromCode(source.PeekFirstLine()), lexerConfig);

                var parser = new PlatypusParser(lexer);

                var node = parser.Parse();

                Console.WriteLine(node);
            }
        }

        public static PlatypusLanguage GetLanguageFromCode(string codeLine)
        {
            return GetLanguageFromCode(codeLine, Encoding.UTF8);
        }

        public static PlatypusLanguage GetLanguageFromCode(string codeLine, Encoding encoding)
        {
            var regex = new Regex(@"#lang=([-\w_\\\/:\s]+)#", RegexOptions.IgnoreCase);
            var match = regex.Match(codeLine);
            if (!match.Success)
            {
                //TODO Notice the user
                return PlatypusLanguage.DefaultLanguage;
            }
            var file = (match.Groups[1].Value.EndsWith(".plang")
                ? match.Groups[1].Value
                : $"{match.Groups[1].Value}.plang");
            if (file.Equals("default.plang"))
            {
                return PlatypusLanguage.DefaultLanguage;
            }
            if (!File.Exists(file))
            {
                //TODO Notice the user
                return PlatypusLanguage.DefaultLanguage;
            }
            var language = PlatypusLanguage.FromFile(file, encoding);
            if (!language.IsCorrect)
            {
                //TODO Notice the user
                return PlatypusLanguage.DefaultLanguage;
            }
            return language;
        }
    }
}
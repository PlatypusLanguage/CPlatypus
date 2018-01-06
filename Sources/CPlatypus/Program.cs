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
using CPlatypus.Lexer;
using Fclp;
using System.IO;
using CPlatypus.Parser;

namespace CPlatypus
{
    public class ApplicationArguments
    {
        public string File { get; set; } = "";
        public string DotTree { get; set; } = "";
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var commandLineParser = new FluentCommandLineParser<ApplicationArguments>();

            var arguments = new ApplicationArguments();

            commandLineParser.Setup(arg => arg.File).As('f', "file").Required().WithDescription("Input file to process")
                .Callback(result => arguments.File = result);
            commandLineParser.Setup(arg => arg.DotTree).As('d', "dottree").Callback(result => arguments.DotTree = result);

            var cmArgs = commandLineParser.Parse(args);

            if (cmArgs.HasErrors)
            {
                Console.WriteLine(cmArgs.ErrorText);
                return;
            }

            var platypus = new Platypus();
            platypus.InterpretFile(arguments.File, FileMode.Open, new PlatypusLexerConfig
            {
                IgnoreComments = true,
                IgnoreWhiteSpaces = true,
                IgnoreUnknownTokens = false
            }, new PlatypusParserConfig
            {
                TreeDotFile = arguments.DotTree
            });
        }
    }
}
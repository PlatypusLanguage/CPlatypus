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
using System.Linq;
using CPlatypus.Core.Errors;
using CPlatypus.Lexer;
using CPlatypus.Parser;
using PowerArgs;

namespace CPlatypus
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parsedArguments = new PlatypusInterpreterArguments();

            try
            {
                parsedArguments = Args.Parse<PlatypusInterpreterArguments>(args);
            }
            catch (ArgException ex)
            {
                ErrorManager.Instance.Add(new PlatypusError(PlatypusErrorPrimaryType.RunArgumentsError, ex.Message));
            }

            if (ErrorManager.Instance.Get(PlatypusErrorPrimaryType.RunArgumentsError).Any())
            {
                ErrorManager.Instance.Print(PlatypusErrorPrimaryType.RunArgumentsError);
                return;
            }

            var platypus = new Platypus();

            var lexerConfig = new PlatypusLexerConfig
            {
                IgnoreComments = true,
                IgnoreWhiteSpaces = true,
                IgnoreUnknownTokens = parsedArguments.IgnoreUnknownTokens
            };

            var parserConfig = new PlatypusParserConfig
            {
                DotGraphFile = parsedArguments.DotGraphFile
            };

            if (parsedArguments.Interactive)
            {
                while (true)
                {
                    Console.Write('>');
                    platypus.InterpretString(Console.ReadLine(), lexerConfig, parserConfig);
                }
            }
            else
            {
                platypus.InterpretFile(parsedArguments.File, FileMode.Open, lexerConfig, parserConfig);
            }
        }
    }
}
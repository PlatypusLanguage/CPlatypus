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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CPlatypus.Core
{
    public class PlatypusLanguage : Dictionary<string, string>
    {
        public static string Pattern { get; } = @"(\w+):(\w+)";

        public bool IsCorrect
        {
            get
            {
                return Enum.GetValues(typeof(PlatypusKeywords)).Cast<PlatypusKeywords>()
                    .All(key => Keys.Any(
                        kw => key.ToKeywordIndex().Equals(kw, StringComparison.CurrentCulture)));
            }
        }

        public static PlatypusLanguage DefaultLanguage
        {
            get
            {
                var defaultLanguage = new PlatypusLanguage();
                foreach (var key in Enum.GetValues(typeof(PlatypusKeywords)).Cast<PlatypusKeywords>())
                {
                    defaultLanguage[key.ToKeywordIndex()] = key.ToKeywordIndex();
                }
                return defaultLanguage;
            }
        }

        public PlatypusLanguage(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var match = Regex.Match(line, Pattern);
                if (match.Success)
                {
                    this[match.Groups[1].Value] = match.Groups[2].Value;
                }
            }
        }

        public PlatypusLanguage(string content) : this(Regex.Split(content, "\r\n|\r|\n"))
        {
        }

        private PlatypusLanguage()
        {
        }

        public static PlatypusLanguage FromFile(string file)
        {
            return FromFile(file, Encoding.UTF8);
        }

        public static PlatypusLanguage FromFile(string file, Encoding encoding)
        {
            return new PlatypusLanguage(File.ReadAllLines(file, encoding));
        }
    }
}
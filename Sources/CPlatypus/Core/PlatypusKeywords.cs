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

using CPlatypus.Lexer;

namespace CPlatypus.Core
{
    public enum PlatypusKeywords
    {
        [KeywordIndex("var", PlatypusTokenType.VarKeyword)] Var,
        [KeywordIndex("static", PlatypusTokenType.StaticKeyword)] Static,
        [KeywordIndex("end", PlatypusTokenType.EndKeyword)] End,
        [KeywordIndex("function", PlatypusTokenType.FunctionKeyword)] Function,
        [KeywordIndex("class", PlatypusTokenType.ClassKeyword)] Class,
        [KeywordIndex("constructor", PlatypusTokenType.ConstructorKeyword)] Constructor,
        [KeywordIndex("this", PlatypusTokenType.ThisKeyword)] This,
        [KeywordIndex("new", PlatypusTokenType.NewKeyword)] New,
        [KeywordIndex("import", PlatypusTokenType.ImportKeyword)] Import,
        [KeywordIndex("true", PlatypusTokenType.TrueLiteral)] True,
        [KeywordIndex("false", PlatypusTokenType.FalseLiteral)] False,
        [KeywordIndex("is", PlatypusTokenType.IsOperator)] Is,
        [KeywordIndex("or", PlatypusTokenType.OrOperator)] Or,
        [KeywordIndex("and", PlatypusTokenType.AndOperator)] And,
        [KeywordIndex("if", PlatypusTokenType.IfKeyword)] If,
        [KeywordIndex("else", PlatypusTokenType.ElseKeyword)] Else,
        [KeywordIndex("while", PlatypusTokenType.WhileKeyword)] While,
        [KeywordIndex("for", PlatypusTokenType.ForKeyword)] For,
        [KeywordIndex("foreach", PlatypusTokenType.ForEachKeyword)] ForEach,
        [KeywordIndex("in", PlatypusTokenType.InKeyword)] In,
        [KeywordIndex("switch", PlatypusTokenType.SwitchKeyword)] Switch,
        [KeywordIndex("case", PlatypusTokenType.CaseKeyword)] Case,
        [KeywordIndex("default", PlatypusTokenType.DefaultKeyword)] Default,
        [KeywordIndex("try", PlatypusTokenType.TryKeyword)] Try,
        [KeywordIndex("catch", PlatypusTokenType.CatchKeyword)] Catch,
        [KeywordIndex("break", PlatypusTokenType.BreakKeyword)] Break,
        [KeywordIndex("continue", PlatypusTokenType.ContinueKeyword)] Continue
    }
}
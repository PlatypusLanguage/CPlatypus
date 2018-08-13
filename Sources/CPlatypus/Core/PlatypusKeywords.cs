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

using CPlatypus.Lexer;

namespace CPlatypus.Core
{
    public enum PlatypusKeywords
    {
        [PlatypusKeywordIndex("var", PlatypusTokenType.VarKeyword)] Var,
        [PlatypusKeywordIndex("static", PlatypusTokenType.StaticKeyword)] Static,
        [PlatypusKeywordIndex("end", PlatypusTokenType.EndKeyword)] End,
        [PlatypusKeywordIndex("function", PlatypusTokenType.FunctionKeyword)] Function,
        [PlatypusKeywordIndex("class", PlatypusTokenType.ClassKeyword)] Class,
        [PlatypusKeywordIndex("module", PlatypusTokenType.ModuleKeyword)] Module,
        [PlatypusKeywordIndex("constructor", PlatypusTokenType.ConstructorKeyword)] Constructor,
        [PlatypusKeywordIndex("this", PlatypusTokenType.ThisKeyword)] This,
        [PlatypusKeywordIndex("new", PlatypusTokenType.NewKeyword)] New,
        [PlatypusKeywordIndex("import", PlatypusTokenType.ImportKeyword)] Import,
        [PlatypusKeywordIndex("true", PlatypusTokenType.TrueLiteral)] True,
        [PlatypusKeywordIndex("false", PlatypusTokenType.FalseLiteral)] False,
        [PlatypusKeywordIndex("is", PlatypusTokenType.IsOperator)] Is,
        [PlatypusKeywordIndex("or", PlatypusTokenType.OrOperator)] Or,
        [PlatypusKeywordIndex("and", PlatypusTokenType.AndOperator)] And,
        [PlatypusKeywordIndex("return", PlatypusTokenType.ReturnKeyword)] Return,
        [PlatypusKeywordIndex("if", PlatypusTokenType.IfKeyword)] If,
        [PlatypusKeywordIndex("else", PlatypusTokenType.ElseKeyword)] Else,
        [PlatypusKeywordIndex("while", PlatypusTokenType.WhileKeyword)] While,
        [PlatypusKeywordIndex("for", PlatypusTokenType.ForKeyword)] For,
        [PlatypusKeywordIndex("foreach", PlatypusTokenType.ForEachKeyword)] ForEach,
        [PlatypusKeywordIndex("in", PlatypusTokenType.InKeyword)] In,
        [PlatypusKeywordIndex("switch", PlatypusTokenType.SwitchKeyword)] Switch,
        [PlatypusKeywordIndex("case", PlatypusTokenType.CaseKeyword)] Case,
        [PlatypusKeywordIndex("default", PlatypusTokenType.DefaultKeyword)] Default,
        [PlatypusKeywordIndex("try", PlatypusTokenType.TryKeyword)] Try,
        [PlatypusKeywordIndex("catch", PlatypusTokenType.CatchKeyword)] Catch,
        [PlatypusKeywordIndex("break", PlatypusTokenType.BreakKeyword)] Break,
        [PlatypusKeywordIndex("continue", PlatypusTokenType.ContinueKeyword)] Continue
    }
}
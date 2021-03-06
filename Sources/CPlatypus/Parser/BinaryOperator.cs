﻿/*
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

namespace CPlatypus.Parser
{
    public enum BinaryOperator
    {
        [OperatorCode("_assignoperator")] Assign,
        [OperatorCode("_plusoperator")] Plus,
        [OperatorCode("_minusoperator")] Minus,
        [OperatorCode("_multiplyoperator")] Multiply,
        [OperatorCode("_divideoperator")] Divide,
        [OperatorCode("_oroperator")] Or,
        [OperatorCode("_andoperator")] And,
        [OperatorCode("_equaloperator")] Equal,
        [OperatorCode("_notequaloperator")] NotEqual,
        [OperatorCode("_greateroperator")] Greater,
        [OperatorCode("_greaterequaloperator")] GreaterEqual,
        [OperatorCode("_isoperator")] Is,
        [OperatorCode("_lessoperator")] Less,
        [OperatorCode("_lessequaloperator")] LessEqual
    }
}
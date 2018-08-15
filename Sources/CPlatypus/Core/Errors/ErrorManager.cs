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
using System.Linq;

namespace CPlatypus.Core.Errors
{
    public class ErrorManager
    {
        private List<PlatypusError> _errors;

        public static ErrorManager Instance { get; } = new ErrorManager();
        
        private ErrorManager()
        {
            _errors = new List<PlatypusError>();
        }

        public void Add(PlatypusError error)
        {
            _errors.Add(error);
        }

        public void Print(PlatypusErrorPrimaryType? primaryType = null)
        {
            var errors = primaryType.HasValue ? _errors.Where(e => e.PrimaryType == primaryType.Value).ToList() : _errors.ToList();

            if (!errors.Any())
            {
                return;
            }

            Console.WriteLine($"{errors.Count} error(s) found");
            
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
        }
        
        public List<PlatypusError> Get(PlatypusErrorPrimaryType? primaryType = null)
        {
            if (primaryType.HasValue)
            {
                return _errors.Where(e => e.PrimaryType == primaryType).ToList();
            }
            
            return _errors.ToList();
        }

        public List<PlatypusError> GetAndClear(PlatypusErrorPrimaryType? primaryType = null)
        {
            var errors = Get(primaryType);
            Clear(primaryType);
            return errors;
        }

        public void Clear(PlatypusErrorPrimaryType? primaryType = null)
        {
            if (primaryType.HasValue)
            {
                _errors.RemoveAll(e => e.PrimaryType == primaryType);
            }
            else
            {
                _errors.Clear();
            }            
        }
    }
}
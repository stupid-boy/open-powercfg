/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
  
    Copyright (C) 2012 Andrey Mushatov ( openPowerCfg@gmail.com )
*/

using System;

namespace OpenPowerCfg.PowerManagement
{
    /// <summary>
    /// The exception that is thrown when an error occures while doing power 
    /// management.
    /// </summary>
    public class PowerManagerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the PowerManagerException class.
        /// </summary>
        public PowerManagerException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PowerManagerException class with a 
        /// specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PowerManagerException(string message)
            : base(message)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the PowerManagerException class with a 
        /// specified error message and Inner Exception.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="innerException">
        /// The message that describes the innerException.
        /// </param>
        public PowerManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
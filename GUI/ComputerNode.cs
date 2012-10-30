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
using System.Collections.Generic;
using System.Text;
using OpenPowerCfg;
using OpenPowerCfg.PowerManagement;
using System.Runtime.InteropServices;
using Aga.Controls.Tree;

namespace OpenPowerCfg.GUI
{
    internal class ComputerNode : Node
    {
        private readonly PersistentSettings settings;

        public ComputerNode(string name, PersistentSettings settings)
            : base(name)
        {
            this.settings = settings;
        }

        protected override void LoadChilds()
        {            
            IntPtr ptrToPowerSchemeGuid = IntPtr.Zero;
            try
            {
                uint buffSize = 100;
                uint schemeIndex = 0;

                ptrToPowerSchemeGuid =
                    Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));

                //Get the guids of all available power schemes.
                while (NativeMethods.PowerEnumerate(
                            IntPtr.Zero, IntPtr.Zero, IntPtr.Zero,
                            NativeMethods.PowerDataAccessor.AccessScheme,
                            schemeIndex, ptrToPowerSchemeGuid, ref buffSize) == 0)
                {
                    Guid schemeGuid = (Guid)Marshal.PtrToStructure(
                        ptrToPowerSchemeGuid, typeof(Guid));

                    SchemeNode n = new SchemeNode(schemeGuid, settings);
                   
                    childs.Add( n );

                    schemeIndex++;
                }
            }
            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }
            finally
            {
                Marshal.FreeHGlobal(ptrToPowerSchemeGuid);
            }
        }
    }
}

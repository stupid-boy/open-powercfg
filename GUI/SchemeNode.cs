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
using OpenPowerCfg.PowerManagement;
using System.Runtime.InteropServices;

namespace OpenPowerCfg.GUI
{
    class SchemeNode : Node 
    {
        private PersistentSettings settings;
        private PowerScheme powerScheme;

        public SchemeNode(Guid schemeGuid, PersistentSettings settings)
            : base() 
        {
            this.settings = settings;
            this.powerScheme = new PowerScheme();
            this.powerScheme.Guid = schemeGuid.ToString();

            IntPtr ptrToPowerSchemeGuid = IntPtr.Zero;
            IntPtr friendlyName = IntPtr.Zero;
            uint buffSize = 1000;

            try
            {
                friendlyName = Marshal.AllocHGlobal((int)buffSize);
                ptrToPowerSchemeGuid = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                Marshal.StructureToPtr(schemeGuid, ptrToPowerSchemeGuid, true);

                //Pass the guid retrieved in PowerEnumerate as parameter 
                //to get the power scheme name.
                NativeMethods.PowerReadFriendlyName(
                    IntPtr.Zero, ptrToPowerSchemeGuid, IntPtr.Zero, IntPtr.Zero,
                    friendlyName, ref buffSize);

                this.powerScheme.Name = Marshal.PtrToStringUni(friendlyName);

                this.powerScheme.Description = PowerManager.GetDescription(powerScheme.Guid, null, null);
            }
            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }
            finally
            {
                Marshal.FreeHGlobal(ptrToPowerSchemeGuid);
                Marshal.FreeHGlobal(friendlyName);
            }
        }

        public override string Text
        {
            get { return powerScheme.Name; }
            set { powerScheme.Name = value; }
        }

        public override String Description
        {
            get { return powerScheme.Description; }
        }


        protected override void LoadChilds()
        {
            IntPtr powerSchemeGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
            IntPtr subGroupGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
            uint bufferSize = 100;// (uint)Marshal.SizeOf(typeof(Guid));

            try
            {
                Guid powerSchemeGuid = new Guid(powerScheme.Guid);
                Marshal.StructureToPtr(powerSchemeGuid, powerSchemeGuidPtr, true);

                uint groupIndex = 0;
                //Enumerate sub groups
                while (NativeMethods.PowerEnumerate(
                            IntPtr.Zero, powerSchemeGuidPtr, IntPtr.Zero,
                            NativeMethods.PowerDataAccessor.AccessSubgroup,
                            groupIndex, subGroupGuidPtr, ref bufferSize) == 0)
                {
                    Guid subGroupGuid = (Guid)Marshal.PtrToStructure(
                        subGroupGuidPtr, typeof(Guid));

                    childs.Add(new SubGroupNode(powerSchemeGuid, subGroupGuid, settings));

                    groupIndex++;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(powerSchemeGuidPtr);
                Marshal.FreeHGlobal(subGroupGuidPtr);
            }
        }

    }
}

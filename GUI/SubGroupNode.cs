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
    class SubGroupNode : Node
    {
        private PersistentSettings settings;
        private SettingSubGroup subGroup;
        private Guid schemeGuid;

        public SubGroupNode(Guid schemeGuid, Guid subGroupGuid, PersistentSettings settings)
        {
            this.settings = settings;
            this.schemeGuid = schemeGuid;
            this.subGroup = new SettingSubGroup();
            this.subGroup.Guid = subGroupGuid.ToString();

            IntPtr friendlyName = Marshal.AllocHGlobal(1000);
            IntPtr powerSchemeGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
            IntPtr groupGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
            uint bufferSize = 1000;

            try
            {
                Marshal.StructureToPtr(schemeGuid, powerSchemeGuidPtr, true);
                Marshal.StructureToPtr(subGroupGuid, groupGuidPtr, true);

                //Pass the guid retrieved in PowerEnumerate as 
                //parameter to get the sub group name.
                NativeMethods.PowerReadFriendlyName(IntPtr.Zero,
                    powerSchemeGuidPtr, groupGuidPtr, IntPtr.Zero, friendlyName, ref bufferSize);

                string subGroupName = Marshal.PtrToStringUni(friendlyName);
                this.subGroup.Name = subGroupName;

                this.subGroup.Description = PowerManager.GetDescription(schemeGuid.ToString(), subGroup.Guid, null);

            }
            finally
            {
                Marshal.FreeHGlobal(powerSchemeGuidPtr);
                Marshal.FreeHGlobal(groupGuidPtr);
                Marshal.FreeHGlobal(friendlyName);
            }
        }


        public override string Text
        {
            get { return subGroup.Name; }
            set { subGroup.Name = value; }
        }

        public override String Description
        {
            get { return subGroup.Description; }
        }

        protected override void LoadChilds()
        {
            IntPtr settingGuidPtr = IntPtr.Zero;
            IntPtr subGroupGuidPtr = IntPtr.Zero;
            IntPtr powerSchemeGuidPtr = IntPtr.Zero;

            try
            {
                List<PowerSetting> powerSettings = new List<PowerSetting>();
                settingGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                subGroupGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                powerSchemeGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));

                uint bufferSize = 1000; // (uint)Marshal.SizeOf(typeof(Guid));

                Guid guid = new Guid(this.subGroup.Guid );
                Marshal.StructureToPtr(guid, subGroupGuidPtr, true);
                Marshal.StructureToPtr(this.schemeGuid, powerSchemeGuidPtr, true);

                uint settingIndex = 0;
                //Enumerate settings
                while (NativeMethods.PowerEnumerate(
                            IntPtr.Zero, powerSchemeGuidPtr, subGroupGuidPtr,
                            NativeMethods.PowerDataAccessor.AccessIndividualSetting,
                            settingIndex, settingGuidPtr, ref bufferSize) == 0)
                {

                    Guid settingGuid = (Guid)Marshal.PtrToStructure(settingGuidPtr, typeof(Guid));

                    childs.Add(new SettingNode(schemeGuid, new Guid(subGroup.Guid), settingGuid, settings));

                    settingIndex++;
                }
            }
            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }
            finally
            {
                Marshal.FreeHGlobal(powerSchemeGuidPtr);
                Marshal.FreeHGlobal(subGroupGuidPtr);
                Marshal.FreeHGlobal(settingGuidPtr);
            }
        }
    }
}

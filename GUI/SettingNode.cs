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
    class SettingNode : Node
    {
        private PersistentSettings settings;
        private PowerSetting powerSetting;
        private Guid powerSchemeGuid;
        private Guid subGroupGuid;

        private uint acValue;
        private uint dcValue;

        private bool isRange;
        private bool isHidden;

        private UInt32 minValue;
        private UInt32 maxValue;
        private UInt32 valueIncrement;
        private string units;

        public class IndexedSetting
        {
            public String name;
            public String description;
        };

        public List<IndexedSetting> indexedSettings;   
        
        public SettingNode(Guid powerSchemeGuid, Guid subGroupGuid, Guid powerSettingGuid, PersistentSettings settings)
            :base()
        {
            this.settings = settings;
            this.powerSchemeGuid = powerSchemeGuid;
            this.subGroupGuid = subGroupGuid;

            uint returnCode = 0;
            IntPtr friendlyName = IntPtr.Zero;
            IntPtr settingGuidPtr = IntPtr.Zero;
            IntPtr subGroupGuidPtr = IntPtr.Zero;
            IntPtr powerSchemeGuidPtr = IntPtr.Zero;
            try
            {
                friendlyName = Marshal.AllocHGlobal(1000);
                settingGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                subGroupGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                powerSchemeGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                uint bufferSize = 1000;

                Marshal.StructureToPtr(powerSchemeGuid, powerSchemeGuidPtr, true);
                Marshal.StructureToPtr(subGroupGuid, subGroupGuidPtr, true);
                Marshal.StructureToPtr(powerSettingGuid, settingGuidPtr, true);

                //Get the power setting name.
                NativeMethods.PowerReadFriendlyName(
                    IntPtr.Zero, powerSchemeGuidPtr, subGroupGuidPtr, settingGuidPtr,
                    friendlyName, ref bufferSize);

                string settingName = Marshal.PtrToStringUni(friendlyName);

                powerSetting = new PowerSetting();
                powerSetting.Name = settingName;
                powerSetting.Guid = powerSettingGuid.ToString();
                powerSetting.Description = PowerManager.GetDescription(powerSchemeGuid.ToString(), subGroupGuid.ToString(), powerSetting.Guid);

                returnCode = NativeMethods.PowerReadACValueIndex(
                        IntPtr.Zero, powerSchemeGuidPtr, subGroupGuidPtr, settingGuidPtr, ref acValue);

                returnCode = NativeMethods.PowerReadDCValueIndex(
                        IntPtr.Zero, powerSchemeGuidPtr, subGroupGuidPtr, settingGuidPtr, ref dcValue);

                isRange = NativeMethods.PowerIsSettingRangeDefined(subGroupGuidPtr, settingGuidPtr);

                // Bit mask !!!
                isHidden = 1 == (NativeMethods.PowerReadSettingAttributes(subGroupGuidPtr, settingGuidPtr) & 1);
                base.IsVisible = !isHidden;

                if ( isRange )
                {
                    ReadNonindexedValues(subGroupGuidPtr, settingGuidPtr);
                }
                else
                {
                    ReadIndexedValues(subGroupGuidPtr, settingGuidPtr);
                }
            }
            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }
            finally
            {
                Marshal.FreeHGlobal(friendlyName);
                Marshal.FreeHGlobal(powerSchemeGuidPtr);
                Marshal.FreeHGlobal(subGroupGuidPtr);
                Marshal.FreeHGlobal(settingGuidPtr);
            }
        }

        public bool isRangeSetting
        {
            get { return isRange; }
        }
        
        public override string Text
        {
            get { return powerSetting.Name; }
            set { powerSetting.Name = value; }
        }

        public override string Description
        {
            get { return powerSetting.Description; }
        }

        public string AcValue
        {
            get { return isRange ? acValue.ToString() : indexedSettings[ (int)acValue ].name; }
            set
            {
                if (isRange)
                {
                    if (Convert.ToUInt32(value) == acValue)
                        return;

                    acValue = Convert.ToUInt32( value );
                }
                else
                {
                    uint i = 0;
                    foreach (IndexedSetting s in indexedSettings)
                    {
                        if (s.name == value)
                        {
                            if (i == acValue)
                                return;

                            acValue = i;
                            break;
                        }
                        ++i;
                    }
                }

                if ( settings.GetValue("applyMenuItem", false))
                {
                    uint returnCode = 0;
                    IntPtr settingGuidPtr = IntPtr.Zero;
                    IntPtr subGroupGuidPtr = IntPtr.Zero;
                    IntPtr powerSchemeGuidPtr = IntPtr.Zero;

                    try
                    {
                        settingGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                        subGroupGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                        powerSchemeGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));

                        Marshal.StructureToPtr(powerSchemeGuid, powerSchemeGuidPtr, true);
                        Marshal.StructureToPtr(subGroupGuid, subGroupGuidPtr, true);
                        Marshal.StructureToPtr(new Guid(powerSetting.Guid), settingGuidPtr, true);

                        returnCode = NativeMethods.PowerWriteACValueIndex(
                                IntPtr.Zero, powerSchemeGuidPtr, subGroupGuidPtr, settingGuidPtr, acValue);
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
                } // if AutoApplyChanges
            } //  set
        }


        public string DcValue
        {
            get { return isRange ? dcValue.ToString() : indexedSettings[(int)dcValue].name; }
            set
            {
                if (isRange)
                {
                    if (Convert.ToUInt32(value) == dcValue)
                        return;

                    dcValue = Convert.ToUInt32(value);
                }
                else
                {
                    uint i = 0;
                    foreach (IndexedSetting s in indexedSettings)
                    {
                        if (s.name == value)
                        {
                            if (i == dcValue)
                                return;

                            dcValue = i;
                            break;
                        }
                        ++i;
                    }
                }

                if (settings.GetValue("applyMenuItem", false))
                {
                    uint returnCode = 0;
                    IntPtr settingGuidPtr = IntPtr.Zero;
                    IntPtr subGroupGuidPtr = IntPtr.Zero;
                    IntPtr powerSchemeGuidPtr = IntPtr.Zero;

                    try
                    {
                        settingGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                        subGroupGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                        powerSchemeGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));

                        Marshal.StructureToPtr(powerSchemeGuid, powerSchemeGuidPtr, true);
                        Marshal.StructureToPtr(subGroupGuid, subGroupGuidPtr, true);
                        Marshal.StructureToPtr(new Guid(powerSetting.Guid), settingGuidPtr, true);

                        returnCode = NativeMethods.PowerWriteDCValueIndex(
                                IntPtr.Zero, powerSchemeGuidPtr, subGroupGuidPtr, settingGuidPtr, dcValue);
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
                } // if AutoApplyChanges
            } //  set
        }

        public String Unit
        {
            get { return isRange ? units : ""; }
        }

        private void ReadNonindexedValues(IntPtr subGroupGuidPtr, IntPtr settingGuidPtr)
        {
            uint returnCode = 0;
            IntPtr buffer = IntPtr.Zero;
            uint bufferSize = 1000;

            try
            {
                buffer = Marshal.AllocHGlobal(1000);

                returnCode = NativeMethods.PowerReadValueMin(
                IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, ref minValue);

                returnCode = NativeMethods.PowerReadValueMax(
                IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, ref maxValue);

                returnCode = NativeMethods.PowerReadValueIncrement(
                    IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, ref valueIncrement);

                returnCode = NativeMethods.PowerReadValueUnitsSpecifier(
                    IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, buffer, ref bufferSize);

                units = ((/*NativeMethods.ERROR_SUCCESS*/ 0 == returnCode) ? Marshal.PtrToStringUni(buffer) : "N/A");
            }
            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }


        private void ReadIndexedValues(IntPtr subGroupGuidPtr, IntPtr settingGuidPtr)
        {
            indexedSettings = new List<IndexedSetting>();

            uint returnCode = 0;
            UInt32 type = 0;
            IntPtr buffer = IntPtr.Zero; 
            UInt32 possibleSettingIndex = 0;
            uint bufferSize = 1000;
            try
            {
                buffer = Marshal.AllocHGlobal(1000);
                while( 0 == NativeMethods.PowerReadPossibleValue(
                                            IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, ref type, 
                                            possibleSettingIndex, buffer, ref bufferSize) )
                {
                    IndexedSetting setting = new IndexedSetting();

                    bufferSize = 1000;
                    returnCode = NativeMethods.PowerReadPossibleFriendlyName(
                        IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, 
                        possibleSettingIndex, buffer, ref bufferSize);

                    setting.name = Marshal.PtrToStringUni(buffer);

                    bufferSize = 1000;
                    returnCode = NativeMethods.PowerReadPossibleDescription(
                        IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, 
                        possibleSettingIndex, buffer, ref bufferSize);

                    setting.description = Marshal.PtrToStringUni(buffer);

                    indexedSettings.Add( setting );

                    ++possibleSettingIndex;

                    bufferSize = 1000;
                }
            }
            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
    }
}

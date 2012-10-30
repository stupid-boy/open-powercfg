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
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Globalization;
using System.Text.RegularExpressions;
using System.ComponentModel;
using OpenPowerCfg.Resources;

namespace OpenPowerCfg.PowerManagement
{
    #region PowerManager class
    
    /// <summary>
    /// Provides wrapper over power management native APIs.
    /// </summary>     
    public abstract class PowerManager
    {
        #region Public Properties

        /// <summary>
        /// Determines whether the computer supports hibernation.
        /// </summary>
        /// <returns>If the computer supports hibernation (power state S4) 
        /// and the file Hiberfil.sys is present on the system, 
        /// the function returns TRUE.Otherwise, the function returns FALSE.
        /// </returns>
        /// <exception cref="PowerManagerException">
        /// Call to native API has raised an error.
        /// </exception>
        public static bool IsPowerHibernateAllowed
        {
            get
            {
                try
                {
                    return !(NativeMethods.IsPowerHibernateAllowed());
                }
                catch (Exception exception)
                {
                    throw new PowerManagerException(exception.Message, exception);
                }
            }
        }

        /// <summary>
        /// Determines whether the computer supports the sleep states.
        /// </summary>
        /// <returns>
        /// If the computer supports the sleep states (S1, S2, and S3), 
        /// the function returns TRUE. Otherwise, the function returns FALSE.
        /// </returns>
        /// <exception cref="PowerManagerException">
        /// Call to native API has raised an error.
        /// </exception>
        public static bool IsPowerSuspendAllowed
        {
            get
            {
                try
                {
                    return NativeMethods.IsPowerSuspendAllowed();
                }
                catch (Exception exception)
                {
                    throw new PowerManagerException(exception.Message, exception);
                }
            }
        }

        /// <summary>
        /// Determines whether the computer supports the soft off power state.
        /// </summary>
        /// <returns>If the computer supports soft off (power state S5), 
        /// the function returns TRUE. 
        /// Otherwise, the function returns FALSE.</returns>
        /// <exception cref="PowerManagerException">
        /// Call to native API has raised an error.
        /// </exception>
        public static bool IsPowerShutdownAllowed
        {
            get
            {
                try
                {
                    return NativeMethods.IsPowerShutdownAllowed();
                }
                catch (Exception exception)
                {
                    throw new PowerManagerException(exception.Message, exception);
                }
            }
        }

        #endregion

        #region Public Methods

    //    #region Power Scheme Related Methods
    //    /// <summary>
    //    /// Retrieves the title of the active power scheme.
    //    /// </summary>
    //    /// <returns>Title of the active power scheme 
    //    /// if the method succeeds else returns null.</returns>
    //    /// <exception cref="PowerManagerException">
    //    /// Call to native API has raised an error.
    //    /// </exception>
    //    public string GetActivePowerScheme()
    //    {
    //        try
    //        {
    //            //Get the active power scheme guid.
    //            string schemeGuid = PowerManager.GetActivePowerSchemeGuid();
        
    //            //Get all available power scheme names and guids.
    //            ArrayList allSchemes = PowerManager.GetAvailablePowerSchemesAndGuid();
                
    //            for (int index = 0; index < allSchemes.Count; index++)
    //            {
    //                //Compare the active power scheme guid with retrieved
    //                //power schemes' guids and get the name of active power 
    //                //scheme and return it.
    //                if (((ArrayList)allSchemes[index])[0].ToString() == schemeGuid)
    //                    return ((ArrayList)allSchemes[index])[1].ToString();
    //            }
    //        }

    //        catch (Exception exception)
    //        {
    //            throw new PowerManagerException(exception.Message, exception);
    //        }

    //        return String.Empty;
    //    }

    //    /// <summary>
    //    /// Sets the active power scheme.
    //    /// </summary>
    //    /// <param name="powerSchemeTitle">The title of the power 
    //    /// scheme to be activated.</param>
    //    /// <returns>True, if the method succeeds.</returns>
    //    /// <exception cref="ArgumentNullException">
    //    /// The required parameter is null or empty.
    //    /// </exception>
    //    /// <exception cref="ArgumentException">
    //    /// The specified parameter is not valid.
    //    /// </exception>
    //    /// <exception cref="PowerManagerException">
    //    /// Call to native API has raised an error.
    //    /// </exception>
    //    public bool SetActivePowerScheme(string powerSchemeTitle)
    //    {
    //        if (String.IsNullOrEmpty(powerSchemeTitle))
    //        {
    //            throw new ArgumentNullException
    //                (PowerManagementResource.ArgumentNullMessage);
    //        }

    //        bool setActivePowerSchemeStatus = false;
    //        bool powerSchemeExist = false;
            
    //        try
    //        {
    //            //Get all available power scheme names and guids.
    //            ArrayList allSchemes = PowerManager.GetAvailablePowerSchemesAndGuid();

    //            for (int index = 0; index < allSchemes.Count; index++)
    //            {
    //                //Get the guid of the schemename passed, pass it as parameter
    //                //to SetActivePowerSchemeGuid function.
    //                if (((ArrayList)allSchemes[index])[1].ToString() ==
    //                    powerSchemeTitle)
    //                {
    //                    powerSchemeExist = true;
                        
    //                    setActivePowerSchemeStatus
    //                        = PowerManager.SetActivePowerSchemeGuid(
    //                            ((ArrayList)allSchemes[index])[0].ToString());                      
                        
    //                    break;
    //                }
    //            }
    //        }

    //        catch (Exception exception)
    //        {
    //            throw new PowerManagerException(exception.Message, exception);
    //        }
            
    //        if (!powerSchemeExist)
    //        {
    //            throw new ArgumentException(PowerManagementResource.PowerSchemeNotFound);
    //        }

    //        return setActivePowerSchemeStatus;
    //    }


    //    /// <summary>
    //    /// Deletes the specified power scheme.
    //    /// </summary>
    //    /// <param name="powerSchemeTitle">The title of the power scheme to 
    //    /// be deleted.</param>
    //    /// <returns>True, if the method succeeds.</returns>
    //    /// <exception cref="ArgumentNullException">
    //    /// The required parameter is null or empty.
    //    /// </exception>
    //    /// <exception cref="ArgumentException">
    //    /// The specified parameter is not valid.
    //    /// </exception>
    //    /// <exception cref="PowerManagerException">
    //    /// Call to native API has raised an error.
    //    /// </exception>
    //    public bool DeletePowerScheme(string powerSchemeTitle)
    //    {
    //        if (String.IsNullOrEmpty(powerSchemeTitle))
    //        {
    //            throw new ArgumentNullException(
    //                PowerManagementResource.ArgumentNullMessage);
    //        }

    //        //Check whether passed powerSchemeTitle is either one of 
    //        //default power schemes or active power scheme.
    //        if (powerSchemeTitle.Equals(PowerManagementResource.PowerSchemeBalancedTitle)
    //            || powerSchemeTitle.Equals(PowerManagementResource.PowerSchemePowerSaverTitle)
    //            || powerSchemeTitle.Equals(PowerManagementResource.PowerSchemeHighPerformanceTitle)
    //            || powerSchemeTitle.Equals(this.GetActivePowerScheme()))
    //        {
    //            throw new PowerManagerException(
    //                PowerManagementResource.DeleteSchemeMessage);
    //        }

    //        bool deletePowerSchemeStatus = false;
    //        bool powerSchemeExist = false;
    //        IntPtr ptrToActiveScheme = IntPtr.Zero;
    //        try
    //        {
    //            //Get all available power scheme names and guids.
    //            ArrayList allSchemes = PowerManager.GetAvailablePowerSchemesAndGuid();

    //            Guid deleteSchemeGuid = Guid.Empty;
    //            for (int index = 0; index < allSchemes.Count; index++)
    //            {
    //                //Compare the active scheme names with passed scheme name
    //                //and get the guid of power scheme to be deleted.
    //                if (((ArrayList)allSchemes[index])[1].ToString()
    //                    == powerSchemeTitle)
    //                {
    //                    deleteSchemeGuid
    //                        = new Guid(((ArrayList)allSchemes[index])[0].ToString());
                        
    //                    powerSchemeExist = true;
    //                    break;
    //                }
    //            }

    //            if (deleteSchemeGuid != Guid.Empty)
    //            {
    //                ptrToActiveScheme
    //                    = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));

    //                Marshal.StructureToPtr(deleteSchemeGuid,ptrToActiveScheme,true);

    //                //Delete the power scheme, giving powerscheme guid ad argument.
    //                deletePowerSchemeStatus
    //                    = !(NativeMethods.PowerDeleteScheme(
    //                            IntPtr.Zero, ptrToActiveScheme));
    //            }
    //        }

    //        catch (Exception exception)
    //        {
    //            throw new PowerManagerException(exception.Message ,exception);
    //        }
    //        finally
    //        {
    //            Marshal.FreeHGlobal(ptrToActiveScheme);
    //        }
    //        //Throw exception if specified power scheme does not exist.
    //        if (!powerSchemeExist)
    //        {
    //            throw new ArgumentException(PowerManagementResource.PowerSchemeNotFound);
    //        }

    //        return deletePowerSchemeStatus;
    //    }

        #endregion

        #region Windows Security Related Methods

        /// <summary>
        /// Shuts down the system and then restarts the system.
        /// </summary>
        /// <param name="force">If True, then it forces processes to terminate.
        /// If false then sends message to processes and it forces processes 
        /// to terminate if they do not respond to the message within 
        /// the timeout interval.</param>
        /// <returns>True, if reboot has been initiated.</returns>
        /// <exception cref="PowerManagerException">
        /// Call to native API has raised an error.
        /// </exception>
        public static bool RebootComputer(bool force)
        {
            bool rebootComputerStatus = false;
            bool hasPrivilege = false;
            try
            {
                //Set privileges for this application for rebooting pc.
                if (PowerManager.SetPrivilege(NativeMethods.SEShutdownName))
                {
                    if (force)
                    {
                        //Force all applications to close and reboot the system .
                        rebootComputerStatus = NativeMethods.ExitWindowsEx(
                            (NativeMethods.ExitWindowReboot
                                + NativeMethods.ExitWindowForceIfHung
                                + NativeMethods.ExitWindowForce),
                            (NativeMethods.ShutdownReasonMajorApplication |
                                NativeMethods.ShutdownReasonMinorMaintenance));
                    }

                    else
                    {
                        //Sends message to processes and it forces processes to terminate
                        //if they do not respond to the message within the timeout interval.
                        rebootComputerStatus = NativeMethods.ExitWindowsEx(
                            NativeMethods.ExitWindowReboot,
                            (NativeMethods.ShutdownReasonMajorApplication |
                                NativeMethods.ShutdownReasonMinorMaintenance));
                    }

                    hasPrivilege = true;
                }
            }

            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }

            if (!hasPrivilege)
            {
                throw new PowerManagerException(PowerManagementResource.NoShutdownPrivilege);
            }
            else if (!rebootComputerStatus)
            {
                throw new PowerManagerException(PowerManagementResource.Win32ErrorCodeMessage
                    + Marshal.GetLastWin32Error());
            }

            return rebootComputerStatus;
        }

 
        /// <summary>
        /// Shuts down the system to a point at which it is safe to turn off 
        /// the power.
        /// </summary>
        /// <param name="force">If True, then it forces processes to terminate. 
        /// If false then sends message to processes and it forces processes 
        /// to terminate if they do not respond to the message within the 
        /// timeout interval.</param>
        /// <returns>True, if shutdown has been initiated.</returns>
        /// <exception cref="PowerManagerException">
        /// Call to native API has raised an error.
        /// </exception>
        public static bool ShutdownComputer(bool force)
        {
            bool shutdownComputerStatus = false;
            bool hasPrivilege = false;
            try
            {
                //Check whether the computer supports the soft off power state and 
                //Set privileges for this application for rebooting pc.
                if (IsPowerShutdownAllowed
                    && PowerManager.SetPrivilege(NativeMethods.SEShutdownName))
                {
                    if (force)
                    {
                        //Force all applications to close and shutdown the system.
                        shutdownComputerStatus =
                            NativeMethods.ExitWindowsEx(
                                (NativeMethods.ExitWindowShutdown
                                    + NativeMethods.ExitWindowForceIfHung
                                    + NativeMethods.ExitWindowForce),
                                (NativeMethods.ShutdownReasonMajorApplication
                                    | NativeMethods.ShutdownReasonMinorMaintenance));
                    }

                    else
                    {
                        //Sends message to processes and it forces processes to terminate
                        //if they do not respond to the message within the timeout interval.
                        shutdownComputerStatus = NativeMethods.ExitWindowsEx(
                            NativeMethods.ExitWindowShutdown,
                            (NativeMethods.ShutdownReasonMajorApplication
                                | NativeMethods.ShutdownReasonMinorMaintenance));
                    }

                    hasPrivilege = true;
                }
            }

            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }

            if (!hasPrivilege)
            {
                throw new PowerManagerException(PowerManagementResource.NoShutdownPrivilege);
            }
            else if (!shutdownComputerStatus)
            {
                throw new PowerManagerException(PowerManagementResource.Win32ErrorCodeMessage
                    + Marshal.GetLastWin32Error());
            }

            return shutdownComputerStatus;
        }


        /// <summary>
        /// Shuts down the system and turns off the power. 
        /// The system must support the power-off feature.
        /// </summary>
        /// <param name="force">If True, then it forces processes to terminate.
        /// If false then sends message to processes and it forces processes 
        /// to terminate if they do not respond to the message within 
        /// the timeout interval.</param>
        /// <returns>True, if the shutdown has been initiated</returns>
        /// <exception cref="PowerManagerException">
        /// Call to native API has raised an error.
        /// </exception>
        public static bool PowerOffComputer(bool force)
        {
            bool powerOffComputerStatus = false;
            bool hasPrivilege = false;
            try
            {
                //Check whether the computer supports the soft off 
                //power state and set privileges for this application for rebooting pc.
                if (IsPowerShutdownAllowed
                    && PowerManager.SetPrivilege(
                        NativeMethods.SEShutdownName))
                {
                    if (force)
                    {
                        //Force all applications to close and poweroff the system. 
                        powerOffComputerStatus =
                            NativeMethods.ExitWindowsEx(
                                (NativeMethods.ExitWindowPowerOff
                                    + NativeMethods.ExitWindowForceIfHung
                                    + NativeMethods.ExitWindowForce),
                                (NativeMethods.ShutdownReasonMajorApplication |
                                    NativeMethods.ShutdownReasonMinorMaintenance));
                    }

                    else
                    {
                        //Sends message to processes and it forces processes to terminate
                        // if they do not respond to the message within the timeout interval.
                        powerOffComputerStatus =
                            NativeMethods.ExitWindowsEx(
                                NativeMethods.ExitWindowPowerOff,
                                (NativeMethods.ShutdownReasonMajorApplication |
                                    NativeMethods.ShutdownReasonMinorMaintenance));
                    }

                    hasPrivilege = true;
                }
            }

            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }

            if (!hasPrivilege)
            {
                throw new PowerManagerException(PowerManagementResource.NoShutdownPrivilege);
            }
            else if (!powerOffComputerStatus)
            {
                throw new PowerManagerException(PowerManagementResource.Win32ErrorCodeMessage
                    + Marshal.GetLastWin32Error());
            }

            return powerOffComputerStatus;
        }

        /// <summary>
        /// Locks the workstation's display.
        /// This simulates the pressing of Windows+L key combination.
        /// </summary>
        /// <returns>True, if the Locking process has been initiated.</returns>
        /// <exception cref="PowerManagerException">
        /// Call to native API has raised an error.
        /// </exception>
        public static bool LockWorkStation()
        {
            bool lockWorkStationStatus = false;
            try
            {
                //Lock workstation
                if (NativeMethods.LockWorkstation())
                {
                    lockWorkStationStatus = true;
                }
            }

            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }

            if (!lockWorkStationStatus)
            {
                throw new PowerManagerException(PowerManagementResource.Win32ErrorCodeMessage
                    + Marshal.GetLastWin32Error());
            }

            return lockWorkStationStatus;
        }

        /// <summary>
        /// Hibernates the system.
        /// </summary>
        /// <param name="force">If this parameter is TRUE,the system suspends 
        /// operation immediately; 
        /// if it is FALSE, the system broadcasts an event to each application 
        /// to request permission to suspend operation.</param>
        /// <returns>True, if the function succeeds.</returns>
        /// <exception cref="PowerManagerException">
        /// Call to native API has raised an error.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// This exception is raised if the computer does 
        /// not support hibernation (power state S4) and 
        /// the file Hiberfil.sys is not present on the system.
        /// </exception>
        public static bool HibernateComputer(bool force)
        {
            bool hibernateComputerStatus = false;

            //Check whether the computer supports hibernation.
            if (IsPowerHibernateAllowed)
                throw new InvalidOperationException(PowerManagementResource.NoHibernationSupport);

            try
            {
                //Hibernates PC.
                hibernateComputerStatus
                    = NativeMethods.SetSuspendState(true, force, false);
            }

            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }

            if (!hibernateComputerStatus)
            {
                throw new PowerManagerException(PowerManagementResource.Win32ErrorCodeMessage
                    + Marshal.GetLastWin32Error());
            }

            return hibernateComputerStatus;
        }


        /// <summary>
        /// Puts the system into suspend (sleep) state.
        /// </summary>
        /// <param name="force">If this parameter is TRUE, the system suspends 
        /// operation immediately; if it is FALSE, the system broadcasts an 
        /// event to each application to request permission to suspend 
        /// operation.
        /// </param>
        /// <param name="disableWakeEvents">If this parameter is TRUE, 
        /// the system disables all wake events. If the parameter is FALSE, 
        /// any system wake events remain enabled.</param>
        /// <returns>True, if the function succeeds.</returns>
        /// <exception cref="PowerManagerException">
        /// Call to native API has raised an error.
        /// </exception>
        public static bool StandbyComputer(bool force, bool disableWakeEvents)
        {
            bool standbyComputerStatus = false;
            try
            {
                //Check whether the computer supports the sleep states.
                if (IsPowerSuspendAllowed)
                {
                    //Standby PC
                    standbyComputerStatus = NativeMethods.SetSuspendState(
                        false, force, disableWakeEvents);
                }
            }

            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }

            if (!standbyComputerStatus)
            {
                throw new PowerManagerException(PowerManagementResource.Win32ErrorCodeMessage
                    + Marshal.GetLastWin32Error());
            }

            return standbyComputerStatus;
        }

        /// <summary>
        /// Logs off the current user.
        /// </summary>
        /// <param name="force">If True, then it forces processes to terminate.
        /// If false then sends message to processes and it forces processes to 
        /// terminate if they do not respond to the message within the timeout 
        /// interval.</param>
        /// <returns>True, if log off has been initiated.</returns>
        /// <exception cref="PowerManagerException">
        /// Call to native API has raised an error.
        /// </exception>
        public static bool LogOffCurrentUser(bool force)
        {
            bool logOffCurrentUserStatus = false;
            try
            {
                if (force)
                {
                    //Forces processes to terminate and Logoff current user.
                    logOffCurrentUserStatus = NativeMethods.ExitWindowsEx(
                        NativeMethods.ExitWindowForce, 0);
                }
                else
                {
                    //Sends message to processes and  forces processes to terminate
                    //if they do not respond to the message within the timeout interval 
                    //and logoff current user.
                    logOffCurrentUserStatus = NativeMethods.ExitWindowsEx(
                        NativeMethods.ExitWindowLogOff, 0);
                }
            }

            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }

            if (!logOffCurrentUserStatus)
            {
                throw new PowerManagerException(PowerManagementResource.Win32ErrorCodeMessage
                    + Marshal.GetLastWin32Error());
            }

            return logOffCurrentUserStatus;
        }

        #endregion

        #region Private Methods

    //    /// <summary>
    //    /// Retrieves the active power scheme and returns a GUID that 
    //    /// identifies the scheme.
    //    /// </summary>
    //    /// <returns>If the method succeeds then returns GUID of the 
    //    /// active power scheme else it returns null.</returns>
    //    private static string GetActivePowerSchemeGuid()
    //    {
    //        IntPtr ptrToActivePowerScheme
    //            = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));

    //        //Get guid of active power scheme
    //        if (!NativeMethods.PowerGetActiveScheme(
    //            IntPtr.Zero, out ptrToActivePowerScheme))
    //        {
    //            Guid guidActiveScheme
    //                = (Guid)Marshal.PtrToStructure(ptrToActivePowerScheme, typeof(Guid));

    //            Marshal.FreeHGlobal(ptrToActivePowerScheme);
    //            return guidActiveScheme.ToString();
    //        }
    //        Marshal.FreeHGlobal(ptrToActivePowerScheme);
    //        return null;
    //    }

    //    /// <summary>
    //    /// Sets the active power scheme for the current user.
    //    /// </summary>
    //    /// <param name="guid">Guid of the power scheme to activate.</param>
    //    /// <returns>True, if the method succeeds.</returns>
    //    private static bool SetActivePowerSchemeGuid(string guid)
    //    {
    //        IntPtr ptrToActiveScheme = IntPtr.Zero;
    //        try
    //        {
    //            Guid activeSchemeGuid = new Guid(guid);

    //            ptrToActiveScheme
    //                = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));

    //            Marshal.StructureToPtr(activeSchemeGuid, ptrToActiveScheme, true);

    //            //Set active power scheme
    //            if (!NativeMethods.PowerSetActiveScheme(
    //                IntPtr.Zero, ptrToActiveScheme))
    //            {
    //                return true;
    //            }
    //        }

    //        catch (Exception exception)
    //        {
    //            throw new PowerManagerException(exception.Message);
    //        }
    //        finally
    //        {
    //            Marshal.FreeHGlobal(ptrToActiveScheme);
    //        }
    //        return false;
    //    }

        /// <summary>
        /// Sets the application privileges.
        /// </summary>
        /// <param name="privilege">Privilege to be given to the current application.</param>
        /// <returns>True, if the method succeeds.</returns>
        private static bool SetPrivilege(string privilege)
        {
            bool success;
            TokenPrivileges tokenPrivileges;
            IntPtr tokenHandle = IntPtr.Zero;

            //Get handle to current process.
            IntPtr currentProcessHandle =
                NativeMethods.GetCurrentProcess();

            // Get a token for this process. 
            success = NativeMethods.OpenProcessToken(
                currentProcessHandle,
                (NativeMethods.TokenAdjustPrivileges
                    | NativeMethods.TokenQuery), ref tokenHandle);

            //Set properties of token.
            tokenPrivileges.Count = 1;
            tokenPrivileges.Luid = 0;

            tokenPrivileges.Attribute = NativeMethods.SEPrivilegeEnabled;

            //Get the LUID for specified privilege name.
            success = NativeMethods.LookupPrivilegeValue(
                null, privilege, ref tokenPrivileges.Luid);

            //Enable specified privilege in the specified access token.
            success = NativeMethods.AdjustTokenPrivileges(
                tokenHandle, false, ref tokenPrivileges, 0, IntPtr.Zero, IntPtr.Zero);

            return success;
        }

    //    /// <summary>
    //    /// Retrieves the title and guid of the currently present 
    //    /// power schemes.
    //    /// </summary>
    //    /// <returns>ArrayList containing the title and Guid of the 
    //    /// power schemes.
    //    /// </returns>
    //    private static ArrayList GetAvailablePowerSchemesAndGuid()
    //    {
    //        ArrayList allSchemesAndGuid = new ArrayList();
    //        IntPtr ptrToPowerScheme = IntPtr.Zero;
    //        IntPtr friendlyName = IntPtr.Zero;
    //        try
    //        {
    //            uint buffSize = 100;
    //            uint schemeIndex = 0;

    //            ptrToPowerScheme =
    //                Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));

    //            //Get the guids of all available power schemes.
    //            while (NativeMethods.PowerEnumerate(
    //                        IntPtr.Zero, IntPtr.Zero, IntPtr.Zero,
    //                        NativeMethods.PowerDataAccessor.AccessScheme,
    //                        schemeIndex, ptrToPowerScheme, ref buffSize) == 0)
    //            {
    //                friendlyName = Marshal.AllocHGlobal(1000);

    //                Guid schemeGuid = (Guid)Marshal.PtrToStructure(
    //                    ptrToPowerScheme, typeof(Guid));

    //                buffSize = 1000;

    //                //Pass the guid retrieved in PowerEnumerate as parameter 
    //                //to get the power scheme name.
    //                NativeMethods.PowerReadFriendlyName(
    //                    IntPtr.Zero, ptrToPowerScheme, IntPtr.Zero, IntPtr.Zero,
    //                    friendlyName, ref buffSize);

    //                string schemeName = Marshal.PtrToStringUni(friendlyName);

    //                allSchemesAndGuid.Add(new ArrayList());

    //                //Add retrieved power scheme name in the arraylist
    //                ((ArrayList)allSchemesAndGuid[(int)schemeIndex]).Add(schemeGuid);

    //                //Add retrieved power scheme Guid in the arraylist
    //                ((ArrayList)allSchemesAndGuid[(int)schemeIndex]).Add(schemeName);

    //                schemeIndex++;
    //            }
    //        }

    //        catch (Exception exception)
    //        {
    //            throw new PowerManagerException(exception.Message, exception);
    //        }
    //        finally
    //        {
    //            Marshal.FreeHGlobal(ptrToPowerScheme);
    //            Marshal.FreeHGlobal(friendlyName);
    //        }
    //        return allSchemesAndGuid;
    //    }


        /// <summary>
        /// Validate a Guid.
        /// </summary>
        /// <param name="guid">Guid to be validated.</param>
        /// <returns>True, if guid is value else false.</returns>
        private static bool ValidateGuid(string guid)
        {
            if (String.IsNullOrEmpty(guid))
                return false;

            Regex isGuid = new Regex(PowerManagementResource.RegularExpressionGuid,
                                RegexOptions.Compiled);

            if (guid.StartsWith("{") != guid.EndsWith("}"))
                return false;

            if (isGuid.IsMatch(guid))
                return true;

            return false;
        }


        /// <summary>
        /// Gets the description of the specifed power scheme , setting sub group or setting.
        /// </summary>
        /// <param name="schemeGuid">Guid of the power scheme.</param>
        /// <param name="subGroupGuid">Guid of the setting sub group.</param>
        /// <param name="settingGuid">Guid of the power setting.</param>
        /// <returns>Description of the specified power scheme , setting sub group or setting.</returns>
        public static string GetDescription(string schemeGuid, string subGroupGuid, string settingGuid)
        {
            uint returnCode = 0;
            uint bufferSize = 1000;

            IntPtr description = IntPtr.Zero;
            IntPtr powerSchemeGuidPtr = IntPtr.Zero;
            IntPtr subGroupGuidPtr = IntPtr.Zero;
            IntPtr settingGuidPtr = IntPtr.Zero;

            try
            {
                if (String.IsNullOrEmpty(schemeGuid))
                    return null;

                Guid guid = new Guid();

                if (!String.IsNullOrEmpty(schemeGuid))
                {
                    powerSchemeGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                    guid = new Guid(schemeGuid);
                    Marshal.StructureToPtr(guid, powerSchemeGuidPtr, true);

                    if (!String.IsNullOrEmpty(subGroupGuid))
                    {
                        guid = new Guid(subGroupGuid);
                        subGroupGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                        Marshal.StructureToPtr(guid, subGroupGuidPtr, true);
                        if (!String.IsNullOrEmpty(settingGuid))
                        {
                            guid = new Guid(settingGuid);
                            settingGuidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                            Marshal.StructureToPtr(guid, settingGuidPtr, true);
                        }
                    }
                }

                description = Marshal.AllocHGlobal(1000);

                returnCode = NativeMethods.PowerReadDescription(
                    IntPtr.Zero, powerSchemeGuidPtr, subGroupGuidPtr, settingGuidPtr,
                        description, ref bufferSize);

                String strRet;
                if (returnCode == 0)
                {
                    if (bufferSize > 0)
                    {
                        strRet = Marshal.PtrToStringUni(description);

                        if (!String.IsNullOrEmpty(settingGuid))
                        {
                            bool indexed = !NativeMethods.PowerIsSettingRangeDefined(subGroupGuidPtr, settingGuidPtr);
                            strRet += System.Environment.NewLine;
                            strRet += indexed ? "Indexed; " : "Range value; ";

                            returnCode = NativeMethods.PowerReadSettingAttributes(subGroupGuidPtr, settingGuidPtr);
                            strRet += "Hidden = " + (returnCode & 1).ToString();

                            if (!indexed)
                            {
                                UInt32 value = 0;
                                returnCode = NativeMethods.PowerReadValueMin(
                                IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, ref value);

                                strRet += System.Environment.NewLine;
                                strRet += "Min value = " + value.ToString();

                                returnCode = NativeMethods.PowerReadValueMax(
                                IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, ref value);

                                strRet += System.Environment.NewLine;
                                strRet += "Max value = " + value.ToString();

                                returnCode = NativeMethods.PowerReadValueIncrement(
                                    IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, ref value);

                                strRet += System.Environment.NewLine;
                                strRet += "Increment value = " + value.ToString();

                                bufferSize = 1000;
                                returnCode = NativeMethods.PowerReadValueUnitsSpecifier(
                                    IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, description, ref bufferSize);

                                strRet += System.Environment.NewLine;
                                strRet += "Units = " + ((NativeMethods.ERROR_SUCCESS == returnCode) ? Marshal.PtrToStringUni(description) : "None (discrete units)");
                            }
                            else
                            {
                                UInt32 type = 0;
                                IntPtr buffer = Marshal.AllocHGlobal(1000);
                                UInt32 possibleSettingIndex = 0;
                                bufferSize = 1000;
                                returnCode = NativeMethods.PowerReadPossibleValue(
                                        IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, ref type,
                                        possibleSettingIndex, buffer, ref bufferSize);
                                do
                                {
                                    String str;
                                    if ((uint)NativeMethods.RegType.REG_SZ == type)
                                    {
                                        str = Marshal.PtrToStringUni(buffer);
                                    }
                                    else if ((uint)NativeMethods.RegType.REG_DWORD == type)
                                    {
                                        str = Marshal.ReadInt32(buffer).ToString();
                                    }
                                    else
                                    {
                                        str = "ERROR: Unsupported type: " + type;
                                    }

                                    strRet += System.Environment.NewLine;
                                    strRet += "(" + possibleSettingIndex + ") " + str;

                                    bufferSize = 1000;
                                    returnCode = NativeMethods.PowerReadPossibleFriendlyName(
                                        IntPtr.Zero, subGroupGuidPtr, settingGuidPtr,
                                        possibleSettingIndex, buffer, ref bufferSize);

                                    str = Marshal.PtrToStringUni(buffer);
                                    strRet += " = " + str;

                                    bufferSize = 1000;
                                    returnCode = NativeMethods.PowerReadPossibleDescription(
                                        IntPtr.Zero, subGroupGuidPtr, settingGuidPtr,
                                        possibleSettingIndex, buffer, ref bufferSize);

                                    bufferSize = 1000;
                                    if (NativeMethods.ERROR_SUCCESS == returnCode)
                                    {
                                        str = Marshal.PtrToStringUni(buffer);
                                        strRet += " ( " + str + " )";
                                    }

                                    ++possibleSettingIndex;

                                    bufferSize = 1000;
                                    returnCode = NativeMethods.PowerReadPossibleValue(
                                        IntPtr.Zero, subGroupGuidPtr, settingGuidPtr, ref type,
                                        possibleSettingIndex, buffer, ref bufferSize);

                                } while (NativeMethods.ERROR_SUCCESS == returnCode);

                                Marshal.FreeHGlobal(buffer);
                            }
                        }

                        return strRet;
                    }
                    else
                        return String.Empty;
                }

            }
            catch (Exception exception)
            {
                throw new PowerManagerException(exception.Message, exception);
            }
            finally
            {
                Marshal.FreeHGlobal(description);
                Marshal.FreeHGlobal(powerSchemeGuidPtr);
                Marshal.FreeHGlobal(subGroupGuidPtr);
                Marshal.FreeHGlobal(settingGuidPtr);
            }
            if (returnCode != 0)
            {
                throw new PowerManagerException(PowerManagementResource.Win32ErrorCodeMessage
                + returnCode.ToString(CultureInfo.InvariantCulture));
            }
            return String.Empty;
        }
        #endregion
    }
    #endregion

    /// <summary>
    /// Represents the power setting values of a power scheme.
    /// </summary>
    public class PowerScheme
    {
        #region Private Members
        /// <summary>
        /// Friendly name of the power scheme.
        /// </summary>
        private string name;
        /// <summary>
        /// Description of the power scheme.
        /// </summary>
        private string description;
        /// <summary>
        /// Guid of the power scheme.
        /// </summary>
        private string guid;
        /// <summary>
        /// List of sub groups of power settings within the power scheme.
        /// </summary>
        private List<SettingSubGroup> subGroups = new List<SettingSubGroup>();
        #endregion

        #region Public Properties
        /// <summary>
        /// Friendly name of the power scheme.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// Description of the power scheme.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        /// <summary>
        /// Guid of the power scheme.
        /// </summary>
        public string Guid
        {
            get { return this.guid; }
            set { this.guid = value; }
        }
        /// <summary>
        /// List of sub groups of power settings within the power scheme.
        /// </summary>
        public List<SettingSubGroup> SubGroups
        {
            get { return this.subGroups; }
        }
        #endregion
    }

    /// <summary>
    /// Represents a power setting sub group of a power scheme.
    /// </summary>
    public class SettingSubGroup
    {
        #region Private Members
        /// <summary>
        /// Friendly name of the sub group.
        /// </summary>
        private string name;
        /// <summary>
        /// Description of the setting sub group.
        /// </summary>
        private string description;
        /// <summary>
        /// Guid of the setting sub group.
        /// </summary>
        private string guid;
        /// <summary>
        /// List of power settings within the sub group.
        /// </summary>
        private List<PowerSetting> powerSettings = new List<PowerSetting>();
        #endregion

        #region Public Properties
        /// <summary>
        /// Friendly name of the sub group.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// Description of the setting sub group.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        /// <summary>
        /// Guid of the setting sub group.
        /// </summary>
        public string Guid
        {
            get { return this.guid; }
            set { this.guid = value; }
        }
        /// <summary>
        /// List of power settings within the sub group.
        /// </summary>
        public List<PowerSetting> PowerSettings
        {
            get { return this.powerSettings; }
        }
        #endregion
    }

    /// <summary>
    /// Represents a power setting of a power scheme.
    /// </summary>
    public class PowerSetting
    {
        #region Private Members
        /// <summary>
        /// Friendly name of the power setting.
        /// </summary>
        private string name;
        /// <summary>
        /// Description of the power setting.
        /// </summary>
        private string description;
        /// <summary>
        /// Guid of the power setting.
        /// </summary>
        private string guid;
        /// <summary>
        /// Value of the power setting.
        /// </summary>
        private string value;
        #endregion

        #region Public Properties
        /// <summary>
        /// Friendly name of the power setting.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// Description of the power setting.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        /// <summary>
        /// Guid of the power setting.
        /// </summary>
        public string Guid
        {
            get { return this.guid; }
            set { this.guid = value; }
        }
        /// <summary>
        /// Value of the power setting.
        /// </summary>
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        #endregion
    }
}

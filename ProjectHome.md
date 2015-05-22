Software is distributed WITHOUT ANY WARRANTY! You bear the risk of using it!

GUI front end for Windows Power Management settings.
Both Microsoft, hardware manufacturers and system integrators optimizes Windows Power Management system in manner, that allow them to satisfy requirements for wide range of audience. These settings are far from best performing for any concrete use of any dedicated system, but attempt was made to fit ANY possible scenario. Microsoft intentionally keeps these settings hidden for end users. In their documents we read:

`"Windows include a rich set of power policy settings that can influence and control the Windows kernel power manager algorithms for choosing target PPM states. These power policy settings are not intended to be changed by end users and are not exposed in the Control Panel Power Options application. Although most systems do not require adjustment to these settings, they are provided to let system manufacturers and system administrators optimize the Windows PPM behavior."`

and:

`"These policy settings should not be exposed to the user in custom power control software. OEMs are encouraged to refrain from creating such software and should limit the use of these policy settings only to default power plan customization. Changing these policy settings to an incorrect combination of values can negatively impact system energy efficiency, performance, and responsiveness."`

Well, i am not affiliated with Microsoft, any system manufacturer or OEM integrator.

PLEASE READ AND UNDERSTAND ANY RELATED DOCUMENTS, either provided or not by links at Wiki page before start any modification to your system! Microsoft and other sources can provide newer versions of any documents or can publish new ones. Also made a backup of your system. I expect that you have enough understandings, so please do not ask how to deal with UAC and other Windows features. If you do not know, most likely this tool is not for you!

Note that if you don't check 'Apply Changes' in Options menu, program is in emulation mode. Any changes you made are not persistent. This mode is safe for your system and can be used to investigate and understand system settings and behavior. For example, usually system manufacturer do not tell you when your laptop use internal video card and when external one. Looking with this tool you will understand what your manufacturer tries to withhold from you.
By checking this option, any change you made is applied to system.

Currently developed for Windows 7, but should works on Windows Vista, Windows 2008 ([R2](https://code.google.com/p/open-powercfg/source/detail?r=2)) and may be on Windows 8. Project is in active development phase, so you can expect any bugs/malfunctions. In future i may or may not add functionality or change behavior of  software. I can't test all aspects, so any feed-backs are welcome.

Software is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY. Proceed at your own risk.
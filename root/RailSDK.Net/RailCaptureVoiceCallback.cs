using System;
using System.Runtime.InteropServices;
using rail;

// Token: 0x02000005 RID: 5
// (Invoke) Token: 0x0600000E RID: 14
[UnmanagedFunctionPointer(2)]
public delegate void RailCaptureVoiceCallback(EnumRailVoiceCaptureFormat fmt, bool is_last_package, IntPtr encoded_buffer, uint encoded_length);

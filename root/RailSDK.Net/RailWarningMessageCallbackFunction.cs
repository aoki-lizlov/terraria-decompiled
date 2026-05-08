using System;
using System.Runtime.InteropServices;

// Token: 0x02000003 RID: 3
// (Invoke) Token: 0x06000006 RID: 6
[UnmanagedFunctionPointer(2)]
public delegate void RailWarningMessageCallbackFunction(int level, string msg);

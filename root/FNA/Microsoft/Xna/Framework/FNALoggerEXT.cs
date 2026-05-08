using System;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ObjCRuntime;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000017 RID: 23
	public static class FNALoggerEXT
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x00009E70 File Offset: 0x00008070
		internal static void Initialize()
		{
			if (FNALoggerEXT.LogInfo == null)
			{
				FNALoggerEXT.LogInfo = new Action<string>(Console.WriteLine);
			}
			if (FNALoggerEXT.LogWarn == null)
			{
				FNALoggerEXT.LogWarn = new Action<string>(Console.WriteLine);
			}
			if (FNALoggerEXT.LogError == null)
			{
				FNALoggerEXT.LogError = new Action<string>(Console.WriteLine);
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00009EC8 File Offset: 0x000080C8
		internal static void HookFNA3D()
		{
			try
			{
				FNA3D.FNA3D_HookLogFunctions(FNALoggerEXT.LogInfoFunc, FNALoggerEXT.LogWarnFunc, FNALoggerEXT.LogErrorFunc);
			}
			catch (DllNotFoundException)
			{
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00009F00 File Offset: 0x00008100
		[MonoPInvokeCallback(typeof(FNA3D.FNA3D_LogFunc))]
		private static void FNA3DLogInfo(IntPtr msg)
		{
			FNALoggerEXT.LogInfo(FNALoggerEXT.UTF8_ToManaged(msg));
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00009F12 File Offset: 0x00008112
		[MonoPInvokeCallback(typeof(FNA3D.FNA3D_LogFunc))]
		private static void FNA3DLogWarn(IntPtr msg)
		{
			FNALoggerEXT.LogWarn(FNALoggerEXT.UTF8_ToManaged(msg));
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00009F24 File Offset: 0x00008124
		[MonoPInvokeCallback(typeof(FNA3D.FNA3D_LogFunc))]
		private static void FNA3DLogError(IntPtr msg)
		{
			string text = FNALoggerEXT.UTF8_ToManaged(msg);
			FNALoggerEXT.LogError(text);
			throw new InvalidOperationException(text);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00009F4C File Offset: 0x0000814C
		private unsafe static string UTF8_ToManaged(IntPtr s)
		{
			byte* ptr = (byte*)(void*)s;
			while (*ptr != 0)
			{
				ptr++;
			}
			int num = (int)((long)((byte*)ptr - (byte*)(void*)s));
			if (num == 0)
			{
				return string.Empty;
			}
			checked
			{
				char* ptr2 = stackalloc char[unchecked((UIntPtr)num) * 2];
				int chars = Encoding.UTF8.GetChars((byte*)(void*)s, num, ptr2, num);
				return new string(ptr2, 0, chars);
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00009FA3 File Offset: 0x000081A3
		// Note: this type is marked as 'beforefieldinit'.
		static FNALoggerEXT()
		{
		}

		// Token: 0x040004C6 RID: 1222
		public static Action<string> LogInfo;

		// Token: 0x040004C7 RID: 1223
		public static Action<string> LogWarn;

		// Token: 0x040004C8 RID: 1224
		public static Action<string> LogError;

		// Token: 0x040004C9 RID: 1225
		private static FNA3D.FNA3D_LogFunc LogInfoFunc = new FNA3D.FNA3D_LogFunc(FNALoggerEXT.FNA3DLogInfo);

		// Token: 0x040004CA RID: 1226
		private static FNA3D.FNA3D_LogFunc LogWarnFunc = new FNA3D.FNA3D_LogFunc(FNALoggerEXT.FNA3DLogWarn);

		// Token: 0x040004CB RID: 1227
		private static FNA3D.FNA3D_LogFunc LogErrorFunc = new FNA3D.FNA3D_LogFunc(FNALoggerEXT.FNA3DLogError);
	}
}

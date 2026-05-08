using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000815 RID: 2069
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class MethodImplAttribute : Attribute
	{
		// Token: 0x06004652 RID: 18002 RVA: 0x000E6B9C File Offset: 0x000E4D9C
		internal MethodImplAttribute(MethodImplAttributes methodImplAttributes)
		{
			MethodImplOptions methodImplOptions = MethodImplOptions.Unmanaged | MethodImplOptions.ForwardRef | MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall | MethodImplOptions.Synchronized | MethodImplOptions.NoInlining | MethodImplOptions.AggressiveInlining | MethodImplOptions.NoOptimization;
			this._val = (MethodImplOptions)(methodImplAttributes & (MethodImplAttributes)methodImplOptions);
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x000E6BBE File Offset: 0x000E4DBE
		public MethodImplAttribute(MethodImplOptions methodImplOptions)
		{
			this._val = methodImplOptions;
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x000E6BBE File Offset: 0x000E4DBE
		public MethodImplAttribute(short value)
		{
			this._val = (MethodImplOptions)value;
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x00002050 File Offset: 0x00000250
		public MethodImplAttribute()
		{
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06004656 RID: 18006 RVA: 0x000E6BCD File Offset: 0x000E4DCD
		public MethodImplOptions Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002D0D RID: 11533
		internal MethodImplOptions _val;

		// Token: 0x04002D0E RID: 11534
		public MethodCodeType MethodCodeType;
	}
}

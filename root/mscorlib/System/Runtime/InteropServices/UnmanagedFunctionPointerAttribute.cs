using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006B2 RID: 1714
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class UnmanagedFunctionPointerAttribute : Attribute
	{
		// Token: 0x06003FF0 RID: 16368 RVA: 0x000E0942 File Offset: 0x000DEB42
		public UnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
		{
			this.m_callingConvention = callingConvention;
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x000E0951 File Offset: 0x000DEB51
		public CallingConvention CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x040029A8 RID: 10664
		private CallingConvention m_callingConvention;

		// Token: 0x040029A9 RID: 10665
		public CharSet CharSet;

		// Token: 0x040029AA RID: 10666
		public bool BestFitMapping;

		// Token: 0x040029AB RID: 10667
		public bool ThrowOnUnmappableChar;

		// Token: 0x040029AC RID: 10668
		public bool SetLastError;
	}
}

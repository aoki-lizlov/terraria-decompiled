using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006A6 RID: 1702
	public sealed class ErrorWrapper
	{
		// Token: 0x06003FC3 RID: 16323 RVA: 0x000E06A9 File Offset: 0x000DE8A9
		public ErrorWrapper(int errorCode)
		{
			this.m_ErrorCode = errorCode;
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x000E06B8 File Offset: 0x000DE8B8
		public ErrorWrapper(object errorCode)
		{
			if (!(errorCode is int))
			{
				throw new ArgumentException("Object must be of type Int32.", "errorCode");
			}
			this.m_ErrorCode = (int)errorCode;
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x000E06E4 File Offset: 0x000DE8E4
		public ErrorWrapper(Exception e)
		{
			this.m_ErrorCode = Marshal.GetHRForException(e);
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x000E06F8 File Offset: 0x000DE8F8
		public int ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
		}

		// Token: 0x040029A2 RID: 10658
		private int m_ErrorCode;
	}
}

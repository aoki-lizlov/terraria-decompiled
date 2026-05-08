using System;

namespace System.Resources
{
	// Token: 0x0200083B RID: 2107
	internal struct ResourceLocator
	{
		// Token: 0x0600474B RID: 18251 RVA: 0x000E9F82 File Offset: 0x000E8182
		internal ResourceLocator(int dataPos, object value)
		{
			this._dataPos = dataPos;
			this._value = value;
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600474C RID: 18252 RVA: 0x000E9F92 File Offset: 0x000E8192
		internal int DataPosition
		{
			get
			{
				return this._dataPos;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600474D RID: 18253 RVA: 0x000E9F9A File Offset: 0x000E819A
		// (set) Token: 0x0600474E RID: 18254 RVA: 0x000E9FA2 File Offset: 0x000E81A2
		internal object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x000E9FAB File Offset: 0x000E81AB
		internal static bool CanCache(ResourceTypeCode value)
		{
			return value <= ResourceTypeCode.TimeSpan;
		}

		// Token: 0x04002D6C RID: 11628
		internal object _value;

		// Token: 0x04002D6D RID: 11629
		internal int _dataPos;
	}
}

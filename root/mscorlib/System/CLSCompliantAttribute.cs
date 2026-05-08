using System;

namespace System
{
	// Token: 0x020000CB RID: 203
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public sealed class CLSCompliantAttribute : Attribute
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00019ECD File Offset: 0x000180CD
		public CLSCompliantAttribute(bool isCompliant)
		{
			this._compliant = isCompliant;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00019EDC File Offset: 0x000180DC
		public bool IsCompliant
		{
			get
			{
				return this._compliant;
			}
		}

		// Token: 0x04000EFA RID: 3834
		private bool _compliant;
	}
}

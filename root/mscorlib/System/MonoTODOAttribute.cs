using System;

namespace System
{
	// Token: 0x020001B0 RID: 432
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x0600148C RID: 5260 RVA: 0x00002050 File Offset: 0x00000250
		public MonoTODOAttribute()
		{
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x000521E1 File Offset: 0x000503E1
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x000521F0 File Offset: 0x000503F0
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x04001357 RID: 4951
		private string comment;
	}
}

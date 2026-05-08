using System;

namespace Terraria.Utilities
{
	// Token: 0x020000CD RID: 205
	public sealed class OldAttribute : Attribute
	{
		// Token: 0x0600180D RID: 6157 RVA: 0x004E11E7 File Offset: 0x004DF3E7
		public OldAttribute()
		{
			this.message = "";
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x004E11FA File Offset: 0x004DF3FA
		public OldAttribute(string message)
		{
			this.message = message;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x004E1209 File Offset: 0x004DF409
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x040012BF RID: 4799
		private string message;
	}
}

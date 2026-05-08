using System;
using ReLogic.Reflection;

namespace Terraria.ID
{
	// Token: 0x020001C2 RID: 450
	public class StatusID
	{
		// Token: 0x06001F5B RID: 8027 RVA: 0x0000357B File Offset: 0x0000177B
		public StatusID()
		{
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00519A15 File Offset: 0x00517C15
		// Note: this type is marked as 'beforefieldinit'.
		static StatusID()
		{
		}

		// Token: 0x04004274 RID: 17012
		public static readonly int Ok = 0;

		// Token: 0x04004275 RID: 17013
		public static readonly int LaterVersion = 1;

		// Token: 0x04004276 RID: 17014
		public static readonly int UnknownError = 2;

		// Token: 0x04004277 RID: 17015
		public static readonly int EmptyFile = 3;

		// Token: 0x04004278 RID: 17016
		public static readonly int DecryptionError = 4;

		// Token: 0x04004279 RID: 17017
		public static readonly int BadSectionPointer = 5;

		// Token: 0x0400427A RID: 17018
		public static readonly int BadFooter = 6;

		// Token: 0x0400427B RID: 17019
		public static readonly IdDictionary Search = IdDictionary.Create<StatusID, int>();
	}
}

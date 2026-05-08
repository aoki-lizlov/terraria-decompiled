using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200067C RID: 1660
	internal sealed class ObjectMapInfo
	{
		// Token: 0x06003E7D RID: 15997 RVA: 0x000D90A1 File Offset: 0x000D72A1
		internal ObjectMapInfo(int objectId, int numMembers, string[] memberNames, Type[] memberTypes)
		{
			this.objectId = objectId;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.memberTypes = memberTypes;
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x000D90C8 File Offset: 0x000D72C8
		internal bool isCompatible(int numMembers, string[] memberNames, Type[] memberTypes)
		{
			bool flag = true;
			if (this.numMembers == numMembers)
			{
				for (int i = 0; i < numMembers; i++)
				{
					if (!this.memberNames[i].Equals(memberNames[i]))
					{
						flag = false;
						break;
					}
					if (memberTypes != null && this.memberTypes[i] != memberTypes[i])
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0400286F RID: 10351
		internal int objectId;

		// Token: 0x04002870 RID: 10352
		private int numMembers;

		// Token: 0x04002871 RID: 10353
		private string[] memberNames;

		// Token: 0x04002872 RID: 10354
		private Type[] memberTypes;
	}
}

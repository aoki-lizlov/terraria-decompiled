using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002EB RID: 747
	internal class Box<T>
	{
		// Token: 0x06002194 RID: 8596 RVA: 0x000794AF File Offset: 0x000776AF
		internal Box(T value)
		{
			this.Value = value;
		}

		// Token: 0x04001AB3 RID: 6835
		internal T Value;
	}
}

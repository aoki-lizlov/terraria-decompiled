using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000902 RID: 2306
	[ComVisible(true)]
	[Serializable]
	public readonly struct Label : IEquatable<Label>
	{
		// Token: 0x0600504D RID: 20557 RVA: 0x000FD190 File Offset: 0x000FB390
		internal Label(int val)
		{
			this.label = val;
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x000FD19C File Offset: 0x000FB39C
		public override bool Equals(object obj)
		{
			bool flag = obj is Label;
			if (flag)
			{
				Label label = (Label)obj;
				flag = this.label == label.label;
			}
			return flag;
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x000FD1CD File Offset: 0x000FB3CD
		public bool Equals(Label obj)
		{
			return this.label == obj.label;
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x000FD1DD File Offset: 0x000FB3DD
		public static bool operator ==(Label a, Label b)
		{
			return a.Equals(b);
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x000FD1E7 File Offset: 0x000FB3E7
		public static bool operator !=(Label a, Label b)
		{
			return !(a == b);
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x000FD1F3 File Offset: 0x000FB3F3
		public override int GetHashCode()
		{
			return this.label.GetHashCode();
		}

		// Token: 0x04003131 RID: 12593
		internal readonly int label;
	}
}

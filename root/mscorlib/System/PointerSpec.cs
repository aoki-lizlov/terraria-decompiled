using System;
using System.Text;

namespace System
{
	// Token: 0x02000237 RID: 567
	internal class PointerSpec : ModifierSpec
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x000687B4 File Offset: 0x000669B4
		internal PointerSpec(int pointer_level)
		{
			this.pointer_level = pointer_level;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x000687C4 File Offset: 0x000669C4
		public Type Resolve(Type type)
		{
			for (int i = 0; i < this.pointer_level; i++)
			{
				type = type.MakePointerType();
			}
			return type;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000687EB File Offset: 0x000669EB
		public StringBuilder Append(StringBuilder sb)
		{
			return sb.Append('*', this.pointer_level);
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000687FB File Offset: 0x000669FB
		public override string ToString()
		{
			return this.Append(new StringBuilder()).ToString();
		}

		// Token: 0x0400187A RID: 6266
		private int pointer_level;
	}
}

using System;
using System.Text;

namespace System
{
	// Token: 0x02000236 RID: 566
	internal class ArraySpec : ModifierSpec
	{
		// Token: 0x06001BAB RID: 7083 RVA: 0x0006871A File Offset: 0x0006691A
		internal ArraySpec(int dimensions, bool bound)
		{
			this.dimensions = dimensions;
			this.bound = bound;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x00068730 File Offset: 0x00066930
		public Type Resolve(Type type)
		{
			if (this.bound)
			{
				return type.MakeArrayType(1);
			}
			if (this.dimensions == 1)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.dimensions);
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0006875E File Offset: 0x0006695E
		public StringBuilder Append(StringBuilder sb)
		{
			if (this.bound)
			{
				return sb.Append("[*]");
			}
			return sb.Append('[').Append(',', this.dimensions - 1).Append(']');
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x00068792 File Offset: 0x00066992
		public override string ToString()
		{
			return this.Append(new StringBuilder()).ToString();
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x000687A4 File Offset: 0x000669A4
		public int Rank
		{
			get
			{
				return this.dimensions;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x000687AC File Offset: 0x000669AC
		public bool IsBound
		{
			get
			{
				return this.bound;
			}
		}

		// Token: 0x04001878 RID: 6264
		private int dimensions;

		// Token: 0x04001879 RID: 6265
		private bool bound;
	}
}

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x020008E9 RID: 2281
	[StructLayout(LayoutKind.Sequential)]
	internal class ArrayType : SymbolType
	{
		// Token: 0x06004EB6 RID: 20150 RVA: 0x000F931E File Offset: 0x000F751E
		internal ArrayType(Type elementType, int rank)
			: base(elementType)
		{
			this.rank = rank;
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x000F932E File Offset: 0x000F752E
		internal int GetEffectiveRank()
		{
			return this.rank;
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x000F9338 File Offset: 0x000F7538
		internal override Type InternalResolve()
		{
			Type type = this.m_baseType.InternalResolve();
			if (this.rank == 0)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.rank);
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x000F936C File Offset: 0x000F756C
		internal override Type RuntimeResolve()
		{
			Type type = this.m_baseType.RuntimeResolve();
			if (this.rank == 0)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.rank);
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected override bool IsArrayImpl()
		{
			return true;
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x000F93A0 File Offset: 0x000F75A0
		public override int GetArrayRank()
		{
			if (this.rank != 0)
			{
				return this.rank;
			}
			return 1;
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x000F93B4 File Offset: 0x000F75B4
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(elementName);
			stringBuilder.Append("[");
			for (int i = 1; i < this.rank; i++)
			{
				stringBuilder.Append(",");
			}
			if (this.rank == 1)
			{
				stringBuilder.Append("*");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040030B3 RID: 12467
		private int rank;
	}
}

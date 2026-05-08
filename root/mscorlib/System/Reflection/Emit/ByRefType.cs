using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008EA RID: 2282
	[StructLayout(LayoutKind.Sequential)]
	internal class ByRefType : SymbolType
	{
		// Token: 0x06004EBD RID: 20157 RVA: 0x000F941D File Offset: 0x000F761D
		internal ByRefType(Type elementType)
			: base(elementType)
		{
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x000F9426 File Offset: 0x000F7626
		internal override Type InternalResolve()
		{
			return this.m_baseType.InternalResolve().MakeByRefType();
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected override bool IsByRefImpl()
		{
			return true;
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x000F9438 File Offset: 0x000F7638
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "&";
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x000F944A File Offset: 0x000F764A
		public override Type MakeArrayType()
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x06004EC2 RID: 20162 RVA: 0x000F944A File Offset: 0x000F764A
		public override Type MakeArrayType(int rank)
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x000F9456 File Offset: 0x000F7656
		public override Type MakeByRefType()
		{
			throw new ArgumentException("Cannot create a byref type of an already byref type");
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x000F9462 File Offset: 0x000F7662
		public override Type MakePointerType()
		{
			throw new ArgumentException("Cannot create a pointer type of a byref type");
		}
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008EB RID: 2283
	[StructLayout(LayoutKind.Sequential)]
	internal class PointerType : SymbolType
	{
		// Token: 0x06004EC5 RID: 20165 RVA: 0x000F941D File Offset: 0x000F761D
		internal PointerType(Type elementType)
			: base(elementType)
		{
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x000F946E File Offset: 0x000F766E
		internal override Type InternalResolve()
		{
			return this.m_baseType.InternalResolve().MakePointerType();
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected override bool IsPointerImpl()
		{
			return true;
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x000F9480 File Offset: 0x000F7680
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "*";
		}
	}
}

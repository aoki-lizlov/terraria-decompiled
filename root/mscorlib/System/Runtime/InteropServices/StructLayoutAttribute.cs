using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006D7 RID: 1751
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	[ComVisible(true)]
	public sealed class StructLayoutAttribute : Attribute
	{
		// Token: 0x06004038 RID: 16440 RVA: 0x000E0E18 File Offset: 0x000DF018
		[SecurityCritical]
		internal static StructLayoutAttribute GetCustomAttribute(RuntimeType type)
		{
			if (!StructLayoutAttribute.IsDefined(type))
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			LayoutKind layoutKind = LayoutKind.Auto;
			TypeAttributes typeAttributes = type.Attributes & TypeAttributes.LayoutMask;
			if (typeAttributes != TypeAttributes.NotPublic)
			{
				if (typeAttributes != TypeAttributes.SequentialLayout)
				{
					if (typeAttributes == TypeAttributes.ExplicitLayout)
					{
						layoutKind = LayoutKind.Explicit;
					}
				}
				else
				{
					layoutKind = LayoutKind.Sequential;
				}
			}
			else
			{
				layoutKind = LayoutKind.Auto;
			}
			CharSet charSet = CharSet.None;
			typeAttributes = type.Attributes & TypeAttributes.StringFormatMask;
			if (typeAttributes != TypeAttributes.NotPublic)
			{
				if (typeAttributes != TypeAttributes.UnicodeClass)
				{
					if (typeAttributes == TypeAttributes.AutoClass)
					{
						charSet = CharSet.Auto;
					}
				}
				else
				{
					charSet = CharSet.Unicode;
				}
			}
			else
			{
				charSet = CharSet.Ansi;
			}
			type.GetPacking(out num, out num2);
			if (num == 0)
			{
				num = 8;
			}
			return new StructLayoutAttribute(layoutKind, num, num2, charSet);
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x000E0EA3 File Offset: 0x000DF0A3
		internal static bool IsDefined(RuntimeType type)
		{
			return !type.IsInterface && !type.HasElementType && !type.IsGenericParameter;
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000E0EC0 File Offset: 0x000DF0C0
		internal StructLayoutAttribute(LayoutKind layoutKind, int pack, int size, CharSet charSet)
		{
			this._val = layoutKind;
			this.Pack = pack;
			this.Size = size;
			this.CharSet = charSet;
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x000E0EE5 File Offset: 0x000DF0E5
		public StructLayoutAttribute(LayoutKind layoutKind)
		{
			this._val = layoutKind;
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x000E0EE5 File Offset: 0x000DF0E5
		public StructLayoutAttribute(short layoutKind)
		{
			this._val = (LayoutKind)layoutKind;
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x0600403D RID: 16445 RVA: 0x000E0EF4 File Offset: 0x000DF0F4
		public LayoutKind Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A5C RID: 10844
		private const int DEFAULT_PACKING_SIZE = 8;

		// Token: 0x04002A5D RID: 10845
		internal LayoutKind _val;

		// Token: 0x04002A5E RID: 10846
		public int Pack;

		// Token: 0x04002A5F RID: 10847
		public int Size;

		// Token: 0x04002A60 RID: 10848
		public CharSet CharSet;
	}
}

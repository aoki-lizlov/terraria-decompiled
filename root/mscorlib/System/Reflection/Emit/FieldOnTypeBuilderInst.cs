using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008F5 RID: 2293
	[StructLayout(LayoutKind.Sequential)]
	internal class FieldOnTypeBuilderInst : FieldInfo
	{
		// Token: 0x06004F91 RID: 20369 RVA: 0x000FA864 File Offset: 0x000F8A64
		public FieldOnTypeBuilderInst(TypeBuilderInstantiation instantiation, FieldInfo fb)
		{
			this.instantiation = instantiation;
			this.fb = fb;
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06004F92 RID: 20370 RVA: 0x000FA87A File Offset: 0x000F8A7A
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06004F93 RID: 20371 RVA: 0x000FA882 File Offset: 0x000F8A82
		public override string Name
		{
			get
			{
				return this.fb.Name;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06004F94 RID: 20372 RVA: 0x000FA87A File Offset: 0x000F8A7A
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x00047E00 File Offset: 0x00046000
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x00047E00 File Offset: 0x00046000
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x00047E00 File Offset: 0x00046000
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x000FA88F File Offset: 0x000F8A8F
		public override string ToString()
		{
			return this.fb.FieldType.ToString() + " " + this.Name;
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06004F99 RID: 20377 RVA: 0x000FA8B1 File Offset: 0x000F8AB1
		public override FieldAttributes Attributes
		{
			get
			{
				return this.fb.Attributes;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06004F9A RID: 20378 RVA: 0x00047E00 File Offset: 0x00046000
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06004F9B RID: 20379 RVA: 0x00084CDD File Offset: 0x00082EDD
		public override int MetadataToken
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06004F9C RID: 20380 RVA: 0x00047E00 File Offset: 0x00046000
		public override Type FieldType
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x00047E00 File Offset: 0x00046000
		public override object GetValue(object obj)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x00047E00 File Offset: 0x00046000
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x000FA8BE File Offset: 0x000F8ABE
		internal FieldInfo RuntimeResolve()
		{
			return this.instantiation.RuntimeResolve().GetField(this.fb);
		}

		// Token: 0x040030E8 RID: 12520
		internal TypeBuilderInstantiation instantiation;

		// Token: 0x040030E9 RID: 12521
		internal FieldInfo fb;
	}
}

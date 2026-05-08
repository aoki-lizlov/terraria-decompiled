using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000911 RID: 2321
	[StructLayout(LayoutKind.Sequential)]
	internal class PropertyOnTypeBuilderInst : PropertyInfo
	{
		// Token: 0x06005194 RID: 20884 RVA: 0x00102046 File Offset: 0x00100246
		internal PropertyOnTypeBuilderInst(TypeBuilderInstantiation instantiation, PropertyInfo prop)
		{
			this.instantiation = instantiation;
			this.prop = prop;
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06005195 RID: 20885 RVA: 0x00047E00 File Offset: 0x00046000
		public override PropertyAttributes Attributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06005196 RID: 20886 RVA: 0x00047E00 File Offset: 0x00046000
		public override bool CanRead
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06005197 RID: 20887 RVA: 0x00047E00 File Offset: 0x00046000
		public override bool CanWrite
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06005198 RID: 20888 RVA: 0x0010205C File Offset: 0x0010025C
		public override Type PropertyType
		{
			get
			{
				return this.instantiation.InflateType(this.prop.PropertyType);
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06005199 RID: 20889 RVA: 0x00102074 File Offset: 0x00100274
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation.InflateType(this.prop.DeclaringType);
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x0600519A RID: 20890 RVA: 0x0010208C File Offset: 0x0010028C
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x0600519B RID: 20891 RVA: 0x00102094 File Offset: 0x00100294
		public override string Name
		{
			get
			{
				return this.prop.Name;
			}
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x001020A4 File Offset: 0x001002A4
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			MethodInfo getMethod = this.GetGetMethod(nonPublic);
			MethodInfo setMethod = this.GetSetMethod(nonPublic);
			int num = 0;
			if (getMethod != null)
			{
				num++;
			}
			if (setMethod != null)
			{
				num++;
			}
			MethodInfo[] array = new MethodInfo[num];
			num = 0;
			if (getMethod != null)
			{
				array[num++] = getMethod;
			}
			if (setMethod != null)
			{
				array[num] = setMethod;
			}
			return array;
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x00102108 File Offset: 0x00100308
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			MethodInfo methodInfo = this.prop.GetGetMethod(nonPublic);
			if (methodInfo != null && this.prop.DeclaringType == this.instantiation.generic_type)
			{
				methodInfo = TypeBuilder.GetMethod(this.instantiation, methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x00102158 File Offset: 0x00100358
		public override ParameterInfo[] GetIndexParameters()
		{
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod != null)
			{
				return getMethod.GetParameters();
			}
			return EmptyArray<ParameterInfo>.Value;
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x00102184 File Offset: 0x00100384
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			MethodInfo methodInfo = this.prop.GetSetMethod(nonPublic);
			if (methodInfo != null && this.prop.DeclaringType == this.instantiation.generic_type)
			{
				methodInfo = TypeBuilder.GetMethod(this.instantiation, methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x001021D2 File Offset: 0x001003D2
		public override string ToString()
		{
			return string.Format("{0} {1}", this.PropertyType, this.Name);
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x00047E00 File Offset: 0x00046000
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x00047E00 File Offset: 0x00046000
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x00047E00 File Offset: 0x00046000
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x00047E00 File Offset: 0x00046000
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x00047E00 File Offset: 0x00046000
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04003286 RID: 12934
		private TypeBuilderInstantiation instantiation;

		// Token: 0x04003287 RID: 12935
		private PropertyInfo prop;
	}
}

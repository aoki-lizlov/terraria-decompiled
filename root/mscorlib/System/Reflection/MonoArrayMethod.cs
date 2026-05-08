using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020008B7 RID: 2231
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoArrayMethod : MethodInfo
	{
		// Token: 0x06004B39 RID: 19257 RVA: 0x000F12D7 File Offset: 0x000EF4D7
		internal MonoArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.name = methodName;
			this.parent = arrayClass;
			this.ret = returnType;
			this.parameters = (Type[])parameterTypes.Clone();
			this.call_conv = callingConvention;
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x000025CE File Offset: 0x000007CE
		[MonoTODO("Always returns this")]
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06004B3B RID: 19259 RVA: 0x000F130E File Offset: 0x000EF50E
		public override Type ReturnType
		{
			get
			{
				return this.ret;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06004B3C RID: 19260 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		[MonoTODO("Not implemented.  Always returns null")]
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("Not implemented.  Always returns zero")]
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MethodImplAttributes.IL;
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x000F1316 File Offset: 0x000EF516
		[MonoTODO("Not implemented.  Always returns an empty array")]
		public override ParameterInfo[] GetParameters()
		{
			return this.GetParametersInternal();
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x000F131E File Offset: 0x000EF51E
		internal override ParameterInfo[] GetParametersInternal()
		{
			return EmptyArray<ParameterInfo>.Value;
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("Not implemented.  Always returns 0")]
		internal override int GetParametersCount()
		{
			return 0;
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Not implemented")]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06004B42 RID: 19266 RVA: 0x000F1325 File Offset: 0x000EF525
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.mhandle;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("Not implemented.  Always returns zero")]
		public override MethodAttributes Attributes
		{
			get
			{
				return MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06004B44 RID: 19268 RVA: 0x000F132D File Offset: 0x000EF52D
		public override Type ReflectedType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x000F132D File Offset: 0x000EF52D
		public override Type DeclaringType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06004B46 RID: 19270 RVA: 0x000F1335 File Offset: 0x000EF535
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x000534DE File Offset: 0x000516DE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x000F133D File Offset: 0x000EF53D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x000F1346 File Offset: 0x000EF546
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x000F1350 File Offset: 0x000EF550
		public override string ToString()
		{
			string text = string.Empty;
			ParameterInfo[] array = this.GetParameters();
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					text += ", ";
				}
				text += array[i].ParameterType.Name;
			}
			if (this.ReturnType != null)
			{
				return string.Concat(new string[]
				{
					this.ReturnType.Name,
					" ",
					this.Name,
					"(",
					text,
					")"
				});
			}
			return string.Concat(new string[] { "void ", this.Name, "(", text, ")" });
		}

		// Token: 0x04002F85 RID: 12165
		internal RuntimeMethodHandle mhandle;

		// Token: 0x04002F86 RID: 12166
		internal Type parent;

		// Token: 0x04002F87 RID: 12167
		internal Type ret;

		// Token: 0x04002F88 RID: 12168
		internal Type[] parameters;

		// Token: 0x04002F89 RID: 12169
		internal string name;

		// Token: 0x04002F8A RID: 12170
		internal int table_idx;

		// Token: 0x04002F8B RID: 12171
		internal CallingConventions call_conv;
	}
}

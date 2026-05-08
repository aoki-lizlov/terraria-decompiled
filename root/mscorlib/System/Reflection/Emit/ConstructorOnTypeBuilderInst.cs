using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008E6 RID: 2278
	[StructLayout(LayoutKind.Sequential)]
	internal class ConstructorOnTypeBuilderInst : ConstructorInfo
	{
		// Token: 0x06004E84 RID: 20100 RVA: 0x000F7FEA File Offset: 0x000F61EA
		public ConstructorOnTypeBuilderInst(TypeBuilderInstantiation instantiation, ConstructorInfo cb)
		{
			this.instantiation = instantiation;
			this.cb = cb;
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06004E85 RID: 20101 RVA: 0x000F8000 File Offset: 0x000F6200
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06004E86 RID: 20102 RVA: 0x000F8008 File Offset: 0x000F6208
		public override string Name
		{
			get
			{
				return this.cb.Name;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06004E87 RID: 20103 RVA: 0x000F8000 File Offset: 0x000F6200
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004E88 RID: 20104 RVA: 0x000F8015 File Offset: 0x000F6215
		public override Module Module
		{
			get
			{
				return this.cb.Module;
			}
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x000F8022 File Offset: 0x000F6222
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.cb.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x000F8031 File Offset: 0x000F6231
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.cb.GetCustomAttributes(inherit);
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x000F803F File Offset: 0x000F623F
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.cb.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x000F804E File Offset: 0x000F624E
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.cb.GetMethodImplementationFlags();
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x000F805B File Offset: 0x000F625B
		public override ParameterInfo[] GetParameters()
		{
			if (!this.instantiation.IsCreated)
			{
				throw new NotSupportedException();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x000F8078 File Offset: 0x000F6278
		internal override ParameterInfo[] GetParametersInternal()
		{
			ParameterInfo[] array;
			if (this.cb is ConstructorBuilder)
			{
				ConstructorBuilder constructorBuilder = (ConstructorBuilder)this.cb;
				array = new ParameterInfo[constructorBuilder.parameters.Length];
				for (int i = 0; i < constructorBuilder.parameters.Length; i++)
				{
					Type type = this.instantiation.InflateType(constructorBuilder.parameters[i]);
					ParameterInfo[] array2 = array;
					int num = i;
					ParameterBuilder[] pinfo = constructorBuilder.pinfo;
					array2[num] = RuntimeParameterInfo.New((pinfo != null) ? pinfo[i] : null, type, this, i + 1);
				}
			}
			else
			{
				ParameterInfo[] parameters = this.cb.GetParameters();
				array = new ParameterInfo[parameters.Length];
				for (int j = 0; j < parameters.Length; j++)
				{
					Type type2 = this.instantiation.InflateType(parameters[j].ParameterType);
					array[j] = RuntimeParameterInfo.New(parameters[j], type2, this, j + 1);
				}
			}
			return array;
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x000F8148 File Offset: 0x000F6348
		internal override Type[] GetParameterTypes()
		{
			if (this.cb is ConstructorBuilder)
			{
				return (this.cb as ConstructorBuilder).parameters;
			}
			ParameterInfo[] parameters = this.cb.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return array;
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x000F81A2 File Offset: 0x000F63A2
		internal ConstructorInfo RuntimeResolve()
		{
			return this.instantiation.InternalResolve().GetConstructor(this.cb);
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06004E91 RID: 20113 RVA: 0x000F81BA File Offset: 0x000F63BA
		public override int MetadataToken
		{
			get
			{
				return base.MetadataToken;
			}
		}

		// Token: 0x06004E92 RID: 20114 RVA: 0x000F81C2 File Offset: 0x000F63C2
		internal override int GetParametersCount()
		{
			return this.cb.GetParametersCount();
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x000F81CF File Offset: 0x000F63CF
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.cb.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06004E94 RID: 20116 RVA: 0x000F81E3 File Offset: 0x000F63E3
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.cb.MethodHandle;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06004E95 RID: 20117 RVA: 0x000F81F0 File Offset: 0x000F63F0
		public override MethodAttributes Attributes
		{
			get
			{
				return this.cb.Attributes;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06004E96 RID: 20118 RVA: 0x000F81FD File Offset: 0x000F63FD
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.cb.CallingConvention;
			}
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x000F820A File Offset: 0x000F640A
		public override Type[] GetGenericArguments()
		{
			return this.cb.GetGenericArguments();
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06004E98 RID: 20120 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004E99 RID: 20121 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004E9A RID: 20122 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x00084CDD File Offset: 0x00082EDD
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x040030A6 RID: 12454
		internal TypeBuilderInstantiation instantiation;

		// Token: 0x040030A7 RID: 12455
		internal ConstructorInfo cb;
	}
}

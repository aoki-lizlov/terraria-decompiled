using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000905 RID: 2309
	[StructLayout(LayoutKind.Sequential)]
	internal class MethodOnTypeBuilderInst : MethodInfo
	{
		// Token: 0x060050A8 RID: 20648 RVA: 0x000FDF4D File Offset: 0x000FC14D
		public MethodOnTypeBuilderInst(TypeBuilderInstantiation instantiation, MethodInfo base_method)
		{
			this.instantiation = instantiation;
			this.base_method = base_method;
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x000FDF64 File Offset: 0x000FC164
		internal MethodOnTypeBuilderInst(MethodOnTypeBuilderInst gmd, Type[] typeArguments)
		{
			this.instantiation = gmd.instantiation;
			this.base_method = gmd.base_method;
			this.method_arguments = new Type[typeArguments.Length];
			typeArguments.CopyTo(this.method_arguments, 0);
			this.generic_method_definition = gmd;
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x000FDFB4 File Offset: 0x000FC1B4
		internal MethodOnTypeBuilderInst(MethodInfo method, Type[] typeArguments)
		{
			this.instantiation = method.DeclaringType;
			this.base_method = MethodOnTypeBuilderInst.ExtractBaseMethod(method);
			this.method_arguments = new Type[typeArguments.Length];
			typeArguments.CopyTo(this.method_arguments, 0);
			if (this.base_method != method)
			{
				this.generic_method_definition = method;
			}
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x000FE010 File Offset: 0x000FC210
		private static MethodInfo ExtractBaseMethod(MethodInfo info)
		{
			if (info is MethodBuilder)
			{
				return info;
			}
			if (info is MethodOnTypeBuilderInst)
			{
				return ((MethodOnTypeBuilderInst)info).base_method;
			}
			if (info.IsGenericMethod)
			{
				info = info.GetGenericMethodDefinition();
			}
			Type declaringType = info.DeclaringType;
			if (!declaringType.IsGenericType || declaringType.IsGenericTypeDefinition)
			{
				return info;
			}
			return (MethodInfo)declaringType.Module.ResolveMethod(info.MetadataToken);
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x000FE07A File Offset: 0x000FC27A
		internal Type[] GetTypeArgs()
		{
			if (!this.instantiation.IsGenericType || this.instantiation.IsGenericParameter)
			{
				return null;
			}
			return this.instantiation.GetGenericArguments();
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x000FE0A4 File Offset: 0x000FC2A4
		internal MethodInfo RuntimeResolve()
		{
			MethodInfo methodInfo = this.instantiation.InternalResolve().GetMethod(this.base_method);
			if (this.method_arguments != null)
			{
				Type[] array = new Type[this.method_arguments.Length];
				for (int i = 0; i < this.method_arguments.Length; i++)
				{
					array[i] = this.method_arguments[i].InternalResolve();
				}
				methodInfo = methodInfo.MakeGenericMethod(array);
			}
			return methodInfo;
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x060050AE RID: 20654 RVA: 0x000FE10A File Offset: 0x000FC30A
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x060050AF RID: 20655 RVA: 0x000FE112 File Offset: 0x000FC312
		public override string Name
		{
			get
			{
				return this.base_method.Name;
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x060050B0 RID: 20656 RVA: 0x000FE10A File Offset: 0x000FC30A
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060050B1 RID: 20657 RVA: 0x000FE11F File Offset: 0x000FC31F
		public override Type ReturnType
		{
			get
			{
				return this.base_method.ReturnType;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060050B2 RID: 20658 RVA: 0x000FE12C File Offset: 0x000FC32C
		public override Module Module
		{
			get
			{
				return this.base_method.Module;
			}
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x00047E00 File Offset: 0x00046000
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x00047E00 File Offset: 0x00046000
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x00047E00 File Offset: 0x00046000
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x000FE13C File Offset: 0x000FC33C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.ReturnType.ToString());
			stringBuilder.Append(" ");
			stringBuilder.Append(this.base_method.Name);
			stringBuilder.Append("(");
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x000FE194 File Offset: 0x000FC394
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.base_method.GetMethodImplementationFlags();
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x000F1316 File Offset: 0x000EF516
		public override ParameterInfo[] GetParameters()
		{
			return this.GetParametersInternal();
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x00047E00 File Offset: 0x00046000
		internal override ParameterInfo[] GetParametersInternal()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x060050BA RID: 20666 RVA: 0x000F81BA File Offset: 0x000F63BA
		public override int MetadataToken
		{
			get
			{
				return base.MetadataToken;
			}
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x000FE1A1 File Offset: 0x000FC3A1
		internal override int GetParametersCount()
		{
			return this.base_method.GetParametersCount();
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x00047E00 File Offset: 0x00046000
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060050BD RID: 20669 RVA: 0x00047E00 File Offset: 0x00046000
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060050BE RID: 20670 RVA: 0x000FE1AE File Offset: 0x000FC3AE
		public override MethodAttributes Attributes
		{
			get
			{
				return this.base_method.Attributes;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060050BF RID: 20671 RVA: 0x000FE1BB File Offset: 0x000FC3BB
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.base_method.CallingConvention;
			}
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x000FE1C8 File Offset: 0x000FC3C8
		public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
		{
			if (!this.base_method.IsGenericMethodDefinition || this.method_arguments != null)
			{
				throw new InvalidOperationException("Method is not a generic method definition");
			}
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			if (this.base_method.GetGenericArguments().Length != methodInstantiation.Length)
			{
				throw new ArgumentException("Incorrect length", "methodInstantiation");
			}
			for (int i = 0; i < methodInstantiation.Length; i++)
			{
				if (methodInstantiation[i] == null)
				{
					throw new ArgumentNullException("methodInstantiation");
				}
			}
			return new MethodOnTypeBuilderInst(this, methodInstantiation);
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x000FE254 File Offset: 0x000FC454
		public override Type[] GetGenericArguments()
		{
			if (!this.base_method.IsGenericMethodDefinition)
			{
				return null;
			}
			Type[] array = this.method_arguments ?? this.base_method.GetGenericArguments();
			Type[] array2 = new Type[array.Length];
			array.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x000FE296 File Offset: 0x000FC496
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.generic_method_definition ?? this.base_method;
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060050C3 RID: 20675 RVA: 0x000FE2A8 File Offset: 0x000FC4A8
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.base_method.ContainsGenericParameters)
				{
					return true;
				}
				if (!this.base_method.IsGenericMethodDefinition)
				{
					throw new NotSupportedException();
				}
				if (this.method_arguments == null)
				{
					return true;
				}
				Type[] array = this.method_arguments;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060050C4 RID: 20676 RVA: 0x000FE303 File Offset: 0x000FC503
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.base_method.IsGenericMethodDefinition && this.method_arguments == null;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060050C5 RID: 20677 RVA: 0x000FE31D File Offset: 0x000FC51D
		public override bool IsGenericMethod
		{
			get
			{
				return this.base_method.IsGenericMethodDefinition;
			}
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x00047E00 File Offset: 0x00046000
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060050C7 RID: 20679 RVA: 0x00047E00 File Offset: 0x00046000
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060050C8 RID: 20680 RVA: 0x00047E00 File Offset: 0x00046000
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04003151 RID: 12625
		private Type instantiation;

		// Token: 0x04003152 RID: 12626
		private MethodInfo base_method;

		// Token: 0x04003153 RID: 12627
		private Type[] method_arguments;

		// Token: 0x04003154 RID: 12628
		private MethodInfo generic_method_definition;
	}
}

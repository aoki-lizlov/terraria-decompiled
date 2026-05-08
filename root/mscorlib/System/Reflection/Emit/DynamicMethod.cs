using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008ED RID: 2285
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DynamicMethod : MethodInfo
	{
		// Token: 0x06004EDA RID: 20186 RVA: 0x000F95BF File Offset: 0x000F77BF
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m)
			: this(name, returnType, parameterTypes, m, false)
		{
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x000F95CD File Offset: 0x000F77CD
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner)
			: this(name, returnType, parameterTypes, owner, false)
		{
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x000F95DB File Offset: 0x000F77DB
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
			: this(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, m, skipVisibility)
		{
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x000F95ED File Offset: 0x000F77ED
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
			: this(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, skipVisibility)
		{
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x000F9600 File Offset: 0x000F7800
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
			: this(name, attributes, callingConvention, returnType, parameterTypes, owner, owner.Module, skipVisibility, false)
		{
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x000F9628 File Offset: 0x000F7828
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
			: this(name, attributes, callingConvention, returnType, parameterTypes, null, m, skipVisibility, false)
		{
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x000F9648 File Offset: 0x000F7848
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes)
			: this(name, returnType, parameterTypes, false)
		{
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x000F9654 File Offset: 0x000F7854
		[MonoTODO("Visibility is not restricted")]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, bool restrictedSkipVisibility)
			: this(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, null, restrictedSkipVisibility, true)
		{
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x000F9674 File Offset: 0x000F7874
		private DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, Module m, bool skipVisibility, bool anonHosted)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			if (m == null && !anonHosted)
			{
				throw new ArgumentNullException("m");
			}
			if (returnType.IsByRef)
			{
				throw new ArgumentException("Return type can't be a byref type", "returnType");
			}
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Parameter " + i.ToString() + " is null", "parameterTypes");
					}
				}
			}
			if (owner != null && (owner.IsArray || owner.IsInterface))
			{
				throw new ArgumentException("Owner can't be an array or an interface.");
			}
			if (m == null)
			{
				m = DynamicMethod.AnonHostModuleHolder.AnonHostModule;
			}
			this.name = name;
			this.attributes = attributes | MethodAttributes.Static;
			this.callingConvention = callingConvention;
			this.returnType = returnType;
			this.parameters = parameterTypes;
			this.owner = owner;
			this.module = m;
			this.skipVisibility = skipVisibility;
		}

		// Token: 0x06004EE3 RID: 20195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void create_dynamic_method(DynamicMethod m);

		// Token: 0x06004EE4 RID: 20196 RVA: 0x000F979C File Offset: 0x000F799C
		private void CreateDynMethod()
		{
			lock (this)
			{
				if (this.mhandle.Value == IntPtr.Zero)
				{
					if (this.ilgen == null || this.ilgen.ILOffset == 0)
					{
						throw new InvalidOperationException("Method '" + this.name + "' does not have a method body.");
					}
					this.ilgen.label_fixup(this);
					try
					{
						this.creating = true;
						if (this.refs != null)
						{
							for (int i = 0; i < this.refs.Length; i++)
							{
								if (this.refs[i] is DynamicMethod)
								{
									DynamicMethod dynamicMethod = (DynamicMethod)this.refs[i];
									if (!dynamicMethod.creating)
									{
										dynamicMethod.CreateDynMethod();
									}
								}
							}
						}
					}
					finally
					{
						this.creating = false;
					}
					DynamicMethod.create_dynamic_method(this);
					this.ilgen = null;
				}
			}
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x000F9898 File Offset: 0x000F7A98
		[ComVisible(true)]
		public sealed override Delegate CreateDelegate(Type delegateType)
		{
			if (delegateType == null)
			{
				throw new ArgumentNullException("delegateType");
			}
			if (this.deleg != null)
			{
				return this.deleg;
			}
			this.CreateDynMethod();
			this.deleg = Delegate.CreateDelegate(delegateType, null, this);
			return this.deleg;
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x000F98D7 File Offset: 0x000F7AD7
		[ComVisible(true)]
		public sealed override Delegate CreateDelegate(Type delegateType, object target)
		{
			if (delegateType == null)
			{
				throw new ArgumentNullException("delegateType");
			}
			this.CreateDynMethod();
			return Delegate.CreateDelegate(delegateType, target, this);
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x000F98FC File Offset: 0x000F7AFC
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string parameterName)
		{
			if (position < 0 || position > this.parameters.Length)
			{
				throw new ArgumentOutOfRangeException("position");
			}
			this.RejectIfCreated();
			ParameterBuilder parameterBuilder = new ParameterBuilder(this, position, attributes, parameterName);
			if (this.pinfo == null)
			{
				this.pinfo = new ParameterBuilder[this.parameters.Length + 1];
			}
			this.pinfo[position] = parameterBuilder;
			return parameterBuilder;
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x000025CE File Offset: 0x000007CE
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x000F995A File Offset: 0x000F7B5A
		public override object[] GetCustomAttributes(bool inherit)
		{
			return new object[]
			{
				new MethodImplAttribute(this.GetMethodImplementationFlags())
			};
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x000F9970 File Offset: 0x000F7B70
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (attributeType.IsAssignableFrom(typeof(MethodImplAttribute)))
			{
				return new object[]
				{
					new MethodImplAttribute(this.GetMethodImplementationFlags())
				};
			}
			return EmptyArray<object>.Value;
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x000F99BD File Offset: 0x000F7BBD
		public DynamicILInfo GetDynamicILInfo()
		{
			if (this.il_info == null)
			{
				this.il_info = new DynamicILInfo(this);
			}
			return this.il_info;
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x000F99D9 File Offset: 0x000F7BD9
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x000F99E4 File Offset: 0x000F7BE4
		public ILGenerator GetILGenerator(int streamSize)
		{
			if ((this.GetMethodImplementationFlags() & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.GetMethodImplementationFlags() & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL)
			{
				throw new InvalidOperationException("Method body should not exist.");
			}
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.Module, new DynamicMethodTokenGenerator(this), streamSize);
			return this.ilgen;
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x00048AA1 File Offset: 0x00046CA1
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MethodImplAttributes.NoInlining;
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x000F1316 File Offset: 0x000EF516
		public override ParameterInfo[] GetParameters()
		{
			return this.GetParametersInternal();
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x000F9A40 File Offset: 0x000F7C40
		internal override ParameterInfo[] GetParametersInternal()
		{
			if (this.parameters == null)
			{
				return EmptyArray<ParameterInfo>.Value;
			}
			ParameterInfo[] array = new ParameterInfo[this.parameters.Length];
			for (int i = 0; i < this.parameters.Length; i++)
			{
				ParameterInfo[] array2 = array;
				int num = i;
				ParameterBuilder[] array3 = this.pinfo;
				array2[num] = RuntimeParameterInfo.New((array3 != null) ? array3[i + 1] : null, this.parameters[i], this, i + 1);
			}
			return array;
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x000F9AA2 File Offset: 0x000F7CA2
		internal override int GetParametersCount()
		{
			if (this.parameters != null)
			{
				return this.parameters.Length;
			}
			return 0;
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x000F9AB6 File Offset: 0x000F7CB6
		internal override Type GetParameterType(int pos)
		{
			return this.parameters[pos];
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x000F9AC0 File Offset: 0x000F7CC0
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			object obj2;
			try
			{
				this.CreateDynMethod();
				if (this.method == null)
				{
					this.method = new RuntimeMethodInfo(this.mhandle);
				}
				obj2 = this.method.Invoke(obj, invokeAttr, binder, parameters, culture);
			}
			catch (MethodAccessException ex)
			{
				throw new TargetInvocationException("Method cannot be invoked.", ex);
			}
			return obj2;
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x000F9B28 File Offset: 0x000F7D28
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return attributeType.IsAssignableFrom(typeof(MethodImplAttribute));
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x000F9B54 File Offset: 0x000F7D54
		public override string ToString()
		{
			string text = string.Empty;
			ParameterInfo[] parametersInternal = this.GetParametersInternal();
			for (int i = 0; i < parametersInternal.Length; i++)
			{
				if (i > 0)
				{
					text += ", ";
				}
				text += parametersInternal[i].ParameterType.Name;
			}
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

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06004EF6 RID: 20214 RVA: 0x000F9BDE File Offset: 0x000F7DDE
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x000F9BE6 File Offset: 0x000F7DE6
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.callingConvention;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x000F9BEE File Offset: 0x000F7DEE
		// (set) Token: 0x06004EFA RID: 20218 RVA: 0x000F9BF6 File Offset: 0x000F7DF6
		public bool InitLocals
		{
			get
			{
				return this.init_locals;
			}
			set
			{
				this.init_locals = value;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06004EFB RID: 20219 RVA: 0x000F9BFF File Offset: 0x000F7DFF
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.mhandle;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06004EFC RID: 20220 RVA: 0x000F9C07 File Offset: 0x000F7E07
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x000F9C0F File Offset: 0x000F7E0F
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override Type ReflectedType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Not implemented")]
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06004F00 RID: 20224 RVA: 0x000F9C17 File Offset: 0x000F7E17
		public override Type ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06004F01 RID: 20225 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Not implemented")]
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x000F9C1F File Offset: 0x000F7E1F
		private void RejectIfCreated()
		{
			if (this.mhandle.Value != IntPtr.Zero)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x000F9C44 File Offset: 0x000F7E44
		internal int AddRef(object reference)
		{
			if (this.refs == null)
			{
				this.refs = new object[4];
			}
			if (this.nrefs >= this.refs.Length - 1)
			{
				object[] array = new object[this.refs.Length * 2];
				Array.Copy(this.refs, array, this.refs.Length);
				this.refs = array;
			}
			this.refs[this.nrefs] = reference;
			this.refs[this.nrefs + 1] = null;
			this.nrefs += 2;
			return this.nrefs - 1;
		}

		// Token: 0x040030B5 RID: 12469
		private RuntimeMethodHandle mhandle;

		// Token: 0x040030B6 RID: 12470
		private string name;

		// Token: 0x040030B7 RID: 12471
		private Type returnType;

		// Token: 0x040030B8 RID: 12472
		private Type[] parameters;

		// Token: 0x040030B9 RID: 12473
		private MethodAttributes attributes;

		// Token: 0x040030BA RID: 12474
		private CallingConventions callingConvention;

		// Token: 0x040030BB RID: 12475
		private Module module;

		// Token: 0x040030BC RID: 12476
		private bool skipVisibility;

		// Token: 0x040030BD RID: 12477
		private bool init_locals = true;

		// Token: 0x040030BE RID: 12478
		private ILGenerator ilgen;

		// Token: 0x040030BF RID: 12479
		private int nrefs;

		// Token: 0x040030C0 RID: 12480
		private object[] refs;

		// Token: 0x040030C1 RID: 12481
		private IntPtr referenced_by;

		// Token: 0x040030C2 RID: 12482
		private Type owner;

		// Token: 0x040030C3 RID: 12483
		private Delegate deleg;

		// Token: 0x040030C4 RID: 12484
		private RuntimeMethodInfo method;

		// Token: 0x040030C5 RID: 12485
		private ParameterBuilder[] pinfo;

		// Token: 0x040030C6 RID: 12486
		internal bool creating;

		// Token: 0x040030C7 RID: 12487
		private DynamicILInfo il_info;

		// Token: 0x020008EE RID: 2286
		private static class AnonHostModuleHolder
		{
			// Token: 0x06004F04 RID: 20228 RVA: 0x000F9CD8 File Offset: 0x000F7ED8
			static AnonHostModuleHolder()
			{
				AssemblyName assemblyName = new AssemblyName();
				assemblyName.Name = "Anonymously Hosted DynamicMethods Assembly";
				DynamicMethod.AnonHostModuleHolder.anon_host_module = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run).GetManifestModule();
			}

			// Token: 0x17000D07 RID: 3335
			// (get) Token: 0x06004F05 RID: 20229 RVA: 0x000F9D0C File Offset: 0x000F7F0C
			public static Module AnonHostModule
			{
				get
				{
					return DynamicMethod.AnonHostModuleHolder.anon_host_module;
				}
			}

			// Token: 0x040030C8 RID: 12488
			public static readonly Module anon_host_module;
		}
	}
}

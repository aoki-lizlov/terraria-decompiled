using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000913 RID: 2323
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_SignatureHelper))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class SignatureHelper : _SignatureHelper
	{
		// Token: 0x060051AE RID: 20910 RVA: 0x00102266 File Offset: 0x00100466
		internal SignatureHelper(ModuleBuilder module, SignatureHelper.SignatureHelperType type)
		{
			this.type = type;
			this.module = module;
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x0010227C File Offset: 0x0010047C
		public static SignatureHelper GetFieldSigHelper(Module mod)
		{
			if (mod != null && !(mod is ModuleBuilder))
			{
				throw new ArgumentException("ModuleBuilder is expected");
			}
			return new SignatureHelper((ModuleBuilder)mod, SignatureHelper.SignatureHelperType.HELPER_FIELD);
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x001022A6 File Offset: 0x001004A6
		public static SignatureHelper GetLocalVarSigHelper(Module mod)
		{
			if (mod != null && !(mod is ModuleBuilder))
			{
				throw new ArgumentException("ModuleBuilder is expected");
			}
			return new SignatureHelper((ModuleBuilder)mod, SignatureHelper.SignatureHelperType.HELPER_LOCAL);
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x001022D0 File Offset: 0x001004D0
		public static SignatureHelper GetLocalVarSigHelper()
		{
			return new SignatureHelper(null, SignatureHelper.SignatureHelperType.HELPER_LOCAL);
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x001022D9 File Offset: 0x001004D9
		public static SignatureHelper GetMethodSigHelper(CallingConventions callingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(null, callingConvention, (CallingConvention)0, returnType, null);
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x001022E5 File Offset: 0x001004E5
		public static SignatureHelper GetMethodSigHelper(CallingConvention unmanagedCallingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(null, CallingConventions.Standard, unmanagedCallingConvention, returnType, null);
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x001022F1 File Offset: 0x001004F1
		public static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(mod, callingConvention, (CallingConvention)0, returnType, null);
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x001022FD File Offset: 0x001004FD
		public static SignatureHelper GetMethodSigHelper(Module mod, CallingConvention unmanagedCallConv, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(mod, CallingConventions.Standard, unmanagedCallConv, returnType, null);
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x00102309 File Offset: 0x00100509
		public static SignatureHelper GetMethodSigHelper(Module mod, Type returnType, Type[] parameterTypes)
		{
			return SignatureHelper.GetMethodSigHelper(mod, CallingConventions.Standard, (CallingConvention)0, returnType, parameterTypes);
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Not implemented")]
		public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] parameterTypes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Not implemented")]
		public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Not implemented")]
		public static SignatureHelper GetPropertySigHelper(Module mod, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x00102318 File Offset: 0x00100518
		private static int AppendArray(ref Type[] array, Type t)
		{
			if (array != null)
			{
				Type[] array2 = new Type[array.Length + 1];
				Array.Copy(array, array2, array.Length);
				array2[array.Length] = t;
				array = array2;
				return array.Length - 1;
			}
			array = new Type[1];
			array[0] = t;
			return 0;
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x00102360 File Offset: 0x00100560
		private static void AppendArrayAt(ref Type[][] array, Type[] t, int pos)
		{
			int num = Math.Max(pos, (array == null) ? 0 : array.Length);
			Type[][] array2 = new Type[num + 1][];
			if (array != null)
			{
				Array.Copy(array, array2, num);
			}
			array2[pos] = t;
			array = array2;
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x0010239C File Offset: 0x0010059C
		private static void ValidateParameterModifiers(string name, Type[] parameter_modifiers)
		{
			foreach (Type type in parameter_modifiers)
			{
				if (type == null)
				{
					throw new ArgumentNullException(name);
				}
				if (type.IsArray)
				{
					throw new ArgumentException(Locale.GetText("Array type not permitted"), name);
				}
				if (type.ContainsGenericParameters)
				{
					throw new ArgumentException(Locale.GetText("Open Generic Type not permitted"), name);
				}
			}
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x00102400 File Offset: 0x00100600
		private static void ValidateCustomModifier(int n, Type[][] custom_modifiers, string name)
		{
			if (custom_modifiers == null)
			{
				return;
			}
			if (custom_modifiers.Length != n)
			{
				throw new ArgumentException(Locale.GetText(string.Format("Custom modifiers length `{0}' does not match the size of the arguments", Array.Empty<object>())));
			}
			foreach (Type[] array in custom_modifiers)
			{
				if (array != null)
				{
					SignatureHelper.ValidateParameterModifiers(name, array);
				}
			}
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x0010244F File Offset: 0x0010064F
		private static Exception MissingFeature()
		{
			throw new NotImplementedException("Mono does not currently support setting modOpt/modReq through SignatureHelper");
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x0010245C File Offset: 0x0010065C
		[MonoTODO("Currently we ignore requiredCustomModifiers and optionalCustomModifiers")]
		public void AddArguments(Type[] arguments, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			if (requiredCustomModifiers != null || optionalCustomModifiers != null)
			{
				throw SignatureHelper.MissingFeature();
			}
			SignatureHelper.ValidateCustomModifier(arguments.Length, requiredCustomModifiers, "requiredCustomModifiers");
			SignatureHelper.ValidateCustomModifier(arguments.Length, optionalCustomModifiers, "optionalCustomModifiers");
			for (int i = 0; i < arguments.Length; i++)
			{
				this.AddArgument(arguments[i], (requiredCustomModifiers != null) ? requiredCustomModifiers[i] : null, (optionalCustomModifiers != null) ? optionalCustomModifiers[i] : null);
			}
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x001024C8 File Offset: 0x001006C8
		[MonoTODO("pinned is ignored")]
		public void AddArgument(Type argument, bool pinned)
		{
			this.AddArgument(argument);
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x001024D4 File Offset: 0x001006D4
		public void AddArgument(Type argument, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			if (argument == null)
			{
				throw new ArgumentNullException("argument");
			}
			if (requiredCustomModifiers != null)
			{
				SignatureHelper.ValidateParameterModifiers("requiredCustomModifiers", requiredCustomModifiers);
			}
			if (optionalCustomModifiers != null)
			{
				SignatureHelper.ValidateParameterModifiers("optionalCustomModifiers", optionalCustomModifiers);
			}
			int num = SignatureHelper.AppendArray(ref this.arguments, argument);
			if (requiredCustomModifiers != null)
			{
				SignatureHelper.AppendArrayAt(ref this.modreqs, requiredCustomModifiers, num);
			}
			if (optionalCustomModifiers != null)
			{
				SignatureHelper.AppendArrayAt(ref this.modopts, optionalCustomModifiers, num);
			}
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x0010253E File Offset: 0x0010073E
		public void AddArgument(Type clsArgument)
		{
			if (clsArgument == null)
			{
				throw new ArgumentNullException("clsArgument");
			}
			SignatureHelper.AppendArray(ref this.arguments, clsArgument);
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Not implemented")]
		public void AddSentinel()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x00102564 File Offset: 0x00100764
		private static bool CompareOK(Type[][] one, Type[][] two)
		{
			if (one == null)
			{
				return two == null;
			}
			if (two == null)
			{
				return false;
			}
			if (one.Length != two.Length)
			{
				return false;
			}
			int i = 0;
			while (i < one.Length)
			{
				Type[] array = one[i];
				Type[] array2 = two[i];
				if (array == null)
				{
					if (array2 != null)
					{
						goto IL_0032;
					}
				}
				else
				{
					if (array2 == null)
					{
						return false;
					}
					goto IL_0032;
				}
				IL_0083:
				i++;
				continue;
				IL_0032:
				if (array.Length != array2.Length)
				{
					return false;
				}
				for (int j = 0; j < array.Length; j++)
				{
					Type type = array[j];
					Type type2 = array2[j];
					if (type == null)
					{
						if (!(type2 == null))
						{
							return false;
						}
					}
					else
					{
						if (type2 == null)
						{
							return false;
						}
						if (!type.Equals(type2))
						{
							return false;
						}
					}
				}
				goto IL_0083;
			}
			return true;
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x00102600 File Offset: 0x00100800
		public override bool Equals(object obj)
		{
			SignatureHelper signatureHelper = obj as SignatureHelper;
			if (signatureHelper == null)
			{
				return false;
			}
			if (signatureHelper.module != this.module || signatureHelper.returnType != this.returnType || signatureHelper.callConv != this.callConv || signatureHelper.unmanagedCallConv != this.unmanagedCallConv)
			{
				return false;
			}
			if (this.arguments != null)
			{
				if (signatureHelper.arguments == null)
				{
					return false;
				}
				if (this.arguments.Length != signatureHelper.arguments.Length)
				{
					return false;
				}
				for (int i = 0; i < this.arguments.Length; i++)
				{
					if (!signatureHelper.arguments[i].Equals(this.arguments[i]))
					{
						return false;
					}
				}
			}
			else if (signatureHelper.arguments != null)
			{
				return false;
			}
			return SignatureHelper.CompareOK(signatureHelper.modreqs, this.modreqs) && SignatureHelper.CompareOK(signatureHelper.modopts, this.modopts);
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x0000408A File Offset: 0x0000228A
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x060051C7 RID: 20935
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern byte[] get_signature_local();

		// Token: 0x060051C8 RID: 20936
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern byte[] get_signature_field();

		// Token: 0x060051C9 RID: 20937 RVA: 0x001026E4 File Offset: 0x001008E4
		public byte[] GetSignature()
		{
			TypeBuilder.ResolveUserTypes(this.arguments);
			SignatureHelper.SignatureHelperType signatureHelperType = this.type;
			if (signatureHelperType == SignatureHelper.SignatureHelperType.HELPER_FIELD)
			{
				return this.get_signature_field();
			}
			if (signatureHelperType == SignatureHelper.SignatureHelperType.HELPER_LOCAL)
			{
				return this.get_signature_local();
			}
			throw new NotImplementedException();
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x0010271D File Offset: 0x0010091D
		public override string ToString()
		{
			return "SignatureHelper";
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x00102724 File Offset: 0x00100924
		internal static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, CallingConvention unmanagedCallingConvention, Type returnType, Type[] parameters)
		{
			if (mod != null && !(mod is ModuleBuilder))
			{
				throw new ArgumentException("ModuleBuilder is expected");
			}
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			if (returnType.IsUserType)
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			if (parameters != null)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					if (parameters[i].IsUserType)
					{
						throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
					}
				}
			}
			SignatureHelper signatureHelper = new SignatureHelper((ModuleBuilder)mod, SignatureHelper.SignatureHelperType.HELPER_METHOD);
			signatureHelper.returnType = returnType;
			signatureHelper.callConv = callingConvention;
			signatureHelper.unmanagedCallConv = unmanagedCallingConvention;
			if (parameters != null)
			{
				signatureHelper.arguments = new Type[parameters.Length];
				for (int j = 0; j < parameters.Length; j++)
				{
					signatureHelper.arguments[j] = parameters[j];
				}
			}
			return signatureHelper;
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x000174FB File Offset: 0x000156FB
		void _SignatureHelper.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x000174FB File Offset: 0x000156FB
		void _SignatureHelper.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x000174FB File Offset: 0x000156FB
		void _SignatureHelper.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x000174FB File Offset: 0x000156FB
		void _SignatureHelper.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400328A RID: 12938
		private ModuleBuilder module;

		// Token: 0x0400328B RID: 12939
		private Type[] arguments;

		// Token: 0x0400328C RID: 12940
		private SignatureHelper.SignatureHelperType type;

		// Token: 0x0400328D RID: 12941
		private Type returnType;

		// Token: 0x0400328E RID: 12942
		private CallingConventions callConv;

		// Token: 0x0400328F RID: 12943
		private CallingConvention unmanagedCallConv;

		// Token: 0x04003290 RID: 12944
		private Type[][] modreqs;

		// Token: 0x04003291 RID: 12945
		private Type[][] modopts;

		// Token: 0x02000914 RID: 2324
		internal enum SignatureHelperType
		{
			// Token: 0x04003293 RID: 12947
			HELPER_FIELD,
			// Token: 0x04003294 RID: 12948
			HELPER_LOCAL,
			// Token: 0x04003295 RID: 12949
			HELPER_METHOD,
			// Token: 0x04003296 RID: 12950
			HELPER_PROPERTY
		}
	}
}

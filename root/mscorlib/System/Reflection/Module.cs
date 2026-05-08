using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace System.Reflection
{
	// Token: 0x02000881 RID: 2177
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class Module : ICustomAttributeProvider, ISerializable
	{
		// Token: 0x060048E9 RID: 18665 RVA: 0x000025BE File Offset: 0x000007BE
		protected Module()
		{
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060048EA RID: 18666 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual Assembly Assembly
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060048EB RID: 18667 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual string FullyQualifiedName
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060048EC RID: 18668 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual string Name
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual int MDStreamVersion
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060048EE RID: 18670 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual Guid ModuleVersionId
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual string ScopeName
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060048F0 RID: 18672 RVA: 0x000EE8E6 File Offset: 0x000ECAE6
		public ModuleHandle ModuleHandle
		{
			get
			{
				return this.GetModuleHandleImpl();
			}
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x000EE8EE File Offset: 0x000ECAEE
		internal virtual ModuleHandle GetModuleHandleImpl()
		{
			return ModuleHandle.EmptyHandle;
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool IsResource()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060048F5 RID: 18677 RVA: 0x000EE8F5 File Offset: 0x000ECAF5
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x000EE8FD File Offset: 0x000ECAFD
		public MethodInfo GetMethod(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, null, null);
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x000EE91A File Offset: 0x000ECB1A
		public MethodInfo GetMethod(string name, Type[] types)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, types, null);
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x000EE92C File Offset: 0x000ECB2C
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x00047D5E File Offset: 0x00045F5E
		protected virtual MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x000EE98B File Offset: 0x000ECB8B
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x000EE995 File Offset: 0x000ECB95
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x000EE9A0 File Offset: 0x000ECBA0
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual Type[] GetTypes()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x000EE9AA File Offset: 0x000ECBAA
		public virtual Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x000EE9B5 File Offset: 0x000ECBB5
		public virtual Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x000EE9C0 File Offset: 0x000ECBC0
		public virtual Type[] FindTypes(TypeFilter filter, object filterCriteria)
		{
			Type[] types = this.GetTypes();
			int num = 0;
			for (int i = 0; i < types.Length; i++)
			{
				if (filter != null && !filter(types[i], filterCriteria))
				{
					types[i] = null;
				}
				else
				{
					num++;
				}
			}
			if (num == types.Length)
			{
				return types;
			}
			Type[] array = new Type[num];
			num = 0;
			for (int j = 0; j < types.Length; j++)
			{
				if (types[j] != null)
				{
					array[num++] = types[j];
				}
			}
			return array;
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06004908 RID: 18696 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual int MetadataToken
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x000EEA38 File Offset: 0x000ECC38
		public FieldInfo ResolveField(int metadataToken)
		{
			return this.ResolveField(metadataToken, null, null);
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x000EEA43 File Offset: 0x000ECC43
		public MemberInfo ResolveMember(int metadataToken)
		{
			return this.ResolveMember(metadataToken, null, null);
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x000EEA4E File Offset: 0x000ECC4E
		public MethodBase ResolveMethod(int metadataToken)
		{
			return this.ResolveMethod(metadataToken, null, null);
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual byte[] ResolveSignature(int metadataToken)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual string ResolveString(int metadataToken)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x000EEA59 File Offset: 0x000ECC59
		public Type ResolveType(int metadataToken)
		{
			return this.ResolveType(metadataToken, null, null);
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x00097F7A File Offset: 0x0009617A
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x00093238 File Offset: 0x00091438
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x00064F2C File Offset: 0x0006312C
		public static bool operator ==(Module left, Module right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x000EEA64 File Offset: 0x000ECC64
		public static bool operator !=(Module left, Module right)
		{
			return !(left == right);
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x000EEA70 File Offset: 0x000ECC70
		public override string ToString()
		{
			return this.ScopeName;
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x000EEA78 File Offset: 0x000ECC78
		private static bool FilterTypeNameImpl(Type cls, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException("A String must be provided for the filter criteria.");
			}
			string text = (string)filterCriteria;
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				return cls.Name.StartsWith(text, StringComparison.Ordinal);
			}
			return cls.Name.Equals(text);
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x000EEAE8 File Offset: 0x000ECCE8
		private static bool FilterTypeNameIgnoreCaseImpl(Type cls, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException("A String must be provided for the filter criteria.");
			}
			string text = (string)filterCriteria;
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				string name = cls.Name;
				return name.Length >= text.Length && string.Compare(name, 0, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return string.Compare(text, cls.Name, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x0600491B RID: 18715 RVA: 0x000EEB78 File Offset: 0x000ECD78
		internal Guid MvId
		{
			get
			{
				return this.GetModuleVersionId();
			}
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x000EEB78 File Offset: 0x000ECD78
		internal static Guid Mono_GetGuid(Module module)
		{
			return module.GetModuleVersionId();
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual Guid GetModuleVersionId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual X509Certificate GetSignerCertificate()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x000EEB80 File Offset: 0x000ECD80
		// Note: this type is marked as 'beforefieldinit'.
		static Module()
		{
		}

		// Token: 0x04002E6F RID: 11887
		public static readonly TypeFilter FilterTypeName = new TypeFilter(Module.FilterTypeNameImpl);

		// Token: 0x04002E70 RID: 11888
		public static readonly TypeFilter FilterTypeNameIgnoreCase = new TypeFilter(Module.FilterTypeNameIgnoreCaseImpl);

		// Token: 0x04002E71 RID: 11889
		private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
	}
}

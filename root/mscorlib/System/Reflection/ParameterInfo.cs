using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000886 RID: 2182
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class ParameterInfo : ICustomAttributeProvider, IObjectReference
	{
		// Token: 0x06004931 RID: 18737 RVA: 0x000025BE File Offset: 0x000007BE
		protected ParameterInfo()
		{
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06004932 RID: 18738 RVA: 0x000EEC3F File Offset: 0x000ECE3F
		public virtual ParameterAttributes Attributes
		{
			get
			{
				return this.AttrsImpl;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06004933 RID: 18739 RVA: 0x000EEC47 File Offset: 0x000ECE47
		public virtual MemberInfo Member
		{
			get
			{
				return this.MemberImpl;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x000EEC4F File Offset: 0x000ECE4F
		public virtual string Name
		{
			get
			{
				return this.NameImpl;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06004935 RID: 18741 RVA: 0x000EEC57 File Offset: 0x000ECE57
		public virtual Type ParameterType
		{
			get
			{
				return this.ClassImpl;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06004936 RID: 18742 RVA: 0x000EEC5F File Offset: 0x000ECE5F
		public virtual int Position
		{
			get
			{
				return this.PositionImpl;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x000EEC67 File Offset: 0x000ECE67
		public bool IsIn
		{
			get
			{
				return (this.Attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06004938 RID: 18744 RVA: 0x000EEC74 File Offset: 0x000ECE74
		public bool IsLcid
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Lcid) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06004939 RID: 18745 RVA: 0x000EEC81 File Offset: 0x000ECE81
		public bool IsOptional
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600493A RID: 18746 RVA: 0x000EEC8F File Offset: 0x000ECE8F
		public bool IsOut
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600493B RID: 18747 RVA: 0x000EEC9C File Offset: 0x000ECE9C
		public bool IsRetval
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Retval) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600493C RID: 18748 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual object DefaultValue
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600493D RID: 18749 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual object RawDefaultValue
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600493E RID: 18750 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool HasDefaultValue
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x000EECA9 File Offset: 0x000ECEA9
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06004940 RID: 18752 RVA: 0x000EECC0 File Offset: 0x000ECEC0
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x000EECC8 File Offset: 0x000ECEC8
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return Array.Empty<object>();
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x000EECCF File Offset: 0x000ECECF
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return Array.Empty<object>();
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x000EECEA File Offset: 0x000ECEEA
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x000EECEA File Offset: 0x000ECEEA
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x000EECF1 File Offset: 0x000ECEF1
		public virtual int MetadataToken
		{
			get
			{
				return 134217728;
			}
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x000EECF8 File Offset: 0x000ECEF8
		public object GetRealObject(StreamingContext context)
		{
			if (this.MemberImpl == null)
			{
				throw new SerializationException("Insufficient state to return the real object.");
			}
			MemberTypes memberType = this.MemberImpl.MemberType;
			if (memberType != MemberTypes.Constructor && memberType != MemberTypes.Method)
			{
				if (memberType != MemberTypes.Property)
				{
					throw new SerializationException("Serialized member does not have a ParameterInfo.");
				}
				ParameterInfo[] array = ((PropertyInfo)this.MemberImpl).GetIndexParameters();
				if (array != null && this.PositionImpl > -1 && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException("Non existent ParameterInfo. Position bigger than member's parameters length.");
			}
			else if (this.PositionImpl == -1)
			{
				if (this.MemberImpl.MemberType == MemberTypes.Method)
				{
					return ((MethodInfo)this.MemberImpl).ReturnParameter;
				}
				throw new SerializationException("Non existent ParameterInfo. Position bigger than member's parameters length.");
			}
			else
			{
				ParameterInfo[] array = ((MethodBase)this.MemberImpl).GetParametersNoCopy();
				if (array != null && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException("Non existent ParameterInfo. Position bigger than member's parameters length.");
			}
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x000EEDEA File Offset: 0x000ECFEA
		public override string ToString()
		{
			return this.ParameterType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x04002E84 RID: 11908
		protected ParameterAttributes AttrsImpl;

		// Token: 0x04002E85 RID: 11909
		protected Type ClassImpl;

		// Token: 0x04002E86 RID: 11910
		protected object DefaultValueImpl;

		// Token: 0x04002E87 RID: 11911
		protected MemberInfo MemberImpl;

		// Token: 0x04002E88 RID: 11912
		protected string NameImpl;

		// Token: 0x04002E89 RID: 11913
		protected int PositionImpl;

		// Token: 0x04002E8A RID: 11914
		private const int MetadataToken_ParamDef = 134217728;
	}
}

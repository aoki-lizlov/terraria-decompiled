using System;
using System.Collections.Generic;

namespace System.Reflection
{
	// Token: 0x0200087A RID: 2170
	[Serializable]
	public abstract class MemberInfo : ICustomAttributeProvider
	{
		// Token: 0x06004890 RID: 18576 RVA: 0x000025BE File Offset: 0x000007BE
		protected MemberInfo()
		{
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06004891 RID: 18577
		public abstract MemberTypes MemberType { get; }

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06004892 RID: 18578
		public abstract string Name { get; }

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06004893 RID: 18579
		public abstract Type DeclaringType { get; }

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06004894 RID: 18580
		public abstract Type ReflectedType { get; }

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06004895 RID: 18581 RVA: 0x000EE388 File Offset: 0x000EC588
		public virtual Module Module
		{
			get
			{
				Type type = this as Type;
				if (type != null)
				{
					return type.Module;
				}
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004897 RID: 18583
		public abstract bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06004898 RID: 18584
		public abstract object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004899 RID: 18585
		public abstract object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600489A RID: 18586 RVA: 0x000EE3B1 File Offset: 0x000EC5B1
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600489C RID: 18588 RVA: 0x00084CDD File Offset: 0x00082EDD
		public virtual int MetadataToken
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x00097F7A File Offset: 0x0009617A
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x00093238 File Offset: 0x00091438
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x000EE3BC File Offset: 0x000EC5BC
		public static bool operator ==(MemberInfo left, MemberInfo right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			Type type;
			Type type2;
			if ((type = left as Type) != null && (type2 = right as Type) != null)
			{
				return type == type2;
			}
			MethodBase methodBase;
			MethodBase methodBase2;
			if ((methodBase = left as MethodBase) != null && (methodBase2 = right as MethodBase) != null)
			{
				return methodBase == methodBase2;
			}
			FieldInfo fieldInfo;
			FieldInfo fieldInfo2;
			if ((fieldInfo = left as FieldInfo) != null && (fieldInfo2 = right as FieldInfo) != null)
			{
				return fieldInfo == fieldInfo2;
			}
			EventInfo eventInfo;
			EventInfo eventInfo2;
			if ((eventInfo = left as EventInfo) != null && (eventInfo2 = right as EventInfo) != null)
			{
				return eventInfo == eventInfo2;
			}
			PropertyInfo propertyInfo;
			PropertyInfo propertyInfo2;
			return (propertyInfo = left as PropertyInfo) != null && (propertyInfo2 = right as PropertyInfo) != null && propertyInfo == propertyInfo2;
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x000EE4AC File Offset: 0x000EC6AC
		public static bool operator !=(MemberInfo left, MemberInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual bool CacheEquals(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x000EE4B8 File Offset: 0x000EC6B8
		internal bool HasSameMetadataDefinitionAsCore<TOther>(MemberInfo other) where TOther : MemberInfo
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			return other is TOther && this.MetadataToken == other.MetadataToken && this.Module.Equals(other.Module);
		}
	}
}

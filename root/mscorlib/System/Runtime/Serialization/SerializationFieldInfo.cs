using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization
{
	// Token: 0x02000642 RID: 1602
	internal sealed class SerializationFieldInfo : FieldInfo
	{
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x000D44B4 File Offset: 0x000D26B4
		public override Module Module
		{
			get
			{
				return this.m_field.Module;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06003D02 RID: 15618 RVA: 0x000D44C1 File Offset: 0x000D26C1
		public override int MetadataToken
		{
			get
			{
				return this.m_field.MetadataToken;
			}
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x000D44CE File Offset: 0x000D26CE
		internal SerializationFieldInfo(RuntimeFieldInfo field, string namePrefix)
		{
			this.m_field = field;
			this.m_serializationName = namePrefix + "+" + this.m_field.Name;
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x000D44F9 File Offset: 0x000D26F9
		public override string Name
		{
			get
			{
				return this.m_serializationName;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003D05 RID: 15621 RVA: 0x000D4501 File Offset: 0x000D2701
		public override Type DeclaringType
		{
			get
			{
				return this.m_field.DeclaringType;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x000D450E File Offset: 0x000D270E
		public override Type ReflectedType
		{
			get
			{
				return this.m_field.ReflectedType;
			}
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x000D451B File Offset: 0x000D271B
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_field.GetCustomAttributes(inherit);
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x000D4529 File Offset: 0x000D2729
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_field.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x000D4538 File Offset: 0x000D2738
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_field.IsDefined(attributeType, inherit);
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x000D4547 File Offset: 0x000D2747
		public override Type FieldType
		{
			get
			{
				return this.m_field.FieldType;
			}
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x000D4554 File Offset: 0x000D2754
		public override object GetValue(object obj)
		{
			return this.m_field.GetValue(obj);
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x000D4564 File Offset: 0x000D2764
		[SecurityCritical]
		internal object InternalGetValue(object obj)
		{
			RtFieldInfo field = this.m_field;
			if (field != null)
			{
				field.CheckConsistency(obj);
				return field.UnsafeGetValue(obj);
			}
			return this.m_field.GetValue(obj);
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x000D459C File Offset: 0x000D279C
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x000D45B0 File Offset: 0x000D27B0
		[SecurityCritical]
		internal void InternalSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			RtFieldInfo field = this.m_field;
			if (field != null)
			{
				field.CheckConsistency(obj);
				field.UnsafeSetValue(obj, value, invokeAttr, binder, culture);
				return;
			}
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06003D0F RID: 15631 RVA: 0x000D45F4 File Offset: 0x000D27F4
		internal RuntimeFieldInfo FieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003D10 RID: 15632 RVA: 0x000D45FC File Offset: 0x000D27FC
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.m_field.FieldHandle;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06003D11 RID: 15633 RVA: 0x000D4609 File Offset: 0x000D2809
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_field.Attributes;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06003D12 RID: 15634 RVA: 0x000D4618 File Offset: 0x000D2818
		internal RemotingFieldCachedData RemotingCache
		{
			get
			{
				RemotingFieldCachedData remotingFieldCachedData = this.m_cachedData;
				if (remotingFieldCachedData == null)
				{
					remotingFieldCachedData = new RemotingFieldCachedData(this);
					RemotingFieldCachedData remotingFieldCachedData2 = Interlocked.CompareExchange<RemotingFieldCachedData>(ref this.m_cachedData, remotingFieldCachedData, null);
					if (remotingFieldCachedData2 != null)
					{
						remotingFieldCachedData = remotingFieldCachedData2;
					}
				}
				return remotingFieldCachedData;
			}
		}

		// Token: 0x04002706 RID: 9990
		internal const string FakeNameSeparatorString = "+";

		// Token: 0x04002707 RID: 9991
		private RuntimeFieldInfo m_field;

		// Token: 0x04002708 RID: 9992
		private string m_serializationName;

		// Token: 0x04002709 RID: 9993
		private RemotingFieldCachedData m_cachedData;
	}
}

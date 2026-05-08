using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009D RID: 157
	public class JsonObjectContract : JsonContainerContract
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001C849 File Offset: 0x0001AA49
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001C851 File Offset: 0x0001AA51
		public MemberSerialization MemberSerialization
		{
			[CompilerGenerated]
			get
			{
				return this.<MemberSerialization>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MemberSerialization>k__BackingField = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001C85A File Offset: 0x0001AA5A
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x0001C862 File Offset: 0x0001AA62
		public Required? ItemRequired
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemRequired>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemRequired>k__BackingField = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001C86B File Offset: 0x0001AA6B
		public JsonPropertyCollection Properties
		{
			[CompilerGenerated]
			get
			{
				return this.<Properties>k__BackingField;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001C873 File Offset: 0x0001AA73
		public JsonPropertyCollection CreatorParameters
		{
			get
			{
				if (this._creatorParameters == null)
				{
					this._creatorParameters = new JsonPropertyCollection(base.UnderlyingType);
				}
				return this._creatorParameters;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001C894 File Offset: 0x0001AA94
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001C89C File Offset: 0x0001AA9C
		public ObjectConstructor<object> OverrideCreator
		{
			get
			{
				return this._overrideCreator;
			}
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001C8A5 File Offset: 0x0001AAA5
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0001C8AD File Offset: 0x0001AAAD
		internal ObjectConstructor<object> ParameterizedCreator
		{
			get
			{
				return this._parameterizedCreator;
			}
			set
			{
				this._parameterizedCreator = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001C8B6 File Offset: 0x0001AAB6
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001C8BE File Offset: 0x0001AABE
		public ExtensionDataSetter ExtensionDataSetter
		{
			[CompilerGenerated]
			get
			{
				return this.<ExtensionDataSetter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExtensionDataSetter>k__BackingField = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001C8C7 File Offset: 0x0001AAC7
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001C8CF File Offset: 0x0001AACF
		public ExtensionDataGetter ExtensionDataGetter
		{
			[CompilerGenerated]
			get
			{
				return this.<ExtensionDataGetter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExtensionDataGetter>k__BackingField = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0001C8E0 File Offset: 0x0001AAE0
		public Type ExtensionDataValueType
		{
			get
			{
				return this._extensionDataValueType;
			}
			set
			{
				this._extensionDataValueType = value;
				this.ExtensionDataIsJToken = value != null && typeof(JToken).IsAssignableFrom(value);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001C90B File Offset: 0x0001AB0B
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0001C913 File Offset: 0x0001AB13
		public Func<string, string> ExtensionDataNameResolver
		{
			[CompilerGenerated]
			get
			{
				return this.<ExtensionDataNameResolver>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExtensionDataNameResolver>k__BackingField = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001C91C File Offset: 0x0001AB1C
		internal bool HasRequiredOrDefaultValueProperties
		{
			get
			{
				if (this._hasRequiredOrDefaultValueProperties == null)
				{
					this._hasRequiredOrDefaultValueProperties = new bool?(false);
					if (this.ItemRequired.GetValueOrDefault(Required.Default) != Required.Default)
					{
						this._hasRequiredOrDefaultValueProperties = new bool?(true);
					}
					else
					{
						foreach (JsonProperty jsonProperty in this.Properties)
						{
							if (jsonProperty.Required != Required.Default || (jsonProperty.DefaultValueHandling & DefaultValueHandling.Populate) == DefaultValueHandling.Populate)
							{
								this._hasRequiredOrDefaultValueProperties = new bool?(true);
								break;
							}
						}
					}
				}
				return this._hasRequiredOrDefaultValueProperties.GetValueOrDefault();
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001CA08 File Offset: 0x0001AC08
		public JsonObjectContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Object;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001CA29 File Offset: 0x0001AC29
		[SecuritySafeCritical]
		internal object GetUninitializedObject()
		{
			if (!JsonTypeReflector.FullyTrusted)
			{
				throw new JsonException("Insufficient permissions. Creating an uninitialized '{0}' type requires full trust.".FormatWith(CultureInfo.InvariantCulture, this.NonNullableUnderlyingType));
			}
			return FormatterServices.GetUninitializedObject(this.NonNullableUnderlyingType);
		}

		// Token: 0x04000303 RID: 771
		[CompilerGenerated]
		private MemberSerialization <MemberSerialization>k__BackingField;

		// Token: 0x04000304 RID: 772
		[CompilerGenerated]
		private Required? <ItemRequired>k__BackingField;

		// Token: 0x04000305 RID: 773
		[CompilerGenerated]
		private readonly JsonPropertyCollection <Properties>k__BackingField;

		// Token: 0x04000306 RID: 774
		[CompilerGenerated]
		private ExtensionDataSetter <ExtensionDataSetter>k__BackingField;

		// Token: 0x04000307 RID: 775
		[CompilerGenerated]
		private ExtensionDataGetter <ExtensionDataGetter>k__BackingField;

		// Token: 0x04000308 RID: 776
		[CompilerGenerated]
		private Func<string, string> <ExtensionDataNameResolver>k__BackingField;

		// Token: 0x04000309 RID: 777
		internal bool ExtensionDataIsJToken;

		// Token: 0x0400030A RID: 778
		private bool? _hasRequiredOrDefaultValueProperties;

		// Token: 0x0400030B RID: 779
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x0400030C RID: 780
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x0400030D RID: 781
		private JsonPropertyCollection _creatorParameters;

		// Token: 0x0400030E RID: 782
		private Type _extensionDataValueType;
	}
}

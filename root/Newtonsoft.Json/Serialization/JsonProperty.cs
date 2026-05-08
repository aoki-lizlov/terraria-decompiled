using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009A RID: 154
	public class JsonProperty
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001C3A6 File Offset: 0x0001A5A6
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0001C3AE File Offset: 0x0001A5AE
		internal JsonContract PropertyContract
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyContract>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PropertyContract>k__BackingField = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001C3B7 File Offset: 0x0001A5B7
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0001C3BF File Offset: 0x0001A5BF
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				this._propertyName = value;
				this._skipPropertyNameEscape = !JavaScriptUtils.ShouldEscapeJavaScriptString(this._propertyName, JavaScriptUtils.HtmlCharEscapeFlags);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001C3E1 File Offset: 0x0001A5E1
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0001C3E9 File Offset: 0x0001A5E9
		public Type DeclaringType
		{
			[CompilerGenerated]
			get
			{
				return this.<DeclaringType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DeclaringType>k__BackingField = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001C3F2 File Offset: 0x0001A5F2
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0001C3FA File Offset: 0x0001A5FA
		public int? Order
		{
			[CompilerGenerated]
			get
			{
				return this.<Order>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Order>k__BackingField = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001C403 File Offset: 0x0001A603
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0001C40B File Offset: 0x0001A60B
		public string UnderlyingName
		{
			[CompilerGenerated]
			get
			{
				return this.<UnderlyingName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnderlyingName>k__BackingField = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001C414 File Offset: 0x0001A614
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0001C41C File Offset: 0x0001A61C
		public IValueProvider ValueProvider
		{
			[CompilerGenerated]
			get
			{
				return this.<ValueProvider>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ValueProvider>k__BackingField = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001C425 File Offset: 0x0001A625
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0001C42D File Offset: 0x0001A62D
		public IAttributeProvider AttributeProvider
		{
			[CompilerGenerated]
			get
			{
				return this.<AttributeProvider>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AttributeProvider>k__BackingField = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001C436 File Offset: 0x0001A636
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0001C43E File Offset: 0x0001A63E
		public Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
			set
			{
				if (this._propertyType != value)
				{
					this._propertyType = value;
					this._hasGeneratedDefaultValue = false;
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001C45C File Offset: 0x0001A65C
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0001C464 File Offset: 0x0001A664
		public JsonConverter Converter
		{
			[CompilerGenerated]
			get
			{
				return this.<Converter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Converter>k__BackingField = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001C46D File Offset: 0x0001A66D
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x0001C475 File Offset: 0x0001A675
		public JsonConverter MemberConverter
		{
			[CompilerGenerated]
			get
			{
				return this.<MemberConverter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MemberConverter>k__BackingField = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001C47E File Offset: 0x0001A67E
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x0001C486 File Offset: 0x0001A686
		public bool Ignored
		{
			[CompilerGenerated]
			get
			{
				return this.<Ignored>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Ignored>k__BackingField = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001C48F File Offset: 0x0001A68F
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001C497 File Offset: 0x0001A697
		public bool Readable
		{
			[CompilerGenerated]
			get
			{
				return this.<Readable>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Readable>k__BackingField = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001C4A0 File Offset: 0x0001A6A0
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001C4A8 File Offset: 0x0001A6A8
		public bool Writable
		{
			[CompilerGenerated]
			get
			{
				return this.<Writable>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Writable>k__BackingField = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001C4B1 File Offset: 0x0001A6B1
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001C4B9 File Offset: 0x0001A6B9
		public bool HasMemberAttribute
		{
			[CompilerGenerated]
			get
			{
				return this.<HasMemberAttribute>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HasMemberAttribute>k__BackingField = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001C4C2 File Offset: 0x0001A6C2
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001C4D4 File Offset: 0x0001A6D4
		public object DefaultValue
		{
			get
			{
				if (!this._hasExplicitDefaultValue)
				{
					return null;
				}
				return this._defaultValue;
			}
			set
			{
				this._hasExplicitDefaultValue = true;
				this._defaultValue = value;
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001C4E4 File Offset: 0x0001A6E4
		internal object GetResolvedDefaultValue()
		{
			if (this._propertyType == null)
			{
				return null;
			}
			if (!this._hasExplicitDefaultValue && !this._hasGeneratedDefaultValue)
			{
				this._defaultValue = ReflectionUtils.GetDefaultValue(this.PropertyType);
				this._hasGeneratedDefaultValue = true;
			}
			return this._defaultValue;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001C524 File Offset: 0x0001A724
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x0001C54A File Offset: 0x0001A74A
		public Required Required
		{
			get
			{
				Required? required = this._required;
				if (required == null)
				{
					return Required.Default;
				}
				return required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001C558 File Offset: 0x0001A758
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x0001C560 File Offset: 0x0001A760
		public bool? IsReference
		{
			[CompilerGenerated]
			get
			{
				return this.<IsReference>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsReference>k__BackingField = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001C569 File Offset: 0x0001A769
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x0001C571 File Offset: 0x0001A771
		public NullValueHandling? NullValueHandling
		{
			[CompilerGenerated]
			get
			{
				return this.<NullValueHandling>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NullValueHandling>k__BackingField = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001C57A File Offset: 0x0001A77A
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0001C582 File Offset: 0x0001A782
		public DefaultValueHandling? DefaultValueHandling
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultValueHandling>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DefaultValueHandling>k__BackingField = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001C58B File Offset: 0x0001A78B
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0001C593 File Offset: 0x0001A793
		public ReferenceLoopHandling? ReferenceLoopHandling
		{
			[CompilerGenerated]
			get
			{
				return this.<ReferenceLoopHandling>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReferenceLoopHandling>k__BackingField = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001C59C File Offset: 0x0001A79C
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0001C5A4 File Offset: 0x0001A7A4
		public ObjectCreationHandling? ObjectCreationHandling
		{
			[CompilerGenerated]
			get
			{
				return this.<ObjectCreationHandling>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ObjectCreationHandling>k__BackingField = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001C5AD File Offset: 0x0001A7AD
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001C5B5 File Offset: 0x0001A7B5
		public TypeNameHandling? TypeNameHandling
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeNameHandling>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TypeNameHandling>k__BackingField = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001C5BE File Offset: 0x0001A7BE
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001C5C6 File Offset: 0x0001A7C6
		public Predicate<object> ShouldSerialize
		{
			[CompilerGenerated]
			get
			{
				return this.<ShouldSerialize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ShouldSerialize>k__BackingField = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001C5CF File Offset: 0x0001A7CF
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001C5D7 File Offset: 0x0001A7D7
		public Predicate<object> ShouldDeserialize
		{
			[CompilerGenerated]
			get
			{
				return this.<ShouldDeserialize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ShouldDeserialize>k__BackingField = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001C5E8 File Offset: 0x0001A7E8
		public Predicate<object> GetIsSpecified
		{
			[CompilerGenerated]
			get
			{
				return this.<GetIsSpecified>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GetIsSpecified>k__BackingField = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001C5F1 File Offset: 0x0001A7F1
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001C5F9 File Offset: 0x0001A7F9
		public Action<object, object> SetIsSpecified
		{
			[CompilerGenerated]
			get
			{
				return this.<SetIsSpecified>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SetIsSpecified>k__BackingField = value;
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001C602 File Offset: 0x0001A802
		public override string ToString()
		{
			return this.PropertyName;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001C60A File Offset: 0x0001A80A
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001C612 File Offset: 0x0001A812
		public JsonConverter ItemConverter
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemConverter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemConverter>k__BackingField = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001C61B File Offset: 0x0001A81B
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001C623 File Offset: 0x0001A823
		public bool? ItemIsReference
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemIsReference>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemIsReference>k__BackingField = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001C62C File Offset: 0x0001A82C
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0001C634 File Offset: 0x0001A834
		public TypeNameHandling? ItemTypeNameHandling
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemTypeNameHandling>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemTypeNameHandling>k__BackingField = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001C63D File Offset: 0x0001A83D
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001C645 File Offset: 0x0001A845
		public ReferenceLoopHandling? ItemReferenceLoopHandling
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemReferenceLoopHandling>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemReferenceLoopHandling>k__BackingField = value;
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001C64E File Offset: 0x0001A84E
		internal void WritePropertyName(JsonWriter writer)
		{
			if (this._skipPropertyNameEscape)
			{
				writer.WritePropertyName(this.PropertyName, false);
				return;
			}
			writer.WritePropertyName(this.PropertyName);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00008020 File Offset: 0x00006220
		public JsonProperty()
		{
		}

		// Token: 0x040002E0 RID: 736
		internal Required? _required;

		// Token: 0x040002E1 RID: 737
		internal bool _hasExplicitDefaultValue;

		// Token: 0x040002E2 RID: 738
		private object _defaultValue;

		// Token: 0x040002E3 RID: 739
		private bool _hasGeneratedDefaultValue;

		// Token: 0x040002E4 RID: 740
		private string _propertyName;

		// Token: 0x040002E5 RID: 741
		internal bool _skipPropertyNameEscape;

		// Token: 0x040002E6 RID: 742
		private Type _propertyType;

		// Token: 0x040002E7 RID: 743
		[CompilerGenerated]
		private JsonContract <PropertyContract>k__BackingField;

		// Token: 0x040002E8 RID: 744
		[CompilerGenerated]
		private Type <DeclaringType>k__BackingField;

		// Token: 0x040002E9 RID: 745
		[CompilerGenerated]
		private int? <Order>k__BackingField;

		// Token: 0x040002EA RID: 746
		[CompilerGenerated]
		private string <UnderlyingName>k__BackingField;

		// Token: 0x040002EB RID: 747
		[CompilerGenerated]
		private IValueProvider <ValueProvider>k__BackingField;

		// Token: 0x040002EC RID: 748
		[CompilerGenerated]
		private IAttributeProvider <AttributeProvider>k__BackingField;

		// Token: 0x040002ED RID: 749
		[CompilerGenerated]
		private JsonConverter <Converter>k__BackingField;

		// Token: 0x040002EE RID: 750
		[CompilerGenerated]
		private JsonConverter <MemberConverter>k__BackingField;

		// Token: 0x040002EF RID: 751
		[CompilerGenerated]
		private bool <Ignored>k__BackingField;

		// Token: 0x040002F0 RID: 752
		[CompilerGenerated]
		private bool <Readable>k__BackingField;

		// Token: 0x040002F1 RID: 753
		[CompilerGenerated]
		private bool <Writable>k__BackingField;

		// Token: 0x040002F2 RID: 754
		[CompilerGenerated]
		private bool <HasMemberAttribute>k__BackingField;

		// Token: 0x040002F3 RID: 755
		[CompilerGenerated]
		private bool? <IsReference>k__BackingField;

		// Token: 0x040002F4 RID: 756
		[CompilerGenerated]
		private NullValueHandling? <NullValueHandling>k__BackingField;

		// Token: 0x040002F5 RID: 757
		[CompilerGenerated]
		private DefaultValueHandling? <DefaultValueHandling>k__BackingField;

		// Token: 0x040002F6 RID: 758
		[CompilerGenerated]
		private ReferenceLoopHandling? <ReferenceLoopHandling>k__BackingField;

		// Token: 0x040002F7 RID: 759
		[CompilerGenerated]
		private ObjectCreationHandling? <ObjectCreationHandling>k__BackingField;

		// Token: 0x040002F8 RID: 760
		[CompilerGenerated]
		private TypeNameHandling? <TypeNameHandling>k__BackingField;

		// Token: 0x040002F9 RID: 761
		[CompilerGenerated]
		private Predicate<object> <ShouldSerialize>k__BackingField;

		// Token: 0x040002FA RID: 762
		[CompilerGenerated]
		private Predicate<object> <ShouldDeserialize>k__BackingField;

		// Token: 0x040002FB RID: 763
		[CompilerGenerated]
		private Predicate<object> <GetIsSpecified>k__BackingField;

		// Token: 0x040002FC RID: 764
		[CompilerGenerated]
		private Action<object, object> <SetIsSpecified>k__BackingField;

		// Token: 0x040002FD RID: 765
		[CompilerGenerated]
		private JsonConverter <ItemConverter>k__BackingField;

		// Token: 0x040002FE RID: 766
		[CompilerGenerated]
		private bool? <ItemIsReference>k__BackingField;

		// Token: 0x040002FF RID: 767
		[CompilerGenerated]
		private TypeNameHandling? <ItemTypeNameHandling>k__BackingField;

		// Token: 0x04000300 RID: 768
		[CompilerGenerated]
		private ReferenceLoopHandling? <ItemReferenceLoopHandling>k__BackingField;
	}
}

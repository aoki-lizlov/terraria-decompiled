using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000098 RID: 152
	public abstract class JsonContract
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001BCED File Offset: 0x00019EED
		public Type UnderlyingType
		{
			[CompilerGenerated]
			get
			{
				return this.<UnderlyingType>k__BackingField;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001BCF5 File Offset: 0x00019EF5
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001BCFD File Offset: 0x00019EFD
		public Type CreatedType
		{
			get
			{
				return this._createdType;
			}
			set
			{
				this._createdType = value;
				this.IsSealed = this._createdType.IsSealed();
				this.IsInstantiable = !this._createdType.IsInterface() && !this._createdType.IsAbstract();
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001BD3B File Offset: 0x00019F3B
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001BD43 File Offset: 0x00019F43
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

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001BD4C File Offset: 0x00019F4C
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0001BD54 File Offset: 0x00019F54
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

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001BD5D File Offset: 0x00019F5D
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x0001BD65 File Offset: 0x00019F65
		internal JsonConverter InternalConverter
		{
			[CompilerGenerated]
			get
			{
				return this.<InternalConverter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InternalConverter>k__BackingField = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001BD6E File Offset: 0x00019F6E
		public IList<SerializationCallback> OnDeserializedCallbacks
		{
			get
			{
				if (this._onDeserializedCallbacks == null)
				{
					this._onDeserializedCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializedCallbacks;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001BD89 File Offset: 0x00019F89
		public IList<SerializationCallback> OnDeserializingCallbacks
		{
			get
			{
				if (this._onDeserializingCallbacks == null)
				{
					this._onDeserializingCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializingCallbacks;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		public IList<SerializationCallback> OnSerializedCallbacks
		{
			get
			{
				if (this._onSerializedCallbacks == null)
				{
					this._onSerializedCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializedCallbacks;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001BDBF File Offset: 0x00019FBF
		public IList<SerializationCallback> OnSerializingCallbacks
		{
			get
			{
				if (this._onSerializingCallbacks == null)
				{
					this._onSerializingCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializingCallbacks;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001BDDA File Offset: 0x00019FDA
		public IList<SerializationErrorCallback> OnErrorCallbacks
		{
			get
			{
				if (this._onErrorCallbacks == null)
				{
					this._onErrorCallbacks = new List<SerializationErrorCallback>();
				}
				return this._onErrorCallbacks;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001BDF5 File Offset: 0x00019FF5
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0001BDFD File Offset: 0x00019FFD
		public Func<object> DefaultCreator
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultCreator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DefaultCreator>k__BackingField = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001BE06 File Offset: 0x0001A006
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x0001BE0E File Offset: 0x0001A00E
		public bool DefaultCreatorNonPublic
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultCreatorNonPublic>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DefaultCreatorNonPublic>k__BackingField = value;
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001BE18 File Offset: 0x0001A018
		internal JsonContract(Type underlyingType)
		{
			ValidationUtils.ArgumentNotNull(underlyingType, "underlyingType");
			this.UnderlyingType = underlyingType;
			this.IsNullable = ReflectionUtils.IsNullable(underlyingType);
			this.NonNullableUnderlyingType = ((this.IsNullable && ReflectionUtils.IsNullableType(underlyingType)) ? Nullable.GetUnderlyingType(underlyingType) : underlyingType);
			this.CreatedType = this.NonNullableUnderlyingType;
			this.IsConvertable = ConvertUtils.IsConvertible(this.NonNullableUnderlyingType);
			this.IsEnum = this.NonNullableUnderlyingType.IsEnum();
			this.InternalReadType = ReadType.Read;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001BEA0 File Offset: 0x0001A0A0
		internal void InvokeOnSerializing(object o, StreamingContext context)
		{
			if (this._onSerializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001BEF4 File Offset: 0x0001A0F4
		internal void InvokeOnSerialized(object o, StreamingContext context)
		{
			if (this._onSerializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001BF48 File Offset: 0x0001A148
		internal void InvokeOnDeserializing(object o, StreamingContext context)
		{
			if (this._onDeserializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001BF9C File Offset: 0x0001A19C
		internal void InvokeOnDeserialized(object o, StreamingContext context)
		{
			if (this._onDeserializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001BFF8 File Offset: 0x0001A1F8
		internal void InvokeOnError(object o, StreamingContext context, ErrorContext errorContext)
		{
			if (this._onErrorCallbacks != null)
			{
				foreach (SerializationErrorCallback serializationErrorCallback in this._onErrorCallbacks)
				{
					serializationErrorCallback(o, context, errorContext);
				}
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001C050 File Offset: 0x0001A250
		internal static SerializationCallback CreateSerializationCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context)
			{
				callbackMethodInfo.Invoke(o, new object[] { context });
			};
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001C069 File Offset: 0x0001A269
		internal static SerializationErrorCallback CreateSerializationErrorCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context, ErrorContext econtext)
			{
				callbackMethodInfo.Invoke(o, new object[] { context, econtext });
			};
		}

		// Token: 0x040002BE RID: 702
		internal bool IsNullable;

		// Token: 0x040002BF RID: 703
		internal bool IsConvertable;

		// Token: 0x040002C0 RID: 704
		internal bool IsEnum;

		// Token: 0x040002C1 RID: 705
		internal Type NonNullableUnderlyingType;

		// Token: 0x040002C2 RID: 706
		internal ReadType InternalReadType;

		// Token: 0x040002C3 RID: 707
		internal JsonContractType ContractType;

		// Token: 0x040002C4 RID: 708
		internal bool IsReadOnlyOrFixedSize;

		// Token: 0x040002C5 RID: 709
		internal bool IsSealed;

		// Token: 0x040002C6 RID: 710
		internal bool IsInstantiable;

		// Token: 0x040002C7 RID: 711
		private List<SerializationCallback> _onDeserializedCallbacks;

		// Token: 0x040002C8 RID: 712
		private IList<SerializationCallback> _onDeserializingCallbacks;

		// Token: 0x040002C9 RID: 713
		private IList<SerializationCallback> _onSerializedCallbacks;

		// Token: 0x040002CA RID: 714
		private IList<SerializationCallback> _onSerializingCallbacks;

		// Token: 0x040002CB RID: 715
		private IList<SerializationErrorCallback> _onErrorCallbacks;

		// Token: 0x040002CC RID: 716
		private Type _createdType;

		// Token: 0x040002CD RID: 717
		[CompilerGenerated]
		private readonly Type <UnderlyingType>k__BackingField;

		// Token: 0x040002CE RID: 718
		[CompilerGenerated]
		private bool? <IsReference>k__BackingField;

		// Token: 0x040002CF RID: 719
		[CompilerGenerated]
		private JsonConverter <Converter>k__BackingField;

		// Token: 0x040002D0 RID: 720
		[CompilerGenerated]
		private JsonConverter <InternalConverter>k__BackingField;

		// Token: 0x040002D1 RID: 721
		[CompilerGenerated]
		private Func<object> <DefaultCreator>k__BackingField;

		// Token: 0x040002D2 RID: 722
		[CompilerGenerated]
		private bool <DefaultCreatorNonPublic>k__BackingField;

		// Token: 0x02000148 RID: 328
		[CompilerGenerated]
		private sealed class <>c__DisplayClass57_0
		{
			// Token: 0x06000CFF RID: 3327 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass57_0()
			{
			}

			// Token: 0x06000D00 RID: 3328 RVA: 0x000315F2 File Offset: 0x0002F7F2
			internal void <CreateSerializationCallback>b__0(object o, StreamingContext context)
			{
				this.callbackMethodInfo.Invoke(o, new object[] { context });
			}

			// Token: 0x040004C0 RID: 1216
			public MethodInfo callbackMethodInfo;
		}

		// Token: 0x02000149 RID: 329
		[CompilerGenerated]
		private sealed class <>c__DisplayClass58_0
		{
			// Token: 0x06000D01 RID: 3329 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass58_0()
			{
			}

			// Token: 0x06000D02 RID: 3330 RVA: 0x00031610 File Offset: 0x0002F810
			internal void <CreateSerializationErrorCallback>b__0(object o, StreamingContext context, ErrorContext econtext)
			{
				this.callbackMethodInfo.Invoke(o, new object[] { context, econtext });
			}

			// Token: 0x040004C1 RID: 1217
			public MethodInfo callbackMethodInfo;
		}
	}
}

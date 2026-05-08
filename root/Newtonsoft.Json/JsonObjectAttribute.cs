using System;

namespace Newtonsoft.Json
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(1036, AllowMultiple = false)]
	public sealed class JsonObjectAttribute : JsonContainerAttribute
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000254E File Offset: 0x0000074E
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002556 File Offset: 0x00000756
		public MemberSerialization MemberSerialization
		{
			get
			{
				return this._memberSerialization;
			}
			set
			{
				this._memberSerialization = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002560 File Offset: 0x00000760
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002586 File Offset: 0x00000786
		public Required ItemRequired
		{
			get
			{
				Required? itemRequired = this._itemRequired;
				if (itemRequired == null)
				{
					return Required.Default;
				}
				return itemRequired.GetValueOrDefault();
			}
			set
			{
				this._itemRequired = new Required?(value);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002050 File Offset: 0x00000250
		public JsonObjectAttribute()
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002594 File Offset: 0x00000794
		public JsonObjectAttribute(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002058 File Offset: 0x00000258
		public JsonObjectAttribute(string id)
			: base(id)
		{
		}

		// Token: 0x0400004F RID: 79
		private MemberSerialization _memberSerialization;

		// Token: 0x04000050 RID: 80
		internal Required? _itemRequired;
	}
}

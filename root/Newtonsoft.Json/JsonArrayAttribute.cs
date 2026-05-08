using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(1028, AllowMultiple = false)]
	public sealed class JsonArrayAttribute : JsonContainerAttribute
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002375 File Offset: 0x00000575
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000237D File Offset: 0x0000057D
		public bool AllowNullItems
		{
			get
			{
				return this._allowNullItems;
			}
			set
			{
				this._allowNullItems = value;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002050 File Offset: 0x00000250
		public JsonArrayAttribute()
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002386 File Offset: 0x00000586
		public JsonArrayAttribute(bool allowNullItems)
		{
			this._allowNullItems = allowNullItems;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002058 File Offset: 0x00000258
		public JsonArrayAttribute(string id)
			: base(id)
		{
		}

		// Token: 0x0400003B RID: 59
		private bool _allowNullItems;
	}
}

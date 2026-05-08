using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000027 RID: 39
	public abstract class JsonConverter
	{
		// Token: 0x0600017D RID: 381
		public abstract void WriteJson(JsonWriter writer, object value, JsonSerializer serializer);

		// Token: 0x0600017E RID: 382
		public abstract object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer);

		// Token: 0x0600017F RID: 383
		public abstract bool CanConvert(Type objectType);

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000071F5 File Offset: 0x000053F5
		public virtual bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000071F5 File Offset: 0x000053F5
		public virtual bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008020 File Offset: 0x00006220
		protected JsonConverter()
		{
		}
	}
}

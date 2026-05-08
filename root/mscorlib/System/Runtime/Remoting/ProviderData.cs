using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x02000539 RID: 1337
	internal class ProviderData
	{
		// Token: 0x060035FC RID: 13820 RVA: 0x000C40E4 File Offset: 0x000C22E4
		public void CopyFrom(ProviderData other)
		{
			if (this.Ref == null)
			{
				this.Ref = other.Ref;
			}
			if (this.Id == null)
			{
				this.Id = other.Id;
			}
			if (this.Type == null)
			{
				this.Type = other.Type;
			}
			foreach (object obj in other.CustomProperties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!this.CustomProperties.ContainsKey(dictionaryEntry.Key))
				{
					this.CustomProperties[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
			if (other.CustomData != null)
			{
				if (this.CustomData == null)
				{
					this.CustomData = new ArrayList();
				}
				foreach (object obj2 in other.CustomData)
				{
					SinkProviderData sinkProviderData = (SinkProviderData)obj2;
					this.CustomData.Add(sinkProviderData);
				}
			}
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000C420C File Offset: 0x000C240C
		public ProviderData()
		{
		}

		// Token: 0x040024CE RID: 9422
		internal string Ref;

		// Token: 0x040024CF RID: 9423
		internal string Type;

		// Token: 0x040024D0 RID: 9424
		internal string Id;

		// Token: 0x040024D1 RID: 9425
		internal Hashtable CustomProperties = new Hashtable();

		// Token: 0x040024D2 RID: 9426
		internal IList CustomData;
	}
}

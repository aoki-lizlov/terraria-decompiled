using System;
using System.Collections;

namespace System.Runtime.Remoting
{
	// Token: 0x02000538 RID: 1336
	internal class ChannelData
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x000C3EAD File Offset: 0x000C20AD
		internal ArrayList ServerProviders
		{
			get
			{
				if (this._serverProviders == null)
				{
					this._serverProviders = new ArrayList();
				}
				return this._serverProviders;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x000C3EC8 File Offset: 0x000C20C8
		public ArrayList ClientProviders
		{
			get
			{
				if (this._clientProviders == null)
				{
					this._clientProviders = new ArrayList();
				}
				return this._clientProviders;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x000C3EE3 File Offset: 0x000C20E3
		public Hashtable CustomProperties
		{
			get
			{
				if (this._customProperties == null)
				{
					this._customProperties = new Hashtable();
				}
				return this._customProperties;
			}
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000C3F00 File Offset: 0x000C2100
		public void CopyFrom(ChannelData other)
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
			if (this.DelayLoadAsClientChannel == null)
			{
				this.DelayLoadAsClientChannel = other.DelayLoadAsClientChannel;
			}
			if (other._customProperties != null)
			{
				foreach (object obj in other._customProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (!this.CustomProperties.ContainsKey(dictionaryEntry.Key))
					{
						this.CustomProperties[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
			}
			if (this._serverProviders == null && other._serverProviders != null)
			{
				foreach (object obj2 in other._serverProviders)
				{
					ProviderData providerData = (ProviderData)obj2;
					ProviderData providerData2 = new ProviderData();
					providerData2.CopyFrom(providerData);
					this.ServerProviders.Add(providerData2);
				}
			}
			if (this._clientProviders == null && other._clientProviders != null)
			{
				foreach (object obj3 in other._clientProviders)
				{
					ProviderData providerData3 = (ProviderData)obj3;
					ProviderData providerData4 = new ProviderData();
					providerData4.CopyFrom(providerData3);
					this.ClientProviders.Add(providerData4);
				}
			}
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000C40B8 File Offset: 0x000C22B8
		public ChannelData()
		{
		}

		// Token: 0x040024C7 RID: 9415
		internal string Ref;

		// Token: 0x040024C8 RID: 9416
		internal string Type;

		// Token: 0x040024C9 RID: 9417
		internal string Id;

		// Token: 0x040024CA RID: 9418
		internal string DelayLoadAsClientChannel;

		// Token: 0x040024CB RID: 9419
		private ArrayList _serverProviders = new ArrayList();

		// Token: 0x040024CC RID: 9420
		private ArrayList _clientProviders = new ArrayList();

		// Token: 0x040024CD RID: 9421
		private Hashtable _customProperties = new Hashtable();
	}
}

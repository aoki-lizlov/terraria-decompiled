using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000578 RID: 1400
	[ComVisible(true)]
	[Serializable]
	public class ChannelDataStore : IChannelDataStore
	{
		// Token: 0x060037C1 RID: 14273 RVA: 0x000C8F3E File Offset: 0x000C713E
		public ChannelDataStore(string[] channelURIs)
		{
			this._channelURIs = channelURIs;
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x000C8F4D File Offset: 0x000C714D
		// (set) Token: 0x060037C3 RID: 14275 RVA: 0x000C8F55 File Offset: 0x000C7155
		public string[] ChannelUris
		{
			get
			{
				return this._channelURIs;
			}
			set
			{
				this._channelURIs = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		public object this[object key]
		{
			get
			{
				if (this._extraData == null)
				{
					return null;
				}
				foreach (DictionaryEntry dictionaryEntry in this._extraData)
				{
					if (dictionaryEntry.Key.Equals(key))
					{
						return dictionaryEntry.Value;
					}
				}
				return null;
			}
			set
			{
				if (this._extraData == null)
				{
					this._extraData = new DictionaryEntry[]
					{
						new DictionaryEntry(key, value)
					};
					return;
				}
				DictionaryEntry[] array = new DictionaryEntry[this._extraData.Length + 1];
				this._extraData.CopyTo(array, 0);
				array[this._extraData.Length] = new DictionaryEntry(key, value);
				this._extraData = array;
			}
		}

		// Token: 0x04002553 RID: 9555
		private string[] _channelURIs;

		// Token: 0x04002554 RID: 9556
		private DictionaryEntry[] _extraData;
	}
}

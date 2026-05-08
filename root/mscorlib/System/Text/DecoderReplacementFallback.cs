using System;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x02000369 RID: 873
	[Serializable]
	public sealed class DecoderReplacementFallback : DecoderFallback, ISerializable
	{
		// Token: 0x0600258E RID: 9614 RVA: 0x000867AC File Offset: 0x000849AC
		public DecoderReplacementFallback()
			: this("?")
		{
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000867BC File Offset: 0x000849BC
		internal DecoderReplacementFallback(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this._strDefault = info.GetString("strDefault");
			}
			catch
			{
				this._strDefault = info.GetString("_strDefault");
			}
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00086808 File Offset: 0x00084A08
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("strDefault", this._strDefault);
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x0008681C File Offset: 0x00084A1C
		public DecoderReplacementFallback(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			bool flag = false;
			for (int i = 0; i < replacement.Length; i++)
			{
				if (char.IsSurrogate(replacement, i))
				{
					if (char.IsHighSurrogate(replacement, i))
					{
						if (flag)
						{
							break;
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							flag = true;
							break;
						}
						flag = false;
					}
				}
				else if (flag)
				{
					break;
				}
			}
			if (flag)
			{
				throw new ArgumentException(SR.Format("String contains invalid Unicode code points.", "replacement"));
			}
			this._strDefault = replacement;
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x00086896 File Offset: 0x00084A96
		public string DefaultString
		{
			get
			{
				return this._strDefault;
			}
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0008689E File Offset: 0x00084A9E
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderReplacementFallbackBuffer(this);
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06002594 RID: 9620 RVA: 0x000868A6 File Offset: 0x00084AA6
		public override int MaxCharCount
		{
			get
			{
				return this._strDefault.Length;
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000868B4 File Offset: 0x00084AB4
		public override bool Equals(object value)
		{
			DecoderReplacementFallback decoderReplacementFallback = value as DecoderReplacementFallback;
			return decoderReplacementFallback != null && this._strDefault == decoderReplacementFallback._strDefault;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000868DE File Offset: 0x00084ADE
		public override int GetHashCode()
		{
			return this._strDefault.GetHashCode();
		}

		// Token: 0x04001C6B RID: 7275
		private string _strDefault;
	}
}

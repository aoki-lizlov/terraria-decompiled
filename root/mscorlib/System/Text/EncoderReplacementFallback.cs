using System;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x02000374 RID: 884
	[Serializable]
	public sealed class EncoderReplacementFallback : EncoderFallback, ISerializable
	{
		// Token: 0x060025EF RID: 9711 RVA: 0x0008792B File Offset: 0x00085B2B
		public EncoderReplacementFallback()
			: this("?")
		{
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x00087938 File Offset: 0x00085B38
		internal EncoderReplacementFallback(SerializationInfo info, StreamingContext context)
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

		// Token: 0x060025F1 RID: 9713 RVA: 0x00087984 File Offset: 0x00085B84
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("strDefault", this._strDefault);
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x00087998 File Offset: 0x00085B98
		public EncoderReplacementFallback(string replacement)
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

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x00087A12 File Offset: 0x00085C12
		public string DefaultString
		{
			get
			{
				return this._strDefault;
			}
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x00087A1A File Offset: 0x00085C1A
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderReplacementFallbackBuffer(this);
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x00087A22 File Offset: 0x00085C22
		public override int MaxCharCount
		{
			get
			{
				return this._strDefault.Length;
			}
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x00087A30 File Offset: 0x00085C30
		public override bool Equals(object value)
		{
			EncoderReplacementFallback encoderReplacementFallback = value as EncoderReplacementFallback;
			return encoderReplacementFallback != null && this._strDefault == encoderReplacementFallback._strDefault;
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x00087A5A File Offset: 0x00085C5A
		public override int GetHashCode()
		{
			return this._strDefault.GetHashCode();
		}

		// Token: 0x04001C8B RID: 7307
		private string _strDefault;
	}
}

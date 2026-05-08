using System;
using System.Runtime.Serialization;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class DecoderException : MP3SharpException
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000488B File Offset: 0x00002A8B
		internal DecoderException(string message, Exception inner)
			: base(message, inner)
		{
			this.InitBlock();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000489B File Offset: 0x00002A9B
		internal DecoderException(int errorcode, Exception inner)
			: this(DecoderException.GetErrorString(errorcode), inner)
		{
			this.InitBlock();
			this._ErrorCode = errorcode;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000048B7 File Offset: 0x00002AB7
		protected DecoderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._ErrorCode = info.GetInt32("ErrorCode");
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000048D2 File Offset: 0x00002AD2
		internal virtual int ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000048DA File Offset: 0x00002ADA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ErrorCode", this._ErrorCode);
			base.GetObjectData(info, context);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004903 File Offset: 0x00002B03
		private void InitBlock()
		{
			this._ErrorCode = 512;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004910 File Offset: 0x00002B10
		internal static string GetErrorString(int errorcode)
		{
			return "Decoder errorcode " + Convert.ToString(errorcode, 16);
		}

		// Token: 0x0400005D RID: 93
		private int _ErrorCode;
	}
}

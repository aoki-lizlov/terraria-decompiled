using System;
using System.Runtime.Serialization;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class BitstreamException : MP3SharpException
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00004068 File Offset: 0x00002268
		internal BitstreamException(string message, Exception inner)
			: base(message, inner)
		{
			this.InitBlock();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004078 File Offset: 0x00002278
		internal BitstreamException(int errorcode, Exception inner)
			: this(BitstreamException.GetErrorString(errorcode), inner)
		{
			this.InitBlock();
			this._ErrorCode = errorcode;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004094 File Offset: 0x00002294
		protected BitstreamException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._ErrorCode = info.GetInt32("ErrorCode");
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000040AF File Offset: 0x000022AF
		internal virtual int ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000040B7 File Offset: 0x000022B7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ErrorCode", this._ErrorCode);
			base.GetObjectData(info, context);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000040E0 File Offset: 0x000022E0
		private void InitBlock()
		{
			this._ErrorCode = 256;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000040ED File Offset: 0x000022ED
		internal static string GetErrorString(int errorcode)
		{
			return "Bitstream errorcode " + Convert.ToString(errorcode, 16);
		}

		// Token: 0x04000049 RID: 73
		private int _ErrorCode;
	}
}

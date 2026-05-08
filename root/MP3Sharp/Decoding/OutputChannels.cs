using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x02000018 RID: 24
	public class OutputChannels
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00012DC9 File Offset: 0x00010FC9
		private OutputChannels(int channels)
		{
			this._OutputChannels = channels;
			if (channels < 0 || channels > 3)
			{
				throw new ArgumentException("channels");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00012DEB File Offset: 0x00010FEB
		internal virtual int ChannelsOutputCode
		{
			get
			{
				return this._OutputChannels;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00012DF3 File Offset: 0x00010FF3
		internal virtual int ChannelCount
		{
			get
			{
				if (this._OutputChannels != 0)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00012E00 File Offset: 0x00011000
		internal static OutputChannels FromInt(int code)
		{
			switch (code)
			{
			case 0:
				return OutputChannels.Both;
			case 1:
				return OutputChannels.Left;
			case 2:
				return OutputChannels.Right;
			case 3:
				return OutputChannels.DownMix;
			default:
				throw new ArgumentException("Invalid channel code: " + code);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00012E54 File Offset: 0x00011054
		public override bool Equals(object obj)
		{
			bool flag = false;
			OutputChannels outputChannels;
			if ((outputChannels = obj as OutputChannels) != null)
			{
				flag = outputChannels._OutputChannels == this._OutputChannels;
			}
			return flag;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00012E7D File Offset: 0x0001107D
		public override int GetHashCode()
		{
			return this._OutputChannels;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00012E85 File Offset: 0x00011085
		// Note: this type is marked as 'beforefieldinit'.
		static OutputChannels()
		{
		}

		// Token: 0x040000A4 RID: 164
		internal const int BOTH_CHANNELS = 0;

		// Token: 0x040000A5 RID: 165
		internal const int LEFT_CHANNEL = 1;

		// Token: 0x040000A6 RID: 166
		internal const int RIGHT_CHANNEL = 2;

		// Token: 0x040000A7 RID: 167
		internal const int DOWNMIX_CHANNELS = 3;

		// Token: 0x040000A8 RID: 168
		internal static readonly OutputChannels Left = new OutputChannels(1);

		// Token: 0x040000A9 RID: 169
		internal static readonly OutputChannels Right = new OutputChannels(2);

		// Token: 0x040000AA RID: 170
		internal static readonly OutputChannels Both = new OutputChannels(0);

		// Token: 0x040000AB RID: 171
		internal static readonly OutputChannels DownMix = new OutputChannels(3);

		// Token: 0x040000AC RID: 172
		private readonly int _OutputChannels;
	}
}

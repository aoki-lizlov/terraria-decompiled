using System;
using System.Runtime.CompilerServices;
using NVorbis.Contracts;

namespace NVorbis
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class NewStreamEventArgs : EventArgs
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00004516 File Offset: 0x00002716
		public NewStreamEventArgs(IStreamDecoder streamDecoder)
		{
			if (streamDecoder == null)
			{
				throw new ArgumentNullException("streamDecoder");
			}
			this.StreamDecoder = streamDecoder;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004534 File Offset: 0x00002734
		public IStreamDecoder StreamDecoder
		{
			[CompilerGenerated]
			get
			{
				return this.<StreamDecoder>k__BackingField;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000453C File Offset: 0x0000273C
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00004544 File Offset: 0x00002744
		public bool IgnoreStream
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreStream>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IgnoreStream>k__BackingField = value;
			}
		}

		// Token: 0x04000047 RID: 71
		[CompilerGenerated]
		private readonly IStreamDecoder <StreamDecoder>k__BackingField;

		// Token: 0x04000048 RID: 72
		[CompilerGenerated]
		private bool <IgnoreStream>k__BackingField;
	}
}

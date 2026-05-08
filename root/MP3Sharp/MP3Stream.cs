using System;
using System.IO;
using System.Runtime.CompilerServices;
using XPT.Core.Audio.MP3Sharp.Decoding;

namespace XPT.Core.Audio.MP3Sharp
{
	// Token: 0x02000004 RID: 4
	public class MP3Stream : Stream
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022B1 File Offset: 0x000004B1
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000022B9 File Offset: 0x000004B9
		public bool IsEOF
		{
			[CompilerGenerated]
			get
			{
				return this.<IsEOF>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsEOF>k__BackingField = value;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022C2 File Offset: 0x000004C2
		public MP3Stream(string fileName)
			: this(new FileStream(fileName, 3, 1))
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022D2 File Offset: 0x000004D2
		public MP3Stream(string fileName, int chunkSize)
			: this(new FileStream(fileName, 3, 1), chunkSize)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022E3 File Offset: 0x000004E3
		public MP3Stream(Stream sourceStream)
			: this(sourceStream, 4096)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022F4 File Offset: 0x000004F4
		public MP3Stream(Stream sourceStream, int chunkSize)
		{
			this.IsEOF = false;
			this._SourceStream = sourceStream;
			this._BitStream = new Bitstream(new PushbackStream(this._SourceStream, chunkSize));
			this._Buffer = new Buffer16BitStereo();
			this._Decoder.OutputBuffer = this._Buffer;
			this.IsEOF |= !this.ReadFrame();
			short channelCountRep = this._ChannelCountRep;
			if (channelCountRep != 1)
			{
				if (channelCountRep != 2)
				{
					throw new MP3SharpException(string.Format("Unhandled channel count rep: {0} (allowed values are 1-mono and 2-stereo).", this._ChannelCountRep));
				}
				this.FormatRep = SoundFormat.Pcm16BitStereo;
			}
			else
			{
				this.FormatRep = SoundFormat.Pcm16BitMono;
			}
			if (this.FormatRep == SoundFormat.Pcm16BitMono)
			{
				this._Buffer.DoubleMonoToStereo = true;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023CE File Offset: 0x000005CE
		internal int ChunkSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023D1 File Offset: 0x000005D1
		public override bool CanRead
		{
			get
			{
				return this._SourceStream.CanRead;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023DE File Offset: 0x000005DE
		public override bool CanSeek
		{
			get
			{
				return this._SourceStream.CanSeek;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023EB File Offset: 0x000005EB
		public override bool CanWrite
		{
			get
			{
				return this._SourceStream.CanWrite;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023F8 File Offset: 0x000005F8
		public override long Length
		{
			get
			{
				return this._SourceStream.Length;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002405 File Offset: 0x00000605
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002414 File Offset: 0x00000614
		public override long Position
		{
			get
			{
				return this._SourceStream.Position;
			}
			set
			{
				if (value < 0L)
				{
					value = 0L;
				}
				if (value > this._SourceStream.Length)
				{
					value = this._SourceStream.Length;
				}
				this._SourceStream.Position = value;
				this.IsEOF = false;
				this.IsEOF |= !this.ReadFrame();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000246E File Offset: 0x0000066E
		public int Frequency
		{
			get
			{
				return this._FrequencyRep;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002476 File Offset: 0x00000676
		internal short ChannelCount
		{
			get
			{
				return this._ChannelCountRep;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000247E File Offset: 0x0000067E
		internal SoundFormat Format
		{
			get
			{
				return this.FormatRep;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002486 File Offset: 0x00000686
		public override void Flush()
		{
			this._SourceStream.Flush();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002493 File Offset: 0x00000693
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._SourceStream.Seek(offset, origin);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024A2 File Offset: 0x000006A2
		public override void SetLength(long value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024A9 File Offset: 0x000006A9
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024B0 File Offset: 0x000006B0
		internal int DecodeFrames(int frameCount)
		{
			int num = 0;
			bool flag = true;
			while (num < frameCount && flag)
			{
				flag = this.ReadFrame();
				if (flag)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024DC File Offset: 0x000006DC
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.IsEOF)
			{
				return 0;
			}
			int num = 0;
			while (this._Buffer.BytesLeft > 0 || this.ReadFrame())
			{
				num += this._Buffer.Read(buffer, offset + num, count - num);
				if (num >= count)
				{
					return num;
				}
			}
			this.IsEOF = true;
			this._BitStream.CloseFrame();
			return num;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002539 File Offset: 0x00000739
		public override void Close()
		{
			this._BitStream.Close();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002548 File Offset: 0x00000748
		private bool ReadFrame()
		{
			Header header = this._BitStream.ReadFrame();
			if (header == null)
			{
				return false;
			}
			try
			{
				if (header.Mode() == 3)
				{
					this._ChannelCountRep = 1;
				}
				else
				{
					this._ChannelCountRep = 2;
				}
				this._FrequencyRep = header.Frequency();
				if (this._Decoder.DecodeFrame(header, this._BitStream) != this._Buffer)
				{
					throw new ApplicationException("Output buffers are different.");
				}
			}
			finally
			{
				this._BitStream.CloseFrame();
			}
			return true;
		}

		// Token: 0x04000007 RID: 7
		private readonly Bitstream _BitStream;

		// Token: 0x04000008 RID: 8
		private readonly Decoder _Decoder = new Decoder(Decoder.DefaultParams);

		// Token: 0x04000009 RID: 9
		private readonly Buffer16BitStereo _Buffer;

		// Token: 0x0400000A RID: 10
		private readonly Stream _SourceStream;

		// Token: 0x0400000B RID: 11
		private const int BACK_STREAM_BYTE_COUNT_REP = 0;

		// Token: 0x0400000C RID: 12
		private short _ChannelCountRep = -1;

		// Token: 0x0400000D RID: 13
		private readonly SoundFormat FormatRep;

		// Token: 0x0400000E RID: 14
		private int _FrequencyRep = -1;

		// Token: 0x0400000F RID: 15
		[CompilerGenerated]
		private bool <IsEOF>k__BackingField;
	}
}

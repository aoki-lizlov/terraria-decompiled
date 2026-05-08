using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using NVorbis.Contracts;

namespace NVorbis
{
	// Token: 0x02000015 RID: 21
	public sealed class StreamDecoder : IStreamDecoder, IDisposable
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004B16 File Offset: 0x00002D16
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00004B1D File Offset: 0x00002D1D
		internal static Func<IFactory> CreateFactory
		{
			[CompilerGenerated]
			get
			{
				return StreamDecoder.<CreateFactory>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				StreamDecoder.<CreateFactory>k__BackingField = value;
			}
		} = () => new Factory();

		// Token: 0x06000091 RID: 145 RVA: 0x00004B25 File Offset: 0x00002D25
		public StreamDecoder(IPacketProvider packetProvider)
			: this(packetProvider, new Factory())
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004B34 File Offset: 0x00002D34
		internal StreamDecoder(IPacketProvider packetProvider, IFactory factory)
		{
			if (packetProvider == null)
			{
				throw new ArgumentNullException("packetProvider");
			}
			this._packetProvider = packetProvider;
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			this._factory = factory;
			this._stats = new StreamStats();
			this._currentPosition = 0L;
			this.ClipSamples = true;
			IPacket packet = this._packetProvider.PeekNextPacket();
			if (!this.ProcessHeaderPackets(packet))
			{
				this._packetProvider = null;
				packet.Reset();
				throw StreamDecoder.GetInvalidStreamException(packet);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004BB8 File Offset: 0x00002DB8
		private static Exception GetInvalidStreamException(IPacket packet)
		{
			Exception ex;
			try
			{
				ulong num = packet.ReadBits(64);
				if (num == 7233173838382854223UL)
				{
					ex = new ArgumentException("Found OPUS bitstream.");
				}
				else if ((num & 255UL) == 127UL)
				{
					ex = new ArgumentException("Found FLAC bitstream.");
				}
				else if (num == 2314885909937746003UL)
				{
					ex = new ArgumentException("Found Speex bitstream.");
				}
				else if (num == 28254585843050854UL)
				{
					ex = new ArgumentException("Found Skeleton metadata bitstream.");
				}
				else if ((num & 72057594037927680UL) == 27428895509214208UL)
				{
					ex = new ArgumentException("Found Theora bitsream.");
				}
				else
				{
					ex = new ArgumentException("Could not find Vorbis data to decode.");
				}
			}
			finally
			{
				packet.Reset();
			}
			return ex;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004C80 File Offset: 0x00002E80
		private bool ProcessHeaderPackets(IPacket packet)
		{
			if (!StreamDecoder.ProcessHeaderPacket(packet, new Func<IPacket, bool>(this.LoadStreamHeader), delegate(IPacket _)
			{
				this._packetProvider.GetNextPacket().Done();
			}))
			{
				return false;
			}
			if (!StreamDecoder.ProcessHeaderPacket(this._packetProvider.GetNextPacket(), new Func<IPacket, bool>(this.LoadComments), delegate(IPacket pkt)
			{
				pkt.Done();
			}))
			{
				return false;
			}
			if (!StreamDecoder.ProcessHeaderPacket(this._packetProvider.GetNextPacket(), new Func<IPacket, bool>(this.LoadBooks), delegate(IPacket pkt)
			{
				pkt.Done();
			}))
			{
				return false;
			}
			this._currentPosition = 0L;
			this.ResetDecoder();
			return true;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004D3C File Offset: 0x00002F3C
		private static bool ProcessHeaderPacket(IPacket packet, Func<IPacket, bool> processAction, Action<IPacket> doneAction)
		{
			if (packet != null)
			{
				try
				{
					return processAction.Invoke(packet);
				}
				finally
				{
					doneAction.Invoke(packet);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004D74 File Offset: 0x00002F74
		private static bool ValidateHeader(IPacket packet, byte[] expected)
		{
			for (int i = 0; i < expected.Length; i++)
			{
				if ((ulong)expected[i] != packet.ReadBits(8))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004DA0 File Offset: 0x00002FA0
		private static string ReadString(IPacket packet)
		{
			int num = (int)packet.ReadBits(32);
			if (num == 0)
			{
				return string.Empty;
			}
			byte[] array = new byte[num];
			if (packet.Read(array, 0, num) < num)
			{
				throw new InvalidDataException("Could not read full string!");
			}
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004DEC File Offset: 0x00002FEC
		private bool LoadStreamHeader(IPacket packet)
		{
			if (!StreamDecoder.ValidateHeader(packet, StreamDecoder.PacketSignatureStream))
			{
				return false;
			}
			this._channels = (byte)packet.ReadBits(8);
			this._sampleRate = (int)packet.ReadBits(32);
			this.UpperBitrate = (int)packet.ReadBits(32);
			this.NominalBitrate = (int)packet.ReadBits(32);
			this.LowerBitrate = (int)packet.ReadBits(32);
			this._block0Size = 1 << (int)packet.ReadBits(4);
			this._block1Size = 1 << (int)packet.ReadBits(4);
			if (this.NominalBitrate == 0 && this.UpperBitrate > 0 && this.LowerBitrate > 0)
			{
				this.NominalBitrate = (this.UpperBitrate + this.LowerBitrate) / 2;
			}
			this._stats.SetSampleRate(this._sampleRate);
			this._stats.AddPacket(-1, packet.BitsRead, packet.BitsRemaining, packet.ContainerOverheadBits);
			return true;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004ED8 File Offset: 0x000030D8
		private bool LoadComments(IPacket packet)
		{
			if (!StreamDecoder.ValidateHeader(packet, StreamDecoder.PacketSignatureComments))
			{
				return false;
			}
			this._vendor = StreamDecoder.ReadString(packet);
			this._comments = new string[packet.ReadBits(32)];
			for (int i = 0; i < this._comments.Length; i++)
			{
				this._comments[i] = StreamDecoder.ReadString(packet);
			}
			this._stats.AddPacket(-1, packet.BitsRead, packet.BitsRemaining, packet.ContainerOverheadBits);
			return true;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004F54 File Offset: 0x00003154
		private bool LoadBooks(IPacket packet)
		{
			if (!StreamDecoder.ValidateHeader(packet, StreamDecoder.PacketSignatureBooks))
			{
				return false;
			}
			IMdct mdct = this._factory.CreateMdct();
			IHuffman huffman = this._factory.CreateHuffman();
			ICodebook[] array = new ICodebook[packet.ReadBits(8) + 1UL];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._factory.CreateCodebook();
				array[i].Init(packet, huffman);
			}
			int num = (int)packet.ReadBits(6) + 1;
			packet.SkipBits(16 * num);
			IFloor[] array2 = new IFloor[packet.ReadBits(6) + 1UL];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = this._factory.CreateFloor(packet);
				array2[j].Init(packet, (int)this._channels, this._block0Size, this._block1Size, array);
			}
			IResidue[] array3 = new IResidue[packet.ReadBits(6) + 1UL];
			for (int k = 0; k < array3.Length; k++)
			{
				array3[k] = this._factory.CreateResidue(packet);
				array3[k].Init(packet, (int)this._channels, array);
			}
			IMapping[] array4 = new IMapping[packet.ReadBits(6) + 1UL];
			for (int l = 0; l < array4.Length; l++)
			{
				array4[l] = this._factory.CreateMapping(packet);
				array4[l].Init(packet, (int)this._channels, array2, array3, mdct);
			}
			this._modes = new IMode[packet.ReadBits(6) + 1UL];
			for (int m = 0; m < this._modes.Length; m++)
			{
				this._modes[m] = this._factory.CreateMode();
				this._modes[m].Init(packet, (int)this._channels, this._block0Size, this._block1Size, array4);
			}
			if (!packet.ReadBit())
			{
				throw new InvalidDataException("Book packet did not end on correct bit!");
			}
			this._modeFieldBits = Utils.ilog(this._modes.Length - 1);
			this._stats.AddPacket(-1, packet.BitsRead, packet.BitsRemaining, packet.ContainerOverheadBits);
			return true;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000516D File Offset: 0x0000336D
		private void ResetDecoder()
		{
			this._prevPacketBuf = null;
			this._prevPacketStart = 0;
			this._prevPacketEnd = 0;
			this._prevPacketStop = 0;
			this._nextPacketBuf = null;
			this._eosFound = false;
			this._hasClipped = false;
			this._hasPosition = false;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000051A8 File Offset: 0x000033A8
		public int Read(float[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count % (int)this._channels != 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be a multiple of Channels!");
			}
			if (this._packetProvider == null)
			{
				throw new ObjectDisposedException("StreamDecoder");
			}
			if (count == 0)
			{
				return 0;
			}
			int i = offset;
			int num = offset + count;
			while (i < num)
			{
				if (this._prevPacketStart == this._prevPacketEnd)
				{
					if (this._eosFound)
					{
						this._nextPacketBuf = null;
						this._prevPacketBuf = null;
						break;
					}
					long? num2;
					if (!this.ReadNextPacket((i - offset) / (int)this._channels, out num2))
					{
						this._prevPacketEnd = this._prevPacketStop;
					}
					if (num2 != null && !this._hasPosition)
					{
						this._hasPosition = true;
						this._currentPosition = num2.Value - (long)(this._prevPacketEnd - this._prevPacketStart) - (long)((i - offset) / (int)this._channels);
					}
				}
				int num3 = Math.Min((num - i) / (int)this._channels, this._prevPacketEnd - this._prevPacketStart);
				if (num3 > 0)
				{
					if (this.ClipSamples)
					{
						i += this.ClippingCopyBuffer(buffer, i, num3);
					}
					else
					{
						i += this.CopyBuffer(buffer, i, num3);
					}
				}
			}
			count = i - offset;
			this._currentPosition += (long)(count / (int)this._channels);
			return count;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005308 File Offset: 0x00003508
		private int ClippingCopyBuffer(float[] target, int targetIndex, int count)
		{
			int num = targetIndex;
			while (count > 0)
			{
				for (int i = 0; i < (int)this._channels; i++)
				{
					target[num++] = Utils.ClipValue(this._prevPacketBuf[i][this._prevPacketStart], ref this._hasClipped);
				}
				this._prevPacketStart++;
				count--;
			}
			return num - targetIndex;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005368 File Offset: 0x00003568
		private int CopyBuffer(float[] target, int targetIndex, int count)
		{
			int num = targetIndex;
			while (count > 0)
			{
				for (int i = 0; i < (int)this._channels; i++)
				{
					target[num++] = this._prevPacketBuf[i][this._prevPacketStart];
				}
				this._prevPacketStart++;
				count--;
			}
			return num - targetIndex;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000053BC File Offset: 0x000035BC
		private bool ReadNextPacket(int bufferedSamples, out long? samplePosition)
		{
			int num;
			int num2;
			int num3;
			bool flag;
			int num4;
			int num5;
			int num6;
			float[][] array = this.DecodeNextPacket(out num, out num2, out num3, out flag, out samplePosition, out num4, out num5, out num6);
			this._eosFound = this._eosFound || flag;
			if (array == null)
			{
				this._stats.AddPacket(0, num4, num5, num6);
				return false;
			}
			if (samplePosition != null && flag)
			{
				long num7 = this._currentPosition + (long)bufferedSamples + (long)num2 - (long)num;
				int num8 = (int)(samplePosition.Value - num7);
				if (num8 < 0)
				{
					num2 += num8;
				}
			}
			if (this._prevPacketEnd > 0)
			{
				StreamDecoder.OverlapBuffers(this._prevPacketBuf, array, this._prevPacketStart, this._prevPacketStop, num, (int)this._channels);
				this._prevPacketStart = num;
			}
			else if (this._prevPacketBuf == null)
			{
				this._prevPacketStart = num2;
			}
			this._stats.AddPacket(num2 - this._prevPacketStart, num4, num5, num6);
			this._nextPacketBuf = this._prevPacketBuf;
			this._prevPacketEnd = num2;
			this._prevPacketStop = num3;
			this._prevPacketBuf = array;
			return true;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000054B4 File Offset: 0x000036B4
		private float[][] DecodeNextPacket(out int packetStartindex, out int packetValidLength, out int packetTotalLength, out bool isEndOfStream, out long? samplePosition, out int bitsRead, out int bitsRemaining, out int containerOverheadBits)
		{
			IPacket packet = null;
			float[][] array;
			try
			{
				if ((packet = this._packetProvider.GetNextPacket()) == null)
				{
					isEndOfStream = true;
				}
				else
				{
					isEndOfStream = packet.IsEndOfStream;
					if (packet.IsResync)
					{
						this._hasPosition = false;
					}
					containerOverheadBits = packet.ContainerOverheadBits;
					if (packet.ReadBit())
					{
						bitsRemaining = packet.BitsRemaining + 1;
					}
					else
					{
						IMode mode = this._modes[(int)packet.ReadBits(this._modeFieldBits)];
						if (this._nextPacketBuf == null)
						{
							this._nextPacketBuf = new float[(int)this._channels][];
							for (int i = 0; i < (int)this._channels; i++)
							{
								this._nextPacketBuf[i] = new float[this._block1Size];
							}
						}
						if (mode.Decode(packet, this._nextPacketBuf, out packetStartindex, out packetValidLength, out packetTotalLength))
						{
							samplePosition = packet.GranulePosition;
							bitsRead = packet.BitsRead;
							bitsRemaining = packet.BitsRemaining;
							return this._nextPacketBuf;
						}
						bitsRemaining = packet.BitsRead + packet.BitsRemaining;
					}
				}
				packetStartindex = 0;
				packetValidLength = 0;
				packetTotalLength = 0;
				samplePosition = default(long?);
				bitsRead = 0;
				bitsRemaining = 0;
				containerOverheadBits = 0;
				array = null;
			}
			finally
			{
				if (packet != null)
				{
					packet.Done();
				}
			}
			return array;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000055F8 File Offset: 0x000037F8
		private static void OverlapBuffers(float[][] previous, float[][] next, int prevStart, int prevLen, int nextStart, int channels)
		{
			while (prevStart < prevLen)
			{
				for (int i = 0; i < channels; i++)
				{
					next[i][nextStart] += previous[i][prevStart];
				}
				prevStart++;
				nextStart++;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005636 File Offset: 0x00003836
		public void SeekTo(TimeSpan timePosition, SeekOrigin seekOrigin = 0)
		{
			this.SeekTo((long)((double)this.SampleRate * timePosition.TotalSeconds), seekOrigin);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005650 File Offset: 0x00003850
		public void SeekTo(long samplePosition, SeekOrigin seekOrigin = 0)
		{
			if (this._packetProvider == null)
			{
				throw new ObjectDisposedException("StreamDecoder");
			}
			if (!this._packetProvider.CanSeek)
			{
				throw new InvalidOperationException("Seek is not supported by the Contracts.IPacketProvider instance.");
			}
			switch (seekOrigin)
			{
			case 0:
				break;
			case 1:
				samplePosition = this.SamplePosition - samplePosition;
				break;
			case 2:
				samplePosition = this.TotalSamples - samplePosition;
				break;
			default:
				throw new ArgumentOutOfRangeException("seekOrigin");
			}
			if (samplePosition < 0L)
			{
				throw new ArgumentOutOfRangeException("samplePosition");
			}
			int num;
			if (samplePosition == 0L)
			{
				this._packetProvider.SeekTo(0L, 0, new GetPacketGranuleCount(this.GetPacketGranules));
				num = 0;
			}
			else
			{
				long num2 = this._packetProvider.SeekTo(samplePosition, 1, new GetPacketGranuleCount(this.GetPacketGranules));
				num = (int)(samplePosition - num2);
			}
			this.ResetDecoder();
			this._hasPosition = true;
			long? num3;
			if (!this.ReadNextPacket(0, out num3))
			{
				this._eosFound = true;
				if (this._packetProvider.GetGranuleCount() != samplePosition)
				{
					this.SeekTo(samplePosition, seekOrigin);
					return;
				}
				this._prevPacketStart = this._prevPacketStop;
				this._currentPosition = samplePosition;
				return;
			}
			else
			{
				if (!this.ReadNextPacket(0, out num3))
				{
					this.ResetDecoder();
					this._eosFound = true;
					this.SeekTo(samplePosition, seekOrigin);
					return;
				}
				this._prevPacketStart += num;
				this._currentPosition = samplePosition;
				return;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005790 File Offset: 0x00003990
		private int GetPacketGranules(IPacket curPacket, bool isLastInPage)
		{
			if (curPacket.IsResync)
			{
				return 0;
			}
			if (curPacket.ReadBit())
			{
				return 0;
			}
			int num = (int)curPacket.ReadBits(this._modeFieldBits);
			if (num < 0 || num >= this._modes.Length)
			{
				return 0;
			}
			return this._modes[num].GetPacketSampleCount(curPacket, isLastInPage);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000057DF File Offset: 0x000039DF
		public void Dispose()
		{
			IDisposable disposable = this._packetProvider as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			this._packetProvider = null;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000057FE File Offset: 0x000039FE
		public int Channels
		{
			get
			{
				return (int)this._channels;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00005806 File Offset: 0x00003A06
		public int SampleRate
		{
			get
			{
				return this._sampleRate;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000580E File Offset: 0x00003A0E
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00005816 File Offset: 0x00003A16
		public int UpperBitrate
		{
			[CompilerGenerated]
			get
			{
				return this.<UpperBitrate>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<UpperBitrate>k__BackingField = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000581F File Offset: 0x00003A1F
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00005827 File Offset: 0x00003A27
		public int NominalBitrate
		{
			[CompilerGenerated]
			get
			{
				return this.<NominalBitrate>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NominalBitrate>k__BackingField = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00005830 File Offset: 0x00003A30
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00005838 File Offset: 0x00003A38
		public int LowerBitrate
		{
			[CompilerGenerated]
			get
			{
				return this.<LowerBitrate>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<LowerBitrate>k__BackingField = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005844 File Offset: 0x00003A44
		public ITagData Tags
		{
			get
			{
				ITagData tagData;
				if ((tagData = this._tags) == null)
				{
					tagData = (this._tags = new TagData(this._vendor, this._comments));
				}
				return tagData;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005875 File Offset: 0x00003A75
		public TimeSpan TotalTime
		{
			get
			{
				return TimeSpan.FromSeconds((double)this.TotalSamples / (double)this._sampleRate);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000588B File Offset: 0x00003A8B
		public long TotalSamples
		{
			get
			{
				IPacketProvider packetProvider = this._packetProvider;
				if (packetProvider == null)
				{
					throw new ObjectDisposedException("StreamDecoder");
				}
				return packetProvider.GetGranuleCount();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000058A7 File Offset: 0x00003AA7
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000058BD File Offset: 0x00003ABD
		public TimeSpan TimePosition
		{
			get
			{
				return TimeSpan.FromSeconds((double)this._currentPosition / (double)this._sampleRate);
			}
			set
			{
				this.SeekTo(value, 0);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000058C7 File Offset: 0x00003AC7
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x000058CF File Offset: 0x00003ACF
		public long SamplePosition
		{
			get
			{
				return this._currentPosition;
			}
			set
			{
				this.SeekTo(value, 0);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000058D9 File Offset: 0x00003AD9
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000058E1 File Offset: 0x00003AE1
		public bool ClipSamples
		{
			[CompilerGenerated]
			get
			{
				return this.<ClipSamples>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ClipSamples>k__BackingField = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000058EA File Offset: 0x00003AEA
		public bool HasClipped
		{
			get
			{
				return this._hasClipped;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000058F2 File Offset: 0x00003AF2
		public bool IsEndOfStream
		{
			get
			{
				return this._eosFound && this._prevPacketBuf == null;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005907 File Offset: 0x00003B07
		public IStreamStats Stats
		{
			get
			{
				return this._stats;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005910 File Offset: 0x00003B10
		// Note: this type is marked as 'beforefieldinit'.
		static StreamDecoder()
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005975 File Offset: 0x00003B75
		[CompilerGenerated]
		private void <ProcessHeaderPackets>b__28_0(IPacket _)
		{
			this._packetProvider.GetNextPacket().Done();
		}

		// Token: 0x04000054 RID: 84
		[CompilerGenerated]
		private static Func<IFactory> <CreateFactory>k__BackingField;

		// Token: 0x04000055 RID: 85
		private IPacketProvider _packetProvider;

		// Token: 0x04000056 RID: 86
		private IFactory _factory;

		// Token: 0x04000057 RID: 87
		private StreamStats _stats;

		// Token: 0x04000058 RID: 88
		private byte _channels;

		// Token: 0x04000059 RID: 89
		private int _sampleRate;

		// Token: 0x0400005A RID: 90
		private int _block0Size;

		// Token: 0x0400005B RID: 91
		private int _block1Size;

		// Token: 0x0400005C RID: 92
		private IMode[] _modes;

		// Token: 0x0400005D RID: 93
		private int _modeFieldBits;

		// Token: 0x0400005E RID: 94
		private string _vendor;

		// Token: 0x0400005F RID: 95
		private string[] _comments;

		// Token: 0x04000060 RID: 96
		private ITagData _tags;

		// Token: 0x04000061 RID: 97
		private long _currentPosition;

		// Token: 0x04000062 RID: 98
		private bool _hasClipped;

		// Token: 0x04000063 RID: 99
		private bool _hasPosition;

		// Token: 0x04000064 RID: 100
		private bool _eosFound;

		// Token: 0x04000065 RID: 101
		private float[][] _nextPacketBuf;

		// Token: 0x04000066 RID: 102
		private float[][] _prevPacketBuf;

		// Token: 0x04000067 RID: 103
		private int _prevPacketStart;

		// Token: 0x04000068 RID: 104
		private int _prevPacketEnd;

		// Token: 0x04000069 RID: 105
		private int _prevPacketStop;

		// Token: 0x0400006A RID: 106
		private static readonly byte[] PacketSignatureStream = new byte[]
		{
			1, 118, 111, 114, 98, 105, 115, 0, 0, 0,
			0
		};

		// Token: 0x0400006B RID: 107
		private static readonly byte[] PacketSignatureComments = new byte[] { 3, 118, 111, 114, 98, 105, 115 };

		// Token: 0x0400006C RID: 108
		private static readonly byte[] PacketSignatureBooks = new byte[] { 5, 118, 111, 114, 98, 105, 115 };

		// Token: 0x0400006D RID: 109
		[CompilerGenerated]
		private int <UpperBitrate>k__BackingField;

		// Token: 0x0400006E RID: 110
		[CompilerGenerated]
		private int <NominalBitrate>k__BackingField;

		// Token: 0x0400006F RID: 111
		[CompilerGenerated]
		private int <LowerBitrate>k__BackingField;

		// Token: 0x04000070 RID: 112
		[CompilerGenerated]
		private bool <ClipSamples>k__BackingField;

		// Token: 0x02000044 RID: 68
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600027C RID: 636 RVA: 0x00009B9F File Offset: 0x00007D9F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600027D RID: 637 RVA: 0x00009BAB File Offset: 0x00007DAB
			public <>c()
			{
			}

			// Token: 0x0600027E RID: 638 RVA: 0x00009BB3 File Offset: 0x00007DB3
			internal void <ProcessHeaderPackets>b__28_1(IPacket pkt)
			{
				pkt.Done();
			}

			// Token: 0x0600027F RID: 639 RVA: 0x00009BBB File Offset: 0x00007DBB
			internal void <ProcessHeaderPackets>b__28_2(IPacket pkt)
			{
				pkt.Done();
			}

			// Token: 0x06000280 RID: 640 RVA: 0x00009BC3 File Offset: 0x00007DC3
			internal IFactory <.cctor>b__87_0()
			{
				return new Factory();
			}

			// Token: 0x04000105 RID: 261
			public static readonly StreamDecoder.<>c <>9 = new StreamDecoder.<>c();

			// Token: 0x04000106 RID: 262
			public static Action<IPacket> <>9__28_1;

			// Token: 0x04000107 RID: 263
			public static Action<IPacket> <>9__28_2;
		}
	}
}

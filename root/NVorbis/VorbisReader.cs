using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using NVorbis.Contracts;
using NVorbis.Ogg;

namespace NVorbis
{
	// Token: 0x02000019 RID: 25
	public sealed class VorbisReader : IVorbisReader, IDisposable
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005FE2 File Offset: 0x000041E2
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00005FE9 File Offset: 0x000041E9
		internal static Func<Stream, bool, IContainerReader> CreateContainerReader
		{
			[CompilerGenerated]
			get
			{
				return VorbisReader.<CreateContainerReader>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				VorbisReader.<CreateContainerReader>k__BackingField = value;
			}
		} = (Stream s, bool cod) => new ContainerReader(s, cod);

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00005FF1 File Offset: 0x000041F1
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00005FF8 File Offset: 0x000041F8
		internal static Func<IPacketProvider, IStreamDecoder> CreateStreamDecoder
		{
			[CompilerGenerated]
			get
			{
				return VorbisReader.<CreateStreamDecoder>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				VorbisReader.<CreateStreamDecoder>k__BackingField = value;
			}
		} = (IPacketProvider pp) => new StreamDecoder(pp, new Factory());

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000E5 RID: 229 RVA: 0x00006000 File Offset: 0x00004200
		// (remove) Token: 0x060000E6 RID: 230 RVA: 0x00006038 File Offset: 0x00004238
		public event EventHandler<NewStreamEventArgs> NewStream
		{
			[CompilerGenerated]
			add
			{
				EventHandler<NewStreamEventArgs> eventHandler = this.NewStream;
				EventHandler<NewStreamEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<NewStreamEventArgs> eventHandler3 = (EventHandler<NewStreamEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<NewStreamEventArgs>>(ref this.NewStream, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<NewStreamEventArgs> eventHandler = this.NewStream;
				EventHandler<NewStreamEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<NewStreamEventArgs> eventHandler3 = (EventHandler<NewStreamEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<NewStreamEventArgs>>(ref this.NewStream, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000606D File Offset: 0x0000426D
		public VorbisReader(string fileName)
			: this(File.OpenRead(fileName), true)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000607C File Offset: 0x0000427C
		public VorbisReader(Stream stream, bool closeOnDispose = true)
		{
			this._decoders = new List<IStreamDecoder>();
			IContainerReader containerReader = VorbisReader.CreateContainerReader.Invoke(stream, closeOnDispose);
			containerReader.NewStreamCallback = new NewStreamHandler(this.ProcessNewStream);
			if (!containerReader.TryInit() || this._decoders.Count == 0)
			{
				containerReader.NewStreamCallback = null;
				containerReader.Dispose();
				if (closeOnDispose)
				{
					stream.Dispose();
				}
				throw new ArgumentException("Could not load the specified container!", "containerReader");
			}
			this._closeOnDispose = closeOnDispose;
			this._containerReader = containerReader;
			this._streamDecoder = this._decoders[0];
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006114 File Offset: 0x00004314
		[Obsolete("Use \"new StreamDecoder(Contracts.IPacketProvider)\" and the container's NewStreamCallback or Streams property instead.", true)]
		public VorbisReader(IContainerReader containerReader)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006121 File Offset: 0x00004321
		[Obsolete("Use \"new StreamDecoder(Contracts.IPacketProvider)\" instead.", true)]
		public VorbisReader(IPacketProvider packetProvider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006130 File Offset: 0x00004330
		private bool ProcessNewStream(IPacketProvider packetProvider)
		{
			IStreamDecoder streamDecoder = VorbisReader.CreateStreamDecoder.Invoke(packetProvider);
			streamDecoder.ClipSamples = true;
			NewStreamEventArgs newStreamEventArgs = new NewStreamEventArgs(streamDecoder);
			EventHandler<NewStreamEventArgs> newStream = this.NewStream;
			if (newStream != null)
			{
				newStream.Invoke(this, newStreamEventArgs);
			}
			if (!newStreamEventArgs.IgnoreStream)
			{
				this._decoders.Add(streamDecoder);
				return true;
			}
			return false;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006184 File Offset: 0x00004384
		public void Dispose()
		{
			if (this._decoders != null)
			{
				foreach (IStreamDecoder streamDecoder in this._decoders)
				{
					streamDecoder.Dispose();
				}
				this._decoders.Clear();
			}
			if (this._containerReader != null)
			{
				this._containerReader.NewStreamCallback = null;
				if (this._closeOnDispose)
				{
					this._containerReader.Dispose();
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00006210 File Offset: 0x00004410
		public IList<IStreamDecoder> Streams
		{
			get
			{
				return this._decoders;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00006218 File Offset: 0x00004418
		public int Channels
		{
			get
			{
				return this._streamDecoder.Channels;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00006225 File Offset: 0x00004425
		public int SampleRate
		{
			get
			{
				return this._streamDecoder.SampleRate;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00006232 File Offset: 0x00004432
		public int UpperBitrate
		{
			get
			{
				return this._streamDecoder.UpperBitrate;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000623F File Offset: 0x0000443F
		public int NominalBitrate
		{
			get
			{
				return this._streamDecoder.NominalBitrate;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000624C File Offset: 0x0000444C
		public int LowerBitrate
		{
			get
			{
				return this._streamDecoder.LowerBitrate;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006259 File Offset: 0x00004459
		public ITagData Tags
		{
			get
			{
				return this._streamDecoder.Tags;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006266 File Offset: 0x00004466
		[Obsolete("Use .Tags.EncoderVendor instead.")]
		public string Vendor
		{
			get
			{
				return this._streamDecoder.Tags.EncoderVendor;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006278 File Offset: 0x00004478
		[Obsolete("Use .Tags.All instead.")]
		public string[] Comments
		{
			get
			{
				return Enumerable.ToArray<string>(Enumerable.SelectMany<KeyValuePair<string, IList<string>>, string, string>(this._streamDecoder.Tags.All, (KeyValuePair<string, IList<string>> k) => k.Value, (KeyValuePair<string, IList<string>> kvp, string Item) => kvp.Key + "=" + Item));
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000062DD File Offset: 0x000044DD
		[Obsolete("No longer supported.  Will receive a new stream when parameters change.", true)]
		public bool IsParameterChange
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000062E4 File Offset: 0x000044E4
		public long ContainerOverheadBits
		{
			get
			{
				IContainerReader containerReader = this._containerReader;
				if (containerReader == null)
				{
					return 0L;
				}
				return containerReader.ContainerBits;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000062F8 File Offset: 0x000044F8
		public long ContainerWasteBits
		{
			get
			{
				IContainerReader containerReader = this._containerReader;
				if (containerReader == null)
				{
					return 0L;
				}
				return containerReader.WasteBits;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000630C File Offset: 0x0000450C
		public int StreamIndex
		{
			get
			{
				return this._decoders.IndexOf(this._streamDecoder);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000631F File Offset: 0x0000451F
		[Obsolete("Use .Streams.Count instead.")]
		public int StreamCount
		{
			get
			{
				return this._decoders.Count;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000632C File Offset: 0x0000452C
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00006339 File Offset: 0x00004539
		[Obsolete("Use VorbisReader.TimePosition instead.")]
		public TimeSpan DecodedTime
		{
			get
			{
				return this._streamDecoder.TimePosition;
			}
			set
			{
				this.TimePosition = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006342 File Offset: 0x00004542
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000634F File Offset: 0x0000454F
		[Obsolete("Use VorbisReader.SamplePosition instead.")]
		public long DecodedPosition
		{
			get
			{
				return this._streamDecoder.SamplePosition;
			}
			set
			{
				this.SamplePosition = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006358 File Offset: 0x00004558
		public TimeSpan TotalTime
		{
			get
			{
				return this._streamDecoder.TotalTime;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00006365 File Offset: 0x00004565
		public long TotalSamples
		{
			get
			{
				return this._streamDecoder.TotalSamples;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006372 File Offset: 0x00004572
		// (set) Token: 0x06000102 RID: 258 RVA: 0x0000637F File Offset: 0x0000457F
		public TimeSpan TimePosition
		{
			get
			{
				return this._streamDecoder.TimePosition;
			}
			set
			{
				this._streamDecoder.TimePosition = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000638D File Offset: 0x0000458D
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000639A File Offset: 0x0000459A
		public long SamplePosition
		{
			get
			{
				return this._streamDecoder.SamplePosition;
			}
			set
			{
				this._streamDecoder.SamplePosition = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000063A8 File Offset: 0x000045A8
		public bool IsEndOfStream
		{
			get
			{
				return this._streamDecoder.IsEndOfStream;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000063B5 File Offset: 0x000045B5
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000063C2 File Offset: 0x000045C2
		public bool ClipSamples
		{
			get
			{
				return this._streamDecoder.ClipSamples;
			}
			set
			{
				this._streamDecoder.ClipSamples = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000063D0 File Offset: 0x000045D0
		public bool HasClipped
		{
			get
			{
				return this._streamDecoder.HasClipped;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000063DD File Offset: 0x000045DD
		public IStreamStats StreamStats
		{
			get
			{
				return this._streamDecoder.Stats;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000063EA File Offset: 0x000045EA
		[Obsolete("Use Streams[*].Stats instead.", true)]
		public IVorbisStreamStatus[] Stats
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000063F1 File Offset: 0x000045F1
		public bool FindNextStream()
		{
			return this._containerReader != null && this._containerReader.FindNextStream();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006408 File Offset: 0x00004608
		public bool SwitchStreams(int index)
		{
			if (index < 0 || index >= this._decoders.Count)
			{
				throw new ArgumentOutOfRangeException(string.Format("Class: {0}, Index: {1} /  {2}", "VorbisReader", index, this._decoders.Count));
			}
			IStreamDecoder streamDecoder = this._decoders[index];
			IStreamDecoder streamDecoder2 = this._streamDecoder;
			if (streamDecoder == streamDecoder2)
			{
				return false;
			}
			streamDecoder.ClipSamples = streamDecoder2.ClipSamples;
			this._streamDecoder = streamDecoder;
			return streamDecoder.Channels != streamDecoder2.Channels || streamDecoder.SampleRate != streamDecoder2.SampleRate;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000064A0 File Offset: 0x000046A0
		public void SeekTo(TimeSpan timePosition, SeekOrigin seekOrigin = 0)
		{
			this._streamDecoder.SeekTo(timePosition, seekOrigin);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000064AF File Offset: 0x000046AF
		public void SeekTo(long samplePosition, SeekOrigin seekOrigin = 0)
		{
			this._streamDecoder.SeekTo(samplePosition, seekOrigin);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000064BE File Offset: 0x000046BE
		public int ReadSamples(float[] buffer, int offset, int count)
		{
			count -= count % this._streamDecoder.Channels;
			if (count > 0)
			{
				return this._streamDecoder.Read(buffer, offset, count);
			}
			return 0;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000064E5 File Offset: 0x000046E5
		[Obsolete("No longer needed.", true)]
		public void ClearParameterChange()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000064EC File Offset: 0x000046EC
		// Note: this type is marked as 'beforefieldinit'.
		static VorbisReader()
		{
		}

		// Token: 0x0400007F RID: 127
		[CompilerGenerated]
		private static Func<Stream, bool, IContainerReader> <CreateContainerReader>k__BackingField;

		// Token: 0x04000080 RID: 128
		[CompilerGenerated]
		private static Func<IPacketProvider, IStreamDecoder> <CreateStreamDecoder>k__BackingField;

		// Token: 0x04000081 RID: 129
		private readonly List<IStreamDecoder> _decoders;

		// Token: 0x04000082 RID: 130
		private readonly IContainerReader _containerReader;

		// Token: 0x04000083 RID: 131
		private readonly bool _closeOnDispose;

		// Token: 0x04000084 RID: 132
		private IStreamDecoder _streamDecoder;

		// Token: 0x04000085 RID: 133
		[CompilerGenerated]
		private EventHandler<NewStreamEventArgs> NewStream;

		// Token: 0x02000045 RID: 69
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000281 RID: 641 RVA: 0x00009BCA File Offset: 0x00007DCA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000282 RID: 642 RVA: 0x00009BD6 File Offset: 0x00007DD6
			public <>c()
			{
			}

			// Token: 0x06000283 RID: 643 RVA: 0x00009BDE File Offset: 0x00007DDE
			internal IEnumerable<string> <get_Comments>b__38_0(KeyValuePair<string, IList<string>> k)
			{
				return k.Value;
			}

			// Token: 0x06000284 RID: 644 RVA: 0x00009BE7 File Offset: 0x00007DE7
			internal string <get_Comments>b__38_1(KeyValuePair<string, IList<string>> kvp, string Item)
			{
				return kvp.Key + "=" + Item;
			}

			// Token: 0x06000285 RID: 645 RVA: 0x00009BFB File Offset: 0x00007DFB
			internal IContainerReader <.cctor>b__82_0(Stream s, bool cod)
			{
				return new ContainerReader(s, cod);
			}

			// Token: 0x06000286 RID: 646 RVA: 0x00009C04 File Offset: 0x00007E04
			internal IStreamDecoder <.cctor>b__82_1(IPacketProvider pp)
			{
				return new StreamDecoder(pp, new Factory());
			}

			// Token: 0x04000108 RID: 264
			public static readonly VorbisReader.<>c <>9 = new VorbisReader.<>c();

			// Token: 0x04000109 RID: 265
			public static Func<KeyValuePair<string, IList<string>>, IEnumerable<string>> <>9__38_0;

			// Token: 0x0400010A RID: 266
			public static Func<KeyValuePair<string, IList<string>>, string, string> <>9__38_1;
		}
	}
}

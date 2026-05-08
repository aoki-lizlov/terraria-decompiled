using System;
using XPT.Core.Audio.MP3Sharp.Decoding.Decoders;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x02000012 RID: 18
	public class Decoder
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000045DA File Offset: 0x000027DA
		internal Decoder()
			: this(null)
		{
			this.InitBlock();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000045EC File Offset: 0x000027EC
		internal Decoder(Decoder.Params params0)
		{
			this.InitBlock();
			if (params0 == null)
			{
				params0 = Decoder.DecoderDefaultParams;
			}
			Equalizer initialEqualizerSettings = params0.InitialEqualizerSettings;
			if (initialEqualizerSettings != null)
			{
				this._Equalizer.FromEqualizer = initialEqualizerSettings;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004625 File Offset: 0x00002825
		internal static Decoder.Params DefaultParams
		{
			get
			{
				return (Decoder.Params)Decoder.DecoderDefaultParams.Clone();
			}
		}

		// Token: 0x17000011 RID: 17
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00004638 File Offset: 0x00002838
		internal virtual Equalizer Equalizer
		{
			set
			{
				if (value == null)
				{
					value = Equalizer.PassThruEq;
				}
				this._Equalizer.FromEqualizer = value;
				float[] bandFactors = this._Equalizer.BandFactors;
				if (this._LeftChannelFilter != null)
				{
					this._LeftChannelFilter.Eq = bandFactors;
				}
				if (this._RightChannelFilter != null)
				{
					this._RightChannelFilter.Eq = bandFactors;
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000468F File Offset: 0x0000288F
		internal virtual ABuffer OutputBuffer
		{
			set
			{
				this._Output = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004698 File Offset: 0x00002898
		internal virtual int OutputFrequency
		{
			get
			{
				return this._OutputFrequency;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000046A0 File Offset: 0x000028A0
		internal virtual int OutputChannels
		{
			get
			{
				return this._OutputChannels;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000046A8 File Offset: 0x000028A8
		internal virtual int OutputBlockSize
		{
			get
			{
				return 2304;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000046AF File Offset: 0x000028AF
		private void InitBlock()
		{
			this._Equalizer = new Equalizer();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000046BC File Offset: 0x000028BC
		internal virtual ABuffer DecodeFrame(Header header, Bitstream stream)
		{
			if (!this._IsInitialized)
			{
				this.Initialize(header);
			}
			int num = header.Layer();
			this._Output.ClearBuffer();
			this.RetrieveDecoder(header, stream, num).DecodeFrame();
			this._Output.WriteBuffer(1);
			return this._Output;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000470A File Offset: 0x0000290A
		protected virtual DecoderException NewDecoderException(int errorcode)
		{
			return new DecoderException(errorcode, null);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004713 File Offset: 0x00002913
		protected virtual DecoderException NewDecoderException(int errorcode, Exception throwable)
		{
			return new DecoderException(errorcode, throwable);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000471C File Offset: 0x0000291C
		protected virtual IFrameDecoder RetrieveDecoder(Header header, Bitstream stream, int layer)
		{
			IFrameDecoder frameDecoder = null;
			switch (layer)
			{
			case 1:
				if (this._L1Decoder == null)
				{
					this._L1Decoder = new LayerIDecoder();
					this._L1Decoder.Create(stream, header, this._LeftChannelFilter, this._RightChannelFilter, this._Output, 0);
				}
				frameDecoder = this._L1Decoder;
				break;
			case 2:
				if (this._L2Decoder == null)
				{
					this._L2Decoder = new LayerIIDecoder();
					this._L2Decoder.Create(stream, header, this._LeftChannelFilter, this._RightChannelFilter, this._Output, 0);
				}
				frameDecoder = this._L2Decoder;
				break;
			case 3:
				if (this._L3Decoder == null)
				{
					this._L3Decoder = new LayerIIIDecoder(stream, header, this._LeftChannelFilter, this._RightChannelFilter, this._Output, 0);
				}
				frameDecoder = this._L3Decoder;
				break;
			}
			if (frameDecoder == null)
			{
				throw this.NewDecoderException(513, null);
			}
			return frameDecoder;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000047FC File Offset: 0x000029FC
		private void Initialize(Header header)
		{
			int num = ((header.Mode() == 3) ? 1 : 2);
			if (this._Output == null)
			{
				this._Output = new SampleBuffer(header.Frequency(), num);
			}
			float[] bandFactors = this._Equalizer.BandFactors;
			this._LeftChannelFilter = new SynthesisFilter(0, 32700f, bandFactors);
			if (num == 2)
			{
				this._RightChannelFilter = new SynthesisFilter(1, 32700f, bandFactors);
			}
			this._OutputChannels = num;
			this._OutputFrequency = header.Frequency();
			this._IsInitialized = true;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000487F File Offset: 0x00002A7F
		// Note: this type is marked as 'beforefieldinit'.
		static Decoder()
		{
		}

		// Token: 0x04000050 RID: 80
		private static readonly Decoder.Params DecoderDefaultParams = new Decoder.Params();

		// Token: 0x04000051 RID: 81
		private Equalizer _Equalizer;

		// Token: 0x04000052 RID: 82
		private SynthesisFilter _LeftChannelFilter;

		// Token: 0x04000053 RID: 83
		private SynthesisFilter _RightChannelFilter;

		// Token: 0x04000054 RID: 84
		private bool _IsInitialized;

		// Token: 0x04000055 RID: 85
		private LayerIDecoder _L1Decoder;

		// Token: 0x04000056 RID: 86
		private LayerIIDecoder _L2Decoder;

		// Token: 0x04000057 RID: 87
		private LayerIIIDecoder _L3Decoder;

		// Token: 0x04000058 RID: 88
		private ABuffer _Output;

		// Token: 0x04000059 RID: 89
		private int _OutputChannels;

		// Token: 0x0400005A RID: 90
		private int _OutputFrequency;

		// Token: 0x02000033 RID: 51
		public class Params : ICloneable
		{
			// Token: 0x17000029 RID: 41
			// (get) Token: 0x06000176 RID: 374 RVA: 0x0001B74C File Offset: 0x0001994C
			// (set) Token: 0x06000177 RID: 375 RVA: 0x0001B754 File Offset: 0x00019954
			internal virtual OutputChannels OutputChannels
			{
				get
				{
					return this._OutputChannels;
				}
				set
				{
					if (value == null)
					{
						throw new NullReferenceException("out");
					}
					this._OutputChannels = value;
				}
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x06000178 RID: 376 RVA: 0x0001B76C File Offset: 0x0001996C
			internal virtual Equalizer InitialEqualizerSettings
			{
				get
				{
					return this._Equalizer;
				}
			}

			// Token: 0x06000179 RID: 377 RVA: 0x0001B774 File Offset: 0x00019974
			public object Clone()
			{
				object obj;
				try
				{
					obj = base.MemberwiseClone();
				}
				catch (Exception ex)
				{
					throw new ApplicationException(this + ": " + ex);
				}
				return obj;
			}

			// Token: 0x0600017A RID: 378 RVA: 0x0001B7B0 File Offset: 0x000199B0
			public Params()
			{
			}

			// Token: 0x040001E6 RID: 486
			private OutputChannels _OutputChannels;

			// Token: 0x040001E7 RID: 487
			private readonly Equalizer _Equalizer;
		}
	}
}

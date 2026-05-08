using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using NVorbis.Contracts;
using NVorbis.Contracts.Ogg;

namespace NVorbis.Ogg
{
	// Token: 0x0200001A RID: 26
	public sealed class ContainerReader : IContainerReader, IDisposable
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006518 File Offset: 0x00004718
		// (set) Token: 0x06000113 RID: 275 RVA: 0x0000651F File Offset: 0x0000471F
		internal static Func<Stream, bool, Func<IPacketProvider, bool>, IPageReader> CreatePageReader
		{
			[CompilerGenerated]
			get
			{
				return ContainerReader.<CreatePageReader>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				ContainerReader.<CreatePageReader>k__BackingField = value;
			}
		} = (Stream s, bool cod, Func<IPacketProvider, bool> cb) => new PageReader(s, cod, cb);

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006527 File Offset: 0x00004727
		// (set) Token: 0x06000115 RID: 277 RVA: 0x0000652E File Offset: 0x0000472E
		internal static Func<Stream, bool, Func<IPacketProvider, bool>, IPageReader> CreateForwardOnlyPageReader
		{
			[CompilerGenerated]
			get
			{
				return ContainerReader.<CreateForwardOnlyPageReader>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				ContainerReader.<CreateForwardOnlyPageReader>k__BackingField = value;
			}
		} = (Stream s, bool cod, Func<IPacketProvider, bool> cb) => new ForwardOnlyPageReader(s, cod, cb);

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006536 File Offset: 0x00004736
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000653E File Offset: 0x0000473E
		public NewStreamHandler NewStreamCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<NewStreamCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NewStreamCallback>k__BackingField = value;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006548 File Offset: 0x00004748
		public IList<IPacketProvider> GetStreams()
		{
			List<IPacketProvider> list = new List<IPacketProvider>(this._packetProviders.Count);
			for (int i = 0; i < this._packetProviders.Count; i++)
			{
				WeakReference weakReference = this._packetProviders[i];
				IPacketProvider packetProvider = weakReference.Target as IPacketProvider;
				if (packetProvider != null && weakReference.IsAlive)
				{
					list.Add(packetProvider);
				}
				else
				{
					list.RemoveAt(i);
					i--;
				}
			}
			return list;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000065B5 File Offset: 0x000047B5
		public bool CanSeek
		{
			[CompilerGenerated]
			get
			{
				return this.<CanSeek>k__BackingField;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000065BD File Offset: 0x000047BD
		public long WasteBits
		{
			get
			{
				return this._reader.WasteBits;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000065CA File Offset: 0x000047CA
		public long ContainerBits
		{
			get
			{
				return this._reader.ContainerBits;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000065D8 File Offset: 0x000047D8
		public ContainerReader(Stream stream, bool closeOnDispose)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this._packetProviders = new List<WeakReference>();
			if (stream.CanSeek)
			{
				this._reader = ContainerReader.CreatePageReader.Invoke(stream, closeOnDispose, new Func<IPacketProvider, bool>(this.ProcessNewStream));
				this.CanSeek = true;
				return;
			}
			this._reader = ContainerReader.CreateForwardOnlyPageReader.Invoke(stream, closeOnDispose, new Func<IPacketProvider, bool>(this.ProcessNewStream));
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006650 File Offset: 0x00004850
		public bool TryInit()
		{
			return this.FindNextStream();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006658 File Offset: 0x00004858
		public bool FindNextStream()
		{
			this._reader.Lock();
			bool flag;
			try
			{
				this._foundStream = false;
				while (this._reader.ReadNextPage())
				{
					if (this._foundStream)
					{
						return true;
					}
				}
				flag = false;
			}
			finally
			{
				this._reader.Release();
			}
			return flag;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000066B4 File Offset: 0x000048B4
		private bool ProcessNewStream(IPacketProvider packetProvider)
		{
			bool flag = this._reader.Release();
			bool flag2;
			try
			{
				NewStreamHandler newStreamCallback = this.NewStreamCallback;
				if (newStreamCallback == null || newStreamCallback(packetProvider))
				{
					this._packetProviders.Add(new WeakReference(packetProvider));
					this._foundStream = true;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			finally
			{
				if (flag)
				{
					this._reader.Lock();
				}
			}
			return flag2;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006724 File Offset: 0x00004924
		public void Dispose()
		{
			IPageReader reader = this._reader;
			if (reader != null)
			{
				reader.Dispose();
			}
			this._reader = null;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000673E File Offset: 0x0000493E
		// Note: this type is marked as 'beforefieldinit'.
		static ContainerReader()
		{
		}

		// Token: 0x04000086 RID: 134
		[CompilerGenerated]
		private static Func<Stream, bool, Func<IPacketProvider, bool>, IPageReader> <CreatePageReader>k__BackingField;

		// Token: 0x04000087 RID: 135
		[CompilerGenerated]
		private static Func<Stream, bool, Func<IPacketProvider, bool>, IPageReader> <CreateForwardOnlyPageReader>k__BackingField;

		// Token: 0x04000088 RID: 136
		private IPageReader _reader;

		// Token: 0x04000089 RID: 137
		private List<WeakReference> _packetProviders;

		// Token: 0x0400008A RID: 138
		private bool _foundStream;

		// Token: 0x0400008B RID: 139
		[CompilerGenerated]
		private NewStreamHandler <NewStreamCallback>k__BackingField;

		// Token: 0x0400008C RID: 140
		[CompilerGenerated]
		private readonly bool <CanSeek>k__BackingField;

		// Token: 0x02000046 RID: 70
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000287 RID: 647 RVA: 0x00009C11 File Offset: 0x00007E11
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000288 RID: 648 RVA: 0x00009C1D File Offset: 0x00007E1D
			public <>c()
			{
			}

			// Token: 0x06000289 RID: 649 RVA: 0x00009C25 File Offset: 0x00007E25
			internal IPageReader <.cctor>b__28_0(Stream s, bool cod, Func<IPacketProvider, bool> cb)
			{
				return new PageReader(s, cod, cb);
			}

			// Token: 0x0600028A RID: 650 RVA: 0x00009C2F File Offset: 0x00007E2F
			internal IPageReader <.cctor>b__28_1(Stream s, bool cod, Func<IPacketProvider, bool> cb)
			{
				return new ForwardOnlyPageReader(s, cod, cb);
			}

			// Token: 0x0400010B RID: 267
			public static readonly ContainerReader.<>c <>9 = new ContainerReader.<>c();
		}
	}
}

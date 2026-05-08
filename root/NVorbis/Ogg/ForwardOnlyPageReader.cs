using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using NVorbis.Contracts;
using NVorbis.Contracts.Ogg;

namespace NVorbis.Ogg
{
	// Token: 0x0200001D RID: 29
	internal class ForwardOnlyPageReader : PageReaderBase
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00006CD3 File Offset: 0x00004ED3
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00006CDA File Offset: 0x00004EDA
		internal static Func<IPageReader, int, IForwardOnlyPacketProvider> CreatePacketProvider
		{
			[CompilerGenerated]
			get
			{
				return ForwardOnlyPageReader.<CreatePacketProvider>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				ForwardOnlyPageReader.<CreatePacketProvider>k__BackingField = value;
			}
		} = (IPageReader pr, int ss) => new ForwardOnlyPacketProvider(pr, ss);

		// Token: 0x06000139 RID: 313 RVA: 0x00006CE2 File Offset: 0x00004EE2
		public ForwardOnlyPageReader(Stream stream, bool closeOnDispose, Func<IPacketProvider, bool> newStreamCallback)
			: base(stream, closeOnDispose)
		{
			this._newStreamCallback = newStreamCallback;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006D00 File Offset: 0x00004F00
		protected override bool AddPage(int streamSerial, byte[] pageBuf, bool isResync)
		{
			IForwardOnlyPacketProvider forwardOnlyPacketProvider;
			if (this._packetProviders.TryGetValue(streamSerial, ref forwardOnlyPacketProvider))
			{
				if (forwardOnlyPacketProvider.AddPage(pageBuf, isResync) && (pageBuf[5] & 4) == 0)
				{
					return true;
				}
				this._packetProviders.Remove(streamSerial);
			}
			forwardOnlyPacketProvider = ForwardOnlyPageReader.CreatePacketProvider.Invoke(this, streamSerial);
			if (forwardOnlyPacketProvider.AddPage(pageBuf, isResync))
			{
				this._packetProviders.Add(streamSerial, forwardOnlyPacketProvider);
				if (this._newStreamCallback.Invoke(forwardOnlyPacketProvider))
				{
					return true;
				}
				this._packetProviders.Remove(streamSerial);
			}
			return false;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006D80 File Offset: 0x00004F80
		protected override void SetEndOfStreams()
		{
			foreach (KeyValuePair<int, IForwardOnlyPacketProvider> keyValuePair in this._packetProviders)
			{
				keyValuePair.Value.SetEndOfStream();
			}
			this._packetProviders.Clear();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public override bool ReadPageAt(long offset)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006DEB File Offset: 0x00004FEB
		// Note: this type is marked as 'beforefieldinit'.
		static ForwardOnlyPageReader()
		{
		}

		// Token: 0x0400009B RID: 155
		[CompilerGenerated]
		private static Func<IPageReader, int, IForwardOnlyPacketProvider> <CreatePacketProvider>k__BackingField;

		// Token: 0x0400009C RID: 156
		private readonly Dictionary<int, IForwardOnlyPacketProvider> _packetProviders = new Dictionary<int, IForwardOnlyPacketProvider>();

		// Token: 0x0400009D RID: 157
		private readonly Func<IPacketProvider, bool> _newStreamCallback;

		// Token: 0x02000047 RID: 71
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600028B RID: 651 RVA: 0x00009C39 File Offset: 0x00007E39
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600028C RID: 652 RVA: 0x00009C45 File Offset: 0x00007E45
			public <>c()
			{
			}

			// Token: 0x0600028D RID: 653 RVA: 0x00009C4D File Offset: 0x00007E4D
			internal IForwardOnlyPacketProvider <.cctor>b__10_0(IPageReader pr, int ss)
			{
				return new ForwardOnlyPacketProvider(pr, ss);
			}

			// Token: 0x0400010C RID: 268
			public static readonly ForwardOnlyPageReader.<>c <>9 = new ForwardOnlyPageReader.<>c();
		}
	}
}

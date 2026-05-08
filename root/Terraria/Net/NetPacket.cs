using System;
using System.IO;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;

namespace Terraria.Net
{
	// Token: 0x02000170 RID: 368
	public struct NetPacket
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x005028C4 File Offset: 0x00500AC4
		// (set) Token: 0x06001DE9 RID: 7657 RVA: 0x005028CC File Offset: 0x00500ACC
		public int Length
		{
			[CompilerGenerated]
			get
			{
				return this.<Length>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Length>k__BackingField = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x005028D5 File Offset: 0x00500AD5
		public BinaryWriter Writer
		{
			get
			{
				return this.Buffer.Writer;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x005028E2 File Offset: 0x00500AE2
		public BinaryReader Reader
		{
			get
			{
				return this.Buffer.Reader;
			}
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x005028F0 File Offset: 0x00500AF0
		public NetPacket(ushort id, int size)
		{
			this = default(NetPacket);
			this.Id = id;
			this.Length = size + 5;
			if (this.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("Tried to create a packet with length > " + ushort.MaxValue);
			}
			this.Buffer = BufferPool.Request(this.Length);
			this.Writer.Write((ushort)this.Length);
			this.Writer.Write(82);
			this.Writer.Write(id);
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00502977 File Offset: 0x00500B77
		public void Recycle()
		{
			this.Buffer.Recycle();
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00502984 File Offset: 0x00500B84
		public void ShrinkToFit()
		{
			if (this.Length == (int)this.Writer.BaseStream.Position)
			{
				return;
			}
			if (this.Writer.BaseStream.Position > (long)this.Length)
			{
				throw new IndexOutOfRangeException("Overwrite on supplied Length. Consider letting Length default to max packet size if you don't know how long it will be");
			}
			this.Length = (int)this.Writer.BaseStream.Position;
			this.Writer.Seek(0, SeekOrigin.Begin);
			this.Writer.Write((ushort)this.Length);
			this.Writer.Seek(this.Length, SeekOrigin.Begin);
		}

		// Token: 0x0400167E RID: 5758
		public const int HEADER_SIZE = 5;

		// Token: 0x0400167F RID: 5759
		public readonly ushort Id;

		// Token: 0x04001680 RID: 5760
		[CompilerGenerated]
		private int <Length>k__BackingField;

		// Token: 0x04001681 RID: 5761
		public readonly CachedBuffer Buffer;
	}
}

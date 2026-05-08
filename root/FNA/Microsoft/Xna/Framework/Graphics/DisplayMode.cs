using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200007D RID: 125
	[Serializable]
	public class DisplayMode
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x000243D0 File Offset: 0x000225D0
		public float AspectRatio
		{
			get
			{
				return (float)this.Width / (float)this.Height;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x000243E1 File Offset: 0x000225E1
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x000243E9 File Offset: 0x000225E9
		public SurfaceFormat Format
		{
			[CompilerGenerated]
			get
			{
				return this.<Format>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Format>k__BackingField = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x000243F2 File Offset: 0x000225F2
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x000243FA File Offset: 0x000225FA
		public int Height
		{
			[CompilerGenerated]
			get
			{
				return this.<Height>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Height>k__BackingField = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00024403 File Offset: 0x00022603
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x0002440B File Offset: 0x0002260B
		public int Width
		{
			[CompilerGenerated]
			get
			{
				return this.<Width>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Width>k__BackingField = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00024414 File Offset: 0x00022614
		public Rectangle TitleSafeArea
		{
			get
			{
				return new Rectangle(0, 0, this.Width, this.Height);
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00024429 File Offset: 0x00022629
		internal DisplayMode(int width, int height, SurfaceFormat format)
		{
			this.Width = width;
			this.Height = height;
			this.Format = format;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00024446 File Offset: 0x00022646
		public static bool operator !=(DisplayMode left, DisplayMode right)
		{
			return !(left == right);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00024452 File Offset: 0x00022652
		public static bool operator ==(DisplayMode left, DisplayMode right)
		{
			return left == right || (left != null && right != null && (left.Format == right.Format && left.Height == right.Height) && left.Width == right.Width);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0002448E File Offset: 0x0002268E
		public override bool Equals(object obj)
		{
			return obj as DisplayMode == this;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0002449C File Offset: 0x0002269C
		public override int GetHashCode()
		{
			return this.Width.GetHashCode() ^ this.Height.GetHashCode() ^ this.Format.GetHashCode();
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000244DC File Offset: 0x000226DC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{{Width:",
				this.Width.ToString(),
				" Height:",
				this.Height.ToString(),
				" Format:",
				this.Format.ToString(),
				"}}"
			});
		}

		// Token: 0x040007C9 RID: 1993
		[CompilerGenerated]
		private SurfaceFormat <Format>k__BackingField;

		// Token: 0x040007CA RID: 1994
		[CompilerGenerated]
		private int <Height>k__BackingField;

		// Token: 0x040007CB RID: 1995
		[CompilerGenerated]
		private int <Width>k__BackingField;
	}
}

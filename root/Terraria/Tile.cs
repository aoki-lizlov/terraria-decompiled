using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria
{
	// Token: 0x0200004C RID: 76
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class Tile
	{
		// Token: 0x06000B60 RID: 2912 RVA: 0x0035523C File Offset: 0x0035343C
		public Tile()
		{
			this.type = 0;
			this.wall = 0;
			this.liquid = 0;
			this.sTileHeader = 0;
			this.bTileHeader = 0;
			this.bTileHeader2 = 0;
			this.bTileHeader3 = 0;
			this.frameX = 0;
			this.frameY = 0;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00355290 File Offset: 0x00353490
		public Tile(Tile copy)
		{
			if (copy == null)
			{
				this.type = 0;
				this.wall = 0;
				this.liquid = 0;
				this.sTileHeader = 0;
				this.bTileHeader = 0;
				this.bTileHeader2 = 0;
				this.bTileHeader3 = 0;
				this.frameX = 0;
				this.frameY = 0;
				return;
			}
			this.type = copy.type;
			this.wall = copy.wall;
			this.liquid = copy.liquid;
			this.sTileHeader = copy.sTileHeader;
			this.bTileHeader = copy.bTileHeader;
			this.bTileHeader2 = copy.bTileHeader2;
			this.bTileHeader3 = copy.bTileHeader3;
			this.frameX = copy.frameX;
			this.frameY = copy.frameY;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00355354 File Offset: 0x00353554
		public void ClearEverything()
		{
			this.type = 0;
			this.wall = 0;
			this.liquid = 0;
			this.sTileHeader = 0;
			this.bTileHeader = 0;
			this.bTileHeader2 = 0;
			this.bTileHeader3 = 0;
			this.frameX = 0;
			this.frameY = 0;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x003553A0 File Offset: 0x003535A0
		public void ClearTile()
		{
			this.ClearSlope();
			this.active(false);
			this.inActive(false);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x003553B6 File Offset: 0x003535B6
		public void ClearSlope()
		{
			this.slope(0);
			this.halfBrick(false);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x003553C6 File Offset: 0x003535C6
		public void ClearTileAndPaint()
		{
			this.ClearTile();
			this.ClearBlockPaintAndCoating();
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x003553D4 File Offset: 0x003535D4
		public void CopyFrom(Tile from)
		{
			this.type = from.type;
			this.wall = from.wall;
			this.liquid = from.liquid;
			this.sTileHeader = from.sTileHeader;
			this.bTileHeader = from.bTileHeader;
			this.bTileHeader2 = from.bTileHeader2;
			this.bTileHeader3 = from.bTileHeader3;
			this.frameX = from.frameX;
			this.frameY = from.frameY;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00355450 File Offset: 0x00353650
		public int collisionType
		{
			get
			{
				if (!this.active())
				{
					return 0;
				}
				if (this.halfBrick())
				{
					return 2;
				}
				if (this.slope() > 0)
				{
					return (int)(2 + this.slope());
				}
				if (Main.tileSolid[(int)this.type] && !Main.tileSolidTop[(int)this.type])
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x003554A4 File Offset: 0x003536A4
		public bool isTheSameAs(Tile compTile)
		{
			if (compTile == null)
			{
				return false;
			}
			if (this.sTileHeader != compTile.sTileHeader)
			{
				return false;
			}
			if (this.active())
			{
				if (this.type != compTile.type)
				{
					return false;
				}
				if (Main.tileFrameImportant[(int)this.type] && (this.frameX != compTile.frameX || this.frameY != compTile.frameY))
				{
					return false;
				}
			}
			if (this.wall != compTile.wall || this.liquid != compTile.liquid)
			{
				return false;
			}
			if (compTile.liquid == 0)
			{
				if (this.wallColor() != compTile.wallColor())
				{
					return false;
				}
				if (this.wire4() != compTile.wire4())
				{
					return false;
				}
			}
			else if (this.bTileHeader != compTile.bTileHeader)
			{
				return false;
			}
			return this.invisibleBlock() == compTile.invisibleBlock() && this.invisibleWall() == compTile.invisibleWall() && this.fullbrightBlock() == compTile.fullbrightBlock() && this.fullbrightWall() == compTile.fullbrightWall();
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0035559C File Offset: 0x0035379C
		public int blockType()
		{
			if (this.halfBrick())
			{
				return 1;
			}
			int num = (int)this.slope();
			if (num > 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x003555C3 File Offset: 0x003537C3
		public void liquidType(int liquidType)
		{
			if (liquidType == 0)
			{
				this.bTileHeader &= 159;
				return;
			}
			if (liquidType == 1)
			{
				this.lava(true);
				return;
			}
			if (liquidType == 2)
			{
				this.honey(true);
				return;
			}
			if (liquidType == 3)
			{
				this.shimmer(true);
			}
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x003555FF File Offset: 0x003537FF
		public byte liquidType()
		{
			return (byte)((this.bTileHeader & 96) >> 5);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0035560D File Offset: 0x0035380D
		public bool nactive()
		{
			return (this.sTileHeader & 96) == 32;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0035561F File Offset: 0x0035381F
		public void ResetToType(ushort type)
		{
			this.liquid = 0;
			this.sTileHeader = 32;
			this.bTileHeader = 0;
			this.bTileHeader2 = 0;
			this.bTileHeader3 = 0;
			this.frameX = 0;
			this.frameY = 0;
			this.type = type;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0035565A File Offset: 0x0035385A
		internal void ClearMetadata()
		{
			this.liquid = 0;
			this.sTileHeader = 0;
			this.bTileHeader = 0;
			this.bTileHeader2 = 0;
			this.bTileHeader3 = 0;
			this.frameX = 0;
			this.frameY = 0;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00355690 File Offset: 0x00353890
		public Color actColor(Color oldColor)
		{
			if (!this.inActive())
			{
				return oldColor;
			}
			double num = 0.4;
			return new Color((int)((byte)(num * (double)oldColor.R)), (int)((byte)(num * (double)oldColor.G)), (int)((byte)(num * (double)oldColor.B)), (int)oldColor.A);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x003556DE File Offset: 0x003538DE
		public void actColor(ref Vector3 oldColor)
		{
			if (!this.inActive())
			{
				return;
			}
			oldColor *= 0.4f;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00355700 File Offset: 0x00353900
		public bool topSlope()
		{
			byte b = this.slope();
			return b == 1 || b == 2;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00355720 File Offset: 0x00353920
		public bool bottomSlope()
		{
			byte b = this.slope();
			return b == 3 || b == 4;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00355740 File Offset: 0x00353940
		public bool leftSlope()
		{
			byte b = this.slope();
			return b == 2 || b == 4;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00355760 File Offset: 0x00353960
		public bool rightSlope()
		{
			byte b = this.slope();
			return b == 1 || b == 3;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0035577E File Offset: 0x0035397E
		public bool HasSameSlope(Tile tile)
		{
			return (this.sTileHeader & 29696) == (tile.sTileHeader & 29696);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0035579A File Offset: 0x0035399A
		public byte wallColor()
		{
			return this.bTileHeader & 31;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x003557A6 File Offset: 0x003539A6
		public void wallColor(byte wallColor)
		{
			this.bTileHeader = (this.bTileHeader & 224) | wallColor;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x003557BD File Offset: 0x003539BD
		public bool lava()
		{
			return (this.bTileHeader & 96) == 32;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x003557CC File Offset: 0x003539CC
		public void lava(bool lava)
		{
			if (lava)
			{
				this.bTileHeader = (this.bTileHeader & 159) | 32;
				return;
			}
			this.bTileHeader &= 223;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x003557FB File Offset: 0x003539FB
		public bool honey()
		{
			return (this.bTileHeader & 96) == 64;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0035580A File Offset: 0x00353A0A
		public void honey(bool honey)
		{
			if (honey)
			{
				this.bTileHeader = (this.bTileHeader & 159) | 64;
				return;
			}
			this.bTileHeader &= 191;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00355839 File Offset: 0x00353A39
		public bool shimmer()
		{
			return (this.bTileHeader & 96) == 96;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00355848 File Offset: 0x00353A48
		public void shimmer(bool shimmer)
		{
			if (shimmer)
			{
				this.bTileHeader = (this.bTileHeader & 159) | 96;
				return;
			}
			this.bTileHeader &= 159;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00355877 File Offset: 0x00353A77
		public bool water()
		{
			return this.liquidType() == 0;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00355882 File Offset: 0x00353A82
		public bool anyWater()
		{
			return this.liquid > 0 && this.water();
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00355895 File Offset: 0x00353A95
		public bool anyLava()
		{
			return this.liquid > 0 && this.lava();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x003558A8 File Offset: 0x00353AA8
		public bool anyHoney()
		{
			return this.liquid > 0 && this.honey();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x003558BB File Offset: 0x00353ABB
		public bool anyShimmer()
		{
			return this.liquid > 0 && this.shimmer();
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x003558CE File Offset: 0x00353ACE
		public bool wire4()
		{
			return (this.bTileHeader & 128) == 128;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x003558E3 File Offset: 0x00353AE3
		public void wire4(bool wire4)
		{
			if (wire4)
			{
				this.bTileHeader |= 128;
				return;
			}
			this.bTileHeader &= 127;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0035590C File Offset: 0x00353B0C
		public int wallFrameX()
		{
			return (int)((this.bTileHeader2 & 15) * 36);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0035591A File Offset: 0x00353B1A
		public void wallFrameX(int wallFrameX)
		{
			this.bTileHeader2 = (byte)((int)(this.bTileHeader2 & 240) | ((wallFrameX / 36) & 15));
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00355937 File Offset: 0x00353B37
		public byte frameNumber()
		{
			return (byte)((this.bTileHeader2 & 48) >> 4);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00355945 File Offset: 0x00353B45
		public void frameNumber(byte frameNumber)
		{
			this.bTileHeader2 = (byte)((int)(this.bTileHeader2 & 207) | ((int)(frameNumber & 3) << 4));
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00355960 File Offset: 0x00353B60
		public byte wallFrameNumber()
		{
			return (byte)((this.bTileHeader2 & 192) >> 6);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00355971 File Offset: 0x00353B71
		public void wallFrameNumber(byte wallFrameNumber)
		{
			this.bTileHeader2 = (byte)((int)(this.bTileHeader2 & 63) | ((int)(wallFrameNumber & 3) << 6));
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00355989 File Offset: 0x00353B89
		public int wallFrameY()
		{
			return (int)((this.bTileHeader3 & 7) * 36);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00355996 File Offset: 0x00353B96
		public void wallFrameY(int wallFrameY)
		{
			this.bTileHeader3 = (byte)((int)(this.bTileHeader3 & 248) | ((wallFrameY / 36) & 7));
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x003559B2 File Offset: 0x00353BB2
		public bool checkingLiquid()
		{
			return (this.bTileHeader3 & 8) == 8;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x003559BF File Offset: 0x00353BBF
		public void checkingLiquid(bool checkingLiquid)
		{
			if (checkingLiquid)
			{
				this.bTileHeader3 |= 8;
				return;
			}
			this.bTileHeader3 &= 247;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x003559E7 File Offset: 0x00353BE7
		public bool skipLiquid()
		{
			return (this.bTileHeader3 & 16) == 16;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x003559F6 File Offset: 0x00353BF6
		public void skipLiquid(bool skipLiquid)
		{
			if (skipLiquid)
			{
				this.bTileHeader3 |= 16;
				return;
			}
			this.bTileHeader3 &= 239;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00355A1F File Offset: 0x00353C1F
		public bool invisibleBlock()
		{
			return (this.bTileHeader3 & 32) == 32;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00355A2E File Offset: 0x00353C2E
		public void invisibleBlock(bool invisibleBlock)
		{
			if (invisibleBlock)
			{
				this.bTileHeader3 |= 32;
				return;
			}
			this.bTileHeader3 = (byte)((int)this.bTileHeader3 & -33);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00355A54 File Offset: 0x00353C54
		public bool invisibleWall()
		{
			return (this.bTileHeader3 & 64) == 64;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00355A63 File Offset: 0x00353C63
		public void invisibleWall(bool invisibleWall)
		{
			if (invisibleWall)
			{
				this.bTileHeader3 |= 64;
				return;
			}
			this.bTileHeader3 = (byte)((int)this.bTileHeader3 & -65);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00355A89 File Offset: 0x00353C89
		public bool fullbrightBlock()
		{
			return (this.bTileHeader3 & 128) == 128;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00355A9E File Offset: 0x00353C9E
		public void fullbrightBlock(bool fullbrightBlock)
		{
			if (fullbrightBlock)
			{
				this.bTileHeader3 |= 128;
				return;
			}
			this.bTileHeader3 = (byte)((int)this.bTileHeader3 & -129);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00355ACA File Offset: 0x00353CCA
		public byte color()
		{
			return (byte)(this.sTileHeader & 31);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00355AD6 File Offset: 0x00353CD6
		public void color(byte color)
		{
			this.sTileHeader = (this.sTileHeader & 65504) | (ushort)color;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00355AED File Offset: 0x00353CED
		public bool active()
		{
			return (this.sTileHeader & 32) == 32;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00355AFC File Offset: 0x00353CFC
		public void active(bool active)
		{
			if (active)
			{
				this.sTileHeader |= 32;
				return;
			}
			this.sTileHeader &= 65503;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00355B25 File Offset: 0x00353D25
		public bool inActive()
		{
			return (this.sTileHeader & 64) == 64;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00355B34 File Offset: 0x00353D34
		public void inActive(bool inActive)
		{
			if (inActive)
			{
				this.sTileHeader |= 64;
				return;
			}
			this.sTileHeader &= 65471;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00355B5D File Offset: 0x00353D5D
		public bool wire()
		{
			return (this.sTileHeader & 128) == 128;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00355B72 File Offset: 0x00353D72
		public void wire(bool wire)
		{
			if (wire)
			{
				this.sTileHeader |= 128;
				return;
			}
			this.sTileHeader &= 65407;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00355B9E File Offset: 0x00353D9E
		public bool wire2()
		{
			return (this.sTileHeader & 256) == 256;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00355BB3 File Offset: 0x00353DB3
		public void wire2(bool wire2)
		{
			if (wire2)
			{
				this.sTileHeader |= 256;
				return;
			}
			this.sTileHeader &= 65279;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00355BDF File Offset: 0x00353DDF
		public bool wire3()
		{
			return (this.sTileHeader & 512) == 512;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00355BF4 File Offset: 0x00353DF4
		public void wire3(bool wire3)
		{
			if (wire3)
			{
				this.sTileHeader |= 512;
				return;
			}
			this.sTileHeader &= 65023;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00355C20 File Offset: 0x00353E20
		public bool halfBrick()
		{
			return (this.sTileHeader & 1024) == 1024;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00355C35 File Offset: 0x00353E35
		public void halfBrick(bool halfBrick)
		{
			if (halfBrick)
			{
				this.sTileHeader |= 1024;
				return;
			}
			this.sTileHeader &= 64511;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00355C61 File Offset: 0x00353E61
		public bool actuator()
		{
			return (this.sTileHeader & 2048) == 2048;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00355C76 File Offset: 0x00353E76
		public void actuator(bool actuator)
		{
			if (actuator)
			{
				this.sTileHeader |= 2048;
				return;
			}
			this.sTileHeader &= 63487;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00355CA2 File Offset: 0x00353EA2
		public byte slope()
		{
			return (byte)((this.sTileHeader & 28672) >> 12);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00355CB4 File Offset: 0x00353EB4
		public void slope(byte slope)
		{
			this.sTileHeader = (ushort)((int)(this.sTileHeader & 36863) | ((int)(slope & 7) << 12));
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00355CD0 File Offset: 0x00353ED0
		public bool fullbrightWall()
		{
			return (this.sTileHeader & 32768) == 32768;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00355CE5 File Offset: 0x00353EE5
		public void fullbrightWall(bool fullbrightWall)
		{
			if (fullbrightWall)
			{
				this.sTileHeader |= 32768;
				return;
			}
			this.sTileHeader = (ushort)((int)this.sTileHeader & -32769);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00355D11 File Offset: 0x00353F11
		public bool anyWire()
		{
			return (this.sTileHeader & 896) != 0 || (this.bTileHeader & 128) > 0;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00355D34 File Offset: 0x00353F34
		public void Clear(TileDataType types)
		{
			if ((types & TileDataType.Tile) != (TileDataType)0)
			{
				this.type = 0;
				this.active(false);
				this.frameX = 0;
				this.frameY = 0;
			}
			if ((types & TileDataType.Wall) != (TileDataType)0)
			{
				this.wall = 0;
				this.wallFrameX(0);
				this.wallFrameY(0);
			}
			if ((types & TileDataType.TilePaint) != (TileDataType)0)
			{
				this.ClearBlockPaintAndCoating();
			}
			if ((types & TileDataType.WallPaint) != (TileDataType)0)
			{
				this.ClearWallPaintAndCoating();
			}
			if ((types & TileDataType.Liquid) != (TileDataType)0)
			{
				this.liquid = 0;
				this.liquidType(0);
				this.checkingLiquid(false);
			}
			if ((types & TileDataType.Slope) != (TileDataType)0)
			{
				this.slope(0);
				this.halfBrick(false);
			}
			if ((types & TileDataType.Wiring) != (TileDataType)0)
			{
				this.wire(false);
				this.wire2(false);
				this.wire3(false);
				this.wire4(false);
			}
			if ((types & TileDataType.Actuator) != (TileDataType)0)
			{
				this.actuator(false);
				this.inActive(false);
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00355DFC File Offset: 0x00353FFC
		public static void SmoothSlope(int x, int y, bool applyToNeighbors = true, bool sync = false)
		{
			if (applyToNeighbors)
			{
				Tile.SmoothSlope(x + 1, y, false, sync);
				Tile.SmoothSlope(x - 1, y, false, sync);
				Tile.SmoothSlope(x, y + 1, false, sync);
				Tile.SmoothSlope(x, y - 1, false, sync);
			}
			Tile tile = Main.tile[x, y];
			if (!WorldGen.CanPoundTile(x, y) || !WorldGen.SolidOrSlopedTile(x, y))
			{
				return;
			}
			bool flag = !WorldGen.TileEmpty(x, y - 1);
			bool flag2 = !WorldGen.SolidOrSlopedTile(x, y - 1) && flag;
			bool flag3 = WorldGen.SolidOrSlopedTile(x, y + 1);
			bool flag4 = WorldGen.SolidOrSlopedTile(x - 1, y);
			bool flag5 = WorldGen.SolidOrSlopedTile(x + 1, y);
			int num = ((flag ? 1 : 0) << 3) | ((flag3 ? 1 : 0) << 2) | ((flag4 ? 1 : 0) << 1) | (flag5 ? 1 : 0);
			bool flag6 = tile.halfBrick();
			int num2 = (int)tile.slope();
			switch (num)
			{
			case 4:
				tile.slope(0);
				tile.halfBrick(true);
				goto IL_014F;
			case 5:
				tile.halfBrick(false);
				tile.slope(2);
				goto IL_014F;
			case 6:
				tile.halfBrick(false);
				tile.slope(1);
				goto IL_014F;
			case 9:
				if (!flag2)
				{
					tile.halfBrick(false);
					tile.slope(4);
					goto IL_014F;
				}
				goto IL_014F;
			case 10:
				if (!flag2)
				{
					tile.halfBrick(false);
					tile.slope(3);
					goto IL_014F;
				}
				goto IL_014F;
			}
			tile.halfBrick(false);
			tile.slope(0);
			IL_014F:
			if (sync)
			{
				int num3 = (int)tile.slope();
				bool flag7 = flag6 != tile.halfBrick();
				bool flag8 = num2 != num3;
				if (flag7 && flag8)
				{
					NetMessage.SendData(17, -1, -1, null, 23, (float)x, (float)y, (float)num3, 0, 0, 0);
					return;
				}
				if (flag7)
				{
					NetMessage.SendData(17, -1, -1, null, 7, (float)x, (float)y, 1f, 0, 0, 0);
					return;
				}
				if (flag8)
				{
					NetMessage.SendData(17, -1, -1, null, 14, (float)x, (float)y, (float)num3, 0, 0, 0);
				}
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00355FD1 File Offset: 0x003541D1
		public void CopyPaintAndCoating(Tile other)
		{
			this.color(other.color());
			this.invisibleBlock(other.invisibleBlock());
			this.fullbrightBlock(other.fullbrightBlock());
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00355FF8 File Offset: 0x003541F8
		public TileColorCache BlockColorAndCoating()
		{
			return new TileColorCache
			{
				Color = this.color(),
				FullBright = this.fullbrightBlock(),
				Invisible = this.invisibleBlock()
			};
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00356038 File Offset: 0x00354238
		public TileColorCache WallColorAndCoating()
		{
			return new TileColorCache
			{
				Color = this.wallColor(),
				FullBright = this.fullbrightWall(),
				Invisible = this.invisibleWall()
			};
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00356075 File Offset: 0x00354275
		public void UseBlockColors(TileColorCache cache)
		{
			cache.ApplyToBlock(this);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0035607F File Offset: 0x0035427F
		public void UseWallColors(TileColorCache cache)
		{
			cache.ApplyToWall(this);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00356089 File Offset: 0x00354289
		public void ClearBlockPaintAndCoating()
		{
			this.color(0);
			this.fullbrightBlock(false);
			this.invisibleBlock(false);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x003560A0 File Offset: 0x003542A0
		public void ClearWallPaintAndCoating()
		{
			this.wallColor(0);
			this.fullbrightWall(false);
			this.invisibleWall(false);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x003560B8 File Offset: 0x003542B8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Tile Type:",
				this.type,
				" Active:",
				this.active().ToString(),
				" Wall:",
				this.wall,
				" Slope:",
				this.slope(),
				" fX:",
				this.frameX,
				" fY:",
				this.frameY
			});
		}

		// Token: 0x040009B5 RID: 2485
		public ushort type;

		// Token: 0x040009B6 RID: 2486
		public ushort wall;

		// Token: 0x040009B7 RID: 2487
		public byte liquid;

		// Token: 0x040009B8 RID: 2488
		public ushort sTileHeader;

		// Token: 0x040009B9 RID: 2489
		public byte bTileHeader;

		// Token: 0x040009BA RID: 2490
		public byte bTileHeader2;

		// Token: 0x040009BB RID: 2491
		public byte bTileHeader3;

		// Token: 0x040009BC RID: 2492
		public short frameX;

		// Token: 0x040009BD RID: 2493
		public short frameY;

		// Token: 0x040009BE RID: 2494
		private const int Bit0 = 1;

		// Token: 0x040009BF RID: 2495
		private const int Bit1 = 2;

		// Token: 0x040009C0 RID: 2496
		private const int Bit2 = 4;

		// Token: 0x040009C1 RID: 2497
		private const int Bit3 = 8;

		// Token: 0x040009C2 RID: 2498
		private const int Bit4 = 16;

		// Token: 0x040009C3 RID: 2499
		private const int Bit5 = 32;

		// Token: 0x040009C4 RID: 2500
		private const int Bit6 = 64;

		// Token: 0x040009C5 RID: 2501
		private const int Bit7 = 128;

		// Token: 0x040009C6 RID: 2502
		private const ushort Bit15 = 32768;

		// Token: 0x040009C7 RID: 2503
		public const int Type_Solid = 0;

		// Token: 0x040009C8 RID: 2504
		public const int Type_Halfbrick = 1;

		// Token: 0x040009C9 RID: 2505
		public const int Type_SlopeDownRight = 2;

		// Token: 0x040009CA RID: 2506
		public const int Type_SlopeDownLeft = 3;

		// Token: 0x040009CB RID: 2507
		public const int Type_SlopeUpRight = 4;

		// Token: 0x040009CC RID: 2508
		public const int Type_SlopeUpLeft = 5;

		// Token: 0x040009CD RID: 2509
		public const int Liquid_Water = 0;

		// Token: 0x040009CE RID: 2510
		public const int Liquid_Lava = 1;

		// Token: 0x040009CF RID: 2511
		public const int Liquid_Honey = 2;

		// Token: 0x040009D0 RID: 2512
		public const int Liquid_Shimmer = 3;

		// Token: 0x040009D1 RID: 2513
		private const int NeitherLavaOrHoney = 159;

		// Token: 0x040009D2 RID: 2514
		private const int EitherLavaOrHoney = 96;
	}
}

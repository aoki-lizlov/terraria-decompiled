using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200000D RID: 13
	[TypeConverter(typeof(ColorConverter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct Color : IEquatable<Color>, IPackedVector, IPackedVector<uint>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00007B21 File Offset: 0x00005D21
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x00007B2D File Offset: 0x00005D2D
		public byte B
		{
			get
			{
				return (byte)(this.packedValue >> 16);
			}
			set
			{
				this.packedValue = (this.packedValue & 4278255615U) | (uint)((uint)value << 16);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00007B46 File Offset: 0x00005D46
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x00007B51 File Offset: 0x00005D51
		public byte G
		{
			get
			{
				return (byte)(this.packedValue >> 8);
			}
			set
			{
				this.packedValue = (this.packedValue & 4294902015U) | (uint)((uint)value << 8);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00007B69 File Offset: 0x00005D69
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x00007B72 File Offset: 0x00005D72
		public byte R
		{
			get
			{
				return (byte)this.packedValue;
			}
			set
			{
				this.packedValue = (this.packedValue & 4294967040U) | (uint)value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00007B88 File Offset: 0x00005D88
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x00007B94 File Offset: 0x00005D94
		public byte A
		{
			get
			{
				return (byte)(this.packedValue >> 24);
			}
			set
			{
				this.packedValue = (this.packedValue & 16777215U) | (uint)((uint)value << 24);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00007BAD File Offset: 0x00005DAD
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x00007BB5 File Offset: 0x00005DB5
		[CLSCompliant(false)]
		public uint PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00007BBE File Offset: 0x00005DBE
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x00007BC5 File Offset: 0x00005DC5
		public static Color Transparent
		{
			[CompilerGenerated]
			get
			{
				return Color.<Transparent>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Transparent>k__BackingField = value;
			}
		} = new Color(0U);

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00007BCD File Offset: 0x00005DCD
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x00007BD4 File Offset: 0x00005DD4
		public static Color AliceBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<AliceBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<AliceBlue>k__BackingField = value;
			}
		} = new Color(4294965488U);

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00007BDC File Offset: 0x00005DDC
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00007BE3 File Offset: 0x00005DE3
		public static Color AntiqueWhite
		{
			[CompilerGenerated]
			get
			{
				return Color.<AntiqueWhite>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<AntiqueWhite>k__BackingField = value;
			}
		} = new Color(4292340730U);

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00007BEB File Offset: 0x00005DEB
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x00007BF2 File Offset: 0x00005DF2
		public static Color Aqua
		{
			[CompilerGenerated]
			get
			{
				return Color.<Aqua>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Aqua>k__BackingField = value;
			}
		} = new Color(4294967040U);

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00007BFA File Offset: 0x00005DFA
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x00007C01 File Offset: 0x00005E01
		public static Color Aquamarine
		{
			[CompilerGenerated]
			get
			{
				return Color.<Aquamarine>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Aquamarine>k__BackingField = value;
			}
		} = new Color(4292149119U);

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00007C09 File Offset: 0x00005E09
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x00007C10 File Offset: 0x00005E10
		public static Color Azure
		{
			[CompilerGenerated]
			get
			{
				return Color.<Azure>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Azure>k__BackingField = value;
			}
		} = new Color(4294967280U);

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00007C18 File Offset: 0x00005E18
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x00007C1F File Offset: 0x00005E1F
		public static Color Beige
		{
			[CompilerGenerated]
			get
			{
				return Color.<Beige>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Beige>k__BackingField = value;
			}
		} = new Color(4292670965U);

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00007C27 File Offset: 0x00005E27
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x00007C2E File Offset: 0x00005E2E
		public static Color Bisque
		{
			[CompilerGenerated]
			get
			{
				return Color.<Bisque>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Bisque>k__BackingField = value;
			}
		} = new Color(4291093759U);

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00007C36 File Offset: 0x00005E36
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x00007C3D File Offset: 0x00005E3D
		public static Color Black
		{
			[CompilerGenerated]
			get
			{
				return Color.<Black>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Black>k__BackingField = value;
			}
		} = new Color(4278190080U);

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00007C45 File Offset: 0x00005E45
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x00007C4C File Offset: 0x00005E4C
		public static Color BlanchedAlmond
		{
			[CompilerGenerated]
			get
			{
				return Color.<BlanchedAlmond>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<BlanchedAlmond>k__BackingField = value;
			}
		} = new Color(4291685375U);

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00007C54 File Offset: 0x00005E54
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00007C5B File Offset: 0x00005E5B
		public static Color Blue
		{
			[CompilerGenerated]
			get
			{
				return Color.<Blue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Blue>k__BackingField = value;
			}
		} = new Color(4294901760U);

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00007C63 File Offset: 0x00005E63
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x00007C6A File Offset: 0x00005E6A
		public static Color BlueViolet
		{
			[CompilerGenerated]
			get
			{
				return Color.<BlueViolet>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<BlueViolet>k__BackingField = value;
			}
		} = new Color(4293012362U);

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00007C72 File Offset: 0x00005E72
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x00007C79 File Offset: 0x00005E79
		public static Color Brown
		{
			[CompilerGenerated]
			get
			{
				return Color.<Brown>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Brown>k__BackingField = value;
			}
		} = new Color(4280953509U);

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00007C81 File Offset: 0x00005E81
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x00007C88 File Offset: 0x00005E88
		public static Color BurlyWood
		{
			[CompilerGenerated]
			get
			{
				return Color.<BurlyWood>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<BurlyWood>k__BackingField = value;
			}
		} = new Color(4287084766U);

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00007C90 File Offset: 0x00005E90
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x00007C97 File Offset: 0x00005E97
		public static Color CadetBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<CadetBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<CadetBlue>k__BackingField = value;
			}
		} = new Color(4288716383U);

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00007C9F File Offset: 0x00005E9F
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x00007CA6 File Offset: 0x00005EA6
		public static Color Chartreuse
		{
			[CompilerGenerated]
			get
			{
				return Color.<Chartreuse>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Chartreuse>k__BackingField = value;
			}
		} = new Color(4278255487U);

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00007CAE File Offset: 0x00005EAE
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x00007CB5 File Offset: 0x00005EB5
		public static Color Chocolate
		{
			[CompilerGenerated]
			get
			{
				return Color.<Chocolate>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Chocolate>k__BackingField = value;
			}
		} = new Color(4280183250U);

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00007CBD File Offset: 0x00005EBD
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public static Color Coral
		{
			[CompilerGenerated]
			get
			{
				return Color.<Coral>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Coral>k__BackingField = value;
			}
		} = new Color(4283465727U);

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00007CCC File Offset: 0x00005ECC
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x00007CD3 File Offset: 0x00005ED3
		public static Color CornflowerBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<CornflowerBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<CornflowerBlue>k__BackingField = value;
			}
		} = new Color(4293760356U);

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00007CDB File Offset: 0x00005EDB
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00007CE2 File Offset: 0x00005EE2
		public static Color Cornsilk
		{
			[CompilerGenerated]
			get
			{
				return Color.<Cornsilk>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Cornsilk>k__BackingField = value;
			}
		} = new Color(4292671743U);

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00007CEA File Offset: 0x00005EEA
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00007CF1 File Offset: 0x00005EF1
		public static Color Crimson
		{
			[CompilerGenerated]
			get
			{
				return Color.<Crimson>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Crimson>k__BackingField = value;
			}
		} = new Color(4282127580U);

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00007CF9 File Offset: 0x00005EF9
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00007D00 File Offset: 0x00005F00
		public static Color Cyan
		{
			[CompilerGenerated]
			get
			{
				return Color.<Cyan>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Cyan>k__BackingField = value;
			}
		} = new Color(4294967040U);

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00007D08 File Offset: 0x00005F08
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00007D0F File Offset: 0x00005F0F
		public static Color DarkBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkBlue>k__BackingField = value;
			}
		} = new Color(4287299584U);

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00007D17 File Offset: 0x00005F17
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x00007D1E File Offset: 0x00005F1E
		public static Color DarkCyan
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkCyan>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkCyan>k__BackingField = value;
			}
		} = new Color(4287335168U);

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00007D26 File Offset: 0x00005F26
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x00007D2D File Offset: 0x00005F2D
		public static Color DarkGoldenrod
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkGoldenrod>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkGoldenrod>k__BackingField = value;
			}
		} = new Color(4278945464U);

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00007D35 File Offset: 0x00005F35
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x00007D3C File Offset: 0x00005F3C
		public static Color DarkGray
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkGray>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkGray>k__BackingField = value;
			}
		} = new Color(4289309097U);

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00007D44 File Offset: 0x00005F44
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x00007D4B File Offset: 0x00005F4B
		public static Color DarkGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkGreen>k__BackingField = value;
			}
		} = new Color(4278215680U);

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00007D53 File Offset: 0x00005F53
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x00007D5A File Offset: 0x00005F5A
		public static Color DarkKhaki
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkKhaki>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkKhaki>k__BackingField = value;
			}
		} = new Color(4285249469U);

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00007D62 File Offset: 0x00005F62
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x00007D69 File Offset: 0x00005F69
		public static Color DarkMagenta
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkMagenta>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkMagenta>k__BackingField = value;
			}
		} = new Color(4287299723U);

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00007D71 File Offset: 0x00005F71
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x00007D78 File Offset: 0x00005F78
		public static Color DarkOliveGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkOliveGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkOliveGreen>k__BackingField = value;
			}
		} = new Color(4281297749U);

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00007D80 File Offset: 0x00005F80
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00007D87 File Offset: 0x00005F87
		public static Color DarkOrange
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkOrange>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkOrange>k__BackingField = value;
			}
		} = new Color(4278226175U);

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00007D8F File Offset: 0x00005F8F
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00007D96 File Offset: 0x00005F96
		public static Color DarkOrchid
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkOrchid>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkOrchid>k__BackingField = value;
			}
		} = new Color(4291572377U);

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00007D9E File Offset: 0x00005F9E
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x00007DA5 File Offset: 0x00005FA5
		public static Color DarkRed
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkRed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkRed>k__BackingField = value;
			}
		} = new Color(4278190219U);

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00007DAD File Offset: 0x00005FAD
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public static Color DarkSalmon
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkSalmon>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkSalmon>k__BackingField = value;
			}
		} = new Color(4286224105U);

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00007DBC File Offset: 0x00005FBC
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x00007DC3 File Offset: 0x00005FC3
		public static Color DarkSeaGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkSeaGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkSeaGreen>k__BackingField = value;
			}
		} = new Color(4287347855U);

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00007DCB File Offset: 0x00005FCB
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00007DD2 File Offset: 0x00005FD2
		public static Color DarkSlateBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkSlateBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkSlateBlue>k__BackingField = value;
			}
		} = new Color(4287315272U);

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00007DDA File Offset: 0x00005FDA
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x00007DE1 File Offset: 0x00005FE1
		public static Color DarkSlateGray
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkSlateGray>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkSlateGray>k__BackingField = value;
			}
		} = new Color(4283387695U);

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00007DE9 File Offset: 0x00005FE9
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public static Color DarkTurquoise
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkTurquoise>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkTurquoise>k__BackingField = value;
			}
		} = new Color(4291939840U);

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00007DF8 File Offset: 0x00005FF8
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x00007DFF File Offset: 0x00005FFF
		public static Color DarkViolet
		{
			[CompilerGenerated]
			get
			{
				return Color.<DarkViolet>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DarkViolet>k__BackingField = value;
			}
		} = new Color(4292018324U);

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00007E07 File Offset: 0x00006007
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00007E0E File Offset: 0x0000600E
		public static Color DeepPink
		{
			[CompilerGenerated]
			get
			{
				return Color.<DeepPink>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DeepPink>k__BackingField = value;
			}
		} = new Color(4287829247U);

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00007E16 File Offset: 0x00006016
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x00007E1D File Offset: 0x0000601D
		public static Color DeepSkyBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<DeepSkyBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DeepSkyBlue>k__BackingField = value;
			}
		} = new Color(4294950656U);

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00007E25 File Offset: 0x00006025
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x00007E2C File Offset: 0x0000602C
		public static Color DimGray
		{
			[CompilerGenerated]
			get
			{
				return Color.<DimGray>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DimGray>k__BackingField = value;
			}
		} = new Color(4285098345U);

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00007E34 File Offset: 0x00006034
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x00007E3B File Offset: 0x0000603B
		public static Color DodgerBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<DodgerBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<DodgerBlue>k__BackingField = value;
			}
		} = new Color(4294938654U);

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00007E43 File Offset: 0x00006043
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x00007E4A File Offset: 0x0000604A
		public static Color Firebrick
		{
			[CompilerGenerated]
			get
			{
				return Color.<Firebrick>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Firebrick>k__BackingField = value;
			}
		} = new Color(4280427186U);

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00007E52 File Offset: 0x00006052
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00007E59 File Offset: 0x00006059
		public static Color FloralWhite
		{
			[CompilerGenerated]
			get
			{
				return Color.<FloralWhite>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<FloralWhite>k__BackingField = value;
			}
		} = new Color(4293982975U);

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00007E61 File Offset: 0x00006061
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x00007E68 File Offset: 0x00006068
		public static Color ForestGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<ForestGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<ForestGreen>k__BackingField = value;
			}
		} = new Color(4280453922U);

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00007E70 File Offset: 0x00006070
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x00007E77 File Offset: 0x00006077
		public static Color Fuchsia
		{
			[CompilerGenerated]
			get
			{
				return Color.<Fuchsia>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Fuchsia>k__BackingField = value;
			}
		} = new Color(4294902015U);

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00007E7F File Offset: 0x0000607F
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x00007E86 File Offset: 0x00006086
		public static Color Gainsboro
		{
			[CompilerGenerated]
			get
			{
				return Color.<Gainsboro>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Gainsboro>k__BackingField = value;
			}
		} = new Color(4292664540U);

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00007E8E File Offset: 0x0000608E
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x00007E95 File Offset: 0x00006095
		public static Color GhostWhite
		{
			[CompilerGenerated]
			get
			{
				return Color.<GhostWhite>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<GhostWhite>k__BackingField = value;
			}
		} = new Color(4294965496U);

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00007E9D File Offset: 0x0000609D
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x00007EA4 File Offset: 0x000060A4
		public static Color Gold
		{
			[CompilerGenerated]
			get
			{
				return Color.<Gold>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Gold>k__BackingField = value;
			}
		} = new Color(4278245375U);

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00007EAC File Offset: 0x000060AC
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00007EB3 File Offset: 0x000060B3
		public static Color Goldenrod
		{
			[CompilerGenerated]
			get
			{
				return Color.<Goldenrod>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Goldenrod>k__BackingField = value;
			}
		} = new Color(4280329690U);

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00007EBB File Offset: 0x000060BB
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00007EC2 File Offset: 0x000060C2
		public static Color Gray
		{
			[CompilerGenerated]
			get
			{
				return Color.<Gray>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Gray>k__BackingField = value;
			}
		} = new Color(4286611584U);

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00007ECA File Offset: 0x000060CA
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x00007ED1 File Offset: 0x000060D1
		public static Color Green
		{
			[CompilerGenerated]
			get
			{
				return Color.<Green>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Green>k__BackingField = value;
			}
		} = new Color(4278222848U);

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00007ED9 File Offset: 0x000060D9
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00007EE0 File Offset: 0x000060E0
		public static Color GreenYellow
		{
			[CompilerGenerated]
			get
			{
				return Color.<GreenYellow>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<GreenYellow>k__BackingField = value;
			}
		} = new Color(4281335725U);

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00007EE8 File Offset: 0x000060E8
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00007EEF File Offset: 0x000060EF
		public static Color Honeydew
		{
			[CompilerGenerated]
			get
			{
				return Color.<Honeydew>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Honeydew>k__BackingField = value;
			}
		} = new Color(4293984240U);

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00007EF7 File Offset: 0x000060F7
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00007EFE File Offset: 0x000060FE
		public static Color HotPink
		{
			[CompilerGenerated]
			get
			{
				return Color.<HotPink>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<HotPink>k__BackingField = value;
			}
		} = new Color(4290013695U);

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00007F06 File Offset: 0x00006106
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x00007F0D File Offset: 0x0000610D
		public static Color IndianRed
		{
			[CompilerGenerated]
			get
			{
				return Color.<IndianRed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<IndianRed>k__BackingField = value;
			}
		} = new Color(4284243149U);

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00007F15 File Offset: 0x00006115
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x00007F1C File Offset: 0x0000611C
		public static Color Indigo
		{
			[CompilerGenerated]
			get
			{
				return Color.<Indigo>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Indigo>k__BackingField = value;
			}
		} = new Color(4286709835U);

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00007F24 File Offset: 0x00006124
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x00007F2B File Offset: 0x0000612B
		public static Color Ivory
		{
			[CompilerGenerated]
			get
			{
				return Color.<Ivory>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Ivory>k__BackingField = value;
			}
		} = new Color(4293984255U);

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00007F33 File Offset: 0x00006133
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x00007F3A File Offset: 0x0000613A
		public static Color Khaki
		{
			[CompilerGenerated]
			get
			{
				return Color.<Khaki>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Khaki>k__BackingField = value;
			}
		} = new Color(4287424240U);

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00007F42 File Offset: 0x00006142
		// (set) Token: 0x060009B5 RID: 2485 RVA: 0x00007F49 File Offset: 0x00006149
		public static Color Lavender
		{
			[CompilerGenerated]
			get
			{
				return Color.<Lavender>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Lavender>k__BackingField = value;
			}
		} = new Color(4294633190U);

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00007F51 File Offset: 0x00006151
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x00007F58 File Offset: 0x00006158
		public static Color LavenderBlush
		{
			[CompilerGenerated]
			get
			{
				return Color.<LavenderBlush>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LavenderBlush>k__BackingField = value;
			}
		} = new Color(4294308095U);

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00007F60 File Offset: 0x00006160
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00007F67 File Offset: 0x00006167
		public static Color LawnGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<LawnGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LawnGreen>k__BackingField = value;
			}
		} = new Color(4278254716U);

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00007F6F File Offset: 0x0000616F
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x00007F76 File Offset: 0x00006176
		public static Color LemonChiffon
		{
			[CompilerGenerated]
			get
			{
				return Color.<LemonChiffon>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LemonChiffon>k__BackingField = value;
			}
		} = new Color(4291689215U);

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00007F7E File Offset: 0x0000617E
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x00007F85 File Offset: 0x00006185
		public static Color LightBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightBlue>k__BackingField = value;
			}
		} = new Color(4293318829U);

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00007F8D File Offset: 0x0000618D
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x00007F94 File Offset: 0x00006194
		public static Color LightCoral
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightCoral>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightCoral>k__BackingField = value;
			}
		} = new Color(4286611696U);

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00007F9C File Offset: 0x0000619C
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x00007FA3 File Offset: 0x000061A3
		public static Color LightCyan
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightCyan>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightCyan>k__BackingField = value;
			}
		} = new Color(4294967264U);

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00007FAB File Offset: 0x000061AB
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x00007FB2 File Offset: 0x000061B2
		public static Color LightGoldenrodYellow
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightGoldenrodYellow>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightGoldenrodYellow>k__BackingField = value;
			}
		} = new Color(4292016890U);

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00007FBA File Offset: 0x000061BA
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00007FC1 File Offset: 0x000061C1
		public static Color LightGray
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightGray>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightGray>k__BackingField = value;
			}
		} = new Color(4292072403U);

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00007FC9 File Offset: 0x000061C9
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x00007FD0 File Offset: 0x000061D0
		public static Color LightGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightGreen>k__BackingField = value;
			}
		} = new Color(4287688336U);

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00007FD8 File Offset: 0x000061D8
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x00007FDF File Offset: 0x000061DF
		public static Color LightPink
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightPink>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightPink>k__BackingField = value;
			}
		} = new Color(4290885375U);

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00007FE7 File Offset: 0x000061E7
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x00007FEE File Offset: 0x000061EE
		public static Color LightSalmon
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightSalmon>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightSalmon>k__BackingField = value;
			}
		} = new Color(4286226687U);

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00007FF6 File Offset: 0x000061F6
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x00007FFD File Offset: 0x000061FD
		public static Color LightSeaGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightSeaGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightSeaGreen>k__BackingField = value;
			}
		} = new Color(4289376800U);

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00008005 File Offset: 0x00006205
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x0000800C File Offset: 0x0000620C
		public static Color LightSkyBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightSkyBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightSkyBlue>k__BackingField = value;
			}
		} = new Color(4294626951U);

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00008014 File Offset: 0x00006214
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x0000801B File Offset: 0x0000621B
		public static Color LightSlateGray
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightSlateGray>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightSlateGray>k__BackingField = value;
			}
		} = new Color(4288252023U);

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00008023 File Offset: 0x00006223
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x0000802A File Offset: 0x0000622A
		public static Color LightSteelBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightSteelBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightSteelBlue>k__BackingField = value;
			}
		} = new Color(4292789424U);

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00008032 File Offset: 0x00006232
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x00008039 File Offset: 0x00006239
		public static Color LightYellow
		{
			[CompilerGenerated]
			get
			{
				return Color.<LightYellow>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LightYellow>k__BackingField = value;
			}
		} = new Color(4292935679U);

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00008041 File Offset: 0x00006241
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00008048 File Offset: 0x00006248
		public static Color Lime
		{
			[CompilerGenerated]
			get
			{
				return Color.<Lime>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Lime>k__BackingField = value;
			}
		} = new Color(4278255360U);

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00008050 File Offset: 0x00006250
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x00008057 File Offset: 0x00006257
		public static Color LimeGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<LimeGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<LimeGreen>k__BackingField = value;
			}
		} = new Color(4281519410U);

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0000805F File Offset: 0x0000625F
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x00008066 File Offset: 0x00006266
		public static Color Linen
		{
			[CompilerGenerated]
			get
			{
				return Color.<Linen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Linen>k__BackingField = value;
			}
		} = new Color(4293325050U);

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0000806E File Offset: 0x0000626E
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00008075 File Offset: 0x00006275
		public static Color Magenta
		{
			[CompilerGenerated]
			get
			{
				return Color.<Magenta>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Magenta>k__BackingField = value;
			}
		} = new Color(4294902015U);

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0000807D File Offset: 0x0000627D
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00008084 File Offset: 0x00006284
		public static Color Maroon
		{
			[CompilerGenerated]
			get
			{
				return Color.<Maroon>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Maroon>k__BackingField = value;
			}
		} = new Color(4278190208U);

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0000808C File Offset: 0x0000628C
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00008093 File Offset: 0x00006293
		public static Color MediumAquamarine
		{
			[CompilerGenerated]
			get
			{
				return Color.<MediumAquamarine>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MediumAquamarine>k__BackingField = value;
			}
		} = new Color(4289383782U);

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0000809B File Offset: 0x0000629B
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x000080A2 File Offset: 0x000062A2
		public static Color MediumBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<MediumBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MediumBlue>k__BackingField = value;
			}
		} = new Color(4291624960U);

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x000080AA File Offset: 0x000062AA
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x000080B1 File Offset: 0x000062B1
		public static Color MediumOrchid
		{
			[CompilerGenerated]
			get
			{
				return Color.<MediumOrchid>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MediumOrchid>k__BackingField = value;
			}
		} = new Color(4292040122U);

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x000080B9 File Offset: 0x000062B9
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x000080C0 File Offset: 0x000062C0
		public static Color MediumPurple
		{
			[CompilerGenerated]
			get
			{
				return Color.<MediumPurple>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MediumPurple>k__BackingField = value;
			}
		} = new Color(4292571283U);

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x000080C8 File Offset: 0x000062C8
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x000080CF File Offset: 0x000062CF
		public static Color MediumSeaGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<MediumSeaGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MediumSeaGreen>k__BackingField = value;
			}
		} = new Color(4285641532U);

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x000080D7 File Offset: 0x000062D7
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x000080DE File Offset: 0x000062DE
		public static Color MediumSlateBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<MediumSlateBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MediumSlateBlue>k__BackingField = value;
			}
		} = new Color(4293814395U);

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x000080E6 File Offset: 0x000062E6
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x000080ED File Offset: 0x000062ED
		public static Color MediumSpringGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<MediumSpringGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MediumSpringGreen>k__BackingField = value;
			}
		} = new Color(4288346624U);

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x000080F5 File Offset: 0x000062F5
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x000080FC File Offset: 0x000062FC
		public static Color MediumTurquoise
		{
			[CompilerGenerated]
			get
			{
				return Color.<MediumTurquoise>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MediumTurquoise>k__BackingField = value;
			}
		} = new Color(4291613000U);

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00008104 File Offset: 0x00006304
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0000810B File Offset: 0x0000630B
		public static Color MediumVioletRed
		{
			[CompilerGenerated]
			get
			{
				return Color.<MediumVioletRed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MediumVioletRed>k__BackingField = value;
			}
		} = new Color(4286911943U);

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00008113 File Offset: 0x00006313
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0000811A File Offset: 0x0000631A
		public static Color MidnightBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<MidnightBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MidnightBlue>k__BackingField = value;
			}
		} = new Color(4285536537U);

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00008122 File Offset: 0x00006322
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x00008129 File Offset: 0x00006329
		public static Color MintCream
		{
			[CompilerGenerated]
			get
			{
				return Color.<MintCream>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MintCream>k__BackingField = value;
			}
		} = new Color(4294639605U);

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00008131 File Offset: 0x00006331
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x00008138 File Offset: 0x00006338
		public static Color MistyRose
		{
			[CompilerGenerated]
			get
			{
				return Color.<MistyRose>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<MistyRose>k__BackingField = value;
			}
		} = new Color(4292994303U);

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00008140 File Offset: 0x00006340
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x00008147 File Offset: 0x00006347
		public static Color Moccasin
		{
			[CompilerGenerated]
			get
			{
				return Color.<Moccasin>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Moccasin>k__BackingField = value;
			}
		} = new Color(4290110719U);

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0000814F File Offset: 0x0000634F
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x00008156 File Offset: 0x00006356
		public static Color NavajoWhite
		{
			[CompilerGenerated]
			get
			{
				return Color.<NavajoWhite>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<NavajoWhite>k__BackingField = value;
			}
		} = new Color(4289584895U);

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0000815E File Offset: 0x0000635E
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x00008165 File Offset: 0x00006365
		public static Color Navy
		{
			[CompilerGenerated]
			get
			{
				return Color.<Navy>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Navy>k__BackingField = value;
			}
		} = new Color(4286578688U);

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0000816D File Offset: 0x0000636D
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x00008174 File Offset: 0x00006374
		public static Color OldLace
		{
			[CompilerGenerated]
			get
			{
				return Color.<OldLace>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<OldLace>k__BackingField = value;
			}
		} = new Color(4293326333U);

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0000817C File Offset: 0x0000637C
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x00008183 File Offset: 0x00006383
		public static Color Olive
		{
			[CompilerGenerated]
			get
			{
				return Color.<Olive>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Olive>k__BackingField = value;
			}
		} = new Color(4278222976U);

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0000818B File Offset: 0x0000638B
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x00008192 File Offset: 0x00006392
		public static Color OliveDrab
		{
			[CompilerGenerated]
			get
			{
				return Color.<OliveDrab>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<OliveDrab>k__BackingField = value;
			}
		} = new Color(4280520299U);

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0000819A File Offset: 0x0000639A
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x000081A1 File Offset: 0x000063A1
		public static Color Orange
		{
			[CompilerGenerated]
			get
			{
				return Color.<Orange>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Orange>k__BackingField = value;
			}
		} = new Color(4278232575U);

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x000081A9 File Offset: 0x000063A9
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x000081B0 File Offset: 0x000063B0
		public static Color OrangeRed
		{
			[CompilerGenerated]
			get
			{
				return Color.<OrangeRed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<OrangeRed>k__BackingField = value;
			}
		} = new Color(4278207999U);

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x000081B8 File Offset: 0x000063B8
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x000081BF File Offset: 0x000063BF
		public static Color Orchid
		{
			[CompilerGenerated]
			get
			{
				return Color.<Orchid>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Orchid>k__BackingField = value;
			}
		} = new Color(4292243674U);

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x000081C7 File Offset: 0x000063C7
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x000081CE File Offset: 0x000063CE
		public static Color PaleGoldenrod
		{
			[CompilerGenerated]
			get
			{
				return Color.<PaleGoldenrod>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<PaleGoldenrod>k__BackingField = value;
			}
		} = new Color(4289390830U);

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x000081D6 File Offset: 0x000063D6
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x000081DD File Offset: 0x000063DD
		public static Color PaleGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<PaleGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<PaleGreen>k__BackingField = value;
			}
		} = new Color(4288215960U);

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x000081E5 File Offset: 0x000063E5
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x000081EC File Offset: 0x000063EC
		public static Color PaleTurquoise
		{
			[CompilerGenerated]
			get
			{
				return Color.<PaleTurquoise>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<PaleTurquoise>k__BackingField = value;
			}
		} = new Color(4293848751U);

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x000081F4 File Offset: 0x000063F4
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x000081FB File Offset: 0x000063FB
		public static Color PaleVioletRed
		{
			[CompilerGenerated]
			get
			{
				return Color.<PaleVioletRed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<PaleVioletRed>k__BackingField = value;
			}
		} = new Color(4287852763U);

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00008203 File Offset: 0x00006403
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x0000820A File Offset: 0x0000640A
		public static Color PapayaWhip
		{
			[CompilerGenerated]
			get
			{
				return Color.<PapayaWhip>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<PapayaWhip>k__BackingField = value;
			}
		} = new Color(4292210687U);

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00008212 File Offset: 0x00006412
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x00008219 File Offset: 0x00006419
		public static Color PeachPuff
		{
			[CompilerGenerated]
			get
			{
				return Color.<PeachPuff>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<PeachPuff>k__BackingField = value;
			}
		} = new Color(4290370303U);

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00008221 File Offset: 0x00006421
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x00008228 File Offset: 0x00006428
		public static Color Peru
		{
			[CompilerGenerated]
			get
			{
				return Color.<Peru>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Peru>k__BackingField = value;
			}
		} = new Color(4282353101U);

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00008230 File Offset: 0x00006430
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x00008237 File Offset: 0x00006437
		public static Color Pink
		{
			[CompilerGenerated]
			get
			{
				return Color.<Pink>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Pink>k__BackingField = value;
			}
		} = new Color(4291543295U);

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0000823F File Offset: 0x0000643F
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x00008246 File Offset: 0x00006446
		public static Color Plum
		{
			[CompilerGenerated]
			get
			{
				return Color.<Plum>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Plum>k__BackingField = value;
			}
		} = new Color(4292714717U);

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0000824E File Offset: 0x0000644E
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x00008255 File Offset: 0x00006455
		public static Color PowderBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<PowderBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<PowderBlue>k__BackingField = value;
			}
		} = new Color(4293320880U);

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0000825D File Offset: 0x0000645D
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x00008264 File Offset: 0x00006464
		public static Color Purple
		{
			[CompilerGenerated]
			get
			{
				return Color.<Purple>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Purple>k__BackingField = value;
			}
		} = new Color(4286578816U);

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0000826C File Offset: 0x0000646C
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x00008273 File Offset: 0x00006473
		public static Color Red
		{
			[CompilerGenerated]
			get
			{
				return Color.<Red>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Red>k__BackingField = value;
			}
		} = new Color(4278190335U);

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0000827B File Offset: 0x0000647B
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00008282 File Offset: 0x00006482
		public static Color RosyBrown
		{
			[CompilerGenerated]
			get
			{
				return Color.<RosyBrown>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<RosyBrown>k__BackingField = value;
			}
		} = new Color(4287598524U);

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0000828A File Offset: 0x0000648A
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00008291 File Offset: 0x00006491
		public static Color RoyalBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<RoyalBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<RoyalBlue>k__BackingField = value;
			}
		} = new Color(4292962625U);

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00008299 File Offset: 0x00006499
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x000082A0 File Offset: 0x000064A0
		public static Color SaddleBrown
		{
			[CompilerGenerated]
			get
			{
				return Color.<SaddleBrown>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<SaddleBrown>k__BackingField = value;
			}
		} = new Color(4279453067U);

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x000082A8 File Offset: 0x000064A8
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x000082AF File Offset: 0x000064AF
		public static Color Salmon
		{
			[CompilerGenerated]
			get
			{
				return Color.<Salmon>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Salmon>k__BackingField = value;
			}
		} = new Color(4285694202U);

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x000082B7 File Offset: 0x000064B7
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x000082BE File Offset: 0x000064BE
		public static Color SandyBrown
		{
			[CompilerGenerated]
			get
			{
				return Color.<SandyBrown>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<SandyBrown>k__BackingField = value;
			}
		} = new Color(4284523764U);

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x000082C6 File Offset: 0x000064C6
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x000082CD File Offset: 0x000064CD
		public static Color SeaGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<SeaGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<SeaGreen>k__BackingField = value;
			}
		} = new Color(4283927342U);

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x000082D5 File Offset: 0x000064D5
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x000082DC File Offset: 0x000064DC
		public static Color SeaShell
		{
			[CompilerGenerated]
			get
			{
				return Color.<SeaShell>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<SeaShell>k__BackingField = value;
			}
		} = new Color(4293850623U);

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x000082E4 File Offset: 0x000064E4
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x000082EB File Offset: 0x000064EB
		public static Color Sienna
		{
			[CompilerGenerated]
			get
			{
				return Color.<Sienna>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Sienna>k__BackingField = value;
			}
		} = new Color(4281160352U);

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x000082F3 File Offset: 0x000064F3
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x000082FA File Offset: 0x000064FA
		public static Color Silver
		{
			[CompilerGenerated]
			get
			{
				return Color.<Silver>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Silver>k__BackingField = value;
			}
		} = new Color(4290822336U);

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00008302 File Offset: 0x00006502
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x00008309 File Offset: 0x00006509
		public static Color SkyBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<SkyBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<SkyBlue>k__BackingField = value;
			}
		} = new Color(4293643911U);

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00008311 File Offset: 0x00006511
		// (set) Token: 0x06000A37 RID: 2615 RVA: 0x00008318 File Offset: 0x00006518
		public static Color SlateBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<SlateBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<SlateBlue>k__BackingField = value;
			}
		} = new Color(4291648106U);

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00008320 File Offset: 0x00006520
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x00008327 File Offset: 0x00006527
		public static Color SlateGray
		{
			[CompilerGenerated]
			get
			{
				return Color.<SlateGray>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<SlateGray>k__BackingField = value;
			}
		} = new Color(4287660144U);

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0000832F File Offset: 0x0000652F
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x00008336 File Offset: 0x00006536
		public static Color Snow
		{
			[CompilerGenerated]
			get
			{
				return Color.<Snow>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Snow>k__BackingField = value;
			}
		} = new Color(4294638335U);

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0000833E File Offset: 0x0000653E
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x00008345 File Offset: 0x00006545
		public static Color SpringGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<SpringGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<SpringGreen>k__BackingField = value;
			}
		} = new Color(4286578432U);

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0000834D File Offset: 0x0000654D
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x00008354 File Offset: 0x00006554
		public static Color SteelBlue
		{
			[CompilerGenerated]
			get
			{
				return Color.<SteelBlue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<SteelBlue>k__BackingField = value;
			}
		} = new Color(4290019910U);

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0000835C File Offset: 0x0000655C
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x00008363 File Offset: 0x00006563
		public static Color Tan
		{
			[CompilerGenerated]
			get
			{
				return Color.<Tan>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Tan>k__BackingField = value;
			}
		} = new Color(4287411410U);

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0000836B File Offset: 0x0000656B
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x00008372 File Offset: 0x00006572
		public static Color Teal
		{
			[CompilerGenerated]
			get
			{
				return Color.<Teal>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Teal>k__BackingField = value;
			}
		} = new Color(4286611456U);

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0000837A File Offset: 0x0000657A
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x00008381 File Offset: 0x00006581
		public static Color Thistle
		{
			[CompilerGenerated]
			get
			{
				return Color.<Thistle>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Thistle>k__BackingField = value;
			}
		} = new Color(4292394968U);

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00008389 File Offset: 0x00006589
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x00008390 File Offset: 0x00006590
		public static Color Tomato
		{
			[CompilerGenerated]
			get
			{
				return Color.<Tomato>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Tomato>k__BackingField = value;
			}
		} = new Color(4282868735U);

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00008398 File Offset: 0x00006598
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x0000839F File Offset: 0x0000659F
		public static Color Turquoise
		{
			[CompilerGenerated]
			get
			{
				return Color.<Turquoise>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Turquoise>k__BackingField = value;
			}
		} = new Color(4291878976U);

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x000083A7 File Offset: 0x000065A7
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x000083AE File Offset: 0x000065AE
		public static Color Violet
		{
			[CompilerGenerated]
			get
			{
				return Color.<Violet>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Violet>k__BackingField = value;
			}
		} = new Color(4293821166U);

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x000083B6 File Offset: 0x000065B6
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x000083BD File Offset: 0x000065BD
		public static Color Wheat
		{
			[CompilerGenerated]
			get
			{
				return Color.<Wheat>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Wheat>k__BackingField = value;
			}
		} = new Color(4289978101U);

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x000083C5 File Offset: 0x000065C5
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x000083CC File Offset: 0x000065CC
		public static Color White
		{
			[CompilerGenerated]
			get
			{
				return Color.<White>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<White>k__BackingField = value;
			}
		} = new Color(uint.MaxValue);

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000083D4 File Offset: 0x000065D4
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x000083DB File Offset: 0x000065DB
		public static Color WhiteSmoke
		{
			[CompilerGenerated]
			get
			{
				return Color.<WhiteSmoke>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<WhiteSmoke>k__BackingField = value;
			}
		} = new Color(4294309365U);

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x000083E3 File Offset: 0x000065E3
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x000083EA File Offset: 0x000065EA
		public static Color Yellow
		{
			[CompilerGenerated]
			get
			{
				return Color.<Yellow>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<Yellow>k__BackingField = value;
			}
		} = new Color(4278255615U);

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x000083F2 File Offset: 0x000065F2
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x000083F9 File Offset: 0x000065F9
		public static Color YellowGreen
		{
			[CompilerGenerated]
			get
			{
				return Color.<YellowGreen>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Color.<YellowGreen>k__BackingField = value;
			}
		} = new Color(4281519514U);

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00008404 File Offset: 0x00006604
		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(new string[]
				{
					this.R.ToString(),
					" ",
					this.G.ToString(),
					" ",
					this.B.ToString(),
					" ",
					this.A.ToString()
				});
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00008478 File Offset: 0x00006678
		static Color()
		{
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00008CBC File Offset: 0x00006EBC
		public Color(Vector4 color)
		{
			this.packedValue = 0U;
			this.R = (byte)MathHelper.Clamp(color.X * 255f, 0f, 255f);
			this.G = (byte)MathHelper.Clamp(color.Y * 255f, 0f, 255f);
			this.B = (byte)MathHelper.Clamp(color.Z * 255f, 0f, 255f);
			this.A = (byte)MathHelper.Clamp(color.W * 255f, 0f, 255f);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00008D58 File Offset: 0x00006F58
		public Color(Vector3 color)
		{
			this.packedValue = 0U;
			this.R = (byte)MathHelper.Clamp(color.X * 255f, 0f, 255f);
			this.G = (byte)MathHelper.Clamp(color.Y * 255f, 0f, 255f);
			this.B = (byte)MathHelper.Clamp(color.Z * 255f, 0f, 255f);
			this.A = byte.MaxValue;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00008DE0 File Offset: 0x00006FE0
		public Color(float r, float g, float b)
		{
			this.packedValue = 0U;
			this.R = (byte)MathHelper.Clamp(r * 255f, 0f, 255f);
			this.G = (byte)MathHelper.Clamp(g * 255f, 0f, 255f);
			this.B = (byte)MathHelper.Clamp(b * 255f, 0f, 255f);
			this.A = byte.MaxValue;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00008E58 File Offset: 0x00007058
		public Color(int r, int g, int b)
		{
			this.packedValue = 0U;
			this.R = (byte)MathHelper.Clamp(r, 0, 255);
			this.G = (byte)MathHelper.Clamp(g, 0, 255);
			this.B = (byte)MathHelper.Clamp(b, 0, 255);
			this.A = byte.MaxValue;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00008EB0 File Offset: 0x000070B0
		public Color(int r, int g, int b, int alpha)
		{
			this.packedValue = 0U;
			this.R = (byte)MathHelper.Clamp(r, 0, 255);
			this.G = (byte)MathHelper.Clamp(g, 0, 255);
			this.B = (byte)MathHelper.Clamp(b, 0, 255);
			this.A = (byte)MathHelper.Clamp(alpha, 0, 255);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00008F14 File Offset: 0x00007114
		public Color(float r, float g, float b, float alpha)
		{
			this.packedValue = 0U;
			this.R = (byte)MathHelper.Clamp(r * 255f, 0f, 255f);
			this.G = (byte)MathHelper.Clamp(g * 255f, 0f, 255f);
			this.B = (byte)MathHelper.Clamp(b * 255f, 0f, 255f);
			this.A = (byte)MathHelper.Clamp(alpha * 255f, 0f, 255f);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00007BB5 File Offset: 0x00005DB5
		private Color(uint packedValue)
		{
			this.packedValue = packedValue;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00008F9D File Offset: 0x0000719D
		public bool Equals(Color other)
		{
			return this.PackedValue == other.PackedValue;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00008FAE File Offset: 0x000071AE
		public Vector3 ToVector3()
		{
			return new Vector3((float)this.R / 255f, (float)this.G / 255f, (float)this.B / 255f);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00008FDC File Offset: 0x000071DC
		public Vector4 ToVector4()
		{
			return new Vector4((float)this.R / 255f, (float)this.G / 255f, (float)this.B / 255f, (float)this.A / 255f);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00009018 File Offset: 0x00007218
		public static Color Lerp(Color value1, Color value2, float amount)
		{
			amount = MathHelper.Clamp(amount, 0f, 1f);
			return new Color((int)MathHelper.Lerp((float)value1.R, (float)value2.R, amount), (int)MathHelper.Lerp((float)value1.G, (float)value2.G, amount), (int)MathHelper.Lerp((float)value1.B, (float)value2.B, amount), (int)MathHelper.Lerp((float)value1.A, (float)value2.A, amount));
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00009098 File Offset: 0x00007298
		public static Color FromNonPremultiplied(Vector4 vector)
		{
			return new Color(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W, vector.W);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000090CC File Offset: 0x000072CC
		public static Color FromNonPremultiplied(int r, int g, int b, int a)
		{
			return new Color(r * a / 255, g * a / 255, b * a / 255, a);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000090F0 File Offset: 0x000072F0
		public static bool operator ==(Color a, Color b)
		{
			return a.A == b.A && a.R == b.R && a.G == b.G && a.B == b.B;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0000913F File Offset: 0x0000733F
		public static bool operator !=(Color a, Color b)
		{
			return !(a == b);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0000914B File Offset: 0x0000734B
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00009158 File Offset: 0x00007358
		public override bool Equals(object obj)
		{
			return obj is Color && this.Equals((Color)obj);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00009170 File Offset: 0x00007370
		public static Color Multiply(Color value, float scale)
		{
			return new Color((int)((float)value.R * scale), (int)((float)value.G * scale), (int)((float)value.B * scale), (int)((float)value.A * scale));
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00009170 File Offset: 0x00007370
		public static Color operator *(Color value, float scale)
		{
			return new Color((int)((float)value.R * scale), (int)((float)value.G * scale), (int)((float)value.B * scale), (int)((float)value.A * scale));
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000091A4 File Offset: 0x000073A4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(25);
			stringBuilder.Append("{R:");
			stringBuilder.Append(this.R);
			stringBuilder.Append(" G:");
			stringBuilder.Append(this.G);
			stringBuilder.Append(" B:");
			stringBuilder.Append(this.B);
			stringBuilder.Append(" A:");
			stringBuilder.Append(this.A);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00009230 File Offset: 0x00007430
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.R = (byte)(vector.X * 255f);
			this.G = (byte)(vector.Y * 255f);
			this.B = (byte)(vector.Z * 255f);
			this.A = (byte)(vector.W * 255f);
		}

		// Token: 0x04000413 RID: 1043
		[CompilerGenerated]
		private static Color <Transparent>k__BackingField;

		// Token: 0x04000414 RID: 1044
		[CompilerGenerated]
		private static Color <AliceBlue>k__BackingField;

		// Token: 0x04000415 RID: 1045
		[CompilerGenerated]
		private static Color <AntiqueWhite>k__BackingField;

		// Token: 0x04000416 RID: 1046
		[CompilerGenerated]
		private static Color <Aqua>k__BackingField;

		// Token: 0x04000417 RID: 1047
		[CompilerGenerated]
		private static Color <Aquamarine>k__BackingField;

		// Token: 0x04000418 RID: 1048
		[CompilerGenerated]
		private static Color <Azure>k__BackingField;

		// Token: 0x04000419 RID: 1049
		[CompilerGenerated]
		private static Color <Beige>k__BackingField;

		// Token: 0x0400041A RID: 1050
		[CompilerGenerated]
		private static Color <Bisque>k__BackingField;

		// Token: 0x0400041B RID: 1051
		[CompilerGenerated]
		private static Color <Black>k__BackingField;

		// Token: 0x0400041C RID: 1052
		[CompilerGenerated]
		private static Color <BlanchedAlmond>k__BackingField;

		// Token: 0x0400041D RID: 1053
		[CompilerGenerated]
		private static Color <Blue>k__BackingField;

		// Token: 0x0400041E RID: 1054
		[CompilerGenerated]
		private static Color <BlueViolet>k__BackingField;

		// Token: 0x0400041F RID: 1055
		[CompilerGenerated]
		private static Color <Brown>k__BackingField;

		// Token: 0x04000420 RID: 1056
		[CompilerGenerated]
		private static Color <BurlyWood>k__BackingField;

		// Token: 0x04000421 RID: 1057
		[CompilerGenerated]
		private static Color <CadetBlue>k__BackingField;

		// Token: 0x04000422 RID: 1058
		[CompilerGenerated]
		private static Color <Chartreuse>k__BackingField;

		// Token: 0x04000423 RID: 1059
		[CompilerGenerated]
		private static Color <Chocolate>k__BackingField;

		// Token: 0x04000424 RID: 1060
		[CompilerGenerated]
		private static Color <Coral>k__BackingField;

		// Token: 0x04000425 RID: 1061
		[CompilerGenerated]
		private static Color <CornflowerBlue>k__BackingField;

		// Token: 0x04000426 RID: 1062
		[CompilerGenerated]
		private static Color <Cornsilk>k__BackingField;

		// Token: 0x04000427 RID: 1063
		[CompilerGenerated]
		private static Color <Crimson>k__BackingField;

		// Token: 0x04000428 RID: 1064
		[CompilerGenerated]
		private static Color <Cyan>k__BackingField;

		// Token: 0x04000429 RID: 1065
		[CompilerGenerated]
		private static Color <DarkBlue>k__BackingField;

		// Token: 0x0400042A RID: 1066
		[CompilerGenerated]
		private static Color <DarkCyan>k__BackingField;

		// Token: 0x0400042B RID: 1067
		[CompilerGenerated]
		private static Color <DarkGoldenrod>k__BackingField;

		// Token: 0x0400042C RID: 1068
		[CompilerGenerated]
		private static Color <DarkGray>k__BackingField;

		// Token: 0x0400042D RID: 1069
		[CompilerGenerated]
		private static Color <DarkGreen>k__BackingField;

		// Token: 0x0400042E RID: 1070
		[CompilerGenerated]
		private static Color <DarkKhaki>k__BackingField;

		// Token: 0x0400042F RID: 1071
		[CompilerGenerated]
		private static Color <DarkMagenta>k__BackingField;

		// Token: 0x04000430 RID: 1072
		[CompilerGenerated]
		private static Color <DarkOliveGreen>k__BackingField;

		// Token: 0x04000431 RID: 1073
		[CompilerGenerated]
		private static Color <DarkOrange>k__BackingField;

		// Token: 0x04000432 RID: 1074
		[CompilerGenerated]
		private static Color <DarkOrchid>k__BackingField;

		// Token: 0x04000433 RID: 1075
		[CompilerGenerated]
		private static Color <DarkRed>k__BackingField;

		// Token: 0x04000434 RID: 1076
		[CompilerGenerated]
		private static Color <DarkSalmon>k__BackingField;

		// Token: 0x04000435 RID: 1077
		[CompilerGenerated]
		private static Color <DarkSeaGreen>k__BackingField;

		// Token: 0x04000436 RID: 1078
		[CompilerGenerated]
		private static Color <DarkSlateBlue>k__BackingField;

		// Token: 0x04000437 RID: 1079
		[CompilerGenerated]
		private static Color <DarkSlateGray>k__BackingField;

		// Token: 0x04000438 RID: 1080
		[CompilerGenerated]
		private static Color <DarkTurquoise>k__BackingField;

		// Token: 0x04000439 RID: 1081
		[CompilerGenerated]
		private static Color <DarkViolet>k__BackingField;

		// Token: 0x0400043A RID: 1082
		[CompilerGenerated]
		private static Color <DeepPink>k__BackingField;

		// Token: 0x0400043B RID: 1083
		[CompilerGenerated]
		private static Color <DeepSkyBlue>k__BackingField;

		// Token: 0x0400043C RID: 1084
		[CompilerGenerated]
		private static Color <DimGray>k__BackingField;

		// Token: 0x0400043D RID: 1085
		[CompilerGenerated]
		private static Color <DodgerBlue>k__BackingField;

		// Token: 0x0400043E RID: 1086
		[CompilerGenerated]
		private static Color <Firebrick>k__BackingField;

		// Token: 0x0400043F RID: 1087
		[CompilerGenerated]
		private static Color <FloralWhite>k__BackingField;

		// Token: 0x04000440 RID: 1088
		[CompilerGenerated]
		private static Color <ForestGreen>k__BackingField;

		// Token: 0x04000441 RID: 1089
		[CompilerGenerated]
		private static Color <Fuchsia>k__BackingField;

		// Token: 0x04000442 RID: 1090
		[CompilerGenerated]
		private static Color <Gainsboro>k__BackingField;

		// Token: 0x04000443 RID: 1091
		[CompilerGenerated]
		private static Color <GhostWhite>k__BackingField;

		// Token: 0x04000444 RID: 1092
		[CompilerGenerated]
		private static Color <Gold>k__BackingField;

		// Token: 0x04000445 RID: 1093
		[CompilerGenerated]
		private static Color <Goldenrod>k__BackingField;

		// Token: 0x04000446 RID: 1094
		[CompilerGenerated]
		private static Color <Gray>k__BackingField;

		// Token: 0x04000447 RID: 1095
		[CompilerGenerated]
		private static Color <Green>k__BackingField;

		// Token: 0x04000448 RID: 1096
		[CompilerGenerated]
		private static Color <GreenYellow>k__BackingField;

		// Token: 0x04000449 RID: 1097
		[CompilerGenerated]
		private static Color <Honeydew>k__BackingField;

		// Token: 0x0400044A RID: 1098
		[CompilerGenerated]
		private static Color <HotPink>k__BackingField;

		// Token: 0x0400044B RID: 1099
		[CompilerGenerated]
		private static Color <IndianRed>k__BackingField;

		// Token: 0x0400044C RID: 1100
		[CompilerGenerated]
		private static Color <Indigo>k__BackingField;

		// Token: 0x0400044D RID: 1101
		[CompilerGenerated]
		private static Color <Ivory>k__BackingField;

		// Token: 0x0400044E RID: 1102
		[CompilerGenerated]
		private static Color <Khaki>k__BackingField;

		// Token: 0x0400044F RID: 1103
		[CompilerGenerated]
		private static Color <Lavender>k__BackingField;

		// Token: 0x04000450 RID: 1104
		[CompilerGenerated]
		private static Color <LavenderBlush>k__BackingField;

		// Token: 0x04000451 RID: 1105
		[CompilerGenerated]
		private static Color <LawnGreen>k__BackingField;

		// Token: 0x04000452 RID: 1106
		[CompilerGenerated]
		private static Color <LemonChiffon>k__BackingField;

		// Token: 0x04000453 RID: 1107
		[CompilerGenerated]
		private static Color <LightBlue>k__BackingField;

		// Token: 0x04000454 RID: 1108
		[CompilerGenerated]
		private static Color <LightCoral>k__BackingField;

		// Token: 0x04000455 RID: 1109
		[CompilerGenerated]
		private static Color <LightCyan>k__BackingField;

		// Token: 0x04000456 RID: 1110
		[CompilerGenerated]
		private static Color <LightGoldenrodYellow>k__BackingField;

		// Token: 0x04000457 RID: 1111
		[CompilerGenerated]
		private static Color <LightGray>k__BackingField;

		// Token: 0x04000458 RID: 1112
		[CompilerGenerated]
		private static Color <LightGreen>k__BackingField;

		// Token: 0x04000459 RID: 1113
		[CompilerGenerated]
		private static Color <LightPink>k__BackingField;

		// Token: 0x0400045A RID: 1114
		[CompilerGenerated]
		private static Color <LightSalmon>k__BackingField;

		// Token: 0x0400045B RID: 1115
		[CompilerGenerated]
		private static Color <LightSeaGreen>k__BackingField;

		// Token: 0x0400045C RID: 1116
		[CompilerGenerated]
		private static Color <LightSkyBlue>k__BackingField;

		// Token: 0x0400045D RID: 1117
		[CompilerGenerated]
		private static Color <LightSlateGray>k__BackingField;

		// Token: 0x0400045E RID: 1118
		[CompilerGenerated]
		private static Color <LightSteelBlue>k__BackingField;

		// Token: 0x0400045F RID: 1119
		[CompilerGenerated]
		private static Color <LightYellow>k__BackingField;

		// Token: 0x04000460 RID: 1120
		[CompilerGenerated]
		private static Color <Lime>k__BackingField;

		// Token: 0x04000461 RID: 1121
		[CompilerGenerated]
		private static Color <LimeGreen>k__BackingField;

		// Token: 0x04000462 RID: 1122
		[CompilerGenerated]
		private static Color <Linen>k__BackingField;

		// Token: 0x04000463 RID: 1123
		[CompilerGenerated]
		private static Color <Magenta>k__BackingField;

		// Token: 0x04000464 RID: 1124
		[CompilerGenerated]
		private static Color <Maroon>k__BackingField;

		// Token: 0x04000465 RID: 1125
		[CompilerGenerated]
		private static Color <MediumAquamarine>k__BackingField;

		// Token: 0x04000466 RID: 1126
		[CompilerGenerated]
		private static Color <MediumBlue>k__BackingField;

		// Token: 0x04000467 RID: 1127
		[CompilerGenerated]
		private static Color <MediumOrchid>k__BackingField;

		// Token: 0x04000468 RID: 1128
		[CompilerGenerated]
		private static Color <MediumPurple>k__BackingField;

		// Token: 0x04000469 RID: 1129
		[CompilerGenerated]
		private static Color <MediumSeaGreen>k__BackingField;

		// Token: 0x0400046A RID: 1130
		[CompilerGenerated]
		private static Color <MediumSlateBlue>k__BackingField;

		// Token: 0x0400046B RID: 1131
		[CompilerGenerated]
		private static Color <MediumSpringGreen>k__BackingField;

		// Token: 0x0400046C RID: 1132
		[CompilerGenerated]
		private static Color <MediumTurquoise>k__BackingField;

		// Token: 0x0400046D RID: 1133
		[CompilerGenerated]
		private static Color <MediumVioletRed>k__BackingField;

		// Token: 0x0400046E RID: 1134
		[CompilerGenerated]
		private static Color <MidnightBlue>k__BackingField;

		// Token: 0x0400046F RID: 1135
		[CompilerGenerated]
		private static Color <MintCream>k__BackingField;

		// Token: 0x04000470 RID: 1136
		[CompilerGenerated]
		private static Color <MistyRose>k__BackingField;

		// Token: 0x04000471 RID: 1137
		[CompilerGenerated]
		private static Color <Moccasin>k__BackingField;

		// Token: 0x04000472 RID: 1138
		[CompilerGenerated]
		private static Color <NavajoWhite>k__BackingField;

		// Token: 0x04000473 RID: 1139
		[CompilerGenerated]
		private static Color <Navy>k__BackingField;

		// Token: 0x04000474 RID: 1140
		[CompilerGenerated]
		private static Color <OldLace>k__BackingField;

		// Token: 0x04000475 RID: 1141
		[CompilerGenerated]
		private static Color <Olive>k__BackingField;

		// Token: 0x04000476 RID: 1142
		[CompilerGenerated]
		private static Color <OliveDrab>k__BackingField;

		// Token: 0x04000477 RID: 1143
		[CompilerGenerated]
		private static Color <Orange>k__BackingField;

		// Token: 0x04000478 RID: 1144
		[CompilerGenerated]
		private static Color <OrangeRed>k__BackingField;

		// Token: 0x04000479 RID: 1145
		[CompilerGenerated]
		private static Color <Orchid>k__BackingField;

		// Token: 0x0400047A RID: 1146
		[CompilerGenerated]
		private static Color <PaleGoldenrod>k__BackingField;

		// Token: 0x0400047B RID: 1147
		[CompilerGenerated]
		private static Color <PaleGreen>k__BackingField;

		// Token: 0x0400047C RID: 1148
		[CompilerGenerated]
		private static Color <PaleTurquoise>k__BackingField;

		// Token: 0x0400047D RID: 1149
		[CompilerGenerated]
		private static Color <PaleVioletRed>k__BackingField;

		// Token: 0x0400047E RID: 1150
		[CompilerGenerated]
		private static Color <PapayaWhip>k__BackingField;

		// Token: 0x0400047F RID: 1151
		[CompilerGenerated]
		private static Color <PeachPuff>k__BackingField;

		// Token: 0x04000480 RID: 1152
		[CompilerGenerated]
		private static Color <Peru>k__BackingField;

		// Token: 0x04000481 RID: 1153
		[CompilerGenerated]
		private static Color <Pink>k__BackingField;

		// Token: 0x04000482 RID: 1154
		[CompilerGenerated]
		private static Color <Plum>k__BackingField;

		// Token: 0x04000483 RID: 1155
		[CompilerGenerated]
		private static Color <PowderBlue>k__BackingField;

		// Token: 0x04000484 RID: 1156
		[CompilerGenerated]
		private static Color <Purple>k__BackingField;

		// Token: 0x04000485 RID: 1157
		[CompilerGenerated]
		private static Color <Red>k__BackingField;

		// Token: 0x04000486 RID: 1158
		[CompilerGenerated]
		private static Color <RosyBrown>k__BackingField;

		// Token: 0x04000487 RID: 1159
		[CompilerGenerated]
		private static Color <RoyalBlue>k__BackingField;

		// Token: 0x04000488 RID: 1160
		[CompilerGenerated]
		private static Color <SaddleBrown>k__BackingField;

		// Token: 0x04000489 RID: 1161
		[CompilerGenerated]
		private static Color <Salmon>k__BackingField;

		// Token: 0x0400048A RID: 1162
		[CompilerGenerated]
		private static Color <SandyBrown>k__BackingField;

		// Token: 0x0400048B RID: 1163
		[CompilerGenerated]
		private static Color <SeaGreen>k__BackingField;

		// Token: 0x0400048C RID: 1164
		[CompilerGenerated]
		private static Color <SeaShell>k__BackingField;

		// Token: 0x0400048D RID: 1165
		[CompilerGenerated]
		private static Color <Sienna>k__BackingField;

		// Token: 0x0400048E RID: 1166
		[CompilerGenerated]
		private static Color <Silver>k__BackingField;

		// Token: 0x0400048F RID: 1167
		[CompilerGenerated]
		private static Color <SkyBlue>k__BackingField;

		// Token: 0x04000490 RID: 1168
		[CompilerGenerated]
		private static Color <SlateBlue>k__BackingField;

		// Token: 0x04000491 RID: 1169
		[CompilerGenerated]
		private static Color <SlateGray>k__BackingField;

		// Token: 0x04000492 RID: 1170
		[CompilerGenerated]
		private static Color <Snow>k__BackingField;

		// Token: 0x04000493 RID: 1171
		[CompilerGenerated]
		private static Color <SpringGreen>k__BackingField;

		// Token: 0x04000494 RID: 1172
		[CompilerGenerated]
		private static Color <SteelBlue>k__BackingField;

		// Token: 0x04000495 RID: 1173
		[CompilerGenerated]
		private static Color <Tan>k__BackingField;

		// Token: 0x04000496 RID: 1174
		[CompilerGenerated]
		private static Color <Teal>k__BackingField;

		// Token: 0x04000497 RID: 1175
		[CompilerGenerated]
		private static Color <Thistle>k__BackingField;

		// Token: 0x04000498 RID: 1176
		[CompilerGenerated]
		private static Color <Tomato>k__BackingField;

		// Token: 0x04000499 RID: 1177
		[CompilerGenerated]
		private static Color <Turquoise>k__BackingField;

		// Token: 0x0400049A RID: 1178
		[CompilerGenerated]
		private static Color <Violet>k__BackingField;

		// Token: 0x0400049B RID: 1179
		[CompilerGenerated]
		private static Color <Wheat>k__BackingField;

		// Token: 0x0400049C RID: 1180
		[CompilerGenerated]
		private static Color <White>k__BackingField;

		// Token: 0x0400049D RID: 1181
		[CompilerGenerated]
		private static Color <WhiteSmoke>k__BackingField;

		// Token: 0x0400049E RID: 1182
		[CompilerGenerated]
		private static Color <Yellow>k__BackingField;

		// Token: 0x0400049F RID: 1183
		[CompilerGenerated]
		private static Color <YellowGreen>k__BackingField;

		// Token: 0x040004A0 RID: 1184
		private uint packedValue;
	}
}

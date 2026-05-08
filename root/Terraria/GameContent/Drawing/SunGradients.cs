using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x02000436 RID: 1078
	public class SunGradients
	{
		// Token: 0x060030B2 RID: 12466 RVA: 0x005BB7AB File Offset: 0x005B99AB
		private static IEnumerable<int> Ocean()
		{
			yield return Main.oceanBG;
			yield break;
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x0000357B File Offset: 0x0000177B
		public SunGradients()
		{
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x005BB7B4 File Offset: 0x005B99B4
		// Note: this type is marked as 'beforefieldinit'.
		static SunGradients()
		{
		}

		// Token: 0x040056F2 RID: 22258
		private static Color[] _Sunrise_Aluminum = new Color[]
		{
			new Color(42, 85, 135),
			new Color(51, 86, 137),
			new Color(63, 86, 140),
			new Color(76, 86, 143),
			new Color(91, 86, 146),
			new Color(107, 87, 150),
			new Color(123, 90, 153),
			new Color(138, 95, 155),
			new Color(152, 102, 157),
			new Color(168, 114, 157),
			new Color(185, 131, 157),
			new Color(202, 150, 157),
			new Color(219, 170, 157),
			new Color(233, 188, 157),
			new Color(246, 204, 157)
		};

		// Token: 0x040056F3 RID: 22259
		private static Color[] _Sunrise_Blue = new Color[]
		{
			new Color(17, 35, 67),
			new Color(21, 43, 76),
			new Color(24, 55, 86),
			new Color(30, 69, 99),
			new Color(36, 87, 114),
			new Color(43, 107, 127),
			new Color(55, 126, 140),
			new Color(68, 144, 149),
			new Color(84, 157, 155),
			new Color(116, 175, 156),
			new Color(154, 190, 155),
			new Color(189, 204, 156),
			new Color(218, 215, 155),
			new Color(241, 225, 154),
			new Color(255, 230, 153)
		};

		// Token: 0x040056F4 RID: 22260
		private static Color[] _Sunrise_Violet = new Color[]
		{
			new Color(37, 42, 58),
			new Color(43, 46, 65),
			new Color(50, 51, 77),
			new Color(58, 56, 90),
			new Color(68, 64, 104),
			new Color(81, 73, 119),
			new Color(93, 82, 131),
			new Color(106, 92, 142),
			new Color(121, 104, 151),
			new Color(145, 124, 152),
			new Color(175, 149, 157),
			new Color(201, 170, 157),
			new Color(225, 191, 158),
			new Color(243, 207, 156),
			new Color(249, 212, 156)
		};

		// Token: 0x040056F5 RID: 22261
		private static Color[] _Sunrise_Yellow = new Color[]
		{
			new Color(15, 18, 28),
			new Color(16, 20, 32),
			new Color(20, 26, 43),
			new Color(25, 36, 58),
			new Color(33, 46, 76),
			new Color(42, 60, 91),
			new Color(53, 74, 97),
			new Color(69, 92, 102),
			new Color(90, 116, 104),
			new Color(118, 141, 106),
			new Color(148, 164, 110),
			new Color(172, 181, 115),
			new Color(195, 198, 128),
			new Color(218, 213, 142),
			new Color(233, 225, 158)
		};

		// Token: 0x040056F6 RID: 22262
		private static Color[] _Sunset_Blue = new Color[]
		{
			new Color(67, 80, 117),
			new Color(82, 84, 120),
			new Color(98, 89, 124),
			new Color(114, 92, 125),
			new Color(129, 95, 125),
			new Color(144, 98, 125),
			new Color(158, 100, 126),
			new Color(171, 103, 125),
			new Color(182, 104, 121),
			new Color(192, 106, 115),
			new Color(200, 109, 107),
			new Color(207, 111, 96),
			new Color(213, 112, 84),
			new Color(218, 112, 70),
			new Color(222, 111, 56)
		};

		// Token: 0x040056F7 RID: 22263
		private static Color[] _Sunset_Dark = new Color[]
		{
			new Color(16, 15, 33),
			new Color(17, 15, 33),
			new Color(20, 16, 34),
			new Color(24, 18, 35),
			new Color(27, 19, 36),
			new Color(34, 21, 38),
			new Color(39, 22, 41),
			new Color(47, 23, 45),
			new Color(51, 25, 47),
			new Color(56, 27, 49),
			new Color(60, 29, 50),
			new Color(65, 32, 53),
			new Color(70, 33, 56),
			new Color(76, 36, 58),
			new Color(80, 39, 60)
		};

		// Token: 0x040056F8 RID: 22264
		private static Color[] _Sunset_Pink = new Color[]
		{
			new Color(72, 48, 93),
			new Color(86, 54, 102),
			new Color(101, 61, 112),
			new Color(117, 68, 122),
			new Color(133, 74, 130),
			new Color(148, 81, 138),
			new Color(162, 87, 143),
			new Color(173, 93, 145),
			new Color(186, 99, 142),
			new Color(199, 105, 133),
			new Color(210, 111, 119),
			new Color(219, 115, 103),
			new Color(227, 119, 87),
			new Color(234, 123, 73),
			new Color(240, 125, 63)
		};

		// Token: 0x040056F9 RID: 22265
		private static Color[] _Sunset_Red = new Color[]
		{
			new Color(27, 24, 39),
			new Color(28, 24, 39),
			new Color(32, 25, 40),
			new Color(38, 27, 40),
			new Color(43, 28, 41),
			new Color(50, 29, 43),
			new Color(57, 30, 44),
			new Color(64, 32, 45),
			new Color(71, 34, 46),
			new Color(79, 36, 47),
			new Color(85, 37, 48),
			new Color(93, 39, 50),
			new Color(100, 41, 50),
			new Color(109, 43, 52),
			new Color(118, 45, 53)
		};

		// Token: 0x040056FA RID: 22266
		public static List<Color[]> Sunrises = new List<Color[]>
		{
			SunGradients._Sunrise_Blue,
			SunGradients._Sunrise_Violet,
			SunGradients._Sunrise_Yellow,
			SunGradients._Sunrise_Aluminum
		};

		// Token: 0x040056FB RID: 22267
		public static List<Color[]> Sunsets = new List<Color[]>
		{
			SunGradients._Sunset_Blue,
			SunGradients._Sunset_Dark,
			SunGradients._Sunset_Pink,
			SunGradients._Sunset_Red
		};

		// Token: 0x040056FC RID: 22268
		public static Dictionary<int, Color> BackgroundGradientColors = new Dictionary<int, Color>
		{
			{
				58,
				new Color(220, 255, 109)
			},
			{
				175,
				new Color(116, 191, 255)
			},
			{
				178,
				new Color(157, 192, 255)
			},
			{
				247,
				new Color(184, 211, 245)
			},
			{
				262,
				new Color(169, 241, 255)
			},
			{
				267,
				new Color(169, 241, 255)
			},
			{
				268,
				new Color(169, 241, 255)
			},
			{
				282,
				new Color(157, 192, 255)
			},
			{
				283,
				new Color(141, 232, 131)
			}
		};

		// Token: 0x040056FD RID: 22269
		public static List<BackgroundGradientDrawer> BackgroundDrawers = new List<BackgroundGradientDrawer>
		{
			new BackgroundGradientDrawer(new Color(116, 191, 255), () => Main.bgAlphaFrontLayer[0], () => Main.treeBGSet1, new int[] { 176 }),
			new BackgroundGradientDrawer(new Color(157, 192, 255), () => Main.bgAlphaFrontLayer[0], () => Main.treeBGSet1, new int[] { 179 }),
			new BackgroundGradientDrawer(new Color(116, 191, 255), () => Main.bgAlphaFrontLayer[10], () => Main.treeBGSet2, new int[] { 176 }),
			new BackgroundGradientDrawer(new Color(157, 192, 255), () => Main.bgAlphaFrontLayer[10], () => Main.treeBGSet2, new int[] { 179 }),
			new BackgroundGradientDrawer(new Color(116, 191, 255), () => Main.bgAlphaFrontLayer[11], () => Main.treeBGSet3, new int[] { 176 }),
			new BackgroundGradientDrawer(new Color(157, 192, 255), () => Main.bgAlphaFrontLayer[11], () => Main.treeBGSet3, new int[] { 179 }),
			new BackgroundGradientDrawer(new Color(116, 191, 255), () => Main.bgAlphaFrontLayer[12], () => Main.treeBGSet4, new int[] { 176 }),
			new BackgroundGradientDrawer(new Color(157, 192, 255), () => Main.bgAlphaFrontLayer[12], () => Main.treeBGSet4, new int[] { 179 }),
			new BackgroundGradientDrawer(new Color(184, 211, 245), () => Main.bgAlphaFrontLayer[2], () => Main.desertBackgroundSet.Pure.Backgrounds, new int[] { 248 }),
			new BackgroundGradientDrawer(new Color(169, 241, 255), () => Main.bgAlphaFrontLayer[7], () => Main.snowBG, new int[] { 263, 268, 269 }),
			new BackgroundGradientDrawer(new Color(220, 255, 109), () => Main.bgAlphaFrontLayer[3], () => Main.jungleBG, new int[] { 59 }),
			new BackgroundGradientDrawer(new Color(141, 232, 131), () => Main.bgAlphaFrontLayer[3], () => Main.jungleBG, new int[] { 284 }),
			new BackgroundGradientDrawer(new Color(157, 192, 255), () => Main.bgAlphaFrontLayer[4], new BackgroundArrayGetterMethod(SunGradients.Ocean), new int[] { 283 })
		};

		// Token: 0x0200093E RID: 2366
		[CompilerGenerated]
		private sealed class <Ocean>d__12 : IEnumerable<int>, IEnumerable, IEnumerator<int>, IDisposable, IEnumerator
		{
			// Token: 0x0600482C RID: 18476 RVA: 0x006CCC2C File Offset: 0x006CAE2C
			[DebuggerHidden]
			public <Ocean>d__12(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x0600482D RID: 18477 RVA: 0x00009E46 File Offset: 0x00008046
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600482E RID: 18478 RVA: 0x006CCC4C File Offset: 0x006CAE4C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = Main.oceanBG;
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				return false;
			}

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x0600482F RID: 18479 RVA: 0x006CCC8C File Offset: 0x006CAE8C
			int IEnumerator<int>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004830 RID: 18480 RVA: 0x0066E2F4 File Offset: 0x0066C4F4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x06004831 RID: 18481 RVA: 0x006CCC94 File Offset: 0x006CAE94
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004832 RID: 18482 RVA: 0x006CCCA4 File Offset: 0x006CAEA4
			[DebuggerHidden]
			IEnumerator<int> IEnumerable<int>.GetEnumerator()
			{
				SunGradients.<Ocean>d__12 <Ocean>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<Ocean>d__ = this;
				}
				else
				{
					<Ocean>d__ = new SunGradients.<Ocean>d__12(0);
				}
				return <Ocean>d__;
			}

			// Token: 0x06004833 RID: 18483 RVA: 0x006CCCE0 File Offset: 0x006CAEE0
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator();
			}

			// Token: 0x04007544 RID: 30020
			private int <>1__state;

			// Token: 0x04007545 RID: 30021
			private int <>2__current;

			// Token: 0x04007546 RID: 30022
			private int <>l__initialThreadId;
		}

		// Token: 0x0200093F RID: 2367
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004834 RID: 18484 RVA: 0x006CCCE8 File Offset: 0x006CAEE8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004835 RID: 18485 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004836 RID: 18486 RVA: 0x006CCCF4 File Offset: 0x006CAEF4
			internal float <.cctor>b__14_0()
			{
				return Main.bgAlphaFrontLayer[0];
			}

			// Token: 0x06004837 RID: 18487 RVA: 0x006CCCFD File Offset: 0x006CAEFD
			internal IEnumerable<int> <.cctor>b__14_1()
			{
				return Main.treeBGSet1;
			}

			// Token: 0x06004838 RID: 18488 RVA: 0x006CCCF4 File Offset: 0x006CAEF4
			internal float <.cctor>b__14_2()
			{
				return Main.bgAlphaFrontLayer[0];
			}

			// Token: 0x06004839 RID: 18489 RVA: 0x006CCCFD File Offset: 0x006CAEFD
			internal IEnumerable<int> <.cctor>b__14_3()
			{
				return Main.treeBGSet1;
			}

			// Token: 0x0600483A RID: 18490 RVA: 0x006CCD04 File Offset: 0x006CAF04
			internal float <.cctor>b__14_4()
			{
				return Main.bgAlphaFrontLayer[10];
			}

			// Token: 0x0600483B RID: 18491 RVA: 0x006CCD0E File Offset: 0x006CAF0E
			internal IEnumerable<int> <.cctor>b__14_5()
			{
				return Main.treeBGSet2;
			}

			// Token: 0x0600483C RID: 18492 RVA: 0x006CCD04 File Offset: 0x006CAF04
			internal float <.cctor>b__14_6()
			{
				return Main.bgAlphaFrontLayer[10];
			}

			// Token: 0x0600483D RID: 18493 RVA: 0x006CCD0E File Offset: 0x006CAF0E
			internal IEnumerable<int> <.cctor>b__14_7()
			{
				return Main.treeBGSet2;
			}

			// Token: 0x0600483E RID: 18494 RVA: 0x006CCD15 File Offset: 0x006CAF15
			internal float <.cctor>b__14_8()
			{
				return Main.bgAlphaFrontLayer[11];
			}

			// Token: 0x0600483F RID: 18495 RVA: 0x006CCD1F File Offset: 0x006CAF1F
			internal IEnumerable<int> <.cctor>b__14_9()
			{
				return Main.treeBGSet3;
			}

			// Token: 0x06004840 RID: 18496 RVA: 0x006CCD15 File Offset: 0x006CAF15
			internal float <.cctor>b__14_10()
			{
				return Main.bgAlphaFrontLayer[11];
			}

			// Token: 0x06004841 RID: 18497 RVA: 0x006CCD1F File Offset: 0x006CAF1F
			internal IEnumerable<int> <.cctor>b__14_11()
			{
				return Main.treeBGSet3;
			}

			// Token: 0x06004842 RID: 18498 RVA: 0x006CCD26 File Offset: 0x006CAF26
			internal float <.cctor>b__14_12()
			{
				return Main.bgAlphaFrontLayer[12];
			}

			// Token: 0x06004843 RID: 18499 RVA: 0x006CCD30 File Offset: 0x006CAF30
			internal IEnumerable<int> <.cctor>b__14_13()
			{
				return Main.treeBGSet4;
			}

			// Token: 0x06004844 RID: 18500 RVA: 0x006CCD26 File Offset: 0x006CAF26
			internal float <.cctor>b__14_14()
			{
				return Main.bgAlphaFrontLayer[12];
			}

			// Token: 0x06004845 RID: 18501 RVA: 0x006CCD30 File Offset: 0x006CAF30
			internal IEnumerable<int> <.cctor>b__14_15()
			{
				return Main.treeBGSet4;
			}

			// Token: 0x06004846 RID: 18502 RVA: 0x006CCD37 File Offset: 0x006CAF37
			internal float <.cctor>b__14_16()
			{
				return Main.bgAlphaFrontLayer[2];
			}

			// Token: 0x06004847 RID: 18503 RVA: 0x006CCD40 File Offset: 0x006CAF40
			internal IEnumerable<int> <.cctor>b__14_17()
			{
				return Main.desertBackgroundSet.Pure.Backgrounds;
			}

			// Token: 0x06004848 RID: 18504 RVA: 0x006CCD51 File Offset: 0x006CAF51
			internal float <.cctor>b__14_18()
			{
				return Main.bgAlphaFrontLayer[7];
			}

			// Token: 0x06004849 RID: 18505 RVA: 0x006CCD5A File Offset: 0x006CAF5A
			internal IEnumerable<int> <.cctor>b__14_19()
			{
				return Main.snowBG;
			}

			// Token: 0x0600484A RID: 18506 RVA: 0x006CCD61 File Offset: 0x006CAF61
			internal float <.cctor>b__14_20()
			{
				return Main.bgAlphaFrontLayer[3];
			}

			// Token: 0x0600484B RID: 18507 RVA: 0x006CCD6A File Offset: 0x006CAF6A
			internal IEnumerable<int> <.cctor>b__14_21()
			{
				return Main.jungleBG;
			}

			// Token: 0x0600484C RID: 18508 RVA: 0x006CCD61 File Offset: 0x006CAF61
			internal float <.cctor>b__14_22()
			{
				return Main.bgAlphaFrontLayer[3];
			}

			// Token: 0x0600484D RID: 18509 RVA: 0x006CCD6A File Offset: 0x006CAF6A
			internal IEnumerable<int> <.cctor>b__14_23()
			{
				return Main.jungleBG;
			}

			// Token: 0x0600484E RID: 18510 RVA: 0x006CCD71 File Offset: 0x006CAF71
			internal float <.cctor>b__14_24()
			{
				return Main.bgAlphaFrontLayer[4];
			}

			// Token: 0x04007547 RID: 30023
			public static readonly SunGradients.<>c <>9 = new SunGradients.<>c();
		}
	}
}

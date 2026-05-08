using System;
using Microsoft.Xna.Framework;
using Terraria.Enums;

namespace Terraria.GameContent
{
	// Token: 0x0200024D RID: 589
	public class DontStarveSeed
	{
		// Token: 0x06002316 RID: 8982 RVA: 0x0053C740 File Offset: 0x0053A940
		public static void ModifyNightColor(ref Color bgColorToSet, ref Color moonColor)
		{
			if (Main.GetMoonPhase() == MoonPhase.Full)
			{
				return;
			}
			float num = (float)(Main.time / 32400.0);
			Color color = bgColorToSet;
			Color black = Color.Black;
			Color color2 = bgColorToSet;
			float num2 = Utils.Remap(num, 0f, 0.5f, 0f, 1f, true);
			float num3 = Utils.Remap(num, 0.5f, 1f, 0f, 1f, true);
			Color color3 = Color.Lerp(Color.Lerp(color, black, num2), color2, num3);
			bgColorToSet = color3;
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x0053C7CC File Offset: 0x0053A9CC
		public static void ModifyMinimumLightColorAtNight(ref byte minimalLight)
		{
			switch (Main.GetMoonPhase())
			{
			case MoonPhase.Full:
				minimalLight = 45;
				break;
			case MoonPhase.ThreeQuartersAtLeft:
			case MoonPhase.ThreeQuartersAtRight:
				minimalLight = 1;
				break;
			case MoonPhase.HalfAtLeft:
			case MoonPhase.HalfAtRight:
				minimalLight = 1;
				break;
			case MoonPhase.QuarterAtLeft:
			case MoonPhase.QuarterAtRight:
				minimalLight = 1;
				break;
			case MoonPhase.Empty:
				minimalLight = 1;
				break;
			}
			if (Main.bloodMoon)
			{
				minimalLight = Utils.Max<byte>(new byte[] { minimalLight, 35 });
			}
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x0053C83D File Offset: 0x0053AA3D
		public static void FixBiomeDarkness(ref Color bgColor, ref int R, ref int G, ref int B)
		{
			if (!Main.dontStarveWorld)
			{
				return;
			}
			R = (int)((byte)Math.Min((int)bgColor.R, R));
			G = (int)((byte)Math.Min((int)bgColor.G, G));
			B = (int)((byte)Math.Min((int)bgColor.B, B));
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x0053C877 File Offset: 0x0053AA77
		public static void Initialize()
		{
			Player.Hooks.OnEnterWorld += DontStarveSeed.Hook_OnEnterWorld;
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x0053C88A File Offset: 0x0053AA8A
		private static void Hook_OnEnterWorld(Player player)
		{
			player.UpdateStarvingState(false);
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x0000357B File Offset: 0x0000177B
		public DontStarveSeed()
		{
		}
	}
}

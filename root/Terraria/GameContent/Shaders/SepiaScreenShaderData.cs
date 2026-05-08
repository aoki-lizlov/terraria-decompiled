using System;
using Microsoft.Xna.Framework;
using Terraria.Enums;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000294 RID: 660
	public class SepiaScreenShaderData : ScreenShaderData
	{
		// Token: 0x0600253A RID: 9530 RVA: 0x00553E64 File Offset: 0x00552064
		public SepiaScreenShaderData(string passName)
			: base(passName)
		{
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x00553E70 File Offset: 0x00552070
		public override void Update(GameTime gameTime)
		{
			float num = (Main.screenPosition.Y + (float)(Main.screenHeight / 2)) / 16f;
			float num2 = 1f - Utils.SmoothStep((float)Main.worldSurface, (float)Main.worldSurface + 30f, num);
			Vector3 vector2;
			Vector3 vector = (vector2 = new Vector3(0.191f, -0.054f, -0.221f));
			Vector3 vector3 = vector * 0.5f;
			Vector3 vector4 = new Vector3(0f, -0.03f, 0.15f);
			Vector3 vector5 = new Vector3(-0.11f, 0.01f, 0.16f);
			float cloudAlpha = Main.cloudAlpha;
			float num3;
			float num4;
			float num5;
			float num6;
			SepiaScreenShaderData.GetDaylightPowers(out num3, out num4, out num5, out num6);
			float num7 = num3 * 0.13f;
			if (Main.starGame)
			{
				float num8 = (float)Main.starGameMath(1.0) - 1f;
				num3 = num8;
				num4 = 1f - num8;
				num5 = num8;
				num6 = 1f - num8;
				num7 = num3 * 0.13f;
			}
			else if (!Main.dayTime)
			{
				if (Main.GetMoonPhase() == MoonPhase.Full)
				{
					vector2 = new Vector3(-0.19f, 0.01f, 0.22f);
					num7 += 0.07f * num5;
				}
				if (Main.bloodMoon)
				{
					vector2 = new Vector3(0.2f, -0.1f, -0.221f);
					num7 = 0.2f;
				}
			}
			num3 *= num2;
			num4 *= num2;
			num5 *= num2;
			num6 *= num2;
			base.UseOpacity(1f);
			base.UseIntensity(1.4f - num4 * 0.2f);
			float num9 = 0.3f - num7 * num3;
			num9 = MathHelper.Lerp(num9, 0.1f, cloudAlpha);
			float num10 = 0.2f;
			num9 = MathHelper.Lerp(num9, num10, 1f - num2);
			base.UseProgress(num9);
			Vector3 vector6 = Vector3.Lerp(vector, vector2, num5);
			vector6 = Vector3.Lerp(vector6, vector4, num6);
			vector6 = Vector3.Lerp(vector6, vector5, cloudAlpha);
			vector6 = Vector3.Lerp(vector6, vector3, 1f - num2);
			base.UseColor(vector6);
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x00554074 File Offset: 0x00552274
		private static void GetDaylightPowers(out float nightlightPower, out float daylightPower, out float moonPower, out float dawnPower)
		{
			nightlightPower = 0f;
			daylightPower = 0f;
			moonPower = 0f;
			Vector2 dayTimeAsDirectionIn24HClock = Utils.GetDayTimeAsDirectionIn24HClock();
			Vector2 dayTimeAsDirectionIn24HClock2 = Utils.GetDayTimeAsDirectionIn24HClock(4.5f);
			Vector2 dayTimeAsDirectionIn24HClock3 = Utils.GetDayTimeAsDirectionIn24HClock(0f);
			float num = Vector2.Dot(dayTimeAsDirectionIn24HClock, dayTimeAsDirectionIn24HClock3);
			float num2 = Vector2.Dot(dayTimeAsDirectionIn24HClock, dayTimeAsDirectionIn24HClock2);
			nightlightPower = Utils.Remap(num, -0.2f, 0.1f, 0f, 1f, true);
			daylightPower = Utils.Remap(num, 0.1f, -1f, 0f, 1f, true);
			dawnPower = Utils.Remap(num2, 0.66f, 1f, 0f, 1f, true);
			if (!Main.dayTime)
			{
				float num3 = (float)(Main.time / 32400.0) * 2f;
				if (num3 > 1f)
				{
					num3 = 2f - num3;
				}
				moonPower = Utils.Remap(num3, 0f, 0.25f, 0f, 1f, true);
			}
		}
	}
}

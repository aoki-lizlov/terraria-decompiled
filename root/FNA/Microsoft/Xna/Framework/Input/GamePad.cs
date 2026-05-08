using System;

namespace Microsoft.Xna.Framework.Input
{
	// Token: 0x0200005D RID: 93
	public static class GamePad
	{
		// Token: 0x06000FCC RID: 4044 RVA: 0x00021B88 File Offset: 0x0001FD88
		private static int DetermineNumGamepads()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("FNA_GAMEPAD_NUM_GAMEPADS");
			int num;
			if (!string.IsNullOrEmpty(environmentVariable) && int.TryParse(environmentVariable, out num) && num >= 0)
			{
				return num;
			}
			return Enum.GetNames(typeof(PlayerIndex)).Length;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00021BC9 File Offset: 0x0001FDC9
		public static GamePadCapabilities GetCapabilities(PlayerIndex playerIndex)
		{
			return FNAPlatform.GetGamePadCapabilities((int)playerIndex);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00021BD6 File Offset: 0x0001FDD6
		public static GamePadState GetState(PlayerIndex playerIndex)
		{
			return FNAPlatform.GetGamePadState((int)playerIndex, GamePadDeadZone.IndependentAxes);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00021BE4 File Offset: 0x0001FDE4
		public static GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode)
		{
			return FNAPlatform.GetGamePadState((int)playerIndex, deadZoneMode);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00021BF2 File Offset: 0x0001FDF2
		public static bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
		{
			return FNAPlatform.SetGamePadVibration((int)playerIndex, leftMotor, rightMotor);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00021C01 File Offset: 0x0001FE01
		public static string GetGUIDEXT(PlayerIndex playerIndex)
		{
			return FNAPlatform.GetGamePadGUID((int)playerIndex);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00021C0E File Offset: 0x0001FE0E
		public static void SetLightBarEXT(PlayerIndex playerIndex, Color color)
		{
			FNAPlatform.SetGamePadLightBar((int)playerIndex, color);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00021C1C File Offset: 0x0001FE1C
		public static bool SetTriggerVibrationEXT(PlayerIndex playerIndex, float leftTrigger, float rightTrigger)
		{
			return FNAPlatform.SetGamePadTriggerVibration((int)playerIndex, leftTrigger, rightTrigger);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00021C2B File Offset: 0x0001FE2B
		public static bool GetGyroEXT(PlayerIndex playerIndex, out Vector3 gyro)
		{
			return FNAPlatform.GetGamePadGyro((int)playerIndex, out gyro);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00021C39 File Offset: 0x0001FE39
		public static bool GetAccelerometerEXT(PlayerIndex playerIndex, out Vector3 accel)
		{
			return FNAPlatform.GetGamePadAccelerometer((int)playerIndex, out accel);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00021C47 File Offset: 0x0001FE47
		internal static float ExcludeAxisDeadZone(float value, float deadZone)
		{
			if (value < -deadZone)
			{
				value += deadZone;
			}
			else
			{
				if (value <= deadZone)
				{
					return 0f;
				}
				value -= deadZone;
			}
			return value / (1f - deadZone);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00021C6F File Offset: 0x0001FE6F
		// Note: this type is marked as 'beforefieldinit'.
		static GamePad()
		{
		}

		// Token: 0x04000669 RID: 1641
		internal const float LeftDeadZone = 0.23953247f;

		// Token: 0x0400066A RID: 1642
		internal const float RightDeadZone = 0.26516724f;

		// Token: 0x0400066B RID: 1643
		internal const float TriggerThreshold = 0.11764706f;

		// Token: 0x0400066C RID: 1644
		internal static readonly int GAMEPAD_COUNT = GamePad.DetermineNumGamepads();
	}
}

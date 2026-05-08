using System;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x0200029A RID: 666
	public class MoonLordScreenShaderData : ScreenShaderData
	{
		// Token: 0x06002553 RID: 9555 RVA: 0x005557D4 File Offset: 0x005539D4
		public MoonLordScreenShaderData(string passName, bool aimAtPlayer)
			: base(passName)
		{
			this._aimAtPlayer = aimAtPlayer;
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x005557EC File Offset: 0x005539EC
		private void UpdateMoonLordIndex()
		{
			if (this._aimAtPlayer)
			{
				return;
			}
			if (this._moonLordIndex >= 0 && Main.npc[this._moonLordIndex].active && Main.npc[this._moonLordIndex].type == 398)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == 398)
				{
					num = i;
					break;
				}
			}
			this._moonLordIndex = num;
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x00555878 File Offset: 0x00553A78
		public override void Apply()
		{
			this.UpdateMoonLordIndex();
			if (this._aimAtPlayer)
			{
				base.UseTargetPosition(Main.SceneMetrics.Center);
			}
			else if (this._moonLordIndex != -1)
			{
				base.UseTargetPosition(Main.npc[this._moonLordIndex].Center);
			}
			base.Apply();
		}

		// Token: 0x04004FB7 RID: 20407
		private int _moonLordIndex = -1;

		// Token: 0x04004FB8 RID: 20408
		private bool _aimAtPlayer;
	}
}

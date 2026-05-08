using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.Cinematics
{
	// Token: 0x020005AF RID: 1455
	public class DSTFilm : Film
	{
		// Token: 0x06003996 RID: 14742 RVA: 0x0065345C File Offset: 0x0065165C
		public DSTFilm()
		{
			this.BuildSequence();
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x0065346A File Offset: 0x0065166A
		public override void OnBegin()
		{
			this.PrepareScene();
			Main.hideUI = true;
			base.OnBegin();
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x0065347E File Offset: 0x0065167E
		public override void OnEnd()
		{
			this.ClearScene();
			Main.hideUI = false;
			base.OnEnd();
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x00653494 File Offset: 0x00651694
		private void BuildSequence()
		{
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.EquipDSTShaderItem)
			});
			base.AppendEmptySequence(60);
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.CreateDeerclops),
				new FrameEvent(this.CreateChester),
				new FrameEvent(this.ControlPlayer)
			});
			base.AppendEmptySequence(60);
			base.AppendEmptySequence(187);
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.StopBeforeCliff)
			});
			base.AppendEmptySequence(20);
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.TurnPlayerToTheLeft)
			});
			base.AppendEmptySequence(20);
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.DeerclopsAttack)
			});
			base.AppendEmptySequence(60);
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.RemoveDSTShaderItem)
			});
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x00653594 File Offset: 0x00651794
		private void PrepareScene()
		{
			Main.dayTime = true;
			Main.time = 13500.0;
			Main.time = 43638.0;
			Main.windSpeedCurrent = (Main.windSpeedTarget = 0.36799997f);
			Main.windCounter = 2011;
			Main.cloudAlpha = 0f;
			Main.raining = true;
			Main.rainTime = 3600;
			Main.maxRaining = (Main.oldMaxRaining = (Main.cloudAlpha = 0.9f));
			Main.raining = true;
			Main.maxRaining = (Main.oldMaxRaining = (Main.cloudAlpha = 0.6f));
			Main.raining = true;
			Main.maxRaining = (Main.oldMaxRaining = (Main.cloudAlpha = 0.6f));
			this._startPoint = new Point(4050, 488).ToWorldCoordinates(8f, 8f);
			this._startPoint -= new Vector2(1280f, 0f);
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x00653689 File Offset: 0x00651889
		private void ClearScene()
		{
			if (this._deerclops != null)
			{
				this._deerclops.active = false;
			}
			if (this._chester != null)
			{
				this._chester.active = false;
			}
			Main.LocalPlayer.isControlledByFilm = false;
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x006536C0 File Offset: 0x006518C0
		private void EquipDSTShaderItem(FrameEventData evt)
		{
			this._oldItem = Main.LocalPlayer.armor[3];
			Item item = new Item();
			item.SetDefaults(5113, null);
			Main.LocalPlayer.armor[3] = item;
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x006536FE File Offset: 0x006518FE
		private void RemoveDSTShaderItem(FrameEventData evt)
		{
			Main.LocalPlayer.armor[3] = this._oldItem;
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x00653714 File Offset: 0x00651914
		private void CreateDeerclops(FrameEventData evt)
		{
			this._deerclops = this.PlaceNPCOnGround(668, this._startPoint);
			this._deerclops.immortal = true;
			this._deerclops.dontTakeDamage = true;
			this._deerclops.takenDamageMultiplier = 0f;
			this._deerclops.immune[255] = 100000;
			this._deerclops.immune[Main.myPlayer] = 100000;
			this._deerclops.ai[0] = -1f;
			this._deerclops.velocity.Y = 4f;
			this._deerclops.velocity.X = 6f;
			NPC deerclops = this._deerclops;
			deerclops.position.X = deerclops.position.X - 24f;
			this._deerclops.direction = (this._deerclops.spriteDirection = 1);
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x006537FC File Offset: 0x006519FC
		private NPC PlaceNPCOnGround(int type, Vector2 position)
		{
			int num;
			int num2;
			DSTFilm.FindFloorAt(position, out num, out num2);
			if (type == 668)
			{
				num2 -= 240;
			}
			int num3 = 100;
			int num4 = NPC.NewNPC(new EntitySource_Film(), num, num2, type, num3, 0f, 0f, 0f, 0f, 255);
			return Main.npc[num4];
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x00653858 File Offset: 0x00651A58
		private void CreateChester(FrameEventData evt)
		{
			int num;
			int num2;
			DSTFilm.FindFloorAt(this._startPoint + new Vector2(110f, 0f), out num, out num2);
			num2 -= 240;
			int num3 = Projectile.NewProjectile(null, (float)num, (float)num2, 0f, 0f, 960, 0, 0f, Main.myPlayer, -1f, 0f, 0f, null);
			this._chester = Main.projectile[num3];
			this._chester.velocity.Y = 4f;
			this._chester.velocity.X = 6f;
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x006538FC File Offset: 0x00651AFC
		private void ControlPlayer(FrameEventData evt)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.isControlledByFilm = true;
			localPlayer.controlRight = true;
			int num;
			int num2;
			DSTFilm.FindFloorAt(this._startPoint + new Vector2(150f, 0f), out num, out num2);
			localPlayer.BottomLeft = new Vector2((float)num, (float)num2);
			localPlayer.velocity.X = 6f;
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x0065395D File Offset: 0x00651B5D
		private void StopBeforeCliff(FrameEventData evt)
		{
			Main.LocalPlayer.controlRight = false;
			this._chester.ai[0] = -2f;
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x0065397C File Offset: 0x00651B7C
		private void TurnPlayerToTheLeft(FrameEventData evt)
		{
			Main.LocalPlayer.ChangeDir(-1);
			this._chester.velocity = new Vector2(-0.1f, 0f);
			this._chester.spriteDirection = (this._chester.direction = -1);
			this._deerclops.ai[0] = 1f;
			this._deerclops.ai[1] = 0f;
			this._deerclops.TargetClosest(true);
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x006539F8 File Offset: 0x00651BF8
		private void DeerclopsAttack(FrameEventData evt)
		{
			Main.LocalPlayer.controlJump = true;
			this._chester.velocity.Y = -11.4f;
			this._deerclops.ai[0] = 1f;
			this._deerclops.ai[1] = 0f;
			this._deerclops.TargetClosest(true);
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x00653A58 File Offset: 0x00651C58
		private static void FindFloorAt(Vector2 position, out int x, out int y)
		{
			x = (int)position.X;
			y = (int)position.Y;
			int num = x / 16;
			int num2 = y / 16;
			while (!WorldGen.SolidTile(num, num2, false))
			{
				num2++;
			}
			y = num2 * 16;
		}

		// Token: 0x04005DA8 RID: 23976
		private NPC _deerclops;

		// Token: 0x04005DA9 RID: 23977
		private Projectile _chester;

		// Token: 0x04005DAA RID: 23978
		private Vector2 _startPoint;

		// Token: 0x04005DAB RID: 23979
		private Item _oldItem;
	}
}

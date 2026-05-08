using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;

namespace Terraria.Cinematics
{
	// Token: 0x020005AE RID: 1454
	public class DD2Film : Film
	{
		// Token: 0x0600397A RID: 14714 RVA: 0x00652350 File Offset: 0x00650550
		public DD2Film()
		{
			base.AppendKeyFrames(new FrameEvent[]
			{
				new FrameEvent(this.CreateDryad),
				new FrameEvent(this.CreateCritters)
			});
			base.AppendSequences(120, new FrameEvent[]
			{
				new FrameEvent(this.DryadStand),
				new FrameEvent(this.DryadLookRight)
			});
			base.AppendSequences(100, new FrameEvent[]
			{
				new FrameEvent(this.DryadLookRight),
				new FrameEvent(this.DryadInteract)
			});
			base.AddKeyFrame(base.AppendPoint - 20, new FrameEvent(this.CreatePortal));
			base.AppendSequences(30, new FrameEvent[]
			{
				new FrameEvent(this.DryadLookLeft),
				new FrameEvent(this.DryadStand)
			});
			base.AppendSequences(40, new FrameEvent[]
			{
				new FrameEvent(this.DryadConfusedEmote),
				new FrameEvent(this.DryadStand),
				new FrameEvent(this.DryadLookLeft)
			});
			base.AppendKeyFrame(new FrameEvent(this.CreateOgre));
			base.AddKeyFrame(base.AppendPoint + 60, new FrameEvent(this.SpawnJavalinThrower));
			base.AddKeyFrame(base.AppendPoint + 120, new FrameEvent(this.SpawnGoblin));
			base.AddKeyFrame(base.AppendPoint + 180, new FrameEvent(this.SpawnGoblin));
			base.AddKeyFrame(base.AppendPoint + 240, new FrameEvent(this.SpawnWitherBeast));
			base.AppendSequences(30, new FrameEvent[]
			{
				new FrameEvent(this.DryadStand),
				new FrameEvent(this.DryadLookLeft)
			});
			base.AppendSequences(30, new FrameEvent[]
			{
				new FrameEvent(this.DryadLookRight),
				new FrameEvent(this.DryadWalk)
			});
			base.AppendSequences(300, new FrameEvent[]
			{
				new FrameEvent(this.DryadAttack),
				new FrameEvent(this.DryadLookLeft)
			});
			base.AppendKeyFrame(new FrameEvent(this.RemoveEnemyDamage));
			base.AppendSequences(60, new FrameEvent[]
			{
				new FrameEvent(this.DryadLookRight),
				new FrameEvent(this.DryadStand),
				new FrameEvent(this.DryadAlertEmote)
			});
			base.AddSequences(base.AppendPoint - 90, 60, new FrameEvent[]
			{
				new FrameEvent(this.OgreLookLeft),
				new FrameEvent(this.OgreStand)
			});
			base.AddKeyFrame(base.AppendPoint - 12, new FrameEvent(this.OgreSwingSound));
			base.AddSequences(base.AppendPoint - 30, 50, new FrameEvent[]
			{
				new FrameEvent(this.DryadPortalKnock),
				new FrameEvent(this.DryadStand)
			});
			base.AppendKeyFrame(new FrameEvent(this.RestoreEnemyDamage));
			base.AppendSequences(40, new FrameEvent[]
			{
				new FrameEvent(this.DryadPortalFade),
				new FrameEvent(this.DryadStand)
			});
			base.AppendSequence(180, new FrameEvent(this.DryadStand));
			base.AddSequence(0, base.AppendPoint, new FrameEvent(this.PerFrameSettings));
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x006526C8 File Offset: 0x006508C8
		private void PerFrameSettings(FrameEventData evt)
		{
			CombatText.clearAll();
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x006526D0 File Offset: 0x006508D0
		private void CreateDryad(FrameEventData evt)
		{
			this._dryad = this.PlaceNPCOnGround(20, this._startPoint);
			this._dryad.knockBackResist = 0f;
			this._dryad.immortal = true;
			this._dryad.dontTakeDamage = true;
			this._dryad.takenDamageMultiplier = 0f;
			this._dryad.immune[255] = 100000;
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x00652740 File Offset: 0x00650940
		private void DryadInteract(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.ai[0] = 9f;
				if (evt.IsFirstFrame)
				{
					this._dryad.ai[1] = (float)evt.Duration;
				}
				this._dryad.localAI[0] = 0f;
			}
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x00652798 File Offset: 0x00650998
		private void SpawnWitherBeast(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 568, 0, 0f, 0f, 0f, 0f, 255);
			NPC npc = Main.npc[num];
			npc.knockBackResist = 0f;
			npc.immortal = true;
			npc.dontTakeDamage = true;
			npc.takenDamageMultiplier = 0f;
			npc.immune[255] = 100000;
			npc.friendly = this._ogre.friendly;
			this._army.Add(npc);
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x0065284C File Offset: 0x00650A4C
		private void SpawnJavalinThrower(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 561, 0, 0f, 0f, 0f, 0f, 255);
			NPC npc = Main.npc[num];
			npc.knockBackResist = 0f;
			npc.immortal = true;
			npc.dontTakeDamage = true;
			npc.takenDamageMultiplier = 0f;
			npc.immune[255] = 100000;
			npc.friendly = this._ogre.friendly;
			this._army.Add(npc);
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x00652900 File Offset: 0x00650B00
		private void SpawnGoblin(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 552, 0, 0f, 0f, 0f, 0f, 255);
			NPC npc = Main.npc[num];
			npc.knockBackResist = 0f;
			npc.immortal = true;
			npc.dontTakeDamage = true;
			npc.takenDamageMultiplier = 0f;
			npc.immune[255] = 100000;
			npc.friendly = this._ogre.friendly;
			this._army.Add(npc);
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x006529B4 File Offset: 0x00650BB4
		private void CreateCritters(FrameEventData evt)
		{
			for (int i = 0; i < 5; i++)
			{
				float num = (float)i / 5f;
				NPC npc = this.PlaceNPCOnGround((int)Utils.SelectRandom<short>(Main.rand, new short[] { 46, 46, 299, 538 }), this._startPoint + new Vector2((num - 0.25f) * 400f + Main.rand.NextFloat() * 50f - 25f, 0f));
				npc.ai[0] = 0f;
				npc.ai[1] = 600f;
				this._critters.Add(npc);
			}
			if (this._dryad == null)
			{
				return;
			}
			for (int j = 0; j < 10; j++)
			{
				float num2 = (float)j / 10f;
				int num3 = NPC.NewNPC(new EntitySource_Film(), (int)this._dryad.position.X + Main.rand.Next(-1000, 800), (int)this._dryad.position.Y - Main.rand.Next(-50, 300), 356, 0, 0f, 0f, 0f, 0f, 255);
				NPC npc2 = Main.npc[num3];
				npc2.ai[0] = Main.rand.NextFloat() * 4f - 2f;
				npc2.ai[1] = Main.rand.NextFloat() * 4f - 2f;
				npc2.velocity.X = Main.rand.NextFloat() * 4f - 2f;
				this._critters.Add(npc2);
			}
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x00652B6D File Offset: 0x00650D6D
		private void OgreSwingSound(FrameEventData evt)
		{
			SoundEngine.PlaySound(SoundID.DD2_OgreAttack, this._ogre.Center, 0f, 1f);
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x00652B90 File Offset: 0x00650D90
		private void DryadPortalKnock(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				if (evt.Frame == 20)
				{
					NPC dryad = this._dryad;
					dryad.velocity.Y = dryad.velocity.Y - 7f;
					NPC dryad2 = this._dryad;
					dryad2.velocity.X = dryad2.velocity.X - 8f;
					SoundEngine.PlaySound(3, (int)this._dryad.Center.X, (int)this._dryad.Center.Y, 1, 1f, 0f);
				}
				if (evt.Frame >= 20)
				{
					this._dryad.ai[0] = 1f;
					this._dryad.ai[1] = (float)evt.Remaining;
					this._dryad.rotation += 0.05f;
				}
			}
			if (this._ogre != null)
			{
				if (evt.Frame > 40)
				{
					this._ogre.target = Main.myPlayer;
					this._ogre.direction = 1;
					return;
				}
				this._ogre.direction = -1;
				this._ogre.ai[1] = 0f;
				this._ogre.ai[0] = Math.Min(40f, this._ogre.ai[0]);
				this._ogre.target = 300 + this._dryad.whoAmI;
			}
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x00652CF4 File Offset: 0x00650EF4
		private void RemoveEnemyDamage(FrameEventData evt)
		{
			this._ogre.friendly = true;
			foreach (NPC npc in this._army)
			{
				npc.friendly = true;
			}
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x00652D54 File Offset: 0x00650F54
		private void RestoreEnemyDamage(FrameEventData evt)
		{
			this._ogre.friendly = false;
			foreach (NPC npc in this._army)
			{
				npc.friendly = false;
			}
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x00652DB4 File Offset: 0x00650FB4
		private void DryadPortalFade(FrameEventData evt)
		{
			if (this._dryad != null && this._portal != null)
			{
				if (evt.IsFirstFrame)
				{
					SoundEngine.PlaySound(SoundID.DD2_EtherianPortalDryadTouch, this._dryad.Center, 0f, 1f);
				}
				float num = (float)(evt.Frame - 7) / (float)(evt.Duration - 7);
				num = Math.Max(0f, num);
				this._dryad.color = new Color(Vector3.Lerp(Vector3.One, new Vector3(0.5f, 0f, 0.8f), num));
				this._dryad.Opacity = 1f - num;
				this._dryad.rotation += 0.05f * (num * 4f + 1f);
				this._dryad.scale = 1f - num;
				if (this._dryad.position.X < this._portal.Right.X)
				{
					NPC dryad = this._dryad;
					dryad.velocity.X = dryad.velocity.X * 0.95f;
					NPC dryad2 = this._dryad;
					dryad2.velocity.Y = dryad2.velocity.Y * 0.55f;
				}
				int num2 = (int)(6f * num);
				float num3 = this._dryad.Size.Length() / 2f;
				num3 /= 20f;
				for (int i = 0; i < num2; i++)
				{
					if (Main.rand.Next(5) == 0)
					{
						Dust dust = Dust.NewDustDirect(this._dryad.position, this._dryad.width, this._dryad.height, 27, this._dryad.velocity.X * 1f, 0f, 100, default(Color), 1f);
						dust.scale = 0.55f;
						dust.fadeIn = 0.7f;
						dust.velocity *= 0.1f * num3;
						dust.velocity += this._dryad.velocity;
					}
				}
			}
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x00652FDC File Offset: 0x006511DC
		private void CreatePortal(FrameEventData evt)
		{
			this._portal = this.PlaceNPCOnGround(549, this._startPoint + new Vector2(-240f, 0f));
			this._portal.immortal = true;
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x00653015 File Offset: 0x00651215
		private void DryadStand(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.ai[0] = 0f;
				this._dryad.ai[1] = (float)evt.Remaining;
			}
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x00653046 File Offset: 0x00651246
		private void DryadLookRight(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.direction = 1;
				this._dryad.spriteDirection = 1;
			}
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x00653068 File Offset: 0x00651268
		private void DryadLookLeft(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.direction = -1;
				this._dryad.spriteDirection = -1;
			}
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x0065308A File Offset: 0x0065128A
		private void DryadWalk(FrameEventData evt)
		{
			this._dryad.ai[0] = 1f;
			this._dryad.ai[1] = 2f;
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x006530B0 File Offset: 0x006512B0
		private void DryadConfusedEmote(FrameEventData evt)
		{
			if (this._dryad != null && evt.IsFirstFrame)
			{
				EmoteBubble.NewBubble(87, new WorldUIAnchor(this._dryad), evt.Duration);
			}
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x006530DD File Offset: 0x006512DD
		private void DryadAlertEmote(FrameEventData evt)
		{
			if (this._dryad != null && evt.IsFirstFrame)
			{
				EmoteBubble.NewBubble(3, new WorldUIAnchor(this._dryad), evt.Duration);
			}
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x0065310C File Offset: 0x0065130C
		private void CreateOgre(FrameEventData evt)
		{
			int num = NPC.NewNPC(new EntitySource_Film(), (int)this._portal.Center.X, (int)this._portal.Bottom.Y, 576, 0, 0f, 0f, 0f, 0f, 255);
			this._ogre = Main.npc[num];
			this._ogre.knockBackResist = 0f;
			this._ogre.immortal = true;
			this._ogre.dontTakeDamage = true;
			this._ogre.takenDamageMultiplier = 0f;
			this._ogre.immune[255] = 100000;
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x006531C0 File Offset: 0x006513C0
		private void OgreStand(FrameEventData evt)
		{
			if (this._ogre != null)
			{
				this._ogre.ai[0] = 0f;
				this._ogre.ai[1] = 0f;
				this._ogre.velocity = Vector2.Zero;
			}
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x006531FE File Offset: 0x006513FE
		private void DryadAttack(FrameEventData evt)
		{
			if (this._dryad != null)
			{
				this._dryad.ai[0] = 14f;
				this._dryad.ai[1] = (float)evt.Remaining;
				this._dryad.dryadWard = false;
			}
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x0065323B File Offset: 0x0065143B
		private void OgreLookRight(FrameEventData evt)
		{
			if (this._ogre != null)
			{
				this._ogre.direction = 1;
				this._ogre.spriteDirection = 1;
			}
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x0065325D File Offset: 0x0065145D
		private void OgreLookLeft(FrameEventData evt)
		{
			if (this._ogre != null)
			{
				this._ogre.direction = -1;
				this._ogre.spriteDirection = -1;
			}
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x00653280 File Offset: 0x00651480
		public override void OnBegin()
		{
			Main.NewText("DD2Film: Begin", byte.MaxValue, byte.MaxValue, byte.MaxValue);
			Main.dayTime = true;
			Main.time = 27000.0;
			this._startPoint = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY - 32f);
			base.OnBegin();
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x006532E8 File Offset: 0x006514E8
		private NPC PlaceNPCOnGround(int type, Vector2 position)
		{
			int num = (int)position.X;
			int num2 = (int)position.Y;
			int num3 = num / 16;
			int num4 = num2 / 16;
			while (!WorldGen.SolidTile(num3, num4, false))
			{
				num4++;
			}
			num2 = num4 * 16;
			int num5 = 100;
			if (type == 20)
			{
				num5 = 1;
			}
			else if (type == 576)
			{
				num5 = 50;
			}
			int num6 = NPC.NewNPC(new EntitySource_Film(), num, num2, type, num5, 0f, 0f, 0f, 0f, 255);
			return Main.npc[num6];
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x00653370 File Offset: 0x00651570
		public override void OnEnd()
		{
			if (this._dryad != null)
			{
				this._dryad.active = false;
			}
			if (this._portal != null)
			{
				this._portal.active = false;
			}
			if (this._ogre != null)
			{
				this._ogre.active = false;
			}
			foreach (NPC npc in this._critters)
			{
				npc.active = false;
			}
			foreach (NPC npc2 in this._army)
			{
				npc2.active = false;
			}
			Main.NewText("DD2Film: End", byte.MaxValue, byte.MaxValue, byte.MaxValue);
			base.OnEnd();
		}

		// Token: 0x04005DA2 RID: 23970
		private NPC _dryad;

		// Token: 0x04005DA3 RID: 23971
		private NPC _ogre;

		// Token: 0x04005DA4 RID: 23972
		private NPC _portal;

		// Token: 0x04005DA5 RID: 23973
		private List<NPC> _army = new List<NPC>();

		// Token: 0x04005DA6 RID: 23974
		private List<NPC> _critters = new List<NPC>();

		// Token: 0x04005DA7 RID: 23975
		private Vector2 _startPoint;
	}
}

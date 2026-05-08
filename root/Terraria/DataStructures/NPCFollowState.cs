using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000557 RID: 1367
	public class NPCFollowState
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x0062F939 File Offset: 0x0062DB39
		public Vector2 BreadcrumbPosition
		{
			get
			{
				return this._floorBreadcrumb;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x0062F941 File Offset: 0x0062DB41
		public bool IsFollowingPlayer
		{
			get
			{
				return this._playerIndexBeingFollowed != null;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x0062F94E File Offset: 0x0062DB4E
		public Player PlayerBeingFollowed
		{
			get
			{
				if (this._playerIndexBeingFollowed != null)
				{
					return Main.player[this._playerIndexBeingFollowed.Value];
				}
				return null;
			}
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x0062F970 File Offset: 0x0062DB70
		public void FollowPlayer(int playerIndex)
		{
			this._playerIndexBeingFollowed = new int?(playerIndex);
			this._floorBreadcrumb = Main.player[playerIndex].Bottom;
			this._npc.netUpdate = true;
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x0062F99C File Offset: 0x0062DB9C
		public void StopFollowing()
		{
			this._playerIndexBeingFollowed = null;
			this.MoveNPCBackHome();
			this._npc.netUpdate = true;
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x0062F9BC File Offset: 0x0062DBBC
		public void Clear(NPC npcToBelongTo)
		{
			this._npc = npcToBelongTo;
			this._playerIndexBeingFollowed = null;
			this._floorBreadcrumb = default(Vector2);
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x0062F9DD File Offset: 0x0062DBDD
		private bool ShouldSync()
		{
			return this._npc.isLikeATownNPC;
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x0062F9EC File Offset: 0x0062DBEC
		public void WriteTo(BinaryWriter writer)
		{
			int num = ((this._playerIndexBeingFollowed != null) ? this._playerIndexBeingFollowed.Value : (-1));
			writer.Write((short)num);
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x0062FA20 File Offset: 0x0062DC20
		public void ReadFrom(BinaryReader reader)
		{
			short num = reader.ReadInt16();
			if (Main.player.IndexInRange((int)num))
			{
				this._playerIndexBeingFollowed = new int?((int)num);
			}
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x0062FA50 File Offset: 0x0062DC50
		private void MoveNPCBackHome()
		{
			this._npc.ai[0] = 20f;
			this._npc.ai[1] = 0f;
			this._npc.ai[2] = 0f;
			this._npc.ai[3] = 0f;
			this._npc.netUpdate = true;
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x0062FAB4 File Offset: 0x0062DCB4
		public void Update()
		{
			if (!this.IsFollowingPlayer)
			{
				return;
			}
			Player playerBeingFollowed = this.PlayerBeingFollowed;
			if (!playerBeingFollowed.active || playerBeingFollowed.dead)
			{
				this.StopFollowing();
				return;
			}
			this.UpdateBreadcrumbs(playerBeingFollowed);
			Dust.QuickDust(this._floorBreadcrumb, Color.Red);
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x0062FB00 File Offset: 0x0062DD00
		private void UpdateBreadcrumbs(Player player)
		{
			Vector2? vector = null;
			if (player.velocity.Y == 0f && player.gravDir == 1f)
			{
				vector = new Vector2?(player.Bottom);
			}
			int num = 8;
			if (vector != null && Vector2.Distance(vector.Value, this._floorBreadcrumb) >= (float)num)
			{
				this._floorBreadcrumb = vector.Value;
				this._npc.netUpdate = true;
			}
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x0000357B File Offset: 0x0000177B
		public NPCFollowState()
		{
		}

		// Token: 0x04005BC9 RID: 23497
		private NPC _npc;

		// Token: 0x04005BCA RID: 23498
		private int? _playerIndexBeingFollowed;

		// Token: 0x04005BCB RID: 23499
		private Vector2 _floorBreadcrumb;
	}
}

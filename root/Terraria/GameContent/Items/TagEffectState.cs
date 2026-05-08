using System;
using System.IO;
using System.Runtime.CompilerServices;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Items
{
	// Token: 0x0200046C RID: 1132
	public class TagEffectState
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x005F2D92 File Offset: 0x005F0F92
		// (set) Token: 0x060032E5 RID: 13029 RVA: 0x005F2D9A File Offset: 0x005F0F9A
		public int Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Type>k__BackingField = value;
			}
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x005F2DA3 File Offset: 0x005F0FA3
		public TagEffectState(Player owner)
		{
			this._owner = owner;
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x005F2DD2 File Offset: 0x005F0FD2
		public bool IsNPCTagged(int npcIndex)
		{
			return this.TimeLeftOnNPC[npcIndex] > 0;
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x005F2DDF File Offset: 0x005F0FDF
		public bool CanProcOnNPC(int npcIndex)
		{
			return this.ProcTimeLeftOnNPC[npcIndex] > 0;
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x005F2DEC File Offset: 0x005F0FEC
		public void ClearProcOnNPC(int npcIndex)
		{
			this.ProcTimeLeftOnNPC[npcIndex] = 0;
			if (this._effect.NetSync && this._owner == Main.LocalPlayer)
			{
				NetManager.Instance.SendToServer(TagEffectState.NetModule.WriteClearProcOnNPC(this, npcIndex));
			}
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x005F2E22 File Offset: 0x005F1022
		public void ResetNPCSlotData(int npcIndex)
		{
			this.TimeLeftOnNPC[npcIndex] = 0;
			this.ProcTimeLeftOnNPC[npcIndex] = 0;
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x005F2E38 File Offset: 0x005F1038
		private void ApplyTagToNPC(NPC npc)
		{
			if (this._effect == null)
			{
				return;
			}
			this.TimeLeftOnNPC[npc.whoAmI] = this._effect.TagDuration;
			if (this._effect.NetSync && this._owner == Main.LocalPlayer)
			{
				NetManager.Instance.SendToServer(TagEffectState.NetModule.WriteApplyTagToNPC(this, npc.whoAmI));
			}
			this._effect.OnTagAppliedToNPC(this._owner, npc);
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x005F2EA8 File Offset: 0x005F10A8
		private void EnableProcOnNPC(NPC npc)
		{
			if (this._effect == null)
			{
				return;
			}
			this.ProcTimeLeftOnNPC[npc.whoAmI] = this._effect.TagDuration;
			if (this._effect.NetSync && this._owner == Main.LocalPlayer)
			{
				NetManager.Instance.SendToServer(TagEffectState.NetModule.WriteEnableProcOnNPC(this, npc.whoAmI));
			}
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x005F2F08 File Offset: 0x005F1108
		public void Update()
		{
			if (this._effect == null)
			{
				return;
			}
			for (int i = 0; i < this.TimeLeftOnNPC.Length; i++)
			{
				if (this.TimeLeftOnNPC[i] > 0)
				{
					this.TimeLeftOnNPC[i]--;
				}
			}
			for (int j = 0; j < this.ProcTimeLeftOnNPC.Length; j++)
			{
				if (this.ProcTimeLeftOnNPC[j] > 0)
				{
					this.ProcTimeLeftOnNPC[j]--;
				}
			}
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x005F2F7C File Offset: 0x005F117C
		private void Clear()
		{
			Array.Clear(this.TimeLeftOnNPC, 0, this.TimeLeftOnNPC.Length);
			Array.Clear(this.ProcTimeLeftOnNPC, 0, this.ProcTimeLeftOnNPC.Length);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x005F2FA6 File Offset: 0x005F11A6
		public void TryApplyTagToNPC(int itemType, NPC npc)
		{
			if (!ItemID.Sets.UniqueTagEffects[itemType].CanApplyTagToNPC(npc.type))
			{
				return;
			}
			this.TrySetActiveEffect(itemType);
			this.ApplyTagToNPC(npc);
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x005F2FCB File Offset: 0x005F11CB
		public void TryEnableProcOnNPC(int expectedActiveEffectType, NPC npc)
		{
			if (this.Type != expectedActiveEffectType)
			{
				return;
			}
			this.EnableProcOnNPC(npc);
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x005F2FE0 File Offset: 0x005F11E0
		public void TrySetActiveEffect(int type)
		{
			if (this.Type == type)
			{
				return;
			}
			if (this._effect != null)
			{
				this._effect.OnRemovedFromPlayer(this._owner);
			}
			this.Clear();
			UniqueTagEffect effect = this._effect;
			this.Type = type;
			this._effect = ItemID.Sets.UniqueTagEffects[type];
			if (this._owner == Main.LocalPlayer && ((this._effect != null && this._effect.NetSync) || (effect != null && effect.NetSync)))
			{
				NetManager.Instance.SendToServer(TagEffectState.NetModule.WriteChangeActiveEffect(this));
			}
			if (this._effect != null)
			{
				this._effect.OnSetToPlayer(this._owner);
			}
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x005F3088 File Offset: 0x005F1288
		public void ModifyHit(Projectile optionalProjectile, NPC npcHit, ref int damageDealt, ref bool crit)
		{
			if (this._effect == null)
			{
				return;
			}
			if (!this.IsNPCTagged(npcHit.whoAmI))
			{
				return;
			}
			if (!this._effect.CanRunHitEffects(this._owner, optionalProjectile, npcHit))
			{
				return;
			}
			this._effect.ModifyTaggedHit(this._owner, optionalProjectile, npcHit, ref damageDealt, ref crit);
			if (this.CanProcOnNPC(npcHit.whoAmI))
			{
				this._effect.ModifyProcHit(this._owner, optionalProjectile, npcHit, ref damageDealt, ref crit);
			}
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x005F3100 File Offset: 0x005F1300
		public void OnHit(Projectile optionalProjectile, NPC npcHit, int calcDamage)
		{
			if (this._effect == null)
			{
				return;
			}
			if (!this.IsNPCTagged(npcHit.whoAmI))
			{
				return;
			}
			if (!this._effect.CanRunHitEffects(this._owner, optionalProjectile, npcHit))
			{
				return;
			}
			this._effect.OnTaggedHit(this._owner, optionalProjectile, npcHit, calcDamage);
			if (this.CanProcOnNPC(npcHit.whoAmI))
			{
				this.ClearProcOnNPC(npcHit.whoAmI);
				this._effect.OnProcHit(this._owner, optionalProjectile, npcHit, calcDamage);
			}
		}

		// Token: 0x04005875 RID: 22645
		private readonly Player _owner;

		// Token: 0x04005876 RID: 22646
		[CompilerGenerated]
		private int <Type>k__BackingField;

		// Token: 0x04005877 RID: 22647
		private UniqueTagEffect _effect;

		// Token: 0x04005878 RID: 22648
		private readonly int[] TimeLeftOnNPC = new int[Main.maxNPCs];

		// Token: 0x04005879 RID: 22649
		private readonly int[] ProcTimeLeftOnNPC = new int[Main.maxNPCs];

		// Token: 0x0200096F RID: 2415
		public class NetModule : Terraria.Net.NetModule
		{
			// Token: 0x060048F1 RID: 18673 RVA: 0x006D05BC File Offset: 0x006CE7BC
			public static void WriteSparseNPCTimeArray(BinaryWriter writer, int[] array)
			{
				for (int i = 0; i < array.Length; i++)
				{
					int num = array[i];
					if (num != 0)
					{
						writer.Write((byte)i);
						writer.Write(num);
					}
				}
				writer.Write((byte)array.Length);
			}

			// Token: 0x060048F2 RID: 18674 RVA: 0x006D05F8 File Offset: 0x006CE7F8
			public static void ReadSparseNPCTimeArray(BinaryReader reader, int[] array)
			{
				Array.Clear(array, 0, array.Length);
				for (;;)
				{
					int num = (int)reader.ReadByte();
					if (num >= array.Length)
					{
						break;
					}
					array[num] = reader.ReadInt32();
				}
			}

			// Token: 0x060048F3 RID: 18675 RVA: 0x006D0628 File Offset: 0x006CE828
			public static NetPacket WriteFullState(TagEffectState state)
			{
				NetPacket netPacket = Terraria.Net.NetModule.CreatePacket<TagEffectState.NetModule>(65530);
				netPacket.Writer.Write((byte)state._owner.whoAmI);
				netPacket.Writer.Write(0);
				netPacket.Writer.Write((short)state.Type);
				TagEffectState.NetModule.WriteSparseNPCTimeArray(netPacket.Writer, state.TimeLeftOnNPC);
				if (state._effect.SyncProcs)
				{
					TagEffectState.NetModule.WriteSparseNPCTimeArray(netPacket.Writer, state.ProcTimeLeftOnNPC);
				}
				return netPacket;
			}

			// Token: 0x060048F4 RID: 18676 RVA: 0x006D06AC File Offset: 0x006CE8AC
			public static NetPacket WriteChangeActiveEffect(TagEffectState state)
			{
				NetPacket netPacket = Terraria.Net.NetModule.CreatePacket<TagEffectState.NetModule>(65530);
				netPacket.Writer.Write((byte)state._owner.whoAmI);
				netPacket.Writer.Write(1);
				netPacket.Writer.Write((short)state.Type);
				return netPacket;
			}

			// Token: 0x060048F5 RID: 18677 RVA: 0x006D0700 File Offset: 0x006CE900
			private static NetPacket WriteNPCChange(TagEffectState state, TagEffectState.NetModule.MessageType msgType, int npcIndex)
			{
				NetPacket netPacket = Terraria.Net.NetModule.CreatePacket<TagEffectState.NetModule>(65530);
				netPacket.Writer.Write((byte)state._owner.whoAmI);
				netPacket.Writer.Write((byte)msgType);
				netPacket.Writer.Write((byte)npcIndex);
				return netPacket;
			}

			// Token: 0x060048F6 RID: 18678 RVA: 0x006D074D File Offset: 0x006CE94D
			public static NetPacket WriteApplyTagToNPC(TagEffectState state, int npcIndex)
			{
				return TagEffectState.NetModule.WriteNPCChange(state, TagEffectState.NetModule.MessageType.ApplyTagToNPC, npcIndex);
			}

			// Token: 0x060048F7 RID: 18679 RVA: 0x006D0757 File Offset: 0x006CE957
			public static NetPacket WriteEnableProcOnNPC(TagEffectState state, int npcIndex)
			{
				return TagEffectState.NetModule.WriteNPCChange(state, TagEffectState.NetModule.MessageType.EnableProcOnNPC, npcIndex);
			}

			// Token: 0x060048F8 RID: 18680 RVA: 0x006D0761 File Offset: 0x006CE961
			public static NetPacket WriteClearProcOnNPC(TagEffectState state, int npcIndex)
			{
				return TagEffectState.NetModule.WriteNPCChange(state, TagEffectState.NetModule.MessageType.ClearProcOnNPC, npcIndex);
			}

			// Token: 0x060048F9 RID: 18681 RVA: 0x006D076C File Offset: 0x006CE96C
			public override bool Deserialize(BinaryReader reader, int userId)
			{
				int num = (int)reader.ReadByte();
				if (Main.netMode == 2)
				{
					num = userId;
				}
				TagEffectState tagEffectState = Main.player[num].TagEffectState;
				TagEffectState.NetModule.MessageType messageType = (TagEffectState.NetModule.MessageType)reader.ReadByte();
				switch (messageType)
				{
				case TagEffectState.NetModule.MessageType.FullState:
					if (Main.netMode == 2)
					{
						return false;
					}
					tagEffectState.TrySetActiveEffect((int)reader.ReadInt16());
					TagEffectState.NetModule.ReadSparseNPCTimeArray(reader, tagEffectState.TimeLeftOnNPC);
					if (tagEffectState._effect.SyncProcs)
					{
						TagEffectState.NetModule.ReadSparseNPCTimeArray(reader, tagEffectState.ProcTimeLeftOnNPC);
					}
					break;
				case TagEffectState.NetModule.MessageType.ChangeActiveEffect:
					tagEffectState.TrySetActiveEffect((int)reader.ReadInt16());
					if (Main.netMode == 2)
					{
						NetManager.Instance.Broadcast(TagEffectState.NetModule.WriteChangeActiveEffect(tagEffectState), num);
					}
					break;
				case TagEffectState.NetModule.MessageType.ApplyTagToNPC:
				case TagEffectState.NetModule.MessageType.EnableProcOnNPC:
				case TagEffectState.NetModule.MessageType.ClearProcOnNPC:
				{
					int num2 = (int)reader.ReadByte();
					if (messageType == TagEffectState.NetModule.MessageType.ApplyTagToNPC)
					{
						tagEffectState.ApplyTagToNPC(Main.npc[num2]);
					}
					else if (messageType == TagEffectState.NetModule.MessageType.EnableProcOnNPC)
					{
						tagEffectState.EnableProcOnNPC(Main.npc[num2]);
					}
					else if (messageType == TagEffectState.NetModule.MessageType.ClearProcOnNPC)
					{
						tagEffectState.ClearProcOnNPC(num2);
					}
					if (Main.netMode == 2)
					{
						NetManager.Instance.Broadcast(TagEffectState.NetModule.WriteNPCChange(tagEffectState, messageType, num2), num);
					}
					break;
				}
				}
				return true;
			}

			// Token: 0x060048FA RID: 18682 RVA: 0x006D0878 File Offset: 0x006CEA78
			public static void SyncStateIfNecessary(TagEffectState state, int toClient, int ignoreClient)
			{
				if (state._effect == null || !state._effect.NetSync)
				{
					return;
				}
				NetPacket netPacket = TagEffectState.NetModule.WriteFullState(state);
				if (toClient >= 0)
				{
					NetManager.Instance.SendToClient(netPacket, toClient);
					return;
				}
				NetManager.Instance.Broadcast(netPacket, ignoreClient);
			}

			// Token: 0x060048FB RID: 18683 RVA: 0x0055DC93 File Offset: 0x0055BE93
			public NetModule()
			{
			}

			// Token: 0x02000AE8 RID: 2792
			private enum MessageType
			{
				// Token: 0x040078B3 RID: 30899
				FullState,
				// Token: 0x040078B4 RID: 30900
				ChangeActiveEffect,
				// Token: 0x040078B5 RID: 30901
				ApplyTagToNPC,
				// Token: 0x040078B6 RID: 30902
				EnableProcOnNPC,
				// Token: 0x040078B7 RID: 30903
				ClearProcOnNPC
			}
		}
	}
}

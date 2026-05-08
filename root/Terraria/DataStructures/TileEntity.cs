using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.UI;

namespace Terraria.DataStructures
{
	// Token: 0x020005A8 RID: 1448
	public abstract class TileEntity
	{
		// Token: 0x06003920 RID: 14624 RVA: 0x006514F5 File Offset: 0x0064F6F5
		public static int AssignNewID()
		{
			return TileEntity.TileEntitiesNextID++;
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06003921 RID: 14625 RVA: 0x00651504 File Offset: 0x0064F704
		// (remove) Token: 0x06003922 RID: 14626 RVA: 0x00651538 File Offset: 0x0064F738
		public static event Action _UpdateStart
		{
			[CompilerGenerated]
			add
			{
				Action action = TileEntity._UpdateStart;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref TileEntity._UpdateStart, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = TileEntity._UpdateStart;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref TileEntity._UpdateStart, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06003923 RID: 14627 RVA: 0x0065156C File Offset: 0x0064F76C
		// (remove) Token: 0x06003924 RID: 14628 RVA: 0x006515A0 File Offset: 0x0064F7A0
		public static event Action _UpdateEnd
		{
			[CompilerGenerated]
			add
			{
				Action action = TileEntity._UpdateEnd;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref TileEntity._UpdateEnd, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = TileEntity._UpdateEnd;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref TileEntity._UpdateEnd, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x006515D3 File Offset: 0x0064F7D3
		public static void Clear()
		{
			TileEntity.ByID.Clear();
			TileEntity.ByPosition.Clear();
			TileEntity.UpdateEntities.Clear();
			TileEntity.TileEntitiesNextID = 0;
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x006515FC File Offset: 0x0064F7FC
		public static void PerformUpdates()
		{
			TileEntity.UpdateStart();
			foreach (TileEntity tileEntity in TileEntity.UpdateEntities)
			{
				tileEntity.Update();
			}
			TileEntity.UpdateEnd();
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x00651658 File Offset: 0x0064F858
		private static void UpdateStart()
		{
			if (TileEntity._UpdateStart != null)
			{
				TileEntity._UpdateStart();
			}
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x0065166B File Offset: 0x0064F86B
		private static void UpdateEnd()
		{
			if (TileEntity._UpdateEnd != null)
			{
				TileEntity._UpdateEnd();
			}
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x00651680 File Offset: 0x0064F880
		public static void Add(TileEntity ent)
		{
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[ent.ID] = ent;
				TileEntity.ByPosition[ent.Position] = ent;
				if (ent.RequiresUpdates)
				{
					TileEntity.UpdateEntities.Add(ent);
				}
			}
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnPlaced()
		{
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnRemoved()
		{
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x006516F0 File Offset: 0x0064F8F0
		protected static int Place(int x, int y, int type)
		{
			TileEntity tileEntity = TileEntity.manager.GenerateInstance(type);
			tileEntity.Position = new Point16(x, y);
			tileEntity.ID = TileEntity.AssignNewID();
			tileEntity.type = (byte)type;
			TileEntity.Add(tileEntity);
			tileEntity.OnPlaced();
			return tileEntity.ID;
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x00651730 File Offset: 0x0064F930
		public static void Kill(int x, int y, int type)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && (int)tileEntity.type == type)
			{
				TileEntity.Remove(tileEntity, false);
			}
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x00651764 File Offset: 0x0064F964
		public static void Remove(TileEntity entity, bool ignorePosition = false)
		{
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				if (entity.RequiresUpdates)
				{
					TileEntity.UpdateEntities.Remove(entity);
				}
				TileEntity.ByID.Remove(entity.ID);
				if (!ignorePosition)
				{
					TileEntity.ByPosition.Remove(entity.Position);
				}
			}
			entity.OnRemoved();
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x006517DC File Offset: 0x0064F9DC
		public static void InitializeAll()
		{
			TileEntity.manager = new TileEntitiesManager();
			TileEntity.manager.RegisterAll();
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x006517F2 File Offset: 0x0064F9F2
		public static void PlaceEntityNet(int x, int y, int type)
		{
			if (!WorldGen.InWorld(x, y, 0))
			{
				return;
			}
			if (TileEntity.ByPosition.ContainsKey(new Point16(x, y)))
			{
				return;
			}
			TileEntity.manager.NetPlaceEntity(type, x, y);
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x00651820 File Offset: 0x0064FA20
		public static bool TryGetAt<T>(int x, int y, out T result) where T : TileEntity
		{
			result = default(T);
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity))
			{
				result = tileEntity as T;
			}
			return result != null;
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x00651868 File Offset: 0x0064FA68
		public static bool TryGet<T>(int id, out T result) where T : TileEntity
		{
			result = default(T);
			TileEntity tileEntity;
			if (TileEntity.ByID.TryGetValue(id, out tileEntity))
			{
				result = tileEntity as T;
			}
			return result != null;
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void Update()
		{
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x006518AA File Offset: 0x0064FAAA
		public static void Write(BinaryWriter writer, TileEntity ent, bool networkSend = false)
		{
			writer.Write(ent.type);
			ent.WriteInner(writer, networkSend);
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x006518C0 File Offset: 0x0064FAC0
		public static TileEntity Read(BinaryReader reader, int gameVersion, bool networkSend = false)
		{
			byte b = reader.ReadByte();
			TileEntity tileEntity = TileEntity.manager.GenerateInstance((int)b);
			tileEntity.type = b;
			tileEntity.ReadInner(reader, gameVersion, networkSend);
			return tileEntity;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x006518EF File Offset: 0x0064FAEF
		private void WriteInner(BinaryWriter writer, bool networkSend)
		{
			if (!networkSend)
			{
				writer.Write(this.ID);
			}
			writer.Write(this.Position.X);
			writer.Write(this.Position.Y);
			this.WriteExtraData(writer, networkSend);
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x0065192A File Offset: 0x0064FB2A
		private void ReadInner(BinaryReader reader, int gameVersion, bool networkSend)
		{
			if (!networkSend)
			{
				this.ID = reader.ReadInt32();
			}
			this.Position = new Point16(reader.ReadInt16(), reader.ReadInt16());
			this.ReadExtraData(reader, gameVersion, networkSend);
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void ReadExtraData(BinaryReader reader, int gameVersion, bool networkSend)
		{
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnPlayerUpdate(Player player)
		{
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x0065195C File Offset: 0x0064FB5C
		public static bool IsOccupied(int id, out int interactingPlayer)
		{
			interactingPlayer = -1;
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active && !player.dead && player.tileEntityAnchor.interactEntityID == id)
				{
					interactingPlayer = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnInventoryDraw(Player player, SpriteBatch spriteBatch)
		{
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x006519A8 File Offset: 0x0064FBA8
		public virtual ItemSlot.AlternateClickAction? GetShiftClickAction(Item[] inv, int context = 0, int slot = 0)
		{
			return null;
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public virtual bool PerformShiftClickAction(Item[] inv, int context = 0, int slot = 0)
		{
			return false;
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x006519C0 File Offset: 0x0064FBC0
		public static void BasicOpenCloseInteraction(Player player, int x, int y, int id)
		{
			player.CloseSign(false);
			if (Main.netMode != 1)
			{
				Main.stackSplit = 600;
				player.GamepadEnableGrappleCooldown();
				int num;
				if (!TileEntity.IsOccupied(id, out num))
				{
					TileEntity.SetInteractionAnchor(player, x, y, id);
					return;
				}
				if (num == player.whoAmI)
				{
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
					player.tileEntityAnchor.Clear();
					return;
				}
			}
			else
			{
				Main.stackSplit = 600;
				player.GamepadEnableGrappleCooldown();
				int num;
				if (TileEntity.IsOccupied(id, out num))
				{
					if (num == player.whoAmI)
					{
						SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
						player.tileEntityAnchor.Clear();
						NetMessage.SendData(122, -1, -1, null, -1, (float)Main.myPlayer, 0f, 0f, 0, 0, 0);
						return;
					}
				}
				else
				{
					NetMessage.SendData(122, -1, -1, null, id, (float)Main.myPlayer, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x00651AB0 File Offset: 0x0064FCB0
		public static void SetInteractionAnchor(Player player, int x, int y, int id)
		{
			player.chest = -1;
			player.SetTalkNPC(-1);
			if (player.whoAmI == Main.myPlayer)
			{
				bool flag = player.tileEntityAnchor.interactEntityID == -1;
				IngameUIWindows.CloseAll(true);
				Main.playerInventory = true;
				Main.PipsUseGrid = false;
				if (PlayerInput.GrappleAndInteractAreShared)
				{
					PlayerInput.Triggers.JustPressed.Grapple = false;
				}
				if (!flag)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				else
				{
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
			}
			player.tileEntityAnchor.Set(id, x, y);
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void RegisterTileEntityID(int assignedID)
		{
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void NetPlaceEntityAttempt(int x, int y)
		{
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public virtual bool IsTileValidForEntity(int x, int y)
		{
			return false;
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x00076333 File Offset: 0x00074533
		public virtual TileEntity GenerateInstance()
		{
			return null;
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnWorldLoaded()
		{
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x0000357B File Offset: 0x0000177B
		protected TileEntity()
		{
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x00651B4B File Offset: 0x0064FD4B
		// Note: this type is marked as 'beforefieldinit'.
		static TileEntity()
		{
		}

		// Token: 0x04005D79 RID: 23929
		public static TileEntitiesManager manager;

		// Token: 0x04005D7A RID: 23930
		public const int MaxEntitiesPerChunk = 1000;

		// Token: 0x04005D7B RID: 23931
		public static object EntityCreationLock = new object();

		// Token: 0x04005D7C RID: 23932
		public static List<TileEntity> UpdateEntities = new List<TileEntity>();

		// Token: 0x04005D7D RID: 23933
		public static Dictionary<int, TileEntity> ByID = new Dictionary<int, TileEntity>();

		// Token: 0x04005D7E RID: 23934
		public static Dictionary<Point16, TileEntity> ByPosition = new Dictionary<Point16, TileEntity>();

		// Token: 0x04005D7F RID: 23935
		public static int TileEntitiesNextID;

		// Token: 0x04005D80 RID: 23936
		[CompilerGenerated]
		private static Action _UpdateStart;

		// Token: 0x04005D81 RID: 23937
		[CompilerGenerated]
		private static Action _UpdateEnd;

		// Token: 0x04005D82 RID: 23938
		public int ID;

		// Token: 0x04005D83 RID: 23939
		public Point16 Position;

		// Token: 0x04005D84 RID: 23940
		public byte type;

		// Token: 0x04005D85 RID: 23941
		public bool RequiresUpdates;
	}
}

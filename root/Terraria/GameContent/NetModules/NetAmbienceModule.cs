using System;
using System.IO;
using System.Runtime.CompilerServices;
using Terraria.GameContent.Ambience;
using Terraria.GameContent.Skies;
using Terraria.Graphics.Effects;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020002DD RID: 733
	public class NetAmbienceModule : NetModule
	{
		// Token: 0x06002620 RID: 9760 RVA: 0x0055DBE4 File Offset: 0x0055BDE4
		public static NetPacket SerializeSkyEntitySpawn(Player player, SkyEntityType type)
		{
			int num = Main.rand.Next();
			NetPacket netPacket = NetModule.CreatePacket<NetAmbienceModule>(65530);
			netPacket.Writer.Write((byte)player.whoAmI);
			netPacket.Writer.Write(num);
			netPacket.Writer.Write((byte)type);
			return netPacket;
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x0055DC38 File Offset: 0x0055BE38
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			if (Main.dedServ)
			{
				return false;
			}
			byte playerId = reader.ReadByte();
			int seed = reader.ReadInt32();
			SkyEntityType type = (SkyEntityType)reader.ReadByte();
			if (Main.remixWorld)
			{
				return true;
			}
			Main.QueueMainThreadAction(delegate
			{
				((AmbientSky)SkyManager.Instance["Ambience"]).Spawn(Main.player[(int)playerId], type, seed);
			});
			return true;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x0055DC93 File Offset: 0x0055BE93
		public NetAmbienceModule()
		{
		}

		// Token: 0x02000825 RID: 2085
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x06004317 RID: 17175 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x06004318 RID: 17176 RVA: 0x006C0AFD File Offset: 0x006BECFD
			internal void <Deserialize>b__0()
			{
				((AmbientSky)SkyManager.Instance["Ambience"]).Spawn(Main.player[(int)this.playerId], this.type, this.seed);
			}

			// Token: 0x04007256 RID: 29270
			public byte playerId;

			// Token: 0x04007257 RID: 29271
			public SkyEntityType type;

			// Token: 0x04007258 RID: 29272
			public int seed;
		}
	}
}

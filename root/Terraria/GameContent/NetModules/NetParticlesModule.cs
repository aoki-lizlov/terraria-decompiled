using System;
using System.IO;
using Terraria.GameContent.Drawing;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020002E2 RID: 738
	public class NetParticlesModule : NetModule
	{
		// Token: 0x06002631 RID: 9777 RVA: 0x0055DFB4 File Offset: 0x0055C1B4
		public static NetPacket Serialize(ParticleOrchestraType particleType, ParticleOrchestraSettings settings)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetParticlesModule>(22);
			netPacket.Writer.Write((byte)particleType);
			settings.Serialize(netPacket.Writer);
			return netPacket;
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x0055DFE8 File Offset: 0x0055C1E8
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			ParticleOrchestraType particleOrchestraType = (ParticleOrchestraType)reader.ReadByte();
			ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
			particleOrchestraSettings.DeserializeFrom(reader);
			if (Main.netMode == 2)
			{
				NetManager.Instance.Broadcast(NetParticlesModule.Serialize(particleOrchestraType, particleOrchestraSettings), userId);
			}
			else
			{
				ParticleOrchestrator.SpawnParticlesDirect(particleOrchestraType, particleOrchestraSettings);
			}
			return true;
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x0055DC93 File Offset: 0x0055BE93
		public NetParticlesModule()
		{
		}
	}
}

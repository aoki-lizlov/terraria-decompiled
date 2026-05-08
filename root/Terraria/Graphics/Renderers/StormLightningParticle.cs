using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.Utilities;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000206 RID: 518
	public class StormLightningParticle : IPooledParticle, IParticle
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x0052CCA0 File Offset: 0x0052AEA0
		// (set) Token: 0x06002133 RID: 8499 RVA: 0x0052CCA8 File Offset: 0x0052AEA8
		public bool ShouldBeRemovedFromRenderer
		{
			[CompilerGenerated]
			get
			{
				return this.<ShouldBeRemovedFromRenderer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ShouldBeRemovedFromRenderer>k__BackingField = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x0052CCB1 File Offset: 0x0052AEB1
		// (set) Token: 0x06002135 RID: 8501 RVA: 0x0052CCB9 File Offset: 0x0052AEB9
		public bool IsRestingInPool
		{
			[CompilerGenerated]
			get
			{
				return this.<IsRestingInPool>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsRestingInPool>k__BackingField = value;
			}
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0052CCC2 File Offset: 0x0052AEC2
		public void RestInPool()
		{
			this.IsRestingInPool = true;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0052CCCB File Offset: 0x0052AECB
		public virtual void FetchFromPool()
		{
			this._lifeTimeCounted = 0;
			this._lifeTimeTotal = 0;
			this.IsRestingInPool = false;
			this.ShouldBeRemovedFromRenderer = false;
			this.bolts.Clear();
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0052CCF4 File Offset: 0x0052AEF4
		public void Prepare(uint seed, Vector2 targetPosition, int lifeTimeTotal, Color color)
		{
			this.Color = color;
			this._lifeTimeTotal = lifeTimeTotal;
			LightningGenerator.Bolt bolt = LightningGenerator.StormLightning.Generate(this.bolts, seed, targetPosition, true, true);
			this.StartPosition = bolt.positions[0];
			this.EndPosition = bolt.positions[bolt.positions.Length - 1];
			LCG32Random lcg32Random = new LCG32Random(seed);
			int num = (int)Math.Ceiling((double)((float)bolt.positions.Length / 10f));
			for (int i = 0; i < bolt.positions.Length; i++)
			{
				if (lcg32Random.Next(num) == 0)
				{
					Vector2 vector = bolt.positions[i];
					Vector2 vector2 = Vector2.UnitY;
					if (bolt.rotations != null)
					{
						vector2 = -bolt.rotations[i].ToRotationVector2();
					}
					Dust dust = Dust.NewDustPerfect(vector, 226, null, 0, default(Color), 1f);
					dust.HackFrame(278);
					dust.color = color;
					dust.customData = dust.color;
					dust.velocity = vector2;
					dust.velocity *= 3f + lcg32Random.NextFloat() * 6.5f;
					dust.fadeIn = 0f;
					dust.scale = 0.4f + lcg32Random.NextFloat() * 0.5f;
					dust.noGravity = true;
					dust.position -= dust.velocity * 6f;
					Dust.CloneDust(dust).velocity *= 0.5f;
					dust.scale -= 0.3f;
				}
			}
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0052CEC0 File Offset: 0x0052B0C0
		public void Update(ref ParticleRendererSettings settings)
		{
			Color color = new Color(80, 220, 220);
			float num = (float)this._lifeTimeCounted / (float)this._lifeTimeTotal;
			float num2 = Utils.Remap(num, 0f, 0.4f, 1f, 0f, true);
			if (num < 0.3f)
			{
				ParticleOrchestrator.RequestParticleSpawn(true, ParticleOrchestraType.StormlightningWindup, new ParticleOrchestraSettings
				{
					PositionInWorld = this.StartPosition,
					MovementVector = Vector2.Zero,
					UniqueInfoPiece = (int)color.PackedValue
				}, null);
			}
			for (int i = 0; i < 3; i++)
			{
				if (Main.rand.Next(4) == 0 && Main.rand.NextFloat() <= num2 * 0.13f)
				{
					Dust dust = Dust.NewDustDirect(this.StartPosition, 16, 16, 306, 0f, 0f, 0, new Color((int)color.R, (int)color.G, (int)color.B, 0), 1f);
					dust.velocity = new Vector2(0f, -4f).RotatedByRandom(1.5707963705062866) * (0.5f + 0.2f * Main.rand.NextFloatDirection());
					dust.scale = 1.8f;
					dust.fadeIn = 0f;
					dust.noGravity = Main.rand.Next(3) != 0;
					dust.noLight = (dust.noLightEmittance = true);
					Dust dust2 = Dust.CloneDust(dust);
					dust2.color = new Color(255, 255, 255, 0);
					dust2.scale = 1.3f;
				}
			}
			for (int j = -1; j <= 1; j += 2)
			{
				if (Main.rand.Next(4) == 0 && Main.rand.NextFloat() <= num2 * 0.2f)
				{
					Dust dust3 = Dust.NewDustPerfect(this.StartPosition, 306, new Vector2?(new Vector2(0f, -4f).RotatedBy((double)(0.7853982f * (float)j * 1f), default(Vector2))), 0, default(Color), 1f);
					dust3.color = new Color((int)color.R, (int)color.G, (int)color.B, 0);
					dust3.scale = 1.8f;
					dust3.fadeIn = 0f;
					dust3.noGravity = Main.rand.Next(3) != 0;
					dust3.noLight = (dust3.noLightEmittance = true);
					Dust dust4 = Dust.CloneDust(dust3);
					dust4.color = new Color(255, 255, 255, 0);
					dust4.scale = 1.3f;
				}
			}
			for (int k = 0; k < 2; k++)
			{
				if (Main.rand.Next(4) == 0 && Main.rand.NextFloat() <= 0.2f)
				{
					Dust dust5 = Dust.NewDustPerfect(this.StartPosition, 226, null, 0, default(Color), 1f);
					dust5.HackFrame(278);
					dust5.color = color;
					dust5.customData = dust5.color;
					dust5.velocity *= 1f + Main.rand.NextFloat() * 2.5f;
					dust5.velocity += new Vector2(0f, -2f);
					dust5.fadeIn = 0f;
					dust5.scale = 0.4f + Main.rand.NextFloat() * 0.5f;
					Dust dust6 = dust5;
					dust6.velocity.X = dust6.velocity.X * 2f;
					dust5.velocity = Main.rand.NextVector2Circular(3f, 2f) + new Vector2(0f, -2f);
					dust5.noLight = (dust5.noLightEmittance = true);
					dust5.position -= dust5.velocity * 3f;
				}
			}
			int num3 = this._lifeTimeCounted + 1;
			this._lifeTimeCounted = num3;
			if (num3 >= this._lifeTimeTotal)
			{
				this.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0052D328 File Offset: 0x0052B528
		public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			StormLightningDrawer stormLightningDrawer = default(StormLightningDrawer);
			foreach (LightningGenerator.Bolt bolt in this.bolts)
			{
				float num = (bolt.IsMainBolt ? 1f : (0.5f * (float)Math.Pow(0.8, (double)(bolt.forkDepth - 1))));
				stormLightningDrawer.Draw(bolt.positions, bolt.rotations, 16f, this.Color, (float)this._lifeTimeCounted / (float)this._lifeTimeTotal, bolt.IsMainBolt, bolt.progressRange, num);
			}
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0052D3E4 File Offset: 0x0052B5E4
		public StormLightningParticle()
		{
		}

		// Token: 0x04004B93 RID: 19347
		[CompilerGenerated]
		private bool <ShouldBeRemovedFromRenderer>k__BackingField;

		// Token: 0x04004B94 RID: 19348
		public Color Color;

		// Token: 0x04004B95 RID: 19349
		public Vector2 EndPosition;

		// Token: 0x04004B96 RID: 19350
		public Vector2 StartPosition;

		// Token: 0x04004B97 RID: 19351
		private List<LightningGenerator.Bolt> bolts = new List<LightningGenerator.Bolt>();

		// Token: 0x04004B98 RID: 19352
		[CompilerGenerated]
		private bool <IsRestingInPool>k__BackingField;

		// Token: 0x04004B99 RID: 19353
		private int _lifeTimeCounted;

		// Token: 0x04004B9A RID: 19354
		private int _lifeTimeTotal;
	}
}

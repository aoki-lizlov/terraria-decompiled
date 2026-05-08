using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000203 RID: 515
	public class ParticleRenderer
	{
		// Token: 0x06002127 RID: 8487 RVA: 0x0052CA24 File Offset: 0x0052AC24
		public ParticleRenderer()
		{
			this.Settings = default(ParticleRendererSettings);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0052CA51 File Offset: 0x0052AC51
		public void Add(IParticle particle)
		{
			this.Particles.Add(particle);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0052CA60 File Offset: 0x0052AC60
		public void Clear()
		{
			for (int i = 0; i < this.Particles.Count; i++)
			{
				IPooledParticle pooledParticle = this.Particles[i] as IPooledParticle;
				if (pooledParticle != null)
				{
					pooledParticle.RestInPool();
				}
			}
			this.Particles.Clear();
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0052CAAC File Offset: 0x0052ACAC
		public void Update()
		{
			for (int i = 0; i < this.Particles.Count; i++)
			{
				if (this.Particles[i].ShouldBeRemovedFromRenderer)
				{
					IPooledParticle pooledParticle = this.Particles[i] as IPooledParticle;
					if (pooledParticle != null)
					{
						pooledParticle.RestInPool();
					}
					this.Particles.RemoveAt(i);
					i--;
				}
				else
				{
					this.Particles[i].Update(ref this.Settings);
				}
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0052CB28 File Offset: 0x0052AD28
		public void Draw(SpriteBatch spriteBatch)
		{
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			for (int i = 0; i < this.Particles.Count; i++)
			{
				if (!this.Particles[i].ShouldBeRemovedFromRenderer)
				{
					this.Particles[i].Draw(ref this.Settings, spriteBatch);
				}
			}
			TimeLogger.Particles.AddTime(startTimestamp);
		}

		// Token: 0x04004B8F RID: 19343
		public ParticleRendererSettings Settings;

		// Token: 0x04004B90 RID: 19344
		public List<IParticle> Particles = new List<IParticle>();
	}
}

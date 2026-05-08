using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000204 RID: 516
	public class ParticlePool<T> where T : IPooledParticle
	{
		// Token: 0x0600212C RID: 8492 RVA: 0x0052CB88 File Offset: 0x0052AD88
		public int CountParticlesInUse()
		{
			int num = 0;
			for (int i = 0; i < num; i++)
			{
				T t = this._particles[i];
				if (!t.IsRestingInPool)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0052CBC4 File Offset: 0x0052ADC4
		public ParticlePool(int initialPoolSize, ParticlePool<T>.ParticleInstantiator instantiator)
		{
			this._particles = new List<T>(initialPoolSize);
			this._instantiator = instantiator;
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0052CBE0 File Offset: 0x0052ADE0
		public T RequestParticle()
		{
			if (Main.NoPooling)
			{
				this._particles.RemoveAll((T p) => p.IsRestingInPool);
			}
			int count = this._particles.Count;
			for (int i = 0; i < count; i++)
			{
				T t = this._particles[i];
				if (t.IsRestingInPool)
				{
					t = this._particles[i];
					t.FetchFromPool();
					return this._particles[i];
				}
			}
			T t2 = this._instantiator();
			this._particles.Add(t2);
			t2.FetchFromPool();
			return t2;
		}

		// Token: 0x04004B91 RID: 19345
		private ParticlePool<T>.ParticleInstantiator _instantiator;

		// Token: 0x04004B92 RID: 19346
		private List<T> _particles;

		// Token: 0x020007A9 RID: 1961
		// (Invoke) Token: 0x060041B7 RID: 16823
		public delegate T ParticleInstantiator();

		// Token: 0x020007AA RID: 1962
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060041BA RID: 16826 RVA: 0x006BCA1C File Offset: 0x006BAC1C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060041BB RID: 16827 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060041BC RID: 16828 RVA: 0x006BCA28 File Offset: 0x006BAC28
			internal bool <RequestParticle>b__5_0(T p)
			{
				return p.IsRestingInPool;
			}

			// Token: 0x040070A4 RID: 28836
			public static readonly ParticlePool<T>.<>c <>9 = new ParticlePool<T>.<>c();

			// Token: 0x040070A5 RID: 28837
			public static Predicate<T> <>9__5_0;
		}
	}
}

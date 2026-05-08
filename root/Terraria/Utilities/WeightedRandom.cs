using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.Utilities
{
	// Token: 0x020000D6 RID: 214
	public class WeightedRandom<T>
	{
		// Token: 0x0600185F RID: 6239 RVA: 0x004E2509 File Offset: 0x004E0709
		public WeightedRandom()
		{
			this.random = new UnifiedRandom();
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x004E252E File Offset: 0x004E072E
		public WeightedRandom(int seed)
		{
			this.random = new UnifiedRandom(seed);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x004E2554 File Offset: 0x004E0754
		public WeightedRandom(UnifiedRandom random)
		{
			this.random = random;
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x004E2575 File Offset: 0x004E0775
		public WeightedRandom(params Tuple<T, double>[] theElements)
		{
			this.random = new UnifiedRandom();
			this.elements = theElements.ToList<Tuple<T, double>>();
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x004E25A6 File Offset: 0x004E07A6
		public WeightedRandom(int seed, params Tuple<T, double>[] theElements)
		{
			this.random = new UnifiedRandom(seed);
			this.elements = theElements.ToList<Tuple<T, double>>();
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x004E25D8 File Offset: 0x004E07D8
		public WeightedRandom(UnifiedRandom random, params Tuple<T, double>[] theElements)
		{
			this.random = random;
			this.elements = theElements.ToList<Tuple<T, double>>();
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x004E2605 File Offset: 0x004E0805
		public void Add(T element, double weight = 1.0)
		{
			this.elements.Add(new Tuple<T, double>(element, weight));
			this.needsRefresh = true;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x004E2620 File Offset: 0x004E0820
		public T Get()
		{
			if (this.needsRefresh)
			{
				this.CalculateTotalWeight();
			}
			double num = this.random.NextDouble();
			num *= this._totalWeight;
			foreach (Tuple<T, double> tuple in this.elements)
			{
				if (num <= tuple.Item2)
				{
					return tuple.Item1;
				}
				num -= tuple.Item2;
			}
			return default(T);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x004E26B8 File Offset: 0x004E08B8
		public void CalculateTotalWeight()
		{
			this._totalWeight = 0.0;
			foreach (Tuple<T, double> tuple in this.elements)
			{
				this._totalWeight += tuple.Item2;
			}
			this.needsRefresh = false;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x004E2730 File Offset: 0x004E0930
		public void Clear()
		{
			this.elements.Clear();
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x004E273D File Offset: 0x004E093D
		public static implicit operator T(WeightedRandom<T> weightedRandom)
		{
			return weightedRandom.Get();
		}

		// Token: 0x040012D8 RID: 4824
		public readonly List<Tuple<T, double>> elements = new List<Tuple<T, double>>();

		// Token: 0x040012D9 RID: 4825
		public readonly UnifiedRandom random;

		// Token: 0x040012DA RID: 4826
		public bool needsRefresh = true;

		// Token: 0x040012DB RID: 4827
		private double _totalWeight;
	}
}

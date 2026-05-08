using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.DataStructures
{
	// Token: 0x02000545 RID: 1349
	public class EntrySorter<TEntryType, TStepType> : IComparer<TEntryType> where TEntryType : new() where TStepType : IEntrySortStep<TEntryType>
	{
		// Token: 0x06003777 RID: 14199 RVA: 0x0062F19D File Offset: 0x0062D39D
		public void AddSortSteps(List<TStepType> sortSteps)
		{
			this.Steps.AddRange(sortSteps);
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x0062F1AC File Offset: 0x0062D3AC
		public int Compare(TEntryType x, TEntryType y)
		{
			int num = 0;
			if (this._prioritizedStep != -1)
			{
				TStepType tstepType = this.Steps[this._prioritizedStep];
				num = tstepType.Compare(x, y);
				if (num != 0)
				{
					return num;
				}
			}
			for (int i = 0; i < this.Steps.Count; i++)
			{
				if (i != this._prioritizedStep)
				{
					TStepType tstepType = this.Steps[i];
					num = tstepType.Compare(x, y);
					if (num != 0)
					{
						return num;
					}
				}
			}
			return num;
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x0062F22D File Offset: 0x0062D42D
		public void SetPrioritizedStepIndex(int index)
		{
			this._prioritizedStep = index;
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x0062F238 File Offset: 0x0062D438
		public string GetDisplayName()
		{
			TStepType tstepType = this.Steps[this._prioritizedStep];
			return Language.GetTextValue(tstepType.GetDisplayNameKey());
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x0062F269 File Offset: 0x0062D469
		public EntrySorter()
		{
		}

		// Token: 0x04005BA4 RID: 23460
		public List<TStepType> Steps = new List<TStepType>();

		// Token: 0x04005BA5 RID: 23461
		private int _prioritizedStep;
	}
}

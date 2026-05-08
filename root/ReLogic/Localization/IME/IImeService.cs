using System;

namespace ReLogic.Localization.IME
{
	// Token: 0x0200007E RID: 126
	public interface IImeService
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002D0 RID: 720
		string CompositionString { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002D1 RID: 721
		bool IsCandidateListVisible { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002D2 RID: 722
		uint? SelectedCandidate { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002D3 RID: 723
		uint CandidateCount { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002D4 RID: 724
		bool IsEnabled { get; }

		// Token: 0x060002D5 RID: 725
		string GetCandidate(uint index);

		// Token: 0x060002D6 RID: 726
		void Enable();

		// Token: 0x060002D7 RID: 727
		void Disable();

		// Token: 0x060002D8 RID: 728
		void AddKeyListener(Action<char> listener);

		// Token: 0x060002D9 RID: 729
		void RemoveKeyListener(Action<char> listener);
	}
}

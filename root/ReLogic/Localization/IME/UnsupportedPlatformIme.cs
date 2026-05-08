using System;

namespace ReLogic.Localization.IME
{
	// Token: 0x0200007C RID: 124
	public class UnsupportedPlatformIme : PlatformIme
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000AA53 File Offset: 0x00008C53
		public override uint CandidateCount
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000AA56 File Offset: 0x00008C56
		public override string CompositionString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000AA53 File Offset: 0x00008C53
		public override bool IsCandidateListVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000AA5D File Offset: 0x00008C5D
		public override uint? SelectedCandidate
		{
			get
			{
				return new uint?(0U);
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000AA56 File Offset: 0x00008C56
		public override string GetCandidate(uint index)
		{
			return string.Empty;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000ABFB File Offset: 0x00008DFB
		public UnsupportedPlatformIme()
		{
		}
	}
}

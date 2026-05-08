using System;
using System.Runtime.CompilerServices;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000096 RID: 150
	public class GenPassResult
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x004DD26F File Offset: 0x004DB46F
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x004DD277 File Offset: 0x004DB477
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x004DD280 File Offset: 0x004DB480
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x004DD288 File Offset: 0x004DB488
		public int DurationMs
		{
			[CompilerGenerated]
			get
			{
				return this.<DurationMs>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DurationMs>k__BackingField = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x004DD291 File Offset: 0x004DB491
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x004DD299 File Offset: 0x004DB499
		public int RandNext
		{
			[CompilerGenerated]
			get
			{
				return this.<RandNext>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RandNext>k__BackingField = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x004DD2A2 File Offset: 0x004DB4A2
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x004DD2AA File Offset: 0x004DB4AA
		public uint? Hash
		{
			[CompilerGenerated]
			get
			{
				return this.<Hash>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Hash>k__BackingField = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x004DD2B3 File Offset: 0x004DB4B3
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x004DD2BB File Offset: 0x004DB4BB
		public bool Skipped
		{
			[CompilerGenerated]
			get
			{
				return this.<Skipped>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Skipped>k__BackingField = value;
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x004DD2C4 File Offset: 0x004DB4C4
		public override string ToString()
		{
			if (this.Skipped)
			{
				return string.Format("Pass - {0}: Skipped", this.Name);
			}
			return string.Format("Pass - {0}: {1}ms, rand: {2:X8}, hash: {3:X8}", new object[] { this.Name, this.DurationMs, this.RandNext, this.Hash });
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x004DD330 File Offset: 0x004DB530
		public bool Matches(GenPassResult other)
		{
			if (!(this.Name == other.Name) || this.RandNext != other.RandNext || this.Skipped != other.Skipped)
			{
				return false;
			}
			if (this.Hash != null && other.Hash != null)
			{
				uint? hash = this.Hash;
				uint? hash2 = other.Hash;
				return (hash.GetValueOrDefault() == hash2.GetValueOrDefault()) & (hash != null == (hash2 != null));
			}
			return true;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0000357B File Offset: 0x0000177B
		public GenPassResult()
		{
		}

		// Token: 0x040011C3 RID: 4547
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x040011C4 RID: 4548
		[CompilerGenerated]
		private int <DurationMs>k__BackingField;

		// Token: 0x040011C5 RID: 4549
		[CompilerGenerated]
		private int <RandNext>k__BackingField;

		// Token: 0x040011C6 RID: 4550
		[CompilerGenerated]
		private uint? <Hash>k__BackingField;

		// Token: 0x040011C7 RID: 4551
		[CompilerGenerated]
		private bool <Skipped>k__BackingField;
	}
}

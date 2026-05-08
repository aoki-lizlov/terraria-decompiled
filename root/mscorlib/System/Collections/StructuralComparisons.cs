using System;

namespace System.Collections
{
	// Token: 0x02000A90 RID: 2704
	public static class StructuralComparisons
	{
		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06006303 RID: 25347 RVA: 0x00151D40 File Offset: 0x0014FF40
		public static IComparer StructuralComparer
		{
			get
			{
				IComparer comparer = StructuralComparisons.s_StructuralComparer;
				if (comparer == null)
				{
					comparer = new StructuralComparer();
					StructuralComparisons.s_StructuralComparer = comparer;
				}
				return comparer;
			}
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x06006304 RID: 25348 RVA: 0x00151D68 File Offset: 0x0014FF68
		public static IEqualityComparer StructuralEqualityComparer
		{
			get
			{
				IEqualityComparer equalityComparer = StructuralComparisons.s_StructuralEqualityComparer;
				if (equalityComparer == null)
				{
					equalityComparer = new StructuralEqualityComparer();
					StructuralComparisons.s_StructuralEqualityComparer = equalityComparer;
				}
				return equalityComparer;
			}
		}

		// Token: 0x04003AFB RID: 15099
		private static volatile IComparer s_StructuralComparer;

		// Token: 0x04003AFC RID: 15100
		private static volatile IEqualityComparer s_StructuralEqualityComparer;
	}
}

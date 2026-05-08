using System;

namespace System.Reflection
{
	// Token: 0x02000870 RID: 2160
	[Flags]
	public enum GenericParameterAttributes
	{
		// Token: 0x04002E26 RID: 11814
		None = 0,
		// Token: 0x04002E27 RID: 11815
		VarianceMask = 3,
		// Token: 0x04002E28 RID: 11816
		Covariant = 1,
		// Token: 0x04002E29 RID: 11817
		Contravariant = 2,
		// Token: 0x04002E2A RID: 11818
		SpecialConstraintMask = 28,
		// Token: 0x04002E2B RID: 11819
		ReferenceTypeConstraint = 4,
		// Token: 0x04002E2C RID: 11820
		NotNullableValueTypeConstraint = 8,
		// Token: 0x04002E2D RID: 11821
		DefaultConstructorConstraint = 16
	}
}

using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006E0 RID: 1760
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class BestFitMappingAttribute : Attribute
	{
		// Token: 0x06004056 RID: 16470 RVA: 0x000E1048 File Offset: 0x000DF248
		public BestFitMappingAttribute(bool BestFitMapping)
		{
			this._bestFitMapping = BestFitMapping;
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06004057 RID: 16471 RVA: 0x000E1057 File Offset: 0x000DF257
		public bool BestFitMapping
		{
			get
			{
				return this._bestFitMapping;
			}
		}

		// Token: 0x04002A6F RID: 10863
		internal bool _bestFitMapping;

		// Token: 0x04002A70 RID: 10864
		public bool ThrowOnUnmappableChar;
	}
}

using System;

namespace System.Runtime.Versioning
{
	// Token: 0x0200060C RID: 1548
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class TargetFrameworkAttribute : Attribute
	{
		// Token: 0x06003BBA RID: 15290 RVA: 0x000D0ACC File Offset: 0x000CECCC
		public TargetFrameworkAttribute(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			this._frameworkName = frameworkName;
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x000D0AE9 File Offset: 0x000CECE9
		public string FrameworkName
		{
			get
			{
				return this._frameworkName;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000D0AF1 File Offset: 0x000CECF1
		// (set) Token: 0x06003BBD RID: 15293 RVA: 0x000D0AF9 File Offset: 0x000CECF9
		public string FrameworkDisplayName
		{
			get
			{
				return this._frameworkDisplayName;
			}
			set
			{
				this._frameworkDisplayName = value;
			}
		}

		// Token: 0x04002672 RID: 9842
		private string _frameworkName;

		// Token: 0x04002673 RID: 9843
		private string _frameworkDisplayName;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x0200001E RID: 30
	public abstract class RgbDeviceGroup : IDisposable, IEnumerable<RgbDevice>, IEnumerable
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000467E File Offset: 0x0000287E
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004686 File Offset: 0x00002886
		public bool IsEnabled
		{
			[CompilerGenerated]
			get
			{
				return this.<IsEnabled>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsEnabled>k__BackingField = value;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000468F File Offset: 0x0000288F
		public void Enable()
		{
			this.IsEnabled = true;
			this.Initialize();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000469E File Offset: 0x0000289E
		public void Disable()
		{
			this.IsEnabled = false;
			this.Uninitialize();
		}

		// Token: 0x060000E3 RID: 227
		protected abstract void Initialize();

		// Token: 0x060000E4 RID: 228
		protected abstract void Uninitialize();

		// Token: 0x060000E5 RID: 229 RVA: 0x000046AD File Offset: 0x000028AD
		public virtual void OnceProcessed()
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000046AD File Offset: 0x000028AD
		public virtual void LoadSpecialRules(object specialRulesObject)
		{
		}

		// Token: 0x060000E7 RID: 231
		public abstract IEnumerator<RgbDevice> GetEnumerator();

		// Token: 0x060000E8 RID: 232 RVA: 0x000046AF File Offset: 0x000028AF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000046B7 File Offset: 0x000028B7
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			this.Disable();
			this._isDisposed = true;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000046D0 File Offset: 0x000028D0
		~RgbDeviceGroup()
		{
			this.Dispose(false);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004700 File Offset: 0x00002900
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000448A File Offset: 0x0000268A
		protected RgbDeviceGroup()
		{
		}

		// Token: 0x04000040 RID: 64
		[CompilerGenerated]
		private bool <IsEnabled>k__BackingField;

		// Token: 0x04000041 RID: 65
		private bool _isDisposed;
	}
}

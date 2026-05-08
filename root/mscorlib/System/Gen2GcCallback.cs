using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000E4 RID: 228
	internal sealed class Gen2GcCallback : CriticalFinalizerObject
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x000202AB File Offset: 0x0001E4AB
		private Gen2GcCallback()
		{
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000202B3 File Offset: 0x0001E4B3
		public static void Register(Func<object, bool> callback, object targetObj)
		{
			new Gen2GcCallback().Setup(callback, targetObj);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000202C1 File Offset: 0x0001E4C1
		private void Setup(Func<object, bool> callback, object targetObj)
		{
			this._callback = callback;
			this._weakTargetObj = GCHandle.Alloc(targetObj, GCHandleType.Weak);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000202D8 File Offset: 0x0001E4D8
		protected override void Finalize()
		{
			try
			{
				object target = this._weakTargetObj.Target;
				if (target == null)
				{
					this._weakTargetObj.Free();
				}
				else
				{
					try
					{
						if (!this._callback(target))
						{
							return;
						}
					}
					catch
					{
					}
					if (!Environment.HasShutdownStarted)
					{
						GC.ReRegisterForFinalize(this);
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04000F66 RID: 3942
		private Func<object, bool> _callback;

		// Token: 0x04000F67 RID: 3943
		private GCHandle _weakTargetObj;
	}
}

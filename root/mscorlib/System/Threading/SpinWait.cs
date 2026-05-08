using System;
using Internal.Runtime.Augments;

namespace System.Threading
{
	// Token: 0x02000278 RID: 632
	public struct SpinWait
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x0006FEB4 File Offset: 0x0006E0B4
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x0006FEBC File Offset: 0x0006E0BC
		public int Count
		{
			get
			{
				return this._count;
			}
			internal set
			{
				this._count = value;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x0006FEC5 File Offset: 0x0006E0C5
		public bool NextSpinWillYield
		{
			get
			{
				return this._count >= 10 || PlatformHelper.IsSingleProcessor;
			}
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0006FED8 File Offset: 0x0006E0D8
		public void SpinOnce()
		{
			this.SpinOnceCore(20);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x0006FEE2 File Offset: 0x0006E0E2
		public void SpinOnce(int sleep1Threshold)
		{
			if (sleep1Threshold < -1)
			{
				throw new ArgumentOutOfRangeException("sleep1Threshold", sleep1Threshold, "Number must be either non-negative and less than or equal to Int32.MaxValue or -1.");
			}
			if (sleep1Threshold >= 0 && sleep1Threshold < 10)
			{
				sleep1Threshold = 10;
			}
			this.SpinOnceCore(sleep1Threshold);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0006FF14 File Offset: 0x0006E114
		private void SpinOnceCore(int sleep1Threshold)
		{
			if ((this._count >= 10 && ((this._count >= sleep1Threshold && sleep1Threshold >= 0) || (this._count - 10) % 2 == 0)) || PlatformHelper.IsSingleProcessor)
			{
				if (this._count >= sleep1Threshold && sleep1Threshold >= 0)
				{
					RuntimeThread.Sleep(1);
				}
				else if (((this._count >= 10) ? ((this._count - 10) / 2) : this._count) % 5 == 4)
				{
					RuntimeThread.Sleep(0);
				}
				else
				{
					RuntimeThread.Yield();
				}
			}
			else
			{
				int num = RuntimeThread.OptimalMaxSpinWaitsPerSpinIteration;
				if (this._count <= 30 && 1 << this._count < num)
				{
					num = 1 << this._count;
				}
				RuntimeThread.SpinWait(num);
			}
			this._count = ((this._count == int.MaxValue) ? 10 : (this._count + 1));
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x0006FFE3 File Offset: 0x0006E1E3
		public void Reset()
		{
			this._count = 0;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x0006FFEC File Offset: 0x0006E1EC
		public static void SpinUntil(Func<bool> condition)
		{
			SpinWait.SpinUntil(condition, -1);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0006FFF8 File Offset: 0x0006E1F8
		public static bool SpinUntil(Func<bool> condition, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, "The timeout must represent a value between -1 and Int32.MaxValue, inclusive.");
			}
			return SpinWait.SpinUntil(condition, (int)num);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x0007003C File Offset: 0x0006E23C
		public static bool SpinUntil(Func<bool> condition, int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, "The timeout must represent a value between -1 and Int32.MaxValue, inclusive.");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition", "The condition argument is null.");
			}
			uint num = 0U;
			if (millisecondsTimeout != 0 && millisecondsTimeout != -1)
			{
				num = TimeoutHelper.GetTime();
			}
			SpinWait spinWait = default(SpinWait);
			while (!condition())
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				spinWait.SpinOnce();
				if (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && (long)millisecondsTimeout <= (long)((ulong)(TimeoutHelper.GetTime() - num)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x000700BF File Offset: 0x0006E2BF
		// Note: this type is marked as 'beforefieldinit'.
		static SpinWait()
		{
		}

		// Token: 0x04001951 RID: 6481
		internal const int YieldThreshold = 10;

		// Token: 0x04001952 RID: 6482
		private const int Sleep0EveryHowManyYields = 5;

		// Token: 0x04001953 RID: 6483
		internal const int DefaultSleep1Threshold = 20;

		// Token: 0x04001954 RID: 6484
		internal static readonly int SpinCountforSpinBeforeWait = (PlatformHelper.IsSingleProcessor ? 1 : 35);

		// Token: 0x04001955 RID: 6485
		internal const int Sleep1ThresholdForLongSpinBeforeWait = 40;

		// Token: 0x04001956 RID: 6486
		private int _count;
	}
}

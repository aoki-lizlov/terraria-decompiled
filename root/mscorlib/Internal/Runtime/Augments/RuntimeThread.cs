using System;
using System.Threading;

namespace Internal.Runtime.Augments
{
	// Token: 0x02000B62 RID: 2914
	internal sealed class RuntimeThread
	{
		// Token: 0x06006AB0 RID: 27312 RVA: 0x0016ECE3 File Offset: 0x0016CEE3
		private RuntimeThread(Thread t)
		{
			this.thread = t;
		}

		// Token: 0x06006AB1 RID: 27313 RVA: 0x00004088 File Offset: 0x00002288
		public void ResetThreadPoolThread()
		{
		}

		// Token: 0x06006AB2 RID: 27314 RVA: 0x0016ECF2 File Offset: 0x0016CEF2
		public static RuntimeThread InitializeThreadPoolThread()
		{
			return new RuntimeThread(null);
		}

		// Token: 0x06006AB3 RID: 27315 RVA: 0x0016ECFA File Offset: 0x0016CEFA
		public static RuntimeThread Create(ParameterizedThreadStart start, int maxStackSize)
		{
			return new RuntimeThread(new Thread(start, maxStackSize));
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06006AB4 RID: 27316 RVA: 0x0016ED08 File Offset: 0x0016CF08
		// (set) Token: 0x06006AB5 RID: 27317 RVA: 0x0016ED15 File Offset: 0x0016CF15
		public bool IsBackground
		{
			get
			{
				return this.thread.IsBackground;
			}
			set
			{
				this.thread.IsBackground = value;
			}
		}

		// Token: 0x06006AB6 RID: 27318 RVA: 0x0016ED23 File Offset: 0x0016CF23
		public void Start()
		{
			this.thread.Start();
		}

		// Token: 0x06006AB7 RID: 27319 RVA: 0x0016ED30 File Offset: 0x0016CF30
		public void Start(object state)
		{
			this.thread.Start(state);
		}

		// Token: 0x06006AB8 RID: 27320 RVA: 0x0016ED3E File Offset: 0x0016CF3E
		public static void Sleep(int millisecondsTimeout)
		{
			Thread.Sleep(millisecondsTimeout);
		}

		// Token: 0x06006AB9 RID: 27321 RVA: 0x0016ED46 File Offset: 0x0016CF46
		public static bool Yield()
		{
			return Thread.Yield();
		}

		// Token: 0x06006ABA RID: 27322 RVA: 0x0016ED4D File Offset: 0x0016CF4D
		public static bool SpinWait(int iterations)
		{
			Thread.SpinWait(iterations);
			return true;
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x00003FB7 File Offset: 0x000021B7
		public static int GetCurrentProcessorId()
		{
			return 1;
		}

		// Token: 0x06006ABC RID: 27324 RVA: 0x0016ED56 File Offset: 0x0016CF56
		// Note: this type is marked as 'beforefieldinit'.
		static RuntimeThread()
		{
		}

		// Token: 0x04003D71 RID: 15729
		internal static readonly int OptimalMaxSpinWaitsPerSpinIteration = 64;

		// Token: 0x04003D72 RID: 15730
		private readonly Thread thread;
	}
}

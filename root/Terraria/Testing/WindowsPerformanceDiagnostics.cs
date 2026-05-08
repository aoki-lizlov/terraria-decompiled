using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using ReLogic.OS;

namespace Terraria.Testing
{
	// Token: 0x02000113 RID: 275
	public class WindowsPerformanceDiagnostics
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x004F8A66 File Offset: 0x004F6C66
		public static bool Supported
		{
			get
			{
				return Platform.IsWindows;
			}
		}

		// Token: 0x06001ADA RID: 6874
		[DllImport("Kernel32.dll")]
		private static extern int GetCurrentProcessorNumber();

		// Token: 0x06001ADB RID: 6875
		[DllImport("Pdh.dll", SetLastError = true)]
		private static extern int PdhOpenQuery(IntPtr dataSource, IntPtr userData, out IntPtr query);

		// Token: 0x06001ADC RID: 6876
		[DllImport("Pdh.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int PdhAddCounter(IntPtr query, string counterPath, IntPtr userData, out IntPtr counter);

		// Token: 0x06001ADD RID: 6877
		[DllImport("Pdh.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int PdhRemoveCounter(IntPtr counter);

		// Token: 0x06001ADE RID: 6878
		[DllImport("Pdh.dll", SetLastError = true)]
		private static extern int PdhCollectQueryData(IntPtr query);

		// Token: 0x06001ADF RID: 6879
		[DllImport("Pdh.dll", SetLastError = true)]
		private static extern int PdhGetFormattedCounterValue(IntPtr counter, uint format, out uint type, out WindowsPerformanceDiagnostics.PDH_FMT_COUNTERVALUE value);

		// Token: 0x06001AE0 RID: 6880
		[DllImport("Pdh.dll", SetLastError = true)]
		private static extern int PdhCloseQuery(IntPtr query);

		// Token: 0x06001AE1 RID: 6881
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetCurrentThread();

		// Token: 0x06001AE2 RID: 6882
		[DllImport("kernel32.dll")]
		private static extern bool SetThreadAffinityMask(IntPtr hThread, IntPtr dwThreadAffinityMask);

		// Token: 0x06001AE3 RID: 6883
		[DllImport("kernel32.dll")]
		private static extern uint SetThreadIdealProcessor(IntPtr hThread, uint dwIdealProcessor);

		// Token: 0x06001AE4 RID: 6884 RVA: 0x004F8A70 File Offset: 0x004F6C70
		public static WindowsPerformanceDiagnostics.Data GetData()
		{
			object @lock = WindowsPerformanceDiagnostics._lock;
			WindowsPerformanceDiagnostics.Data data;
			lock (@lock)
			{
				if (WindowsPerformanceDiagnostics._monitorThread == null)
				{
					WindowsPerformanceDiagnostics._data.PinnedToProcessor = true;
					WindowsPerformanceDiagnostics._data.CurrentProcessor = WindowsPerformanceDiagnostics.GetCurrentProcessorNumber();
					WindowsPerformanceDiagnostics._monitorThread = new Thread(new ThreadStart(WindowsPerformanceDiagnostics.MonitorPerformanceCounters))
					{
						IsBackground = true,
						Name = "Perf Counter Monitoring"
					};
					WindowsPerformanceDiagnostics._monitorThread.Start();
				}
				else
				{
					int currentProcessorNumber = WindowsPerformanceDiagnostics.GetCurrentProcessorNumber();
					if (WindowsPerformanceDiagnostics._data.CurrentProcessor != currentProcessorNumber)
					{
						WindowsPerformanceDiagnostics._data.PinnedToProcessor = false;
					}
					WindowsPerformanceDiagnostics._data.CurrentProcessor = currentProcessorNumber;
				}
				WindowsPerformanceDiagnostics._data.ExpectedCPUPercent = (double)(DetailedFPS.GetCPUUtilization(60) * 100f);
				data = WindowsPerformanceDiagnostics._data;
			}
			return data;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x004F8B48 File Offset: 0x004F6D48
		private static bool ShouldRecommendUnpinning()
		{
			if (Environment.ProcessorCount < 4)
			{
				return false;
			}
			bool flag;
			if (WindowsPerformanceDiagnostics._data.PinnedToProcessor && WindowsPerformanceDiagnostics._data.CurrentProcessor == 0)
			{
				long? contentionQueueLength = WindowsPerformanceDiagnostics._data.ContentionQueueLength;
				long num = 0L;
				if ((contentionQueueLength.GetValueOrDefault() > num) & (contentionQueueLength != null))
				{
					double? mainThreadCPUPercent = WindowsPerformanceDiagnostics._data.MainThreadCPUPercent;
					double num2 = WindowsPerformanceDiagnostics._data.ExpectedCPUPercent * (double)WindowsPerformanceDiagnostics.ContentionPerfDropThreshold;
					flag = (mainThreadCPUPercent.GetValueOrDefault() < num2) & (mainThreadCPUPercent != null);
					goto IL_0076;
				}
			}
			flag = false;
			IL_0076:
			if (flag)
			{
				WindowsPerformanceDiagnostics._unpinHintCount++;
			}
			else
			{
				WindowsPerformanceDiagnostics._unpinHintCount = 0;
			}
			return WindowsPerformanceDiagnostics._unpinHintCount >= WindowsPerformanceDiagnostics.ConsecutiveContentionChecksBeforeUnpin;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x004F8BF0 File Offset: 0x004F6DF0
		public static void UnpinFromCore0()
		{
			int allProcMask = (1 << Environment.ProcessorCount) - 1;
			Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)(allProcMask ^ 1);
			Task.Factory.StartNew(delegate
			{
				Thread.Sleep(100);
				Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)allProcMask;
			});
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x004F8C43 File Offset: 0x004F6E43
		private static string GetMainThreadCounterIdentifier()
		{
			return string.Format("\\Thread({0}/0{1})", WindowsPerformanceDiagnostics.ProcessName, (WindowsPerformanceDiagnostics.ProcessCopyNumber == 0) ? "" : ("#" + WindowsPerformanceDiagnostics.ProcessCopyNumber));
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x004F8C76 File Offset: 0x004F6E76
		private static bool AddCounter(string name, ref IntPtr handle)
		{
			if (handle != IntPtr.Zero)
			{
				WindowsPerformanceDiagnostics.PdhRemoveCounter(handle);
			}
			return WindowsPerformanceDiagnostics.PdhAddCounter(WindowsPerformanceDiagnostics.queryHandle, name, IntPtr.Zero, out handle) != 0;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x004F8CA4 File Offset: 0x004F6EA4
		private static bool ReadCounter(IntPtr handle, out double value)
		{
			uint num;
			WindowsPerformanceDiagnostics.PDH_FMT_COUNTERVALUE pdh_FMT_COUNTERVALUE;
			if (WindowsPerformanceDiagnostics.PdhGetFormattedCounterValue(handle, WindowsPerformanceDiagnostics.PDH_FMT_DOUBLE, out num, out pdh_FMT_COUNTERVALUE) == 0)
			{
				value = pdh_FMT_COUNTERVALUE.doubleValue;
				return true;
			}
			value = 0.0;
			return false;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x004F8CD8 File Offset: 0x004F6ED8
		private static void ReadCounter(IntPtr handle, out double? value)
		{
			double num;
			if (handle != IntPtr.Zero && WindowsPerformanceDiagnostics.ReadCounter(handle, out num))
			{
				value = new double?(num);
				return;
			}
			value = null;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x004F8D10 File Offset: 0x004F6F10
		private static void MonitorPerformanceCounters()
		{
			if (WindowsPerformanceDiagnostics.PdhOpenQuery(IntPtr.Zero, IntPtr.Zero, out WindowsPerformanceDiagnostics.queryHandle) != 0)
			{
				return;
			}
			WindowsPerformanceDiagnostics.AddCounter("\\System\\Processor Queue Length", ref WindowsPerformanceDiagnostics.processorQueueLengthHandle);
			WindowsPerformanceDiagnostics.RecreateCoreCounters();
			WindowsPerformanceDiagnostics.RecreateThreadCounters();
			for (;;)
			{
				int num = (WindowsPerformanceDiagnostics._data.PinnedToProcessor ? WindowsPerformanceDiagnostics._data.CurrentProcessor : (-1));
				if (num != WindowsPerformanceDiagnostics.MonitoringCoreNumber)
				{
					WindowsPerformanceDiagnostics.MonitoringCoreNumber = num;
					WindowsPerformanceDiagnostics.RecreateCoreCounters();
				}
				Thread.Sleep(250);
				WindowsPerformanceDiagnostics.PdhCollectQueryData(WindowsPerformanceDiagnostics.queryHandle);
				double? num2;
				WindowsPerformanceDiagnostics.ReadCounter(WindowsPerformanceDiagnostics.processorPerformanceHandle, out num2);
				double? num3;
				WindowsPerformanceDiagnostics.ReadCounter(WindowsPerformanceDiagnostics.processorQueueLengthHandle, out num3);
				double num4;
				if (!WindowsPerformanceDiagnostics.ReadCounter(WindowsPerformanceDiagnostics.threadProcessIdHandle, out num4))
				{
					WindowsPerformanceDiagnostics.ProcessCopyNumber = 0;
					WindowsPerformanceDiagnostics.RecreateThreadCounters();
				}
				else if (num4 != (double)WindowsPerformanceDiagnostics.PID)
				{
					WindowsPerformanceDiagnostics.ProcessCopyNumber++;
					WindowsPerformanceDiagnostics.RecreateThreadCounters();
				}
				else
				{
					double? num5;
					WindowsPerformanceDiagnostics.ReadCounter(WindowsPerformanceDiagnostics.threadProcessorTimeHandle, out num5);
					object @lock = WindowsPerformanceDiagnostics._lock;
					lock (@lock)
					{
						WindowsPerformanceDiagnostics._data.ProcessorThrottlePercent = num2;
						WindowsPerformanceDiagnostics._data.ContentionQueueLength = ((num3 != null) ? new long?((long)num3.Value) : null);
						WindowsPerformanceDiagnostics.LowPassUpdate(ref WindowsPerformanceDiagnostics._data.MainThreadCPUPercent, num5, 0.25f);
						WindowsPerformanceDiagnostics._data.RecommendUnpinning = WindowsPerformanceDiagnostics.ShouldRecommendUnpinning();
					}
				}
			}
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x004F8E80 File Offset: 0x004F7080
		private static void LowPassUpdate(ref double? filtered, double? newValue, float rate)
		{
			if (filtered == null || newValue == null)
			{
				filtered = newValue;
				return;
			}
			filtered = filtered * (double)(1f - rate) + newValue * (double)rate;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x004F8F3C File Offset: 0x004F713C
		private static void RecreateCoreCounters()
		{
			string text = ((WindowsPerformanceDiagnostics.MonitoringCoreNumber < 0) ? "_Total" : WindowsPerformanceDiagnostics.MonitoringCoreNumber.ToString());
			WindowsPerformanceDiagnostics.AddCounter("\\Processor Information(0," + text + ")\\% Processor Performance", ref WindowsPerformanceDiagnostics.processorPerformanceHandle);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x004F8F7E File Offset: 0x004F717E
		private static void RecreateThreadCounters()
		{
			WindowsPerformanceDiagnostics.AddCounter(WindowsPerformanceDiagnostics.GetMainThreadCounterIdentifier() + "\\% Processor Time", ref WindowsPerformanceDiagnostics.threadProcessorTimeHandle);
			WindowsPerformanceDiagnostics.AddCounter(WindowsPerformanceDiagnostics.GetMainThreadCounterIdentifier() + "\\ID Process", ref WindowsPerformanceDiagnostics.threadProcessIdHandle);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0000357B File Offset: 0x0000177B
		public WindowsPerformanceDiagnostics()
		{
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x004F8FB4 File Offset: 0x004F71B4
		// Note: this type is marked as 'beforefieldinit'.
		static WindowsPerformanceDiagnostics()
		{
		}

		// Token: 0x0400152B RID: 5419
		private static readonly uint PDH_FMT_DOUBLE = 512U;

		// Token: 0x0400152C RID: 5420
		private static Thread _monitorThread;

		// Token: 0x0400152D RID: 5421
		private static object _lock = new object();

		// Token: 0x0400152E RID: 5422
		private static WindowsPerformanceDiagnostics.Data _data;

		// Token: 0x0400152F RID: 5423
		private static readonly float ContentionPerfDropThreshold = 0.8f;

		// Token: 0x04001530 RID: 5424
		private static readonly int ConsecutiveContentionChecksBeforeUnpin = 20;

		// Token: 0x04001531 RID: 5425
		private static int _unpinHintCount = 0;

		// Token: 0x04001532 RID: 5426
		private static IntPtr queryHandle = IntPtr.Zero;

		// Token: 0x04001533 RID: 5427
		private static IntPtr processorPerformanceHandle = IntPtr.Zero;

		// Token: 0x04001534 RID: 5428
		private static IntPtr processorQueueLengthHandle = IntPtr.Zero;

		// Token: 0x04001535 RID: 5429
		private static IntPtr threadProcessorTimeHandle = IntPtr.Zero;

		// Token: 0x04001536 RID: 5430
		private static IntPtr threadProcessIdHandle = IntPtr.Zero;

		// Token: 0x04001537 RID: 5431
		private static readonly string ProcessName = Process.GetCurrentProcess().ProcessName;

		// Token: 0x04001538 RID: 5432
		private static readonly int PID = Process.GetCurrentProcess().Id;

		// Token: 0x04001539 RID: 5433
		private static int ProcessCopyNumber = 0;

		// Token: 0x0400153A RID: 5434
		private static int MonitoringCoreNumber = 0;

		// Token: 0x02000723 RID: 1827
		public struct Data
		{
			// Token: 0x0400695F RID: 26975
			public double? ProcessorThrottlePercent;

			// Token: 0x04006960 RID: 26976
			public double? MainThreadCPUPercent;

			// Token: 0x04006961 RID: 26977
			public double ExpectedCPUPercent;

			// Token: 0x04006962 RID: 26978
			public long? ContentionQueueLength;

			// Token: 0x04006963 RID: 26979
			public int CurrentProcessor;

			// Token: 0x04006964 RID: 26980
			public bool PinnedToProcessor;

			// Token: 0x04006965 RID: 26981
			public bool RecommendUnpinning;
		}

		// Token: 0x02000724 RID: 1828
		private struct PDH_FMT_COUNTERVALUE
		{
			// Token: 0x04006966 RID: 26982
			public int CStatus;

			// Token: 0x04006967 RID: 26983
			public double doubleValue;
		}

		// Token: 0x02000725 RID: 1829
		[CompilerGenerated]
		private sealed class <>c__DisplayClass23_0
		{
			// Token: 0x0600406A RID: 16490 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass23_0()
			{
			}

			// Token: 0x0600406B RID: 16491 RVA: 0x0069E148 File Offset: 0x0069C348
			internal void <UnpinFromCore0>b__0()
			{
				Thread.Sleep(100);
				Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)this.allProcMask;
			}

			// Token: 0x04006968 RID: 26984
			public int allProcMask;
		}
	}
}

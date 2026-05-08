using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System
{
	// Token: 0x020001D5 RID: 469
	public static class GC
	{
		// Token: 0x060015D1 RID: 5585
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCollectionCount(int generation);

		// Token: 0x060015D2 RID: 5586
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxGeneration();

		// Token: 0x060015D3 RID: 5587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalCollect(int generation);

		// Token: 0x060015D4 RID: 5588
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecordPressure(long bytesAllocated);

		// Token: 0x060015D5 RID: 5589
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void register_ephemeron_array(Ephemeron[] array);

		// Token: 0x060015D6 RID: 5590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object get_ephemeron_tombstone();

		// Token: 0x060015D7 RID: 5591 RVA: 0x000575D9 File Offset: 0x000557D9
		internal static void GetMemoryInfo(out uint highMemLoadThreshold, out ulong totalPhysicalMem, out uint lastRecordedMemLoad, out UIntPtr lastRecordedHeapSize, out UIntPtr lastRecordedFragmentation)
		{
			highMemLoadThreshold = 0U;
			totalPhysicalMem = ulong.MaxValue;
			lastRecordedMemLoad = 0U;
			lastRecordedHeapSize = UIntPtr.Zero;
			lastRecordedFragmentation = UIntPtr.Zero;
		}

		// Token: 0x060015D8 RID: 5592
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetAllocatedBytesForCurrentThread();

		// Token: 0x060015D9 RID: 5593 RVA: 0x000575F4 File Offset: 0x000557F4
		[SecurityCritical]
		public static void AddMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Positive number required."));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("pressure", Environment.GetResourceString("Value must be non-negative and less than or equal to Int32.MaxValue."));
			}
			GC.RecordPressure(bytesAllocated);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00057648 File Offset: 0x00055848
		[SecurityCritical]
		public static void RemoveMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Positive number required."));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Value must be non-negative and less than or equal to Int32.MaxValue."));
			}
			GC.RecordPressure(-bytesAllocated);
		}

		// Token: 0x060015DB RID: 5595
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetGeneration(object obj);

		// Token: 0x060015DC RID: 5596 RVA: 0x0005769C File Offset: 0x0005589C
		public static void Collect(int generation)
		{
			GC.Collect(generation, GCCollectionMode.Default);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000576A5 File Offset: 0x000558A5
		[SecuritySafeCritical]
		public static void Collect()
		{
			GC.InternalCollect(GC.MaxGeneration);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000576B1 File Offset: 0x000558B1
		[SecuritySafeCritical]
		public static void Collect(int generation, GCCollectionMode mode)
		{
			GC.Collect(generation, mode, true);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000576BB File Offset: 0x000558BB
		[SecuritySafeCritical]
		public static void Collect(int generation, GCCollectionMode mode, bool blocking)
		{
			GC.Collect(generation, mode, blocking, false);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x000576C8 File Offset: 0x000558C8
		[SecuritySafeCritical]
		public static void Collect(int generation, GCCollectionMode mode, bool blocking, bool compacting)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("Value must be positive."));
			}
			if (mode < GCCollectionMode.Default || mode > GCCollectionMode.Optimized)
			{
				throw new ArgumentOutOfRangeException("mode", Environment.GetResourceString("Enum value was out of legal range."));
			}
			int num = 0;
			if (mode == GCCollectionMode.Optimized)
			{
				num |= 4;
			}
			if (compacting)
			{
				num |= 8;
			}
			if (blocking)
			{
				num |= 2;
			}
			else if (!compacting)
			{
				num |= 1;
			}
			GC.InternalCollect(generation);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00057732 File Offset: 0x00055932
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int CollectionCount(int generation)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("Value must be positive."));
			}
			return GC.GetCollectionCount(generation);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00004088 File Offset: 0x00002288
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void KeepAlive(object obj)
		{
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00057753 File Offset: 0x00055953
		[SecuritySafeCritical]
		public static int GetGeneration(WeakReference wo)
		{
			object target = wo.Target;
			if (target == null)
			{
				throw new ArgumentException();
			}
			return GC.GetGeneration(target);
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00057769 File Offset: 0x00055969
		public static int MaxGeneration
		{
			[SecuritySafeCritical]
			get
			{
				return GC.GetMaxGeneration();
			}
		}

		// Token: 0x060015E5 RID: 5605
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WaitForPendingFinalizers();

		// Token: 0x060015E6 RID: 5606
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _SuppressFinalize(object o);

		// Token: 0x060015E7 RID: 5607 RVA: 0x00057770 File Offset: 0x00055970
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void SuppressFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._SuppressFinalize(obj);
		}

		// Token: 0x060015E8 RID: 5608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _ReRegisterForFinalize(object o);

		// Token: 0x060015E9 RID: 5609 RVA: 0x00057786 File Offset: 0x00055986
		[SecuritySafeCritical]
		public static void ReRegisterForFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._ReRegisterForFinalize(obj);
		}

		// Token: 0x060015EA RID: 5610
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTotalMemory(bool forceFullCollection);

		// Token: 0x060015EB RID: 5611 RVA: 0x000174FB File Offset: 0x000156FB
		private static bool _RegisterForFullGCNotification(int maxGenerationPercentage, int largeObjectHeapPercentage)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000174FB File Offset: 0x000156FB
		private static bool _CancelFullGCNotification()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000174FB File Offset: 0x000156FB
		private static int _WaitForFullGCApproach(int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000174FB File Offset: 0x000156FB
		private static int _WaitForFullGCComplete(int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0005779C File Offset: 0x0005599C
		[SecurityCritical]
		public static void RegisterForFullGCNotification(int maxGenerationThreshold, int largeObjectHeapThreshold)
		{
			if (maxGenerationThreshold <= 0 || maxGenerationThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("maxGenerationThreshold", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument must be between {0} and {1}."), 1, 99));
			}
			if (largeObjectHeapThreshold <= 0 || largeObjectHeapThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("largeObjectHeapThreshold", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument must be between {0} and {1}."), 1, 99));
			}
			if (!GC._RegisterForFullGCNotification(maxGenerationThreshold, largeObjectHeapThreshold))
			{
				throw new InvalidOperationException(Environment.GetResourceString("This API is not available when the concurrent GC is enabled."));
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0005782C File Offset: 0x00055A2C
		[SecurityCritical]
		public static void CancelFullGCNotification()
		{
			if (!GC._CancelFullGCNotification())
			{
				throw new InvalidOperationException(Environment.GetResourceString("This API is not available when the concurrent GC is enabled."));
			}
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00057845 File Offset: 0x00055A45
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCApproach()
		{
			return (GCNotificationStatus)GC._WaitForFullGCApproach(-1);
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x0005784D File Offset: 0x00055A4D
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return (GCNotificationStatus)GC._WaitForFullGCApproach(millisecondsTimeout);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0005786E File Offset: 0x00055A6E
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCComplete()
		{
			return (GCNotificationStatus)GC._WaitForFullGCComplete(-1);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00057876 File Offset: 0x00055A76
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return (GCNotificationStatus)GC._WaitForFullGCComplete(millisecondsTimeout);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x000174FB File Offset: 0x000156FB
		[SecurityCritical]
		private static bool StartNoGCRegionWorker(long totalSize, bool hasLohSize, long lohSize, bool disallowFullBlockingGC)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00057897 File Offset: 0x00055A97
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, false);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x000578A3 File Offset: 0x00055AA3
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, long lohSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, false);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000578AE File Offset: 0x00055AAE
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, disallowFullBlockingGC);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000578BA File Offset: 0x00055ABA
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, long lohSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, disallowFullBlockingGC);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000174FB File Offset: 0x000156FB
		[SecurityCritical]
		private static GC.EndNoGCRegionStatus EndNoGCRegionWorker()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000578C5 File Offset: 0x00055AC5
		[SecurityCritical]
		public static void EndNoGCRegion()
		{
			GC.EndNoGCRegionWorker();
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000578CD File Offset: 0x00055ACD
		// Note: this type is marked as 'beforefieldinit'.
		static GC()
		{
		}

		// Token: 0x0400142D RID: 5165
		internal static readonly object EPHEMERON_TOMBSTONE = GC.get_ephemeron_tombstone();

		// Token: 0x020001D6 RID: 470
		private enum StartNoGCRegionStatus
		{
			// Token: 0x0400142F RID: 5167
			Succeeded,
			// Token: 0x04001430 RID: 5168
			NotEnoughMemory,
			// Token: 0x04001431 RID: 5169
			AmountTooLarge,
			// Token: 0x04001432 RID: 5170
			AlreadyInProgress
		}

		// Token: 0x020001D7 RID: 471
		private enum EndNoGCRegionStatus
		{
			// Token: 0x04001434 RID: 5172
			Succeeded,
			// Token: 0x04001435 RID: 5173
			NotInProgress,
			// Token: 0x04001436 RID: 5174
			GCInduced,
			// Token: 0x04001437 RID: 5175
			AllocationExceeded
		}
	}
}

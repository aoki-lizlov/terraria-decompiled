using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Util;
using System.Threading;

namespace System
{
	// Token: 0x020001E2 RID: 482
	internal sealed class SharedStatics
	{
		// Token: 0x0600170F RID: 5903 RVA: 0x000025BE File Offset: 0x000007BE
		private SharedStatics()
		{
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x0005B01C File Offset: 0x0005921C
		public static string Remoting_Identity_IDGuid
		{
			[SecuritySafeCritical]
			get
			{
				if (SharedStatics._sharedStatics._Remoting_Identity_IDGuid == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(SharedStatics._sharedStatics, ref flag);
						if (SharedStatics._sharedStatics._Remoting_Identity_IDGuid == null)
						{
							SharedStatics._sharedStatics._Remoting_Identity_IDGuid = Guid.NewGuid().ToString().Replace('-', '_');
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(SharedStatics._sharedStatics);
						}
					}
				}
				return SharedStatics._sharedStatics._Remoting_Identity_IDGuid;
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0005B0AC File Offset: 0x000592AC
		[SecuritySafeCritical]
		public static Tokenizer.StringMaker GetSharedStringMaker()
		{
			Tokenizer.StringMaker stringMaker = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(SharedStatics._sharedStatics, ref flag);
				if (SharedStatics._sharedStatics._maker != null)
				{
					stringMaker = SharedStatics._sharedStatics._maker;
					SharedStatics._sharedStatics._maker = null;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(SharedStatics._sharedStatics);
				}
			}
			if (stringMaker == null)
			{
				stringMaker = new Tokenizer.StringMaker();
			}
			return stringMaker;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0005B11C File Offset: 0x0005931C
		[SecuritySafeCritical]
		public static void ReleaseSharedStringMaker(ref Tokenizer.StringMaker maker)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(SharedStatics._sharedStatics, ref flag);
				SharedStatics._sharedStatics._maker = maker;
				maker = null;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(SharedStatics._sharedStatics);
				}
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0005B16C File Offset: 0x0005936C
		internal static int Remoting_Identity_GetNextSeqNum()
		{
			return Interlocked.Increment(ref SharedStatics._sharedStatics._Remoting_Identity_IDSeqNum);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0005B17D File Offset: 0x0005937D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static long AddMemoryFailPointReservation(long size)
		{
			return Interlocked.Add(ref SharedStatics._sharedStatics._memFailPointReservedMemory, size);
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0005B18F File Offset: 0x0005938F
		internal static ulong MemoryFailPointReservedMemory
		{
			get
			{
				return (ulong)Volatile.Read(ref SharedStatics._sharedStatics._memFailPointReservedMemory);
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0005B1A0 File Offset: 0x000593A0
		// Note: this type is marked as 'beforefieldinit'.
		static SharedStatics()
		{
		}

		// Token: 0x040014A7 RID: 5287
		private static readonly SharedStatics _sharedStatics = new SharedStatics();

		// Token: 0x040014A8 RID: 5288
		private volatile string _Remoting_Identity_IDGuid;

		// Token: 0x040014A9 RID: 5289
		private Tokenizer.StringMaker _maker;

		// Token: 0x040014AA RID: 5290
		private int _Remoting_Identity_IDSeqNum;

		// Token: 0x040014AB RID: 5291
		private long _memFailPointReservedMemory;
	}
}

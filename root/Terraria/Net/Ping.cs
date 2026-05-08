using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Terraria.Net.Sockets;
using Terraria.Testing;

namespace Terraria.Net
{
	// Token: 0x02000165 RID: 357
	public class Ping
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x00501D34 File Offset: 0x004FFF34
		// (set) Token: 0x06001DAE RID: 7598 RVA: 0x00501D3B File Offset: 0x004FFF3B
		public static int CurrentPing
		{
			[CompilerGenerated]
			get
			{
				return Ping.<CurrentPing>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Ping.<CurrentPing>k__BackingField = value;
			}
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00501D43 File Offset: 0x004FFF43
		public static void Reset()
		{
			Ping.CurrentPing = 0;
			Ping._stopwatch.Restart();
			Ping._waitingForResponse = false;
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x00501D5C File Offset: 0x004FFF5C
		public static void Update()
		{
			if (Ping._waitingForResponse)
			{
				Ping.CurrentPing = Math.Max(Ping.CurrentPing, (int)Ping._stopwatch.ElapsedMilliseconds);
				return;
			}
			if (Ping._stopwatch.ElapsedMilliseconds >= 250L)
			{
				NetMessage.SendData(154, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				Ping._waitingForResponse = true;
				Ping._stopwatch.Restart();
			}
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00501DD0 File Offset: 0x004FFFD0
		internal static void PingRecieved()
		{
			Ping.CurrentPing = (int)Ping._stopwatch.ElapsedMilliseconds;
			Ping._waitingForResponse = false;
			if (DebugOptions.Shared_ServerPing > 0)
			{
				int num = (DebugOptions.Shared_ServerPing - Ping.CurrentPing) / 2;
				num /= 5;
				DebugNetworkStream.Latency = (uint)Utils.Clamp<long>((long)((ulong)DebugNetworkStream.Latency + (ulong)((long)num)), 0L, 5000L);
			}
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0000357B File Offset: 0x0000177B
		public Ping()
		{
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00501E29 File Offset: 0x00500029
		// Note: this type is marked as 'beforefieldinit'.
		static Ping()
		{
		}

		// Token: 0x0400165A RID: 5722
		[CompilerGenerated]
		private static int <CurrentPing>k__BackingField;

		// Token: 0x0400165B RID: 5723
		private static Stopwatch _stopwatch = new Stopwatch();

		// Token: 0x0400165C RID: 5724
		private static bool _waitingForResponse;
	}
}

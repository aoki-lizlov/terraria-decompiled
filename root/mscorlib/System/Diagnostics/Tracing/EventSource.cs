using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A4F RID: 2639
	public class EventSource : IDisposable
	{
		// Token: 0x060060DC RID: 24796 RVA: 0x0014D55D File Offset: 0x0014B75D
		protected EventSource()
		{
			this.Name = base.GetType().Name;
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x0007B6EC File Offset: 0x000798EC
		protected EventSource(bool throwOnEventWriteErrors)
			: this()
		{
		}

		// Token: 0x060060DE RID: 24798 RVA: 0x0014D576 File Offset: 0x0014B776
		protected EventSource(EventSourceSettings settings)
			: this()
		{
			this.Settings = settings;
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x0014D585 File Offset: 0x0014B785
		protected EventSource(EventSourceSettings settings, params string[] traits)
			: this(settings)
		{
		}

		// Token: 0x060060E0 RID: 24800 RVA: 0x0014D58E File Offset: 0x0014B78E
		public EventSource(string eventSourceName)
		{
			this.Name = eventSourceName;
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x0014D59D File Offset: 0x0014B79D
		public EventSource(string eventSourceName, EventSourceSettings config)
			: this(eventSourceName)
		{
			this.Settings = config;
		}

		// Token: 0x060060E2 RID: 24802 RVA: 0x0014D5AD File Offset: 0x0014B7AD
		public EventSource(string eventSourceName, EventSourceSettings config, params string[] traits)
			: this(eventSourceName, config)
		{
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x0014D5B7 File Offset: 0x0014B7B7
		internal EventSource(Guid eventSourceGuid, string eventSourceName)
			: this(eventSourceName)
		{
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x0014D5C0 File Offset: 0x0014B7C0
		~EventSource()
		{
			this.Dispose(false);
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060060E5 RID: 24805 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public Exception ConstructionException
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060060E6 RID: 24806 RVA: 0x0014D5F0 File Offset: 0x0014B7F0
		public static Guid CurrentThreadActivityId
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060060E7 RID: 24807 RVA: 0x0014D5F0 File Offset: 0x0014B7F0
		public Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060060E8 RID: 24808 RVA: 0x0014D5F7 File Offset: 0x0014B7F7
		// (set) Token: 0x060060E9 RID: 24809 RVA: 0x0014D5FF File Offset: 0x0014B7FF
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x060060EA RID: 24810 RVA: 0x0014D608 File Offset: 0x0014B808
		// (set) Token: 0x060060EB RID: 24811 RVA: 0x0014D610 File Offset: 0x0014B810
		public EventSourceSettings Settings
		{
			[CompilerGenerated]
			get
			{
				return this.<Settings>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Settings>k__BackingField = value;
			}
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsEnabled()
		{
			return false;
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsEnabled(EventLevel level, EventKeywords keywords)
		{
			return false;
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsEnabled(EventLevel level, EventKeywords keywords, EventChannel channel)
		{
			return false;
		}

		// Token: 0x060060EF RID: 24815 RVA: 0x0014D619 File Offset: 0x0014B819
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public string GetTrait(string key)
		{
			return null;
		}

		// Token: 0x060060F1 RID: 24817 RVA: 0x00004088 File Offset: 0x00002288
		public void Write(string eventName)
		{
		}

		// Token: 0x060060F2 RID: 24818 RVA: 0x00004088 File Offset: 0x00002288
		public void Write(string eventName, EventSourceOptions options)
		{
		}

		// Token: 0x060060F3 RID: 24819 RVA: 0x00004088 File Offset: 0x00002288
		public void Write<T>(string eventName, T data)
		{
		}

		// Token: 0x060060F4 RID: 24820 RVA: 0x00004088 File Offset: 0x00002288
		public void Write<T>(string eventName, EventSourceOptions options, T data)
		{
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x00004088 File Offset: 0x00002288
		[CLSCompliant(false)]
		public void Write<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
		}

		// Token: 0x060060F6 RID: 24822 RVA: 0x00004088 File Offset: 0x00002288
		public void Write<T>(string eventName, ref EventSourceOptions options, ref Guid activityId, ref Guid relatedActivityId, ref T data)
		{
		}

		// Token: 0x060060F7 RID: 24823 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060060F8 RID: 24824 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnEventCommand(EventCommandEventArgs command)
		{
		}

		// Token: 0x060060F9 RID: 24825 RVA: 0x00004088 File Offset: 0x00002288
		internal void ReportOutOfBandMessage(string msg, bool flush)
		{
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x0014D628 File Offset: 0x0014B828
		protected void WriteEvent(int eventId)
		{
			this.WriteEvent(eventId, new object[0]);
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x0014D637 File Offset: 0x0014B837
		protected void WriteEvent(int eventId, byte[] arg1)
		{
			this.WriteEvent(eventId, new object[] { arg1 });
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x0014D64A File Offset: 0x0014B84A
		protected void WriteEvent(int eventId, int arg1)
		{
			this.WriteEvent(eventId, new object[] { arg1 });
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x0014D637 File Offset: 0x0014B837
		protected void WriteEvent(int eventId, string arg1)
		{
			this.WriteEvent(eventId, new object[] { arg1 });
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x0014D662 File Offset: 0x0014B862
		protected void WriteEvent(int eventId, int arg1, int arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x0014D683 File Offset: 0x0014B883
		protected void WriteEvent(int eventId, int arg1, int arg2, int arg3)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2, arg3 });
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x0014D6AE File Offset: 0x0014B8AE
		protected void WriteEvent(int eventId, int arg1, string arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x0014D6CA File Offset: 0x0014B8CA
		protected void WriteEvent(int eventId, long arg1)
		{
			this.WriteEvent(eventId, new object[] { arg1 });
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x0014D6E2 File Offset: 0x0014B8E2
		protected void WriteEvent(int eventId, long arg1, byte[] arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		// Token: 0x06006103 RID: 24835 RVA: 0x0014D6FE File Offset: 0x0014B8FE
		protected void WriteEvent(int eventId, long arg1, long arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		// Token: 0x06006104 RID: 24836 RVA: 0x0014D71F File Offset: 0x0014B91F
		protected void WriteEvent(int eventId, long arg1, long arg2, long arg3)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2, arg3 });
		}

		// Token: 0x06006105 RID: 24837 RVA: 0x0014D6E2 File Offset: 0x0014B8E2
		protected void WriteEvent(int eventId, long arg1, string arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x00004088 File Offset: 0x00002288
		protected void WriteEvent(int eventId, params object[] args)
		{
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x0014D74A File Offset: 0x0014B94A
		protected void WriteEvent(int eventId, string arg1, int arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x0014D766 File Offset: 0x0014B966
		protected void WriteEvent(int eventId, string arg1, int arg2, int arg3)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2, arg3 });
		}

		// Token: 0x06006109 RID: 24841 RVA: 0x0014D78C File Offset: 0x0014B98C
		protected void WriteEvent(int eventId, string arg1, long arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		// Token: 0x0600610A RID: 24842 RVA: 0x0014D7A8 File Offset: 0x0014B9A8
		protected void WriteEvent(int eventId, string arg1, string arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		// Token: 0x0600610B RID: 24843 RVA: 0x0014D7BF File Offset: 0x0014B9BF
		protected void WriteEvent(int eventId, string arg1, string arg2, string arg3)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2, arg3 });
		}

		// Token: 0x0600610C RID: 24844 RVA: 0x00004088 File Offset: 0x00002288
		[CLSCompliant(false)]
		protected unsafe void WriteEventCore(int eventId, int eventDataCount, EventSource.EventData* data)
		{
		}

		// Token: 0x0600610D RID: 24845 RVA: 0x00004088 File Offset: 0x00002288
		protected void WriteEventWithRelatedActivityId(int eventId, Guid relatedActivityId, params object[] args)
		{
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x00004088 File Offset: 0x00002288
		[CLSCompliant(false)]
		protected unsafe void WriteEventWithRelatedActivityIdCore(int eventId, Guid* relatedActivityId, int eventDataCount, EventSource.EventData* data)
		{
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600610F RID: 24847 RVA: 0x000174FB File Offset: 0x000156FB
		// (remove) Token: 0x06006110 RID: 24848 RVA: 0x000174FB File Offset: 0x000156FB
		public event EventHandler<EventCommandEventArgs> EventCommandExecuted
		{
			add
			{
				throw new NotImplementedException();
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06006111 RID: 24849 RVA: 0x000174FB File Offset: 0x000156FB
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006112 RID: 24850 RVA: 0x000174FB File Offset: 0x000156FB
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest, EventManifestOptions flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006113 RID: 24851 RVA: 0x000174FB File Offset: 0x000156FB
		public static Guid GetGuid(Type eventSourceType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006114 RID: 24852 RVA: 0x000174FB File Offset: 0x000156FB
		public static string GetName(Type eventSourceType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006115 RID: 24853 RVA: 0x000174FB File Offset: 0x000156FB
		public static IEnumerable<EventSource> GetSources()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x000174FB File Offset: 0x000156FB
		public static void SendCommand(EventSource eventSource, EventCommand command, IDictionary<string, string> commandArguments)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x000174FB File Offset: 0x000156FB
		public static void SetCurrentThreadActivityId(Guid activityId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006118 RID: 24856 RVA: 0x000174FB File Offset: 0x000156FB
		public static void SetCurrentThreadActivityId(Guid activityId, out Guid oldActivityThatWillContinue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04003A5C RID: 14940
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04003A5D RID: 14941
		[CompilerGenerated]
		private EventSourceSettings <Settings>k__BackingField;

		// Token: 0x02000A50 RID: 2640
		protected internal struct EventData
		{
			// Token: 0x1700105D RID: 4189
			// (get) Token: 0x06006119 RID: 24857 RVA: 0x0014D7DB File Offset: 0x0014B9DB
			// (set) Token: 0x0600611A RID: 24858 RVA: 0x0014D7E3 File Offset: 0x0014B9E3
			public IntPtr DataPointer
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<DataPointer>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<DataPointer>k__BackingField = value;
				}
			}

			// Token: 0x1700105E RID: 4190
			// (get) Token: 0x0600611B RID: 24859 RVA: 0x0014D7EC File Offset: 0x0014B9EC
			// (set) Token: 0x0600611C RID: 24860 RVA: 0x0014D7F4 File Offset: 0x0014B9F4
			public int Size
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<Size>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Size>k__BackingField = value;
				}
			}

			// Token: 0x1700105F RID: 4191
			// (get) Token: 0x0600611D RID: 24861 RVA: 0x0014D7FD File Offset: 0x0014B9FD
			// (set) Token: 0x0600611E RID: 24862 RVA: 0x0014D805 File Offset: 0x0014BA05
			internal int Reserved
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<Reserved>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Reserved>k__BackingField = value;
				}
			}

			// Token: 0x04003A5E RID: 14942
			[CompilerGenerated]
			private IntPtr <DataPointer>k__BackingField;

			// Token: 0x04003A5F RID: 14943
			[CompilerGenerated]
			private int <Size>k__BackingField;

			// Token: 0x04003A60 RID: 14944
			[CompilerGenerated]
			private int <Reserved>k__BackingField;
		}
	}
}

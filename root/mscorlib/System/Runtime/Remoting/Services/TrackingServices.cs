using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x0200054E RID: 1358
	[ComVisible(true)]
	public class TrackingServices
	{
		// Token: 0x060036A7 RID: 13991 RVA: 0x000025BE File Offset: 0x000007BE
		public TrackingServices()
		{
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000C647C File Offset: 0x000C467C
		public static void RegisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object syncRoot = TrackingServices._handlers.SyncRoot;
			lock (syncRoot)
			{
				if (-1 != TrackingServices._handlers.IndexOf(handler))
				{
					throw new RemotingException("handler already registered");
				}
				TrackingServices._handlers.Add(handler);
			}
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000C64F0 File Offset: 0x000C46F0
		public static void UnregisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object syncRoot = TrackingServices._handlers.SyncRoot;
			lock (syncRoot)
			{
				int num = TrackingServices._handlers.IndexOf(handler);
				if (num == -1)
				{
					throw new RemotingException("handler is not registered");
				}
				TrackingServices._handlers.RemoveAt(num);
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x000C6564 File Offset: 0x000C4764
		public static ITrackingHandler[] RegisteredHandlers
		{
			get
			{
				object syncRoot = TrackingServices._handlers.SyncRoot;
				ITrackingHandler[] array;
				lock (syncRoot)
				{
					if (TrackingServices._handlers.Count == 0)
					{
						array = new ITrackingHandler[0];
					}
					else
					{
						array = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
					}
				}
				return array;
			}
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000C65D4 File Offset: 0x000C47D4
		internal static void NotifyMarshaledObject(object obj, ObjRef or)
		{
			object syncRoot = TrackingServices._handlers.SyncRoot;
			ITrackingHandler[] array;
			lock (syncRoot)
			{
				if (TrackingServices._handlers.Count == 0)
				{
					return;
				}
				array = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i].MarshaledObject(obj, or);
			}
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000C6654 File Offset: 0x000C4854
		internal static void NotifyUnmarshaledObject(object obj, ObjRef or)
		{
			object syncRoot = TrackingServices._handlers.SyncRoot;
			ITrackingHandler[] array;
			lock (syncRoot)
			{
				if (TrackingServices._handlers.Count == 0)
				{
					return;
				}
				array = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i].UnmarshaledObject(obj, or);
			}
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000C66D4 File Offset: 0x000C48D4
		internal static void NotifyDisconnectedObject(object obj)
		{
			object syncRoot = TrackingServices._handlers.SyncRoot;
			ITrackingHandler[] array;
			lock (syncRoot)
			{
				if (TrackingServices._handlers.Count == 0)
				{
					return;
				}
				array = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i].DisconnectedObject(obj);
			}
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000C6754 File Offset: 0x000C4954
		// Note: this type is marked as 'beforefieldinit'.
		static TrackingServices()
		{
		}

		// Token: 0x040024FB RID: 9467
		private static ArrayList _handlers = new ArrayList();
	}
}

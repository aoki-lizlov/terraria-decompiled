using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000241 RID: 577
	[Serializable]
	public sealed class WeakReference<T> : ISerializable where T : class
	{
		// Token: 0x06001BFF RID: 7167 RVA: 0x00069E90 File Offset: 0x00068090
		public WeakReference(T target)
			: this(target, false)
		{
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00069E9C File Offset: 0x0006809C
		public WeakReference(T target, bool trackResurrection)
		{
			this.trackResurrection = trackResurrection;
			GCHandleType gchandleType = (trackResurrection ? GCHandleType.WeakTrackResurrection : GCHandleType.Weak);
			this.handle = GCHandle.Alloc(target, gchandleType);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00069ED0 File Offset: 0x000680D0
		private WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.trackResurrection = info.GetBoolean("TrackResurrection");
			object value = info.GetValue("TrackedObject", typeof(T));
			GCHandleType gchandleType = (this.trackResurrection ? GCHandleType.WeakTrackResurrection : GCHandleType.Weak);
			this.handle = GCHandle.Alloc(value, gchandleType);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00069F34 File Offset: 0x00068134
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackResurrection", this.trackResurrection);
			if (this.handle.IsAllocated)
			{
				info.AddValue("TrackedObject", this.handle.Target);
				return;
			}
			info.AddValue("TrackedObject", null);
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00069F90 File Offset: 0x00068190
		public void SetTarget(T target)
		{
			this.handle.Target = target;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00069FA3 File Offset: 0x000681A3
		public bool TryGetTarget(out T target)
		{
			if (!this.handle.IsAllocated)
			{
				target = default(T);
				return false;
			}
			target = (T)((object)this.handle.Target);
			return target != null;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00069FE0 File Offset: 0x000681E0
		~WeakReference()
		{
			this.handle.Free();
		}

		// Token: 0x040018A3 RID: 6307
		private GCHandle handle;

		// Token: 0x040018A4 RID: 6308
		private bool trackResurrection;
	}
}

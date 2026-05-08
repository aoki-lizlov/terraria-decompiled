using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000240 RID: 576
	[ComVisible(true)]
	[Serializable]
	public class WeakReference : ISerializable
	{
		// Token: 0x06001BF5 RID: 7157 RVA: 0x00069D28 File Offset: 0x00067F28
		private void AllocateHandle(object target)
		{
			if (this.isLongReference)
			{
				this.gcHandle = GCHandle.Alloc(target, GCHandleType.WeakTrackResurrection);
				return;
			}
			this.gcHandle = GCHandle.Alloc(target, GCHandleType.Weak);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00069D4D File Offset: 0x00067F4D
		public WeakReference(object target)
			: this(target, false)
		{
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00069D57 File Offset: 0x00067F57
		public WeakReference(object target, bool trackResurrection)
		{
			this.isLongReference = trackResurrection;
			this.AllocateHandle(target);
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00069D70 File Offset: 0x00067F70
		protected WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.isLongReference = info.GetBoolean("TrackResurrection");
			object value = info.GetValue("TrackedObject", typeof(object));
			this.AllocateHandle(value);
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00069DBF File Offset: 0x00067FBF
		public virtual bool IsAlive
		{
			get
			{
				return this.Target != null;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x00069DCA File Offset: 0x00067FCA
		// (set) Token: 0x06001BFB RID: 7163 RVA: 0x00069DE6 File Offset: 0x00067FE6
		public virtual object Target
		{
			get
			{
				if (!this.gcHandle.IsAllocated)
				{
					return null;
				}
				return this.gcHandle.Target;
			}
			set
			{
				this.gcHandle.Target = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x00069DF4 File Offset: 0x00067FF4
		public virtual bool TrackResurrection
		{
			get
			{
				return this.isLongReference;
			}
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x00069DFC File Offset: 0x00067FFC
		~WeakReference()
		{
			this.gcHandle.Free();
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00069E30 File Offset: 0x00068030
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackResurrection", this.TrackResurrection);
			try
			{
				info.AddValue("TrackedObject", this.Target);
			}
			catch (Exception)
			{
				info.AddValue("TrackedObject", null);
			}
		}

		// Token: 0x040018A1 RID: 6305
		private bool isLongReference;

		// Token: 0x040018A2 RID: 6306
		private GCHandle gcHandle;
	}
}

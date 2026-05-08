using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x0200014E RID: 334
	public class AudioEmitter
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0003C09D File Offset: 0x0003A29D
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x0003C0AA File Offset: 0x0003A2AA
		public float DopplerScale
		{
			get
			{
				return this.emitterData.DopplerScaler;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentOutOfRangeException("AudioEmitter.DopplerScale must be greater than or equal to 0.0f");
				}
				this.emitterData.DopplerScaler = value;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0003C0CB File Offset: 0x0003A2CB
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x0003C104 File Offset: 0x0003A304
		public Vector3 Forward
		{
			get
			{
				return new Vector3(this.emitterData.OrientFront.x, this.emitterData.OrientFront.y, -this.emitterData.OrientFront.z);
			}
			set
			{
				this.emitterData.OrientFront.x = value.X;
				this.emitterData.OrientFront.y = value.Y;
				this.emitterData.OrientFront.z = -value.Z;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0003C154 File Offset: 0x0003A354
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x0003C18C File Offset: 0x0003A38C
		public Vector3 Position
		{
			get
			{
				return new Vector3(this.emitterData.Position.x, this.emitterData.Position.y, -this.emitterData.Position.z);
			}
			set
			{
				this.emitterData.Position.x = value.X;
				this.emitterData.Position.y = value.Y;
				this.emitterData.Position.z = -value.Z;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0003C1DC File Offset: 0x0003A3DC
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x0003C214 File Offset: 0x0003A414
		public Vector3 Up
		{
			get
			{
				return new Vector3(this.emitterData.OrientTop.x, this.emitterData.OrientTop.y, -this.emitterData.OrientTop.z);
			}
			set
			{
				this.emitterData.OrientTop.x = value.X;
				this.emitterData.OrientTop.y = value.Y;
				this.emitterData.OrientTop.z = -value.Z;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0003C264 File Offset: 0x0003A464
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x0003C29C File Offset: 0x0003A49C
		public Vector3 Velocity
		{
			get
			{
				return new Vector3(this.emitterData.Velocity.x, this.emitterData.Velocity.y, -this.emitterData.Velocity.z);
			}
			set
			{
				this.emitterData.Velocity.x = value.X;
				this.emitterData.Velocity.y = value.Y;
				this.emitterData.Velocity.z = -value.Z;
			}
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0003C2EC File Offset: 0x0003A4EC
		public AudioEmitter()
		{
			this.emitterData = default(FAudio.F3DAUDIO_EMITTER);
			this.DopplerScale = 1f;
			this.Forward = Vector3.Forward;
			this.Position = Vector3.Zero;
			this.Up = Vector3.Up;
			this.Velocity = Vector3.Zero;
			this.emitterData.pCone = IntPtr.Zero;
			this.emitterData.ChannelCount = 1U;
			this.emitterData.ChannelRadius = 1f;
			this.emitterData.pChannelAzimuths = AudioEmitter.stereoAzimuthHandle.AddrOfPinnedObject();
			this.emitterData.pVolumeCurve = IntPtr.Zero;
			this.emitterData.pLFECurve = IntPtr.Zero;
			this.emitterData.pLPFDirectCurve = IntPtr.Zero;
			this.emitterData.pLPFReverbCurve = IntPtr.Zero;
			this.emitterData.pReverbCurve = IntPtr.Zero;
			this.emitterData.CurveDistanceScaler = 1f;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0003C3E6 File Offset: 0x0003A5E6
		// Note: this type is marked as 'beforefieldinit'.
		static AudioEmitter()
		{
		}

		// Token: 0x04000AEB RID: 2795
		internal FAudio.F3DAUDIO_EMITTER emitterData;

		// Token: 0x04000AEC RID: 2796
		private static readonly float[] stereoAzimuth = new float[2];

		// Token: 0x04000AED RID: 2797
		private static readonly GCHandle stereoAzimuthHandle = GCHandle.Alloc(AudioEmitter.stereoAzimuth, GCHandleType.Pinned);
	}
}

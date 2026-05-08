using System;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x02000150 RID: 336
	public class AudioListener
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0003CAEC File Offset: 0x0003ACEC
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x0003CB24 File Offset: 0x0003AD24
		public Vector3 Forward
		{
			get
			{
				return new Vector3(this.listenerData.OrientFront.x, this.listenerData.OrientFront.y, -this.listenerData.OrientFront.z);
			}
			set
			{
				this.listenerData.OrientFront.x = value.X;
				this.listenerData.OrientFront.y = value.Y;
				this.listenerData.OrientFront.z = -value.Z;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x0003CB74 File Offset: 0x0003AD74
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x0003CBAC File Offset: 0x0003ADAC
		public Vector3 Position
		{
			get
			{
				return new Vector3(this.listenerData.Position.x, this.listenerData.Position.y, -this.listenerData.Position.z);
			}
			set
			{
				this.listenerData.Position.x = value.X;
				this.listenerData.Position.y = value.Y;
				this.listenerData.Position.z = -value.Z;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0003CBFC File Offset: 0x0003ADFC
		// (set) Token: 0x060017F7 RID: 6135 RVA: 0x0003CC34 File Offset: 0x0003AE34
		public Vector3 Up
		{
			get
			{
				return new Vector3(this.listenerData.OrientTop.x, this.listenerData.OrientTop.y, -this.listenerData.OrientTop.z);
			}
			set
			{
				this.listenerData.OrientTop.x = value.X;
				this.listenerData.OrientTop.y = value.Y;
				this.listenerData.OrientTop.z = -value.Z;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0003CC84 File Offset: 0x0003AE84
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x0003CCBC File Offset: 0x0003AEBC
		public Vector3 Velocity
		{
			get
			{
				return new Vector3(this.listenerData.Velocity.x, this.listenerData.Velocity.y, -this.listenerData.Velocity.z);
			}
			set
			{
				this.listenerData.Velocity.x = value.X;
				this.listenerData.Velocity.y = value.Y;
				this.listenerData.Velocity.z = -value.Z;
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0003CD0C File Offset: 0x0003AF0C
		public AudioListener()
		{
			this.listenerData = default(FAudio.F3DAUDIO_LISTENER);
			this.Forward = Vector3.Forward;
			this.Position = Vector3.Zero;
			this.Up = Vector3.Up;
			this.Velocity = Vector3.Zero;
			this.listenerData.pCone = IntPtr.Zero;
		}

		// Token: 0x04000AFB RID: 2811
		internal FAudio.F3DAUDIO_LISTENER listenerData;
	}
}

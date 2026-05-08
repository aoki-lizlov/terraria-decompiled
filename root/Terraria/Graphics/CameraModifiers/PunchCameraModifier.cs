using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x0200021D RID: 541
	public class PunchCameraModifier : ICameraModifier
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x00532A20 File Offset: 0x00530C20
		// (set) Token: 0x060021CF RID: 8655 RVA: 0x00532A28 File Offset: 0x00530C28
		public string UniqueIdentity
		{
			[CompilerGenerated]
			get
			{
				return this.<UniqueIdentity>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<UniqueIdentity>k__BackingField = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x00532A31 File Offset: 0x00530C31
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x00532A39 File Offset: 0x00530C39
		public bool Finished
		{
			[CompilerGenerated]
			get
			{
				return this.<Finished>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Finished>k__BackingField = value;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x00532A42 File Offset: 0x00530C42
		// (set) Token: 0x060021D3 RID: 8659 RVA: 0x00532A4A File Offset: 0x00530C4A
		public bool IsAScreenShake
		{
			[CompilerGenerated]
			get
			{
				return this.<IsAScreenShake>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsAScreenShake>k__BackingField = value;
			}
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x00532A54 File Offset: 0x00530C54
		public PunchCameraModifier(Vector2 startPosition, Vector2 direction, float strength, float vibrationCyclesPerSecond, int frames, float distanceFalloff = -1f, string uniqueIdentity = null)
		{
			this._startPosition = startPosition;
			this._direction = direction;
			this._strength = strength;
			this._vibrationCyclesPerSecond = vibrationCyclesPerSecond;
			this._framesToLast = frames;
			this._distanceFalloff = distanceFalloff;
			this.UniqueIdentity = uniqueIdentity;
			this.IsAScreenShake = true;
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x00532AA4 File Offset: 0x00530CA4
		public void Update(ref CameraInfo cameraInfo)
		{
			float num = (float)Math.Cos((double)((float)this._framesLasted / 60f * this._vibrationCyclesPerSecond * 6.2831855f));
			float num2 = Utils.Remap((float)this._framesLasted, 0f, (float)this._framesToLast, 1f, 0f, true);
			float num3 = Utils.Remap(Vector2.Distance(this._startPosition, cameraInfo.OriginalCameraCenter), 0f, this._distanceFalloff, 1f, 0f, true);
			if (this._distanceFalloff == -1f)
			{
				num3 = 1f;
			}
			cameraInfo.CameraPosition += this._direction * num * this._strength * num2 * num3;
			this._framesLasted++;
			if (this._framesLasted >= this._framesToLast)
			{
				this.Finished = true;
			}
		}

		// Token: 0x04004C3C RID: 19516
		private int _framesToLast;

		// Token: 0x04004C3D RID: 19517
		private Vector2 _startPosition;

		// Token: 0x04004C3E RID: 19518
		private Vector2 _direction;

		// Token: 0x04004C3F RID: 19519
		private float _distanceFalloff;

		// Token: 0x04004C40 RID: 19520
		private float _strength;

		// Token: 0x04004C41 RID: 19521
		private float _vibrationCyclesPerSecond;

		// Token: 0x04004C42 RID: 19522
		private int _framesLasted;

		// Token: 0x04004C43 RID: 19523
		[CompilerGenerated]
		private string <UniqueIdentity>k__BackingField;

		// Token: 0x04004C44 RID: 19524
		[CompilerGenerated]
		private bool <Finished>k__BackingField;

		// Token: 0x04004C45 RID: 19525
		[CompilerGenerated]
		private bool <IsAScreenShake>k__BackingField;
	}
}

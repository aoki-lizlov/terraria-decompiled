using System;

namespace ReLogic.Peripherals.RGB.Razer
{
	// Token: 0x02000032 RID: 50
	internal class EffectHandle
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00005EF6 File Offset: 0x000040F6
		public void SetAsKeyboardEffect(ref NativeMethods.CustomKeyboardEffect effect)
		{
			this.Reset();
			EffectHandle.ValidateNativeCall(NativeMethods.CreateKeyboardEffect(NativeMethods.KeyboardEffectType.CustomKey, ref effect, ref this._handle));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005F10 File Offset: 0x00004110
		public void SetAsMouseEffect(ref NativeMethods.CustomMouseEffect effect)
		{
			this.Reset();
			EffectHandle.ValidateNativeCall(NativeMethods.CreateMouseEffect(NativeMethods.MouseEffectType.Custom2, ref effect, ref this._handle));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005F2A File Offset: 0x0000412A
		public void SetAsHeadsetEffect(ref NativeMethods.CustomHeadsetEffect effect)
		{
			this.Reset();
			EffectHandle.ValidateNativeCall(NativeMethods.CreateHeadsetEffect(NativeMethods.HeadsetEffectType.Custom, ref effect, ref this._handle));
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005F44 File Offset: 0x00004144
		public void SetAsMousepadEffect(ref NativeMethods.CustomMousepadEffect effect)
		{
			this.Reset();
			EffectHandle.ValidateNativeCall(NativeMethods.CreateMousepadEffect(NativeMethods.MousepadEffectType.Custom, ref effect, ref this._handle));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005F5E File Offset: 0x0000415E
		public void SetAsKeypadEffect(ref NativeMethods.CustomKeypadEffect effect)
		{
			this.Reset();
			EffectHandle.ValidateNativeCall(NativeMethods.CreateKeypadEffect(NativeMethods.KeypadEffectType.Custom, ref effect, ref this._handle));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005F78 File Offset: 0x00004178
		public void SetAsChromaLinkEffect(ref NativeMethods.CustomChromaLinkEffect effect)
		{
			this.Reset();
			EffectHandle.ValidateNativeCall(NativeMethods.CreateChromaLinkEffect(NativeMethods.ChromaLinkEffectType.Custom, ref effect, ref this._handle));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005F92 File Offset: 0x00004192
		public void Reset()
		{
			if (this._handle == Guid.Empty)
			{
				return;
			}
			EffectHandle.ValidateNativeCall(NativeMethods.DeleteEffect(this._handle));
			this._handle = Guid.Empty;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005FC2 File Offset: 0x000041C2
		public void Apply()
		{
			if (this._handle != Guid.Empty)
			{
				EffectHandle.ValidateNativeCall(NativeMethods.SetEffect(this._handle));
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000046AD File Offset: 0x000028AD
		private static void ValidateNativeCall(RzResult result)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005FE6 File Offset: 0x000041E6
		public EffectHandle()
		{
		}

		// Token: 0x04000098 RID: 152
		private Guid _handle = Guid.Empty;
	}
}

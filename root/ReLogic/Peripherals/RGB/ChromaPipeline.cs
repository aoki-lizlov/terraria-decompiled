using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x0200002A RID: 42
	internal class ChromaPipeline
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000129 RID: 297 RVA: 0x0000529C File Offset: 0x0000349C
		// (remove) Token: 0x0600012A RID: 298 RVA: 0x000052D4 File Offset: 0x000034D4
		public event ChromaPipeline.PostProcessingEvent PostProcessingEvents
		{
			[CompilerGenerated]
			add
			{
				ChromaPipeline.PostProcessingEvent postProcessingEvent = this.PostProcessingEvents;
				ChromaPipeline.PostProcessingEvent postProcessingEvent2;
				do
				{
					postProcessingEvent2 = postProcessingEvent;
					ChromaPipeline.PostProcessingEvent postProcessingEvent3 = (ChromaPipeline.PostProcessingEvent)Delegate.Combine(postProcessingEvent2, value);
					postProcessingEvent = Interlocked.CompareExchange<ChromaPipeline.PostProcessingEvent>(ref this.PostProcessingEvents, postProcessingEvent3, postProcessingEvent2);
				}
				while (postProcessingEvent != postProcessingEvent2);
			}
			[CompilerGenerated]
			remove
			{
				ChromaPipeline.PostProcessingEvent postProcessingEvent = this.PostProcessingEvents;
				ChromaPipeline.PostProcessingEvent postProcessingEvent2;
				do
				{
					postProcessingEvent2 = postProcessingEvent;
					ChromaPipeline.PostProcessingEvent postProcessingEvent3 = (ChromaPipeline.PostProcessingEvent)Delegate.Remove(postProcessingEvent2, value);
					postProcessingEvent = Interlocked.CompareExchange<ChromaPipeline.PostProcessingEvent>(ref this.PostProcessingEvents, postProcessingEvent3, postProcessingEvent2);
				}
				while (postProcessingEvent != postProcessingEvent2);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005309 File Offset: 0x00003509
		public void SetHotkeys(IEnumerable<RgbKey> keys)
		{
			this._hotkeys = keys;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005314 File Offset: 0x00003514
		public void Process(IEnumerable<RgbDevice> devices, IEnumerable<ShaderOperation> shaders, float time)
		{
			foreach (RgbDevice rgbDevice in devices)
			{
				this.ProcessDevice(rgbDevice, shaders, time);
				rgbDevice.Present();
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005364 File Offset: 0x00003564
		private void ProcessDevice(RgbDevice device, IEnumerable<ShaderOperation> shaders, float time)
		{
			Fragment fragment = device.Rasterize();
			if (shaders != null)
			{
				foreach (ShaderOperation shaderOperation in shaders)
				{
					shaderOperation.Process(device, fragment, time);
					device.Render(fragment, shaderOperation.BlendState);
				}
			}
			if (this._hotkeys != null)
			{
				RgbKeyboard rgbKeyboard = device as RgbKeyboard;
				if (rgbKeyboard != null)
				{
					rgbKeyboard.Render(Enumerable.Where<RgbKey>(this._hotkeys, (RgbKey key) => key.IsVisible));
				}
			}
			if (this.PostProcessingEvents != null)
			{
				fragment.Clear();
				this.PostProcessingEvents(device, fragment, time);
				device.Render(fragment, new ShaderBlendState(BlendMode.PerPixelOpacity, 1f));
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000448A File Offset: 0x0000268A
		public ChromaPipeline()
		{
		}

		// Token: 0x04000071 RID: 113
		[CompilerGenerated]
		private ChromaPipeline.PostProcessingEvent PostProcessingEvents;

		// Token: 0x04000072 RID: 114
		private IEnumerable<RgbKey> _hotkeys;

		// Token: 0x020000B9 RID: 185
		// (Invoke) Token: 0x06000428 RID: 1064
		public delegate void PostProcessingEvent(RgbDevice device, Fragment fragment, float time);

		// Token: 0x020000BA RID: 186
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600042B RID: 1067 RVA: 0x0000E123 File Offset: 0x0000C323
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c()
			{
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x0000E12F File Offset: 0x0000C32F
			internal bool <ProcessDevice>b__7_0(RgbKey key)
			{
				return key.IsVisible;
			}

			// Token: 0x04000567 RID: 1383
			public static readonly ChromaPipeline.<>c <>9 = new ChromaPipeline.<>c();

			// Token: 0x04000568 RID: 1384
			public static Func<RgbKey, bool> <>9__7_0;
		}
	}
}

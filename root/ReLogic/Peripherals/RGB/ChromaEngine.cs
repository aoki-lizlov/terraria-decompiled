using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000028 RID: 40
	public class ChromaEngine
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000112 RID: 274 RVA: 0x00004A78 File Offset: 0x00002C78
		// (remove) Token: 0x06000113 RID: 275 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public event EventHandler<ChromaEngineUpdateEventArgs> OnUpdate
		{
			[CompilerGenerated]
			add
			{
				EventHandler<ChromaEngineUpdateEventArgs> eventHandler = this.OnUpdate;
				EventHandler<ChromaEngineUpdateEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ChromaEngineUpdateEventArgs> eventHandler3 = (EventHandler<ChromaEngineUpdateEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ChromaEngineUpdateEventArgs>>(ref this.OnUpdate, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<ChromaEngineUpdateEventArgs> eventHandler = this.OnUpdate;
				EventHandler<ChromaEngineUpdateEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ChromaEngineUpdateEventArgs> eventHandler3 = (EventHandler<ChromaEngineUpdateEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ChromaEngineUpdateEventArgs>>(ref this.OnUpdate, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004AE5 File Offset: 0x00002CE5
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00004AED File Offset: 0x00002CED
		public float FrameTimeInSeconds
		{
			[CompilerGenerated]
			get
			{
				return this.<FrameTimeInSeconds>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<FrameTimeInSeconds>k__BackingField = value;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public ChromaEngine()
		{
			this._pipeline.SetHotkeys(this._hotkeys);
			this.FrameTimeInSeconds = 0.022222223f;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004B60 File Offset: 0x00002D60
		public void AddDeviceGroup(string name, RgbDeviceGroup deviceGroup)
		{
			object updateLock = this._updateLock;
			lock (updateLock)
			{
				this._deviceGroups[name] = deviceGroup;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004BA8 File Offset: 0x00002DA8
		public bool HasDeviceGroup(string name)
		{
			return this._deviceGroups.ContainsKey(name);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public void RemoveDeviceGroup(string name)
		{
			RgbDeviceGroup rgbDeviceGroup = this._deviceGroups[name];
			object updateLock = this._updateLock;
			lock (updateLock)
			{
				if (rgbDeviceGroup.IsEnabled)
				{
					rgbDeviceGroup.Disable();
				}
				this._deviceGroups.Remove(name);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004C1C File Offset: 0x00002E1C
		public void EnableDeviceGroup(string name)
		{
			RgbDeviceGroup rgbDeviceGroup = this._deviceGroups[name];
			object updateLock = this._updateLock;
			lock (updateLock)
			{
				if (!rgbDeviceGroup.IsEnabled)
				{
					rgbDeviceGroup.Enable();
				}
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004C74 File Offset: 0x00002E74
		public void DisableDeviceGroup(string name)
		{
			RgbDeviceGroup rgbDeviceGroup = this._deviceGroups[name];
			object updateLock = this._updateLock;
			lock (updateLock)
			{
				if (rgbDeviceGroup.IsEnabled)
				{
					rgbDeviceGroup.Disable();
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004CCC File Offset: 0x00002ECC
		public void EnableAllDeviceGroups()
		{
			object updateLock = this._updateLock;
			lock (updateLock)
			{
				foreach (RgbDeviceGroup rgbDeviceGroup in Enumerable.Where<RgbDeviceGroup>(this._deviceGroups.Values, (RgbDeviceGroup deviceGroup) => !deviceGroup.IsEnabled))
				{
					rgbDeviceGroup.Enable();
				}
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004D68 File Offset: 0x00002F68
		public void DisableAllDeviceGroups()
		{
			object updateLock = this._updateLock;
			lock (updateLock)
			{
				foreach (RgbDeviceGroup rgbDeviceGroup in Enumerable.Where<RgbDeviceGroup>(this._deviceGroups.Values, (RgbDeviceGroup deviceGroup) => deviceGroup.IsEnabled))
				{
					rgbDeviceGroup.Disable();
				}
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004E04 File Offset: 0x00003004
		public void LoadSpecialRules(object specialRulesObject)
		{
			object updateLock = this._updateLock;
			lock (updateLock)
			{
				foreach (RgbDeviceGroup rgbDeviceGroup in Enumerable.Where<RgbDeviceGroup>(this._deviceGroups.Values, (RgbDeviceGroup deviceGroup) => deviceGroup.IsEnabled))
				{
					rgbDeviceGroup.LoadSpecialRules(specialRulesObject);
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004EA0 File Offset: 0x000030A0
		public bool IsDeviceGroupEnabled(string name)
		{
			return this._deviceGroups[name].IsEnabled;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004EB4 File Offset: 0x000030B4
		public void RegisterShader(ChromaShader shader, ChromaCondition condition, ShaderLayer layer)
		{
			object updateLock = this._updateLock;
			lock (updateLock)
			{
				this._shaderSelector.Register(shader, condition, layer);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004EFC File Offset: 0x000030FC
		public void UnregisterShader(ChromaShader shader)
		{
			object updateLock = this._updateLock;
			lock (updateLock)
			{
				this._shaderSelector.Unregister(shader);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004F44 File Offset: 0x00003144
		public RgbKey BindKey(Keys key, string keyTriggerName)
		{
			object updateLock = this._updateLock;
			RgbKey rgbKey;
			lock (updateLock)
			{
				rgbKey = this._hotkeys.BindKey(key, keyTriggerName);
			}
			return rgbKey;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004F90 File Offset: 0x00003190
		public void UnbindKey(Keys key)
		{
			object updateLock = this._updateLock;
			lock (updateLock)
			{
				this._hotkeys.UnbindKey(key);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004FD8 File Offset: 0x000031D8
		public void DebugDraw(IDebugDrawer drawer, Vector2 position, float scale)
		{
			foreach (RgbDeviceGroup rgbDeviceGroup in this._deviceGroups.Values)
			{
				bool flag = false;
				foreach (RgbDevice rgbDevice in rgbDeviceGroup)
				{
					rgbDevice.DebugDraw(drawer, position, scale);
					flag = true;
				}
				if (flag)
				{
					position.Y += 1.2f * scale;
				}
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005078 File Offset: 0x00003278
		public void Update(float totalTime)
		{
			if (this._deviceGroups.Count == 0)
			{
				return;
			}
			if (totalTime < this._lastTime)
			{
				this._lastTime = totalTime;
			}
			float num = totalTime - this._lastTime;
			if (num >= this.FrameTimeInSeconds && Monitor.TryEnter(this._updateLock))
			{
				try
				{
					if (this.OnUpdate != null)
					{
						this.OnUpdate.Invoke(this, new ChromaEngineUpdateEventArgs(num));
					}
					this._hotkeys.UpdateAll(num);
					this._shaderSelector.Update(num);
					this._lastTime = totalTime;
					ThreadPool.QueueUserWorkItem(delegate(object context)
					{
						((ChromaEngine)context).Draw();
					}, this);
				}
				catch
				{
					this.DisableAllDeviceGroups();
				}
				finally
				{
					Monitor.Exit(this._updateLock);
				}
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005158 File Offset: 0x00003358
		private void Draw()
		{
			object updateLock = this._updateLock;
			lock (updateLock)
			{
				try
				{
					for (int i = 0; i <= 1; i++)
					{
						EffectDetailLevel detail = (EffectDetailLevel)i;
						ICollection<ShaderOperation> collection = this._shaderSelector.AtDetailLevel(detail);
						Func<RgbDevice, bool> <>9__0;
						foreach (RgbDeviceGroup rgbDeviceGroup in this._deviceGroups.Values)
						{
							ChromaPipeline pipeline = this._pipeline;
							IEnumerable<RgbDevice> enumerable = rgbDeviceGroup;
							Func<RgbDevice, bool> func;
							if ((func = <>9__0) == null)
							{
								func = (<>9__0 = (RgbDevice device) => device.PreferredLevelOfDetail == detail);
							}
							pipeline.Process(Enumerable.Where<RgbDevice>(enumerable, func), collection, this._lastTime);
						}
					}
				}
				catch
				{
					this.DisableAllDeviceGroups();
				}
				foreach (RgbDeviceGroup rgbDeviceGroup2 in this._deviceGroups.Values)
				{
					rgbDeviceGroup2.OnceProcessed();
				}
			}
		}

		// Token: 0x04000069 RID: 105
		[CompilerGenerated]
		private EventHandler<ChromaEngineUpdateEventArgs> OnUpdate;

		// Token: 0x0400006A RID: 106
		[CompilerGenerated]
		private float <FrameTimeInSeconds>k__BackingField;

		// Token: 0x0400006B RID: 107
		private readonly ChromaPipeline _pipeline = new ChromaPipeline();

		// Token: 0x0400006C RID: 108
		private readonly ShaderSelector _shaderSelector = new ShaderSelector();

		// Token: 0x0400006D RID: 109
		private readonly HotkeyCollection _hotkeys = new HotkeyCollection();

		// Token: 0x0400006E RID: 110
		private readonly Dictionary<string, RgbDeviceGroup> _deviceGroups = new Dictionary<string, RgbDeviceGroup>();

		// Token: 0x0400006F RID: 111
		private readonly object _updateLock = new object();

		// Token: 0x04000070 RID: 112
		private float _lastTime;

		// Token: 0x020000B6 RID: 182
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600041D RID: 1053 RVA: 0x0000E0CB File Offset: 0x0000C2CB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c()
			{
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x0000E0D7 File Offset: 0x0000C2D7
			internal bool <EnableAllDeviceGroups>b__19_0(RgbDeviceGroup deviceGroup)
			{
				return !deviceGroup.IsEnabled;
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x0000E0E2 File Offset: 0x0000C2E2
			internal bool <DisableAllDeviceGroups>b__20_0(RgbDeviceGroup deviceGroup)
			{
				return deviceGroup.IsEnabled;
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x0000E0E2 File Offset: 0x0000C2E2
			internal bool <LoadSpecialRules>b__21_0(RgbDeviceGroup deviceGroup)
			{
				return deviceGroup.IsEnabled;
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x0000E0EA File Offset: 0x0000C2EA
			internal void <Update>b__28_0(object context)
			{
				((ChromaEngine)context).Draw();
			}

			// Token: 0x0400055F RID: 1375
			public static readonly ChromaEngine.<>c <>9 = new ChromaEngine.<>c();

			// Token: 0x04000560 RID: 1376
			public static Func<RgbDeviceGroup, bool> <>9__19_0;

			// Token: 0x04000561 RID: 1377
			public static Func<RgbDeviceGroup, bool> <>9__20_0;

			// Token: 0x04000562 RID: 1378
			public static Func<RgbDeviceGroup, bool> <>9__21_0;

			// Token: 0x04000563 RID: 1379
			public static WaitCallback <>9__28_0;
		}

		// Token: 0x020000B7 RID: 183
		[CompilerGenerated]
		private sealed class <>c__DisplayClass29_0
		{
			// Token: 0x06000423 RID: 1059 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass29_0()
			{
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x0000E0F7 File Offset: 0x0000C2F7
			internal bool <Draw>b__0(RgbDevice device)
			{
				return device.PreferredLevelOfDetail == this.detail;
			}

			// Token: 0x04000564 RID: 1380
			public EffectDetailLevel detail;

			// Token: 0x04000565 RID: 1381
			public Func<RgbDevice, bool> <>9__0;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ReLogic.Content;
using Terraria.IO;

namespace Terraria.DataStructures
{
	// Token: 0x0200055A RID: 1370
	public abstract class SelectionHolder<TCycleType> where TCycleType : class, IConfigKeyHolder
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x0062FC52 File Offset: 0x0062DE52
		// (set) Token: 0x060037AF RID: 14255 RVA: 0x0062FC5A File Offset: 0x0062DE5A
		public string ActiveSelectionKeyName
		{
			[CompilerGenerated]
			get
			{
				return this.<ActiveSelectionKeyName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ActiveSelectionKeyName>k__BackingField = value;
			}
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x0062FC63 File Offset: 0x0062DE63
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Wrapped_Configuration_OnLoad;
			preferences.OnSave += this.Configuration_Save;
		}

		// Token: 0x060037B1 RID: 14257
		protected abstract void Configuration_Save(Preferences obj);

		// Token: 0x060037B2 RID: 14258
		protected abstract void Configuration_OnLoad(Preferences obj);

		// Token: 0x060037B3 RID: 14259 RVA: 0x0062FC8A File Offset: 0x0062DE8A
		protected void Wrapped_Configuration_OnLoad(Preferences obj)
		{
			this.Configuration_OnLoad(obj);
			if (this.LoadedContent)
			{
				this.SetActiveMinimapFromLoadedConfigKey();
			}
		}

		// Token: 0x060037B4 RID: 14260
		protected abstract void PopulateOptionsAndLoadContent(AssetRequestMode mode);

		// Token: 0x060037B5 RID: 14261 RVA: 0x0062FCA1 File Offset: 0x0062DEA1
		public void LoadContent(AssetRequestMode mode)
		{
			this.PopulateOptionsAndLoadContent(mode);
			this.LoadedContent = true;
			this.SetActiveMinimapFromLoadedConfigKey();
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x0062FCB8 File Offset: 0x0062DEB8
		public void CycleSelection()
		{
			TCycleType lastFrame = default(TCycleType);
			this.Options.Values.FirstOrDefault(delegate(TCycleType frame)
			{
				if (frame == this.ActiveSelection)
				{
					return true;
				}
				lastFrame = frame;
				return false;
			});
			if (lastFrame == null)
			{
				lastFrame = this.Options.Values.Last<TCycleType>();
			}
			this.SetActiveFrame(lastFrame);
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x0062FD2A File Offset: 0x0062DF2A
		public void SetActiveMinimapFromLoadedConfigKey()
		{
			this.SetActiveFrame(this.ActiveSelectionConfigKey);
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x0062FD38 File Offset: 0x0062DF38
		private void SetActiveFrame(string frameName)
		{
			TCycleType tcycleType = this.Options.FirstOrDefault((KeyValuePair<string, TCycleType> pair) => pair.Key == frameName).Value;
			if (tcycleType == null)
			{
				tcycleType = this.Options.Values.First<TCycleType>();
			}
			this.SetActiveFrame(tcycleType);
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x0062FD92 File Offset: 0x0062DF92
		private void SetActiveFrame(TCycleType frame)
		{
			this.ActiveSelection = frame;
			this.ActiveSelectionConfigKey = frame.ConfigKey;
			this.ActiveSelectionKeyName = frame.NameKey;
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x0062FDBD File Offset: 0x0062DFBD
		protected SelectionHolder()
		{
		}

		// Token: 0x04005BD1 RID: 23505
		protected Dictionary<string, TCycleType> Options = new Dictionary<string, TCycleType>();

		// Token: 0x04005BD2 RID: 23506
		protected TCycleType ActiveSelection;

		// Token: 0x04005BD3 RID: 23507
		protected string ActiveSelectionConfigKey;

		// Token: 0x04005BD4 RID: 23508
		protected bool LoadedContent;

		// Token: 0x04005BD5 RID: 23509
		[CompilerGenerated]
		private string <ActiveSelectionKeyName>k__BackingField;

		// Token: 0x020009BA RID: 2490
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06004A32 RID: 18994 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x06004A33 RID: 18995 RVA: 0x006D3EE0 File Offset: 0x006D20E0
			internal bool <CycleSelection>b__0(TCycleType frame)
			{
				if (frame == this.<>4__this.ActiveSelection)
				{
					return true;
				}
				this.lastFrame = frame;
				return false;
			}

			// Token: 0x040076CD RID: 30413
			public SelectionHolder<TCycleType> <>4__this;

			// Token: 0x040076CE RID: 30414
			public TCycleType lastFrame;
		}

		// Token: 0x020009BB RID: 2491
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_0
		{
			// Token: 0x06004A34 RID: 18996 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass16_0()
			{
			}

			// Token: 0x06004A35 RID: 18997 RVA: 0x006D3F04 File Offset: 0x006D2104
			internal bool <SetActiveFrame>b__0(KeyValuePair<string, TCycleType> pair)
			{
				return pair.Key == this.frameName;
			}

			// Token: 0x040076CF RID: 30415
			public string frameName;
		}
	}
}

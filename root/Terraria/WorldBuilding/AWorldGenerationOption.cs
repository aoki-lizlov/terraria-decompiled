using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000099 RID: 153
	public abstract class AWorldGenerationOption
	{
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060016FD RID: 5885 RVA: 0x004DD6EC File Offset: 0x004DB8EC
		// (remove) Token: 0x060016FE RID: 5886 RVA: 0x004DD720 File Offset: 0x004DB920
		protected static event Action<AWorldGenerationOption> OnOptionStateChanged
		{
			[CompilerGenerated]
			add
			{
				Action<AWorldGenerationOption> action = AWorldGenerationOption.OnOptionStateChanged;
				Action<AWorldGenerationOption> action2;
				do
				{
					action2 = action;
					Action<AWorldGenerationOption> action3 = (Action<AWorldGenerationOption>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<AWorldGenerationOption>>(ref AWorldGenerationOption.OnOptionStateChanged, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<AWorldGenerationOption> action = AWorldGenerationOption.OnOptionStateChanged;
				Action<AWorldGenerationOption> action2;
				do
				{
					action2 = action;
					Action<AWorldGenerationOption> action3 = (Action<AWorldGenerationOption>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<AWorldGenerationOption>>(ref AWorldGenerationOption.OnOptionStateChanged, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x004DD753 File Offset: 0x004DB953
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x004DD75B File Offset: 0x004DB95B
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (this._enabled == value)
				{
					return;
				}
				this._enabled = value;
				this.OnEnabledStateChanged();
				AWorldGenerationOption.OnOptionStateChanged(this);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001701 RID: 5889
		protected abstract string KeyName { get; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001702 RID: 5890
		public abstract string ServerConfigName { get; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x004DD77F File Offset: 0x004DB97F
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x004DD787 File Offset: 0x004DB987
		public string[] SpecialSeedNames
		{
			[CompilerGenerated]
			get
			{
				return this.<SpecialSeedNames>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<SpecialSeedNames>k__BackingField = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x004DD790 File Offset: 0x004DB990
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x004DD798 File Offset: 0x004DB998
		public int[] SpecialSeedValues
		{
			[CompilerGenerated]
			get
			{
				return this.<SpecialSeedValues>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<SpecialSeedValues>k__BackingField = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x004DD7A1 File Offset: 0x004DB9A1
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x004DD7A9 File Offset: 0x004DB9A9
		public LocalizedText Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Description>k__BackingField = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x004DD7B2 File Offset: 0x004DB9B2
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x004DD7BA File Offset: 0x004DB9BA
		public LocalizedText Title
		{
			[CompilerGenerated]
			get
			{
				return this.<Title>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Title>k__BackingField = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x004DD7C3 File Offset: 0x004DB9C3
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x004DD7CB File Offset: 0x004DB9CB
		private protected Asset<Texture2D> Texture
		{
			[CompilerGenerated]
			protected get
			{
				return this.<Texture>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Texture>k__BackingField = value;
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void OnEnabledStateChanged()
		{
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x004DD7D4 File Offset: 0x004DB9D4
		public void Load()
		{
			if (this.Texture != null)
			{
				return;
			}
			this.Description = Language.GetText("UI." + this.KeyName);
			this.Title = Language.GetText("UI." + this.KeyName + "_Title");
			if (Main.dedServ)
			{
				return;
			}
			this.Texture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/" + this.KeyName, 1);
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x004DD84E File Offset: 0x004DBA4E
		public virtual UIElement ProvideUIElement()
		{
			return new UIImage(this.Texture)
			{
				Left = StyleDimension.FromPixels(-1f)
			};
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0000357B File Offset: 0x0000177B
		protected AWorldGenerationOption()
		{
		}

		// Token: 0x040011CE RID: 4558
		[CompilerGenerated]
		private static Action<AWorldGenerationOption> OnOptionStateChanged;

		// Token: 0x040011CF RID: 4559
		private bool _enabled;

		// Token: 0x040011D0 RID: 4560
		public bool AutoGenEnabled;

		// Token: 0x040011D1 RID: 4561
		[CompilerGenerated]
		private string[] <SpecialSeedNames>k__BackingField;

		// Token: 0x040011D2 RID: 4562
		[CompilerGenerated]
		private int[] <SpecialSeedValues>k__BackingField;

		// Token: 0x040011D3 RID: 4563
		[CompilerGenerated]
		private LocalizedText <Description>k__BackingField;

		// Token: 0x040011D4 RID: 4564
		[CompilerGenerated]
		private LocalizedText <Title>k__BackingField;

		// Token: 0x040011D5 RID: 4565
		[CompilerGenerated]
		private Asset<Texture2D> <Texture>k__BackingField;
	}
}

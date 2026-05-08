using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000A2 RID: 162
	public class WorldSeedOption_Everything : AWorldGenerationOption
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x004DDAD3 File Offset: 0x004DBCD3
		protected override string KeyName
		{
			get
			{
				return "Seed_Everything";
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x004DDADA File Offset: 0x004DBCDA
		public override string ServerConfigName
		{
			get
			{
				return "zenith";
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x004DDAE4 File Offset: 0x004DBCE4
		public List<AWorldGenerationOption> Dependencies
		{
			get
			{
				if (this._dependencies == null)
				{
					this._dependencies = new List<AWorldGenerationOption>
					{
						WorldGenerationOptions.Get<WorldSeedOption_Remix>(),
						WorldGenerationOptions.Get<WorldSeedOption_Drunk>(),
						WorldGenerationOptions.Get<WorldSeedOption_NotTheBees>(),
						WorldGenerationOptions.Get<WorldSeedOption_NoTraps>(),
						WorldGenerationOptions.Get<WorldSeedOption_DontStarve>(),
						WorldGenerationOptions.Get<WorldSeedOption_Anniversary>(),
						WorldGenerationOptions.Get<WorldSeedOption_ForTheWorthy>()
					};
				}
				return this._dependencies;
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x004DDB57 File Offset: 0x004DBD57
		public WorldSeedOption_Everything()
		{
			base.SpecialSeedNames = new string[] { "getfixedboi" };
			base.SpecialSeedValues = new int[0];
			AWorldGenerationOption.OnOptionStateChanged += this.UpdateDependentState;
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x004DDB90 File Offset: 0x004DBD90
		private void UpdateDependentState(AWorldGenerationOption changed)
		{
			if (this.Dependencies.Contains(changed) && changed.Enabled != base.Enabled)
			{
				base.Enabled = this.Dependencies.All((AWorldGenerationOption d) => d.Enabled);
			}
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x004DDBEC File Offset: 0x004DBDEC
		protected override void OnEnabledStateChanged()
		{
			if (!base.Enabled)
			{
				if (this.Dependencies.Any((AWorldGenerationOption d) => !d.Enabled))
				{
					return;
				}
			}
			foreach (AWorldGenerationOption aworldGenerationOption in this.Dependencies)
			{
				aworldGenerationOption.Enabled = base.Enabled;
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x004DDC78 File Offset: 0x004DBE78
		public override UIElement ProvideUIElement()
		{
			UIImageFramed image = new UIImageFramed(base.Texture, base.Texture.Frame(7, 16, 0, 0, 0, 0))
			{
				Left = StyleDimension.FromPixels(-1f)
			};
			int glitchFrameCounter = 0;
			int glitchFrame = 0;
			int glitchVariation = 0;
			image.OnUpdate += delegate(UIElement _)
			{
				int num = 3;
				int num2 = 3;
				if (glitchFrame == 0)
				{
					num = 15;
					num2 = 120;
				}
				int num3 = glitchFrameCounter + 1;
				glitchFrameCounter = num3;
				if (num3 >= Main.rand.Next(num, num2 + 1))
				{
					glitchFrameCounter = 0;
					glitchFrame = (glitchFrame + 1) % 16;
					if ((glitchFrame == 4 || glitchFrame == 8 || glitchFrame == 12) && Main.rand.Next(3) == 0)
					{
						glitchVariation = Main.rand.Next(7);
					}
				}
				image.SetFrame(7, 16, glitchVariation, glitchFrame, 0, 0);
			};
			return image;
		}

		// Token: 0x040011D6 RID: 4566
		protected List<AWorldGenerationOption> _dependencies;

		// Token: 0x0200068E RID: 1678
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003E98 RID: 16024 RVA: 0x0069816A File Offset: 0x0069636A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003E99 RID: 16025 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003E9A RID: 16026 RVA: 0x00698176 File Offset: 0x00696376
			internal bool <UpdateDependentState>b__8_0(AWorldGenerationOption d)
			{
				return d.Enabled;
			}

			// Token: 0x06003E9B RID: 16027 RVA: 0x0069817E File Offset: 0x0069637E
			internal bool <OnEnabledStateChanged>b__9_0(AWorldGenerationOption d)
			{
				return !d.Enabled;
			}

			// Token: 0x04006765 RID: 26469
			public static readonly WorldSeedOption_Everything.<>c <>9 = new WorldSeedOption_Everything.<>c();

			// Token: 0x04006766 RID: 26470
			public static Func<AWorldGenerationOption, bool> <>9__8_0;

			// Token: 0x04006767 RID: 26471
			public static Func<AWorldGenerationOption, bool> <>9__9_0;
		}

		// Token: 0x0200068F RID: 1679
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x06003E9C RID: 16028 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x06003E9D RID: 16029 RVA: 0x0069818C File Offset: 0x0069638C
			internal void <ProvideUIElement>b__0(UIElement _)
			{
				int num = 3;
				int num2 = 3;
				if (this.glitchFrame == 0)
				{
					num = 15;
					num2 = 120;
				}
				int num3 = this.glitchFrameCounter + 1;
				this.glitchFrameCounter = num3;
				if (num3 >= Main.rand.Next(num, num2 + 1))
				{
					this.glitchFrameCounter = 0;
					this.glitchFrame = (this.glitchFrame + 1) % 16;
					if ((this.glitchFrame == 4 || this.glitchFrame == 8 || this.glitchFrame == 12) && Main.rand.Next(3) == 0)
					{
						this.glitchVariation = Main.rand.Next(7);
					}
				}
				this.image.SetFrame(7, 16, this.glitchVariation, this.glitchFrame, 0, 0);
			}

			// Token: 0x04006768 RID: 26472
			public int glitchFrame;

			// Token: 0x04006769 RID: 26473
			public int glitchFrameCounter;

			// Token: 0x0400676A RID: 26474
			public int glitchVariation;

			// Token: 0x0400676B RID: 26475
			public UIImageFramed image;
		}
	}
}

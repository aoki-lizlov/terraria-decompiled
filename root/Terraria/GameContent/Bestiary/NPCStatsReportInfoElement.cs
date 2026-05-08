using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000360 RID: 864
	public class NPCStatsReportInfoElement : IBestiaryInfoElement, IUpdateBeforeSorting
	{
		// Token: 0x060028C8 RID: 10440 RVA: 0x005747C1 File Offset: 0x005729C1
		public NPCStatsReportInfoElement(int npcNetId)
		{
			this.NpcId = npcNetId;
			this._instance = new NPC();
			this.RefreshStats(this._instance);
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060028C9 RID: 10441 RVA: 0x005747E8 File Offset: 0x005729E8
		// (remove) Token: 0x060028CA RID: 10442 RVA: 0x00574820 File Offset: 0x00572A20
		public event NPCStatsReportInfoElement.StatAdjustmentStep OnRefreshStats
		{
			[CompilerGenerated]
			add
			{
				NPCStatsReportInfoElement.StatAdjustmentStep statAdjustmentStep = this.OnRefreshStats;
				NPCStatsReportInfoElement.StatAdjustmentStep statAdjustmentStep2;
				do
				{
					statAdjustmentStep2 = statAdjustmentStep;
					NPCStatsReportInfoElement.StatAdjustmentStep statAdjustmentStep3 = (NPCStatsReportInfoElement.StatAdjustmentStep)Delegate.Combine(statAdjustmentStep2, value);
					statAdjustmentStep = Interlocked.CompareExchange<NPCStatsReportInfoElement.StatAdjustmentStep>(ref this.OnRefreshStats, statAdjustmentStep3, statAdjustmentStep2);
				}
				while (statAdjustmentStep != statAdjustmentStep2);
			}
			[CompilerGenerated]
			remove
			{
				NPCStatsReportInfoElement.StatAdjustmentStep statAdjustmentStep = this.OnRefreshStats;
				NPCStatsReportInfoElement.StatAdjustmentStep statAdjustmentStep2;
				do
				{
					statAdjustmentStep2 = statAdjustmentStep;
					NPCStatsReportInfoElement.StatAdjustmentStep statAdjustmentStep3 = (NPCStatsReportInfoElement.StatAdjustmentStep)Delegate.Remove(statAdjustmentStep2, value);
					statAdjustmentStep = Interlocked.CompareExchange<NPCStatsReportInfoElement.StatAdjustmentStep>(ref this.OnRefreshStats, statAdjustmentStep3, statAdjustmentStep2);
				}
				while (statAdjustmentStep != statAdjustmentStep2);
			}
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x00574855 File Offset: 0x00572A55
		public void UpdateBeforeSorting()
		{
			this.RefreshStats(this._instance);
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x00574864 File Offset: 0x00572A64
		private void RefreshStats(NPC instance)
		{
			instance.SetDefaults(this.NpcId, default(NPCSpawnParams));
			this.Damage = instance.damage;
			this.LifeMax = instance.lifeMax;
			this.MonetaryValue = instance.value;
			this.Defense = instance.defense;
			this.KnockbackResist = instance.knockBackResist;
			if (this.OnRefreshStats != null)
			{
				this.OnRefreshStats(this);
			}
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x005748D8 File Offset: 0x00572AD8
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			this.RefreshStats(this._instance);
			UIElement uielement = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(109f, 0f)
			};
			int num = 99;
			int num2 = 35;
			int num3 = 3;
			int num4 = 0;
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_HP", 1))
			{
				Top = new StyleDimension((float)num4, 0f),
				Left = new StyleDimension((float)num3, 0f)
			};
			UIImage uiimage2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Attack", 1))
			{
				Top = new StyleDimension((float)(num4 + num2), 0f),
				Left = new StyleDimension((float)num3, 0f)
			};
			UIImage uiimage3 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Defense", 1))
			{
				Top = new StyleDimension((float)(num4 + num2), 0f),
				Left = new StyleDimension((float)(num3 + num), 0f)
			};
			UIImage uiimage4 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Knockback", 1))
			{
				Top = new StyleDimension((float)num4, 0f),
				Left = new StyleDimension((float)(num3 + num), 0f)
			};
			uielement.Append(uiimage);
			uielement.Append(uiimage2);
			uielement.Append(uiimage3);
			uielement.Append(uiimage4);
			int num5 = -10;
			int num6 = 0;
			int num7 = (int)this.MonetaryValue;
			string text = Utils.Clamp<int>(num7 / 1000000, 0, 999).ToString();
			string text2 = Utils.Clamp<int>(num7 % 1000000 / 10000, 0, 99).ToString();
			string text3 = Utils.Clamp<int>(num7 % 10000 / 100, 0, 99).ToString();
			string text4 = Utils.Clamp<int>(num7 % 100 / 1, 0, 99).ToString();
			if (num7 / 1000000 < 1)
			{
				text = "-";
			}
			if (num7 / 10000 < 1)
			{
				text2 = "-";
			}
			if (num7 / 100 < 1)
			{
				text3 = "-";
			}
			if (num7 < 1)
			{
				text4 = "-";
			}
			string text5 = this.LifeMax.ToString();
			string text6 = this.Damage.ToString();
			string text7 = this.Defense.ToString();
			string text8;
			if (this.KnockbackResist > 0.8f)
			{
				text8 = Language.GetText("BestiaryInfo.KnockbackHigh").Value;
			}
			else if (this.KnockbackResist > 0.4f)
			{
				text8 = Language.GetText("BestiaryInfo.KnockbackMedium").Value;
			}
			else if (this.KnockbackResist > 0f)
			{
				text8 = Language.GetText("BestiaryInfo.KnockbackLow").Value;
			}
			else
			{
				text8 = Language.GetText("BestiaryInfo.KnockbackNone").Value;
			}
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2 || this.HideStats)
			{
				text2 = (text = (text3 = (text4 = "?")));
				text6 = (text5 = (text7 = (text8 = "???")));
			}
			UIText uitext = new UIText(text5, 1f, false)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension((float)num5, 0f),
				Top = new StyleDimension((float)num6, 0f),
				IgnoresMouseInteraction = true
			};
			UIText uitext2 = new UIText(text8, 1f, false)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension((float)num5, 0f),
				Top = new StyleDimension((float)num6, 0f),
				IgnoresMouseInteraction = true
			};
			UIText uitext3 = new UIText(text6, 1f, false)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension((float)num5, 0f),
				Top = new StyleDimension((float)num6, 0f),
				IgnoresMouseInteraction = true
			};
			UIText uitext4 = new UIText(text7, 1f, false)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension((float)num5, 0f),
				Top = new StyleDimension((float)num6, 0f),
				IgnoresMouseInteraction = true
			};
			uiimage.Append(uitext);
			uiimage2.Append(uitext3);
			uiimage3.Append(uitext4);
			uiimage4.Append(uitext2);
			int num8 = 66;
			if (num7 > 0)
			{
				UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
					Color = new Color(89, 116, 213, 255) * 0.9f,
					Left = new StyleDimension(0f, 0f),
					Top = new StyleDimension((float)(num6 + num2 * 2), 0f)
				};
				uielement.Append(uihorizontalSeparator);
				num8 += 4;
				int num9 = num3;
				int num10 = num8 + 8;
				int num11 = 49;
				UIImage uiimage5 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Platinum", 1))
				{
					Top = new StyleDimension((float)num10, 0f),
					Left = new StyleDimension((float)num9, 0f)
				};
				UIImage uiimage6 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Gold", 1))
				{
					Top = new StyleDimension((float)num10, 0f),
					Left = new StyleDimension((float)(num9 + num11), 0f)
				};
				UIImage uiimage7 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Silver", 1))
				{
					Top = new StyleDimension((float)num10, 0f),
					Left = new StyleDimension((float)(num9 + num11 * 2 + 1), 0f)
				};
				UIImage uiimage8 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Copper", 1))
				{
					Top = new StyleDimension((float)num10, 0f),
					Left = new StyleDimension((float)(num9 + num11 * 3 + 1), 0f)
				};
				if (text != "-")
				{
					uielement.Append(uiimage5);
				}
				if (text2 != "-")
				{
					uielement.Append(uiimage6);
				}
				if (text3 != "-")
				{
					uielement.Append(uiimage7);
				}
				if (text4 != "-")
				{
					uielement.Append(uiimage8);
				}
				int num12 = num5 + 3;
				float num13 = 0.85f;
				UIText uitext5 = new UIText(text, num13, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num12, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				UIText uitext6 = new UIText(text2, num13, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num12, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				UIText uitext7 = new UIText(text3, num13, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num12, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				UIText uitext8 = new UIText(text4, num13, false)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension((float)num12, 0f),
					Top = new StyleDimension((float)num6, 0f)
				};
				uiimage5.Append(uitext5);
				uiimage6.Append(uitext6);
				uiimage7.Append(uitext7);
				uiimage8.Append(uitext8);
				num8 += 34;
			}
			num8 += 4;
			uielement.Height.Pixels = (float)num8;
			uiimage2.OnUpdate += this.ShowStats_Attack;
			uiimage3.OnUpdate += this.ShowStats_Defense;
			uiimage.OnUpdate += this.ShowStats_Life;
			uiimage4.OnUpdate += this.ShowStats_Knockback;
			return uielement;
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x005750E8 File Offset: 0x005732E8
		private void ShowStats_Attack(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Attack"), 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x0057511C File Offset: 0x0057331C
		private void ShowStats_Defense(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Defense"), 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x00575150 File Offset: 0x00573350
		private void ShowStats_Knockback(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Knockback"), 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x00575184 File Offset: 0x00573384
		private void ShowStats_Life(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Life"), 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x04005169 RID: 20841
		public int NpcId;

		// Token: 0x0400516A RID: 20842
		public int Damage;

		// Token: 0x0400516B RID: 20843
		public int LifeMax;

		// Token: 0x0400516C RID: 20844
		public float MonetaryValue;

		// Token: 0x0400516D RID: 20845
		public int Defense;

		// Token: 0x0400516E RID: 20846
		public float KnockbackResist;

		// Token: 0x0400516F RID: 20847
		private NPC _instance;

		// Token: 0x04005170 RID: 20848
		[CompilerGenerated]
		private NPCStatsReportInfoElement.StatAdjustmentStep OnRefreshStats;

		// Token: 0x04005171 RID: 20849
		public bool HideStats;

		// Token: 0x020008C4 RID: 2244
		// (Invoke) Token: 0x0600462C RID: 17964
		public delegate void StatAdjustmentStep(NPCStatsReportInfoElement element);
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.Localization;

namespace Terraria.DataStructures
{
	// Token: 0x02000535 RID: 1333
	public class ArmorSetBonus
	{
		// Token: 0x0600373D RID: 14141 RVA: 0x0062E51C File Offset: 0x0062C71C
		public int GetPart(ArmorSetBonus.PartType part)
		{
			switch (part)
			{
			case ArmorSetBonus.PartType.Head:
				return this.Head;
			case ArmorSetBonus.PartType.Body:
				return this.Body;
			case ArmorSetBonus.PartType.Legs:
				return this.Legs;
			default:
				return 0;
			}
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x0062E54C File Offset: 0x0062C74C
		public ArmorSetBonus.QueryResult QueryCount(ArmorSetBonus.QueryContext context)
		{
			ArmorSetBonus.QueryResult queryResult = default(ArmorSetBonus.QueryResult);
			this.TryCounting(context.HeadItem, this.Head, ref queryResult.ItemsFound, ref queryResult.ItemsNeeded);
			this.TryCounting(context.BodyItem, this.Body, ref queryResult.ItemsFound, ref queryResult.ItemsNeeded);
			this.TryCounting(context.LegItem, this.Legs, ref queryResult.ItemsFound, ref queryResult.ItemsNeeded);
			return queryResult;
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x0062E5C2 File Offset: 0x0062C7C2
		private void TryCounting(int testedItem, int neededItem, ref int foundItemCount, ref int neededItemCount)
		{
			if (neededItem == 0)
			{
				return;
			}
			neededItemCount++;
			if (testedItem == neededItem)
			{
				foundItemCount++;
			}
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x0062E5DC File Offset: 0x0062C7DC
		public string GetTooltipForSinglePiece(int itemType)
		{
			LocalizedText localizedText = this.Description;
			if (this.PrimaryPart != ArmorSetBonus.PartType.None && this.GetPart(this.PrimaryPart) != itemType)
			{
				localizedText = ArmorSetBonus.ItemSetBonusDecidedBy[(int)this.PrimaryPart];
			}
			return ArmorSetBonus.ItemSetBonusGeneral.FormatWith(new ArmorSetBonus.SetBonusDisplayStringSubstitutes
			{
				Description = localizedText
			});
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x0062E62C File Offset: 0x0062C82C
		public string GetTooltipForWornArmor(ArmorSetBonus.QueryContext context, ArmorSetBonus.QueryResult result)
		{
			LocalizedText localizedText = this.Description;
			if (this.PrimaryPart != ArmorSetBonus.PartType.None && context.GetPart(this.PrimaryPart) != this.GetPart(this.PrimaryPart))
			{
				localizedText = ArmorSetBonus.ItemSetBonusDecidedBy[(int)this.PrimaryPart];
			}
			return ArmorSetBonus.ItemSetBonusEquipped.FormatWith(new ArmorSetBonus.SetBonusDisplayStringSubstitutes
			{
				Description = localizedText,
				Numerator = result.ItemsFound,
				Denominator = result.ItemsNeeded
			});
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x0062E69E File Offset: 0x0062C89E
		public static ArmorSetBonus.Builder Create(ArmorSetBonus.ArmorSetEffect effect, string textKey, ArmorSetBonus.PartType primaryPart = ArmorSetBonus.PartType.None)
		{
			return new ArmorSetBonus.Builder(effect, textKey, primaryPart);
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x0000357B File Offset: 0x0000177B
		public ArmorSetBonus()
		{
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x0062E6A8 File Offset: 0x0062C8A8
		// Note: this type is marked as 'beforefieldinit'.
		static ArmorSetBonus()
		{
		}

		// Token: 0x04005B6F RID: 23407
		public ArmorSetBonus.ArmorSetEffect Effect;

		// Token: 0x04005B70 RID: 23408
		public LocalizedText Description;

		// Token: 0x04005B71 RID: 23409
		public int Head;

		// Token: 0x04005B72 RID: 23410
		public int Body;

		// Token: 0x04005B73 RID: 23411
		public int Legs;

		// Token: 0x04005B74 RID: 23412
		public ArmorSetBonus.PartType PrimaryPart;

		// Token: 0x04005B75 RID: 23413
		private static LocalizedText ItemSetBonusEquipped = Language.GetText("UI.ItemSetBonusEquipped");

		// Token: 0x04005B76 RID: 23414
		private static LocalizedText ItemSetBonusGeneral = Language.GetText("UI.ItemSetBonusGeneral");

		// Token: 0x04005B77 RID: 23415
		private static LocalizedText[] ItemSetBonusDecidedBy = new LocalizedText[]
		{
			null,
			Language.GetText("UI.ItemSetBonusDecidedByHead"),
			Language.GetText("UI.ItemSetBonusDecidedByBody"),
			Language.GetText("UI.ItemSetBonusDecidedByLegs")
		};

		// Token: 0x020009B2 RID: 2482
		// (Invoke) Token: 0x06004A1D RID: 18973
		public delegate void ArmorSetEffect(Player player);

		// Token: 0x020009B3 RID: 2483
		public enum PartType
		{
			// Token: 0x040076B8 RID: 30392
			None,
			// Token: 0x040076B9 RID: 30393
			Head,
			// Token: 0x040076BA RID: 30394
			Body,
			// Token: 0x040076BB RID: 30395
			Legs
		}

		// Token: 0x020009B4 RID: 2484
		public struct QueryContext
		{
			// Token: 0x06004A20 RID: 18976 RVA: 0x006D3BBF File Offset: 0x006D1DBF
			public QueryContext(Player player)
			{
				this.HeadItem = ArmorSetBonus.QueryContext.TryGetType(player.armor[0]);
				this.BodyItem = ArmorSetBonus.QueryContext.TryGetType(player.armor[1]);
				this.LegItem = ArmorSetBonus.QueryContext.TryGetType(player.armor[2]);
			}

			// Token: 0x06004A21 RID: 18977 RVA: 0x006D3BFA File Offset: 0x006D1DFA
			private static int TryGetType(Item item)
			{
				if (item != null)
				{
					return item.type;
				}
				return 0;
			}

			// Token: 0x06004A22 RID: 18978 RVA: 0x006D3C07 File Offset: 0x006D1E07
			public int GetPart(ArmorSetBonus.PartType part)
			{
				switch (part)
				{
				case ArmorSetBonus.PartType.Head:
					return this.HeadItem;
				case ArmorSetBonus.PartType.Body:
					return this.BodyItem;
				case ArmorSetBonus.PartType.Legs:
					return this.LegItem;
				default:
					return 0;
				}
			}

			// Token: 0x040076BC RID: 30396
			public int HeadItem;

			// Token: 0x040076BD RID: 30397
			public int BodyItem;

			// Token: 0x040076BE RID: 30398
			public int LegItem;
		}

		// Token: 0x020009B5 RID: 2485
		public struct QueryResult
		{
			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x06004A23 RID: 18979 RVA: 0x006D3C35 File Offset: 0x006D1E35
			public bool Complete
			{
				get
				{
					return this.ItemsNeeded == this.ItemsFound;
				}
			}

			// Token: 0x040076BF RID: 30399
			public int ItemsNeeded;

			// Token: 0x040076C0 RID: 30400
			public int ItemsFound;
		}

		// Token: 0x020009B6 RID: 2486
		private class SetBonusDisplayStringSubstitutes
		{
			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x06004A24 RID: 18980 RVA: 0x006D3C45 File Offset: 0x006D1E45
			// (set) Token: 0x06004A25 RID: 18981 RVA: 0x006D3C4D File Offset: 0x006D1E4D
			public int Numerator
			{
				[CompilerGenerated]
				get
				{
					return this.<Numerator>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Numerator>k__BackingField = value;
				}
			}

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x06004A26 RID: 18982 RVA: 0x006D3C56 File Offset: 0x006D1E56
			// (set) Token: 0x06004A27 RID: 18983 RVA: 0x006D3C5E File Offset: 0x006D1E5E
			public int Denominator
			{
				[CompilerGenerated]
				get
				{
					return this.<Denominator>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Denominator>k__BackingField = value;
				}
			}

			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x06004A28 RID: 18984 RVA: 0x006D3C67 File Offset: 0x006D1E67
			// (set) Token: 0x06004A29 RID: 18985 RVA: 0x006D3C6F File Offset: 0x006D1E6F
			public LocalizedText Description
			{
				[CompilerGenerated]
				get
				{
					return this.<Description>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Description>k__BackingField = value;
				}
			}

			// Token: 0x06004A2A RID: 18986 RVA: 0x0000357B File Offset: 0x0000177B
			public SetBonusDisplayStringSubstitutes()
			{
			}

			// Token: 0x040076C1 RID: 30401
			[CompilerGenerated]
			private int <Numerator>k__BackingField;

			// Token: 0x040076C2 RID: 30402
			[CompilerGenerated]
			private int <Denominator>k__BackingField;

			// Token: 0x040076C3 RID: 30403
			[CompilerGenerated]
			private LocalizedText <Description>k__BackingField;
		}

		// Token: 0x020009B7 RID: 2487
		public class Builder
		{
			// Token: 0x06004A2B RID: 18987 RVA: 0x006D3C78 File Offset: 0x006D1E78
			public Builder(ArmorSetBonus.ArmorSetEffect effect, string textKey, ArmorSetBonus.PartType primaryPart)
			{
				this.Effect = effect;
				this.TextKey = textKey;
				this.PrimaryPart = primaryPart;
			}

			// Token: 0x06004A2C RID: 18988 RVA: 0x006D3CA0 File Offset: 0x006D1EA0
			public ArmorSetBonus.Builder Set(int head, int body, int legs)
			{
				this._sets.Add(new ArmorSetBonus.Builder.Parts
				{
					Head = head,
					Body = body,
					Legs = legs
				});
				return this;
			}

			// Token: 0x06004A2D RID: 18989 RVA: 0x006D3CDC File Offset: 0x006D1EDC
			public ArmorSetBonus.Builder Set(int[] headOptions, int[] bodyOptions, int[] legsOptions)
			{
				if (headOptions == null)
				{
					headOptions = new int[1];
				}
				if (bodyOptions == null)
				{
					bodyOptions = new int[1];
				}
				if (legsOptions == null)
				{
					legsOptions = new int[1];
				}
				foreach (int num in headOptions)
				{
					foreach (int num2 in bodyOptions)
					{
						foreach (int num3 in legsOptions)
						{
							this.Set(num, num2, num3);
						}
					}
				}
				return this;
			}

			// Token: 0x06004A2E RID: 18990 RVA: 0x006D3D64 File Offset: 0x006D1F64
			public void Add()
			{
				foreach (ArmorSetBonus.Builder.Parts parts in this._sets)
				{
					ArmorSetBonuses.All.Add(new ArmorSetBonus
					{
						Effect = this.Effect,
						Description = Language.GetText(this.TextKey),
						Head = parts.Head,
						Body = parts.Body,
						Legs = parts.Legs,
						PrimaryPart = this.PrimaryPart
					});
				}
			}

			// Token: 0x040076C4 RID: 30404
			private ArmorSetBonus.ArmorSetEffect Effect;

			// Token: 0x040076C5 RID: 30405
			private string TextKey;

			// Token: 0x040076C6 RID: 30406
			private ArmorSetBonus.PartType PrimaryPart;

			// Token: 0x040076C7 RID: 30407
			private List<ArmorSetBonus.Builder.Parts> _sets = new List<ArmorSetBonus.Builder.Parts>();

			// Token: 0x02000B12 RID: 2834
			private struct Parts
			{
				// Token: 0x04007916 RID: 30998
				public int Head;

				// Token: 0x04007917 RID: 30999
				public int Body;

				// Token: 0x04007918 RID: 31000
				public int Legs;
			}
		}
	}
}

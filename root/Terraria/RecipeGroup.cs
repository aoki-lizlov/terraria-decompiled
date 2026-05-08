using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria
{
	// Token: 0x0200003D RID: 61
	public class RecipeGroup
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x0012C015 File Offset: 0x0012A215
		private static Func<string> WithDefaultCombineFormat(string key)
		{
			LocalizedText text = Language.GetText(key);
			return () => RecipeGroup.DefaultCombineFormat.Format(text);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0012C033 File Offset: 0x0012A233
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x0012C03B File Offset: 0x0012A23B
		public int RegisteredId
		{
			[CompilerGenerated]
			get
			{
				return this.<RegisteredId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RegisteredId>k__BackingField = value;
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0012C044 File Offset: 0x0012A244
		public RecipeGroup(string groupDescriptorKey, params int[] validItems)
			: this(RecipeGroup.WithDefaultCombineFormat(groupDescriptorKey), validItems)
		{
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0012C054 File Offset: 0x0012A254
		public RecipeGroup(Func<string> getName, params int[] validItems)
		{
			this.RegisteredId = -1;
			this.GetText = getName;
			foreach (int num in validItems)
			{
				this.Add(num, null);
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0012C0A8 File Offset: 0x0012A2A8
		public RecipeGroup Add(int itemID, Func<bool> isPreferred = null)
		{
			this.ValidItems.Add(itemID);
			this.Items.Add(itemID);
			return this;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0012C0C4 File Offset: 0x0012A2C4
		internal void SortDecraftingEntries()
		{
			this.DecraftItemId = this.Items.OrderBy((int e) => ContentSamples.ItemsByType[e].value).First<int>();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0012C0FB File Offset: 0x0012A2FB
		public override string ToString()
		{
			return this.GetText();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0012C108 File Offset: 0x0012A308
		public RecipeGroup Register()
		{
			if (this.RegisteredId >= 0)
			{
				throw new Exception("Already registered");
			}
			int num = RecipeGroup.nextRecipeGroupIndex++;
			this.RegisteredId = num;
			RecipeGroup.recipeGroups.Add(num, this);
			return this;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0012C14C File Offset: 0x0012A34C
		public int CountUsableItems(Dictionary<int, int> itemStacksAvailable)
		{
			int num = 0;
			foreach (int num2 in this.ValidItems)
			{
				int num3;
				if (itemStacksAvailable.TryGetValue(num2, out num3))
				{
					num += num3;
				}
			}
			return num;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0012C1AC File Offset: 0x0012A3AC
		public int GetGroupFakeItemId()
		{
			return this.RegisteredId + RecipeGroup.FakeItemIdOffset;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0012C1BA File Offset: 0x0012A3BA
		public bool Contains(int itemType)
		{
			return this.ValidItems.Contains(itemType);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0012C1C8 File Offset: 0x0012A3C8
		public int GetPlaceholderItemType()
		{
			return this.Items[0];
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0012C1D6 File Offset: 0x0012A3D6
		// Note: this type is marked as 'beforefieldinit'.
		static RecipeGroup()
		{
		}

		// Token: 0x040002EA RID: 746
		public static readonly int FakeItemIdOffset = 1000000;

		// Token: 0x040002EB RID: 747
		public static LocalizedText DefaultCombineFormat = Language.GetText("CombineFormat.RecipeGroup");

		// Token: 0x040002EC RID: 748
		[CompilerGenerated]
		private int <RegisteredId>k__BackingField;

		// Token: 0x040002ED RID: 749
		public Func<string> GetText;

		// Token: 0x040002EE RID: 750
		public HashSet<int> ValidItems = new HashSet<int>();

		// Token: 0x040002EF RID: 751
		public List<int> Items = new List<int>();

		// Token: 0x040002F0 RID: 752
		public int DecraftItemId;

		// Token: 0x040002F1 RID: 753
		public static Dictionary<int, RecipeGroup> recipeGroups = new Dictionary<int, RecipeGroup>();

		// Token: 0x040002F2 RID: 754
		public static int nextRecipeGroupIndex;

		// Token: 0x0200060E RID: 1550
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x06003BE9 RID: 15337 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x06003BEA RID: 15338 RVA: 0x0065CA15 File Offset: 0x0065AC15
			internal string <WithDefaultCombineFormat>b__0()
			{
				return RecipeGroup.DefaultCombineFormat.Format(this.text);
			}

			// Token: 0x0400644A RID: 25674
			public LocalizedText text;
		}

		// Token: 0x0200060F RID: 1551
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003BEB RID: 15339 RVA: 0x0065CA27 File Offset: 0x0065AC27
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003BEC RID: 15340 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003BED RID: 15341 RVA: 0x0065CA33 File Offset: 0x0065AC33
			internal int <SortDecraftingEntries>b__14_0(int e)
			{
				return ContentSamples.ItemsByType[e].value;
			}

			// Token: 0x0400644B RID: 25675
			public static readonly RecipeGroup.<>c <>9 = new RecipeGroup.<>c();

			// Token: 0x0400644C RID: 25676
			public static Func<int, int> <>9__14_0;
		}
	}
}

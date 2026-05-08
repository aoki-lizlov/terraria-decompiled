using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200054F RID: 1359
	public class RecipeItemCreationContext : ItemCreationContext
	{
		// Token: 0x06003789 RID: 14217 RVA: 0x0062F69B File Offset: 0x0062D89B
		public RecipeItemCreationContext(Recipe recipe)
		{
			this.Recipe = recipe;
		}

		// Token: 0x04005BBB RID: 23483
		public readonly Recipe Recipe;
	}
}

using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000046 RID: 70
	public interface IRailAssets : IRailComponent
	{
		// Token: 0x060015E7 RID: 5607
		RailResult AsyncRequestAllAssets(string user_data);

		// Token: 0x060015E8 RID: 5608
		RailResult QueryAssetInfo(ulong asset_id, RailAssetInfo asset_info);

		// Token: 0x060015E9 RID: 5609
		RailResult AsyncUpdateAssetsProperty(List<RailAssetProperty> asset_property_list, string user_data);

		// Token: 0x060015EA RID: 5610
		RailResult AsyncDirectConsumeAssets(List<RailAssetItem> assets, string user_data);

		// Token: 0x060015EB RID: 5611
		RailResult AsyncStartConsumeAsset(ulong asset_id, string user_data);

		// Token: 0x060015EC RID: 5612
		RailResult AsyncUpdateConsumeProgress(ulong asset_id, string progress, string user_data);

		// Token: 0x060015ED RID: 5613
		RailResult AsyncCompleteConsumeAsset(ulong asset_id, uint quantity, string user_data);

		// Token: 0x060015EE RID: 5614
		RailResult AsyncExchangeAssets(List<RailAssetItem> old_assets, RailProductItem to_product_info, string user_data);

		// Token: 0x060015EF RID: 5615
		RailResult AsyncExchangeAssetsTo(List<RailAssetItem> old_assets, RailProductItem to_product_info, ulong add_to_exist_assets, string user_data);

		// Token: 0x060015F0 RID: 5616
		RailResult AsyncSplitAsset(ulong source_asset, uint to_quantity, string user_data);

		// Token: 0x060015F1 RID: 5617
		RailResult AsyncSplitAssetTo(ulong source_asset, uint to_quantity, ulong add_to_asset, string user_data);

		// Token: 0x060015F2 RID: 5618
		RailResult AsyncMergeAsset(List<RailAssetItem> source_assets, string user_data);

		// Token: 0x060015F3 RID: 5619
		RailResult AsyncMergeAssetTo(List<RailAssetItem> source_assets, ulong add_to_asset, string user_data);
	}
}

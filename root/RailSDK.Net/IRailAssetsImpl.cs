using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200000A RID: 10
	public class IRailAssetsImpl : RailObject, IRailAssets, IRailComponent
	{
		// Token: 0x060010DF RID: 4319 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailAssetsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000022F4 File Offset: 0x000004F4
		~IRailAssetsImpl()
		{
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0000231C File Offset: 0x0000051C
		public virtual RailResult AsyncRequestAllAssets(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncRequestAllAssets(this.swigCPtr_, user_data);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0000232C File Offset: 0x0000052C
		public virtual RailResult QueryAssetInfo(ulong asset_id, RailAssetInfo asset_info)
		{
			IntPtr intPtr = ((asset_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailAssetInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_QueryAssetInfo(this.swigCPtr_, asset_id, intPtr);
			}
			finally
			{
				if (asset_info != null)
				{
					RailConverter.Cpp2Csharp(intPtr, asset_info);
				}
				RAIL_API_PINVOKE.delete_RailAssetInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0000237C File Offset: 0x0000057C
		public virtual RailResult AsyncUpdateAssetsProperty(List<RailAssetProperty> asset_property_list, string user_data)
		{
			IntPtr intPtr = ((asset_property_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetProperty__SWIG_0());
			if (asset_property_list != null)
			{
				RailConverter.Csharp2Cpp(asset_property_list, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncUpdateAssetsProperty(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetProperty(intPtr);
			}
			return railResult;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000023CC File Offset: 0x000005CC
		public virtual RailResult AsyncDirectConsumeAssets(List<RailAssetItem> assets, string user_data)
		{
			IntPtr intPtr = ((assets == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0());
			if (assets != null)
			{
				RailConverter.Csharp2Cpp(assets, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncDirectConsumeAssets(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(intPtr);
			}
			return railResult;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0000241C File Offset: 0x0000061C
		public virtual RailResult AsyncStartConsumeAsset(ulong asset_id, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncStartConsumeAsset(this.swigCPtr_, asset_id, user_data);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0000242B File Offset: 0x0000062B
		public virtual RailResult AsyncUpdateConsumeProgress(ulong asset_id, string progress, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncUpdateConsumeProgress(this.swigCPtr_, asset_id, progress, user_data);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0000243B File Offset: 0x0000063B
		public virtual RailResult AsyncCompleteConsumeAsset(ulong asset_id, uint quantity, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncCompleteConsumeAsset(this.swigCPtr_, asset_id, quantity, user_data);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0000244C File Offset: 0x0000064C
		public virtual RailResult AsyncExchangeAssets(List<RailAssetItem> old_assets, RailProductItem to_product_info, string user_data)
		{
			IntPtr intPtr = ((old_assets == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0());
			if (old_assets != null)
			{
				RailConverter.Csharp2Cpp(old_assets, intPtr);
			}
			IntPtr intPtr2 = ((to_product_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailProductItem__SWIG_0());
			if (to_product_info != null)
			{
				RailConverter.Csharp2Cpp(to_product_info, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncExchangeAssets(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(intPtr);
				RAIL_API_PINVOKE.delete_RailProductItem(intPtr2);
			}
			return railResult;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000024C0 File Offset: 0x000006C0
		public virtual RailResult AsyncExchangeAssetsTo(List<RailAssetItem> old_assets, RailProductItem to_product_info, ulong add_to_exist_assets, string user_data)
		{
			IntPtr intPtr = ((old_assets == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0());
			if (old_assets != null)
			{
				RailConverter.Csharp2Cpp(old_assets, intPtr);
			}
			IntPtr intPtr2 = ((to_product_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailProductItem__SWIG_0());
			if (to_product_info != null)
			{
				RailConverter.Csharp2Cpp(to_product_info, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncExchangeAssetsTo(this.swigCPtr_, intPtr, intPtr2, add_to_exist_assets, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(intPtr);
				RAIL_API_PINVOKE.delete_RailProductItem(intPtr2);
			}
			return railResult;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00002534 File Offset: 0x00000734
		public virtual RailResult AsyncSplitAsset(ulong source_asset, uint to_quantity, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncSplitAsset(this.swigCPtr_, source_asset, to_quantity, user_data);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00002544 File Offset: 0x00000744
		public virtual RailResult AsyncSplitAssetTo(ulong source_asset, uint to_quantity, ulong add_to_asset, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncSplitAssetTo(this.swigCPtr_, source_asset, to_quantity, add_to_asset, user_data);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00002558 File Offset: 0x00000758
		public virtual RailResult AsyncMergeAsset(List<RailAssetItem> source_assets, string user_data)
		{
			IntPtr intPtr = ((source_assets == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0());
			if (source_assets != null)
			{
				RailConverter.Csharp2Cpp(source_assets, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncMergeAsset(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(intPtr);
			}
			return railResult;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000025A8 File Offset: 0x000007A8
		public virtual RailResult AsyncMergeAssetTo(List<RailAssetItem> source_assets, ulong add_to_asset, string user_data)
		{
			IntPtr intPtr = ((source_assets == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0());
			if (source_assets != null)
			{
				RailConverter.Csharp2Cpp(source_assets, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncMergeAssetTo(this.swigCPtr_, intPtr, add_to_asset, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(intPtr);
			}
			return railResult;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

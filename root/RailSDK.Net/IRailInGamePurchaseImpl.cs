using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200001D RID: 29
	public class IRailInGamePurchaseImpl : RailObject, IRailInGamePurchase
	{
		// Token: 0x060011F9 RID: 4601 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailInGamePurchaseImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00004C80 File Offset: 0x00002E80
		~IRailInGamePurchaseImpl()
		{
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public virtual RailResult AsyncRequestAllPurchasableProducts(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_AsyncRequestAllPurchasableProducts(this.swigCPtr_, user_data);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00004CB6 File Offset: 0x00002EB6
		public virtual RailResult AsyncRequestAllProducts(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_AsyncRequestAllProducts(this.swigCPtr_, user_data);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00004CC4 File Offset: 0x00002EC4
		public virtual RailResult GetProductInfo(uint product_id, RailPurchaseProductInfo product)
		{
			IntPtr intPtr = ((product == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailPurchaseProductInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_GetProductInfo(this.swigCPtr_, product_id, intPtr);
			}
			finally
			{
				if (product != null)
				{
					RailConverter.Cpp2Csharp(intPtr, product);
				}
				RAIL_API_PINVOKE.delete_RailPurchaseProductInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00004D14 File Offset: 0x00002F14
		public virtual RailResult AsyncPurchaseProducts(List<RailProductItem> cart_items, string user_data)
		{
			IntPtr intPtr = ((cart_items == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailProductItem__SWIG_0());
			if (cart_items != null)
			{
				RailConverter.Csharp2Cpp(cart_items, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_AsyncPurchaseProducts(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailProductItem(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00004D64 File Offset: 0x00002F64
		public virtual RailResult AsyncFinishOrder(string order_id, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_AsyncFinishOrder(this.swigCPtr_, order_id, user_data);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00004D74 File Offset: 0x00002F74
		public virtual RailResult AsyncPurchaseProductsToAssets(List<RailProductItem> cart_items, string user_data)
		{
			IntPtr intPtr = ((cart_items == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailProductItem__SWIG_0());
			if (cart_items != null)
			{
				RailConverter.Csharp2Cpp(cart_items, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_AsyncPurchaseProductsToAssets(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailProductItem(intPtr);
			}
			return railResult;
		}
	}
}

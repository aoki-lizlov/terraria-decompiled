using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000036 RID: 54
	public class IRailZoneServerImpl : RailObject, IRailZoneServer, IRailComponent
	{
		// Token: 0x0600135E RID: 4958 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailZoneServerImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00008828 File Offset: 0x00006A28
		~IRailZoneServerImpl()
		{
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00008850 File Offset: 0x00006A50
		public virtual RailZoneID GetZoneID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailZoneServer_GetZoneID(this.swigCPtr_);
			RailZoneID railZoneID = new RailZoneID();
			RailConverter.Cpp2Csharp(intPtr, railZoneID);
			return railZoneID;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00008878 File Offset: 0x00006A78
		public virtual RailResult GetZoneNameLanguages(List<string> languages)
		{
			IntPtr intPtr = ((languages == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetZoneNameLanguages(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (languages != null)
				{
					RailConverter.Cpp2Csharp(intPtr, languages);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000088C8 File Offset: 0x00006AC8
		public virtual RailResult GetZoneName(string language_filter, out string zone_name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetZoneName(this.swigCPtr_, language_filter, intPtr);
			}
			finally
			{
				zone_name = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0000890C File Offset: 0x00006B0C
		public virtual RailResult GetZoneDescriptionLanguages(List<string> languages)
		{
			IntPtr intPtr = ((languages == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetZoneDescriptionLanguages(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (languages != null)
				{
					RailConverter.Cpp2Csharp(intPtr, languages);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0000895C File Offset: 0x00006B5C
		public virtual RailResult GetZoneDescription(string language_filter, out string zone_description)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetZoneDescription(this.swigCPtr_, language_filter, intPtr);
			}
			finally
			{
				zone_description = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x000089A0 File Offset: 0x00006BA0
		public virtual RailResult GetGameServerAddresses(List<string> server_addresses)
		{
			IntPtr intPtr = ((server_addresses == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetGameServerAddresses(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (server_addresses != null)
				{
					RailConverter.Cpp2Csharp(intPtr, server_addresses);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000089F0 File Offset: 0x00006BF0
		public virtual RailResult GetZoneMetadatas(List<RailKeyValue> key_values)
		{
			IntPtr intPtr = ((key_values == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetZoneMetadatas(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (key_values != null)
				{
					RailConverter.Cpp2Csharp(intPtr, key_values);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00008A40 File Offset: 0x00006C40
		public virtual RailResult GetChildrenZoneIDs(List<RailZoneID> zone_ids)
		{
			IntPtr intPtr = ((zone_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailZoneID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetChildrenZoneIDs(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (zone_ids != null)
				{
					RailConverter.Cpp2Csharp(intPtr, zone_ids);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailZoneID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00008A90 File Offset: 0x00006C90
		public virtual bool IsZoneVisiable()
		{
			return RAIL_API_PINVOKE.IRailZoneServer_IsZoneVisiable(this.swigCPtr_);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00008A9D File Offset: 0x00006C9D
		public virtual bool IsZoneJoinable()
		{
			return RAIL_API_PINVOKE.IRailZoneServer_IsZoneJoinable(this.swigCPtr_);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00008AAA File Offset: 0x00006CAA
		public virtual uint GetZoneEnableStartTime()
		{
			return RAIL_API_PINVOKE.IRailZoneServer_GetZoneEnableStartTime(this.swigCPtr_);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00008AB7 File Offset: 0x00006CB7
		public virtual uint GetZoneEnableEndTime()
		{
			return RAIL_API_PINVOKE.IRailZoneServer_GetZoneEnableEndTime(this.swigCPtr_);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

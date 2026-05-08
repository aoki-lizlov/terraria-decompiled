using System;

namespace rail
{
	// Token: 0x02000024 RID: 36
	public class IRailPlayerStatsImpl : RailObject, IRailPlayerStats, IRailComponent
	{
		// Token: 0x0600124D RID: 4685 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailPlayerStatsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00005C34 File Offset: 0x00003E34
		~IRailPlayerStatsImpl()
		{
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00005C5C File Offset: 0x00003E5C
		public virtual RailID GetRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailPlayerStats_GetRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00005C81 File Offset: 0x00003E81
		public virtual RailResult AsyncRequestStats(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_AsyncRequestStats(this.swigCPtr_, user_data);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00005C8F File Offset: 0x00003E8F
		public virtual RailResult GetStatValue(string name, out int data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_GetStatValue__SWIG_0(this.swigCPtr_, name, out data);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00005C9E File Offset: 0x00003E9E
		public virtual RailResult GetStatValue(string name, out double data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_GetStatValue__SWIG_1(this.swigCPtr_, name, out data);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00005CAD File Offset: 0x00003EAD
		public virtual RailResult SetStatValue(string name, int data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_SetStatValue__SWIG_0(this.swigCPtr_, name, data);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00005CBC File Offset: 0x00003EBC
		public virtual RailResult SetStatValue(string name, double data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_SetStatValue__SWIG_1(this.swigCPtr_, name, data);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00005CCB File Offset: 0x00003ECB
		public virtual RailResult UpdateAverageStatValue(string name, double data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_UpdateAverageStatValue(this.swigCPtr_, name, data);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00005CDA File Offset: 0x00003EDA
		public virtual RailResult AsyncStoreStats(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_AsyncStoreStats(this.swigCPtr_, user_data);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00005CE8 File Offset: 0x00003EE8
		public virtual RailResult ResetAllStats()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_ResetAllStats(this.swigCPtr_);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

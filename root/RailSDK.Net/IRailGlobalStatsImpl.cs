using System;

namespace rail
{
	// Token: 0x02000018 RID: 24
	public class IRailGlobalStatsImpl : RailObject, IRailGlobalStats, IRailComponent
	{
		// Token: 0x060011D0 RID: 4560 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailGlobalStatsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000048C0 File Offset: 0x00002AC0
		~IRailGlobalStatsImpl()
		{
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000048E8 File Offset: 0x00002AE8
		public virtual RailResult AsyncRequestGlobalStats(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalStats_AsyncRequestGlobalStats(this.swigCPtr_, user_data);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000048F6 File Offset: 0x00002AF6
		public virtual RailResult GetGlobalStatValue(string name, out long data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValue__SWIG_0(this.swigCPtr_, name, out data);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00004905 File Offset: 0x00002B05
		public virtual RailResult GetGlobalStatValue(string name, out double data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValue__SWIG_1(this.swigCPtr_, name, out data);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00004914 File Offset: 0x00002B14
		public virtual RailResult GetGlobalStatValueHistory(string name, long[] global_stats_data, uint data_size, out int num_global_stats)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValueHistory__SWIG_0(this.swigCPtr_, name, global_stats_data, data_size, out num_global_stats);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00004926 File Offset: 0x00002B26
		public virtual RailResult GetGlobalStatValueHistory(string name, double[] global_stats_data, uint data_size, out int num_global_stats)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValueHistory__SWIG_1(this.swigCPtr_, name, global_stats_data, data_size, out num_global_stats);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

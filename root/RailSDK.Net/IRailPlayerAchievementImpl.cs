using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000023 RID: 35
	public class IRailPlayerAchievementImpl : RailObject, IRailPlayerAchievement, IRailComponent
	{
		// Token: 0x0600123D RID: 4669 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailPlayerAchievementImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00005ACC File Offset: 0x00003CCC
		~IRailPlayerAchievementImpl()
		{
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public virtual RailID GetRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailPlayerAchievement_GetRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00005B19 File Offset: 0x00003D19
		public virtual RailResult AsyncRequestAchievement(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncRequestAchievement(this.swigCPtr_, user_data);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00005B27 File Offset: 0x00003D27
		public virtual RailResult HasAchieved(string name, out bool achieved)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_HasAchieved(this.swigCPtr_, name, out achieved);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00005B38 File Offset: 0x00003D38
		public virtual RailResult GetAchievementInfo(string name, out string achievement_info)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_GetAchievementInfo(this.swigCPtr_, name, intPtr);
			}
			finally
			{
				achievement_info = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00005B7C File Offset: 0x00003D7C
		public virtual RailResult AsyncTriggerAchievementProgress(string name, uint current_value, uint max_value, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_0(this.swigCPtr_, name, current_value, max_value, user_data);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00005B8E File Offset: 0x00003D8E
		public virtual RailResult AsyncTriggerAchievementProgress(string name, uint current_value, uint max_value)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_1(this.swigCPtr_, name, current_value, max_value);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00005B9E File Offset: 0x00003D9E
		public virtual RailResult AsyncTriggerAchievementProgress(string name, uint current_value)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_2(this.swigCPtr_, name, current_value);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00005BAD File Offset: 0x00003DAD
		public virtual RailResult MakeAchievement(string name)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_MakeAchievement(this.swigCPtr_, name);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00005BBB File Offset: 0x00003DBB
		public virtual RailResult CancelAchievement(string name)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_CancelAchievement(this.swigCPtr_, name);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00005BC9 File Offset: 0x00003DC9
		public virtual RailResult AsyncStoreAchievement(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncStoreAchievement(this.swigCPtr_, user_data);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00005BD7 File Offset: 0x00003DD7
		public virtual RailResult ResetAllAchievements()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_ResetAllAchievements(this.swigCPtr_);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00005BE4 File Offset: 0x00003DE4
		public virtual RailResult GetAllAchievementsName(List<string> names)
		{
			IntPtr intPtr = ((names == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_GetAllAchievementsName(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (names != null)
				{
					RailConverter.Cpp2Csharp(intPtr, names);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

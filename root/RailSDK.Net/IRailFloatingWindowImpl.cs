using System;

namespace rail
{
	// Token: 0x02000012 RID: 18
	public class IRailFloatingWindowImpl : RailObject, IRailFloatingWindow
	{
		// Token: 0x06001156 RID: 4438 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailFloatingWindowImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000033E8 File Offset: 0x000015E8
		~IRailFloatingWindowImpl()
		{
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00003410 File Offset: 0x00001610
		public virtual RailResult AsyncShowRailFloatingWindow(EnumRailWindowType window_type, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_AsyncShowRailFloatingWindow(this.swigCPtr_, (int)window_type, user_data);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0000341F File Offset: 0x0000161F
		public virtual RailResult AsyncCloseRailFloatingWindow(EnumRailWindowType window_type, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_AsyncCloseRailFloatingWindow(this.swigCPtr_, (int)window_type, user_data);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00003430 File Offset: 0x00001630
		public virtual RailResult SetNotifyWindowPosition(EnumRailNotifyWindowType window_type, RailWindowLayout layout)
		{
			IntPtr intPtr = ((layout == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailWindowLayout__SWIG_0());
			if (layout != null)
			{
				RailConverter.Csharp2Cpp(layout, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_SetNotifyWindowPosition(this.swigCPtr_, (int)window_type, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailWindowLayout(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00003480 File Offset: 0x00001680
		public virtual RailResult AsyncShowStoreWindow(ulong id, RailStoreOptions options, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailStoreOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_AsyncShowStoreWindow(this.swigCPtr_, id, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailStoreOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000034D4 File Offset: 0x000016D4
		public virtual bool IsFloatingWindowAvailable()
		{
			return RAIL_API_PINVOKE.IRailFloatingWindow_IsFloatingWindowAvailable(this.swigCPtr_);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x000034E1 File Offset: 0x000016E1
		public virtual RailResult AsyncShowDefaultGameStoreWindow(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_AsyncShowDefaultGameStoreWindow(this.swigCPtr_, user_data);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000034EF File Offset: 0x000016EF
		public virtual RailResult SetNotifyWindowEnable(EnumRailNotifyWindowType window_type, bool enable)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_SetNotifyWindowEnable(this.swigCPtr_, (int)window_type, enable);
		}
	}
}

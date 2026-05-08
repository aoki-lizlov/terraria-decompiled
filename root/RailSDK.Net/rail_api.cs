using System;

namespace rail
{
	// Token: 0x02000044 RID: 68
	public class rail_api
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x0000F051 File Offset: 0x0000D251
		public static int RAIL_DEFAULT_MAX_ROOM_MEMBERS
		{
			get
			{
				return RAIL_API_PINVOKE.RAIL_DEFAULT_MAX_ROOM_MEMBERS_get();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0000F058 File Offset: 0x0000D258
		public static uint kRailMaxGameDefinePlayingStateValue
		{
			get
			{
				return RAIL_API_PINVOKE.kRailMaxGameDefinePlayingStateValue_get();
			}
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0000F060 File Offset: 0x0000D260
		public static bool RailNeedRestartAppForCheckingEnvironment(RailGameID game_id, int argc, string[] argv)
		{
			IntPtr intPtr = ((game_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGameID__SWIG_0());
			if (game_id != null)
			{
				RailConverter.Csharp2Cpp(game_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.RailNeedRestartAppForCheckingEnvironment(intPtr, argc, argv);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailGameID(intPtr);
			}
			return flag;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		public static bool RailInitialize()
		{
			return RAIL_API_PINVOKE.RailInitialize();
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0000F0BF File Offset: 0x0000D2BF
		public static void RailFinalize()
		{
			RAIL_API_PINVOKE.RailFinalize();
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0000F0C6 File Offset: 0x0000D2C6
		public static void RailFireEvents()
		{
			RAIL_API_PINVOKE.RailFireEvents();
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0000F0D0 File Offset: 0x0000D2D0
		public static IRailFactory RailFactory()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.RailFactory();
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFactoryImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0000F0F8 File Offset: 0x0000D2F8
		public static void RailGetSdkVersion(out string version, out string description)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			IntPtr intPtr2 = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			try
			{
				RAIL_API_PINVOKE.RailGetSdkVersion(intPtr, intPtr2);
			}
			finally
			{
				version = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
				description = RAIL_API_PINVOKE.RailString_c_str(intPtr2);
				RAIL_API_PINVOKE.delete_RailString(intPtr2);
			}
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00002119 File Offset: 0x00000319
		public rail_api()
		{
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0000F148 File Offset: 0x0000D348
		// Note: this type is marked as 'beforefieldinit'.
		static rail_api()
		{
		}

		// Token: 0x0400000D RID: 13
		public static readonly int USE_MANUAL_ALLOC = RAIL_API_PINVOKE.USE_MANUAL_ALLOC_get();

		// Token: 0x0400000E RID: 14
		public static readonly int RAIL_SDK_PACKING = RAIL_API_PINVOKE.RAIL_SDK_PACKING_get();
	}
}

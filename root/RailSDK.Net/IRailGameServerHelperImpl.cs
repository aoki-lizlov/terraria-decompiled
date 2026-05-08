using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000016 RID: 22
	public class IRailGameServerHelperImpl : RailObject, IRailGameServerHelper
	{
		// Token: 0x060011BB RID: 4539 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailGameServerHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00004444 File Offset: 0x00002644
		~IRailGameServerHelperImpl()
		{
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0000446C File Offset: 0x0000266C
		public virtual RailResult AsyncGetGameServerPlayerList(RailID gameserver_rail_id, string user_data)
		{
			IntPtr intPtr = ((gameserver_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (gameserver_rail_id != null)
			{
				RailConverter.Csharp2Cpp(gameserver_rail_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetGameServerPlayerList(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000044C8 File Offset: 0x000026C8
		public virtual RailResult AsyncGetGameServerList(uint start_index, uint end_index, List<GameServerListFilter> alternative_filters, List<GameServerListSorter> sorter, string user_data)
		{
			IntPtr intPtr = ((alternative_filters == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerListFilter__SWIG_0());
			if (alternative_filters != null)
			{
				RailConverter.Csharp2Cpp(alternative_filters, intPtr);
			}
			IntPtr intPtr2 = ((sorter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerListSorter__SWIG_0());
			if (sorter != null)
			{
				RailConverter.Csharp2Cpp(sorter, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetGameServerList(this.swigCPtr_, start_index, end_index, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayGameServerListFilter(intPtr);
				RAIL_API_PINVOKE.delete_RailArrayGameServerListSorter(intPtr2);
			}
			return railResult;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00004540 File Offset: 0x00002740
		public virtual IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options, string game_server_name, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateGameServerOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailGameServer railGameServer;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_0(this.swigCPtr_, intPtr, game_server_name, user_data);
				railGameServer = ((intPtr2 == IntPtr.Zero) ? null : new IRailGameServerImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateGameServerOptions(intPtr);
			}
			return railGameServer;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x000045A8 File Offset: 0x000027A8
		public virtual IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options, string game_server_name)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateGameServerOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailGameServer railGameServer;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_1(this.swigCPtr_, intPtr, game_server_name);
				railGameServer = ((intPtr2 == IntPtr.Zero) ? null : new IRailGameServerImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateGameServerOptions(intPtr);
			}
			return railGameServer;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00004610 File Offset: 0x00002810
		public virtual IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateGameServerOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailGameServer railGameServer;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_2(this.swigCPtr_, intPtr);
				railGameServer = ((intPtr2 == IntPtr.Zero) ? null : new IRailGameServerImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateGameServerOptions(intPtr);
			}
			return railGameServer;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00004678 File Offset: 0x00002878
		public virtual IRailGameServer AsyncCreateGameServer()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_3(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGameServerImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000046A6 File Offset: 0x000028A6
		public virtual RailResult AsyncGetFavoriteGameServers(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_0(this.swigCPtr_, user_data);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000046B4 File Offset: 0x000028B4
		public virtual RailResult AsyncGetFavoriteGameServers()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_1(this.swigCPtr_);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x000046C4 File Offset: 0x000028C4
		public virtual RailResult AsyncAddFavoriteGameServer(RailID game_server_id, string user_data)
		{
			IntPtr intPtr = ((game_server_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (game_server_id != null)
			{
				RailConverter.Csharp2Cpp(game_server_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_0(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00004720 File Offset: 0x00002920
		public virtual RailResult AsyncAddFavoriteGameServer(RailID game_server_id)
		{
			IntPtr intPtr = ((game_server_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (game_server_id != null)
			{
				RailConverter.Csharp2Cpp(game_server_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_1(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0000477C File Offset: 0x0000297C
		public virtual RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id, string user_Data)
		{
			IntPtr intPtr = ((game_server_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (game_server_id != null)
			{
				RailConverter.Csharp2Cpp(game_server_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_0(this.swigCPtr_, intPtr, user_Data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x000047D8 File Offset: 0x000029D8
		public virtual RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id)
		{
			IntPtr intPtr = ((game_server_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (game_server_id != null)
			{
				RailConverter.Csharp2Cpp(game_server_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_1(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}
	}
}

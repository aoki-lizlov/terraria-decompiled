using System;
using System.Collections.Generic;
using Steamworks;

namespace Terraria.Social.Steam
{
	// Token: 0x02000149 RID: 329
	public class Lobby
	{
		// Token: 0x06001CD8 RID: 7384 RVA: 0x004FFD20 File Offset: 0x004FDF20
		public Lobby()
		{
			this._lobbyEnter = CallResult<LobbyEnter_t>.Create(new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.OnLobbyEntered));
			this._lobbyCreated = CallResult<LobbyCreated_t>.Create(new CallResult<LobbyCreated_t>.APIDispatchDelegate(this.OnLobbyCreated));
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x004FFD94 File Offset: 0x004FDF94
		public void Create(bool inviteOnly, CallResult<LobbyCreated_t>.APIDispatchDelegate callResult)
		{
			SteamAPICall_t steamAPICall_t = SteamMatchmaking.CreateLobby(inviteOnly ? 0 : 1, 256);
			this._lobbyCreatedExternalCallback = callResult;
			this._lobbyCreated.Set(steamAPICall_t, null);
			this.State = LobbyState.Creating;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x004FFDCE File Offset: 0x004FDFCE
		public void OpenInviteOverlay()
		{
			if (this.State == LobbyState.Inactive)
			{
				SteamFriends.ActivateGameOverlayInviteDialog(new CSteamID(Main.LobbyId));
				return;
			}
			SteamFriends.ActivateGameOverlayInviteDialog(this.Id);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x004FFDF4 File Offset: 0x004FDFF4
		public void Join(CSteamID lobbyId, CallResult<LobbyEnter_t>.APIDispatchDelegate callResult)
		{
			if (this.State != LobbyState.Inactive)
			{
				return;
			}
			this.State = LobbyState.Connecting;
			this._lobbyEnterExternalCallback = callResult;
			SteamAPICall_t steamAPICall_t = SteamMatchmaking.JoinLobby(lobbyId);
			this._lobbyEnter.Set(steamAPICall_t, null);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x004FFE2C File Offset: 0x004FE02C
		public byte[] GetMessage(int index)
		{
			CSteamID csteamID;
			EChatEntryType echatEntryType;
			int lobbyChatEntry = SteamMatchmaking.GetLobbyChatEntry(this.Id, index, ref csteamID, this._messageBuffer, this._messageBuffer.Length, ref echatEntryType);
			byte[] array = new byte[lobbyChatEntry];
			Array.Copy(this._messageBuffer, array, lobbyChatEntry);
			return array;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x004FFE6D File Offset: 0x004FE06D
		public int GetUserCount()
		{
			return SteamMatchmaking.GetNumLobbyMembers(this.Id);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x004FFE7A File Offset: 0x004FE07A
		public CSteamID GetUserByIndex(int index)
		{
			return SteamMatchmaking.GetLobbyMemberByIndex(this.Id, index);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x004FFE88 File Offset: 0x004FE088
		public bool SendMessage(byte[] data)
		{
			return this.SendMessage(data, data.Length);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x004FFE94 File Offset: 0x004FE094
		public bool SendMessage(byte[] data, int length)
		{
			return this.State == LobbyState.Active && SteamMatchmaking.SendLobbyChatMsg(this.Id, data, length);
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x004FFEAE File Offset: 0x004FE0AE
		public void Set(CSteamID lobbyId)
		{
			this.Id = lobbyId;
			this.State = LobbyState.Active;
			this.Owner = SteamMatchmaking.GetLobbyOwner(lobbyId);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x004FFECA File Offset: 0x004FE0CA
		public void SetPlayedWith(CSteamID userId)
		{
			if (this._usersSeen.Contains(userId))
			{
				return;
			}
			SteamFriends.SetPlayedWith(userId);
			this._usersSeen.Add(userId);
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x004FFEEE File Offset: 0x004FE0EE
		public void Leave()
		{
			if (this.State == LobbyState.Active)
			{
				SteamMatchmaking.LeaveLobby(this.Id);
			}
			this.State = LobbyState.Inactive;
			this._usersSeen.Clear();
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x004FFF18 File Offset: 0x004FE118
		private void OnLobbyEntered(LobbyEnter_t result, bool failure)
		{
			if (this.State != LobbyState.Connecting)
			{
				return;
			}
			if (failure)
			{
				this.State = LobbyState.Inactive;
			}
			else
			{
				this.State = LobbyState.Active;
			}
			this.Id = new CSteamID(result.m_ulSteamIDLobby);
			this.Owner = SteamMatchmaking.GetLobbyOwner(this.Id);
			this._lobbyEnterExternalCallback.Invoke(result, failure);
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x004FFF74 File Offset: 0x004FE174
		private void OnLobbyCreated(LobbyCreated_t result, bool failure)
		{
			if (this.State != LobbyState.Creating)
			{
				return;
			}
			if (failure)
			{
				this.State = LobbyState.Inactive;
			}
			else
			{
				this.State = LobbyState.Active;
			}
			this.Id = new CSteamID(result.m_ulSteamIDLobby);
			this.Owner = SteamMatchmaking.GetLobbyOwner(this.Id);
			this._lobbyCreatedExternalCallback.Invoke(result, failure);
		}

		// Token: 0x040015FE RID: 5630
		private HashSet<CSteamID> _usersSeen = new HashSet<CSteamID>();

		// Token: 0x040015FF RID: 5631
		private byte[] _messageBuffer = new byte[1024];

		// Token: 0x04001600 RID: 5632
		public CSteamID Id = CSteamID.Nil;

		// Token: 0x04001601 RID: 5633
		public CSteamID Owner = CSteamID.Nil;

		// Token: 0x04001602 RID: 5634
		public LobbyState State;

		// Token: 0x04001603 RID: 5635
		private CallResult<LobbyEnter_t> _lobbyEnter;

		// Token: 0x04001604 RID: 5636
		private CallResult<LobbyEnter_t>.APIDispatchDelegate _lobbyEnterExternalCallback;

		// Token: 0x04001605 RID: 5637
		private CallResult<LobbyCreated_t> _lobbyCreated;

		// Token: 0x04001606 RID: 5638
		private CallResult<LobbyCreated_t>.APIDispatchDelegate _lobbyCreatedExternalCallback;
	}
}

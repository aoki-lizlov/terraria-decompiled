using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using FullSerializer;
using Newtonsoft.Json.Linq;
using ReLogic.OS;
using SteelSeries.GameSense;

namespace ReLogic.Peripherals.RGB.SteelSeries
{
	// Token: 0x02000057 RID: 87
	public class GameSenseConnection
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x0000831E File Offset: 0x0000651E
		public void SetEvents(params Bind_Event[] bindEvents)
		{
			this.Events = bindEvents;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008328 File Offset: 0x00006528
		public void BeginConnection()
		{
			this._mSerializer = new fsSerializer();
			this._mMsgQueue = new LocklessQueue<QueueMsg>(100U);
			this._gameSenseThread = new Thread(new ThreadStart(this._gamesenseWrk));
			this._mGameSenseWrkShouldRun = true;
			this._setClientState(ClientState.Probing);
			try
			{
				this._gameSenseThread.Start();
				this._addGUIDefinedEvents();
			}
			catch (Exception ex)
			{
				GameSenseConnection._logException("Could not start the client thread", ex);
				this._setClientState(ClientState.Inactive);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000083AC File Offset: 0x000065AC
		private void _addGUIDefinedEvents()
		{
			if (this.GameName == null || this.GameDisplayName == null || this.Events == null)
			{
				GameSenseConnection._logWarning("Incomplete game registration form");
				this._setClientState(ClientState.Inactive);
				return;
			}
			this.RegisterGame(this.GameName, this.GameDisplayName, this.IconColor);
			this.RegisterEvents(this.Events);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008407 File Offset: 0x00006607
		public void TryRegisteringEvents(Bind_Event[] theEvents)
		{
			this.RegisterEvents(theEvents);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008410 File Offset: 0x00006610
		private void RegisterEvents(Bind_Event[] theEvents)
		{
			foreach (Bind_Event bind_Event in theEvents)
			{
				QueueMsg queueMsg;
				if (bind_Event.handlers == null || bind_Event.handlers.Length == 0)
				{
					queueMsg = new QueueMsgRegisterEvent();
					queueMsg.data = new Register_Event(this.GameName, bind_Event.eventName, bind_Event.minValue, bind_Event.maxValue, bind_Event.iconId);
				}
				else
				{
					bind_Event.game = this.GameName;
					queueMsg = new QueueMsgBindEvent();
					queueMsg.data = bind_Event;
				}
				this._mMsgQueue.PEnqueue(queueMsg);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000849C File Offset: 0x0000669C
		public void RegisterGame(string name, string displayName, IconColor iconColor)
		{
			this.GameName = name.ToUpper();
			this.GameDisplayName = displayName;
			this.IconColor = iconColor;
			Register_Game register_Game = new Register_Game();
			register_Game.game = this.GameName;
			register_Game.game_display_name = this.GameDisplayName;
			register_Game.icon_color_id = iconColor;
			QueueMsgRegisterGame queueMsgRegisterGame = new QueueMsgRegisterGame();
			queueMsgRegisterGame.data = register_Game;
			this._mMsgQueue.PEnqueue(queueMsgRegisterGame);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008504 File Offset: 0x00006704
		public void RemoveGame(string name)
		{
			this.GameName = name.ToUpper();
			QueueMsgRemoveGame queueMsgRemoveGame = new QueueMsgRemoveGame();
			queueMsgRemoveGame.data = new Game(this.GameName);
			this._mMsgQueue.PEnqueue(queueMsgRemoveGame);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008541 File Offset: 0x00006741
		public void EndConnection()
		{
			GameSenseConnection._logDbgMsg("Ending Connection");
			this._mGameSenseWrkShouldRun = false;
			if (this._currentRequest != null)
			{
				this._currentRequest.Abort();
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008568 File Offset: 0x00006768
		private void _gamesenseWrk()
		{
			QueueMsg queueMsg = null;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (this._mGameSenseWrkShouldRun)
			{
				switch (this._mClientState)
				{
				case ClientState.Active:
				{
					QueueMsg queueMsg2;
					while ((queueMsg2 = this._mMsgQueue.CDequeue()) == null)
					{
						Thread.Sleep(10);
						if (stopwatch.ElapsedMilliseconds > 1000L)
						{
							queueMsg2 = new QueueMsgSendHeartbeat();
							queueMsg2.data = new Game(this.GameName);
							break;
						}
					}
					try
					{
						this._sendMsg(queueMsg2);
						stopwatch.Reset();
						stopwatch.Start();
						continue;
					}
					catch (ServerDownException ex)
					{
						GameSenseConnection._logException("Failed connecting to GameSense server", ex);
						queueMsg = queueMsg2;
						this._setClientState(ClientState.Probing);
						continue;
					}
					catch (CriticalMessageIllFormedException ex2)
					{
						GameSenseConnection._logException("Message ill-formed", ex2);
						this._setClientState(ClientState.Inactive);
						continue;
					}
					catch (Exception ex3)
					{
						GameSenseConnection._logException("Failed processing msg", ex3);
						continue;
					}
					break;
				}
				case ClientState.Probing:
					break;
				case ClientState.Inactive:
					GameSenseConnection._logDbgMsg("Entering inactive state");
					this._mGameSenseWrkShouldRun = false;
					continue;
				default:
					GameSenseConnection._logErrorMsg("Unknown GameSense client state");
					this._setClientState(ClientState.Inactive);
					continue;
				}
				this._mServerPort = GameSenseConnection._getServerPort();
				if (this._mServerPort == null)
				{
					GameSenseConnection._logWarning("Failed to obtain GameSense server port. GameSense will not function");
					this._setClientState(ClientState.Inactive);
				}
				else
				{
					this._initializeUris();
					if (queueMsg != null)
					{
						try
						{
							this._sendMsg(queueMsg);
							queueMsg = null;
						}
						catch (ServerDownException ex4)
						{
							GameSenseConnection._logException("Failed connecting to GameSense server", ex4);
							GameSenseConnection._logDbgMsg("Retrying in 5 seconds...");
							Thread.Sleep(5000);
							continue;
						}
					}
					this._setClientState(ClientState.Active);
				}
			}
			GameSenseConnection._logDbgMsg("Worker exiting");
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000871C File Offset: 0x0000691C
		private void _setClientState(ClientState state)
		{
			if (this._mClientState == state)
			{
				return;
			}
			this._mClientState = state;
			if (state == ClientState.Active && this.OnConnectionBecameActive != null)
			{
				this.OnConnectionBecameActive();
			}
			if (state == ClientState.Inactive && this.OnConnectionBecameInactive != null)
			{
				this.OnConnectionBecameInactive();
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000046AD File Offset: 0x000028AD
		private static void _logException(string msg, Exception e)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000046AD File Offset: 0x000028AD
		private static void _logWarning(string msg)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000046AD File Offset: 0x000028AD
		private static void _logDbgMsg(string msg)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000046AD File Offset: 0x000028AD
		private static void _logErrorMsg(string msg)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000875C File Offset: 0x0000695C
		private void _sendMsg(QueueMsg msg)
		{
			string text = this._toJSON<object>(msg.data);
			JsonMsg jsonMsg = msg as JsonMsg;
			if (jsonMsg != null)
			{
				text = jsonMsg.JsonText;
			}
			GameSenseConnection._logDbgMsg(text);
			try
			{
				this._sendServer(msg.uri, text);
			}
			catch (WebException ex)
			{
				WebExceptionStatus status = ex.Status;
				if (status == 2)
				{
					throw new ServerDownException(ex.Message);
				}
				if (status != 7)
				{
					GameSenseConnection._logException("Unexpected status", ex);
					throw;
				}
				if (msg.IsCritical())
				{
					Stream responseStream = ex.Response.GetResponseStream();
					string text2 = new StreamReader(responseStream, Encoding.UTF8).ReadToEnd();
					responseStream.Close();
					throw new CriticalMessageIllFormedException(text2);
				}
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000880C File Offset: 0x00006A0C
		private void SendJson(Uri uri, string data, bool isCritical)
		{
			GameSenseConnection._logDbgMsg(data);
			try
			{
				this._sendServer(uri, data);
			}
			catch (WebException ex)
			{
				WebExceptionStatus status = ex.Status;
				if (status == 2)
				{
					throw new ServerDownException(ex.Message);
				}
				if (status != 7)
				{
					GameSenseConnection._logException("Unexpected status", ex);
					throw;
				}
				if (isCritical)
				{
					Stream responseStream = ex.Response.GetResponseStream();
					string text = new StreamReader(responseStream, Encoding.UTF8).ReadToEnd();
					responseStream.Close();
					throw new CriticalMessageIllFormedException(text);
				}
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008890 File Offset: 0x00006A90
		private static string _getPropsPath()
		{
			if (Platform.IsWindows)
			{
				return Environment.ExpandEnvironmentVariables("%PROGRAMDATA%/SteelSeries/SteelSeries Engine 3/coreProps.json");
			}
			return "/Library/Application Support/SteelSeries Engine 3/coreProps.json";
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000088AC File Offset: 0x00006AAC
		private static string _readProps()
		{
			string text = GameSenseConnection._getPropsPath();
			string text2 = null;
			try
			{
				if (File.Exists(text))
				{
					text2 = File.ReadAllText(text);
				}
				else
				{
					GameSenseConnection._logErrorMsg("Could not read server props file, because it can't be found");
				}
			}
			catch (Exception ex)
			{
				GameSenseConnection._logException("Could not read server props file", ex);
			}
			return text2;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008900 File Offset: 0x00006B00
		private static string _getServerPort()
		{
			string text = null;
			string text2 = GameSenseConnection._readProps();
			if (text2 != null)
			{
				try
				{
					JObject jobject = JObject.Parse(text2);
					string[] array = new coreProps
					{
						address = (string)jobject.GetValue("address")
					}.address.Split(new char[] { ':' });
					Convert.ToUInt16(array[1]);
					text = array[1];
				}
				catch (Exception ex)
				{
					GameSenseConnection._logException("Cannot parse port information", ex);
				}
			}
			return text;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008984 File Offset: 0x00006B84
		private void _initializeUris()
		{
			this._uriBase = new Uri("http://127.0.0.1:" + this._mServerPort);
			QueueMsgRegisterGame._uri = new Uri(this._uriBase, "game_metadata");
			QueueMsgBindEvent._uri = new Uri(this._uriBase, "bind_game_event");
			QueueMsgRegisterEvent._uri = new Uri(this._uriBase, "register_game_event");
			QueueMsgSendEvent._uri = new Uri(this._uriBase, "game_event");
			QueueMsgSendHeartbeat._uri = new Uri(this._uriBase, "game_heartbeat");
			QueueMsgRemoveGame._uri = new Uri(this._uriBase, "remove_game");
			JsonMsg._bitmapEventUri = new Uri(this._uriBase, "bitmap_event");
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008A40 File Offset: 0x00006C40
		[DebuggerNonUserCode]
		private void _sendServer(Uri uri, string data)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(data);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";
			Stream requestStream = httpWebRequest.GetRequestStream();
			requestStream.Write(bytes, 0, bytes.Length);
			requestStream.Close();
			this._currentRequest = httpWebRequest;
			try
			{
				((HttpWebResponse)httpWebRequest.GetResponse()).Close();
			}
			catch
			{
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008AC0 File Offset: 0x00006CC0
		private string _toJSON<T>(T obj)
		{
			fsData fsData;
			if (this._mSerializer.TrySerialize<T>(obj, out fsData).Succeeded)
			{
				return fsJsonPrinter.CompressedJson(fsData);
			}
			throw new Exception("Failed serializing object: " + obj.ToString());
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008B0E File Offset: 0x00006D0E
		private bool _isClientActive()
		{
			return this._mClientState == ClientState.Active;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008B19 File Offset: 0x00006D19
		public void SendEvent(string fullEventJson)
		{
			if (!this._isClientActive())
			{
				return;
			}
			this._mMsgQueue.PEnqueue(new JsonMsg
			{
				JsonText = fullEventJson
			});
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008B3C File Offset: 0x00006D3C
		public void SendEvent(string upperCaseEventName, int value)
		{
			if (!this._isClientActive())
			{
				return;
			}
			Send_Event send_Event = new Send_Event();
			send_Event.game = this.GameName;
			send_Event.event_name = upperCaseEventName;
			send_Event.data.value = value;
			QueueMsgSendEvent queueMsgSendEvent = new QueueMsgSendEvent();
			queueMsgSendEvent.data = send_Event;
			this._mMsgQueue.PEnqueue(queueMsgSendEvent);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008B94 File Offset: 0x00006D94
		public void RegisterEvent(string upperCaseEventName, int minValue = 0, int maxValue = 100, EventIconId iconId = EventIconId.Default)
		{
			if (!this._isClientActive())
			{
				return;
			}
			QueueMsgRegisterEvent queueMsgRegisterEvent = new QueueMsgRegisterEvent();
			queueMsgRegisterEvent.data = new Register_Event(this.GameName, upperCaseEventName, minValue, maxValue, iconId);
			this._mMsgQueue.PEnqueue(queueMsgRegisterEvent);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000448A File Offset: 0x0000268A
		public GameSenseConnection()
		{
		}

		// Token: 0x040002C4 RID: 708
		public string GameName;

		// Token: 0x040002C5 RID: 709
		public string GameDisplayName;

		// Token: 0x040002C6 RID: 710
		public IconColor IconColor;

		// Token: 0x040002C7 RID: 711
		private Bind_Event[] Events;

		// Token: 0x040002C8 RID: 712
		private const string _SceneObjName = "GameSenseManager_Auto";

		// Token: 0x040002C9 RID: 713
		private const string _GameSenseObjName = "GameSenseManager";

		// Token: 0x040002CA RID: 714
		private const uint _MsgQueueSize = 100U;

		// Token: 0x040002CB RID: 715
		private const int _ServerProbeInterval = 5000;

		// Token: 0x040002CC RID: 716
		private const int _MsgCheckInterval = 10;

		// Token: 0x040002CD RID: 717
		private const long _MaxIdleTimeBeforeHeartbeat = 1000L;

		// Token: 0x040002CE RID: 718
		private Thread _gameSenseThread;

		// Token: 0x040002CF RID: 719
		private bool _mGameSenseWrkShouldRun;

		// Token: 0x040002D0 RID: 720
		private Uri _uriBase;

		// Token: 0x040002D1 RID: 721
		private LocklessQueue<QueueMsg> _mMsgQueue;

		// Token: 0x040002D2 RID: 722
		private ClientState _mClientState;

		// Token: 0x040002D3 RID: 723
		private string _mServerPort;

		// Token: 0x040002D4 RID: 724
		private fsSerializer _mSerializer;

		// Token: 0x040002D5 RID: 725
		public GameSenseConnection.ClientStateEvent OnConnectionBecameActive;

		// Token: 0x040002D6 RID: 726
		public GameSenseConnection.ClientStateEvent OnConnectionBecameInactive;

		// Token: 0x040002D7 RID: 727
		private HttpWebRequest _currentRequest;

		// Token: 0x020000CC RID: 204
		// (Invoke) Token: 0x0600044C RID: 1100
		public delegate void ClientStateEvent();
	}
}

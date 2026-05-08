using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020001CF RID: 463
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class Exception : ISerializable, _Exception
	{
		// Token: 0x060015A4 RID: 5540 RVA: 0x00056CAF File Offset: 0x00054EAF
		private void Init()
		{
			this._message = null;
			this._stackTrace = null;
			this._dynamicMethods = null;
			this.HResult = -2146233088;
			this._safeSerializationManager = new SafeSerializationManager();
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00056CDC File Offset: 0x00054EDC
		public Exception()
		{
			this.Init();
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00056CEA File Offset: 0x00054EEA
		public Exception(string message)
		{
			this.Init();
			this._message = message;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00056CFF File Offset: 0x00054EFF
		public Exception(string message, Exception innerException)
		{
			this.Init();
			this._message = message;
			this._innerException = innerException;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00056D1C File Offset: 0x00054F1C
		[SecuritySafeCritical]
		protected Exception(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._className = info.GetString("ClassName");
			this._message = info.GetString("Message");
			this._data = (IDictionary)info.GetValueNoThrow("Data", typeof(IDictionary));
			this._innerException = (Exception)info.GetValue("InnerException", typeof(Exception));
			this._helpURL = info.GetString("HelpURL");
			this._stackTraceString = info.GetString("StackTraceString");
			this._remoteStackTraceString = info.GetString("RemoteStackTraceString");
			this._remoteStackIndex = info.GetInt32("RemoteStackIndex");
			this.HResult = info.GetInt32("HResult");
			this._source = info.GetString("Source");
			this._safeSerializationManager = info.GetValueNoThrow("SafeSerializationManager", typeof(SafeSerializationManager)) as SafeSerializationManager;
			if (this._className == null || this.HResult == 0)
			{
				throw new SerializationException(Environment.GetResourceString("Insufficient state to return the real object."));
			}
			if (context.State == StreamingContextStates.CrossAppDomain)
			{
				this._remoteStackTraceString += this._stackTraceString;
				this._stackTraceString = null;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00056E71 File Offset: 0x00055071
		public virtual string Message
		{
			get
			{
				if (this._message == null)
				{
					if (this._className == null)
					{
						this._className = this.GetClassName();
					}
					return Environment.GetResourceString("Exception of type '{0}' was thrown.", new object[] { this._className });
				}
				return this._message;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00056EAF File Offset: 0x000550AF
		public virtual IDictionary Data
		{
			[SecuritySafeCritical]
			get
			{
				if (this._data == null)
				{
					this._data = new ListDictionaryInternal();
				}
				return this._data;
			}
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0000408A File Offset: 0x0000228A
		private static bool IsImmutableAgileException(Exception e)
		{
			return false;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00056ECC File Offset: 0x000550CC
		internal void AddExceptionDataForRestrictedErrorInfo(string restrictedError, string restrictedErrorReference, string restrictedCapabilitySid, object restrictedErrorObject, bool hasrestrictedLanguageErrorObject = false)
		{
			IDictionary data = this.Data;
			if (data != null)
			{
				data.Add("RestrictedDescription", restrictedError);
				data.Add("RestrictedErrorReference", restrictedErrorReference);
				data.Add("RestrictedCapabilitySid", restrictedCapabilitySid);
				data.Add("__RestrictedErrorObject", (restrictedErrorObject == null) ? null : new Exception.__RestrictedErrorObject(restrictedErrorObject));
				data.Add("__HasRestrictedLanguageErrorObject", hasrestrictedLanguageErrorObject);
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00056F34 File Offset: 0x00055134
		internal bool TryGetRestrictedLanguageErrorObject(out object restrictedErrorObject)
		{
			restrictedErrorObject = null;
			if (this.Data != null && this.Data.Contains("__HasRestrictedLanguageErrorObject"))
			{
				if (this.Data.Contains("__RestrictedErrorObject"))
				{
					Exception.__RestrictedErrorObject _RestrictedErrorObject = this.Data["__RestrictedErrorObject"] as Exception.__RestrictedErrorObject;
					if (_RestrictedErrorObject != null)
					{
						restrictedErrorObject = _RestrictedErrorObject.RealErrorObject;
					}
				}
				return (bool)this.Data["__HasRestrictedLanguageErrorObject"];
			}
			return false;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00056FA8 File Offset: 0x000551A8
		private string GetClassName()
		{
			if (this._className == null)
			{
				this._className = this.GetType().ToString();
			}
			return this._className;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00056FCC File Offset: 0x000551CC
		public virtual Exception GetBaseException()
		{
			Exception ex = this.InnerException;
			Exception ex2 = this;
			while (ex != null)
			{
				ex2 = ex;
				ex = ex.InnerException;
			}
			return ex2;
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00056FF1 File Offset: 0x000551F1
		public Exception InnerException
		{
			get
			{
				return this._innerException;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x00056FFC File Offset: 0x000551FC
		public MethodBase TargetSite
		{
			[SecuritySafeCritical]
			get
			{
				StackTrace stackTrace = new StackTrace(this, true);
				if (stackTrace.FrameCount > 0)
				{
					return stackTrace.GetFrame(0).GetMethod();
				}
				return null;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x00057028 File Offset: 0x00055228
		public virtual string StackTrace
		{
			get
			{
				return this.GetStackTrace(true);
			}
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00057034 File Offset: 0x00055234
		private string GetStackTrace(bool needFileInfo)
		{
			string text = this._stackTraceString;
			string text2 = this._remoteStackTraceString;
			if (!needFileInfo)
			{
				text = this.StripFileInfo(text, false);
				text2 = this.StripFileInfo(text2, true);
			}
			if (text != null)
			{
				return text2 + text;
			}
			if (this._stackTrace == null)
			{
				return text2;
			}
			string stackTrace = Environment.GetStackTrace(this, needFileInfo);
			return text2 + stackTrace;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00057088 File Offset: 0x00055288
		internal void SetErrorCode(int hr)
		{
			this.HResult = hr;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00057091 File Offset: 0x00055291
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x00057099 File Offset: 0x00055299
		public virtual string HelpLink
		{
			get
			{
				return this._helpURL;
			}
			set
			{
				this._helpURL = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x000570A4 File Offset: 0x000552A4
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x00057101 File Offset: 0x00055301
		public virtual string Source
		{
			get
			{
				if (this._source == null)
				{
					StackTrace stackTrace = new StackTrace(this, true);
					if (stackTrace.FrameCount > 0)
					{
						MethodBase method = stackTrace.GetFrame(0).GetMethod();
						if (method != null)
						{
							this._source = method.DeclaringType.Assembly.GetName().Name;
						}
					}
				}
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0005710A File Offset: 0x0005530A
		public override string ToString()
		{
			return this.ToString(true, true);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00057114 File Offset: 0x00055314
		private string ToString(bool needFileLineInfo, bool needMessage)
		{
			string text = (needMessage ? this.Message : null);
			string text2;
			if (text == null || text.Length <= 0)
			{
				text2 = this.GetClassName();
			}
			else
			{
				text2 = this.GetClassName() + ": " + text;
			}
			if (this._innerException != null)
			{
				text2 = string.Concat(new string[]
				{
					text2,
					" ---> ",
					this._innerException.ToString(needFileLineInfo, needMessage),
					Environment.NewLine,
					"   ",
					Environment.GetResourceString("--- End of inner exception stack trace ---")
				});
			}
			string stackTrace = this.GetStackTrace(needFileLineInfo);
			if (stackTrace != null)
			{
				text2 = text2 + Environment.NewLine + stackTrace;
			}
			return text2;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060015BB RID: 5563 RVA: 0x000571BB File Offset: 0x000553BB
		// (remove) Token: 0x060015BC RID: 5564 RVA: 0x000571C9 File Offset: 0x000553C9
		protected event EventHandler<SafeSerializationEventArgs> SerializeObjectState
		{
			add
			{
				this._safeSerializationManager.SerializeObjectState += value;
			}
			remove
			{
				this._safeSerializationManager.SerializeObjectState -= value;
			}
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x000571D8 File Offset: 0x000553D8
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			string text = this._stackTraceString;
			if (this._stackTrace != null && text == null)
			{
				text = Environment.GetStackTrace(this, true);
			}
			if (this._source == null)
			{
				this._source = this.Source;
			}
			info.AddValue("ClassName", this.GetClassName(), typeof(string));
			info.AddValue("Message", this._message, typeof(string));
			info.AddValue("Data", this._data, typeof(IDictionary));
			info.AddValue("InnerException", this._innerException, typeof(Exception));
			info.AddValue("HelpURL", this._helpURL, typeof(string));
			info.AddValue("StackTraceString", text, typeof(string));
			info.AddValue("RemoteStackTraceString", this._remoteStackTraceString, typeof(string));
			info.AddValue("RemoteStackIndex", this._remoteStackIndex, typeof(int));
			info.AddValue("ExceptionMethod", null);
			info.AddValue("HResult", this.HResult);
			info.AddValue("Source", this._source, typeof(string));
			if (this._safeSerializationManager != null && this._safeSerializationManager.IsActive)
			{
				info.AddValue("SafeSerializationManager", this._safeSerializationManager, typeof(SafeSerializationManager));
				this._safeSerializationManager.CompleteSerialization(this, info, context);
			}
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00057370 File Offset: 0x00055570
		internal Exception PrepForRemoting()
		{
			string text;
			if (this._remoteStackIndex == 0)
			{
				text = string.Concat(new string[]
				{
					Environment.NewLine,
					"Server stack trace: ",
					Environment.NewLine,
					this.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Exception rethrown at [",
					this._remoteStackIndex.ToString(),
					"]: ",
					Environment.NewLine
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					this.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Exception rethrown at [",
					this._remoteStackIndex.ToString(),
					"]: ",
					Environment.NewLine
				});
			}
			this._remoteStackTraceString = text;
			this._remoteStackIndex++;
			return this;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0005744F File Offset: 0x0005564F
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this._stackTrace = null;
			if (this._safeSerializationManager == null)
			{
				this._safeSerializationManager = new SafeSerializationManager();
				return;
			}
			this._safeSerializationManager.CompleteDeserialization(this);
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00057478 File Offset: 0x00055678
		internal void InternalPreserveStackTrace()
		{
			string stackTrace = this.StackTrace;
			if (stackTrace != null && stackTrace.Length > 0)
			{
				this._remoteStackTraceString = stackTrace + Environment.NewLine;
			}
			this._stackTrace = null;
			this._stackTraceString = null;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000025F2 File Offset: 0x000007F2
		private string StripFileInfo(string stackTrace, bool isRemoteStackTrace)
		{
			return stackTrace;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x000574B7 File Offset: 0x000556B7
		internal string RemoteStackTrace
		{
			get
			{
				return this._remoteStackTraceString;
			}
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000574BF File Offset: 0x000556BF
		[SecuritySafeCritical]
		internal void RestoreExceptionDispatchInfo(ExceptionDispatchInfo exceptionDispatchInfo)
		{
			this.captured_traces = (StackTrace[])exceptionDispatchInfo.BinaryStackTraceArray;
			this._stackTrace = null;
			this._stackTraceString = null;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x000574E0 File Offset: 0x000556E0
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x000574E8 File Offset: 0x000556E8
		public int HResult
		{
			get
			{
				return this._HResult;
			}
			protected set
			{
				this._HResult = value;
			}
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x000574F4 File Offset: 0x000556F4
		[SecurityCritical]
		internal virtual string InternalToString()
		{
			bool flag = true;
			return this.ToString(flag, true);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00047D48 File Offset: 0x00045F48
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x0005750B File Offset: 0x0005570B
		internal bool IsTransient
		{
			[SecuritySafeCritical]
			get
			{
				return Exception.nIsTransient(this._HResult);
			}
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000174FB File Offset: 0x000156FB
		private static bool nIsTransient(int hr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00057518 File Offset: 0x00055718
		[SecuritySafeCritical]
		internal static string GetMessageFromNativeResources(Exception.ExceptionMessageKind kind)
		{
			switch (kind)
			{
			case Exception.ExceptionMessageKind.ThreadAbort:
				return "Thread was being aborted.";
			case Exception.ExceptionMessageKind.ThreadInterrupted:
				return "Thread was interrupted from a waiting state.";
			case Exception.ExceptionMessageKind.OutOfMemory:
				return "Insufficient memory to continue the execution of the program.";
			default:
				return "";
			}
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00057547 File Offset: 0x00055747
		internal void SetMessage(string s)
		{
			this._message = s;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00057550 File Offset: 0x00055750
		internal void SetStackTrace(string s)
		{
			this._stackTraceString = s;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0005755C File Offset: 0x0005575C
		internal Exception FixRemotingException()
		{
			string text = string.Format((this._remoteStackIndex == 0) ? "{0}{0}Server stack trace: {0}{1}{0}{0}Exception rethrown at [{2}]: {0}" : "{1}{0}{0}Exception rethrown at [{2}]: {0}", Environment.NewLine, this.StackTrace, this._remoteStackIndex);
			this._remoteStackTraceString = text;
			this._remoteStackIndex++;
			this._stackTraceString = null;
			return this;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000575B6 File Offset: 0x000557B6
		// Note: this type is marked as 'beforefieldinit'.
		static Exception()
		{
		}

		// Token: 0x04001407 RID: 5127
		[OptionalField]
		private static object s_EDILock = new object();

		// Token: 0x04001408 RID: 5128
		private string _className;

		// Token: 0x04001409 RID: 5129
		internal string _message;

		// Token: 0x0400140A RID: 5130
		private IDictionary _data;

		// Token: 0x0400140B RID: 5131
		private Exception _innerException;

		// Token: 0x0400140C RID: 5132
		private string _helpURL;

		// Token: 0x0400140D RID: 5133
		private object _stackTrace;

		// Token: 0x0400140E RID: 5134
		private string _stackTraceString;

		// Token: 0x0400140F RID: 5135
		private string _remoteStackTraceString;

		// Token: 0x04001410 RID: 5136
		private int _remoteStackIndex;

		// Token: 0x04001411 RID: 5137
		private object _dynamicMethods;

		// Token: 0x04001412 RID: 5138
		internal int _HResult;

		// Token: 0x04001413 RID: 5139
		private string _source;

		// Token: 0x04001414 RID: 5140
		[OptionalField(VersionAdded = 4)]
		private SafeSerializationManager _safeSerializationManager;

		// Token: 0x04001415 RID: 5141
		internal StackTrace[] captured_traces;

		// Token: 0x04001416 RID: 5142
		private IntPtr[] native_trace_ips;

		// Token: 0x04001417 RID: 5143
		private int caught_in_unmanaged;

		// Token: 0x04001418 RID: 5144
		private const int _COMPlusExceptionCode = -532462766;

		// Token: 0x020001D0 RID: 464
		[Serializable]
		internal class __RestrictedErrorObject
		{
			// Token: 0x060015CF RID: 5583 RVA: 0x000575C2 File Offset: 0x000557C2
			internal __RestrictedErrorObject(object errorObject)
			{
				this._realErrorObject = errorObject;
			}

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x060015D0 RID: 5584 RVA: 0x000575D1 File Offset: 0x000557D1
			public object RealErrorObject
			{
				get
				{
					return this._realErrorObject;
				}
			}

			// Token: 0x04001419 RID: 5145
			[NonSerialized]
			private object _realErrorObject;
		}

		// Token: 0x020001D1 RID: 465
		internal enum ExceptionMessageKind
		{
			// Token: 0x0400141B RID: 5147
			ThreadAbort = 1,
			// Token: 0x0400141C RID: 5148
			ThreadInterrupted,
			// Token: 0x0400141D RID: 5149
			OutOfMemory
		}
	}
}

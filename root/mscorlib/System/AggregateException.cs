using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	// Token: 0x020000B8 RID: 184
	[DebuggerDisplay("Count = {InnerExceptionCount}")]
	[Serializable]
	public class AggregateException : Exception
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x000183FF File Offset: 0x000165FF
		public AggregateException()
			: base("One or more errors occurred.")
		{
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(Array.Empty<Exception>());
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001841C File Offset: 0x0001661C
		public AggregateException(string message)
			: base(message)
		{
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(Array.Empty<Exception>());
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00018435 File Offset: 0x00016635
		public AggregateException(string message, Exception innerException)
			: base(message, innerException)
		{
			if (innerException == null)
			{
				throw new ArgumentNullException("innerException");
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(new Exception[] { innerException });
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00018462 File Offset: 0x00016662
		public AggregateException(IEnumerable<Exception> innerExceptions)
			: this("One or more errors occurred.", innerExceptions)
		{
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00018470 File Offset: 0x00016670
		public AggregateException(params Exception[] innerExceptions)
			: this("One or more errors occurred.", innerExceptions)
		{
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001847E File Offset: 0x0001667E
		public AggregateException(string message, IEnumerable<Exception> innerExceptions)
			: this(message, (innerExceptions as IList<Exception>) ?? ((innerExceptions == null) ? null : new List<Exception>(innerExceptions)))
		{
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001849D File Offset: 0x0001669D
		public AggregateException(string message, params Exception[] innerExceptions)
			: this(message, innerExceptions)
		{
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000184A8 File Offset: 0x000166A8
		private AggregateException(string message, IList<Exception> innerExceptions)
			: base(message, (innerExceptions != null && innerExceptions.Count > 0) ? innerExceptions[0] : null)
		{
			if (innerExceptions == null)
			{
				throw new ArgumentNullException("innerExceptions");
			}
			Exception[] array = new Exception[innerExceptions.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = innerExceptions[i];
				if (array[i] == null)
				{
					throw new ArgumentException("An element of innerExceptions was null.");
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00018520 File Offset: 0x00016720
		internal AggregateException(IEnumerable<ExceptionDispatchInfo> innerExceptionInfos)
			: this("One or more errors occurred.", innerExceptionInfos)
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001852E File Offset: 0x0001672E
		internal AggregateException(string message, IEnumerable<ExceptionDispatchInfo> innerExceptionInfos)
			: this(message, (innerExceptionInfos as IList<ExceptionDispatchInfo>) ?? ((innerExceptionInfos == null) ? null : new List<ExceptionDispatchInfo>(innerExceptionInfos)))
		{
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00018550 File Offset: 0x00016750
		private AggregateException(string message, IList<ExceptionDispatchInfo> innerExceptionInfos)
			: base(message, (innerExceptionInfos != null && innerExceptionInfos.Count > 0 && innerExceptionInfos[0] != null) ? innerExceptionInfos[0].SourceException : null)
		{
			if (innerExceptionInfos == null)
			{
				throw new ArgumentNullException("innerExceptionInfos");
			}
			Exception[] array = new Exception[innerExceptionInfos.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ExceptionDispatchInfo exceptionDispatchInfo = innerExceptionInfos[i];
				if (exceptionDispatchInfo != null)
				{
					array[i] = exceptionDispatchInfo.SourceException;
				}
				if (array[i] == null)
				{
					throw new ArgumentException("An element of innerExceptions was null.");
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000185E0 File Offset: 0x000167E0
		protected AggregateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			Exception[] array = info.GetValue("InnerExceptions", typeof(Exception[])) as Exception[];
			if (array == null)
			{
				throw new SerializationException("The serialization stream contains no inner exceptions.");
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00018638 File Offset: 0x00016838
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			Exception[] array = new Exception[this.m_innerExceptions.Count];
			this.m_innerExceptions.CopyTo(array, 0);
			info.AddValue("InnerExceptions", array, typeof(Exception[]));
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00018684 File Offset: 0x00016884
		public override Exception GetBaseException()
		{
			Exception ex = this;
			AggregateException ex2 = this;
			while (ex2 != null && ex2.InnerExceptions.Count == 1)
			{
				ex = ex.InnerException;
				ex2 = ex as AggregateException;
			}
			return ex;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x000186B7 File Offset: 0x000168B7
		public ReadOnlyCollection<Exception> InnerExceptions
		{
			get
			{
				return this.m_innerExceptions;
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000186C0 File Offset: 0x000168C0
		public void Handle(Func<Exception, bool> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			List<Exception> list = null;
			for (int i = 0; i < this.m_innerExceptions.Count; i++)
			{
				if (!predicate(this.m_innerExceptions[i]))
				{
					if (list == null)
					{
						list = new List<Exception>();
					}
					list.Add(this.m_innerExceptions[i]);
				}
			}
			if (list != null)
			{
				throw new AggregateException(this.Message, list);
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00018734 File Offset: 0x00016934
		public AggregateException Flatten()
		{
			List<Exception> list = new List<Exception>();
			List<AggregateException> list2 = new List<AggregateException>();
			list2.Add(this);
			int num = 0;
			while (list2.Count > num)
			{
				IList<Exception> innerExceptions = list2[num++].InnerExceptions;
				for (int i = 0; i < innerExceptions.Count; i++)
				{
					Exception ex = innerExceptions[i];
					if (ex != null)
					{
						AggregateException ex2 = ex as AggregateException;
						if (ex2 != null)
						{
							list2.Add(ex2);
						}
						else
						{
							list.Add(ex);
						}
					}
				}
			}
			return new AggregateException(this.Message, list);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x000187C0 File Offset: 0x000169C0
		public override string Message
		{
			get
			{
				if (this.m_innerExceptions.Count == 0)
				{
					return base.Message;
				}
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				stringBuilder.Append(base.Message);
				stringBuilder.Append(' ');
				for (int i = 0; i < this.m_innerExceptions.Count; i++)
				{
					stringBuilder.Append('(');
					stringBuilder.Append(this.m_innerExceptions[i].Message);
					stringBuilder.Append(") ");
				}
				stringBuilder.Length--;
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00018858 File Offset: 0x00016A58
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			for (int i = 0; i < this.m_innerExceptions.Count; i++)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("---> ");
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "(Inner Exception #{0}) ", i);
				stringBuilder.Append(this.m_innerExceptions[i].ToString());
				stringBuilder.Append("<---");
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x000188E9 File Offset: 0x00016AE9
		private int InnerExceptionCount
		{
			get
			{
				return this.InnerExceptions.Count;
			}
		}

		// Token: 0x04000ECA RID: 3786
		private ReadOnlyCollection<Exception> m_innerExceptions;
	}
}

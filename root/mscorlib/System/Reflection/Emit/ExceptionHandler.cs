using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008DB RID: 2267
	[ComVisible(false)]
	public readonly struct ExceptionHandler : IEquatable<ExceptionHandler>
	{
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004DB7 RID: 19895 RVA: 0x000F5B7F File Offset: 0x000F3D7F
		public int ExceptionTypeToken
		{
			get
			{
				return this.m_exceptionClass;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004DB8 RID: 19896 RVA: 0x000F5B87 File Offset: 0x000F3D87
		public int TryOffset
		{
			get
			{
				return this.m_tryStartOffset;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004DB9 RID: 19897 RVA: 0x000F5B8F File Offset: 0x000F3D8F
		public int TryLength
		{
			get
			{
				return this.m_tryEndOffset - this.m_tryStartOffset;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004DBA RID: 19898 RVA: 0x000F5B9E File Offset: 0x000F3D9E
		public int FilterOffset
		{
			get
			{
				return this.m_filterOffset;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004DBB RID: 19899 RVA: 0x000F5BA6 File Offset: 0x000F3DA6
		public int HandlerOffset
		{
			get
			{
				return this.m_handlerStartOffset;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06004DBC RID: 19900 RVA: 0x000F5BAE File Offset: 0x000F3DAE
		public int HandlerLength
		{
			get
			{
				return this.m_handlerEndOffset - this.m_handlerStartOffset;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06004DBD RID: 19901 RVA: 0x000F5BBD File Offset: 0x000F3DBD
		public ExceptionHandlingClauseOptions Kind
		{
			get
			{
				return this.m_kind;
			}
		}

		// Token: 0x06004DBE RID: 19902 RVA: 0x000F5BC8 File Offset: 0x000F3DC8
		public ExceptionHandler(int tryOffset, int tryLength, int filterOffset, int handlerOffset, int handlerLength, ExceptionHandlingClauseOptions kind, int exceptionTypeToken)
		{
			if (tryOffset < 0)
			{
				throw new ArgumentOutOfRangeException("tryOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (tryLength < 0)
			{
				throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("Non-negative number required."));
			}
			if (filterOffset < 0)
			{
				throw new ArgumentOutOfRangeException("filterOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (handlerOffset < 0)
			{
				throw new ArgumentOutOfRangeException("handlerOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (handlerLength < 0)
			{
				throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("Non-negative number required."));
			}
			if ((long)tryOffset + (long)tryLength > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					0,
					int.MaxValue - tryOffset
				}));
			}
			if ((long)handlerOffset + (long)handlerLength > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					0,
					int.MaxValue - handlerOffset
				}));
			}
			if (kind == ExceptionHandlingClauseOptions.Clause && (exceptionTypeToken & 16777215) == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Token {0:x} is not a valid Type token.", new object[] { exceptionTypeToken }), "exceptionTypeToken");
			}
			if (!ExceptionHandler.IsValidKind(kind))
			{
				throw new ArgumentOutOfRangeException("kind", Environment.GetResourceString("Enum value was out of legal range."));
			}
			this.m_tryStartOffset = tryOffset;
			this.m_tryEndOffset = tryOffset + tryLength;
			this.m_filterOffset = filterOffset;
			this.m_handlerStartOffset = handlerOffset;
			this.m_handlerEndOffset = handlerOffset + handlerLength;
			this.m_kind = kind;
			this.m_exceptionClass = exceptionTypeToken;
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x000F5D62 File Offset: 0x000F3F62
		internal ExceptionHandler(int tryStartOffset, int tryEndOffset, int filterOffset, int handlerStartOffset, int handlerEndOffset, int kind, int exceptionTypeToken)
		{
			this.m_tryStartOffset = tryStartOffset;
			this.m_tryEndOffset = tryEndOffset;
			this.m_filterOffset = filterOffset;
			this.m_handlerStartOffset = handlerStartOffset;
			this.m_handlerEndOffset = handlerEndOffset;
			this.m_kind = (ExceptionHandlingClauseOptions)kind;
			this.m_exceptionClass = exceptionTypeToken;
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x000F5D99 File Offset: 0x000F3F99
		private static bool IsValidKind(ExceptionHandlingClauseOptions kind)
		{
			return kind <= ExceptionHandlingClauseOptions.Finally || kind == ExceptionHandlingClauseOptions.Fault;
		}

		// Token: 0x06004DC1 RID: 19905 RVA: 0x000F5DA6 File Offset: 0x000F3FA6
		public override int GetHashCode()
		{
			return this.m_exceptionClass ^ this.m_tryStartOffset ^ this.m_tryEndOffset ^ this.m_filterOffset ^ this.m_handlerStartOffset ^ this.m_handlerEndOffset ^ (int)this.m_kind;
		}

		// Token: 0x06004DC2 RID: 19906 RVA: 0x000F5DD8 File Offset: 0x000F3FD8
		public override bool Equals(object obj)
		{
			return obj is ExceptionHandler && this.Equals((ExceptionHandler)obj);
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x000F5DF0 File Offset: 0x000F3FF0
		public bool Equals(ExceptionHandler other)
		{
			return other.m_exceptionClass == this.m_exceptionClass && other.m_tryStartOffset == this.m_tryStartOffset && other.m_tryEndOffset == this.m_tryEndOffset && other.m_filterOffset == this.m_filterOffset && other.m_handlerStartOffset == this.m_handlerStartOffset && other.m_handlerEndOffset == this.m_handlerEndOffset && other.m_kind == this.m_kind;
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x000F5E61 File Offset: 0x000F4061
		public static bool operator ==(ExceptionHandler left, ExceptionHandler right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x000F5E6B File Offset: 0x000F406B
		public static bool operator !=(ExceptionHandler left, ExceptionHandler right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04003044 RID: 12356
		internal readonly int m_exceptionClass;

		// Token: 0x04003045 RID: 12357
		internal readonly int m_tryStartOffset;

		// Token: 0x04003046 RID: 12358
		internal readonly int m_tryEndOffset;

		// Token: 0x04003047 RID: 12359
		internal readonly int m_filterOffset;

		// Token: 0x04003048 RID: 12360
		internal readonly int m_handlerStartOffset;

		// Token: 0x04003049 RID: 12361
		internal readonly int m_handlerEndOffset;

		// Token: 0x0400304A RID: 12362
		internal readonly ExceptionHandlingClauseOptions m_kind;
	}
}

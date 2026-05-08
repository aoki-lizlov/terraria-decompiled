using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000A1A RID: 2586
	[ComVisible(true)]
	[MonoTODO("Serialized objects are not compatible with .NET")]
	[Serializable]
	public class StackTrace
	{
		// Token: 0x06005FEF RID: 24559 RVA: 0x0014C643 File Offset: 0x0014A843
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace()
		{
			this.init_frames(0, false);
		}

		// Token: 0x06005FF0 RID: 24560 RVA: 0x0014C653 File Offset: 0x0014A853
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace(bool fNeedFileInfo)
		{
			this.init_frames(0, fNeedFileInfo);
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x0014C663 File Offset: 0x0014A863
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace(int skipFrames)
		{
			this.init_frames(skipFrames, false);
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x0014C673 File Offset: 0x0014A873
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace(int skipFrames, bool fNeedFileInfo)
		{
			this.init_frames(skipFrames, fNeedFileInfo);
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x0014C684 File Offset: 0x0014A884
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void init_frames(int skipFrames, bool fNeedFileInfo)
		{
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			List<StackFrame> list = new List<StackFrame>();
			skipFrames += 2;
			StackFrame stackFrame;
			while ((stackFrame = new StackFrame(skipFrames, fNeedFileInfo)) != null && stackFrame.GetMethod() != null)
			{
				list.Add(stackFrame);
				skipFrames++;
			}
			this.debug_info = fNeedFileInfo;
			this.frames = list.ToArray();
		}

		// Token: 0x06005FF4 RID: 24564
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern StackFrame[] get_trace(Exception e, int skipFrames, bool fNeedFileInfo);

		// Token: 0x06005FF5 RID: 24565 RVA: 0x0014C6EA File Offset: 0x0014A8EA
		public StackTrace(Exception e)
			: this(e, 0, false)
		{
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x0014C6F5 File Offset: 0x0014A8F5
		public StackTrace(Exception e, bool fNeedFileInfo)
			: this(e, 0, fNeedFileInfo)
		{
		}

		// Token: 0x06005FF7 RID: 24567 RVA: 0x0014C700 File Offset: 0x0014A900
		public StackTrace(Exception e, int skipFrames)
			: this(e, skipFrames, false)
		{
		}

		// Token: 0x06005FF8 RID: 24568 RVA: 0x0014C70C File Offset: 0x0014A90C
		public StackTrace(Exception e, int skipFrames, bool fNeedFileInfo)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			this.frames = StackTrace.get_trace(e, skipFrames, fNeedFileInfo);
			this.captured_traces = e.captured_traces;
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x0014C75B File Offset: 0x0014A95B
		public StackTrace(StackFrame frame)
		{
			this.frames = new StackFrame[1];
			this.frames[0] = frame;
		}

		// Token: 0x06005FFA RID: 24570 RVA: 0x0014C778 File Offset: 0x0014A978
		[MonoLimitation("Not possible to create StackTraces from other threads")]
		[Obsolete]
		public StackTrace(Thread targetThread, bool needFileInfo)
		{
			if (targetThread == Thread.CurrentThread)
			{
				this.init_frames(0, needFileInfo);
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x0014C796 File Offset: 0x0014A996
		internal StackTrace(StackFrame[] frames)
		{
			this.frames = frames;
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06005FFC RID: 24572 RVA: 0x0014C7A5 File Offset: 0x0014A9A5
		public virtual int FrameCount
		{
			get
			{
				if (this.frames != null)
				{
					return this.frames.Length;
				}
				return 0;
			}
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x0014C7B9 File Offset: 0x0014A9B9
		public virtual StackFrame GetFrame(int index)
		{
			if (index < 0 || index >= this.FrameCount)
			{
				return null;
			}
			return this.frames[index];
		}

		// Token: 0x06005FFE RID: 24574 RVA: 0x0014C7D4 File Offset: 0x0014A9D4
		[ComVisible(false)]
		public virtual StackFrame[] GetFrames()
		{
			if (this.captured_traces == null)
			{
				return this.frames;
			}
			List<StackFrame> list = new List<StackFrame>();
			foreach (StackTrace stackTrace in this.captured_traces)
			{
				for (int j = 0; j < stackTrace.FrameCount; j++)
				{
					list.Add(stackTrace.GetFrame(j));
				}
			}
			list.AddRange(this.frames);
			return list.ToArray();
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x0014C844 File Offset: 0x0014AA44
		private static string GetAotId()
		{
			if (!StackTrace.isAotidSet)
			{
				byte[] aotId = RuntimeAssembly.GetAotId();
				if (aotId != null)
				{
					StackTrace.aotid = new Guid(aotId).ToString("N");
				}
				StackTrace.isAotidSet = true;
			}
			return StackTrace.aotid;
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x0014C884 File Offset: 0x0014AA84
		private bool AddFrames(StringBuilder sb, bool separator, out bool isAsync)
		{
			isAsync = false;
			bool flag = false;
			int i = 0;
			while (i < this.FrameCount)
			{
				StackFrame frame = this.GetFrame(i);
				if (frame.GetMethod() == null)
				{
					if (flag || separator)
					{
						sb.Append(Environment.NewLine);
					}
					sb.Append("  at ");
					string internalMethodName = frame.GetInternalMethodName();
					if (internalMethodName != null)
					{
						sb.Append(internalMethodName);
						goto IL_0180;
					}
					sb.AppendFormat("<0x{0:x5} + 0x{1:x5}> <unknown method>", frame.GetMethodAddress(), frame.GetNativeOffset());
					goto IL_0180;
				}
				else
				{
					bool flag2;
					this.GetFullNameForStackTrace(sb, frame.GetMethod(), flag || separator, out flag2, out isAsync);
					if (!flag2)
					{
						if (frame.GetILOffset() == -1)
						{
							sb.AppendFormat(" <0x{0:x5} + 0x{1:x5}>", frame.GetMethodAddress(), frame.GetNativeOffset());
							if (frame.GetMethodIndex() != 16777215U)
							{
								sb.AppendFormat(" {0}", frame.GetMethodIndex());
							}
						}
						else
						{
							sb.AppendFormat(" [0x{0:x5}]", frame.GetILOffset());
						}
						string text = frame.GetSecureFileName();
						if (text[0] == '<')
						{
							string text2 = frame.GetMethod().Module.ModuleVersionId.ToString("N");
							string aotId = StackTrace.GetAotId();
							if (frame.GetILOffset() != -1 || aotId == null)
							{
								text = string.Format("<{0}>", text2);
							}
							else
							{
								text = string.Format("<{0}#{1}>", text2, aotId);
							}
						}
						sb.AppendFormat(" in {0}:{1} ", text, frame.GetFileLineNumber());
						goto IL_0180;
					}
				}
				IL_0182:
				i++;
				continue;
				IL_0180:
				flag = true;
				goto IL_0182;
			}
			return flag;
		}

		// Token: 0x06006001 RID: 24577 RVA: 0x0014CA24 File Offset: 0x0014AC24
		private void GetFullNameForStackTrace(StringBuilder sb, MethodBase mi, bool needsNewLine, out bool skipped, out bool isAsync)
		{
			Type type = mi.DeclaringType;
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				type = type.GetGenericTypeDefinition();
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.MetadataToken == mi.MetadataToken)
					{
						mi = methodInfo;
						break;
					}
				}
			}
			isAsync = typeof(IAsyncStateMachine).IsAssignableFrom(type);
			skipped = mi.IsDefined(typeof(StackTraceHiddenAttribute)) || type.IsDefined(typeof(StackTraceHiddenAttribute));
			if (skipped)
			{
				return;
			}
			if (isAsync)
			{
				StackTrace.ConvertAsyncStateMachineMethod(ref mi, ref type);
			}
			if (needsNewLine)
			{
				sb.Append(Environment.NewLine);
			}
			sb.Append("  at ");
			sb.Append(type.ToString());
			sb.Append(".");
			sb.Append(mi.Name);
			if (mi.IsGenericMethod)
			{
				mi = ((MethodInfo)mi).GetGenericMethodDefinition();
				Type[] genericArguments = mi.GetGenericArguments();
				sb.Append("[");
				for (int j = 0; j < genericArguments.Length; j++)
				{
					if (j > 0)
					{
						sb.Append(",");
					}
					sb.Append(genericArguments[j].Name);
				}
				sb.Append("]");
			}
			ParameterInfo[] parameters = mi.GetParameters();
			sb.Append(" (");
			for (int k = 0; k < parameters.Length; k++)
			{
				if (k > 0)
				{
					sb.Append(", ");
				}
				Type type2 = parameters[k].ParameterType;
				if (type2.IsGenericType && !type2.IsGenericTypeDefinition)
				{
					type2 = type2.GetGenericTypeDefinition();
				}
				sb.Append(type2.ToString());
				if (parameters[k].Name != null)
				{
					sb.Append(" ");
					sb.Append(parameters[k].Name);
				}
			}
			sb.Append(")");
		}

		// Token: 0x06006002 RID: 24578 RVA: 0x0014CC18 File Offset: 0x0014AE18
		private static void ConvertAsyncStateMachineMethod(ref MethodBase method, ref Type declaringType)
		{
			Type declaringType2 = declaringType.DeclaringType;
			if (declaringType2 == null)
			{
				return;
			}
			MethodInfo[] methods = declaringType2.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (methods == null)
			{
				return;
			}
			foreach (MethodInfo methodInfo in methods)
			{
				IEnumerable<AsyncStateMachineAttribute> customAttributes = methodInfo.GetCustomAttributes<AsyncStateMachineAttribute>();
				if (customAttributes != null)
				{
					using (IEnumerator<AsyncStateMachineAttribute> enumerator = customAttributes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.StateMachineType == declaringType)
							{
								method = methodInfo;
								declaringType = methodInfo.DeclaringType;
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x06006003 RID: 24579 RVA: 0x0014CCBC File Offset: 0x0014AEBC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			if (this.captured_traces != null)
			{
				StackTrace[] array = this.captured_traces;
				for (int i = 0; i < array.Length; i++)
				{
					bool flag2;
					flag = array[i].AddFrames(stringBuilder, flag, out flag2);
					if (flag && !flag2)
					{
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append("--- End of stack trace from previous location where exception was thrown ---");
						stringBuilder.Append(Environment.NewLine);
					}
				}
			}
			bool flag3;
			this.AddFrames(stringBuilder, flag, out flag3);
			return stringBuilder.ToString();
		}

		// Token: 0x06006004 RID: 24580 RVA: 0x00056468 File Offset: 0x00054668
		internal string ToString(StackTrace.TraceFormat traceFormat)
		{
			return this.ToString();
		}

		// Token: 0x040039C5 RID: 14789
		public const int METHODS_TO_SKIP = 0;

		// Token: 0x040039C6 RID: 14790
		private const string prefix = "  at ";

		// Token: 0x040039C7 RID: 14791
		private StackFrame[] frames;

		// Token: 0x040039C8 RID: 14792
		private readonly StackTrace[] captured_traces;

		// Token: 0x040039C9 RID: 14793
		private bool debug_info;

		// Token: 0x040039CA RID: 14794
		private static bool isAotidSet;

		// Token: 0x040039CB RID: 14795
		private static string aotid;

		// Token: 0x02000A1B RID: 2587
		internal enum TraceFormat
		{
			// Token: 0x040039CD RID: 14797
			Normal,
			// Token: 0x040039CE RID: 14798
			TrailingNewLine,
			// Token: 0x040039CF RID: 14799
			NoResourceLookup
		}
	}
}

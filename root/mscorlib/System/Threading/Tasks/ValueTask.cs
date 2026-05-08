using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;

namespace System.Threading.Tasks
{
	// Token: 0x020002DF RID: 735
	[AsyncMethodBuilder(typeof(AsyncValueTaskMethodBuilder))]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ValueTask : IEquatable<ValueTask>
	{
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x000789AA File Offset: 0x00076BAA
		internal static Task CompletedTask
		{
			get
			{
				return Task.CompletedTask;
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000789B1 File Offset: 0x00076BB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(Task task)
		{
			if (task == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.task);
			}
			this._obj = task;
			this._continueOnCapturedContext = true;
			this._token = 0;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000789D2 File Offset: 0x00076BD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(IValueTaskSource source, short token)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			this._obj = source;
			this._token = token;
			this._continueOnCapturedContext = true;
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000789F3 File Offset: 0x00076BF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ValueTask(object obj, short token, bool continueOnCapturedContext)
		{
			this._obj = obj;
			this._token = token;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00078A0A File Offset: 0x00076C0A
		public override int GetHashCode()
		{
			object obj = this._obj;
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00078A1D File Offset: 0x00076C1D
		public override bool Equals(object obj)
		{
			return obj is ValueTask && this.Equals((ValueTask)obj);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00078A35 File Offset: 0x00076C35
		public bool Equals(ValueTask other)
		{
			return this._obj == other._obj && this._token == other._token;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x00078A55 File Offset: 0x00076C55
		public static bool operator ==(ValueTask left, ValueTask right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x00078A5F File Offset: 0x00076C5F
		public static bool operator !=(ValueTask left, ValueTask right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00078A6C File Offset: 0x00076C6C
		public Task AsTask()
		{
			object obj = this._obj;
			Task task;
			if (obj != null)
			{
				if ((task = obj as Task) == null)
				{
					return this.GetTaskForValueTaskSource(Unsafe.As<IValueTaskSource>(obj));
				}
			}
			else
			{
				task = ValueTask.CompletedTask;
			}
			return task;
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x00078A9F File Offset: 0x00076C9F
		public ValueTask Preserve()
		{
			if (this._obj != null)
			{
				return new ValueTask(this.AsTask());
			}
			return this;
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00078ABC File Offset: 0x00076CBC
		private Task GetTaskForValueTaskSource(IValueTaskSource t)
		{
			ValueTaskSourceStatus status = t.GetStatus(this._token);
			if (status != ValueTaskSourceStatus.Pending)
			{
				try
				{
					t.GetResult(this._token);
					return ValueTask.CompletedTask;
				}
				catch (Exception ex)
				{
					if (status != ValueTaskSourceStatus.Canceled)
					{
						return Task.FromException(ex);
					}
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 != null)
					{
						Task<VoidTaskResult> task = new Task<VoidTaskResult>();
						task.TrySetCanceled(ex2.CancellationToken, ex2);
						return task;
					}
					return ValueTask.s_canceledTask;
				}
			}
			return new ValueTask.ValueTaskSourceAsTask(t, this._token);
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x00078B44 File Offset: 0x00076D44
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return true;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsCompleted;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) > ValueTaskSourceStatus.Pending;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x00078B84 File Offset: 0x00076D84
		public bool IsCompletedSuccessfully
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return true;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsCompletedSuccessfully;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Succeeded;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x00078BC4 File Offset: 0x00076DC4
		public bool IsFaulted
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsFaulted;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Faulted;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x00078C04 File Offset: 0x00076E04
		public bool IsCanceled
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsCanceled;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Canceled;
			}
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00078C44 File Offset: 0x00076E44
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ThrowIfCompletedUnsuccessfully()
		{
			object obj = this._obj;
			if (obj != null)
			{
				Task task = obj as Task;
				if (task != null)
				{
					TaskAwaiter.ValidateEnd(task);
					return;
				}
				Unsafe.As<IValueTaskSource>(obj).GetResult(this._token);
			}
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00078C7D File Offset: 0x00076E7D
		public ValueTaskAwaiter GetAwaiter()
		{
			return new ValueTaskAwaiter(this);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00078C8A File Offset: 0x00076E8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredValueTaskAwaitable(new ValueTask(this._obj, this._token, continueOnCapturedContext));
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x00078CA3 File Offset: 0x00076EA3
		// Note: this type is marked as 'beforefieldinit'.
		static ValueTask()
		{
		}

		// Token: 0x04001A99 RID: 6809
		private static readonly Task s_canceledTask = Task.FromCanceled(new CancellationToken(true));

		// Token: 0x04001A9A RID: 6810
		internal readonly object _obj;

		// Token: 0x04001A9B RID: 6811
		internal readonly short _token;

		// Token: 0x04001A9C RID: 6812
		internal readonly bool _continueOnCapturedContext;

		// Token: 0x020002E0 RID: 736
		private sealed class ValueTaskSourceAsTask : Task<VoidTaskResult>
		{
			// Token: 0x06002159 RID: 8537 RVA: 0x00078CB5 File Offset: 0x00076EB5
			public ValueTaskSourceAsTask(IValueTaskSource source, short token)
			{
				this._token = token;
				this._source = source;
				source.OnCompleted(ValueTask.ValueTaskSourceAsTask.s_completionAction, this, token, ValueTaskSourceOnCompletedFlags.None);
			}

			// Token: 0x0600215A RID: 8538 RVA: 0x00078CD9 File Offset: 0x00076ED9
			// Note: this type is marked as 'beforefieldinit'.
			static ValueTaskSourceAsTask()
			{
			}

			// Token: 0x04001A9D RID: 6813
			private static readonly Action<object> s_completionAction = delegate(object state)
			{
				ValueTask.ValueTaskSourceAsTask valueTaskSourceAsTask = state as ValueTask.ValueTaskSourceAsTask;
				if (valueTaskSourceAsTask != null)
				{
					IValueTaskSource source = valueTaskSourceAsTask._source;
					if (source != null)
					{
						valueTaskSourceAsTask._source = null;
						ValueTaskSourceStatus status = source.GetStatus(valueTaskSourceAsTask._token);
						try
						{
							source.GetResult(valueTaskSourceAsTask._token);
							valueTaskSourceAsTask.TrySetResult(default(VoidTaskResult));
						}
						catch (Exception ex)
						{
							if (status == ValueTaskSourceStatus.Canceled)
							{
								OperationCanceledException ex2 = ex as OperationCanceledException;
								if (ex2 != null)
								{
									valueTaskSourceAsTask.TrySetCanceled(ex2.CancellationToken, ex2);
								}
								else
								{
									valueTaskSourceAsTask.TrySetCanceled(new CancellationToken(true));
								}
							}
							else
							{
								valueTaskSourceAsTask.TrySetException(ex);
							}
						}
						return;
					}
				}
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
			};

			// Token: 0x04001A9E RID: 6814
			private IValueTaskSource _source;

			// Token: 0x04001A9F RID: 6815
			private readonly short _token;

			// Token: 0x020002E1 RID: 737
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x0600215B RID: 8539 RVA: 0x00078CF0 File Offset: 0x00076EF0
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x0600215C RID: 8540 RVA: 0x000025BE File Offset: 0x000007BE
				public <>c()
				{
				}

				// Token: 0x0600215D RID: 8541 RVA: 0x00078CFC File Offset: 0x00076EFC
				internal void <.cctor>b__4_0(object state)
				{
					ValueTask.ValueTaskSourceAsTask valueTaskSourceAsTask = state as ValueTask.ValueTaskSourceAsTask;
					if (valueTaskSourceAsTask != null)
					{
						IValueTaskSource source = valueTaskSourceAsTask._source;
						if (source != null)
						{
							valueTaskSourceAsTask._source = null;
							ValueTaskSourceStatus status = source.GetStatus(valueTaskSourceAsTask._token);
							try
							{
								source.GetResult(valueTaskSourceAsTask._token);
								valueTaskSourceAsTask.TrySetResult(default(VoidTaskResult));
							}
							catch (Exception ex)
							{
								if (status == ValueTaskSourceStatus.Canceled)
								{
									OperationCanceledException ex2 = ex as OperationCanceledException;
									if (ex2 != null)
									{
										valueTaskSourceAsTask.TrySetCanceled(ex2.CancellationToken, ex2);
									}
									else
									{
										valueTaskSourceAsTask.TrySetCanceled(new CancellationToken(true));
									}
								}
								else
								{
									valueTaskSourceAsTask.TrySetException(ex);
								}
							}
							return;
						}
					}
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
				}

				// Token: 0x04001AA0 RID: 6816
				public static readonly ValueTask.ValueTaskSourceAsTask.<>c <>9 = new ValueTask.ValueTaskSourceAsTask.<>c();
			}
		}
	}
}

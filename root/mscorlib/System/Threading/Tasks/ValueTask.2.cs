using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;

namespace System.Threading.Tasks
{
	// Token: 0x020002E2 RID: 738
	[AsyncMethodBuilder(typeof(AsyncValueTaskMethodBuilder<>))]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ValueTask<TResult> : IEquatable<ValueTask<TResult>>
	{
		// Token: 0x0600215E RID: 8542 RVA: 0x00078DA8 File Offset: 0x00076FA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(TResult result)
		{
			this._result = result;
			this._obj = null;
			this._continueOnCapturedContext = true;
			this._token = 0;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x00078DC6 File Offset: 0x00076FC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(Task<TResult> task)
		{
			if (task == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.task);
			}
			this._obj = task;
			this._result = default(TResult);
			this._continueOnCapturedContext = true;
			this._token = 0;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00078DF3 File Offset: 0x00076FF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(IValueTaskSource<TResult> source, short token)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			this._obj = source;
			this._token = token;
			this._result = default(TResult);
			this._continueOnCapturedContext = true;
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x00078E20 File Offset: 0x00077020
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ValueTask(object obj, TResult result, short token, bool continueOnCapturedContext)
		{
			this._obj = obj;
			this._result = result;
			this._token = token;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x00078E40 File Offset: 0x00077040
		public override int GetHashCode()
		{
			if (this._obj != null)
			{
				return this._obj.GetHashCode();
			}
			if (this._result == null)
			{
				return 0;
			}
			TResult result = this._result;
			return result.GetHashCode();
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00078E84 File Offset: 0x00077084
		public override bool Equals(object obj)
		{
			return obj is ValueTask<TResult> && this.Equals((ValueTask<TResult>)obj);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00078E9C File Offset: 0x0007709C
		public bool Equals(ValueTask<TResult> other)
		{
			if (this._obj == null && other._obj == null)
			{
				return EqualityComparer<TResult>.Default.Equals(this._result, other._result);
			}
			return this._obj == other._obj && this._token == other._token;
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00078EEE File Offset: 0x000770EE
		public static bool operator ==(ValueTask<TResult> left, ValueTask<TResult> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00078EF8 File Offset: 0x000770F8
		public static bool operator !=(ValueTask<TResult> left, ValueTask<TResult> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00078F08 File Offset: 0x00077108
		public Task<TResult> AsTask()
		{
			object obj = this._obj;
			if (obj == null)
			{
				return AsyncTaskMethodBuilder<TResult>.GetTaskForResult(this._result);
			}
			Task<TResult> task = obj as Task<TResult>;
			if (task != null)
			{
				return task;
			}
			return this.GetTaskForValueTaskSource(Unsafe.As<IValueTaskSource<TResult>>(obj));
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x00078F43 File Offset: 0x00077143
		public ValueTask<TResult> Preserve()
		{
			if (this._obj != null)
			{
				return new ValueTask<TResult>(this.AsTask());
			}
			return this;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x00078F60 File Offset: 0x00077160
		private Task<TResult> GetTaskForValueTaskSource(IValueTaskSource<TResult> t)
		{
			ValueTaskSourceStatus status = t.GetStatus(this._token);
			if (status != ValueTaskSourceStatus.Pending)
			{
				try
				{
					return AsyncTaskMethodBuilder<TResult>.GetTaskForResult(t.GetResult(this._token));
				}
				catch (Exception ex)
				{
					if (status != ValueTaskSourceStatus.Canceled)
					{
						return Task.FromException<TResult>(ex);
					}
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 != null)
					{
						Task<TResult> task = new Task<TResult>();
						task.TrySetCanceled(ex2.CancellationToken, ex2);
						return task;
					}
					Task<TResult> task2 = ValueTask<TResult>.s_canceledTask;
					if (task2 == null)
					{
						task2 = Task.FromCanceled<TResult>(new CancellationToken(true));
						ValueTask<TResult>.s_canceledTask = task2;
					}
					return task2;
				}
			}
			return new ValueTask<TResult>.ValueTaskSourceAsTask(t, this._token);
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x00079004 File Offset: 0x00077204
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
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					return task.IsCompleted;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) > ValueTaskSourceStatus.Pending;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600216B RID: 8555 RVA: 0x00079044 File Offset: 0x00077244
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
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					return task.IsCompletedSuccessfully;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Succeeded;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x00079084 File Offset: 0x00077284
		public bool IsFaulted
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					return task.IsFaulted;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Faulted;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x000790C4 File Offset: 0x000772C4
		public bool IsCanceled
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					return task.IsCanceled;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Canceled;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x00079104 File Offset: 0x00077304
		public TResult Result
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return this._result;
				}
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					TaskAwaiter.ValidateEnd(task);
					return task.ResultOnSuccess;
				}
				return Unsafe.As<IValueTaskSource<TResult>>(obj).GetResult(this._token);
			}
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0007914A File Offset: 0x0007734A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTaskAwaiter<TResult> GetAwaiter()
		{
			return new ValueTaskAwaiter<TResult>(this);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x00079157 File Offset: 0x00077357
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable<TResult> ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredValueTaskAwaitable<TResult>(new ValueTask<TResult>(this._obj, this._result, this._token, continueOnCapturedContext));
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x00079178 File Offset: 0x00077378
		public override string ToString()
		{
			if (this.IsCompletedSuccessfully)
			{
				TResult result = this.Result;
				if (result != null)
				{
					return result.ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x04001AA1 RID: 6817
		private static Task<TResult> s_canceledTask;

		// Token: 0x04001AA2 RID: 6818
		internal readonly object _obj;

		// Token: 0x04001AA3 RID: 6819
		internal readonly TResult _result;

		// Token: 0x04001AA4 RID: 6820
		internal readonly short _token;

		// Token: 0x04001AA5 RID: 6821
		internal readonly bool _continueOnCapturedContext;

		// Token: 0x020002E3 RID: 739
		private sealed class ValueTaskSourceAsTask : Task<TResult>
		{
			// Token: 0x06002172 RID: 8562 RVA: 0x000791AF File Offset: 0x000773AF
			public ValueTaskSourceAsTask(IValueTaskSource<TResult> source, short token)
			{
				this._source = source;
				this._token = token;
				source.OnCompleted(ValueTask<TResult>.ValueTaskSourceAsTask.s_completionAction, this, token, ValueTaskSourceOnCompletedFlags.None);
			}

			// Token: 0x06002173 RID: 8563 RVA: 0x000791D3 File Offset: 0x000773D3
			// Note: this type is marked as 'beforefieldinit'.
			static ValueTaskSourceAsTask()
			{
			}

			// Token: 0x04001AA6 RID: 6822
			private static readonly Action<object> s_completionAction = delegate(object state)
			{
				ValueTask<TResult>.ValueTaskSourceAsTask valueTaskSourceAsTask = state as ValueTask<TResult>.ValueTaskSourceAsTask;
				if (valueTaskSourceAsTask != null)
				{
					IValueTaskSource<TResult> source = valueTaskSourceAsTask._source;
					if (source != null)
					{
						valueTaskSourceAsTask._source = null;
						ValueTaskSourceStatus status = source.GetStatus(valueTaskSourceAsTask._token);
						try
						{
							valueTaskSourceAsTask.TrySetResult(source.GetResult(valueTaskSourceAsTask._token));
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

			// Token: 0x04001AA7 RID: 6823
			private IValueTaskSource<TResult> _source;

			// Token: 0x04001AA8 RID: 6824
			private readonly short _token;

			// Token: 0x020002E4 RID: 740
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06002174 RID: 8564 RVA: 0x000791EA File Offset: 0x000773EA
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06002175 RID: 8565 RVA: 0x000025BE File Offset: 0x000007BE
				public <>c()
				{
				}

				// Token: 0x06002176 RID: 8566 RVA: 0x000791F8 File Offset: 0x000773F8
				internal void <.cctor>b__4_0(object state)
				{
					ValueTask<TResult>.ValueTaskSourceAsTask valueTaskSourceAsTask = state as ValueTask<TResult>.ValueTaskSourceAsTask;
					if (valueTaskSourceAsTask != null)
					{
						IValueTaskSource<TResult> source = valueTaskSourceAsTask._source;
						if (source != null)
						{
							valueTaskSourceAsTask._source = null;
							ValueTaskSourceStatus status = source.GetStatus(valueTaskSourceAsTask._token);
							try
							{
								valueTaskSourceAsTask.TrySetResult(source.GetResult(valueTaskSourceAsTask._token));
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

				// Token: 0x04001AA9 RID: 6825
				public static readonly ValueTask<TResult>.ValueTaskSourceAsTask.<>c <>9 = new ValueTask<TResult>.ValueTaskSourceAsTask.<>c();
			}
		}
	}
}

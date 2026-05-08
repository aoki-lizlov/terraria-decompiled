using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007DC RID: 2012
	public readonly struct ValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x060045BE RID: 17854 RVA: 0x000E53D0 File Offset: 0x000E35D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ValueTaskAwaiter(ValueTask value)
		{
			this._value = value;
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x000E53D9 File Offset: 0x000E35D9
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._value.IsCompleted;
			}
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x000E53E6 File Offset: 0x000E35E6
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetResult()
		{
			this._value.ThrowIfCompletedUnsuccessfully();
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x000E53F4 File Offset: 0x000E35F4
		public void OnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task task = obj as Task;
			if (task != null)
			{
				task.GetAwaiter().OnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext | ValueTaskSourceOnCompletedFlags.FlowExecutionContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().OnCompleted(continuation);
		}

		// Token: 0x060045C2 RID: 17858 RVA: 0x000E545C File Offset: 0x000E365C
		public void UnsafeOnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task task = obj as Task;
			if (task != null)
			{
				task.GetAwaiter().UnsafeOnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().UnsafeOnCompleted(continuation);
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x000E54C3 File Offset: 0x000E36C3
		// Note: this type is marked as 'beforefieldinit'.
		static ValueTaskAwaiter()
		{
		}

		// Token: 0x04002CC6 RID: 11462
		internal static readonly Action<object> s_invokeActionDelegate = delegate(object state)
		{
			Action action = state as Action;
			if (action == null)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
				return;
			}
			action();
		};

		// Token: 0x04002CC7 RID: 11463
		private readonly ValueTask _value;

		// Token: 0x020007DD RID: 2013
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060045C4 RID: 17860 RVA: 0x000E54DA File Offset: 0x000E36DA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060045C5 RID: 17861 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x060045C6 RID: 17862 RVA: 0x000E54E8 File Offset: 0x000E36E8
			internal void <.cctor>b__9_0(object state)
			{
				Action action = state as Action;
				if (action == null)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
					return;
				}
				action();
			}

			// Token: 0x04002CC8 RID: 11464
			public static readonly ValueTaskAwaiter.<>c <>9 = new ValueTaskAwaiter.<>c();
		}
	}
}

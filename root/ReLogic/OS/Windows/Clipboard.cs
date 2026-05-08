using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using ReLogic.OS.Base;

namespace ReLogic.OS.Windows
{
	// Token: 0x02000066 RID: 102
	internal class Clipboard : Clipboard
	{
		// Token: 0x06000232 RID: 562 RVA: 0x000099A9 File Offset: 0x00007BA9
		protected override string GetClipboard()
		{
			return this.TryToGetClipboardText();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000099B4 File Offset: 0x00007BB4
		protected override void SetClipboard(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			try
			{
				Clipboard.InvokeInStaThread(delegate
				{
					Clipboard.SetText(text);
				});
			}
			catch
			{
				Console.WriteLine("Failed to set clipboard contents!");
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009A10 File Offset: 0x00007C10
		private string TryToGetClipboardText()
		{
			string text;
			try
			{
				text = Clipboard.InvokeInStaThread<string>(() => Clipboard.GetText());
			}
			catch
			{
				text = "";
				Console.WriteLine("Failed to get clipboard contents!");
			}
			return text;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009A68 File Offset: 0x00007C68
		private static T InvokeInStaThread<T>(Func<T> callback)
		{
			if (Clipboard.GetApartmentStateSafely() == null)
			{
				return callback.Invoke();
			}
			T result = default(T);
			Thread thread = new Thread(delegate
			{
				result = callback.Invoke();
			});
			thread.SetApartmentState(0);
			thread.Start();
			thread.Join();
			return result;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009ACC File Offset: 0x00007CCC
		private static void InvokeInStaThread(Action callback)
		{
			if (Clipboard.GetApartmentStateSafely() == null)
			{
				callback.Invoke();
				return;
			}
			Thread thread = new Thread(delegate
			{
				callback.Invoke();
			});
			thread.SetApartmentState(0);
			thread.Start();
			thread.Join();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009B1C File Offset: 0x00007D1C
		private static ApartmentState GetApartmentStateSafely()
		{
			ApartmentState apartmentState;
			try
			{
				apartmentState = Thread.CurrentThread.GetApartmentState();
			}
			catch
			{
				apartmentState = 1;
			}
			return apartmentState;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009B4C File Offset: 0x00007D4C
		public Clipboard()
		{
		}

		// Token: 0x020000CF RID: 207
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x06000455 RID: 1109 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x06000456 RID: 1110 RVA: 0x0000E49A File Offset: 0x0000C69A
			internal void <SetClipboard>b__0()
			{
				Clipboard.SetText(this.text);
			}

			// Token: 0x040005C2 RID: 1474
			public string text;
		}

		// Token: 0x020000D0 RID: 208
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000457 RID: 1111 RVA: 0x0000E4A7 File Offset: 0x0000C6A7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000458 RID: 1112 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c()
			{
			}

			// Token: 0x06000459 RID: 1113 RVA: 0x0000E4B3 File Offset: 0x0000C6B3
			internal string <TryToGetClipboardText>b__2_0()
			{
				return Clipboard.GetText();
			}

			// Token: 0x040005C3 RID: 1475
			public static readonly Clipboard.<>c <>9 = new Clipboard.<>c();

			// Token: 0x040005C4 RID: 1476
			public static Func<string> <>9__2_0;
		}

		// Token: 0x020000D1 RID: 209
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0<T>
		{
			// Token: 0x0600045A RID: 1114 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x0600045B RID: 1115 RVA: 0x0000E4BA File Offset: 0x0000C6BA
			internal void <InvokeInStaThread>b__0()
			{
				this.result = this.callback.Invoke();
			}

			// Token: 0x040005C5 RID: 1477
			public T result;

			// Token: 0x040005C6 RID: 1478
			public Func<T> callback;
		}

		// Token: 0x020000D2 RID: 210
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x0600045C RID: 1116 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x0600045D RID: 1117 RVA: 0x0000E4CD File Offset: 0x0000C6CD
			internal void <InvokeInStaThread>b__0()
			{
				this.callback.Invoke();
			}

			// Token: 0x040005C7 RID: 1479
			public Action callback;
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x0200049D RID: 1181
	[ComVisible(true)]
	public sealed class RNGCryptoServiceProvider : RandomNumberGenerator
	{
		// Token: 0x060030D3 RID: 12499 RVA: 0x000B5607 File Offset: 0x000B3807
		static RNGCryptoServiceProvider()
		{
			if (RNGCryptoServiceProvider.RngOpen())
			{
				RNGCryptoServiceProvider._lock = new object();
			}
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000B561A File Offset: 0x000B381A
		public RNGCryptoServiceProvider()
		{
			this._handle = RNGCryptoServiceProvider.RngInitialize(null, IntPtr.Zero);
			this.Check();
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000B563C File Offset: 0x000B383C
		public unsafe RNGCryptoServiceProvider(byte[] rgb)
		{
			fixed (byte[] array = rgb)
			{
				byte* ptr;
				if (rgb == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				this._handle = RNGCryptoServiceProvider.RngInitialize(ptr, (rgb != null) ? ((IntPtr)rgb.Length) : IntPtr.Zero);
			}
			this.Check();
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000B561A File Offset: 0x000B381A
		public RNGCryptoServiceProvider(CspParameters cspParams)
		{
			this._handle = RNGCryptoServiceProvider.RngInitialize(null, IntPtr.Zero);
			this.Check();
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000B5690 File Offset: 0x000B3890
		public unsafe RNGCryptoServiceProvider(string str)
		{
			if (str == null)
			{
				this._handle = RNGCryptoServiceProvider.RngInitialize(null, IntPtr.Zero);
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes(str);
				byte[] array;
				byte* ptr;
				if ((array = bytes) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				this._handle = RNGCryptoServiceProvider.RngInitialize(ptr, (IntPtr)bytes.Length);
				array = null;
			}
			this.Check();
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000B56FA File Offset: 0x000B38FA
		private void Check()
		{
			if (this._handle == IntPtr.Zero)
			{
				throw new CryptographicException(Locale.GetText("Couldn't access random source."));
			}
		}

		// Token: 0x060030D9 RID: 12505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RngOpen();

		// Token: 0x060030DA RID: 12506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr RngInitialize(byte* seed, IntPtr seed_length);

		// Token: 0x060030DB RID: 12507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr RngGetBytes(IntPtr handle, byte* data, IntPtr data_length);

		// Token: 0x060030DC RID: 12508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RngClose(IntPtr handle);

		// Token: 0x060030DD RID: 12509 RVA: 0x000B5720 File Offset: 0x000B3920
		public unsafe override void GetBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			fixed (byte[] array = data)
			{
				byte* ptr;
				if (data == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (RNGCryptoServiceProvider._lock == null)
				{
					this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, ptr, (IntPtr)((long)data.Length));
				}
				else
				{
					object @lock = RNGCryptoServiceProvider._lock;
					lock (@lock)
					{
						this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, ptr, (IntPtr)((long)data.Length));
					}
				}
			}
			this.Check();
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000B57C4 File Offset: 0x000B39C4
		internal unsafe void GetBytes(byte* data, IntPtr data_length)
		{
			if (RNGCryptoServiceProvider._lock == null)
			{
				this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, data, data_length);
			}
			else
			{
				object @lock = RNGCryptoServiceProvider._lock;
				lock (@lock)
				{
					this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, data, data_length);
				}
			}
			this.Check();
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000B5834 File Offset: 0x000B3A34
		public unsafe override void GetNonZeroBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array = new byte[(long)data.Length * 2L];
			long num = 0L;
			while (num < (long)data.Length)
			{
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, ptr, (IntPtr)((long)array.Length));
				array2 = null;
				this.Check();
				long num2 = 0L;
				while (num2 < (long)array.Length && num != (long)data.Length)
				{
					checked
					{
						if (array[(int)((IntPtr)num2)] != 0)
						{
							long num3 = num;
							num = unchecked(num3 + 1L);
							data[(int)((IntPtr)num3)] = array[(int)((IntPtr)num2)];
						}
					}
					num2 += 1L;
				}
			}
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000B58D0 File Offset: 0x000B3AD0
		~RNGCryptoServiceProvider()
		{
			if (this._handle != IntPtr.Zero)
			{
				RNGCryptoServiceProvider.RngClose(this._handle);
				this._handle = IntPtr.Zero;
			}
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000B5920 File Offset: 0x000B3B20
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x040021F0 RID: 8688
		private static object _lock;

		// Token: 0x040021F1 RID: 8689
		private IntPtr _handle;
	}
}

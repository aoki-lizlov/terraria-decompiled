using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Buffers.Text
{
	// Token: 0x02000B56 RID: 2902
	internal ref struct NumberBuffer
	{
		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06006A93 RID: 27283 RVA: 0x0016EAFD File Offset: 0x0016CCFD
		public Span<byte> Digits
		{
			get
			{
				return new Span<byte>(Unsafe.AsPointer<byte>(ref this._b0), 51);
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06006A94 RID: 27284 RVA: 0x0016EB11 File Offset: 0x0016CD11
		public unsafe byte* UnsafeDigits
		{
			get
			{
				return (byte*)Unsafe.AsPointer<byte>(ref this._b0);
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06006A95 RID: 27285 RVA: 0x0016EB1E File Offset: 0x0016CD1E
		public int NumDigits
		{
			get
			{
				return this.Digits.IndexOf(0);
			}
		}

		// Token: 0x06006A96 RID: 27286 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("DEBUG")]
		public void CheckConsistency()
		{
		}

		// Token: 0x06006A97 RID: 27287 RVA: 0x0016EB2C File Offset: 0x0016CD2C
		public unsafe override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append('"');
			Span<byte> digits = this.Digits;
			for (int i = 0; i < 51; i++)
			{
				byte b = *digits[i];
				if (b == 0)
				{
					break;
				}
				stringBuilder.Append((char)b);
			}
			stringBuilder.Append('"');
			stringBuilder.Append(", Scale = " + this.Scale.ToString());
			stringBuilder.Append(", IsNegative   = " + this.IsNegative.ToString());
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x04003D21 RID: 15649
		public int Scale;

		// Token: 0x04003D22 RID: 15650
		public bool IsNegative;

		// Token: 0x04003D23 RID: 15651
		public const int BufferSize = 51;

		// Token: 0x04003D24 RID: 15652
		private byte _b0;

		// Token: 0x04003D25 RID: 15653
		private byte _b1;

		// Token: 0x04003D26 RID: 15654
		private byte _b2;

		// Token: 0x04003D27 RID: 15655
		private byte _b3;

		// Token: 0x04003D28 RID: 15656
		private byte _b4;

		// Token: 0x04003D29 RID: 15657
		private byte _b5;

		// Token: 0x04003D2A RID: 15658
		private byte _b6;

		// Token: 0x04003D2B RID: 15659
		private byte _b7;

		// Token: 0x04003D2C RID: 15660
		private byte _b8;

		// Token: 0x04003D2D RID: 15661
		private byte _b9;

		// Token: 0x04003D2E RID: 15662
		private byte _b10;

		// Token: 0x04003D2F RID: 15663
		private byte _b11;

		// Token: 0x04003D30 RID: 15664
		private byte _b12;

		// Token: 0x04003D31 RID: 15665
		private byte _b13;

		// Token: 0x04003D32 RID: 15666
		private byte _b14;

		// Token: 0x04003D33 RID: 15667
		private byte _b15;

		// Token: 0x04003D34 RID: 15668
		private byte _b16;

		// Token: 0x04003D35 RID: 15669
		private byte _b17;

		// Token: 0x04003D36 RID: 15670
		private byte _b18;

		// Token: 0x04003D37 RID: 15671
		private byte _b19;

		// Token: 0x04003D38 RID: 15672
		private byte _b20;

		// Token: 0x04003D39 RID: 15673
		private byte _b21;

		// Token: 0x04003D3A RID: 15674
		private byte _b22;

		// Token: 0x04003D3B RID: 15675
		private byte _b23;

		// Token: 0x04003D3C RID: 15676
		private byte _b24;

		// Token: 0x04003D3D RID: 15677
		private byte _b25;

		// Token: 0x04003D3E RID: 15678
		private byte _b26;

		// Token: 0x04003D3F RID: 15679
		private byte _b27;

		// Token: 0x04003D40 RID: 15680
		private byte _b28;

		// Token: 0x04003D41 RID: 15681
		private byte _b29;

		// Token: 0x04003D42 RID: 15682
		private byte _b30;

		// Token: 0x04003D43 RID: 15683
		private byte _b31;

		// Token: 0x04003D44 RID: 15684
		private byte _b32;

		// Token: 0x04003D45 RID: 15685
		private byte _b33;

		// Token: 0x04003D46 RID: 15686
		private byte _b34;

		// Token: 0x04003D47 RID: 15687
		private byte _b35;

		// Token: 0x04003D48 RID: 15688
		private byte _b36;

		// Token: 0x04003D49 RID: 15689
		private byte _b37;

		// Token: 0x04003D4A RID: 15690
		private byte _b38;

		// Token: 0x04003D4B RID: 15691
		private byte _b39;

		// Token: 0x04003D4C RID: 15692
		private byte _b40;

		// Token: 0x04003D4D RID: 15693
		private byte _b41;

		// Token: 0x04003D4E RID: 15694
		private byte _b42;

		// Token: 0x04003D4F RID: 15695
		private byte _b43;

		// Token: 0x04003D50 RID: 15696
		private byte _b44;

		// Token: 0x04003D51 RID: 15697
		private byte _b45;

		// Token: 0x04003D52 RID: 15698
		private byte _b46;

		// Token: 0x04003D53 RID: 15699
		private byte _b47;

		// Token: 0x04003D54 RID: 15700
		private byte _b48;

		// Token: 0x04003D55 RID: 15701
		private byte _b49;

		// Token: 0x04003D56 RID: 15702
		private byte _b50;
	}
}

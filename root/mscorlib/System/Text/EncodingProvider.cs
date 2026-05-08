using System;

namespace System.Text
{
	// Token: 0x02000378 RID: 888
	public abstract class EncodingProvider
	{
		// Token: 0x06002614 RID: 9748 RVA: 0x000025BE File Offset: 0x000007BE
		public EncodingProvider()
		{
		}

		// Token: 0x06002615 RID: 9749
		public abstract Encoding GetEncoding(string name);

		// Token: 0x06002616 RID: 9750
		public abstract Encoding GetEncoding(int codepage);

		// Token: 0x06002617 RID: 9751 RVA: 0x00088204 File Offset: 0x00086404
		public virtual Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(name);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(name).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x00088240 File Offset: 0x00086440
		public virtual Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(codepage);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(codepage).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x0008827C File Offset: 0x0008647C
		internal static void AddProvider(EncodingProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			object obj = EncodingProvider.s_InternalSyncObject;
			lock (obj)
			{
				if (EncodingProvider.s_providers == null)
				{
					EncodingProvider.s_providers = new EncodingProvider[] { provider };
				}
				else if (Array.IndexOf<EncodingProvider>(EncodingProvider.s_providers, provider) < 0)
				{
					EncodingProvider[] array = new EncodingProvider[EncodingProvider.s_providers.Length + 1];
					Array.Copy(EncodingProvider.s_providers, array, EncodingProvider.s_providers.Length);
					array[array.Length - 1] = provider;
					EncodingProvider.s_providers = array;
				}
			}
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x00088328 File Offset: 0x00086528
		internal static Encoding GetEncodingFromProvider(int codepage)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(codepage);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00088368 File Offset: 0x00086568
		internal static Encoding GetEncodingFromProvider(string encodingName)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(encodingName);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000883A8 File Offset: 0x000865A8
		internal static Encoding GetEncodingFromProvider(int codepage, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(codepage, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000883E8 File Offset: 0x000865E8
		internal static Encoding GetEncodingFromProvider(string encodingName, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(encodingName, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x00088428 File Offset: 0x00086628
		// Note: this type is marked as 'beforefieldinit'.
		static EncodingProvider()
		{
		}

		// Token: 0x04001C92 RID: 7314
		private static object s_InternalSyncObject = new object();

		// Token: 0x04001C93 RID: 7315
		private static volatile EncodingProvider[] s_providers;
	}
}

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Text
{
	// Token: 0x02000393 RID: 915
	internal static class EncodingHelper
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x000927A4 File Offset: 0x000909A4
		internal static Encoding UTF8Unmarked
		{
			get
			{
				if (EncodingHelper.utf8EncodingWithoutMarkers == null)
				{
					object obj = EncodingHelper.lockobj;
					lock (obj)
					{
						if (EncodingHelper.utf8EncodingWithoutMarkers == null)
						{
							EncodingHelper.utf8EncodingWithoutMarkers = new UTF8Encoding(false, false);
							EncodingHelper.utf8EncodingWithoutMarkers.setReadOnly(true);
						}
					}
				}
				return EncodingHelper.utf8EncodingWithoutMarkers;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x00092814 File Offset: 0x00090A14
		internal static Encoding UTF8UnmarkedUnsafe
		{
			get
			{
				if (EncodingHelper.utf8EncodingUnsafe == null)
				{
					object obj = EncodingHelper.lockobj;
					lock (obj)
					{
						if (EncodingHelper.utf8EncodingUnsafe == null)
						{
							EncodingHelper.utf8EncodingUnsafe = new UTF8Encoding(false, false);
							EncodingHelper.utf8EncodingUnsafe.setReadOnly(false);
							EncodingHelper.utf8EncodingUnsafe.DecoderFallback = new DecoderReplacementFallback(string.Empty);
							EncodingHelper.utf8EncodingUnsafe.setReadOnly(true);
						}
					}
				}
				return EncodingHelper.utf8EncodingUnsafe;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x000928A4 File Offset: 0x00090AA4
		internal static Encoding BigEndianUTF32
		{
			get
			{
				if (EncodingHelper.bigEndianUTF32Encoding == null)
				{
					object obj = EncodingHelper.lockobj;
					lock (obj)
					{
						if (EncodingHelper.bigEndianUTF32Encoding == null)
						{
							EncodingHelper.bigEndianUTF32Encoding = new UTF32Encoding(true, true);
							EncodingHelper.bigEndianUTF32Encoding.setReadOnly(true);
						}
					}
				}
				return EncodingHelper.bigEndianUTF32Encoding;
			}
		}

		// Token: 0x060027FA RID: 10234
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string InternalCodePage(ref int code_page);

		// Token: 0x060027FB RID: 10235 RVA: 0x00092914 File Offset: 0x00090B14
		internal static Encoding GetDefaultEncoding()
		{
			Encoding encoding = null;
			int num = 1;
			string text = EncodingHelper.InternalCodePage(ref num);
			try
			{
				if (num == -1)
				{
					encoding = Encoding.GetEncoding(text);
				}
				else
				{
					num &= 268435455;
					switch (num)
					{
					case 1:
						num = 20127;
						break;
					case 2:
						num = 65007;
						break;
					case 3:
						num = 65001;
						break;
					case 4:
						num = 1200;
						break;
					case 5:
						num = 1201;
						break;
					case 6:
						num = 1252;
						break;
					}
					encoding = Encoding.GetEncoding(num);
				}
			}
			catch (NotSupportedException)
			{
				encoding = EncodingHelper.UTF8Unmarked;
			}
			catch (ArgumentException)
			{
				encoding = EncodingHelper.UTF8Unmarked;
			}
			return encoding;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000929CC File Offset: 0x00090BCC
		internal static object InvokeI18N(string name, params object[] args)
		{
			object obj = EncodingHelper.lockobj;
			object obj2;
			lock (obj)
			{
				if (EncodingHelper.i18nDisabled)
				{
					obj2 = null;
				}
				else
				{
					if (EncodingHelper.i18nAssembly == null)
					{
						try
						{
							try
							{
								EncodingHelper.i18nAssembly = Assembly.Load("I18N, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756");
							}
							catch (NotImplementedException)
							{
								EncodingHelper.i18nDisabled = true;
								return null;
							}
							if (EncodingHelper.i18nAssembly == null)
							{
								return null;
							}
						}
						catch (SystemException)
						{
							return null;
						}
					}
					Type type;
					try
					{
						type = EncodingHelper.i18nAssembly.GetType("I18N.Common.Manager");
					}
					catch (NotImplementedException)
					{
						EncodingHelper.i18nDisabled = true;
						return null;
					}
					if (type == null)
					{
						obj2 = null;
					}
					else
					{
						object obj3;
						try
						{
							obj3 = type.InvokeMember("PrimaryManager", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, null, null, null, null);
							if (obj3 == null)
							{
								return null;
							}
						}
						catch (MissingMethodException)
						{
							return null;
						}
						catch (SecurityException)
						{
							return null;
						}
						catch (NotImplementedException)
						{
							EncodingHelper.i18nDisabled = true;
							return null;
						}
						try
						{
							obj2 = type.InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, obj3, args, null, null, null);
						}
						catch (MissingMethodException)
						{
							obj2 = null;
						}
						catch (SecurityException)
						{
							obj2 = null;
						}
					}
				}
			}
			return obj2;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x00092B4C File Offset: 0x00090D4C
		// Note: this type is marked as 'beforefieldinit'.
		static EncodingHelper()
		{
		}

		// Token: 0x04001D53 RID: 7507
		private static volatile Encoding utf8EncodingWithoutMarkers;

		// Token: 0x04001D54 RID: 7508
		private static volatile Encoding utf8EncodingUnsafe;

		// Token: 0x04001D55 RID: 7509
		private static volatile Encoding bigEndianUTF32Encoding;

		// Token: 0x04001D56 RID: 7510
		private static readonly object lockobj = new object();

		// Token: 0x04001D57 RID: 7511
		private static Assembly i18nAssembly;

		// Token: 0x04001D58 RID: 7512
		private static bool i18nDisabled;
	}
}

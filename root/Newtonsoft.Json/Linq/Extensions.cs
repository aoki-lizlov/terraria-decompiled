using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000B3 RID: 179
	public static class Extensions
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x00022F66 File Offset: 0x00021166
		public static IJEnumerable<JToken> Ancestors<T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T j) => j.Ancestors()).AsJEnumerable();
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00022F9D File Offset: 0x0002119D
		public static IJEnumerable<JToken> AncestorsAndSelf<T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T j) => j.AncestorsAndSelf()).AsJEnumerable();
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00022FD4 File Offset: 0x000211D4
		public static IJEnumerable<JToken> Descendants<T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T j) => j.Descendants()).AsJEnumerable();
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002300B File Offset: 0x0002120B
		public static IJEnumerable<JToken> DescendantsAndSelf<T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T j) => j.DescendantsAndSelf()).AsJEnumerable();
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00023042 File Offset: 0x00021242
		public static IJEnumerable<JProperty> Properties(this IEnumerable<JObject> source)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<JObject, JProperty>(source, (JObject d) => d.Properties()).AsJEnumerable<JProperty>();
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00023079 File Offset: 0x00021279
		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source, object key)
		{
			return source.Values(key).AsJEnumerable();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00023087 File Offset: 0x00021287
		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00023090 File Offset: 0x00021290
		public static IEnumerable<U> Values<U>(this IEnumerable<JToken> source, object key)
		{
			return source.Values(key);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00023099 File Offset: 0x00021299
		public static IEnumerable<U> Values<U>(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000230A2 File Offset: 0x000212A2
		public static U Value<U>(this IEnumerable<JToken> value)
		{
			return value.Value<JToken, U>();
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000230AA File Offset: 0x000212AA
		public static U Value<T, U>(this IEnumerable<T> value) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Source value must be a JToken.");
			}
			return jtoken.Convert<JToken, U>();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000230D0 File Offset: 0x000212D0
		internal static IEnumerable<U> Values<T, U>(this IEnumerable<T> source, object key) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			if (key == null)
			{
				foreach (T token in source)
				{
					JValue jvalue = token as JValue;
					if (jvalue != null)
					{
						yield return jvalue.Convert<JValue, U>();
					}
					else
					{
						foreach (JToken jtoken in token.Children())
						{
							yield return jtoken.Convert<JToken, U>();
						}
						IEnumerator<JToken> enumerator2 = null;
					}
					token = default(T);
				}
				IEnumerator<T> enumerator = null;
			}
			else
			{
				foreach (T t in source)
				{
					JToken jtoken2 = t[key];
					if (jtoken2 != null)
					{
						yield return jtoken2.Convert<JToken, U>();
					}
				}
				IEnumerator<T> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000230E7 File Offset: 0x000212E7
		public static IJEnumerable<JToken> Children<T>(this IEnumerable<T> source) where T : JToken
		{
			return source.Children<T, JToken>().AsJEnumerable();
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000230F4 File Offset: 0x000212F4
		public static IEnumerable<U> Children<T, U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T c) => c.Children()).Convert<JToken, U>();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002312B File Offset: 0x0002132B
		internal static IEnumerable<U> Convert<T, U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			foreach (T t in source)
			{
				yield return t.Convert<JToken, U>();
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002313C File Offset: 0x0002133C
		internal static U Convert<T, U>(this T token) where T : JToken
		{
			if (token == null)
			{
				return default(U);
			}
			if (token is U && typeof(U) != typeof(IComparable) && typeof(U) != typeof(IFormattable))
			{
				return (U)((object)token);
			}
			JValue jvalue = token as JValue;
			if (jvalue == null)
			{
				throw new InvalidCastException("Cannot cast {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, token.GetType(), typeof(T)));
			}
			if (jvalue.Value is U)
			{
				return (U)((object)jvalue.Value);
			}
			Type type = typeof(U);
			if (ReflectionUtils.IsNullableType(type))
			{
				if (jvalue.Value == null)
				{
					return default(U);
				}
				type = Nullable.GetUnderlyingType(type);
			}
			return (U)((object)global::System.Convert.ChangeType(jvalue.Value, type, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002323E File Offset: 0x0002143E
		public static IJEnumerable<JToken> AsJEnumerable(this IEnumerable<JToken> source)
		{
			return source.AsJEnumerable<JToken>();
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00023246 File Offset: 0x00021446
		public static IJEnumerable<T> AsJEnumerable<T>(this IEnumerable<T> source) where T : JToken
		{
			if (source == null)
			{
				return null;
			}
			if (source is IJEnumerable<T>)
			{
				return (IJEnumerable<T>)source;
			}
			return new JEnumerable<T>(source);
		}

		// Token: 0x02000151 RID: 337
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__0<T> where T : JToken
		{
			// Token: 0x06000D14 RID: 3348 RVA: 0x00031754 File Offset: 0x0002F954
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__0()
			{
			}

			// Token: 0x06000D15 RID: 3349 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__0()
			{
			}

			// Token: 0x06000D16 RID: 3350 RVA: 0x00031760 File Offset: 0x0002F960
			internal IEnumerable<JToken> <Ancestors>b__0_0(T j)
			{
				return j.Ancestors();
			}

			// Token: 0x040004D6 RID: 1238
			public static readonly Extensions.<>c__0<T> <>9 = new Extensions.<>c__0<T>();

			// Token: 0x040004D7 RID: 1239
			public static Func<T, IEnumerable<JToken>> <>9__0_0;
		}

		// Token: 0x02000152 RID: 338
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__1<T> where T : JToken
		{
			// Token: 0x06000D17 RID: 3351 RVA: 0x0003176D File Offset: 0x0002F96D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__1()
			{
			}

			// Token: 0x06000D18 RID: 3352 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__1()
			{
			}

			// Token: 0x06000D19 RID: 3353 RVA: 0x00031779 File Offset: 0x0002F979
			internal IEnumerable<JToken> <AncestorsAndSelf>b__1_0(T j)
			{
				return j.AncestorsAndSelf();
			}

			// Token: 0x040004D8 RID: 1240
			public static readonly Extensions.<>c__1<T> <>9 = new Extensions.<>c__1<T>();

			// Token: 0x040004D9 RID: 1241
			public static Func<T, IEnumerable<JToken>> <>9__1_0;
		}

		// Token: 0x02000153 RID: 339
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__2<T> where T : JContainer
		{
			// Token: 0x06000D1A RID: 3354 RVA: 0x00031786 File Offset: 0x0002F986
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__2()
			{
			}

			// Token: 0x06000D1B RID: 3355 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__2()
			{
			}

			// Token: 0x06000D1C RID: 3356 RVA: 0x00031792 File Offset: 0x0002F992
			internal IEnumerable<JToken> <Descendants>b__2_0(T j)
			{
				return j.Descendants();
			}

			// Token: 0x040004DA RID: 1242
			public static readonly Extensions.<>c__2<T> <>9 = new Extensions.<>c__2<T>();

			// Token: 0x040004DB RID: 1243
			public static Func<T, IEnumerable<JToken>> <>9__2_0;
		}

		// Token: 0x02000154 RID: 340
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__3<T> where T : JContainer
		{
			// Token: 0x06000D1D RID: 3357 RVA: 0x0003179F File Offset: 0x0002F99F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__3()
			{
			}

			// Token: 0x06000D1E RID: 3358 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__3()
			{
			}

			// Token: 0x06000D1F RID: 3359 RVA: 0x000317AB File Offset: 0x0002F9AB
			internal IEnumerable<JToken> <DescendantsAndSelf>b__3_0(T j)
			{
				return j.DescendantsAndSelf();
			}

			// Token: 0x040004DC RID: 1244
			public static readonly Extensions.<>c__3<T> <>9 = new Extensions.<>c__3<T>();

			// Token: 0x040004DD RID: 1245
			public static Func<T, IEnumerable<JToken>> <>9__3_0;
		}

		// Token: 0x02000155 RID: 341
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000D20 RID: 3360 RVA: 0x000317B8 File Offset: 0x0002F9B8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000D21 RID: 3361 RVA: 0x00008020 File Offset: 0x00006220
			public <>c()
			{
			}

			// Token: 0x06000D22 RID: 3362 RVA: 0x000317C4 File Offset: 0x0002F9C4
			internal IEnumerable<JProperty> <Properties>b__4_0(JObject d)
			{
				return d.Properties();
			}

			// Token: 0x040004DE RID: 1246
			public static readonly Extensions.<>c <>9 = new Extensions.<>c();

			// Token: 0x040004DF RID: 1247
			public static Func<JObject, IEnumerable<JProperty>> <>9__4_0;
		}

		// Token: 0x02000156 RID: 342
		[CompilerGenerated]
		private sealed class <Values>d__11<T, U> : IEnumerable<U>, IEnumerable, IEnumerator<U>, IDisposable, IEnumerator where T : JToken
		{
			// Token: 0x06000D23 RID: 3363 RVA: 0x000317CC File Offset: 0x0002F9CC
			[DebuggerHidden]
			public <Values>d__11(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000D24 RID: 3364 RVA: 0x000317EC File Offset: 0x0002F9EC
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				switch (num)
				{
				case -5:
				case 3:
					break;
				case -4:
				case -3:
				case 1:
				case 2:
					try
					{
						if (num != -4 && num != 2)
						{
							return;
						}
						try
						{
							return;
						}
						finally
						{
							this.<>m__Finally2();
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
					break;
				case -2:
				case -1:
				case 0:
					return;
				default:
					return;
				}
				try
				{
				}
				finally
				{
					this.<>m__Finally3();
				}
			}

			// Token: 0x06000D25 RID: 3365 RVA: 0x00031880 File Offset: 0x0002FA80
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						ValidationUtils.ArgumentNotNull(source, "source");
						if (key == null)
						{
							enumerator = source.GetEnumerator();
							this.<>1__state = -3;
							goto IL_012B;
						}
						enumerator = source.GetEnumerator();
						this.<>1__state = -5;
						break;
					case 1:
						this.<>1__state = -3;
						goto IL_011F;
					case 2:
						this.<>1__state = -4;
						goto IL_0105;
					case 3:
						this.<>1__state = -5;
						break;
					default:
						return false;
					}
					while (enumerator.MoveNext())
					{
						JToken jtoken = enumerator.Current[key];
						if (jtoken != null)
						{
							this.<>2__current = jtoken.Convert<JToken, U>();
							this.<>1__state = 3;
							return true;
						}
					}
					this.<>m__Finally3();
					enumerator = null;
					goto IL_01C0;
					IL_0105:
					if (enumerator2.MoveNext())
					{
						JToken jtoken2 = enumerator2.Current;
						this.<>2__current = jtoken2.Convert<JToken, U>();
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally2();
					enumerator2 = null;
					IL_011F:
					token = default(T);
					IL_012B:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
					}
					else
					{
						token = enumerator.Current;
						JValue jvalue = token as JValue;
						if (jvalue != null)
						{
							this.<>2__current = jvalue.Convert<JValue, U>();
							this.<>1__state = 1;
							return true;
						}
						enumerator2 = token.Children().GetEnumerator();
						this.<>1__state = -4;
						goto IL_0105;
					}
					IL_01C0:
					flag = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06000D26 RID: 3366 RVA: 0x00031A78 File Offset: 0x0002FC78
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06000D27 RID: 3367 RVA: 0x00031A94 File Offset: 0x0002FC94
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x06000D28 RID: 3368 RVA: 0x00031A78 File Offset: 0x0002FC78
			private void <>m__Finally3()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00031AB1 File Offset: 0x0002FCB1
			U IEnumerator<U>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D2A RID: 3370 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700028E RID: 654
			// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00031AB9 File Offset: 0x0002FCB9
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D2C RID: 3372 RVA: 0x00031AC8 File Offset: 0x0002FCC8
			[DebuggerHidden]
			IEnumerator<U> IEnumerable<U>.GetEnumerator()
			{
				Extensions.<Values>d__11<T, U> <Values>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<Values>d__ = this;
				}
				else
				{
					<Values>d__ = new Extensions.<Values>d__11<T, U>(0);
				}
				<Values>d__.source = source;
				<Values>d__.key = key;
				return <Values>d__;
			}

			// Token: 0x06000D2D RID: 3373 RVA: 0x00031B1C File Offset: 0x0002FD1C
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<U>.GetEnumerator();
			}

			// Token: 0x040004E0 RID: 1248
			private int <>1__state;

			// Token: 0x040004E1 RID: 1249
			private U <>2__current;

			// Token: 0x040004E2 RID: 1250
			private int <>l__initialThreadId;

			// Token: 0x040004E3 RID: 1251
			private IEnumerable<T> source;

			// Token: 0x040004E4 RID: 1252
			public IEnumerable<T> <>3__source;

			// Token: 0x040004E5 RID: 1253
			private object key;

			// Token: 0x040004E6 RID: 1254
			public object <>3__key;

			// Token: 0x040004E7 RID: 1255
			private T <token>5__1;

			// Token: 0x040004E8 RID: 1256
			private IEnumerator<T> <>7__wrap1;

			// Token: 0x040004E9 RID: 1257
			private IEnumerator<JToken> <>7__wrap2;
		}

		// Token: 0x02000157 RID: 343
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__13<T, U> where T : JToken
		{
			// Token: 0x06000D2E RID: 3374 RVA: 0x00031B24 File Offset: 0x0002FD24
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__13()
			{
			}

			// Token: 0x06000D2F RID: 3375 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__13()
			{
			}

			// Token: 0x06000D30 RID: 3376 RVA: 0x00031B30 File Offset: 0x0002FD30
			internal IEnumerable<JToken> <Children>b__13_0(T c)
			{
				return c.Children();
			}

			// Token: 0x040004EA RID: 1258
			public static readonly Extensions.<>c__13<T, U> <>9 = new Extensions.<>c__13<T, U>();

			// Token: 0x040004EB RID: 1259
			public static Func<T, IEnumerable<JToken>> <>9__13_0;
		}

		// Token: 0x02000158 RID: 344
		[CompilerGenerated]
		private sealed class <Convert>d__14<T, U> : IEnumerable<U>, IEnumerable, IEnumerator<U>, IDisposable, IEnumerator where T : JToken
		{
			// Token: 0x06000D31 RID: 3377 RVA: 0x00031B42 File Offset: 0x0002FD42
			[DebuggerHidden]
			public <Convert>d__14(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000D32 RID: 3378 RVA: 0x00031B64 File Offset: 0x0002FD64
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000D33 RID: 3379 RVA: 0x00031B9C File Offset: 0x0002FD9C
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						ValidationUtils.ArgumentNotNull(source, "source");
						enumerator = source.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						flag = false;
					}
					else
					{
						T t = enumerator.Current;
						this.<>2__current = t.Convert<JToken, U>();
						this.<>1__state = 1;
						flag = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06000D34 RID: 3380 RVA: 0x00031C58 File Offset: 0x0002FE58
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x1700028F RID: 655
			// (get) Token: 0x06000D35 RID: 3381 RVA: 0x00031C74 File Offset: 0x0002FE74
			U IEnumerator<U>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D36 RID: 3382 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000290 RID: 656
			// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00031C7C File Offset: 0x0002FE7C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D38 RID: 3384 RVA: 0x00031C8C File Offset: 0x0002FE8C
			[DebuggerHidden]
			IEnumerator<U> IEnumerable<U>.GetEnumerator()
			{
				Extensions.<Convert>d__14<T, U> <Convert>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<Convert>d__ = this;
				}
				else
				{
					<Convert>d__ = new Extensions.<Convert>d__14<T, U>(0);
				}
				<Convert>d__.source = source;
				return <Convert>d__;
			}

			// Token: 0x06000D39 RID: 3385 RVA: 0x00031CD4 File Offset: 0x0002FED4
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<U>.GetEnumerator();
			}

			// Token: 0x040004EC RID: 1260
			private int <>1__state;

			// Token: 0x040004ED RID: 1261
			private U <>2__current;

			// Token: 0x040004EE RID: 1262
			private int <>l__initialThreadId;

			// Token: 0x040004EF RID: 1263
			private IEnumerable<T> source;

			// Token: 0x040004F0 RID: 1264
			public IEnumerable<T> <>3__source;

			// Token: 0x040004F1 RID: 1265
			private IEnumerator<T> <>7__wrap1;
		}
	}
}

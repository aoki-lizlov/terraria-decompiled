using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace FullSerializer.Internal
{
	// Token: 0x0200002D RID: 45
	public static class fsPortableReflection
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00007607 File Offset: 0x00005807
		public static bool HasAttribute(MemberInfo element, Type attributeType)
		{
			return fsPortableReflection.GetAttribute(element, attributeType, true) != null;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007614 File Offset: 0x00005814
		public static bool HasAttribute<TAttribute>(MemberInfo element)
		{
			return fsPortableReflection.HasAttribute(element, typeof(TAttribute));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007628 File Offset: 0x00005828
		public static Attribute GetAttribute(MemberInfo element, Type attributeType, bool shouldCache)
		{
			fsPortableReflection.AttributeQuery attributeQuery = new fsPortableReflection.AttributeQuery
			{
				MemberInfo = element,
				AttributeType = attributeType
			};
			Attribute attribute;
			if (!fsPortableReflection._cachedAttributeQueries.TryGetValue(attributeQuery, ref attribute))
			{
				attribute = (Attribute)Enumerable.FirstOrDefault<object>(element.GetCustomAttributes(attributeType, true));
				if (shouldCache)
				{
					fsPortableReflection._cachedAttributeQueries[attributeQuery] = attribute;
				}
			}
			return attribute;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007681 File Offset: 0x00005881
		public static TAttribute GetAttribute<TAttribute>(MemberInfo element, bool shouldCache) where TAttribute : Attribute
		{
			return (TAttribute)((object)fsPortableReflection.GetAttribute(element, typeof(TAttribute), shouldCache));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007699 File Offset: 0x00005899
		public static TAttribute GetAttribute<TAttribute>(MemberInfo element) where TAttribute : Attribute
		{
			return fsPortableReflection.GetAttribute<TAttribute>(element, true);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000076A4 File Offset: 0x000058A4
		public static PropertyInfo GetDeclaredProperty(this Type type, string propertyName)
		{
			PropertyInfo[] declaredProperties = type.GetDeclaredProperties();
			for (int i = 0; i < declaredProperties.Length; i++)
			{
				if (declaredProperties[i].Name == propertyName)
				{
					return declaredProperties[i];
				}
			}
			return null;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000076DC File Offset: 0x000058DC
		public static MethodInfo GetDeclaredMethod(this Type type, string methodName)
		{
			MethodInfo[] declaredMethods = type.GetDeclaredMethods();
			for (int i = 0; i < declaredMethods.Length; i++)
			{
				if (declaredMethods[i].Name == methodName)
				{
					return declaredMethods[i];
				}
			}
			return null;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007714 File Offset: 0x00005914
		public static ConstructorInfo GetDeclaredConstructor(this Type type, Type[] parameters)
		{
			foreach (ConstructorInfo constructorInfo in type.GetDeclaredConstructors())
			{
				ParameterInfo[] parameters2 = constructorInfo.GetParameters();
				if (parameters.Length == parameters2.Length)
				{
					for (int j = 0; j < parameters2.Length; j++)
					{
						parameters2[j].ParameterType != parameters[j];
					}
					return constructorInfo;
				}
			}
			return null;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007771 File Offset: 0x00005971
		public static ConstructorInfo[] GetDeclaredConstructors(this Type type)
		{
			return type.GetConstructors(fsPortableReflection.DeclaredFlags);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007780 File Offset: 0x00005980
		public static MemberInfo[] GetFlattenedMember(this Type type, string memberName)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			while (type != null)
			{
				MemberInfo[] declaredMembers = type.GetDeclaredMembers();
				for (int i = 0; i < declaredMembers.Length; i++)
				{
					if (declaredMembers[i].Name == memberName)
					{
						list.Add(declaredMembers[i]);
					}
				}
				type = type.Resolve().BaseType;
			}
			return list.ToArray();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000077E0 File Offset: 0x000059E0
		public static MethodInfo GetFlattenedMethod(this Type type, string methodName)
		{
			while (type != null)
			{
				MethodInfo[] declaredMethods = type.GetDeclaredMethods();
				for (int i = 0; i < declaredMethods.Length; i++)
				{
					if (declaredMethods[i].Name == methodName)
					{
						return declaredMethods[i];
					}
				}
				type = type.Resolve().BaseType;
			}
			return null;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000782F File Offset: 0x00005A2F
		public static IEnumerable<MethodInfo> GetFlattenedMethods(this Type type, string methodName)
		{
			while (type != null)
			{
				MethodInfo[] methods = type.GetDeclaredMethods();
				int num;
				for (int i = 0; i < methods.Length; i = num)
				{
					if (methods[i].Name == methodName)
					{
						yield return methods[i];
					}
					num = i + 1;
				}
				type = type.Resolve().BaseType;
				methods = null;
			}
			yield break;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007848 File Offset: 0x00005A48
		public static PropertyInfo GetFlattenedProperty(this Type type, string propertyName)
		{
			while (type != null)
			{
				PropertyInfo[] declaredProperties = type.GetDeclaredProperties();
				for (int i = 0; i < declaredProperties.Length; i++)
				{
					if (declaredProperties[i].Name == propertyName)
					{
						return declaredProperties[i];
					}
				}
				type = type.Resolve().BaseType;
			}
			return null;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007898 File Offset: 0x00005A98
		public static MemberInfo GetDeclaredMember(this Type type, string memberName)
		{
			MemberInfo[] declaredMembers = type.GetDeclaredMembers();
			for (int i = 0; i < declaredMembers.Length; i++)
			{
				if (declaredMembers[i].Name == memberName)
				{
					return declaredMembers[i];
				}
			}
			return null;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000078CF File Offset: 0x00005ACF
		public static MethodInfo[] GetDeclaredMethods(this Type type)
		{
			return type.GetMethods(fsPortableReflection.DeclaredFlags);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000078DC File Offset: 0x00005ADC
		public static PropertyInfo[] GetDeclaredProperties(this Type type)
		{
			return type.GetProperties(fsPortableReflection.DeclaredFlags);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000078E9 File Offset: 0x00005AE9
		public static FieldInfo[] GetDeclaredFields(this Type type)
		{
			return type.GetFields(fsPortableReflection.DeclaredFlags);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000078F6 File Offset: 0x00005AF6
		public static MemberInfo[] GetDeclaredMembers(this Type type)
		{
			return type.GetMembers(fsPortableReflection.DeclaredFlags);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007903 File Offset: 0x00005B03
		public static MemberInfo AsMemberInfo(Type type)
		{
			return type;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007906 File Offset: 0x00005B06
		public static bool IsType(MemberInfo member)
		{
			return member is Type;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007911 File Offset: 0x00005B11
		public static Type AsType(MemberInfo member)
		{
			return (Type)member;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007903 File Offset: 0x00005B03
		public static Type Resolve(this Type type)
		{
			return type;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007919 File Offset: 0x00005B19
		// Note: this type is marked as 'beforefieldinit'.
		static fsPortableReflection()
		{
		}

		// Token: 0x04000050 RID: 80
		public static Type[] EmptyTypes = new Type[0];

		// Token: 0x04000051 RID: 81
		private static IDictionary<fsPortableReflection.AttributeQuery, Attribute> _cachedAttributeQueries = new Dictionary<fsPortableReflection.AttributeQuery, Attribute>(new fsPortableReflection.AttributeQueryComparator());

		// Token: 0x04000052 RID: 82
		private static BindingFlags DeclaredFlags = 62;

		// Token: 0x020000BA RID: 186
		private struct AttributeQuery
		{
			// Token: 0x04000253 RID: 595
			public MemberInfo MemberInfo;

			// Token: 0x04000254 RID: 596
			public Type AttributeType;
		}

		// Token: 0x020000BB RID: 187
		private class AttributeQueryComparator : IEqualityComparer<fsPortableReflection.AttributeQuery>
		{
			// Token: 0x060002A4 RID: 676 RVA: 0x00009CA3 File Offset: 0x00007EA3
			public bool Equals(fsPortableReflection.AttributeQuery x, fsPortableReflection.AttributeQuery y)
			{
				return x.MemberInfo == y.MemberInfo && x.AttributeType == y.AttributeType;
			}

			// Token: 0x060002A5 RID: 677 RVA: 0x00009CCB File Offset: 0x00007ECB
			public int GetHashCode(fsPortableReflection.AttributeQuery obj)
			{
				return obj.MemberInfo.GetHashCode() + 17 * obj.AttributeType.GetHashCode();
			}

			// Token: 0x060002A6 RID: 678 RVA: 0x00002493 File Offset: 0x00000693
			public AttributeQueryComparator()
			{
			}
		}

		// Token: 0x020000BC RID: 188
		[CompilerGenerated]
		private sealed class <GetFlattenedMethods>d__16 : IEnumerable<MethodInfo>, IEnumerable, IEnumerator<MethodInfo>, IDisposable, IEnumerator
		{
			// Token: 0x060002A7 RID: 679 RVA: 0x00009CE7 File Offset: 0x00007EE7
			[DebuggerHidden]
			public <GetFlattenedMethods>d__16(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060002A8 RID: 680 RVA: 0x000040C8 File Offset: 0x000022C8
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060002A9 RID: 681 RVA: 0x00009D08 File Offset: 0x00007F08
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					goto IL_00B5;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_0078:
				int num2 = i + 1;
				i = num2;
				IL_0088:
				if (i >= methods.Length)
				{
					type = type.Resolve().BaseType;
					methods = null;
				}
				else
				{
					if (methods[i].Name == methodName)
					{
						this.<>2__current = methods[i];
						this.<>1__state = 1;
						return true;
					}
					goto IL_0078;
				}
				IL_00B5:
				if (!(type != null))
				{
					return false;
				}
				methods = type.GetDeclaredMethods();
				i = 0;
				goto IL_0088;
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060002AA RID: 682 RVA: 0x00009DDC File Offset: 0x00007FDC
			MethodInfo IEnumerator<MethodInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002AB RID: 683 RVA: 0x00009DE4 File Offset: 0x00007FE4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060002AC RID: 684 RVA: 0x00009DDC File Offset: 0x00007FDC
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002AD RID: 685 RVA: 0x00009DEC File Offset: 0x00007FEC
			[DebuggerHidden]
			IEnumerator<MethodInfo> IEnumerable<MethodInfo>.GetEnumerator()
			{
				fsPortableReflection.<GetFlattenedMethods>d__16 <GetFlattenedMethods>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<GetFlattenedMethods>d__ = this;
				}
				else
				{
					<GetFlattenedMethods>d__ = new fsPortableReflection.<GetFlattenedMethods>d__16(0);
				}
				<GetFlattenedMethods>d__.type = type;
				<GetFlattenedMethods>d__.methodName = methodName;
				return <GetFlattenedMethods>d__;
			}

			// Token: 0x060002AE RID: 686 RVA: 0x00009E40 File Offset: 0x00008040
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo>.GetEnumerator();
			}

			// Token: 0x04000255 RID: 597
			private int <>1__state;

			// Token: 0x04000256 RID: 598
			private MethodInfo <>2__current;

			// Token: 0x04000257 RID: 599
			private int <>l__initialThreadId;

			// Token: 0x04000258 RID: 600
			private Type type;

			// Token: 0x04000259 RID: 601
			public Type <>3__type;

			// Token: 0x0400025A RID: 602
			private string methodName;

			// Token: 0x0400025B RID: 603
			public string <>3__methodName;

			// Token: 0x0400025C RID: 604
			private MethodInfo[] <methods>5__2;

			// Token: 0x0400025D RID: 605
			private int <i>5__3;
		}
	}
}

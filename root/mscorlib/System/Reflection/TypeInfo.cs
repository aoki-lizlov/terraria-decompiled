using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020008A0 RID: 2208
	public abstract class TypeInfo : Type, IReflectableType
	{
		// Token: 0x06004A90 RID: 19088 RVA: 0x000EF40B File Offset: 0x000ED60B
		protected TypeInfo()
		{
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x000025CE File Offset: 0x000007CE
		TypeInfo IReflectableType.GetTypeInfo()
		{
			return this;
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x000025CE File Offset: 0x000007CE
		public virtual Type AsType()
		{
			return this;
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x000EFB4E File Offset: 0x000EDD4E
		public virtual Type[] GenericTypeParameters
		{
			get
			{
				if (!this.IsGenericTypeDefinition)
				{
					return Type.EmptyTypes;
				}
				return this.GetGenericArguments();
			}
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x000EFB64 File Offset: 0x000EDD64
		public virtual EventInfo GetDeclaredEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x000EFB6F File Offset: 0x000EDD6F
		public virtual FieldInfo GetDeclaredField(string name)
		{
			return this.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x000EFB7A File Offset: 0x000EDD7A
		public virtual MethodInfo GetDeclaredMethod(string name)
		{
			return base.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x000EFB85 File Offset: 0x000EDD85
		public virtual TypeInfo GetDeclaredNestedType(string name)
		{
			Type nestedType = this.GetNestedType(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (nestedType == null)
			{
				return null;
			}
			return nestedType.GetTypeInfo();
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x000EFB9B File Offset: 0x000EDD9B
		public virtual PropertyInfo GetDeclaredProperty(string name)
		{
			return base.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x000EFBA6 File Offset: 0x000EDDA6
		public virtual IEnumerable<MethodInfo> GetDeclaredMethods(string name)
		{
			foreach (MethodInfo methodInfo in this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo.Name == name)
				{
					yield return methodInfo;
				}
			}
			MethodInfo[] array = null;
			yield break;
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06004A9A RID: 19098 RVA: 0x000EFBBD File Offset: 0x000EDDBD
		public virtual IEnumerable<ConstructorInfo> DeclaredConstructors
		{
			get
			{
				return this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06004A9B RID: 19099 RVA: 0x000EFBC7 File Offset: 0x000EDDC7
		public virtual IEnumerable<EventInfo> DeclaredEvents
		{
			get
			{
				return this.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06004A9C RID: 19100 RVA: 0x000EFBD1 File Offset: 0x000EDDD1
		public virtual IEnumerable<FieldInfo> DeclaredFields
		{
			get
			{
				return this.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06004A9D RID: 19101 RVA: 0x000EFBDB File Offset: 0x000EDDDB
		public virtual IEnumerable<MemberInfo> DeclaredMembers
		{
			get
			{
				return this.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06004A9E RID: 19102 RVA: 0x000EFBE5 File Offset: 0x000EDDE5
		public virtual IEnumerable<MethodInfo> DeclaredMethods
		{
			get
			{
				return this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06004A9F RID: 19103 RVA: 0x000EFBEF File Offset: 0x000EDDEF
		public virtual IEnumerable<TypeInfo> DeclaredNestedTypes
		{
			get
			{
				foreach (Type type in this.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					yield return type.GetTypeInfo();
				}
				Type[] array = null;
				yield break;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06004AA0 RID: 19104 RVA: 0x000EFBFF File Offset: 0x000EDDFF
		public virtual IEnumerable<PropertyInfo> DeclaredProperties
		{
			get
			{
				return this.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06004AA1 RID: 19105 RVA: 0x000EFC09 File Offset: 0x000EDE09
		public virtual IEnumerable<Type> ImplementedInterfaces
		{
			get
			{
				return this.GetInterfaces();
			}
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x000EFC14 File Offset: 0x000EDE14
		public virtual bool IsAssignableFrom(TypeInfo typeInfo)
		{
			if (typeInfo == null)
			{
				return false;
			}
			if (this == typeInfo)
			{
				return true;
			}
			if (typeInfo.IsSubclassOf(this))
			{
				return true;
			}
			if (base.IsInterface)
			{
				return typeInfo.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(typeInfo))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04002ED6 RID: 11990
		private const BindingFlags DeclaredOnlyLookup = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x020008A1 RID: 2209
		[CompilerGenerated]
		private sealed class <GetDeclaredMethods>d__10 : IEnumerable<MethodInfo>, IEnumerable, IEnumerator<MethodInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06004AA3 RID: 19107 RVA: 0x000EFC7F File Offset: 0x000EDE7F
			[DebuggerHidden]
			public <GetDeclaredMethods>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06004AA4 RID: 19108 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06004AA5 RID: 19109 RVA: 0x000EFC9C File Offset: 0x000EDE9C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TypeInfo typeInfo = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					array = typeInfo.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					i = 0;
					goto IL_007B;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_006D:
				i++;
				IL_007B:
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				MethodInfo methodInfo = array[i];
				if (methodInfo.Name == name)
				{
					this.<>2__current = methodInfo;
					this.<>1__state = 1;
					return true;
				}
				goto IL_006D;
			}

			// Token: 0x17000C18 RID: 3096
			// (get) Token: 0x06004AA6 RID: 19110 RVA: 0x000EFD3C File Offset: 0x000EDF3C
			MethodInfo IEnumerator<MethodInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004AA7 RID: 19111 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000C19 RID: 3097
			// (get) Token: 0x06004AA8 RID: 19112 RVA: 0x000EFD3C File Offset: 0x000EDF3C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004AA9 RID: 19113 RVA: 0x000EFD44 File Offset: 0x000EDF44
			[DebuggerHidden]
			IEnumerator<MethodInfo> IEnumerable<MethodInfo>.GetEnumerator()
			{
				TypeInfo.<GetDeclaredMethods>d__10 <GetDeclaredMethods>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetDeclaredMethods>d__ = this;
				}
				else
				{
					<GetDeclaredMethods>d__ = new TypeInfo.<GetDeclaredMethods>d__10(0);
					<GetDeclaredMethods>d__.<>4__this = this;
				}
				<GetDeclaredMethods>d__.name = name;
				return <GetDeclaredMethods>d__;
			}

			// Token: 0x06004AAA RID: 19114 RVA: 0x000EFD93 File Offset: 0x000EDF93
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo>.GetEnumerator();
			}

			// Token: 0x04002ED7 RID: 11991
			private int <>1__state;

			// Token: 0x04002ED8 RID: 11992
			private MethodInfo <>2__current;

			// Token: 0x04002ED9 RID: 11993
			private int <>l__initialThreadId;

			// Token: 0x04002EDA RID: 11994
			public TypeInfo <>4__this;

			// Token: 0x04002EDB RID: 11995
			private string name;

			// Token: 0x04002EDC RID: 11996
			public string <>3__name;

			// Token: 0x04002EDD RID: 11997
			private MethodInfo[] <>7__wrap1;

			// Token: 0x04002EDE RID: 11998
			private int <>7__wrap2;
		}

		// Token: 0x020008A2 RID: 2210
		[CompilerGenerated]
		private sealed class <get_DeclaredNestedTypes>d__22 : IEnumerable<TypeInfo>, IEnumerable, IEnumerator<TypeInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06004AAB RID: 19115 RVA: 0x000EFD9B File Offset: 0x000EDF9B
			[DebuggerHidden]
			public <get_DeclaredNestedTypes>d__22(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06004AAC RID: 19116 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06004AAD RID: 19117 RVA: 0x000EFDB8 File Offset: 0x000EDFB8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TypeInfo typeInfo = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					i++;
				}
				else
				{
					this.<>1__state = -1;
					array = typeInfo.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					i = 0;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				Type type = array[i];
				this.<>2__current = type.GetTypeInfo();
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000C1A RID: 3098
			// (get) Token: 0x06004AAE RID: 19118 RVA: 0x000EFE4A File Offset: 0x000EE04A
			TypeInfo IEnumerator<TypeInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004AAF RID: 19119 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000C1B RID: 3099
			// (get) Token: 0x06004AB0 RID: 19120 RVA: 0x000EFE4A File Offset: 0x000EE04A
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004AB1 RID: 19121 RVA: 0x000EFE54 File Offset: 0x000EE054
			[DebuggerHidden]
			IEnumerator<TypeInfo> IEnumerable<TypeInfo>.GetEnumerator()
			{
				TypeInfo.<get_DeclaredNestedTypes>d__22 <get_DeclaredNestedTypes>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_DeclaredNestedTypes>d__ = this;
				}
				else
				{
					<get_DeclaredNestedTypes>d__ = new TypeInfo.<get_DeclaredNestedTypes>d__22(0);
					<get_DeclaredNestedTypes>d__.<>4__this = this;
				}
				return <get_DeclaredNestedTypes>d__;
			}

			// Token: 0x06004AB2 RID: 19122 RVA: 0x000EFE97 File Offset: 0x000EE097
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.TypeInfo>.GetEnumerator();
			}

			// Token: 0x04002EDF RID: 11999
			private int <>1__state;

			// Token: 0x04002EE0 RID: 12000
			private TypeInfo <>2__current;

			// Token: 0x04002EE1 RID: 12001
			private int <>l__initialThreadId;

			// Token: 0x04002EE2 RID: 12002
			public TypeInfo <>4__this;

			// Token: 0x04002EE3 RID: 12003
			private Type[] <>7__wrap1;

			// Token: 0x04002EE4 RID: 12004
			private int <>7__wrap2;
		}
	}
}

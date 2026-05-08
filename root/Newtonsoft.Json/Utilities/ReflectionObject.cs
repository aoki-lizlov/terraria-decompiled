using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000058 RID: 88
	internal class ReflectionObject
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000120D1 File Offset: 0x000102D1
		public ObjectConstructor<object> Creator
		{
			[CompilerGenerated]
			get
			{
				return this.<Creator>k__BackingField;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x000120D9 File Offset: 0x000102D9
		public IDictionary<string, ReflectionMember> Members
		{
			[CompilerGenerated]
			get
			{
				return this.<Members>k__BackingField;
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000120E1 File Offset: 0x000102E1
		private ReflectionObject(ObjectConstructor<object> creator)
		{
			this.Members = new Dictionary<string, ReflectionMember>();
			this.Creator = creator;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000120FB File Offset: 0x000102FB
		public object GetValue(object target, string member)
		{
			return this.Members[member].Getter.Invoke(target);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00012114 File Offset: 0x00010314
		public void SetValue(object target, string member, object value)
		{
			this.Members[member].Setter.Invoke(target, value);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001212E File Offset: 0x0001032E
		public Type GetType(string member)
		{
			return this.Members[member].MemberType;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00012141 File Offset: 0x00010341
		public static ReflectionObject Create(Type t, params string[] memberNames)
		{
			return ReflectionObject.Create(t, null, memberNames);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001214C File Offset: 0x0001034C
		public static ReflectionObject Create(Type t, MethodBase creator, params string[] memberNames)
		{
			ReflectionDelegateFactory reflectionDelegateFactory = JsonTypeReflector.ReflectionDelegateFactory;
			ObjectConstructor<object> objectConstructor = null;
			if (creator != null)
			{
				objectConstructor = reflectionDelegateFactory.CreateParameterizedConstructor(creator);
			}
			else if (ReflectionUtils.HasDefaultConstructor(t, false))
			{
				Func<object> ctor = reflectionDelegateFactory.CreateDefaultConstructor<object>(t);
				objectConstructor = (object[] args) => ctor.Invoke();
			}
			ReflectionObject reflectionObject = new ReflectionObject(objectConstructor);
			int i = 0;
			while (i < memberNames.Length)
			{
				string text = memberNames[i];
				MemberInfo[] member = t.GetMember(text, 20);
				if (member.Length != 1)
				{
					throw new ArgumentException("Expected a single member with the name '{0}'.".FormatWith(CultureInfo.InvariantCulture, text));
				}
				MemberInfo memberInfo = Enumerable.Single<MemberInfo>(member);
				ReflectionMember reflectionMember = new ReflectionMember();
				MemberTypes memberTypes = memberInfo.MemberType();
				if (memberTypes == 4)
				{
					goto IL_00AA;
				}
				if (memberTypes != 8)
				{
					if (memberTypes == 16)
					{
						goto IL_00AA;
					}
					throw new ArgumentException("Unexpected member type '{0}' for member '{1}'.".FormatWith(CultureInfo.InvariantCulture, memberInfo.MemberType(), memberInfo.Name));
				}
				else
				{
					MethodInfo methodInfo = (MethodInfo)memberInfo;
					if (methodInfo.IsPublic)
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (parameters.Length == 0 && methodInfo.ReturnType != typeof(void))
						{
							MethodCall<object, object> call2 = reflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
							reflectionMember.Getter = (object target) => call2(target, new object[0]);
						}
						else if (parameters.Length == 1 && methodInfo.ReturnType == typeof(void))
						{
							MethodCall<object, object> call = reflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
							reflectionMember.Setter = delegate(object target, object arg)
							{
								call(target, new object[] { arg });
							};
						}
					}
				}
				IL_01BF:
				if (ReflectionUtils.CanReadMemberValue(memberInfo, false))
				{
					reflectionMember.Getter = reflectionDelegateFactory.CreateGet<object>(memberInfo);
				}
				if (ReflectionUtils.CanSetMemberValue(memberInfo, false, false))
				{
					reflectionMember.Setter = reflectionDelegateFactory.CreateSet<object>(memberInfo);
				}
				reflectionMember.MemberType = ReflectionUtils.GetMemberUnderlyingType(memberInfo);
				reflectionObject.Members[text] = reflectionMember;
				i++;
				continue;
				IL_00AA:
				if (ReflectionUtils.CanReadMemberValue(memberInfo, false))
				{
					reflectionMember.Getter = reflectionDelegateFactory.CreateGet<object>(memberInfo);
				}
				if (ReflectionUtils.CanSetMemberValue(memberInfo, false, false))
				{
					reflectionMember.Setter = reflectionDelegateFactory.CreateSet<object>(memberInfo);
					goto IL_01BF;
				}
				goto IL_01BF;
			}
			return reflectionObject;
		}

		// Token: 0x04000206 RID: 518
		[CompilerGenerated]
		private readonly ObjectConstructor<object> <Creator>k__BackingField;

		// Token: 0x04000207 RID: 519
		[CompilerGenerated]
		private readonly IDictionary<string, ReflectionMember> <Members>k__BackingField;

		// Token: 0x0200012E RID: 302
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x06000CB7 RID: 3255 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06000CB8 RID: 3256 RVA: 0x0003102D File Offset: 0x0002F22D
			internal object <Create>b__0(object[] args)
			{
				return this.ctor.Invoke();
			}

			// Token: 0x04000488 RID: 1160
			public Func<object> ctor;
		}

		// Token: 0x0200012F RID: 303
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_1
		{
			// Token: 0x06000CB9 RID: 3257 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass11_1()
			{
			}

			// Token: 0x06000CBA RID: 3258 RVA: 0x0003103A File Offset: 0x0002F23A
			internal object <Create>b__1(object target)
			{
				return this.call(target, new object[0]);
			}

			// Token: 0x04000489 RID: 1161
			public MethodCall<object, object> call;
		}

		// Token: 0x02000130 RID: 304
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_2
		{
			// Token: 0x06000CBB RID: 3259 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass11_2()
			{
			}

			// Token: 0x06000CBC RID: 3260 RVA: 0x0003104E File Offset: 0x0002F24E
			internal void <Create>b__2(object target, object arg)
			{
				this.call(target, new object[] { arg });
			}

			// Token: 0x0400048A RID: 1162
			public MethodCall<object, object> call;
		}
	}
}

using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x0200011D RID: 285
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class DebugCommandAttribute : Attribute
	{
		// Token: 0x06001B58 RID: 7000 RVA: 0x004FAB89 File Offset: 0x004F8D89
		public DebugCommandAttribute(string name, string description, CommandRequirement requirements)
		{
			this.Name = name;
			this.Description = description;
			this.Requirements = requirements;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x004FABA6 File Offset: 0x004F8DA6
		public IDebugCommand ToDebugCommand(MethodInfo method)
		{
			return new DebugCommandAttribute.InternalDebugCommand(this, method);
		}

		// Token: 0x0400156D RID: 5485
		public readonly string Name;

		// Token: 0x0400156E RID: 5486
		public readonly string Description;

		// Token: 0x0400156F RID: 5487
		public readonly CommandRequirement Requirements;

		// Token: 0x04001570 RID: 5488
		public string HelpText;

		// Token: 0x0200072F RID: 1839
		private class InternalDebugCommand : IDebugCommand
		{
			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x06004092 RID: 16530 RVA: 0x0069E9BF File Offset: 0x0069CBBF
			// (set) Token: 0x06004093 RID: 16531 RVA: 0x0069E9C7 File Offset: 0x0069CBC7
			public string Name
			{
				[CompilerGenerated]
				get
				{
					return this.<Name>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Name>k__BackingField = value;
				}
			}

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x06004094 RID: 16532 RVA: 0x0069E9D0 File Offset: 0x0069CBD0
			// (set) Token: 0x06004095 RID: 16533 RVA: 0x0069E9D8 File Offset: 0x0069CBD8
			public string Description
			{
				[CompilerGenerated]
				get
				{
					return this.<Description>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Description>k__BackingField = value;
				}
			}

			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x06004096 RID: 16534 RVA: 0x0069E9E1 File Offset: 0x0069CBE1
			// (set) Token: 0x06004097 RID: 16535 RVA: 0x0069E9E9 File Offset: 0x0069CBE9
			public string HelpText
			{
				[CompilerGenerated]
				get
				{
					return this.<HelpText>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<HelpText>k__BackingField = value;
				}
			}

			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x06004098 RID: 16536 RVA: 0x0069E9F2 File Offset: 0x0069CBF2
			// (set) Token: 0x06004099 RID: 16537 RVA: 0x0069E9FA File Offset: 0x0069CBFA
			public CommandRequirement Requirements
			{
				[CompilerGenerated]
				get
				{
					return this.<Requirements>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Requirements>k__BackingField = value;
				}
			}

			// Token: 0x0600409A RID: 16538 RVA: 0x0069EA04 File Offset: 0x0069CC04
			public InternalDebugCommand(DebugCommandAttribute attribute, MethodInfo method)
			{
				this.Name = attribute.Name;
				this.Description = attribute.Description;
				this.HelpText = attribute.HelpText;
				this.Requirements = attribute.Requirements;
				this._processMethod = (DebugCommandAttribute.InternalDebugCommand.ProcessMethod)Delegate.CreateDelegate(typeof(DebugCommandAttribute.InternalDebugCommand.ProcessMethod), method);
			}

			// Token: 0x0600409B RID: 16539 RVA: 0x0069EA62 File Offset: 0x0069CC62
			public bool Process(DebugMessage message)
			{
				return this._processMethod(message);
			}

			// Token: 0x04006998 RID: 27032
			[CompilerGenerated]
			private string <Name>k__BackingField;

			// Token: 0x04006999 RID: 27033
			[CompilerGenerated]
			private string <Description>k__BackingField;

			// Token: 0x0400699A RID: 27034
			[CompilerGenerated]
			private string <HelpText>k__BackingField;

			// Token: 0x0400699B RID: 27035
			[CompilerGenerated]
			private CommandRequirement <Requirements>k__BackingField;

			// Token: 0x0400699C RID: 27036
			private readonly DebugCommandAttribute.InternalDebugCommand.ProcessMethod _processMethod;

			// Token: 0x02000A89 RID: 2697
			// (Invoke) Token: 0x06004BA9 RID: 19369
			private delegate bool ProcessMethod(DebugMessage message);
		}
	}
}

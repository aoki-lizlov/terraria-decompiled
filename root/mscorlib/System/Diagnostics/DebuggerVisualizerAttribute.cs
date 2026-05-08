using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x02000A16 RID: 2582
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerVisualizerAttribute : Attribute
	{
		// Token: 0x06005FBF RID: 24511 RVA: 0x0014C170 File Offset: 0x0014A370
		public DebuggerVisualizerAttribute(string visualizerTypeName)
		{
			this.visualizerName = visualizerTypeName;
		}

		// Token: 0x06005FC0 RID: 24512 RVA: 0x0014C17F File Offset: 0x0014A37F
		public DebuggerVisualizerAttribute(string visualizerTypeName, string visualizerObjectSourceTypeName)
		{
			this.visualizerName = visualizerTypeName;
			this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x0014C195 File Offset: 0x0014A395
		public DebuggerVisualizerAttribute(string visualizerTypeName, Type visualizerObjectSource)
		{
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizerTypeName;
			this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		// Token: 0x06005FC2 RID: 24514 RVA: 0x0014C1C4 File Offset: 0x0014A3C4
		public DebuggerVisualizerAttribute(Type visualizer)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
		}

		// Token: 0x06005FC3 RID: 24515 RVA: 0x0014C1EC File Offset: 0x0014A3EC
		public DebuggerVisualizerAttribute(Type visualizer, Type visualizerObjectSource)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		// Token: 0x06005FC4 RID: 24516 RVA: 0x0014C23F File Offset: 0x0014A43F
		public DebuggerVisualizerAttribute(Type visualizer, string visualizerObjectSourceTypeName)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06005FC5 RID: 24517 RVA: 0x0014C26E File Offset: 0x0014A46E
		public string VisualizerObjectSourceTypeName
		{
			get
			{
				return this.visualizerObjectSourceName;
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06005FC6 RID: 24518 RVA: 0x0014C276 File Offset: 0x0014A476
		public string VisualizerTypeName
		{
			get
			{
				return this.visualizerName;
			}
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06005FC7 RID: 24519 RVA: 0x0014C27E File Offset: 0x0014A47E
		// (set) Token: 0x06005FC8 RID: 24520 RVA: 0x0014C286 File Offset: 0x0014A486
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06005FCA RID: 24522 RVA: 0x0014C2B8 File Offset: 0x0014A4B8
		// (set) Token: 0x06005FC9 RID: 24521 RVA: 0x0014C28F File Offset: 0x0014A48F
		public Type Target
		{
			get
			{
				return this.target;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.targetName = value.AssemblyQualifiedName;
				this.target = value;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06005FCC RID: 24524 RVA: 0x0014C2C9 File Offset: 0x0014A4C9
		// (set) Token: 0x06005FCB RID: 24523 RVA: 0x0014C2C0 File Offset: 0x0014A4C0
		public string TargetTypeName
		{
			get
			{
				return this.targetName;
			}
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x040039B5 RID: 14773
		private string visualizerObjectSourceName;

		// Token: 0x040039B6 RID: 14774
		private string visualizerName;

		// Token: 0x040039B7 RID: 14775
		private string description;

		// Token: 0x040039B8 RID: 14776
		private string targetName;

		// Token: 0x040039B9 RID: 14777
		private Type target;
	}
}

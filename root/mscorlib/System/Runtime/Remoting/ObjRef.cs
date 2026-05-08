using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x02000534 RID: 1332
	[ComVisible(true)]
	[Serializable]
	public class ObjRef : IObjectReference, ISerializable
	{
		// Token: 0x0600359E RID: 13726 RVA: 0x000C205D File Offset: 0x000C025D
		public ObjRef()
		{
			this.UpdateChannelInfo();
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000C206B File Offset: 0x000C026B
		internal ObjRef(string uri, IChannelInfo cinfo)
		{
			this.uri = uri;
			this.channel_info = cinfo;
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000C2084 File Offset: 0x000C0284
		internal ObjRef DeserializeInTheCurrentDomain(int domainId, byte[] tInfo)
		{
			string text = string.Copy(this.uri);
			ChannelInfo channelInfo = new ChannelInfo(new CrossAppDomainData(domainId));
			ObjRef objRef = new ObjRef(text, channelInfo);
			IRemotingTypeInfo remotingTypeInfo = (IRemotingTypeInfo)CADSerializer.DeserializeObjectSafe(tInfo);
			objRef.typeInfo = remotingTypeInfo;
			return objRef;
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000C20C1 File Offset: 0x000C02C1
		internal byte[] SerializeType()
		{
			if (this.typeInfo == null)
			{
				throw new Exception("Attempt to serialize a null TypeInfo.");
			}
			return CADSerializer.SerializeObject(this.typeInfo).GetBuffer();
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000C20E8 File Offset: 0x000C02E8
		internal ObjRef(ObjRef o, bool unmarshalAsProxy)
		{
			this.channel_info = o.channel_info;
			this.uri = o.uri;
			this.typeInfo = o.typeInfo;
			this.envoyInfo = o.envoyInfo;
			this.flags = o.flags;
			if (unmarshalAsProxy)
			{
				this.flags |= ObjRef.MarshalledObjectRef;
			}
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000C214C File Offset: 0x000C034C
		public ObjRef(MarshalByRefObject o, Type requestedType)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			if (requestedType == null)
			{
				throw new ArgumentNullException("requestedType");
			}
			this.uri = RemotingServices.GetObjectUri(o);
			this.typeInfo = new TypeInfo(requestedType);
			if (!requestedType.IsInstanceOfType(o))
			{
				throw new RemotingException("The server object type cannot be cast to the requested type " + requestedType.FullName);
			}
			this.UpdateChannelInfo();
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000C21BE File Offset: 0x000C03BE
		internal ObjRef(Type type, string url, object remoteChannelData)
		{
			this.uri = url;
			this.typeInfo = new TypeInfo(type);
			if (remoteChannelData != null)
			{
				this.channel_info = new ChannelInfo(remoteChannelData);
			}
			this.flags |= ObjRef.WellKnowObjectRef;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000C21FC File Offset: 0x000C03FC
		protected ObjRef(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			bool flag = true;
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "uri"))
				{
					if (!(name == "typeInfo"))
					{
						if (!(name == "channelInfo"))
						{
							if (!(name == "envoyInfo"))
							{
								if (!(name == "fIsMarshalled"))
								{
									if (!(name == "objrefFlags"))
									{
										throw new NotSupportedException();
									}
									this.flags = Convert.ToInt32(enumerator.Value);
								}
								else
								{
									object value = enumerator.Value;
									int num;
									if (value is string)
									{
										num = ((IConvertible)value).ToInt32(null);
									}
									else
									{
										num = (int)value;
									}
									if (num == 0)
									{
										flag = false;
									}
								}
							}
							else
							{
								this.envoyInfo = (IEnvoyInfo)enumerator.Value;
							}
						}
						else
						{
							this.channel_info = (IChannelInfo)enumerator.Value;
						}
					}
					else
					{
						this.typeInfo = (IRemotingTypeInfo)enumerator.Value;
					}
				}
				else
				{
					this.uri = (string)enumerator.Value;
				}
			}
			if (flag)
			{
				this.flags |= ObjRef.MarshalledObjectRef;
			}
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x0000408A File Offset: 0x0000228A
		internal bool IsPossibleToCAD()
		{
			return false;
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060035A7 RID: 13735 RVA: 0x000C232F File Offset: 0x000C052F
		internal bool IsReferenceToWellKnow
		{
			get
			{
				return (this.flags & ObjRef.WellKnowObjectRef) > 0;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x000C2340 File Offset: 0x000C0540
		// (set) Token: 0x060035A9 RID: 13737 RVA: 0x000C2348 File Offset: 0x000C0548
		public virtual IChannelInfo ChannelInfo
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.channel_info;
			}
			set
			{
				this.channel_info = value;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060035AA RID: 13738 RVA: 0x000C2351 File Offset: 0x000C0551
		// (set) Token: 0x060035AB RID: 13739 RVA: 0x000C2359 File Offset: 0x000C0559
		public virtual IEnvoyInfo EnvoyInfo
		{
			get
			{
				return this.envoyInfo;
			}
			set
			{
				this.envoyInfo = value;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000C2362 File Offset: 0x000C0562
		// (set) Token: 0x060035AD RID: 13741 RVA: 0x000C236A File Offset: 0x000C056A
		public virtual IRemotingTypeInfo TypeInfo
		{
			get
			{
				return this.typeInfo;
			}
			set
			{
				this.typeInfo = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x000C2373 File Offset: 0x000C0573
		// (set) Token: 0x060035AF RID: 13743 RVA: 0x000C237B File Offset: 0x000C057B
		public virtual string URI
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000C2384 File Offset: 0x000C0584
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(base.GetType());
			info.AddValue("uri", this.uri);
			info.AddValue("typeInfo", this.typeInfo, typeof(IRemotingTypeInfo));
			info.AddValue("envoyInfo", this.envoyInfo, typeof(IEnvoyInfo));
			info.AddValue("channelInfo", this.channel_info, typeof(IChannelInfo));
			info.AddValue("objrefFlags", this.flags);
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000C2410 File Offset: 0x000C0610
		public virtual object GetRealObject(StreamingContext context)
		{
			if ((this.flags & ObjRef.MarshalledObjectRef) > 0)
			{
				return RemotingServices.Unmarshal(this);
			}
			return this;
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000C242C File Offset: 0x000C062C
		public bool IsFromThisAppDomain()
		{
			Identity identityForUri = RemotingServices.GetIdentityForUri(this.uri);
			return identityForUri != null && identityForUri.IsFromThisAppDomain;
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000C2450 File Offset: 0x000C0650
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool IsFromThisProcess()
		{
			foreach (object obj in this.channel_info.ChannelData)
			{
				if (obj is CrossAppDomainData)
				{
					return ((CrossAppDomainData)obj).ProcessID == RemotingConfiguration.ProcessId;
				}
			}
			return true;
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000C249A File Offset: 0x000C069A
		internal void UpdateChannelInfo()
		{
			this.channel_info = new ChannelInfo();
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x000C24A7 File Offset: 0x000C06A7
		internal Type ServerType
		{
			get
			{
				if (this._serverType == null)
				{
					this._serverType = Type.GetType(this.typeInfo.TypeName);
				}
				return this._serverType;
			}
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x00004088 File Offset: 0x00002288
		internal void SetDomainID(int id)
		{
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000C24D3 File Offset: 0x000C06D3
		// Note: this type is marked as 'beforefieldinit'.
		static ObjRef()
		{
		}

		// Token: 0x040024A9 RID: 9385
		private IChannelInfo channel_info;

		// Token: 0x040024AA RID: 9386
		private string uri;

		// Token: 0x040024AB RID: 9387
		private IRemotingTypeInfo typeInfo;

		// Token: 0x040024AC RID: 9388
		private IEnvoyInfo envoyInfo;

		// Token: 0x040024AD RID: 9389
		private int flags;

		// Token: 0x040024AE RID: 9390
		private Type _serverType;

		// Token: 0x040024AF RID: 9391
		private static int MarshalledObjectRef = 1;

		// Token: 0x040024B0 RID: 9392
		private static int WellKnowObjectRef = 2;
	}
}

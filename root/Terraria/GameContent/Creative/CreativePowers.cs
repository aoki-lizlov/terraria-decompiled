using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.Net;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
	// Token: 0x02000329 RID: 809
	public class CreativePowers
	{
		// Token: 0x060027CF RID: 10191 RVA: 0x0000357B File Offset: 0x0000177B
		public CreativePowers()
		{
		}

		// Token: 0x02000897 RID: 2199
		public abstract class APerPlayerTogglePower : ICreativePower, IOnPlayerJoining
		{
			// Token: 0x17000542 RID: 1346
			// (get) Token: 0x060044CF RID: 17615 RVA: 0x006C368C File Offset: 0x006C188C
			// (set) Token: 0x060044D0 RID: 17616 RVA: 0x006C3694 File Offset: 0x006C1894
			public ushort PowerId
			{
				[CompilerGenerated]
				get
				{
					return this.<PowerId>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<PowerId>k__BackingField = value;
				}
			}

			// Token: 0x17000543 RID: 1347
			// (get) Token: 0x060044D1 RID: 17617 RVA: 0x006C369D File Offset: 0x006C189D
			// (set) Token: 0x060044D2 RID: 17618 RVA: 0x006C36A5 File Offset: 0x006C18A5
			public string ServerConfigName
			{
				[CompilerGenerated]
				get
				{
					return this.<ServerConfigName>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ServerConfigName>k__BackingField = value;
				}
			}

			// Token: 0x17000544 RID: 1348
			// (get) Token: 0x060044D3 RID: 17619 RVA: 0x006C36AE File Offset: 0x006C18AE
			// (set) Token: 0x060044D4 RID: 17620 RVA: 0x006C36B6 File Offset: 0x006C18B6
			public PowerPermissionLevel CurrentPermissionLevel
			{
				[CompilerGenerated]
				get
				{
					return this.<CurrentPermissionLevel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<CurrentPermissionLevel>k__BackingField = value;
				}
			}

			// Token: 0x17000545 RID: 1349
			// (get) Token: 0x060044D5 RID: 17621 RVA: 0x006C36BF File Offset: 0x006C18BF
			// (set) Token: 0x060044D6 RID: 17622 RVA: 0x006C36C7 File Offset: 0x006C18C7
			public PowerPermissionLevel DefaultPermissionLevel
			{
				[CompilerGenerated]
				get
				{
					return this.<DefaultPermissionLevel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<DefaultPermissionLevel>k__BackingField = value;
				}
			}

			// Token: 0x060044D7 RID: 17623 RVA: 0x006C36D0 File Offset: 0x006C18D0
			public bool IsEnabledForPlayer(int playerIndex)
			{
				return this._perPlayerIsEnabled.IndexInRange(playerIndex) && this._perPlayerIsEnabled[playerIndex];
			}

			// Token: 0x060044D8 RID: 17624 RVA: 0x006C36EC File Offset: 0x006C18EC
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				CreativePowers.APerPlayerTogglePower.SubMessageType subMessageType = (CreativePowers.APerPlayerTogglePower.SubMessageType)reader.ReadByte();
				if (subMessageType == CreativePowers.APerPlayerTogglePower.SubMessageType.SyncEveryone)
				{
					this.Deserialize_SyncEveryone(reader, userId);
					return;
				}
				if (subMessageType != CreativePowers.APerPlayerTogglePower.SubMessageType.SyncOnePlayer)
				{
					return;
				}
				int num = (int)reader.ReadByte();
				bool flag = reader.ReadBoolean();
				if (Main.netMode == 2)
				{
					num = userId;
					if (!CreativePowersHelper.IsAvailableForPlayer(this, num))
					{
						return;
					}
				}
				this.SetEnabledState(num, flag);
			}

			// Token: 0x060044D9 RID: 17625 RVA: 0x006C373C File Offset: 0x006C193C
			private void Deserialize_SyncEveryone(BinaryReader reader, int userId)
			{
				int num = (int)Math.Ceiling((double)((float)this._perPlayerIsEnabled.Length / 8f));
				if (Main.netMode == 2 && !CreativePowersHelper.IsAvailableForPlayer(this, userId))
				{
					reader.ReadBytes(num);
					return;
				}
				for (int i = 0; i < num; i++)
				{
					BitsByte bitsByte = reader.ReadByte();
					for (int j = 0; j < 8; j++)
					{
						int num2 = i * 8 + j;
						if (num2 != Main.myPlayer)
						{
							if (num2 >= this._perPlayerIsEnabled.Length)
							{
								break;
							}
							this.SetEnabledState(num2, bitsByte[j]);
						}
					}
				}
			}

			// Token: 0x060044DA RID: 17626 RVA: 0x006C37CC File Offset: 0x006C19CC
			public void SetEnabledState(int playerIndex, bool state)
			{
				this._perPlayerIsEnabled[playerIndex] = state;
				if (Main.netMode == 2)
				{
					NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 3);
					netPacket.Writer.Write(1);
					netPacket.Writer.Write((byte)playerIndex);
					netPacket.Writer.Write(state);
					NetManager.Instance.Broadcast(netPacket, -1);
				}
			}

			// Token: 0x060044DB RID: 17627 RVA: 0x006C382B File Offset: 0x006C1A2B
			public void DebugCall()
			{
				this.RequestUse();
			}

			// Token: 0x060044DC RID: 17628 RVA: 0x006C3834 File Offset: 0x006C1A34
			internal void RequestUse()
			{
				NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 3);
				netPacket.Writer.Write(1);
				netPacket.Writer.Write((byte)Main.myPlayer);
				netPacket.Writer.Write(!this._perPlayerIsEnabled[Main.myPlayer]);
				NetManager.Instance.SendToServerOrLoopback(netPacket);
			}

			// Token: 0x060044DD RID: 17629 RVA: 0x006C3894 File Offset: 0x006C1A94
			public void Reset()
			{
				for (int i = 0; i < this._perPlayerIsEnabled.Length; i++)
				{
					this._perPlayerIsEnabled[i] = this._defaultToggleState;
				}
			}

			// Token: 0x060044DE RID: 17630 RVA: 0x006C38C4 File Offset: 0x006C1AC4
			public void OnPlayerJoining(int playerIndex)
			{
				int num = (int)Math.Ceiling((double)((float)this._perPlayerIsEnabled.Length / 8f));
				NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, num + 1);
				netPacket.Writer.Write(0);
				for (int i = 0; i < num; i++)
				{
					BitsByte bitsByte = 0;
					for (int j = 0; j < 8; j++)
					{
						int num2 = i * 8 + j;
						if (num2 >= this._perPlayerIsEnabled.Length)
						{
							break;
						}
						bitsByte[j] = this._perPlayerIsEnabled[num2];
					}
					netPacket.Writer.Write(bitsByte);
				}
				NetManager.Instance.SendToClient(netPacket, playerIndex);
			}

			// Token: 0x060044DF RID: 17631 RVA: 0x006C396C File Offset: 0x006C1B6C
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				GroupOptionButton<bool> groupOptionButton = CreativePowersHelper.CreateToggleButton(info);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, Main.OurFavoriteColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnLeftClick += this.button_OnClick;
				groupOptionButton.OnUpdate += this.button_OnUpdate;
				elements.Add(groupOptionButton);
			}

			// Token: 0x060044E0 RID: 17632 RVA: 0x006C39C8 File Offset: 0x006C1BC8
			private void button_OnUpdate(UIElement affectedElement)
			{
				bool flag = this._perPlayerIsEnabled[Main.myPlayer];
				GroupOptionButton<bool> groupOptionButton = affectedElement as GroupOptionButton<bool>;
				groupOptionButton.SetCurrentOption(flag);
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue(groupOptionButton.IsSelected ? (this._powerNameKey + "_Enabled") : (this._powerNameKey + "_Disabled"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, this._powerNameKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref textValue, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref textValue);
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x060044E1 RID: 17633 RVA: 0x006C3A76 File Offset: 0x006C1C76
			private void button_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				if (!this.GetIsUnlocked())
				{
					return;
				}
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				this.RequestUse();
			}

			// Token: 0x060044E2 RID: 17634
			public abstract bool GetIsUnlocked();

			// Token: 0x060044E3 RID: 17635 RVA: 0x006C3A95 File Offset: 0x006C1C95
			protected APerPlayerTogglePower()
			{
			}

			// Token: 0x040072B7 RID: 29367
			[CompilerGenerated]
			private ushort <PowerId>k__BackingField;

			// Token: 0x040072B8 RID: 29368
			[CompilerGenerated]
			private string <ServerConfigName>k__BackingField;

			// Token: 0x040072B9 RID: 29369
			[CompilerGenerated]
			private PowerPermissionLevel <CurrentPermissionLevel>k__BackingField;

			// Token: 0x040072BA RID: 29370
			[CompilerGenerated]
			private PowerPermissionLevel <DefaultPermissionLevel>k__BackingField;

			// Token: 0x040072BB RID: 29371
			internal string _powerNameKey;

			// Token: 0x040072BC RID: 29372
			internal Point _iconLocation;

			// Token: 0x040072BD RID: 29373
			internal bool _defaultToggleState;

			// Token: 0x040072BE RID: 29374
			private bool[] _perPlayerIsEnabled = new bool[255];

			// Token: 0x02000ADC RID: 2780
			private enum SubMessageType : byte
			{
				// Token: 0x0400788E RID: 30862
				SyncEveryone,
				// Token: 0x0400788F RID: 30863
				SyncOnePlayer
			}
		}

		// Token: 0x02000898 RID: 2200
		public abstract class APerPlayerSliderPower : ICreativePower, IOnPlayerJoining, IProvideSliderElement, IPowerSubcategoryElement
		{
			// Token: 0x17000546 RID: 1350
			// (get) Token: 0x060044E4 RID: 17636 RVA: 0x006C3AAD File Offset: 0x006C1CAD
			// (set) Token: 0x060044E5 RID: 17637 RVA: 0x006C3AB5 File Offset: 0x006C1CB5
			public ushort PowerId
			{
				[CompilerGenerated]
				get
				{
					return this.<PowerId>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<PowerId>k__BackingField = value;
				}
			}

			// Token: 0x17000547 RID: 1351
			// (get) Token: 0x060044E6 RID: 17638 RVA: 0x006C3ABE File Offset: 0x006C1CBE
			// (set) Token: 0x060044E7 RID: 17639 RVA: 0x006C3AC6 File Offset: 0x006C1CC6
			public string ServerConfigName
			{
				[CompilerGenerated]
				get
				{
					return this.<ServerConfigName>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ServerConfigName>k__BackingField = value;
				}
			}

			// Token: 0x17000548 RID: 1352
			// (get) Token: 0x060044E8 RID: 17640 RVA: 0x006C3ACF File Offset: 0x006C1CCF
			// (set) Token: 0x060044E9 RID: 17641 RVA: 0x006C3AD7 File Offset: 0x006C1CD7
			public PowerPermissionLevel CurrentPermissionLevel
			{
				[CompilerGenerated]
				get
				{
					return this.<CurrentPermissionLevel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<CurrentPermissionLevel>k__BackingField = value;
				}
			}

			// Token: 0x17000549 RID: 1353
			// (get) Token: 0x060044EA RID: 17642 RVA: 0x006C3AE0 File Offset: 0x006C1CE0
			// (set) Token: 0x060044EB RID: 17643 RVA: 0x006C3AE8 File Offset: 0x006C1CE8
			public PowerPermissionLevel DefaultPermissionLevel
			{
				[CompilerGenerated]
				get
				{
					return this.<DefaultPermissionLevel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<DefaultPermissionLevel>k__BackingField = value;
				}
			}

			// Token: 0x060044EC RID: 17644 RVA: 0x006C3AF1 File Offset: 0x006C1CF1
			public bool GetRemappedSliderValueFor(int playerIndex, out float value)
			{
				value = 0f;
				if (!this._cachePerPlayer.IndexInRange(playerIndex))
				{
					return false;
				}
				value = this.RemapSliderValueToPowerValue(this._cachePerPlayer[playerIndex]);
				return true;
			}

			// Token: 0x060044ED RID: 17645
			public abstract float RemapSliderValueToPowerValue(float sliderValue);

			// Token: 0x060044EE RID: 17646 RVA: 0x006C3B1C File Offset: 0x006C1D1C
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				int num = (int)reader.ReadByte();
				float num2 = reader.ReadSingle();
				if (Main.netMode == 2)
				{
					num = userId;
					if (!CreativePowersHelper.IsAvailableForPlayer(this, num))
					{
						return;
					}
				}
				this._cachePerPlayer[num] = num2;
				if (num == Main.myPlayer)
				{
					this._sliderCurrentValueCache = num2;
					this.UpdateInfoFromSliderValueCache();
				}
			}

			// Token: 0x060044EF RID: 17647
			internal abstract void UpdateInfoFromSliderValueCache();

			// Token: 0x060044F0 RID: 17648 RVA: 0x00356859 File Offset: 0x00354A59
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060044F1 RID: 17649 RVA: 0x006C3B6C File Offset: 0x006C1D6C
			public void DebugCall()
			{
				NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 5);
				netPacket.Writer.Write((byte)Main.myPlayer);
				netPacket.Writer.Write(0f);
				NetManager.Instance.SendToServerOrLoopback(netPacket);
			}

			// Token: 0x060044F2 RID: 17650
			public abstract UIElement ProvideSlider();

			// Token: 0x060044F3 RID: 17651 RVA: 0x006C3BB4 File Offset: 0x006C1DB4
			internal float GetSliderValue()
			{
				if (Main.netMode == 1 && this._needsToCommitChange)
				{
					return this._currentTargetValue;
				}
				return this._sliderCurrentValueCache;
			}

			// Token: 0x060044F4 RID: 17652 RVA: 0x006C3BD3 File Offset: 0x006C1DD3
			internal void SetValueKeyboard(float value)
			{
				if (value == this._currentTargetValue)
				{
					return;
				}
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				this._currentTargetValue = value;
				this._needsToCommitChange = true;
			}

			// Token: 0x060044F5 RID: 17653 RVA: 0x006C3BFC File Offset: 0x006C1DFC
			internal void SetValueGamepad()
			{
				float sliderValue = this.GetSliderValue();
				float num = UILinksInitializer.HandleSliderVerticalInput(sliderValue, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				if (num != sliderValue)
				{
					this.SetValueKeyboard(num);
				}
			}

			// Token: 0x060044F6 RID: 17654 RVA: 0x006C3C3B File Offset: 0x006C1E3B
			public void PushChangeAndSetSlider(float value)
			{
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				value = MathHelper.Clamp(value, 0f, 1f);
				this._sliderCurrentValueCache = value;
				this._currentTargetValue = value;
				this.PushChange(value);
			}

			// Token: 0x060044F7 RID: 17655 RVA: 0x006C3C74 File Offset: 0x006C1E74
			public GroupOptionButton<int> GetOptionButton(CreativePowerUIElementRequestInfo info, int optionIndex, int currentOptionIndex)
			{
				GroupOptionButton<int> groupOptionButton = CreativePowersHelper.CreateCategoryButton<int>(info, optionIndex, currentOptionIndex);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, CreativePowersHelper.CommonSelectedColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnUpdate += this.categoryButton_OnUpdate;
				return groupOptionButton;
			}

			// Token: 0x060044F8 RID: 17656 RVA: 0x006C3CBC File Offset: 0x006C1EBC
			private void categoryButton_OnUpdate(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					GroupOptionButton<int> groupOptionButton = affectedElement as GroupOptionButton<int>;
					string textValue = Language.GetTextValue(this._powerNameKey + (groupOptionButton.IsSelected ? "_Opened" : "_Closed"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, this._powerNameKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref textValue, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref textValue);
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
				this.AttemptPushingChange();
			}

			// Token: 0x060044F9 RID: 17657 RVA: 0x006C3D54 File Offset: 0x006C1F54
			private void AttemptPushingChange()
			{
				if (!this._needsToCommitChange)
				{
					return;
				}
				if (DateTime.UtcNow.CompareTo(this._nextTimeWeCanPush) == -1)
				{
					return;
				}
				this.PushChange(this._currentTargetValue);
			}

			// Token: 0x060044FA RID: 17658 RVA: 0x006C3D90 File Offset: 0x006C1F90
			internal void PushChange(float newSliderValue)
			{
				this._needsToCommitChange = false;
				this._sliderCurrentValueCache = newSliderValue;
				this._nextTimeWeCanPush = DateTime.UtcNow;
				NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 5);
				netPacket.Writer.Write((byte)Main.myPlayer);
				netPacket.Writer.Write(newSliderValue);
				NetManager.Instance.SendToServerOrLoopback(netPacket);
			}

			// Token: 0x060044FB RID: 17659 RVA: 0x006C3DF0 File Offset: 0x006C1FF0
			public virtual void Reset()
			{
				for (int i = 0; i < this._cachePerPlayer.Length; i++)
				{
					this.ResetForPlayer(i);
				}
			}

			// Token: 0x060044FC RID: 17660 RVA: 0x006C3E17 File Offset: 0x006C2017
			public virtual void ResetForPlayer(int playerIndex)
			{
				this._cachePerPlayer[playerIndex] = this._sliderDefaultValue;
				if (playerIndex == Main.myPlayer)
				{
					this._sliderCurrentValueCache = this._sliderDefaultValue;
					this._currentTargetValue = this._sliderDefaultValue;
				}
			}

			// Token: 0x060044FD RID: 17661 RVA: 0x006C3E47 File Offset: 0x006C2047
			public void OnPlayerJoining(int playerIndex)
			{
				this.ResetForPlayer(playerIndex);
			}

			// Token: 0x060044FE RID: 17662
			public abstract bool GetIsUnlocked();

			// Token: 0x060044FF RID: 17663 RVA: 0x006C3E50 File Offset: 0x006C2050
			protected APerPlayerSliderPower()
			{
			}

			// Token: 0x040072BF RID: 29375
			[CompilerGenerated]
			private ushort <PowerId>k__BackingField;

			// Token: 0x040072C0 RID: 29376
			[CompilerGenerated]
			private string <ServerConfigName>k__BackingField;

			// Token: 0x040072C1 RID: 29377
			[CompilerGenerated]
			private PowerPermissionLevel <CurrentPermissionLevel>k__BackingField;

			// Token: 0x040072C2 RID: 29378
			[CompilerGenerated]
			private PowerPermissionLevel <DefaultPermissionLevel>k__BackingField;

			// Token: 0x040072C3 RID: 29379
			internal Point _iconLocation;

			// Token: 0x040072C4 RID: 29380
			internal float _sliderCurrentValueCache;

			// Token: 0x040072C5 RID: 29381
			internal string _powerNameKey;

			// Token: 0x040072C6 RID: 29382
			internal float[] _cachePerPlayer = new float[256];

			// Token: 0x040072C7 RID: 29383
			internal float _sliderDefaultValue;

			// Token: 0x040072C8 RID: 29384
			private float _currentTargetValue;

			// Token: 0x040072C9 RID: 29385
			private bool _needsToCommitChange;

			// Token: 0x040072CA RID: 29386
			private DateTime _nextTimeWeCanPush = DateTime.UtcNow;
		}

		// Token: 0x02000899 RID: 2201
		public abstract class ASharedButtonPower : ICreativePower
		{
			// Token: 0x1700054A RID: 1354
			// (get) Token: 0x06004500 RID: 17664 RVA: 0x006C3E73 File Offset: 0x006C2073
			// (set) Token: 0x06004501 RID: 17665 RVA: 0x006C3E7B File Offset: 0x006C207B
			public ushort PowerId
			{
				[CompilerGenerated]
				get
				{
					return this.<PowerId>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<PowerId>k__BackingField = value;
				}
			}

			// Token: 0x1700054B RID: 1355
			// (get) Token: 0x06004502 RID: 17666 RVA: 0x006C3E84 File Offset: 0x006C2084
			// (set) Token: 0x06004503 RID: 17667 RVA: 0x006C3E8C File Offset: 0x006C208C
			public string ServerConfigName
			{
				[CompilerGenerated]
				get
				{
					return this.<ServerConfigName>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ServerConfigName>k__BackingField = value;
				}
			}

			// Token: 0x1700054C RID: 1356
			// (get) Token: 0x06004504 RID: 17668 RVA: 0x006C3E95 File Offset: 0x006C2095
			// (set) Token: 0x06004505 RID: 17669 RVA: 0x006C3E9D File Offset: 0x006C209D
			public PowerPermissionLevel CurrentPermissionLevel
			{
				[CompilerGenerated]
				get
				{
					return this.<CurrentPermissionLevel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<CurrentPermissionLevel>k__BackingField = value;
				}
			}

			// Token: 0x1700054D RID: 1357
			// (get) Token: 0x06004506 RID: 17670 RVA: 0x006C3EA6 File Offset: 0x006C20A6
			// (set) Token: 0x06004507 RID: 17671 RVA: 0x006C3EAE File Offset: 0x006C20AE
			public PowerPermissionLevel DefaultPermissionLevel
			{
				[CompilerGenerated]
				get
				{
					return this.<DefaultPermissionLevel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<DefaultPermissionLevel>k__BackingField = value;
				}
			}

			// Token: 0x06004508 RID: 17672 RVA: 0x006C3EB7 File Offset: 0x006C20B7
			public ASharedButtonPower()
			{
				this.OnCreation();
			}

			// Token: 0x06004509 RID: 17673 RVA: 0x006C3EC8 File Offset: 0x006C20C8
			public void RequestUse()
			{
				NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 0);
				NetManager.Instance.SendToServerOrLoopback(netPacket);
			}

			// Token: 0x0600450A RID: 17674 RVA: 0x006C3EED File Offset: 0x006C20ED
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				if (Main.netMode == 2 && !CreativePowersHelper.IsAvailableForPlayer(this, userId))
				{
					return;
				}
				this.UsePower();
			}

			// Token: 0x0600450B RID: 17675
			internal abstract void UsePower();

			// Token: 0x0600450C RID: 17676
			internal abstract void OnCreation();

			// Token: 0x0600450D RID: 17677 RVA: 0x006C3F08 File Offset: 0x006C2108
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				GroupOptionButton<bool> groupOptionButton = CreativePowersHelper.CreateSimpleButton(info);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, CreativePowersHelper.CommonSelectedColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnLeftClick += this.button_OnClick;
				groupOptionButton.OnUpdate += this.button_OnUpdate;
				elements.Add(groupOptionButton);
			}

			// Token: 0x0600450E RID: 17678 RVA: 0x006C3F64 File Offset: 0x006C2164
			private void button_OnUpdate(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue(this._powerNameKey);
					CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, this._descriptionKey);
					CreativePowersHelper.AddUnlockTextIfNeeded(ref textValue, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref textValue);
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600450F RID: 17679 RVA: 0x006C3FC9 File Offset: 0x006C21C9
			private void button_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				this.RequestUse();
			}

			// Token: 0x06004510 RID: 17680
			public abstract bool GetIsUnlocked();

			// Token: 0x040072CB RID: 29387
			[CompilerGenerated]
			private ushort <PowerId>k__BackingField;

			// Token: 0x040072CC RID: 29388
			[CompilerGenerated]
			private string <ServerConfigName>k__BackingField;

			// Token: 0x040072CD RID: 29389
			[CompilerGenerated]
			private PowerPermissionLevel <CurrentPermissionLevel>k__BackingField;

			// Token: 0x040072CE RID: 29390
			[CompilerGenerated]
			private PowerPermissionLevel <DefaultPermissionLevel>k__BackingField;

			// Token: 0x040072CF RID: 29391
			internal Point _iconLocation;

			// Token: 0x040072D0 RID: 29392
			internal string _powerNameKey;

			// Token: 0x040072D1 RID: 29393
			internal string _descriptionKey;
		}

		// Token: 0x0200089A RID: 2202
		public abstract class ASharedTogglePower : ICreativePower, IOnPlayerJoining
		{
			// Token: 0x1700054E RID: 1358
			// (get) Token: 0x06004511 RID: 17681 RVA: 0x006C3FDF File Offset: 0x006C21DF
			// (set) Token: 0x06004512 RID: 17682 RVA: 0x006C3FE7 File Offset: 0x006C21E7
			public ushort PowerId
			{
				[CompilerGenerated]
				get
				{
					return this.<PowerId>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<PowerId>k__BackingField = value;
				}
			}

			// Token: 0x1700054F RID: 1359
			// (get) Token: 0x06004513 RID: 17683 RVA: 0x006C3FF0 File Offset: 0x006C21F0
			// (set) Token: 0x06004514 RID: 17684 RVA: 0x006C3FF8 File Offset: 0x006C21F8
			public string ServerConfigName
			{
				[CompilerGenerated]
				get
				{
					return this.<ServerConfigName>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ServerConfigName>k__BackingField = value;
				}
			}

			// Token: 0x17000550 RID: 1360
			// (get) Token: 0x06004515 RID: 17685 RVA: 0x006C4001 File Offset: 0x006C2201
			// (set) Token: 0x06004516 RID: 17686 RVA: 0x006C4009 File Offset: 0x006C2209
			public PowerPermissionLevel CurrentPermissionLevel
			{
				[CompilerGenerated]
				get
				{
					return this.<CurrentPermissionLevel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<CurrentPermissionLevel>k__BackingField = value;
				}
			}

			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x06004517 RID: 17687 RVA: 0x006C4012 File Offset: 0x006C2212
			// (set) Token: 0x06004518 RID: 17688 RVA: 0x006C401A File Offset: 0x006C221A
			public PowerPermissionLevel DefaultPermissionLevel
			{
				[CompilerGenerated]
				get
				{
					return this.<DefaultPermissionLevel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<DefaultPermissionLevel>k__BackingField = value;
				}
			}

			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x06004519 RID: 17689 RVA: 0x006C4023 File Offset: 0x006C2223
			// (set) Token: 0x0600451A RID: 17690 RVA: 0x006C402B File Offset: 0x006C222B
			public bool Enabled
			{
				[CompilerGenerated]
				get
				{
					return this.<Enabled>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Enabled>k__BackingField = value;
				}
			}

			// Token: 0x0600451B RID: 17691 RVA: 0x006C4034 File Offset: 0x006C2234
			public void SetPowerInfo(bool enabled)
			{
				this.Enabled = enabled;
			}

			// Token: 0x0600451C RID: 17692 RVA: 0x006C403D File Offset: 0x006C223D
			public void Reset()
			{
				this.Enabled = false;
			}

			// Token: 0x0600451D RID: 17693 RVA: 0x006C4048 File Offset: 0x006C2248
			public void OnPlayerJoining(int playerIndex)
			{
				NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 1);
				netPacket.Writer.Write(this.Enabled);
				NetManager.Instance.SendToClient(netPacket, playerIndex);
			}

			// Token: 0x0600451E RID: 17694 RVA: 0x006C4080 File Offset: 0x006C2280
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				bool flag = reader.ReadBoolean();
				if (Main.netMode == 2 && !CreativePowersHelper.IsAvailableForPlayer(this, userId))
				{
					return;
				}
				this.SetPowerInfo(flag);
				if (Main.netMode == 2)
				{
					NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 1);
					netPacket.Writer.Write(this.Enabled);
					NetManager.Instance.Broadcast(netPacket, -1);
				}
			}

			// Token: 0x0600451F RID: 17695 RVA: 0x006C40E0 File Offset: 0x006C22E0
			private void RequestUse()
			{
				NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 1);
				netPacket.Writer.Write(!this.Enabled);
				NetManager.Instance.SendToServerOrLoopback(netPacket);
			}

			// Token: 0x06004520 RID: 17696 RVA: 0x006C411C File Offset: 0x006C231C
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				GroupOptionButton<bool> groupOptionButton = CreativePowersHelper.CreateToggleButton(info);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, Main.OurFavoriteColor);
				this.CustomizeButton(groupOptionButton);
				groupOptionButton.OnLeftClick += this.button_OnClick;
				groupOptionButton.OnUpdate += this.button_OnUpdate;
				elements.Add(groupOptionButton);
			}

			// Token: 0x06004521 RID: 17697 RVA: 0x006C4170 File Offset: 0x006C2370
			private void button_OnUpdate(UIElement affectedElement)
			{
				bool enabled = this.Enabled;
				GroupOptionButton<bool> groupOptionButton = affectedElement as GroupOptionButton<bool>;
				groupOptionButton.SetCurrentOption(enabled);
				if (affectedElement.IsMouseHovering)
				{
					string buttonTextKey = this.GetButtonTextKey();
					string textValue = Language.GetTextValue(buttonTextKey + (groupOptionButton.IsSelected ? "_Enabled" : "_Disabled"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, buttonTextKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref textValue, this.GetIsUnlocked(), buttonTextKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref textValue);
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x06004522 RID: 17698 RVA: 0x006C4205 File Offset: 0x006C2405
			private void button_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				this.RequestUse();
			}

			// Token: 0x06004523 RID: 17699
			internal abstract void CustomizeButton(UIElement button);

			// Token: 0x06004524 RID: 17700
			internal abstract string GetButtonTextKey();

			// Token: 0x06004525 RID: 17701
			public abstract bool GetIsUnlocked();

			// Token: 0x06004526 RID: 17702 RVA: 0x0000357B File Offset: 0x0000177B
			protected ASharedTogglePower()
			{
			}

			// Token: 0x040072D2 RID: 29394
			[CompilerGenerated]
			private ushort <PowerId>k__BackingField;

			// Token: 0x040072D3 RID: 29395
			[CompilerGenerated]
			private string <ServerConfigName>k__BackingField;

			// Token: 0x040072D4 RID: 29396
			[CompilerGenerated]
			private PowerPermissionLevel <CurrentPermissionLevel>k__BackingField;

			// Token: 0x040072D5 RID: 29397
			[CompilerGenerated]
			private PowerPermissionLevel <DefaultPermissionLevel>k__BackingField;

			// Token: 0x040072D6 RID: 29398
			[CompilerGenerated]
			private bool <Enabled>k__BackingField;
		}

		// Token: 0x0200089B RID: 2203
		public abstract class ASharedSliderPower : ICreativePower, IOnPlayerJoining, IProvideSliderElement, IPowerSubcategoryElement
		{
			// Token: 0x17000553 RID: 1363
			// (get) Token: 0x06004527 RID: 17703 RVA: 0x006C421B File Offset: 0x006C241B
			// (set) Token: 0x06004528 RID: 17704 RVA: 0x006C4223 File Offset: 0x006C2423
			public ushort PowerId
			{
				[CompilerGenerated]
				get
				{
					return this.<PowerId>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<PowerId>k__BackingField = value;
				}
			}

			// Token: 0x17000554 RID: 1364
			// (get) Token: 0x06004529 RID: 17705 RVA: 0x006C422C File Offset: 0x006C242C
			// (set) Token: 0x0600452A RID: 17706 RVA: 0x006C4234 File Offset: 0x006C2434
			public string ServerConfigName
			{
				[CompilerGenerated]
				get
				{
					return this.<ServerConfigName>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ServerConfigName>k__BackingField = value;
				}
			}

			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x0600452B RID: 17707 RVA: 0x006C423D File Offset: 0x006C243D
			// (set) Token: 0x0600452C RID: 17708 RVA: 0x006C4245 File Offset: 0x006C2445
			public PowerPermissionLevel CurrentPermissionLevel
			{
				[CompilerGenerated]
				get
				{
					return this.<CurrentPermissionLevel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<CurrentPermissionLevel>k__BackingField = value;
				}
			}

			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x0600452D RID: 17709 RVA: 0x006C424E File Offset: 0x006C244E
			// (set) Token: 0x0600452E RID: 17710 RVA: 0x006C4256 File Offset: 0x006C2456
			public PowerPermissionLevel DefaultPermissionLevel
			{
				[CompilerGenerated]
				get
				{
					return this.<DefaultPermissionLevel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<DefaultPermissionLevel>k__BackingField = value;
				}
			}

			// Token: 0x0600452F RID: 17711 RVA: 0x006C4260 File Offset: 0x006C2460
			public void DeserializeNetMessage(BinaryReader reader, int userId)
			{
				float num = reader.ReadSingle();
				if (Main.netMode == 2 && !CreativePowersHelper.IsAvailableForPlayer(this, userId))
				{
					return;
				}
				this._sliderCurrentValueCache = num;
				this.UpdateInfoFromSliderValueCache();
				if (Main.netMode == 2)
				{
					NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
					netPacket.Writer.Write(num);
					NetManager.Instance.Broadcast(netPacket, -1);
				}
			}

			// Token: 0x06004530 RID: 17712
			internal abstract void UpdateInfoFromSliderValueCache();

			// Token: 0x06004531 RID: 17713 RVA: 0x00356859 File Offset: 0x00354A59
			public void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06004532 RID: 17714 RVA: 0x006C42C4 File Offset: 0x006C24C4
			public void DebugCall()
			{
				NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
				netPacket.Writer.Write(0f);
				NetManager.Instance.SendToServerOrLoopback(netPacket);
			}

			// Token: 0x06004533 RID: 17715
			public abstract UIElement ProvideSlider();

			// Token: 0x06004534 RID: 17716 RVA: 0x006C42FA File Offset: 0x006C24FA
			internal float GetSliderValue()
			{
				if (Main.netMode == 1 && this._needsToCommitChange)
				{
					return this._currentTargetValue;
				}
				return this.GetSliderValueInner();
			}

			// Token: 0x06004535 RID: 17717 RVA: 0x006C4319 File Offset: 0x006C2519
			internal virtual float GetSliderValueInner()
			{
				return this._sliderCurrentValueCache;
			}

			// Token: 0x06004536 RID: 17718 RVA: 0x006C4321 File Offset: 0x006C2521
			internal void SetValueKeyboard(float value)
			{
				if (value == this._currentTargetValue)
				{
					return;
				}
				this.SetValueKeyboardForced(value);
			}

			// Token: 0x06004537 RID: 17719 RVA: 0x006C4334 File Offset: 0x006C2534
			internal void SetValueKeyboardForced(float value)
			{
				if (!CreativePowersHelper.IsAvailableForPlayer(this, Main.myPlayer))
				{
					return;
				}
				this._currentTargetValue = value;
				this._needsToCommitChange = true;
			}

			// Token: 0x06004538 RID: 17720 RVA: 0x006C4354 File Offset: 0x006C2554
			internal void SetValueGamepad()
			{
				float sliderValue = this.GetSliderValue();
				float num = UILinksInitializer.HandleSliderVerticalInput(sliderValue, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				if (num != sliderValue)
				{
					this.SetValueKeyboard(num);
				}
			}

			// Token: 0x06004539 RID: 17721 RVA: 0x006C4394 File Offset: 0x006C2594
			public GroupOptionButton<int> GetOptionButton(CreativePowerUIElementRequestInfo info, int optionIndex, int currentOptionIndex)
			{
				GroupOptionButton<int> groupOptionButton = CreativePowersHelper.CreateCategoryButton<int>(info, optionIndex, currentOptionIndex);
				CreativePowersHelper.UpdateUnlockStateByPower(this, groupOptionButton, CreativePowersHelper.CommonSelectedColor);
				groupOptionButton.Append(CreativePowersHelper.GetIconImage(this._iconLocation));
				groupOptionButton.OnUpdate += this.categoryButton_OnUpdate;
				return groupOptionButton;
			}

			// Token: 0x0600453A RID: 17722 RVA: 0x006C43DC File Offset: 0x006C25DC
			private void categoryButton_OnUpdate(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					GroupOptionButton<int> groupOptionButton = affectedElement as GroupOptionButton<int>;
					string textValue = Language.GetTextValue(this._powerNameKey + (groupOptionButton.IsSelected ? "_Opened" : "_Closed"));
					CreativePowersHelper.AddDescriptionIfNeeded(ref textValue, this._powerNameKey + "_Description");
					CreativePowersHelper.AddUnlockTextIfNeeded(ref textValue, this.GetIsUnlocked(), this._powerNameKey + "_Unlock");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref textValue);
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
				this.AttemptPushingChange();
			}

			// Token: 0x0600453B RID: 17723 RVA: 0x006C4474 File Offset: 0x006C2674
			private void AttemptPushingChange()
			{
				if (!this._needsToCommitChange)
				{
					return;
				}
				if (DateTime.UtcNow.CompareTo(this._nextTimeWeCanPush) == -1)
				{
					return;
				}
				this._needsToCommitChange = false;
				this._sliderCurrentValueCache = this._currentTargetValue;
				this._nextTimeWeCanPush = DateTime.UtcNow;
				NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
				netPacket.Writer.Write(this._currentTargetValue);
				NetManager.Instance.SendToServerOrLoopback(netPacket);
			}

			// Token: 0x0600453C RID: 17724 RVA: 0x006C44E9 File Offset: 0x006C26E9
			public virtual void Reset()
			{
				this._sliderCurrentValueCache = 0f;
			}

			// Token: 0x0600453D RID: 17725 RVA: 0x006C44F8 File Offset: 0x006C26F8
			public void OnPlayerJoining(int playerIndex)
			{
				if (!this._syncToJoiningPlayers)
				{
					return;
				}
				NetPacket netPacket = NetCreativePowersModule.PreparePacket(this.PowerId, 4);
				netPacket.Writer.Write(this._sliderCurrentValueCache);
				NetManager.Instance.SendToClient(netPacket, playerIndex);
			}

			// Token: 0x0600453E RID: 17726
			public abstract bool GetIsUnlocked();

			// Token: 0x0600453F RID: 17727 RVA: 0x006C4539 File Offset: 0x006C2739
			protected ASharedSliderPower()
			{
			}

			// Token: 0x040072D7 RID: 29399
			[CompilerGenerated]
			private ushort <PowerId>k__BackingField;

			// Token: 0x040072D8 RID: 29400
			[CompilerGenerated]
			private string <ServerConfigName>k__BackingField;

			// Token: 0x040072D9 RID: 29401
			[CompilerGenerated]
			private PowerPermissionLevel <CurrentPermissionLevel>k__BackingField;

			// Token: 0x040072DA RID: 29402
			[CompilerGenerated]
			private PowerPermissionLevel <DefaultPermissionLevel>k__BackingField;

			// Token: 0x040072DB RID: 29403
			internal Point _iconLocation;

			// Token: 0x040072DC RID: 29404
			internal float _sliderCurrentValueCache;

			// Token: 0x040072DD RID: 29405
			internal string _powerNameKey;

			// Token: 0x040072DE RID: 29406
			internal bool _syncToJoiningPlayers = true;

			// Token: 0x040072DF RID: 29407
			internal float _currentTargetValue;

			// Token: 0x040072E0 RID: 29408
			private bool _needsToCommitChange;

			// Token: 0x040072E1 RID: 29409
			private DateTime _nextTimeWeCanPush = DateTime.UtcNow;
		}

		// Token: 0x0200089C RID: 2204
		public class GodmodePower : CreativePowers.APerPlayerTogglePower, IPersistentPerPlayerContent
		{
			// Token: 0x06004540 RID: 17728 RVA: 0x006C4553 File Offset: 0x006C2753
			public GodmodePower()
			{
				this._powerNameKey = "CreativePowers.Godmode";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.Godmode;
			}

			// Token: 0x06004541 RID: 17729 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06004542 RID: 17730 RVA: 0x006C4574 File Offset: 0x006C2774
			public void Save(Player player, BinaryWriter writer)
			{
				bool flag = base.IsEnabledForPlayer(Main.myPlayer);
				writer.Write(flag);
			}

			// Token: 0x06004543 RID: 17731 RVA: 0x006C4594 File Offset: 0x006C2794
			public void ResetDataForNewPlayer(Player player)
			{
				player.savedPerPlayerFieldsThatArentInThePlayerClass.godmodePowerEnabled = this._defaultToggleState;
			}

			// Token: 0x06004544 RID: 17732 RVA: 0x006C45A8 File Offset: 0x006C27A8
			public void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool flag = reader.ReadBoolean();
				player.savedPerPlayerFieldsThatArentInThePlayerClass.godmodePowerEnabled = flag;
			}

			// Token: 0x06004545 RID: 17733 RVA: 0x006C45C8 File Offset: 0x006C27C8
			public void ApplyLoadedDataToOutOfPlayerFields(Player player)
			{
				if (player.savedPerPlayerFieldsThatArentInThePlayerClass.godmodePowerEnabled != base.IsEnabledForPlayer(player.whoAmI))
				{
					base.RequestUse();
				}
			}
		}

		// Token: 0x0200089D RID: 2205
		public class FarPlacementRangePower : CreativePowers.APerPlayerTogglePower, IPersistentPerPlayerContent
		{
			// Token: 0x06004546 RID: 17734 RVA: 0x006C45E9 File Offset: 0x006C27E9
			public FarPlacementRangePower()
			{
				this._powerNameKey = "CreativePowers.InfinitePlacementRange";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.BlockPlacementRange;
				this._defaultToggleState = true;
			}

			// Token: 0x06004547 RID: 17735 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06004548 RID: 17736 RVA: 0x006C4610 File Offset: 0x006C2810
			public void Save(Player player, BinaryWriter writer)
			{
				bool flag = base.IsEnabledForPlayer(Main.myPlayer);
				writer.Write(flag);
			}

			// Token: 0x06004549 RID: 17737 RVA: 0x006C4630 File Offset: 0x006C2830
			public void ResetDataForNewPlayer(Player player)
			{
				player.savedPerPlayerFieldsThatArentInThePlayerClass.farPlacementRangePowerEnabled = this._defaultToggleState;
			}

			// Token: 0x0600454A RID: 17738 RVA: 0x006C4644 File Offset: 0x006C2844
			public void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool flag = reader.ReadBoolean();
				player.savedPerPlayerFieldsThatArentInThePlayerClass.farPlacementRangePowerEnabled = flag;
			}

			// Token: 0x0600454B RID: 17739 RVA: 0x006C4664 File Offset: 0x006C2864
			public void ApplyLoadedDataToOutOfPlayerFields(Player player)
			{
				if (player.savedPerPlayerFieldsThatArentInThePlayerClass.farPlacementRangePowerEnabled != base.IsEnabledForPlayer(player.whoAmI))
				{
					base.RequestUse();
				}
			}
		}

		// Token: 0x0200089E RID: 2206
		public class StartDayImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x0600454C RID: 17740 RVA: 0x006C4685 File Offset: 0x006C2885
			internal override void UsePower()
			{
				if (Main.netMode == 1)
				{
					return;
				}
				Main.SkipToTime(0, true);
			}

			// Token: 0x0600454D RID: 17741 RVA: 0x006C4697 File Offset: 0x006C2897
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartDayImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeDawn;
			}

			// Token: 0x0600454E RID: 17742 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x0600454F RID: 17743 RVA: 0x006C46C5 File Offset: 0x006C28C5
			public StartDayImmediately()
			{
			}
		}

		// Token: 0x0200089F RID: 2207
		public class StartNightImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x06004550 RID: 17744 RVA: 0x006C46CD File Offset: 0x006C28CD
			internal override void UsePower()
			{
				if (Main.netMode == 1)
				{
					return;
				}
				Main.SkipToTime(0, false);
			}

			// Token: 0x06004551 RID: 17745 RVA: 0x006C46DF File Offset: 0x006C28DF
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartNightImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeDusk;
			}

			// Token: 0x06004552 RID: 17746 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06004553 RID: 17747 RVA: 0x006C46C5 File Offset: 0x006C28C5
			public StartNightImmediately()
			{
			}
		}

		// Token: 0x020008A0 RID: 2208
		public class StartNoonImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x06004554 RID: 17748 RVA: 0x006C470D File Offset: 0x006C290D
			internal override void UsePower()
			{
				if (Main.netMode == 1)
				{
					return;
				}
				Main.SkipToTime(27000, true);
			}

			// Token: 0x06004555 RID: 17749 RVA: 0x006C4723 File Offset: 0x006C2923
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartNoonImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeNoon;
			}

			// Token: 0x06004556 RID: 17750 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06004557 RID: 17751 RVA: 0x006C46C5 File Offset: 0x006C28C5
			public StartNoonImmediately()
			{
			}
		}

		// Token: 0x020008A1 RID: 2209
		public class StartMidnightImmediately : CreativePowers.ASharedButtonPower
		{
			// Token: 0x06004558 RID: 17752 RVA: 0x006C4751 File Offset: 0x006C2951
			internal override void UsePower()
			{
				if (Main.netMode == 1)
				{
					return;
				}
				Main.SkipToTime(16200, false);
			}

			// Token: 0x06004559 RID: 17753 RVA: 0x006C4767 File Offset: 0x006C2967
			internal override void OnCreation()
			{
				this._powerNameKey = "CreativePowers.StartMidnightImmediately";
				this._descriptionKey = this._powerNameKey + "_Description";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.TimeMidnight;
			}

			// Token: 0x0600455A RID: 17754 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x0600455B RID: 17755 RVA: 0x006C46C5 File Offset: 0x006C28C5
			public StartMidnightImmediately()
			{
			}
		}

		// Token: 0x020008A2 RID: 2210
		public class ModifyTimeRate : CreativePowers.ASharedSliderPower, IPersistentPerWorldContent
		{
			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x0600455C RID: 17756 RVA: 0x006C4795 File Offset: 0x006C2995
			// (set) Token: 0x0600455D RID: 17757 RVA: 0x006C479D File Offset: 0x006C299D
			public int TargetTimeRate
			{
				[CompilerGenerated]
				get
				{
					return this.<TargetTimeRate>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<TargetTimeRate>k__BackingField = value;
				}
			}

			// Token: 0x0600455E RID: 17758 RVA: 0x006C47A6 File Offset: 0x006C29A6
			public ModifyTimeRate()
			{
				this._powerNameKey = "CreativePowers.ModifyTimeRate";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.ModifyTime;
			}

			// Token: 0x0600455F RID: 17759 RVA: 0x006C47C4 File Offset: 0x006C29C4
			public override void Reset()
			{
				this._sliderCurrentValueCache = 0f;
				this.TargetTimeRate = 1;
			}

			// Token: 0x06004560 RID: 17760 RVA: 0x006C47D8 File Offset: 0x006C29D8
			internal override void UpdateInfoFromSliderValueCache()
			{
				this.TargetTimeRate = (int)Math.Round((double)Utils.Remap(this._sliderCurrentValueCache, 0f, 1f, 1f, 24f, true));
			}

			// Token: 0x06004561 RID: 17761 RVA: 0x006C4808 File Offset: 0x006C2A08
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uiverticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uiverticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(87f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uiverticalSlider);
				uipanel.OnUpdate += CreativePowersHelper.UpdateUseMouseInterface;
				UIText uitext = new UIText("x24", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uitext.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext.OnMouseOver += this.Button_OnMouseOver;
				uitext.OnMouseOut += this.Button_OnMouseOut;
				uitext.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uitext);
				UIText uitext2 = new UIText("x12", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uitext2.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext2.OnMouseOver += this.Button_OnMouseOver;
				uitext2.OnMouseOut += this.Button_OnMouseOut;
				uitext2.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uitext2);
				UIText uitext3 = new UIText("x1", 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uitext3.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext3.OnMouseOver += this.Button_OnMouseOver;
				uitext3.OnMouseOut += this.Button_OnMouseOut;
				uitext3.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uitext3);
				return uipanel;
			}

			// Token: 0x06004562 RID: 17762 RVA: 0x006C4A11 File Offset: 0x006C2C11
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004563 RID: 17763 RVA: 0x006C4A33 File Offset: 0x006C2C33
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004564 RID: 17764 RVA: 0x006C4A55 File Offset: 0x006C2C55
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004565 RID: 17765 RVA: 0x006C4A78 File Offset: 0x006C2C78
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004566 RID: 17766 RVA: 0x006C4AB0 File Offset: 0x006C2CB0
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004567 RID: 17767 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06004568 RID: 17768 RVA: 0x006C4AE7 File Offset: 0x006C2CE7
			public void Save(BinaryWriter writer)
			{
				writer.Write(this._sliderCurrentValueCache);
			}

			// Token: 0x06004569 RID: 17769 RVA: 0x006C4AF5 File Offset: 0x006C2CF5
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				this._sliderCurrentValueCache = reader.ReadSingle();
				this.UpdateInfoFromSliderValueCache();
			}

			// Token: 0x0600456A RID: 17770 RVA: 0x006C4B09 File Offset: 0x006C2D09
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadSingle();
			}

			// Token: 0x0600456B RID: 17771 RVA: 0x006C4B14 File Offset: 0x006C2D14
			private void UpdateMouseOverNoItemText(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					Main.instance.MouseTextNoOverride(string.Empty, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600456C RID: 17772 RVA: 0x006C4B40 File Offset: 0x006C2D40
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string text = "x" + this.TargetTimeRate.ToString();
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref text);
					Main.instance.MouseTextNoOverride(text, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x040072E2 RID: 29410
			[CompilerGenerated]
			private int <TargetTimeRate>k__BackingField;
		}

		// Token: 0x020008A3 RID: 2211
		public class DifficultySliderPower : CreativePowers.ASharedSliderPower, IPersistentPerWorldContent
		{
			// Token: 0x17000558 RID: 1368
			// (get) Token: 0x0600456D RID: 17773 RVA: 0x006C4B88 File Offset: 0x006C2D88
			// (set) Token: 0x0600456E RID: 17774 RVA: 0x006C4B90 File Offset: 0x006C2D90
			public float StrengthMultiplierToGiveNPCs
			{
				[CompilerGenerated]
				get
				{
					return this.<StrengthMultiplierToGiveNPCs>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<StrengthMultiplierToGiveNPCs>k__BackingField = value;
				}
			}

			// Token: 0x0600456F RID: 17775 RVA: 0x006C4B99 File Offset: 0x006C2D99
			public DifficultySliderPower()
			{
				this._powerNameKey = "CreativePowers.DifficultySlider";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.EnemyStrengthSlider;
			}

			// Token: 0x06004570 RID: 17776 RVA: 0x006C4BB7 File Offset: 0x006C2DB7
			public override void Reset()
			{
				this._sliderCurrentValueCache = 0f;
				this.UpdateInfoFromSliderValueCache();
			}

			// Token: 0x06004571 RID: 17777 RVA: 0x006C4BCC File Offset: 0x006C2DCC
			internal override void UpdateInfoFromSliderValueCache()
			{
				if (this._sliderCurrentValueCache <= 0.33f)
				{
					this.StrengthMultiplierToGiveNPCs = Utils.Remap(this._sliderCurrentValueCache, 0f, 0.33f, 0.5f, 1f, true);
				}
				else
				{
					this.StrengthMultiplierToGiveNPCs = Utils.Remap(this._sliderCurrentValueCache, 0.33f, 1f, 1f, 3f, true);
				}
				float num = (float)Math.Round((double)(this.StrengthMultiplierToGiveNPCs * 20f)) / 20f;
				this.StrengthMultiplierToGiveNPCs = num;
			}

			// Token: 0x06004572 RID: 17778 RVA: 0x006C4C58 File Offset: 0x006C2E58
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uiverticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(82f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uiverticalSlider);
				uipanel.OnUpdate += CreativePowersHelper.UpdateUseMouseInterface;
				uiverticalSlider.OnUpdate += this.UpdateSliderColorAndShowMultiplierMouseOver;
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 0f, "x3", "Images/UI/WorldCreation/IconDifficultyMaster", new UIElement.ElementEvent(this.MouseOver_Master), new UIElement.MouseEvent(this.Click_Master));
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 0.33333334f, "x2", "Images/UI/WorldCreation/IconDifficultyExpert", new UIElement.ElementEvent(this.MouseOver_Expert), new UIElement.MouseEvent(this.Click_Expert));
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 0.6666667f, "x1", "Images/UI/WorldCreation/IconDifficultyNormal", new UIElement.ElementEvent(this.MouseOver_Normal), new UIElement.MouseEvent(this.Click_Normal));
				CreativePowers.DifficultySliderPower.AddIndication(uipanel, 1f, "x0.5", "Images/UI/WorldCreation/IconDifficultyCreative", new UIElement.ElementEvent(this.MouseOver_Journey), new UIElement.MouseEvent(this.Click_Journey));
				return uipanel;
			}

			// Token: 0x06004573 RID: 17779 RVA: 0x006C4A55 File Offset: 0x006C2C55
			private void Click_Master(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004574 RID: 17780 RVA: 0x006C4DB3 File Offset: 0x006C2FB3
			private void Click_Expert(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.66f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004575 RID: 17781 RVA: 0x006C4DD5 File Offset: 0x006C2FD5
			private void Click_Normal(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.33f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004576 RID: 17782 RVA: 0x006C4A11 File Offset: 0x006C2C11
			private void Click_Journey(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004577 RID: 17783 RVA: 0x006C4DF8 File Offset: 0x006C2FF8
			private static void AddIndication(UIPanel panel, float yAnchor, string indicationText, string iconImagePath, UIElement.ElementEvent updateEvent, UIElement.MouseEvent clickEvent)
			{
				UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>(iconImagePath, 1))
				{
					HAlign = 1f,
					VAlign = yAnchor,
					Left = new StyleDimension(4f, 0f),
					Top = new StyleDimension(2f, 0f),
					RemoveFloatingPointsFromDrawPosition = true
				};
				uiimage.OnMouseOut += CreativePowers.DifficultySliderPower.Button_OnMouseOut;
				uiimage.OnMouseOver += CreativePowers.DifficultySliderPower.Button_OnMouseOver;
				if (updateEvent != null)
				{
					uiimage.OnUpdate += updateEvent;
				}
				if (clickEvent != null)
				{
					uiimage.OnLeftClick += clickEvent;
				}
				panel.Append(uiimage);
			}

			// Token: 0x06004578 RID: 17784 RVA: 0x00593C8E File Offset: 0x00591E8E
			private static void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004579 RID: 17785 RVA: 0x00593C8E File Offset: 0x00591E8E
			private static void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600457A RID: 17786 RVA: 0x006C4EA0 File Offset: 0x006C30A0
			private void MouseOver_Journey(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Creative");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600457B RID: 17787 RVA: 0x006C4ED4 File Offset: 0x006C30D4
			private void MouseOver_Normal(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Normal");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600457C RID: 17788 RVA: 0x006C4F08 File Offset: 0x006C3108
			private void MouseOver_Expert(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Expert");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600457D RID: 17789 RVA: 0x006C4F3C File Offset: 0x006C313C
			private void MouseOver_Master(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string textValue = Language.GetTextValue("UI.Master");
					Main.instance.MouseTextNoOverride(textValue, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600457E RID: 17790 RVA: 0x006C4F70 File Offset: 0x006C3170
			private void UpdateSliderColorAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string text = "x" + this.StrengthMultiplierToGiveNPCs.ToString("F2");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref text);
					Main.instance.MouseTextNoOverride(text, 0, 0, -1, -1, -1, -1, 0);
				}
				UIVerticalSlider uiverticalSlider = affectedElement as UIVerticalSlider;
				if (uiverticalSlider == null)
				{
					return;
				}
				uiverticalSlider.EmptyColor = Color.Black;
				Color color;
				if (Main.masterMode)
				{
					color = Main.hcColor;
				}
				else if (Main.expertMode)
				{
					color = Main.mcColor;
				}
				else if (this.StrengthMultiplierToGiveNPCs < 1f)
				{
					color = Main.creativeModeColor;
				}
				else
				{
					color = Color.White;
				}
				uiverticalSlider.FilledColor = color;
			}

			// Token: 0x0600457F RID: 17791 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06004580 RID: 17792 RVA: 0x006C4AE7 File Offset: 0x006C2CE7
			public void Save(BinaryWriter writer)
			{
				writer.Write(this._sliderCurrentValueCache);
			}

			// Token: 0x06004581 RID: 17793 RVA: 0x006C4AF5 File Offset: 0x006C2CF5
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				this._sliderCurrentValueCache = reader.ReadSingle();
				this.UpdateInfoFromSliderValueCache();
			}

			// Token: 0x06004582 RID: 17794 RVA: 0x006C4B09 File Offset: 0x006C2D09
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadSingle();
			}

			// Token: 0x040072E3 RID: 29411
			[CompilerGenerated]
			private float <StrengthMultiplierToGiveNPCs>k__BackingField;
		}

		// Token: 0x020008A4 RID: 2212
		public class ModifyWindDirectionAndStrength : CreativePowers.ASharedSliderPower
		{
			// Token: 0x06004583 RID: 17795 RVA: 0x006C5013 File Offset: 0x006C3213
			public ModifyWindDirectionAndStrength()
			{
				this._powerNameKey = "CreativePowers.ModifyWindDirectionAndStrength";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.WindDirection;
				this._syncToJoiningPlayers = false;
			}

			// Token: 0x06004584 RID: 17796 RVA: 0x006C5038 File Offset: 0x006C3238
			internal override void UpdateInfoFromSliderValueCache()
			{
				Main.windSpeedCurrent = (Main.windSpeedTarget = MathHelper.Lerp(-0.8f, 0.8f, this._sliderCurrentValueCache));
			}

			// Token: 0x06004585 RID: 17797 RVA: 0x006C505A File Offset: 0x006C325A
			internal override float GetSliderValueInner()
			{
				return Utils.GetLerpValue(-0.8f, 0.8f, Main.windSpeedTarget, false);
			}

			// Token: 0x06004586 RID: 17798 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06004587 RID: 17799 RVA: 0x006C5074 File Offset: 0x006C3274
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uiverticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uiverticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(132f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uiverticalSlider);
				uipanel.OnUpdate += CreativePowersHelper.UpdateUseMouseInterface;
				UIText uitext = new UIText(Language.GetText("CreativePowers.WindWest"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uitext.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext.OnMouseOut += this.Button_OnMouseOut;
				uitext.OnMouseOver += this.Button_OnMouseOver;
				uitext.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uitext);
				UIText uitext2 = new UIText(Language.GetText("CreativePowers.WindEast"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uitext2.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext2.OnMouseOut += this.Button_OnMouseOut;
				uitext2.OnMouseOver += this.Button_OnMouseOver;
				uitext2.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uitext2);
				UIText uitext3 = new UIText(Language.GetText("CreativePowers.WindNone"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uitext3.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext3.OnMouseOut += this.Button_OnMouseOut;
				uitext3.OnMouseOver += this.Button_OnMouseOver;
				uitext3.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uitext3);
				return uipanel;
			}

			// Token: 0x06004588 RID: 17800 RVA: 0x006C4A55 File Offset: 0x006C2C55
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004589 RID: 17801 RVA: 0x006C4A11 File Offset: 0x006C2C11
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600458A RID: 17802 RVA: 0x006C4A33 File Offset: 0x006C2C33
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600458B RID: 17803 RVA: 0x006C528C File Offset: 0x006C348C
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600458C RID: 17804 RVA: 0x006C52C4 File Offset: 0x006C34C4
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x0600458D RID: 17805 RVA: 0x006C52FC File Offset: 0x006C34FC
			private void UpdateMouseOverNoItemText(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					Main.instance.MouseTextNoOverride(string.Empty, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600458E RID: 17806 RVA: 0x006C5328 File Offset: 0x006C3528
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					int num = (int)(Main.windSpeedCurrent * 50f);
					string text = "";
					if (num < 0)
					{
						text += Language.GetTextValue("GameUI.EastWind", Math.Abs(num));
					}
					else if (num > 0)
					{
						text += Language.GetTextValue("GameUI.WestWind", num);
					}
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref text);
					Main.instance.MouseTextNoOverride(text, 0, 0, -1, -1, -1, -1, 0);
				}
			}
		}

		// Token: 0x020008A5 RID: 2213
		public class ModifyRainPower : CreativePowers.ASharedSliderPower
		{
			// Token: 0x0600458F RID: 17807 RVA: 0x006C53A7 File Offset: 0x006C35A7
			public ModifyRainPower()
			{
				this._powerNameKey = "CreativePowers.ModifyRainPower";
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.RainStrength;
				this._syncToJoiningPlayers = false;
			}

			// Token: 0x06004590 RID: 17808 RVA: 0x006C53CC File Offset: 0x006C35CC
			internal override void UpdateInfoFromSliderValueCache()
			{
				if (this._sliderCurrentValueCache == 0f)
				{
					Main.StopRain(true);
					return;
				}
				Main.StartRain(true, new float?(this._sliderCurrentValueCache), false);
			}

			// Token: 0x06004591 RID: 17809 RVA: 0x006C53F4 File Offset: 0x006C35F4
			internal override float GetSliderValueInner()
			{
				return Main.cloudAlpha;
			}

			// Token: 0x06004592 RID: 17810 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x06004593 RID: 17811 RVA: 0x006C53FC File Offset: 0x006C35FC
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uiverticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uiverticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(132f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uiverticalSlider);
				uipanel.OnUpdate += CreativePowersHelper.UpdateUseMouseInterface;
				UIText uitext = new UIText(Language.GetText("CreativePowers.WeatherMonsoon"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uitext.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext.OnMouseOut += this.Button_OnMouseOut;
				uitext.OnMouseOver += this.Button_OnMouseOver;
				uitext.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uitext);
				UIText uitext2 = new UIText(Language.GetText("CreativePowers.WeatherClearSky"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uitext2.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext2.OnMouseOut += this.Button_OnMouseOut;
				uitext2.OnMouseOver += this.Button_OnMouseOver;
				uitext2.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uitext2);
				UIText uitext3 = new UIText(Language.GetText("CreativePowers.WeatherDrizzle"), 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uitext3.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext3.OnMouseOut += this.Button_OnMouseOut;
				uitext3.OnMouseOver += this.Button_OnMouseOver;
				uitext3.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uitext3);
				return uipanel;
			}

			// Token: 0x06004594 RID: 17812 RVA: 0x006C4A55 File Offset: 0x006C2C55
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004595 RID: 17813 RVA: 0x006C4A33 File Offset: 0x006C2C33
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004596 RID: 17814 RVA: 0x006C4A11 File Offset: 0x006C2C11
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboardForced(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004597 RID: 17815 RVA: 0x006C5614 File Offset: 0x006C3814
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004598 RID: 17816 RVA: 0x006C564C File Offset: 0x006C384C
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x06004599 RID: 17817 RVA: 0x006C5684 File Offset: 0x006C3884
			private void UpdateMouseOverNoItemText(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					Main.instance.MouseTextNoOverride(string.Empty, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x0600459A RID: 17818 RVA: 0x006C56B0 File Offset: 0x006C38B0
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string text = Main.maxRaining.ToString("P0");
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref text);
					Main.instance.MouseTextNoOverride(text, 0, 0, -1, -1, -1, -1, 0);
				}
			}
		}

		// Token: 0x020008A6 RID: 2214
		public class FreezeTime : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x0600459B RID: 17819 RVA: 0x006C56EF File Offset: 0x006C38EF
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.FreezeTime));
			}

			// Token: 0x0600459C RID: 17820 RVA: 0x006C5701 File Offset: 0x006C3901
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.FreezeTime";
			}

			// Token: 0x0600459D RID: 17821 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x0600459E RID: 17822 RVA: 0x006C5708 File Offset: 0x006C3908
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x0600459F RID: 17823 RVA: 0x006C5718 File Offset: 0x006C3918
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool flag = reader.ReadBoolean();
				base.SetPowerInfo(flag);
			}

			// Token: 0x060045A0 RID: 17824 RVA: 0x006C5733 File Offset: 0x006C3933
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}

			// Token: 0x060045A1 RID: 17825 RVA: 0x006C573C File Offset: 0x006C393C
			public FreezeTime()
			{
			}
		}

		// Token: 0x020008A7 RID: 2215
		public class FreezeWindDirectionAndStrength : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x060045A2 RID: 17826 RVA: 0x006C5744 File Offset: 0x006C3944
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.WindFreeze));
			}

			// Token: 0x060045A3 RID: 17827 RVA: 0x006C5756 File Offset: 0x006C3956
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.FreezeWindDirectionAndStrength";
			}

			// Token: 0x060045A4 RID: 17828 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x060045A5 RID: 17829 RVA: 0x006C5708 File Offset: 0x006C3908
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x060045A6 RID: 17830 RVA: 0x006C5760 File Offset: 0x006C3960
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool flag = reader.ReadBoolean();
				base.SetPowerInfo(flag);
			}

			// Token: 0x060045A7 RID: 17831 RVA: 0x006C5733 File Offset: 0x006C3933
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}

			// Token: 0x060045A8 RID: 17832 RVA: 0x006C573C File Offset: 0x006C393C
			public FreezeWindDirectionAndStrength()
			{
			}
		}

		// Token: 0x020008A8 RID: 2216
		public class FreezeRainPower : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x060045A9 RID: 17833 RVA: 0x006C577B File Offset: 0x006C397B
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.RainFreeze));
			}

			// Token: 0x060045AA RID: 17834 RVA: 0x006C578D File Offset: 0x006C398D
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.FreezeRainPower";
			}

			// Token: 0x060045AB RID: 17835 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x060045AC RID: 17836 RVA: 0x006C5708 File Offset: 0x006C3908
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x060045AD RID: 17837 RVA: 0x006C5794 File Offset: 0x006C3994
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool flag = reader.ReadBoolean();
				base.SetPowerInfo(flag);
			}

			// Token: 0x060045AE RID: 17838 RVA: 0x006C5733 File Offset: 0x006C3933
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}

			// Token: 0x060045AF RID: 17839 RVA: 0x006C573C File Offset: 0x006C393C
			public FreezeRainPower()
			{
			}
		}

		// Token: 0x020008A9 RID: 2217
		public class StopBiomeSpreadPower : CreativePowers.ASharedTogglePower, IPersistentPerWorldContent
		{
			// Token: 0x060045B0 RID: 17840 RVA: 0x006C57AF File Offset: 0x006C39AF
			internal override void CustomizeButton(UIElement button)
			{
				button.Append(CreativePowersHelper.GetIconImage(CreativePowersHelper.CreativePowerIconLocations.StopBiomeSpread));
			}

			// Token: 0x060045B1 RID: 17841 RVA: 0x006C57C1 File Offset: 0x006C39C1
			internal override string GetButtonTextKey()
			{
				return "CreativePowers.StopBiomeSpread";
			}

			// Token: 0x060045B2 RID: 17842 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x060045B3 RID: 17843 RVA: 0x006C5708 File Offset: 0x006C3908
			public void Save(BinaryWriter writer)
			{
				writer.Write(base.Enabled);
			}

			// Token: 0x060045B4 RID: 17844 RVA: 0x006C57C8 File Offset: 0x006C39C8
			public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				bool flag = reader.ReadBoolean();
				base.SetPowerInfo(flag);
			}

			// Token: 0x060045B5 RID: 17845 RVA: 0x006C5733 File Offset: 0x006C3933
			public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				reader.ReadBoolean();
			}

			// Token: 0x060045B6 RID: 17846 RVA: 0x006C573C File Offset: 0x006C393C
			public StopBiomeSpreadPower()
			{
			}
		}

		// Token: 0x020008AA RID: 2218
		public class SpawnRateSliderPerPlayerPower : CreativePowers.APerPlayerSliderPower, IPersistentPerPlayerContent
		{
			// Token: 0x17000559 RID: 1369
			// (get) Token: 0x060045B7 RID: 17847 RVA: 0x006C57E3 File Offset: 0x006C39E3
			// (set) Token: 0x060045B8 RID: 17848 RVA: 0x006C57EB File Offset: 0x006C39EB
			public float StrengthMultiplierToGiveNPCs
			{
				[CompilerGenerated]
				get
				{
					return this.<StrengthMultiplierToGiveNPCs>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<StrengthMultiplierToGiveNPCs>k__BackingField = value;
				}
			}

			// Token: 0x060045B9 RID: 17849 RVA: 0x006C57F4 File Offset: 0x006C39F4
			public SpawnRateSliderPerPlayerPower()
			{
				this._powerNameKey = "CreativePowers.NPCSpawnRateSlider";
				this._sliderDefaultValue = 0.5f;
				this._iconLocation = CreativePowersHelper.CreativePowerIconLocations.EnemySpawnRate;
			}

			// Token: 0x060045BA RID: 17850 RVA: 0x006C581D File Offset: 0x006C3A1D
			public bool GetShouldDisableSpawnsFor(int playerIndex)
			{
				if (!this._cachePerPlayer.IndexInRange(playerIndex))
				{
					return false;
				}
				if (playerIndex == Main.myPlayer)
				{
					return this._sliderCurrentValueCache == 0f;
				}
				return this._cachePerPlayer[playerIndex] == 0f;
			}

			// Token: 0x060045BB RID: 17851 RVA: 0x00009E46 File Offset: 0x00008046
			internal override void UpdateInfoFromSliderValueCache()
			{
			}

			// Token: 0x060045BC RID: 17852 RVA: 0x006C5854 File Offset: 0x006C3A54
			public override float RemapSliderValueToPowerValue(float sliderValue)
			{
				if (sliderValue < 0.5f)
				{
					return Utils.Remap(sliderValue, 0f, 0.5f, 0.1f, 1f, true);
				}
				return Utils.Remap(sliderValue, 0.5f, 1f, 1f, 10f, true);
			}

			// Token: 0x060045BD RID: 17853 RVA: 0x006C58A0 File Offset: 0x006C3AA0
			public override UIElement ProvideSlider()
			{
				UIVerticalSlider uiverticalSlider = CreativePowersHelper.CreateSlider(new Func<float>(base.GetSliderValue), new Action<float>(base.SetValueKeyboard), new Action(base.SetValueGamepad));
				uiverticalSlider.OnUpdate += this.UpdateSliderAndShowMultiplierMouseOver;
				UIPanel uipanel = new UIPanel();
				uipanel.Width = new StyleDimension(77f, 0f);
				uipanel.Height = new StyleDimension(180f, 0f);
				uipanel.HAlign = 0f;
				uipanel.VAlign = 0.5f;
				uipanel.Append(uiverticalSlider);
				uipanel.OnUpdate += CreativePowersHelper.UpdateUseMouseInterface;
				UIText uitext = new UIText("x10", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0f
				};
				uitext.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext.OnMouseOut += this.Button_OnMouseOut;
				uitext.OnMouseOver += this.Button_OnMouseOver;
				uitext.OnLeftClick += this.topText_OnClick;
				uipanel.Append(uitext);
				UIText uitext2 = new UIText("x1", 1f, false)
				{
					HAlign = 1f,
					VAlign = 0.5f
				};
				uitext2.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext2.OnMouseOut += this.Button_OnMouseOut;
				uitext2.OnMouseOver += this.Button_OnMouseOver;
				uitext2.OnLeftClick += this.middleText_OnClick;
				uipanel.Append(uitext2);
				UIText uitext3 = new UIText("x0", 1f, false)
				{
					HAlign = 1f,
					VAlign = 1f
				};
				uitext3.OnUpdate += this.UpdateMouseOverNoItemText;
				uitext3.OnMouseOut += this.Button_OnMouseOut;
				uitext3.OnMouseOver += this.Button_OnMouseOver;
				uitext3.OnLeftClick += this.bottomText_OnClick;
				uipanel.Append(uitext3);
				return uipanel;
			}

			// Token: 0x060045BE RID: 17854 RVA: 0x006C5AAC File Offset: 0x006C3CAC
			private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Color.Black;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x060045BF RID: 17855 RVA: 0x006C5AE4 File Offset: 0x006C3CE4
			private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				UIText uitext = listeningElement as UIText;
				if (uitext != null)
				{
					uitext.ShadowColor = Main.OurFavoriteColor;
				}
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x060045C0 RID: 17856 RVA: 0x006C5B1B File Offset: 0x006C3D1B
			private void topText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboard(1f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x060045C1 RID: 17857 RVA: 0x006C5B3D File Offset: 0x006C3D3D
			private void middleText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboard(0.5f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x060045C2 RID: 17858 RVA: 0x006C5B5F File Offset: 0x006C3D5F
			private void bottomText_OnClick(UIMouseEvent evt, UIElement listeningElement)
			{
				base.SetValueKeyboard(0f);
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}

			// Token: 0x060045C3 RID: 17859 RVA: 0x006C5B84 File Offset: 0x006C3D84
			private void UpdateMouseOverNoItemText(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					Main.instance.MouseTextNoOverride(string.Empty, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x060045C4 RID: 17860 RVA: 0x006C5BB0 File Offset: 0x006C3DB0
			private void UpdateSliderAndShowMultiplierMouseOver(UIElement affectedElement)
			{
				if (affectedElement.IsMouseHovering)
				{
					string text = "x" + this.RemapSliderValueToPowerValue(base.GetSliderValue()).ToString("F2");
					if (this.GetShouldDisableSpawnsFor(Main.myPlayer))
					{
						text = Language.GetTextValue(this._powerNameKey + "EnemySpawnsDisabled");
					}
					CreativePowersHelper.AddPermissionTextIfNeeded(this, ref text);
					Main.instance.MouseTextNoOverride(text, 0, 0, -1, -1, -1, -1, 0);
				}
			}

			// Token: 0x060045C5 RID: 17861 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool GetIsUnlocked()
			{
				return true;
			}

			// Token: 0x060045C6 RID: 17862 RVA: 0x006C5C28 File Offset: 0x006C3E28
			public void Save(Player player, BinaryWriter writer)
			{
				float sliderCurrentValueCache = this._sliderCurrentValueCache;
				writer.Write(sliderCurrentValueCache);
			}

			// Token: 0x060045C7 RID: 17863 RVA: 0x006C5C43 File Offset: 0x006C3E43
			public void ResetDataForNewPlayer(Player player)
			{
				player.savedPerPlayerFieldsThatArentInThePlayerClass.spawnRatePowerSliderValue = this._sliderDefaultValue;
			}

			// Token: 0x060045C8 RID: 17864 RVA: 0x006C5C58 File Offset: 0x006C3E58
			public void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn)
			{
				float num = reader.ReadSingle();
				player.savedPerPlayerFieldsThatArentInThePlayerClass.spawnRatePowerSliderValue = num;
			}

			// Token: 0x060045C9 RID: 17865 RVA: 0x006C5C78 File Offset: 0x006C3E78
			public void ApplyLoadedDataToOutOfPlayerFields(Player player)
			{
				base.PushChangeAndSetSlider(player.savedPerPlayerFieldsThatArentInThePlayerClass.spawnRatePowerSliderValue);
			}

			// Token: 0x040072E4 RID: 29412
			[CompilerGenerated]
			private float <StrengthMultiplierToGiveNPCs>k__BackingField;
		}
	}
}

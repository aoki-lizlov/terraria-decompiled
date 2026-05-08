using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using ReLogic.OS;
using ReLogic.Peripherals.RGB;
using ReLogic.Peripherals.RGB.Corsair;
using ReLogic.Peripherals.RGB.Logitech;
using ReLogic.Peripherals.RGB.Razer;
using ReLogic.Peripherals.RGB.SteelSeries;
using SteelSeries.GameSense;
using SteelSeries.GameSense.DeviceZone;
using Terraria.GameContent.RGB;
using Terraria.GameInput;
using Terraria.IO;

namespace Terraria.Initializers
{
	// Token: 0x02000081 RID: 129
	public static class ChromaInitializer
	{
		// Token: 0x0600157F RID: 5503 RVA: 0x004C6C69 File Offset: 0x004C4E69
		public static void BindTo(Preferences preferences)
		{
			preferences.OnSave += ChromaInitializer.Configuration_OnSave;
			preferences.OnLoad += ChromaInitializer.Configuration_OnLoad;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x004C6C90 File Offset: 0x004C4E90
		private static void Configuration_OnLoad(Preferences obj)
		{
			ChromaInitializer._useRazer = obj.Get<bool>("UseRazerRGB", true);
			ChromaInitializer._useCorsair = obj.Get<bool>("UseCorsairRGB", true);
			ChromaInitializer._useLogitech = obj.Get<bool>("UseLogitechRGB", true);
			ChromaInitializer._useSteelSeries = obj.Get<bool>("UseSteelSeriesRGB", true);
			ChromaInitializer._razerColorProfile = obj.Get<VendorColorProfile>("RazerColorProfile", new VendorColorProfile(new Vector3(1f, 0.765f, 0.568f)));
			ChromaInitializer._corsairColorProfile = obj.Get<VendorColorProfile>("CorsairColorProfile", new VendorColorProfile());
			ChromaInitializer._logitechColorProfile = obj.Get<VendorColorProfile>("LogitechColorProfile", new VendorColorProfile());
			ChromaInitializer._steelSeriesColorProfile = obj.Get<VendorColorProfile>("SteelSeriesColorProfile", new VendorColorProfile());
			if (ChromaInitializer._razerColorProfile == null)
			{
				ChromaInitializer._razerColorProfile = new VendorColorProfile(new Vector3(1f, 0.765f, 0.568f));
			}
			if (ChromaInitializer._corsairColorProfile == null)
			{
				ChromaInitializer._corsairColorProfile = new VendorColorProfile();
			}
			if (ChromaInitializer._logitechColorProfile == null)
			{
				ChromaInitializer._logitechColorProfile = new VendorColorProfile();
			}
			if (ChromaInitializer._steelSeriesColorProfile == null)
			{
				ChromaInitializer._steelSeriesColorProfile = new VendorColorProfile();
			}
			ChromaInitializer._rgbUpdateRate = obj.Get<float>("RGBUpdatesPerSecond", 45f);
			if (ChromaInitializer._rgbUpdateRate <= 1E-07f)
			{
				ChromaInitializer._rgbUpdateRate = 45f;
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x004C6DCC File Offset: 0x004C4FCC
		private static void Configuration_OnSave(Preferences preferences)
		{
			preferences.Put("RGBUpdatesPerSecond", ChromaInitializer._rgbUpdateRate);
			preferences.Put("UseRazerRGB", ChromaInitializer._useRazer);
			preferences.Put("RazerColorProfile", ChromaInitializer._razerColorProfile);
			preferences.Put("UseCorsairRGB", ChromaInitializer._useCorsair);
			preferences.Put("CorsairColorProfile", ChromaInitializer._corsairColorProfile);
			preferences.Put("UseLogitechRGB", ChromaInitializer._useLogitech);
			preferences.Put("LogitechColorProfile", ChromaInitializer._logitechColorProfile);
			preferences.Put("UseSteelSeriesRGB", ChromaInitializer._useSteelSeries);
			preferences.Put("SteelSeriesColorProfile", ChromaInitializer._steelSeriesColorProfile);
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x004C6E84 File Offset: 0x004C5084
		private static void AddDevices()
		{
			ChromaInitializer._engine.AddDeviceGroup("Razer", new RazerDeviceGroup(ChromaInitializer._razerColorProfile));
			ChromaInitializer._engine.AddDeviceGroup("Corsair", new CorsairDeviceGroup(ChromaInitializer._corsairColorProfile));
			ChromaInitializer._engine.AddDeviceGroup("Logitech", new LogitechDeviceGroup(ChromaInitializer._logitechColorProfile));
			ChromaInitializer._engine.AddDeviceGroup("SteelSeries", new SteelSeriesDeviceGroup(ChromaInitializer._steelSeriesColorProfile, "TERRARIA", "Terraria", 3));
			ChromaInitializer._engine.FrameTimeInSeconds = 1f / ChromaInitializer._rgbUpdateRate;
			if (ChromaInitializer._useRazer)
			{
				ChromaInitializer._engine.EnableDeviceGroup("Razer");
			}
			if (ChromaInitializer._useCorsair)
			{
				ChromaInitializer._engine.EnableDeviceGroup("Corsair");
			}
			if (ChromaInitializer._useLogitech)
			{
				ChromaInitializer._engine.EnableDeviceGroup("Logitech");
			}
			if (ChromaInitializer._useSteelSeries)
			{
				ChromaInitializer._engine.EnableDeviceGroup("SteelSeries");
			}
			ChromaInitializer.LoadSpecialRulesForDevices();
			AppDomain.CurrentDomain.ProcessExit += ChromaInitializer.OnProcessExit;
			if (Platform.IsWindows)
			{
				Application.ApplicationExit += ChromaInitializer.OnProcessExit;
			}
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x004C6FA0 File Offset: 0x004C51A0
		private static void LoadSpecialRulesForDevices()
		{
			ChromaInitializer.Event_LifePercent = new IntRgbGameValueTracker
			{
				EventName = "LIFE"
			};
			ChromaInitializer.Event_ManaPercent = new IntRgbGameValueTracker
			{
				EventName = "MANA"
			};
			ChromaInitializer.Event_BreathPercent = new IntRgbGameValueTracker
			{
				EventName = "BREATH"
			};
			ChromaInitializer.LoadSpecialRulesFor_GameSense();
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x004C6FF4 File Offset: 0x004C51F4
		public static void UpdateEvents()
		{
			if (Main.gameMenu)
			{
				ChromaInitializer.Event_LifePercent.Update(0, false);
				ChromaInitializer.Event_ManaPercent.Update(0, false);
				ChromaInitializer.Event_BreathPercent.Update(0, false);
				return;
			}
			Player localPlayer = Main.LocalPlayer;
			int num = (int)Utils.Clamp<float>((float)localPlayer.statLife / (float)localPlayer.statLifeMax2 * 100f, 0f, 100f);
			ChromaInitializer.Event_LifePercent.Update(num, true);
			int num2 = (int)Utils.Clamp<float>((float)localPlayer.statMana / (float)localPlayer.statManaMax2 * 100f, 0f, 100f);
			ChromaInitializer.Event_ManaPercent.Update(num2, true);
			int num3 = (int)Utils.Clamp<float>((float)localPlayer.breath / (float)localPlayer.breathMax * 100f, 0f, 100f);
			ChromaInitializer.Event_BreathPercent.Update(num3, true);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x004C70CC File Offset: 0x004C52CC
		private static void LoadSpecialRulesFor_GameSense()
		{
			GameSenseSpecificInfo gameSenseSpecificInfo = new GameSenseSpecificInfo();
			List<Bind_Event> list = new List<Bind_Event>();
			gameSenseSpecificInfo.EventsToBind = list;
			ChromaInitializer.LoadSpecialRulesFor_GameSense_Keyboard(list);
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE1", "zone1", new RGBZonedDevice("one"));
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE2", "zone2", new RGBZonedDevice("two"));
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE3", "zone3", new RGBZonedDevice("three"));
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE4", "zone4", new RGBZonedDevice("four"));
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE5", "zone5", new RGBZonedDevice("five"));
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE6", "zone6", new RGBZonedDevice("six"));
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE7", "zone7", new RGBZonedDevice("seven"));
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE8", "zone8", new RGBZonedDevice("eight"));
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE9", "zone9", new RGBZonedDevice("nine"));
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE10", "zone10", new RGBZonedDevice("ten"));
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE11", "zone11", new RGBZonedDevice("eleven"));
			ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(list, "ZONE12", "zone12", new RGBZonedDevice("twelve"));
			ChromaInitializer.AddGameplayEvents(list);
			gameSenseSpecificInfo.MiscEvents = new List<ARgbGameValueTracker>
			{
				ChromaInitializer.Event_LifePercent,
				ChromaInitializer.Event_ManaPercent,
				ChromaInitializer.Event_BreathPercent
			};
			foreach (Bind_Event bind_Event in gameSenseSpecificInfo.EventsToBind)
			{
				ChromaInitializer.EventLocalization eventLocalization;
				if (ChromaInitializer._localizedEvents.TryGetValue(bind_Event.eventName, out eventLocalization))
				{
					bind_Event.defaultDisplayName = eventLocalization.DefaultDisplayName;
					bind_Event.localizedDisplayNames = eventLocalization.LocalizedNames;
				}
			}
			ChromaInitializer._engine.LoadSpecialRules(gameSenseSpecificInfo);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x004C72D4 File Offset: 0x004C54D4
		private static void AddGameplayEvents(List<Bind_Event> eventsToBind)
		{
			eventsToBind.Add(new Bind_Event("TERRARIA", ChromaInitializer.Event_LifePercent.EventName, 0, 100, 1, new AbstractHandler[0]));
			eventsToBind.Add(new Bind_Event("TERRARIA", ChromaInitializer.Event_ManaPercent.EventName, 0, 100, 14, new AbstractHandler[0]));
			eventsToBind.Add(new Bind_Event("TERRARIA", ChromaInitializer.Event_BreathPercent.EventName, 0, 100, 11, new AbstractHandler[0]));
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x004C7350 File Offset: 0x004C5550
		private static void LoadSpecialRulesFor_SecondaryDevice(List<Bind_Event> eventsToBind, string eventName, string contextFrameKey, AbstractIlluminationDevice_Zone zone)
		{
			Bind_Event bind_Event = new Bind_Event("TERRARIA", eventName, 0, 10, 0, new AbstractHandler[]
			{
				new ContextColorEventHandlerType
				{
					ContextFrameKey = contextFrameKey,
					DeviceZone = zone
				}
			});
			eventsToBind.Add(bind_Event);
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x004C7390 File Offset: 0x004C5590
		private static void LoadSpecialRulesFor_GameSense_Keyboard(List<Bind_Event> eventsToBind)
		{
			Dictionary<string, byte> xnaKeyNamesToSteelSeriesKeyIndex = HIDCodes.XnaKeyNamesToSteelSeriesKeyIndex;
			Color white = Color.White;
			foreach (KeyValuePair<string, List<string>> keyValuePair in PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus)
			{
				string key = keyValuePair.Key;
				List<string> value = keyValuePair.Value;
				List<byte> list = new List<byte>();
				foreach (string text in value)
				{
					byte b;
					if (xnaKeyNamesToSteelSeriesKeyIndex.TryGetValue(text, out b))
					{
						list.Add(b);
					}
				}
				RGBPerkeyZoneCustom rgbperkeyZoneCustom = new RGBPerkeyZoneCustom(list.ToArray());
				ColorStatic colorStatic = new ColorStatic();
				colorStatic.red = white.R;
				colorStatic.green = white.G;
				colorStatic.blue = white.B;
				Bind_Event bind_Event = new Bind_Event("TERRARIA", "KEY_" + key.ToUpper(), 0, 10, 0, new AbstractHandler[]
				{
					new ContextColorEventHandlerType
					{
						ContextFrameKey = key,
						DeviceZone = rgbperkeyZoneCustom
					}
				});
				eventsToBind.Add(bind_Event);
			}
			string text2 = "TERRARIA";
			string text3 = "DO_RAINBOWS";
			int num = 0;
			int num2 = 10;
			EventIconId eventIconId = 0;
			AbstractHandler[] array = new AbstractHandler[1];
			int num3 = 0;
			PartialBitmapEventHandlerType partialBitmapEventHandlerType = new PartialBitmapEventHandlerType();
			partialBitmapEventHandlerType.EventsToExclude = eventsToBind.Select((Bind_Event x) => x.eventName).ToArray<string>();
			array[num3] = partialBitmapEventHandlerType;
			Bind_Event bind_Event2 = new Bind_Event(text2, text3, num, num2, eventIconId, array);
			eventsToBind.Add(bind_Event2);
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x004C753C File Offset: 0x004C573C
		public static void DisableAllDeviceGroups()
		{
			if (ChromaInitializer._engine != null)
			{
				ChromaInitializer._engine.DisableAllDeviceGroups();
			}
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x004C754F File Offset: 0x004C574F
		private static void OnProcessExit(object sender, EventArgs e)
		{
			ChromaInitializer.DisableAllDeviceGroups();
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x004C7558 File Offset: 0x004C5758
		public static void Load()
		{
			ChromaInitializer._engine = Main.Chroma;
			ChromaInitializer.AddDevices();
			Color color = new Color(46, 23, 12);
			ChromaInitializer.RegisterShader("Base", new SurfaceBiomeShader(Color.Green, color), CommonConditions.InMenu, 9);
			ChromaInitializer.RegisterShader("Surface Mushroom", new SurfaceBiomeShader(Color.DarkBlue, new Color(33, 31, 27)), CommonConditions.DrunkMenu, 9);
			ChromaInitializer.RegisterShader("Sky", new SkyShader(new Color(34, 51, 128), new Color(5, 5, 5)), CommonConditions.Depth.Sky, 1);
			ChromaInitializer.RegisterShader("Surface", new SurfaceBiomeShader(Color.Green, color), CommonConditions.Depth.Surface, 1);
			ChromaInitializer.RegisterShader("Vines", new VineShader(), CommonConditions.Depth.Vines, 1);
			ChromaInitializer.RegisterShader("Underground", new CavernShader(new Color(122, 62, 32), new Color(25, 13, 7), 0.5f), CommonConditions.Depth.Underground, 1);
			ChromaInitializer.RegisterShader("Caverns", new CavernShader(color, new Color(25, 25, 25), 0.5f), CommonConditions.Depth.Caverns, 1);
			ChromaInitializer.RegisterShader("Magma", new CavernShader(new Color(181, 17, 0), new Color(25, 25, 25), 0.5f), CommonConditions.Depth.Magma, 1);
			ChromaInitializer.RegisterShader("Underworld", new UnderworldShader(Color.Red, new Color(1f, 0.5f, 0f), 1f), CommonConditions.Depth.Underworld, 1);
			ChromaInitializer.RegisterShader("Surface Desert", new SurfaceBiomeShader(new Color(84, 49, 0), new Color(245, 225, 33)), CommonConditions.SurfaceBiome.Desert, 2);
			ChromaInitializer.RegisterShader("Surface Jungle", new SurfaceBiomeShader(Color.Green, Color.Teal), CommonConditions.SurfaceBiome.Jungle, 2);
			ChromaInitializer.RegisterShader("Surface Ocean", new SurfaceBiomeShader(Color.SkyBlue, Color.Blue), CommonConditions.SurfaceBiome.Ocean, 2);
			ChromaInitializer.RegisterShader("Surface Snow", new SurfaceBiomeShader(new Color(0, 10, 50), new Color(0.5f, 0.75f, 1f)), CommonConditions.SurfaceBiome.Snow, 2);
			ChromaInitializer.RegisterShader("Surface Mushroom", new SurfaceBiomeShader(Color.DarkBlue, new Color(33, 31, 27)), CommonConditions.SurfaceBiome.Mushroom, 2);
			ChromaInitializer.RegisterShader("Surface Hallow", new HallowSurfaceShader(), CommonConditions.SurfaceBiome.Hallow, 3);
			ChromaInitializer.RegisterShader("Surface Crimson", new CorruptSurfaceShader(Color.Red, new Color(25, 25, 40)), CommonConditions.SurfaceBiome.Crimson, 3);
			ChromaInitializer.RegisterShader("Surface Corruption", new CorruptSurfaceShader(new Color(73, 0, 255), new Color(15, 15, 27)), CommonConditions.SurfaceBiome.Corruption, 3);
			ChromaInitializer.RegisterShader("Hive", new DrippingShader(new Color(0.05f, 0.01f, 0f), new Color(255, 150, 0), 0.5f), CommonConditions.UndergroundBiome.Hive, 3);
			ChromaInitializer.RegisterShader("Underground Mushroom", new UndergroundMushroomShader(), CommonConditions.UndergroundBiome.Mushroom, 2);
			ChromaInitializer.RegisterShader("Underground Corrutpion", new UndergroundCorruptionShader(), CommonConditions.UndergroundBiome.Corrupt, 2);
			ChromaInitializer.RegisterShader("Underground Crimson", new DrippingShader(new Color(0.05f, 0f, 0f), new Color(255, 0, 0), 1f), CommonConditions.UndergroundBiome.Crimson, 2);
			ChromaInitializer.RegisterShader("Underground Hallow", new UndergroundHallowShader(), CommonConditions.UndergroundBiome.Hallow, 2);
			ChromaInitializer.RegisterShader("Meteorite", new MeteoriteShader(), CommonConditions.MiscBiome.Meteorite, 3);
			ChromaInitializer.RegisterShader("Temple", new TempleShader(), CommonConditions.UndergroundBiome.Temple, 3);
			ChromaInitializer.RegisterShader("Dungeon", new DungeonShader(), CommonConditions.UndergroundBiome.Dungeon, 3);
			ChromaInitializer.RegisterShader("Granite", new CavernShader(new Color(14, 19, 46), new Color(5, 0, 30), 0.5f), CommonConditions.UndergroundBiome.Granite, 3);
			ChromaInitializer.RegisterShader("Marble", new CavernShader(new Color(100, 100, 100), new Color(20, 20, 20), 0.5f), CommonConditions.UndergroundBiome.Marble, 3);
			ChromaInitializer.RegisterShader("Gem Cave", new GemCaveShader(color, new Color(25, 25, 25), new Vector4[]
			{
				Color.White.ToVector4(),
				Color.Yellow.ToVector4(),
				Color.Orange.ToVector4(),
				Color.Red.ToVector4(),
				Color.Green.ToVector4(),
				Color.Blue.ToVector4(),
				Color.Purple.ToVector4()
			})
			{
				CycleTime = 100f,
				ColorRarity = 20f,
				TimeRate = 0.25f
			}, CommonConditions.UndergroundBiome.GemCave, 3);
			Vector4[] array = new Vector4[12];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Main.hslToRgb((float)i / (float)array.Length, 1f, 0.5f, byte.MaxValue).ToVector4();
			}
			ChromaInitializer.RegisterShader("Shimmer", new GemCaveShader(Color.Silver * 0.5f, new Color(125, 55, 125), array)
			{
				CycleTime = 2f,
				ColorRarity = 4f,
				TimeRate = 0.5f
			}, CommonConditions.UndergroundBiome.Shimmer, 3);
			ChromaInitializer.RegisterShader("Underground Jungle", new JungleShader(), CommonConditions.UndergroundBiome.Jungle, 2);
			ChromaInitializer.RegisterShader("Underground Ice", new IceShader(new Color(0, 10, 50), new Color(0.5f, 0.75f, 1f)), CommonConditions.UndergroundBiome.Ice, 2);
			ChromaInitializer.RegisterShader("Corrupt Ice", new IceShader(new Color(5, 0, 25), new Color(152, 102, 255)), CommonConditions.UndergroundBiome.CorruptIce, 3);
			ChromaInitializer.RegisterShader("Crimson Ice", new IceShader(new Color(0.1f, 0f, 0f), new Color(1f, 0.45f, 0.4f)), CommonConditions.UndergroundBiome.CrimsonIce, 3);
			ChromaInitializer.RegisterShader("Hallow Ice", new IceShader(new Color(0.2f, 0f, 0.1f), new Color(1f, 0.7f, 0.7f)), CommonConditions.UndergroundBiome.HallowIce, 3);
			ChromaInitializer.RegisterShader("Underground Desert", new DesertShader(new Color(60, 10, 0), new Color(255, 165, 0)), CommonConditions.UndergroundBiome.Desert, 2);
			ChromaInitializer.RegisterShader("Corrupt Desert", new DesertShader(new Color(15, 0, 15), new Color(116, 103, 255)), CommonConditions.UndergroundBiome.CorruptDesert, 3);
			ChromaInitializer.RegisterShader("Crimson Desert", new DesertShader(new Color(20, 10, 0), new Color(195, 0, 0)), CommonConditions.UndergroundBiome.CrimsonDesert, 3);
			ChromaInitializer.RegisterShader("Hallow Desert", new DesertShader(new Color(29, 0, 56), new Color(255, 221, 255)), CommonConditions.UndergroundBiome.HallowDesert, 3);
			ChromaInitializer.RegisterShader("Pumpkin Moon", new MoonShader(new Color(13, 0, 26), Color.Orange), CommonConditions.Events.PumpkinMoon, 4);
			ChromaInitializer.RegisterShader("Blood Moon", new MoonShader(new Color(10, 0, 0), Color.Red, Color.Red, new Color(255, 150, 125)), CommonConditions.Events.BloodMoon, 4);
			ChromaInitializer.RegisterShader("Frost Moon", new MoonShader(new Color(0, 4, 13), new Color(255, 255, 255)), CommonConditions.Events.FrostMoon, 4);
			ChromaInitializer.RegisterShader("Solar Eclipse", new MoonShader(new Color(0.02f, 0.02f, 0.02f), Color.Orange, Color.Black), CommonConditions.Events.SolarEclipse, 4);
			ChromaInitializer.RegisterShader("Pirate Invasion", new PirateInvasionShader(new Color(173, 173, 173), new Color(101, 101, 255), Color.Blue, Color.Black), CommonConditions.Events.PirateInvasion, 4);
			ChromaInitializer.RegisterShader("DD2 Event", new DD2Shader(new Color(222, 94, 245), Color.White), CommonConditions.Events.DD2Event, 4);
			ChromaInitializer.RegisterShader("Goblin Army", new GoblinArmyShader(new Color(14, 0, 79), new Color(176, 0, 144)), CommonConditions.Events.GoblinArmy, 4);
			ChromaInitializer.RegisterShader("Frost Legion", new FrostLegionShader(Color.White, new Color(27, 80, 201)), CommonConditions.Events.FrostLegion, 4);
			ChromaInitializer.RegisterShader("Martian Madness", new MartianMadnessShader(new Color(64, 64, 64), new Color(64, 113, 122), new Color(255, 255, 0), new Color(3, 3, 18)), CommonConditions.Events.MartianMadness, 4);
			ChromaInitializer.RegisterShader("Solar Pillar", new PillarShader(Color.Red, Color.Orange), CommonConditions.Events.SolarPillar, 4);
			ChromaInitializer.RegisterShader("Nebula Pillar", new PillarShader(new Color(255, 144, 209), new Color(100, 0, 76)), CommonConditions.Events.NebulaPillar, 4);
			ChromaInitializer.RegisterShader("Vortex Pillar", new PillarShader(Color.Green, Color.Black), CommonConditions.Events.VortexPillar, 4);
			ChromaInitializer.RegisterShader("Stardust Pillar", new PillarShader(new Color(46, 63, 255), Color.White), CommonConditions.Events.StardustPillar, 4);
			ChromaInitializer.RegisterShader("Eater of Worlds", new WormShader(new Color(14, 0, 15), new Color(47, 51, 59), new Color(20, 25, 11)), CommonConditions.Boss.EaterOfWorlds, 5);
			ChromaInitializer.RegisterShader("Eye of Cthulhu", new EyeOfCthulhuShader(new Color(145, 145, 126), new Color(138, 0, 0), new Color(3, 3, 18)), CommonConditions.Boss.EyeOfCthulhu, 5);
			ChromaInitializer.RegisterShader("Skeletron", new SkullShader(new Color(110, 92, 47), new Color(36, 32, 51), new Color(0, 0, 0)), CommonConditions.Boss.Skeletron, 5);
			ChromaInitializer.RegisterShader("Brain Of Cthulhu", new BrainShader(new Color(54, 0, 0), new Color(186, 137, 139)), CommonConditions.Boss.BrainOfCthulhu, 5);
			ChromaInitializer.RegisterShader("Empress of Light", new EmpressShader(), CommonConditions.Boss.Empress, 5);
			ChromaInitializer.RegisterShader("Queen Slime", new QueenSlimeShader(new Color(72, 41, 130), new Color(126, 220, 255)), CommonConditions.Boss.QueenSlime, 5);
			ChromaInitializer.RegisterShader("King Slime", new KingSlimeShader(new Color(41, 70, 130), Color.White), CommonConditions.Boss.KingSlime, 5);
			ChromaInitializer.RegisterShader("Queen Bee", new QueenBeeShader(new Color(5, 5, 0), new Color(255, 235, 0)), CommonConditions.Boss.QueenBee, 5);
			ChromaInitializer.RegisterShader("Wall of Flesh", new WallOfFleshShader(new Color(112, 48, 60), new Color(5, 0, 0)), CommonConditions.Boss.WallOfFlesh, 5);
			ChromaInitializer.RegisterShader("Destroyer", new WormShader(new Color(25, 25, 25), new Color(192, 0, 0), new Color(10, 0, 0)), CommonConditions.Boss.Destroyer, 5);
			ChromaInitializer.RegisterShader("Skeletron Prime", new SkullShader(new Color(110, 92, 47), new Color(79, 0, 0), new Color(255, 29, 0)), CommonConditions.Boss.SkeletronPrime, 5);
			ChromaInitializer.RegisterShader("The Twins", new TwinsShader(new Color(145, 145, 126), new Color(138, 0, 0), new Color(138, 0, 0), new Color(20, 20, 20), new Color(65, 140, 0), new Color(3, 3, 18)), CommonConditions.Boss.TheTwins, 5);
			ChromaInitializer.RegisterShader("Duke Fishron", new DukeFishronShader(new Color(0, 0, 122), new Color(100, 254, 194)), CommonConditions.Boss.DukeFishron, 5);
			ChromaInitializer.RegisterShader("Deerclops", new BlizzardShader(new Vector4(1f, 1f, 1f, 1f), new Vector4(0.15f, 0.1f, 0.4f, 1f), -0.1f, 0.4f), CommonConditions.Boss.Deerclops, 5);
			ChromaInitializer.RegisterShader("Plantera", new PlanteraShader(new Color(255, 0, 220), new Color(0, 255, 0), new Color(12, 4, 0)), CommonConditions.Boss.Plantera, 5);
			ChromaInitializer.RegisterShader("Golem", new GolemShader(new Color(255, 144, 0), new Color(255, 198, 0), new Color(10, 10, 0)), CommonConditions.Boss.Golem, 5);
			ChromaInitializer.RegisterShader("Cultist", new CultistShader(), CommonConditions.Boss.Cultist, 5);
			ChromaInitializer.RegisterShader("Moon Lord", new EyeballShader(false), CommonConditions.Boss.MoonLord, 5);
			ChromaInitializer.RegisterShader("Rain", new RainShader(), CommonConditions.Weather.Rain, 6);
			ChromaInitializer.RegisterShader("Snowstorm", new BlizzardShader(new Vector4(1f, 1f, 1f, 1f), new Vector4(0.1f, 0.1f, 0.3f, 1f), 0.35f, -0.35f), CommonConditions.Weather.Blizzard, 6);
			ChromaInitializer.RegisterShader("Sandstorm", new SandstormShader(), CommonConditions.Weather.Sandstorm, 6);
			ChromaInitializer.RegisterShader("Slime Rain", new SlimeRainShader(), CommonConditions.Weather.SlimeRain, 6);
			ChromaInitializer.RegisterShader("Drowning", new DrowningShader(), CommonConditions.Alert.Drowning, 7);
			ChromaInitializer.RegisterShader("Keybinds", new KeybindsMenuShader(), CommonConditions.Alert.Keybinds, 7);
			ChromaInitializer.RegisterShader("Lava Indicator", new LavaIndicatorShader(Color.Black, Color.Red, new Color(255, 188, 0)), CommonConditions.Alert.LavaIndicator, 7);
			ChromaInitializer.RegisterShader("Moon Lord Spawn", new EyeballShader(true), CommonConditions.Alert.MoonlordComing, 7);
			ChromaInitializer.RegisterShader("Low Life", new LowLifeShader(), CommonConditions.CriticalAlert.LowLife, 8);
			ChromaInitializer.RegisterShader("Death", new DeathShader(new Color(36, 0, 10), new Color(158, 28, 53)), CommonConditions.CriticalAlert.Death, 8);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x004C8395 File Offset: 0x004C6595
		private static void RegisterShader(string name, ChromaShader shader, ChromaCondition condition, ShaderLayer layer)
		{
			ChromaInitializer._engine.RegisterShader(shader, condition, layer);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x004C83A4 File Offset: 0x004C65A4
		[Conditional("DEBUG")]
		private static void AddDebugDraw()
		{
			new BasicDebugDrawer(Main.instance.GraphicsDevice);
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x004C83B8 File Offset: 0x004C65B8
		// Note: this type is marked as 'beforefieldinit'.
		static ChromaInitializer()
		{
		}

		// Token: 0x04001106 RID: 4358
		private static ChromaEngine _engine;

		// Token: 0x04001107 RID: 4359
		private const string GAME_NAME_ID = "TERRARIA";

		// Token: 0x04001108 RID: 4360
		private static float _rgbUpdateRate = 45f;

		// Token: 0x04001109 RID: 4361
		private static bool _useRazer = true;

		// Token: 0x0400110A RID: 4362
		private static bool _useCorsair = true;

		// Token: 0x0400110B RID: 4363
		private static bool _useLogitech = true;

		// Token: 0x0400110C RID: 4364
		private static bool _useSteelSeries = true;

		// Token: 0x0400110D RID: 4365
		private static VendorColorProfile _razerColorProfile = new VendorColorProfile(new Vector3(1f, 0.765f, 0.568f));

		// Token: 0x0400110E RID: 4366
		private static VendorColorProfile _corsairColorProfile = new VendorColorProfile();

		// Token: 0x0400110F RID: 4367
		private static VendorColorProfile _logitechColorProfile = new VendorColorProfile();

		// Token: 0x04001110 RID: 4368
		private static VendorColorProfile _steelSeriesColorProfile = new VendorColorProfile();

		// Token: 0x04001111 RID: 4369
		private static Dictionary<string, ChromaInitializer.EventLocalization> _localizedEvents = new Dictionary<string, ChromaInitializer.EventLocalization>
		{
			{
				"KEY_MOUSELEFT",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Left Mouse Button"
				}
			},
			{
				"KEY_MOUSERIGHT",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Right Mouse Button"
				}
			},
			{
				"KEY_UP",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Up"
				}
			},
			{
				"KEY_DOWN",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Down"
				}
			},
			{
				"KEY_LEFT",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Left"
				}
			},
			{
				"KEY_RIGHT",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Right"
				}
			},
			{
				"KEY_JUMP",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Jump"
				}
			},
			{
				"KEY_THROW",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Throw"
				}
			},
			{
				"KEY_INVENTORY",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Inventory"
				}
			},
			{
				"KEY_GRAPPLE",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Grapple"
				}
			},
			{
				"KEY_SMARTSELECT",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Smart Select"
				}
			},
			{
				"KEY_SMARTCURSOR",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Smart Cursor"
				}
			},
			{
				"KEY_QUICKMOUNT",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Quick Mount"
				}
			},
			{
				"KEY_QUICKHEAL",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Quick Heal"
				}
			},
			{
				"KEY_QUICKMANA",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Quick Mana"
				}
			},
			{
				"KEY_QUICKBUFF",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Quick Buff"
				}
			},
			{
				"KEY_MAPZOOMIN",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Map Zoom In"
				}
			},
			{
				"KEY_MAPZOOMOUT",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Map Zoom Out"
				}
			},
			{
				"KEY_MAPALPHAUP",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Map Transparency Up"
				}
			},
			{
				"KEY_MAPALPHADOWN",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Map Transparency Down"
				}
			},
			{
				"KEY_MAPFULL",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Map Full"
				}
			},
			{
				"KEY_MAPSTYLE",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Map Style"
				}
			},
			{
				"KEY_HOTBAR1",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar 1"
				}
			},
			{
				"KEY_HOTBAR2",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar 2"
				}
			},
			{
				"KEY_HOTBAR3",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar 3"
				}
			},
			{
				"KEY_HOTBAR4",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar 4"
				}
			},
			{
				"KEY_HOTBAR5",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar 5"
				}
			},
			{
				"KEY_HOTBAR6",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar 6"
				}
			},
			{
				"KEY_HOTBAR7",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar 7"
				}
			},
			{
				"KEY_HOTBAR8",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar 8"
				}
			},
			{
				"KEY_HOTBAR9",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar 9"
				}
			},
			{
				"KEY_HOTBAR10",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar 10"
				}
			},
			{
				"KEY_HOTBARMINUS",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar Minus"
				}
			},
			{
				"KEY_HOTBARPLUS",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Hotbar Plus"
				}
			},
			{
				"KEY_DPADRADIAL1",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Dpad Radial 1"
				}
			},
			{
				"KEY_DPADRADIAL2",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Dpad Radial 2"
				}
			},
			{
				"KEY_DPADRADIAL3",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Dpad Radial 3"
				}
			},
			{
				"KEY_DPADRADIAL4",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Dpad Radial 4"
				}
			},
			{
				"KEY_RADIALHOTBAR",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Radial Hotbar"
				}
			},
			{
				"KEY_RADIALQUICKBAR",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Radial Quickbar"
				}
			},
			{
				"KEY_DPADSNAP1",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Dpad Snap 1"
				}
			},
			{
				"KEY_DPADSNAP2",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Dpad Snap 2"
				}
			},
			{
				"KEY_DPADSNAP3",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Dpad Snap 3"
				}
			},
			{
				"KEY_DPADSNAP4",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Dpad Snap 4"
				}
			},
			{
				"KEY_MENUUP",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Menu Up"
				}
			},
			{
				"KEY_MENUDOWN",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Menu Down"
				}
			},
			{
				"KEY_MENULEFT",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Menu Left"
				}
			},
			{
				"KEY_MENURIGHT",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Menu Right"
				}
			},
			{
				"KEY_LOCKON",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Lock On"
				}
			},
			{
				"KEY_VIEWZOOMIN",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zoom In"
				}
			},
			{
				"KEY_VIEWZOOMOUT",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zoom Out"
				}
			},
			{
				"KEY_TOGGLECREATIVEMENU",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Toggle Creative Menu"
				}
			},
			{
				"DO_RAINBOWS",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Theme"
				}
			},
			{
				"ZONE1",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 1"
				}
			},
			{
				"ZONE2",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 2"
				}
			},
			{
				"ZONE3",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 3"
				}
			},
			{
				"ZONE4",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 4"
				}
			},
			{
				"ZONE5",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 5"
				}
			},
			{
				"ZONE6",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 6"
				}
			},
			{
				"ZONE7",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 7"
				}
			},
			{
				"ZONE8",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 8"
				}
			},
			{
				"ZONE9",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 9"
				}
			},
			{
				"ZONE10",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 10"
				}
			},
			{
				"ZONE11",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 11"
				}
			},
			{
				"ZONE12",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Zone 12"
				}
			},
			{
				"LIFE",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Life Percent"
				}
			},
			{
				"MANA",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Mana Percent"
				}
			},
			{
				"BREATH",
				new ChromaInitializer.EventLocalization
				{
					DefaultDisplayName = "Breath Percent"
				}
			}
		};

		// Token: 0x04001112 RID: 4370
		public static IntRgbGameValueTracker Event_LifePercent;

		// Token: 0x04001113 RID: 4371
		public static IntRgbGameValueTracker Event_ManaPercent;

		// Token: 0x04001114 RID: 4372
		public static IntRgbGameValueTracker Event_BreathPercent;

		// Token: 0x0200066C RID: 1644
		public struct EventLocalization
		{
			// Token: 0x040066AC RID: 26284
			public string DefaultDisplayName;

			// Token: 0x040066AD RID: 26285
			public Dictionary<string, string> LocalizedNames;
		}

		// Token: 0x0200066D RID: 1645
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003DBD RID: 15805 RVA: 0x006932A0 File Offset: 0x006914A0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003DBE RID: 15806 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003DBF RID: 15807 RVA: 0x006932AC File Offset: 0x006914AC
			internal string <LoadSpecialRulesFor_GameSense_Keyboard>b__25_0(Bind_Event x)
			{
				return x.eventName;
			}

			// Token: 0x040066AE RID: 26286
			public static readonly ChromaInitializer.<>c <>9 = new ChromaInitializer.<>c();

			// Token: 0x040066AF RID: 26287
			public static Func<Bind_Event, string> <>9__25_0;
		}
	}
}

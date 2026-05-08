using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Reflection;
using Terraria.DataStructures;

namespace Terraria.ID
{
	// Token: 0x020001C5 RID: 453
	public class NPCID
	{
		// Token: 0x06001F60 RID: 8032 RVA: 0x00519A6C File Offset: 0x00517C6C
		public static int FromLegacyName(string name)
		{
			int num;
			if (NPCID.LegacyNameToIdMap.TryGetValue(name, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00519A8B File Offset: 0x00517C8B
		public static int FromNetId(int id)
		{
			if (id < 0)
			{
				return NPCID.NetIdMap[-id - 1];
			}
			return id;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0000357B File Offset: 0x0000177B
		public NPCID()
		{
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00519AA0 File Offset: 0x00517CA0
		// Note: this type is marked as 'beforefieldinit'.
		static NPCID()
		{
		}

		// Token: 0x040043F7 RID: 17399
		private static readonly int[] NetIdMap = new int[]
		{
			81, 81, 1, 1, 1, 1, 1, 1, 1, 1,
			6, 6, 31, 31, 77, 42, 42, 176, 176, 176,
			176, 173, 173, 183, 183, 3, 3, 132, 132, 186,
			186, 187, 187, 188, 188, 189, 189, 190, 191, 192,
			193, 194, 2, 200, 200, 21, 21, 201, 201, 202,
			202, 203, 203, 223, 223, 231, 231, 232, 232, 233,
			233, 234, 234, 235, 235
		};

		// Token: 0x040043F8 RID: 17400
		private static readonly Dictionary<string, int> LegacyNameToIdMap = new Dictionary<string, int>
		{
			{ "Slimeling", -1 },
			{ "Slimer2", -2 },
			{ "Green Slime", -3 },
			{ "Pinky", -4 },
			{ "Baby Slime", -5 },
			{ "Black Slime", -6 },
			{ "Purple Slime", -7 },
			{ "Red Slime", -8 },
			{ "Yellow Slime", -9 },
			{ "Jungle Slime", -10 },
			{ "Little Eater", -11 },
			{ "Big Eater", -12 },
			{ "Short Bones", -13 },
			{ "Big Boned", -14 },
			{ "Heavy Skeleton", -15 },
			{ "Little Stinger", -16 },
			{ "Big Stinger", -17 },
			{ "Tiny Moss Hornet", -18 },
			{ "Little Moss Hornet", -19 },
			{ "Big Moss Hornet", -20 },
			{ "Giant Moss Hornet", -21 },
			{ "Little Crimera", -22 },
			{ "Big Crimera", -23 },
			{ "Little Crimslime", -24 },
			{ "Big Crimslime", -25 },
			{ "Small Zombie", -26 },
			{ "Big Zombie", -27 },
			{ "Small Bald Zombie", -28 },
			{ "Big Bald Zombie", -29 },
			{ "Small Pincushion Zombie", -30 },
			{ "Big Pincushion Zombie", -31 },
			{ "Small Slimed Zombie", -32 },
			{ "Big Slimed Zombie", -33 },
			{ "Small Swamp Zombie", -34 },
			{ "Big Swamp Zombie", -35 },
			{ "Small Twiggy Zombie", -36 },
			{ "Big Twiggy Zombie", -37 },
			{ "Cataract Eye 2", -38 },
			{ "Sleepy Eye 2", -39 },
			{ "Dialated Eye 2", -40 },
			{ "Green Eye 2", -41 },
			{ "Purple Eye 2", -42 },
			{ "Demon Eye 2", -43 },
			{ "Small Female Zombie", -44 },
			{ "Big Female Zombie", -45 },
			{ "Small Skeleton", -46 },
			{ "Big Skeleton", -47 },
			{ "Small Headache Skeleton", -48 },
			{ "Big Headache Skeleton", -49 },
			{ "Small Misassembled Skeleton", -50 },
			{ "Big Misassembled Skeleton", -51 },
			{ "Small Pantless Skeleton", -52 },
			{ "Big Pantless Skeleton", -53 },
			{ "Small Rain Zombie", -54 },
			{ "Big Rain Zombie", -55 },
			{ "Little Hornet Fatty", -56 },
			{ "Big Hornet Fatty", -57 },
			{ "Little Hornet Honey", -58 },
			{ "Big Hornet Honey", -59 },
			{ "Little Hornet Leafy", -60 },
			{ "Big Hornet Leafy", -61 },
			{ "Little Hornet Spikey", -62 },
			{ "Big Hornet Spikey", -63 },
			{ "Little Hornet Stingy", -64 },
			{ "Big Hornet Stingy", -65 },
			{ "Blue Slime", 1 },
			{ "Demon Eye", 2 },
			{ "Zombie", 3 },
			{ "Eye of Cthulhu", 4 },
			{ "Servant of Cthulhu", 5 },
			{ "Eater of Souls", 6 },
			{ "Devourer", 7 },
			{ "Giant Worm", 10 },
			{ "Eater of Worlds", 13 },
			{ "Mother Slime", 16 },
			{ "Merchant", 17 },
			{ "Nurse", 18 },
			{ "Arms Dealer", 19 },
			{ "Dryad", 20 },
			{ "Skeleton", 21 },
			{ "Guide", 22 },
			{ "Meteor Head", 23 },
			{ "Fire Imp", 24 },
			{ "Burning Sphere", 25 },
			{ "Goblin Peon", 26 },
			{ "Goblin Thief", 27 },
			{ "Goblin Warrior", 28 },
			{ "Goblin Sorcerer", 29 },
			{ "Chaos Ball", 30 },
			{ "Angry Bones", 31 },
			{ "Dark Caster", 32 },
			{ "Water Sphere", 33 },
			{ "Cursed Skull", 34 },
			{ "Skeletron", 35 },
			{ "Old Man", 37 },
			{ "Demolitionist", 38 },
			{ "Bone Serpent", 39 },
			{ "Hornet", 42 },
			{ "Man Eater", 43 },
			{ "Undead Miner", 44 },
			{ "Tim", 45 },
			{ "Bunny", 46 },
			{ "Corrupt Bunny", 47 },
			{ "Harpy", 48 },
			{ "Cave Bat", 49 },
			{ "King Slime", 50 },
			{ "Jungle Bat", 51 },
			{ "Doctor Bones", 52 },
			{ "The Groom", 53 },
			{ "Clothier", 54 },
			{ "Goldfish", 55 },
			{ "Snatcher", 56 },
			{ "Corrupt Goldfish", 57 },
			{ "Piranha", 58 },
			{ "Lava Slime", 59 },
			{ "Hellbat", 60 },
			{ "Vulture", 61 },
			{ "Demon", 62 },
			{ "Blue Jellyfish", 63 },
			{ "Pink Jellyfish", 64 },
			{ "Shark", 65 },
			{ "Voodoo Demon", 66 },
			{ "Crab", 67 },
			{ "Dungeon Guardian", 68 },
			{ "Antlion", 69 },
			{ "Spike Ball", 70 },
			{ "Dungeon Slime", 71 },
			{ "Blazing Wheel", 72 },
			{ "Goblin Scout", 73 },
			{ "Bird", 74 },
			{ "Pixie", 75 },
			{ "Armored Skeleton", 77 },
			{ "Mummy", 78 },
			{ "Dark Mummy", 79 },
			{ "Light Mummy", 80 },
			{ "Corrupt Slime", 81 },
			{ "Wraith", 82 },
			{ "Cursed Hammer", 83 },
			{ "Enchanted Sword", 84 },
			{ "Mimic", 85 },
			{ "Unicorn", 86 },
			{ "Wyvern", 87 },
			{ "Giant Bat", 93 },
			{ "Corruptor", 94 },
			{ "Digger", 95 },
			{ "World Feeder", 98 },
			{ "Clinger", 101 },
			{ "Angler Fish", 102 },
			{ "Green Jellyfish", 103 },
			{ "Werewolf", 104 },
			{ "Bound Goblin", 105 },
			{ "Bound Wizard", 106 },
			{ "Goblin Tinkerer", 107 },
			{ "Wizard", 108 },
			{ "Clown", 109 },
			{ "Skeleton Archer", 110 },
			{ "Goblin Archer", 111 },
			{ "Vile Spit", 112 },
			{ "Wall of Flesh", 113 },
			{ "The Hungry", 115 },
			{ "Leech", 117 },
			{ "Chaos Elemental", 120 },
			{ "Slimer", 121 },
			{ "Gastropod", 122 },
			{ "Bound Mechanic", 123 },
			{ "Mechanic", 124 },
			{ "Retinazer", 125 },
			{ "Spazmatism", 126 },
			{ "Skeletron Prime", 127 },
			{ "Prime Cannon", 128 },
			{ "Prime Saw", 129 },
			{ "Prime Vice", 130 },
			{ "Prime Laser", 131 },
			{ "Wandering Eye", 133 },
			{ "The Destroyer", 134 },
			{ "Illuminant Bat", 137 },
			{ "Illuminant Slime", 138 },
			{ "Probe", 139 },
			{ "Possessed Armor", 140 },
			{ "Toxic Sludge", 141 },
			{ "Santa Claus", 142 },
			{ "Snowman Gangsta", 143 },
			{ "Mister Stabby", 144 },
			{ "Snow Balla", 145 },
			{ "Ice Slime", 147 },
			{ "Penguin", 148 },
			{ "Ice Bat", 150 },
			{ "Lava Bat", 151 },
			{ "Giant Flying Fox", 152 },
			{ "Giant Tortoise", 153 },
			{ "Ice Tortoise", 154 },
			{ "Wolf", 155 },
			{ "Red Devil", 156 },
			{ "Arapaima", 157 },
			{ "Vampire", 158 },
			{ "Truffle", 160 },
			{ "Zombie Eskimo", 161 },
			{ "Frankenstein", 162 },
			{ "Black Recluse", 163 },
			{ "Wall Creeper", 164 },
			{ "Swamp Thing", 166 },
			{ "Undead Viking", 167 },
			{ "Corrupt Penguin", 168 },
			{ "Ice Elemental", 169 },
			{ "Pigron", 170 },
			{ "Rune Wizard", 172 },
			{ "Crimera", 173 },
			{ "Herpling", 174 },
			{ "Angry Trapper", 175 },
			{ "Moss Hornet", 176 },
			{ "Derpling", 177 },
			{ "Steampunker", 178 },
			{ "Crimson Axe", 179 },
			{ "Face Monster", 181 },
			{ "Floaty Gross", 182 },
			{ "Crimslime", 183 },
			{ "Spiked Ice Slime", 184 },
			{ "Snow Flinx", 185 },
			{ "Lost Girl", 195 },
			{ "Nymph", 196 },
			{ "Armored Viking", 197 },
			{ "Lihzahrd", 198 },
			{ "Spiked Jungle Slime", 204 },
			{ "Moth", 205 },
			{ "Icy Merman", 206 },
			{ "Dye Trader", 207 },
			{ "Party Girl", 208 },
			{ "Cyborg", 209 },
			{ "Bee", 210 },
			{ "Pirate Deckhand", 212 },
			{ "Pirate Corsair", 213 },
			{ "Pirate Deadeye", 214 },
			{ "Pirate Crossbower", 215 },
			{ "Pirate Captain", 216 },
			{ "Cochineal Beetle", 217 },
			{ "Cyan Beetle", 218 },
			{ "Lac Beetle", 219 },
			{ "Sea Snail", 220 },
			{ "Squid", 221 },
			{ "Queen Bee", 222 },
			{ "Raincoat Zombie", 223 },
			{ "Flying Fish", 224 },
			{ "Umbrella Slime", 225 },
			{ "Flying Snake", 226 },
			{ "Painter", 227 },
			{ "Witch Doctor", 228 },
			{ "Pirate", 229 },
			{ "Jungle Creeper", 236 },
			{ "Blood Crawler", 239 },
			{ "Blood Feeder", 241 },
			{ "Blood Jelly", 242 },
			{ "Ice Golem", 243 },
			{ "Rainbow Slime", 244 },
			{ "Golem", 245 },
			{ "Golem Head", 246 },
			{ "Golem Fist", 247 },
			{ "Angry Nimbus", 250 },
			{ "Eyezor", 251 },
			{ "Parrot", 252 },
			{ "Reaper", 253 },
			{ "Spore Zombie", 254 },
			{ "Fungo Fish", 256 },
			{ "Anomura Fungus", 257 },
			{ "Mushi Ladybug", 258 },
			{ "Fungi Bulb", 259 },
			{ "Giant Fungi Bulb", 260 },
			{ "Fungi Spore", 261 },
			{ "Plantera", 262 },
			{ "Plantera's Hook", 263 },
			{ "Plantera's Tentacle", 264 },
			{ "Spore", 265 },
			{ "Brain of Cthulhu", 266 },
			{ "Creeper", 267 },
			{ "Ichor Sticker", 268 },
			{ "Rusty Armored Bones", 269 },
			{ "Blue Armored Bones", 273 },
			{ "Hell Armored Bones", 277 },
			{ "Ragged Caster", 281 },
			{ "Necromancer", 283 },
			{ "Diabolist", 285 },
			{ "Bone Lee", 287 },
			{ "Dungeon Spirit", 288 },
			{ "Giant Cursed Skull", 289 },
			{ "Paladin", 290 },
			{ "Skeleton Sniper", 291 },
			{ "Tactical Skeleton", 292 },
			{ "Skeleton Commando", 293 },
			{ "Blue Jay", 297 },
			{ "Cardinal", 298 },
			{ "Squirrel", 299 },
			{ "Mouse", 300 },
			{ "Raven", 301 },
			{ "Slime", 302 },
			{ "Hoppin' Jack", 304 },
			{ "Scarecrow", 305 },
			{ "Headless Horseman", 315 },
			{ "Ghost", 316 },
			{ "Mourning Wood", 325 },
			{ "Splinterling", 326 },
			{ "Pumpking", 327 },
			{ "Hellhound", 329 },
			{ "Poltergeist", 330 },
			{ "Zombie Elf", 338 },
			{ "Present Mimic", 341 },
			{ "Gingerbread Man", 342 },
			{ "Yeti", 343 },
			{ "Everscream", 344 },
			{ "Ice Queen", 345 },
			{ "Santa", 346 },
			{ "Elf Copter", 347 },
			{ "Nutcracker", 348 },
			{ "Elf Archer", 350 },
			{ "Krampus", 351 },
			{ "Flocko", 352 },
			{ "Stylist", 353 },
			{ "Webbed Stylist", 354 },
			{ "Firefly", 355 },
			{ "Butterfly", 356 },
			{ "Worm", 357 },
			{ "Lightning Bug", 358 },
			{ "Snail", 359 },
			{ "Glowing Snail", 360 },
			{ "Frog", 361 },
			{ "Duck", 362 },
			{ "Scorpion", 366 },
			{ "Traveling Merchant", 368 },
			{ "Angler", 369 },
			{ "Duke Fishron", 370 },
			{ "Detonating Bubble", 371 },
			{ "Sharkron", 372 },
			{ "Truffle Worm", 374 },
			{ "Sleeping Angler", 376 },
			{ "Grasshopper", 377 },
			{ "Chattering Teeth Bomb", 378 },
			{ "Blue Cultist Archer", 379 },
			{ "White Cultist Archer", 380 },
			{ "Brain Scrambler", 381 },
			{ "Ray Gunner", 382 },
			{ "Martian Officer", 383 },
			{ "Bubble Shield", 384 },
			{ "Gray Grunt", 385 },
			{ "Martian Engineer", 386 },
			{ "Tesla Turret", 387 },
			{ "Martian Drone", 388 },
			{ "Gigazapper", 389 },
			{ "Scutlix Gunner", 390 },
			{ "Scutlix", 391 },
			{ "Martian Saucer", 392 },
			{ "Martian Saucer Turret", 393 },
			{ "Martian Saucer Cannon", 394 },
			{ "Moon Lord", 396 },
			{ "Moon Lord's Hand", 397 },
			{ "Moon Lord's Core", 398 },
			{ "Martian Probe", 399 },
			{ "Milkyway Weaver", 402 },
			{ "Star Cell", 405 },
			{ "Flow Invader", 407 },
			{ "Twinkle Popper", 409 },
			{ "Twinkle", 410 },
			{ "Stargazer", 411 },
			{ "Crawltipede", 412 },
			{ "Drakomire", 415 },
			{ "Drakomire Rider", 416 },
			{ "Sroller", 417 },
			{ "Corite", 418 },
			{ "Selenian", 419 },
			{ "Nebula Floater", 420 },
			{ "Brain Suckler", 421 },
			{ "Vortex Pillar", 422 },
			{ "Evolution Beast", 423 },
			{ "Predictor", 424 },
			{ "Storm Diver", 425 },
			{ "Alien Queen", 426 },
			{ "Alien Hornet", 427 },
			{ "Alien Larva", 428 },
			{ "Vortexian", 429 },
			{ "Mysterious Tablet", 437 },
			{ "Lunatic Devote", 438 },
			{ "Lunatic Cultist", 439 },
			{ "Tax Collector", 441 },
			{ "Gold Bird", 442 },
			{ "Gold Bunny", 443 },
			{ "Gold Butterfly", 444 },
			{ "Gold Frog", 445 },
			{ "Gold Grasshopper", 446 },
			{ "Gold Mouse", 447 },
			{ "Gold Worm", 448 },
			{ "Phantasm Dragon", 454 },
			{ "Butcher", 460 },
			{ "Creature from the Deep", 461 },
			{ "Fritz", 462 },
			{ "Nailhead", 463 },
			{ "Crimtane Bunny", 464 },
			{ "Crimtane Goldfish", 465 },
			{ "Psycho", 466 },
			{ "Deadly Sphere", 467 },
			{ "Dr. Man Fly", 468 },
			{ "The Possessed", 469 },
			{ "Vicious Penguin", 470 },
			{ "Goblin Summoner", 471 },
			{ "Shadowflame Apparation", 472 },
			{ "Corrupt Mimic", 473 },
			{ "Crimson Mimic", 474 },
			{ "Hallowed Mimic", 475 },
			{ "Jungle Mimic", 476 },
			{ "Mothron", 477 },
			{ "Mothron Egg", 478 },
			{ "Baby Mothron", 479 },
			{ "Medusa", 480 },
			{ "Hoplite", 481 },
			{ "Granite Golem", 482 },
			{ "Granite Elemental", 483 },
			{ "Enchanted Nightcrawler", 484 },
			{ "Grubby", 485 },
			{ "Sluggy", 486 },
			{ "Buggy", 487 },
			{ "Target Dummy", 488 },
			{ "Blood Zombie", 489 },
			{ "Drippler", 490 },
			{ "Stardust Pillar", 493 },
			{ "Crawdad", 494 },
			{ "Giant Shelly", 496 },
			{ "Salamander", 498 },
			{ "Nebula Pillar", 507 },
			{ "Antlion Charger", 508 },
			{ "Antlion Swarmer", 509 },
			{ "Dune Splicer", 510 },
			{ "Tomb Crawler", 513 },
			{ "Solar Flare", 516 },
			{ "Solar Pillar", 517 },
			{ "Drakanian", 518 },
			{ "Solar Fragment", 519 },
			{ "Martian Walker", 520 },
			{ "Ancient Vision", 521 },
			{ "Ancient Light", 522 },
			{ "Ancient Doom", 523 },
			{ "Ghoul", 524 },
			{ "Vile Ghoul", 525 },
			{ "Tainted Ghoul", 526 },
			{ "Dreamer Ghoul", 527 },
			{ "Lamia", 528 },
			{ "Sand Poacher", 530 },
			{ "Basilisk", 532 },
			{ "Desert Spirit", 533 },
			{ "Tortured Soul", 534 },
			{ "Spiked Slime", 535 },
			{ "The Bride", 536 },
			{ "Sand Slime", 537 },
			{ "Red Squirrel", 538 },
			{ "Gold Squirrel", 539 },
			{ "Sand Elemental", 541 },
			{ "Sand Shark", 542 },
			{ "Bone Biter", 543 },
			{ "Flesh Reaver", 544 },
			{ "Crystal Thresher", 545 },
			{ "Angry Tumbler", 546 },
			{ "???", 547 },
			{ "Eternia Crystal", 548 },
			{ "Mysterious Portal", 549 },
			{ "Tavernkeep", 550 },
			{ "Betsy", 551 },
			{ "Etherian Goblin", 552 },
			{ "Etherian Goblin Bomber", 555 },
			{ "Etherian Wyvern", 558 },
			{ "Etherian Javelin Thrower", 561 },
			{ "Dark Mage", 564 },
			{ "Old One's Skeleton", 566 },
			{ "Wither Beast", 568 },
			{ "Drakin", 570 },
			{ "Kobold", 572 },
			{ "Kobold Glider", 574 },
			{ "Ogre", 576 },
			{ "Etherian Lightning Bug", 578 }
		};

		// Token: 0x040043F9 RID: 17401
		public const short NegativeIDCount = -66;

		// Token: 0x040043FA RID: 17402
		public const short BigHornetStingy = -65;

		// Token: 0x040043FB RID: 17403
		public const short LittleHornetStingy = -64;

		// Token: 0x040043FC RID: 17404
		public const short BigHornetSpikey = -63;

		// Token: 0x040043FD RID: 17405
		public const short LittleHornetSpikey = -62;

		// Token: 0x040043FE RID: 17406
		public const short BigHornetLeafy = -61;

		// Token: 0x040043FF RID: 17407
		public const short LittleHornetLeafy = -60;

		// Token: 0x04004400 RID: 17408
		public const short BigHornetHoney = -59;

		// Token: 0x04004401 RID: 17409
		public const short LittleHornetHoney = -58;

		// Token: 0x04004402 RID: 17410
		public const short BigHornetFatty = -57;

		// Token: 0x04004403 RID: 17411
		public const short LittleHornetFatty = -56;

		// Token: 0x04004404 RID: 17412
		public const short BigRainZombie = -55;

		// Token: 0x04004405 RID: 17413
		public const short SmallRainZombie = -54;

		// Token: 0x04004406 RID: 17414
		public const short BigPantlessSkeleton = -53;

		// Token: 0x04004407 RID: 17415
		public const short SmallPantlessSkeleton = -52;

		// Token: 0x04004408 RID: 17416
		public const short BigMisassembledSkeleton = -51;

		// Token: 0x04004409 RID: 17417
		public const short SmallMisassembledSkeleton = -50;

		// Token: 0x0400440A RID: 17418
		public const short BigHeadacheSkeleton = -49;

		// Token: 0x0400440B RID: 17419
		public const short SmallHeadacheSkeleton = -48;

		// Token: 0x0400440C RID: 17420
		public const short BigSkeleton = -47;

		// Token: 0x0400440D RID: 17421
		public const short SmallSkeleton = -46;

		// Token: 0x0400440E RID: 17422
		public const short BigFemaleZombie = -45;

		// Token: 0x0400440F RID: 17423
		public const short SmallFemaleZombie = -44;

		// Token: 0x04004410 RID: 17424
		public const short DemonEye2 = -43;

		// Token: 0x04004411 RID: 17425
		public const short PurpleEye2 = -42;

		// Token: 0x04004412 RID: 17426
		public const short GreenEye2 = -41;

		// Token: 0x04004413 RID: 17427
		public const short DialatedEye2 = -40;

		// Token: 0x04004414 RID: 17428
		public const short SleepyEye2 = -39;

		// Token: 0x04004415 RID: 17429
		public const short CataractEye2 = -38;

		// Token: 0x04004416 RID: 17430
		public const short BigTwiggyZombie = -37;

		// Token: 0x04004417 RID: 17431
		public const short SmallTwiggyZombie = -36;

		// Token: 0x04004418 RID: 17432
		public const short BigSwampZombie = -35;

		// Token: 0x04004419 RID: 17433
		public const short SmallSwampZombie = -34;

		// Token: 0x0400441A RID: 17434
		public const short BigSlimedZombie = -33;

		// Token: 0x0400441B RID: 17435
		public const short SmallSlimedZombie = -32;

		// Token: 0x0400441C RID: 17436
		public const short BigPincushionZombie = -31;

		// Token: 0x0400441D RID: 17437
		public const short SmallPincushionZombie = -30;

		// Token: 0x0400441E RID: 17438
		public const short BigBaldZombie = -29;

		// Token: 0x0400441F RID: 17439
		public const short SmallBaldZombie = -28;

		// Token: 0x04004420 RID: 17440
		public const short BigZombie = -27;

		// Token: 0x04004421 RID: 17441
		public const short SmallZombie = -26;

		// Token: 0x04004422 RID: 17442
		public const short BigCrimslime = -25;

		// Token: 0x04004423 RID: 17443
		public const short LittleCrimslime = -24;

		// Token: 0x04004424 RID: 17444
		public const short BigCrimera = -23;

		// Token: 0x04004425 RID: 17445
		public const short LittleCrimera = -22;

		// Token: 0x04004426 RID: 17446
		public const short GiantMossHornet = -21;

		// Token: 0x04004427 RID: 17447
		public const short BigMossHornet = -20;

		// Token: 0x04004428 RID: 17448
		public const short LittleMossHornet = -19;

		// Token: 0x04004429 RID: 17449
		public const short TinyMossHornet = -18;

		// Token: 0x0400442A RID: 17450
		public const short BigStinger = -17;

		// Token: 0x0400442B RID: 17451
		public const short LittleStinger = -16;

		// Token: 0x0400442C RID: 17452
		public const short HeavySkeleton = -15;

		// Token: 0x0400442D RID: 17453
		public const short BigBoned = -14;

		// Token: 0x0400442E RID: 17454
		public const short ShortBones = -13;

		// Token: 0x0400442F RID: 17455
		public const short BigEater = -12;

		// Token: 0x04004430 RID: 17456
		public const short LittleEater = -11;

		// Token: 0x04004431 RID: 17457
		public const short JungleSlime = -10;

		// Token: 0x04004432 RID: 17458
		public const short YellowSlime = -9;

		// Token: 0x04004433 RID: 17459
		public const short RedSlime = -8;

		// Token: 0x04004434 RID: 17460
		public const short PurpleSlime = -7;

		// Token: 0x04004435 RID: 17461
		public const short BlackSlime = -6;

		// Token: 0x04004436 RID: 17462
		public const short BabySlime = -5;

		// Token: 0x04004437 RID: 17463
		public const short Pinky = -4;

		// Token: 0x04004438 RID: 17464
		public const short GreenSlime = -3;

		// Token: 0x04004439 RID: 17465
		public const short Slimer2 = -2;

		// Token: 0x0400443A RID: 17466
		public const short Slimeling = -1;

		// Token: 0x0400443B RID: 17467
		public const short None = 0;

		// Token: 0x0400443C RID: 17468
		public const short BlueSlime = 1;

		// Token: 0x0400443D RID: 17469
		public const short DemonEye = 2;

		// Token: 0x0400443E RID: 17470
		public const short Zombie = 3;

		// Token: 0x0400443F RID: 17471
		public const short EyeofCthulhu = 4;

		// Token: 0x04004440 RID: 17472
		public const short ServantofCthulhu = 5;

		// Token: 0x04004441 RID: 17473
		public const short EaterofSouls = 6;

		// Token: 0x04004442 RID: 17474
		public const short DevourerHead = 7;

		// Token: 0x04004443 RID: 17475
		public const short DevourerBody = 8;

		// Token: 0x04004444 RID: 17476
		public const short DevourerTail = 9;

		// Token: 0x04004445 RID: 17477
		public const short GiantWormHead = 10;

		// Token: 0x04004446 RID: 17478
		public const short GiantWormBody = 11;

		// Token: 0x04004447 RID: 17479
		public const short GiantWormTail = 12;

		// Token: 0x04004448 RID: 17480
		public const short EaterofWorldsHead = 13;

		// Token: 0x04004449 RID: 17481
		public const short EaterofWorldsBody = 14;

		// Token: 0x0400444A RID: 17482
		public const short EaterofWorldsTail = 15;

		// Token: 0x0400444B RID: 17483
		public const short MotherSlime = 16;

		// Token: 0x0400444C RID: 17484
		public const short Merchant = 17;

		// Token: 0x0400444D RID: 17485
		public const short Nurse = 18;

		// Token: 0x0400444E RID: 17486
		public const short ArmsDealer = 19;

		// Token: 0x0400444F RID: 17487
		public const short Dryad = 20;

		// Token: 0x04004450 RID: 17488
		public const short Skeleton = 21;

		// Token: 0x04004451 RID: 17489
		public const short Guide = 22;

		// Token: 0x04004452 RID: 17490
		public const short MeteorHead = 23;

		// Token: 0x04004453 RID: 17491
		public const short FireImp = 24;

		// Token: 0x04004454 RID: 17492
		public const short BurningSphere = 25;

		// Token: 0x04004455 RID: 17493
		public const short GoblinPeon = 26;

		// Token: 0x04004456 RID: 17494
		public const short GoblinThief = 27;

		// Token: 0x04004457 RID: 17495
		public const short GoblinWarrior = 28;

		// Token: 0x04004458 RID: 17496
		public const short GoblinSorcerer = 29;

		// Token: 0x04004459 RID: 17497
		public const short ChaosBall = 30;

		// Token: 0x0400445A RID: 17498
		public const short AngryBones = 31;

		// Token: 0x0400445B RID: 17499
		public const short DarkCaster = 32;

		// Token: 0x0400445C RID: 17500
		public const short WaterSphere = 33;

		// Token: 0x0400445D RID: 17501
		public const short CursedSkull = 34;

		// Token: 0x0400445E RID: 17502
		public const short SkeletronHead = 35;

		// Token: 0x0400445F RID: 17503
		public const short SkeletronHand = 36;

		// Token: 0x04004460 RID: 17504
		public const short OldMan = 37;

		// Token: 0x04004461 RID: 17505
		public const short Demolitionist = 38;

		// Token: 0x04004462 RID: 17506
		public const short BoneSerpentHead = 39;

		// Token: 0x04004463 RID: 17507
		public const short BoneSerpentBody = 40;

		// Token: 0x04004464 RID: 17508
		public const short BoneSerpentTail = 41;

		// Token: 0x04004465 RID: 17509
		public const short Hornet = 42;

		// Token: 0x04004466 RID: 17510
		public const short ManEater = 43;

		// Token: 0x04004467 RID: 17511
		public const short UndeadMiner = 44;

		// Token: 0x04004468 RID: 17512
		public const short Tim = 45;

		// Token: 0x04004469 RID: 17513
		public const short Bunny = 46;

		// Token: 0x0400446A RID: 17514
		public const short CorruptBunny = 47;

		// Token: 0x0400446B RID: 17515
		public const short Harpy = 48;

		// Token: 0x0400446C RID: 17516
		public const short CaveBat = 49;

		// Token: 0x0400446D RID: 17517
		public const short KingSlime = 50;

		// Token: 0x0400446E RID: 17518
		public const short JungleBat = 51;

		// Token: 0x0400446F RID: 17519
		public const short DoctorBones = 52;

		// Token: 0x04004470 RID: 17520
		public const short TheGroom = 53;

		// Token: 0x04004471 RID: 17521
		public const short Clothier = 54;

		// Token: 0x04004472 RID: 17522
		public const short Goldfish = 55;

		// Token: 0x04004473 RID: 17523
		public const short Snatcher = 56;

		// Token: 0x04004474 RID: 17524
		public const short CorruptGoldfish = 57;

		// Token: 0x04004475 RID: 17525
		public const short Piranha = 58;

		// Token: 0x04004476 RID: 17526
		public const short LavaSlime = 59;

		// Token: 0x04004477 RID: 17527
		public const short Hellbat = 60;

		// Token: 0x04004478 RID: 17528
		public const short Vulture = 61;

		// Token: 0x04004479 RID: 17529
		public const short Demon = 62;

		// Token: 0x0400447A RID: 17530
		public const short BlueJellyfish = 63;

		// Token: 0x0400447B RID: 17531
		public const short PinkJellyfish = 64;

		// Token: 0x0400447C RID: 17532
		public const short Shark = 65;

		// Token: 0x0400447D RID: 17533
		public const short VoodooDemon = 66;

		// Token: 0x0400447E RID: 17534
		public const short Crab = 67;

		// Token: 0x0400447F RID: 17535
		public const short DungeonGuardian = 68;

		// Token: 0x04004480 RID: 17536
		public const short Antlion = 69;

		// Token: 0x04004481 RID: 17537
		public const short SpikeBall = 70;

		// Token: 0x04004482 RID: 17538
		public const short DungeonSlime = 71;

		// Token: 0x04004483 RID: 17539
		public const short BlazingWheel = 72;

		// Token: 0x04004484 RID: 17540
		public const short GoblinScout = 73;

		// Token: 0x04004485 RID: 17541
		public const short Bird = 74;

		// Token: 0x04004486 RID: 17542
		public const short Pixie = 75;

		// Token: 0x04004487 RID: 17543
		public const short None2 = 76;

		// Token: 0x04004488 RID: 17544
		public const short ArmoredSkeleton = 77;

		// Token: 0x04004489 RID: 17545
		public const short Mummy = 78;

		// Token: 0x0400448A RID: 17546
		public const short DarkMummy = 79;

		// Token: 0x0400448B RID: 17547
		public const short LightMummy = 80;

		// Token: 0x0400448C RID: 17548
		public const short CorruptSlime = 81;

		// Token: 0x0400448D RID: 17549
		public const short Wraith = 82;

		// Token: 0x0400448E RID: 17550
		public const short CursedHammer = 83;

		// Token: 0x0400448F RID: 17551
		public const short EnchantedSword = 84;

		// Token: 0x04004490 RID: 17552
		public const short Mimic = 85;

		// Token: 0x04004491 RID: 17553
		public const short Unicorn = 86;

		// Token: 0x04004492 RID: 17554
		public const short WyvernHead = 87;

		// Token: 0x04004493 RID: 17555
		public const short WyvernLegs = 88;

		// Token: 0x04004494 RID: 17556
		public const short WyvernBody = 89;

		// Token: 0x04004495 RID: 17557
		public const short WyvernBody2 = 90;

		// Token: 0x04004496 RID: 17558
		public const short WyvernBody3 = 91;

		// Token: 0x04004497 RID: 17559
		public const short WyvernTail = 92;

		// Token: 0x04004498 RID: 17560
		public const short GiantBat = 93;

		// Token: 0x04004499 RID: 17561
		public const short Corruptor = 94;

		// Token: 0x0400449A RID: 17562
		public const short DiggerHead = 95;

		// Token: 0x0400449B RID: 17563
		public const short DiggerBody = 96;

		// Token: 0x0400449C RID: 17564
		public const short DiggerTail = 97;

		// Token: 0x0400449D RID: 17565
		public const short SeekerHead = 98;

		// Token: 0x0400449E RID: 17566
		public const short SeekerBody = 99;

		// Token: 0x0400449F RID: 17567
		public const short SeekerTail = 100;

		// Token: 0x040044A0 RID: 17568
		public const short Clinger = 101;

		// Token: 0x040044A1 RID: 17569
		public const short AnglerFish = 102;

		// Token: 0x040044A2 RID: 17570
		public const short GreenJellyfish = 103;

		// Token: 0x040044A3 RID: 17571
		public const short Werewolf = 104;

		// Token: 0x040044A4 RID: 17572
		public const short BoundGoblin = 105;

		// Token: 0x040044A5 RID: 17573
		public const short BoundWizard = 106;

		// Token: 0x040044A6 RID: 17574
		public const short GoblinTinkerer = 107;

		// Token: 0x040044A7 RID: 17575
		public const short Wizard = 108;

		// Token: 0x040044A8 RID: 17576
		public const short Clown = 109;

		// Token: 0x040044A9 RID: 17577
		public const short SkeletonArcher = 110;

		// Token: 0x040044AA RID: 17578
		public const short GoblinArcher = 111;

		// Token: 0x040044AB RID: 17579
		public const short VileSpit = 112;

		// Token: 0x040044AC RID: 17580
		public const short WallofFlesh = 113;

		// Token: 0x040044AD RID: 17581
		public const short WallofFleshEye = 114;

		// Token: 0x040044AE RID: 17582
		public const short TheHungry = 115;

		// Token: 0x040044AF RID: 17583
		public const short TheHungryII = 116;

		// Token: 0x040044B0 RID: 17584
		public const short LeechHead = 117;

		// Token: 0x040044B1 RID: 17585
		public const short LeechBody = 118;

		// Token: 0x040044B2 RID: 17586
		public const short LeechTail = 119;

		// Token: 0x040044B3 RID: 17587
		public const short ChaosElemental = 120;

		// Token: 0x040044B4 RID: 17588
		public const short Slimer = 121;

		// Token: 0x040044B5 RID: 17589
		public const short Gastropod = 122;

		// Token: 0x040044B6 RID: 17590
		public const short BoundMechanic = 123;

		// Token: 0x040044B7 RID: 17591
		public const short Mechanic = 124;

		// Token: 0x040044B8 RID: 17592
		public const short Retinazer = 125;

		// Token: 0x040044B9 RID: 17593
		public const short Spazmatism = 126;

		// Token: 0x040044BA RID: 17594
		public const short SkeletronPrime = 127;

		// Token: 0x040044BB RID: 17595
		public const short PrimeCannon = 128;

		// Token: 0x040044BC RID: 17596
		public const short PrimeSaw = 129;

		// Token: 0x040044BD RID: 17597
		public const short PrimeVice = 130;

		// Token: 0x040044BE RID: 17598
		public const short PrimeLaser = 131;

		// Token: 0x040044BF RID: 17599
		public const short BaldZombie = 132;

		// Token: 0x040044C0 RID: 17600
		public const short WanderingEye = 133;

		// Token: 0x040044C1 RID: 17601
		public const short TheDestroyer = 134;

		// Token: 0x040044C2 RID: 17602
		public const short TheDestroyerBody = 135;

		// Token: 0x040044C3 RID: 17603
		public const short TheDestroyerTail = 136;

		// Token: 0x040044C4 RID: 17604
		public const short IlluminantBat = 137;

		// Token: 0x040044C5 RID: 17605
		public const short IlluminantSlime = 138;

		// Token: 0x040044C6 RID: 17606
		public const short Probe = 139;

		// Token: 0x040044C7 RID: 17607
		public const short PossessedArmor = 140;

		// Token: 0x040044C8 RID: 17608
		public const short ToxicSludge = 141;

		// Token: 0x040044C9 RID: 17609
		public const short SantaClaus = 142;

		// Token: 0x040044CA RID: 17610
		public const short SnowmanGangsta = 143;

		// Token: 0x040044CB RID: 17611
		public const short MisterStabby = 144;

		// Token: 0x040044CC RID: 17612
		public const short SnowBalla = 145;

		// Token: 0x040044CD RID: 17613
		public const short None3 = 146;

		// Token: 0x040044CE RID: 17614
		public const short IceSlime = 147;

		// Token: 0x040044CF RID: 17615
		public const short Penguin = 148;

		// Token: 0x040044D0 RID: 17616
		public const short PenguinBlack = 149;

		// Token: 0x040044D1 RID: 17617
		public const short IceBat = 150;

		// Token: 0x040044D2 RID: 17618
		public const short Lavabat = 151;

		// Token: 0x040044D3 RID: 17619
		public const short GiantFlyingFox = 152;

		// Token: 0x040044D4 RID: 17620
		public const short GiantTortoise = 153;

		// Token: 0x040044D5 RID: 17621
		public const short IceTortoise = 154;

		// Token: 0x040044D6 RID: 17622
		public const short Wolf = 155;

		// Token: 0x040044D7 RID: 17623
		public const short RedDevil = 156;

		// Token: 0x040044D8 RID: 17624
		public const short Arapaima = 157;

		// Token: 0x040044D9 RID: 17625
		public const short VampireBat = 158;

		// Token: 0x040044DA RID: 17626
		public const short Vampire = 159;

		// Token: 0x040044DB RID: 17627
		public const short Truffle = 160;

		// Token: 0x040044DC RID: 17628
		public const short ZombieEskimo = 161;

		// Token: 0x040044DD RID: 17629
		public const short Frankenstein = 162;

		// Token: 0x040044DE RID: 17630
		public const short BlackRecluse = 163;

		// Token: 0x040044DF RID: 17631
		public const short WallCreeper = 164;

		// Token: 0x040044E0 RID: 17632
		public const short WallCreeperWall = 165;

		// Token: 0x040044E1 RID: 17633
		public const short SwampThing = 166;

		// Token: 0x040044E2 RID: 17634
		public const short UndeadViking = 167;

		// Token: 0x040044E3 RID: 17635
		public const short CorruptPenguin = 168;

		// Token: 0x040044E4 RID: 17636
		public const short IceElemental = 169;

		// Token: 0x040044E5 RID: 17637
		public const short PigronCorruption = 170;

		// Token: 0x040044E6 RID: 17638
		public const short PigronHallow = 171;

		// Token: 0x040044E7 RID: 17639
		public const short RuneWizard = 172;

		// Token: 0x040044E8 RID: 17640
		public const short Crimera = 173;

		// Token: 0x040044E9 RID: 17641
		public const short Herpling = 174;

		// Token: 0x040044EA RID: 17642
		public const short AngryTrapper = 175;

		// Token: 0x040044EB RID: 17643
		public const short MossHornet = 176;

		// Token: 0x040044EC RID: 17644
		public const short Derpling = 177;

		// Token: 0x040044ED RID: 17645
		public const short Steampunker = 178;

		// Token: 0x040044EE RID: 17646
		public const short CrimsonAxe = 179;

		// Token: 0x040044EF RID: 17647
		public const short PigronCrimson = 180;

		// Token: 0x040044F0 RID: 17648
		public const short FaceMonster = 181;

		// Token: 0x040044F1 RID: 17649
		public const short FloatyGross = 182;

		// Token: 0x040044F2 RID: 17650
		public const short Crimslime = 183;

		// Token: 0x040044F3 RID: 17651
		public const short SpikedIceSlime = 184;

		// Token: 0x040044F4 RID: 17652
		public const short SnowFlinx = 185;

		// Token: 0x040044F5 RID: 17653
		public const short PincushionZombie = 186;

		// Token: 0x040044F6 RID: 17654
		public const short SlimedZombie = 187;

		// Token: 0x040044F7 RID: 17655
		public const short SwampZombie = 188;

		// Token: 0x040044F8 RID: 17656
		public const short TwiggyZombie = 189;

		// Token: 0x040044F9 RID: 17657
		public const short CataractEye = 190;

		// Token: 0x040044FA RID: 17658
		public const short SleepyEye = 191;

		// Token: 0x040044FB RID: 17659
		public const short DialatedEye = 192;

		// Token: 0x040044FC RID: 17660
		public const short GreenEye = 193;

		// Token: 0x040044FD RID: 17661
		public const short PurpleEye = 194;

		// Token: 0x040044FE RID: 17662
		public const short LostGirl = 195;

		// Token: 0x040044FF RID: 17663
		public const short Nymph = 196;

		// Token: 0x04004500 RID: 17664
		public const short ArmoredViking = 197;

		// Token: 0x04004501 RID: 17665
		public const short Lihzahrd = 198;

		// Token: 0x04004502 RID: 17666
		public const short LihzahrdCrawler = 199;

		// Token: 0x04004503 RID: 17667
		public const short FemaleZombie = 200;

		// Token: 0x04004504 RID: 17668
		public const short HeadacheSkeleton = 201;

		// Token: 0x04004505 RID: 17669
		public const short MisassembledSkeleton = 202;

		// Token: 0x04004506 RID: 17670
		public const short PantlessSkeleton = 203;

		// Token: 0x04004507 RID: 17671
		public const short SpikedJungleSlime = 204;

		// Token: 0x04004508 RID: 17672
		public const short Moth = 205;

		// Token: 0x04004509 RID: 17673
		public const short IcyMerman = 206;

		// Token: 0x0400450A RID: 17674
		public const short DyeTrader = 207;

		// Token: 0x0400450B RID: 17675
		public const short PartyGirl = 208;

		// Token: 0x0400450C RID: 17676
		public const short Cyborg = 209;

		// Token: 0x0400450D RID: 17677
		public const short Bee = 210;

		// Token: 0x0400450E RID: 17678
		public const short BeeSmall = 211;

		// Token: 0x0400450F RID: 17679
		public const short PirateDeckhand = 212;

		// Token: 0x04004510 RID: 17680
		public const short PirateCorsair = 213;

		// Token: 0x04004511 RID: 17681
		public const short PirateDeadeye = 214;

		// Token: 0x04004512 RID: 17682
		public const short PirateCrossbower = 215;

		// Token: 0x04004513 RID: 17683
		public const short PirateCaptain = 216;

		// Token: 0x04004514 RID: 17684
		public const short CochinealBeetle = 217;

		// Token: 0x04004515 RID: 17685
		public const short CyanBeetle = 218;

		// Token: 0x04004516 RID: 17686
		public const short LacBeetle = 219;

		// Token: 0x04004517 RID: 17687
		public const short SeaSnail = 220;

		// Token: 0x04004518 RID: 17688
		public const short Squid = 221;

		// Token: 0x04004519 RID: 17689
		public const short QueenBee = 222;

		// Token: 0x0400451A RID: 17690
		public const short ZombieRaincoat = 223;

		// Token: 0x0400451B RID: 17691
		public const short FlyingFish = 224;

		// Token: 0x0400451C RID: 17692
		public const short UmbrellaSlime = 225;

		// Token: 0x0400451D RID: 17693
		public const short FlyingSnake = 226;

		// Token: 0x0400451E RID: 17694
		public const short Painter = 227;

		// Token: 0x0400451F RID: 17695
		public const short WitchDoctor = 228;

		// Token: 0x04004520 RID: 17696
		public const short Pirate = 229;

		// Token: 0x04004521 RID: 17697
		public const short GoldfishWalker = 230;

		// Token: 0x04004522 RID: 17698
		public const short HornetFatty = 231;

		// Token: 0x04004523 RID: 17699
		public const short HornetHoney = 232;

		// Token: 0x04004524 RID: 17700
		public const short HornetLeafy = 233;

		// Token: 0x04004525 RID: 17701
		public const short HornetSpikey = 234;

		// Token: 0x04004526 RID: 17702
		public const short HornetStingy = 235;

		// Token: 0x04004527 RID: 17703
		public const short JungleCreeper = 236;

		// Token: 0x04004528 RID: 17704
		public const short JungleCreeperWall = 237;

		// Token: 0x04004529 RID: 17705
		public const short BlackRecluseWall = 238;

		// Token: 0x0400452A RID: 17706
		public const short BloodCrawler = 239;

		// Token: 0x0400452B RID: 17707
		public const short BloodCrawlerWall = 240;

		// Token: 0x0400452C RID: 17708
		public const short BloodFeeder = 241;

		// Token: 0x0400452D RID: 17709
		public const short BloodJelly = 242;

		// Token: 0x0400452E RID: 17710
		public const short IceGolem = 243;

		// Token: 0x0400452F RID: 17711
		public const short RainbowSlime = 244;

		// Token: 0x04004530 RID: 17712
		public const short Golem = 245;

		// Token: 0x04004531 RID: 17713
		public const short GolemHead = 246;

		// Token: 0x04004532 RID: 17714
		public const short GolemFistLeft = 247;

		// Token: 0x04004533 RID: 17715
		public const short GolemFistRight = 248;

		// Token: 0x04004534 RID: 17716
		public const short GolemHeadFree = 249;

		// Token: 0x04004535 RID: 17717
		public const short AngryNimbus = 250;

		// Token: 0x04004536 RID: 17718
		public const short Eyezor = 251;

		// Token: 0x04004537 RID: 17719
		public const short Parrot = 252;

		// Token: 0x04004538 RID: 17720
		public const short Reaper = 253;

		// Token: 0x04004539 RID: 17721
		public const short ZombieMushroom = 254;

		// Token: 0x0400453A RID: 17722
		public const short ZombieMushroomHat = 255;

		// Token: 0x0400453B RID: 17723
		public const short FungoFish = 256;

		// Token: 0x0400453C RID: 17724
		public const short AnomuraFungus = 257;

		// Token: 0x0400453D RID: 17725
		public const short MushiLadybug = 258;

		// Token: 0x0400453E RID: 17726
		public const short FungiBulb = 259;

		// Token: 0x0400453F RID: 17727
		public const short GiantFungiBulb = 260;

		// Token: 0x04004540 RID: 17728
		public const short FungiSpore = 261;

		// Token: 0x04004541 RID: 17729
		public const short Plantera = 262;

		// Token: 0x04004542 RID: 17730
		public const short PlanterasHook = 263;

		// Token: 0x04004543 RID: 17731
		public const short PlanterasTentacle = 264;

		// Token: 0x04004544 RID: 17732
		public const short Spore = 265;

		// Token: 0x04004545 RID: 17733
		public const short BrainofCthulhu = 266;

		// Token: 0x04004546 RID: 17734
		public const short Creeper = 267;

		// Token: 0x04004547 RID: 17735
		public const short IchorSticker = 268;

		// Token: 0x04004548 RID: 17736
		public const short RustyArmoredBonesAxe = 269;

		// Token: 0x04004549 RID: 17737
		public const short RustyArmoredBonesFlail = 270;

		// Token: 0x0400454A RID: 17738
		public const short RustyArmoredBonesSword = 271;

		// Token: 0x0400454B RID: 17739
		public const short RustyArmoredBonesSwordNoArmor = 272;

		// Token: 0x0400454C RID: 17740
		public const short BlueArmoredBones = 273;

		// Token: 0x0400454D RID: 17741
		public const short BlueArmoredBonesMace = 274;

		// Token: 0x0400454E RID: 17742
		public const short BlueArmoredBonesNoPants = 275;

		// Token: 0x0400454F RID: 17743
		public const short BlueArmoredBonesSword = 276;

		// Token: 0x04004550 RID: 17744
		public const short HellArmoredBones = 277;

		// Token: 0x04004551 RID: 17745
		public const short HellArmoredBonesSpikeShield = 278;

		// Token: 0x04004552 RID: 17746
		public const short HellArmoredBonesMace = 279;

		// Token: 0x04004553 RID: 17747
		public const short HellArmoredBonesSword = 280;

		// Token: 0x04004554 RID: 17748
		public const short RaggedCaster = 281;

		// Token: 0x04004555 RID: 17749
		public const short RaggedCasterOpenCoat = 282;

		// Token: 0x04004556 RID: 17750
		public const short Necromancer = 283;

		// Token: 0x04004557 RID: 17751
		public const short NecromancerArmored = 284;

		// Token: 0x04004558 RID: 17752
		public const short DiabolistRed = 285;

		// Token: 0x04004559 RID: 17753
		public const short DiabolistWhite = 286;

		// Token: 0x0400455A RID: 17754
		public const short BoneLee = 287;

		// Token: 0x0400455B RID: 17755
		public const short DungeonSpirit = 288;

		// Token: 0x0400455C RID: 17756
		public const short GiantCursedSkull = 289;

		// Token: 0x0400455D RID: 17757
		public const short Paladin = 290;

		// Token: 0x0400455E RID: 17758
		public const short SkeletonSniper = 291;

		// Token: 0x0400455F RID: 17759
		public const short TacticalSkeleton = 292;

		// Token: 0x04004560 RID: 17760
		public const short SkeletonCommando = 293;

		// Token: 0x04004561 RID: 17761
		public const short AngryBonesBig = 294;

		// Token: 0x04004562 RID: 17762
		public const short AngryBonesBigMuscle = 295;

		// Token: 0x04004563 RID: 17763
		public const short AngryBonesBigHelmet = 296;

		// Token: 0x04004564 RID: 17764
		public const short BirdBlue = 297;

		// Token: 0x04004565 RID: 17765
		public const short BirdRed = 298;

		// Token: 0x04004566 RID: 17766
		public const short Squirrel = 299;

		// Token: 0x04004567 RID: 17767
		public const short Mouse = 300;

		// Token: 0x04004568 RID: 17768
		public const short Raven = 301;

		// Token: 0x04004569 RID: 17769
		public const short SlimeMasked = 302;

		// Token: 0x0400456A RID: 17770
		public const short BunnySlimed = 303;

		// Token: 0x0400456B RID: 17771
		public const short HoppinJack = 304;

		// Token: 0x0400456C RID: 17772
		public const short Scarecrow1 = 305;

		// Token: 0x0400456D RID: 17773
		public const short Scarecrow2 = 306;

		// Token: 0x0400456E RID: 17774
		public const short Scarecrow3 = 307;

		// Token: 0x0400456F RID: 17775
		public const short Scarecrow4 = 308;

		// Token: 0x04004570 RID: 17776
		public const short Scarecrow5 = 309;

		// Token: 0x04004571 RID: 17777
		public const short Scarecrow6 = 310;

		// Token: 0x04004572 RID: 17778
		public const short Scarecrow7 = 311;

		// Token: 0x04004573 RID: 17779
		public const short Scarecrow8 = 312;

		// Token: 0x04004574 RID: 17780
		public const short Scarecrow9 = 313;

		// Token: 0x04004575 RID: 17781
		public const short Scarecrow10 = 314;

		// Token: 0x04004576 RID: 17782
		public const short HeadlessHorseman = 315;

		// Token: 0x04004577 RID: 17783
		public const short Ghost = 316;

		// Token: 0x04004578 RID: 17784
		public const short DemonEyeOwl = 317;

		// Token: 0x04004579 RID: 17785
		public const short DemonEyeSpaceship = 318;

		// Token: 0x0400457A RID: 17786
		public const short ZombieDoctor = 319;

		// Token: 0x0400457B RID: 17787
		public const short ZombieSuperman = 320;

		// Token: 0x0400457C RID: 17788
		public const short ZombiePixie = 321;

		// Token: 0x0400457D RID: 17789
		public const short SkeletonTopHat = 322;

		// Token: 0x0400457E RID: 17790
		public const short SkeletonAstonaut = 323;

		// Token: 0x0400457F RID: 17791
		public const short SkeletonAlien = 324;

		// Token: 0x04004580 RID: 17792
		public const short MourningWood = 325;

		// Token: 0x04004581 RID: 17793
		public const short Splinterling = 326;

		// Token: 0x04004582 RID: 17794
		public const short Pumpking = 327;

		// Token: 0x04004583 RID: 17795
		public const short PumpkingBlade = 328;

		// Token: 0x04004584 RID: 17796
		public const short Hellhound = 329;

		// Token: 0x04004585 RID: 17797
		public const short Poltergeist = 330;

		// Token: 0x04004586 RID: 17798
		public const short ZombieXmas = 331;

		// Token: 0x04004587 RID: 17799
		public const short ZombieSweater = 332;

		// Token: 0x04004588 RID: 17800
		public const short SlimeRibbonWhite = 333;

		// Token: 0x04004589 RID: 17801
		public const short SlimeRibbonYellow = 334;

		// Token: 0x0400458A RID: 17802
		public const short SlimeRibbonGreen = 335;

		// Token: 0x0400458B RID: 17803
		public const short SlimeRibbonRed = 336;

		// Token: 0x0400458C RID: 17804
		public const short BunnyXmas = 337;

		// Token: 0x0400458D RID: 17805
		public const short ZombieElf = 338;

		// Token: 0x0400458E RID: 17806
		public const short ZombieElfBeard = 339;

		// Token: 0x0400458F RID: 17807
		public const short ZombieElfGirl = 340;

		// Token: 0x04004590 RID: 17808
		public const short PresentMimic = 341;

		// Token: 0x04004591 RID: 17809
		public const short GingerbreadMan = 342;

		// Token: 0x04004592 RID: 17810
		public const short Yeti = 343;

		// Token: 0x04004593 RID: 17811
		public const short Everscream = 344;

		// Token: 0x04004594 RID: 17812
		public const short IceQueen = 345;

		// Token: 0x04004595 RID: 17813
		public const short SantaNK1 = 346;

		// Token: 0x04004596 RID: 17814
		public const short ElfCopter = 347;

		// Token: 0x04004597 RID: 17815
		public const short Nutcracker = 348;

		// Token: 0x04004598 RID: 17816
		public const short NutcrackerSpinning = 349;

		// Token: 0x04004599 RID: 17817
		public const short ElfArcher = 350;

		// Token: 0x0400459A RID: 17818
		public const short Krampus = 351;

		// Token: 0x0400459B RID: 17819
		public const short Flocko = 352;

		// Token: 0x0400459C RID: 17820
		public const short Stylist = 353;

		// Token: 0x0400459D RID: 17821
		public const short WebbedStylist = 354;

		// Token: 0x0400459E RID: 17822
		public const short Firefly = 355;

		// Token: 0x0400459F RID: 17823
		public const short Butterfly = 356;

		// Token: 0x040045A0 RID: 17824
		public const short Worm = 357;

		// Token: 0x040045A1 RID: 17825
		public const short LightningBug = 358;

		// Token: 0x040045A2 RID: 17826
		public const short Snail = 359;

		// Token: 0x040045A3 RID: 17827
		public const short GlowingSnail = 360;

		// Token: 0x040045A4 RID: 17828
		public const short Frog = 361;

		// Token: 0x040045A5 RID: 17829
		public const short Duck = 362;

		// Token: 0x040045A6 RID: 17830
		public const short Duck2 = 363;

		// Token: 0x040045A7 RID: 17831
		public const short DuckWhite = 364;

		// Token: 0x040045A8 RID: 17832
		public const short DuckWhite2 = 365;

		// Token: 0x040045A9 RID: 17833
		public const short ScorpionBlack = 366;

		// Token: 0x040045AA RID: 17834
		public const short Scorpion = 367;

		// Token: 0x040045AB RID: 17835
		public const short TravellingMerchant = 368;

		// Token: 0x040045AC RID: 17836
		public const short Angler = 369;

		// Token: 0x040045AD RID: 17837
		public const short DukeFishron = 370;

		// Token: 0x040045AE RID: 17838
		public const short DetonatingBubble = 371;

		// Token: 0x040045AF RID: 17839
		public const short Sharkron = 372;

		// Token: 0x040045B0 RID: 17840
		public const short Sharkron2 = 373;

		// Token: 0x040045B1 RID: 17841
		public const short TruffleWorm = 374;

		// Token: 0x040045B2 RID: 17842
		public const short TruffleWormDigger = 375;

		// Token: 0x040045B3 RID: 17843
		public const short SleepingAngler = 376;

		// Token: 0x040045B4 RID: 17844
		public const short Grasshopper = 377;

		// Token: 0x040045B5 RID: 17845
		public const short ChatteringTeethBomb = 378;

		// Token: 0x040045B6 RID: 17846
		public const short CultistArcherBlue = 379;

		// Token: 0x040045B7 RID: 17847
		public const short CultistArcherWhite = 380;

		// Token: 0x040045B8 RID: 17848
		public const short BrainScrambler = 381;

		// Token: 0x040045B9 RID: 17849
		public const short RayGunner = 382;

		// Token: 0x040045BA RID: 17850
		public const short MartianOfficer = 383;

		// Token: 0x040045BB RID: 17851
		public const short ForceBubble = 384;

		// Token: 0x040045BC RID: 17852
		public const short GrayGrunt = 385;

		// Token: 0x040045BD RID: 17853
		public const short MartianEngineer = 386;

		// Token: 0x040045BE RID: 17854
		public const short MartianTurret = 387;

		// Token: 0x040045BF RID: 17855
		public const short MartianDrone = 388;

		// Token: 0x040045C0 RID: 17856
		public const short GigaZapper = 389;

		// Token: 0x040045C1 RID: 17857
		public const short ScutlixRider = 390;

		// Token: 0x040045C2 RID: 17858
		public const short Scutlix = 391;

		// Token: 0x040045C3 RID: 17859
		public const short MartianSaucer = 392;

		// Token: 0x040045C4 RID: 17860
		public const short MartianSaucerTurret = 393;

		// Token: 0x040045C5 RID: 17861
		public const short MartianSaucerCannon = 394;

		// Token: 0x040045C6 RID: 17862
		public const short MartianSaucerCore = 395;

		// Token: 0x040045C7 RID: 17863
		public const short MoonLordHead = 396;

		// Token: 0x040045C8 RID: 17864
		public const short MoonLordHand = 397;

		// Token: 0x040045C9 RID: 17865
		public const short MoonLordCore = 398;

		// Token: 0x040045CA RID: 17866
		public const short MartianProbe = 399;

		// Token: 0x040045CB RID: 17867
		public const short MoonLordFreeEye = 400;

		// Token: 0x040045CC RID: 17868
		public const short MoonLordLeechBlob = 401;

		// Token: 0x040045CD RID: 17869
		public const short StardustWormHead = 402;

		// Token: 0x040045CE RID: 17870
		public const short StardustWormBody = 403;

		// Token: 0x040045CF RID: 17871
		public const short StardustWormTail = 404;

		// Token: 0x040045D0 RID: 17872
		public const short StardustCellBig = 405;

		// Token: 0x040045D1 RID: 17873
		public const short StardustCellSmall = 406;

		// Token: 0x040045D2 RID: 17874
		public const short StardustJellyfishBig = 407;

		// Token: 0x040045D3 RID: 17875
		public const short StardustJellyfishSmall = 408;

		// Token: 0x040045D4 RID: 17876
		public const short StardustSpiderBig = 409;

		// Token: 0x040045D5 RID: 17877
		public const short StardustSpiderSmall = 410;

		// Token: 0x040045D6 RID: 17878
		public const short StardustSoldier = 411;

		// Token: 0x040045D7 RID: 17879
		public const short SolarCrawltipedeHead = 412;

		// Token: 0x040045D8 RID: 17880
		public const short SolarCrawltipedeBody = 413;

		// Token: 0x040045D9 RID: 17881
		public const short SolarCrawltipedeTail = 414;

		// Token: 0x040045DA RID: 17882
		public const short SolarDrakomire = 415;

		// Token: 0x040045DB RID: 17883
		public const short SolarDrakomireRider = 416;

		// Token: 0x040045DC RID: 17884
		public const short SolarSroller = 417;

		// Token: 0x040045DD RID: 17885
		public const short SolarCorite = 418;

		// Token: 0x040045DE RID: 17886
		public const short SolarSolenian = 419;

		// Token: 0x040045DF RID: 17887
		public const short NebulaBrain = 420;

		// Token: 0x040045E0 RID: 17888
		public const short NebulaHeadcrab = 421;

		// Token: 0x040045E1 RID: 17889
		public const short NebulaBeast = 423;

		// Token: 0x040045E2 RID: 17890
		public const short NebulaSoldier = 424;

		// Token: 0x040045E3 RID: 17891
		public const short VortexRifleman = 425;

		// Token: 0x040045E4 RID: 17892
		public const short VortexHornetQueen = 426;

		// Token: 0x040045E5 RID: 17893
		public const short VortexHornet = 427;

		// Token: 0x040045E6 RID: 17894
		public const short VortexLarva = 428;

		// Token: 0x040045E7 RID: 17895
		public const short VortexSoldier = 429;

		// Token: 0x040045E8 RID: 17896
		public const short ArmedZombie = 430;

		// Token: 0x040045E9 RID: 17897
		public const short ArmedZombieEskimo = 431;

		// Token: 0x040045EA RID: 17898
		public const short ArmedZombiePincussion = 432;

		// Token: 0x040045EB RID: 17899
		public const short ArmedZombieSlimed = 433;

		// Token: 0x040045EC RID: 17900
		public const short ArmedZombieSwamp = 434;

		// Token: 0x040045ED RID: 17901
		public const short ArmedZombieTwiggy = 435;

		// Token: 0x040045EE RID: 17902
		public const short ArmedZombieCenx = 436;

		// Token: 0x040045EF RID: 17903
		public const short CultistTablet = 437;

		// Token: 0x040045F0 RID: 17904
		public const short CultistDevote = 438;

		// Token: 0x040045F1 RID: 17905
		public const short CultistBoss = 439;

		// Token: 0x040045F2 RID: 17906
		public const short CultistBossClone = 440;

		// Token: 0x040045F3 RID: 17907
		public const short GoldBird = 442;

		// Token: 0x040045F4 RID: 17908
		public const short GoldBunny = 443;

		// Token: 0x040045F5 RID: 17909
		public const short GoldButterfly = 444;

		// Token: 0x040045F6 RID: 17910
		public const short GoldFrog = 445;

		// Token: 0x040045F7 RID: 17911
		public const short GoldGrasshopper = 446;

		// Token: 0x040045F8 RID: 17912
		public const short GoldMouse = 447;

		// Token: 0x040045F9 RID: 17913
		public const short GoldWorm = 448;

		// Token: 0x040045FA RID: 17914
		public const short BoneThrowingSkeleton = 449;

		// Token: 0x040045FB RID: 17915
		public const short BoneThrowingSkeleton2 = 450;

		// Token: 0x040045FC RID: 17916
		public const short BoneThrowingSkeleton3 = 451;

		// Token: 0x040045FD RID: 17917
		public const short BoneThrowingSkeleton4 = 452;

		// Token: 0x040045FE RID: 17918
		public const short SkeletonMerchant = 453;

		// Token: 0x040045FF RID: 17919
		public const short CultistDragonHead = 454;

		// Token: 0x04004600 RID: 17920
		public const short CultistDragonBody1 = 455;

		// Token: 0x04004601 RID: 17921
		public const short CultistDragonBody2 = 456;

		// Token: 0x04004602 RID: 17922
		public const short CultistDragonBody3 = 457;

		// Token: 0x04004603 RID: 17923
		public const short CultistDragonBody4 = 458;

		// Token: 0x04004604 RID: 17924
		public const short CultistDragonTail = 459;

		// Token: 0x04004605 RID: 17925
		public const short Butcher = 460;

		// Token: 0x04004606 RID: 17926
		public const short CreatureFromTheDeep = 461;

		// Token: 0x04004607 RID: 17927
		public const short Fritz = 462;

		// Token: 0x04004608 RID: 17928
		public const short Nailhead = 463;

		// Token: 0x04004609 RID: 17929
		public const short CrimsonBunny = 464;

		// Token: 0x0400460A RID: 17930
		public const short CrimsonGoldfish = 465;

		// Token: 0x0400460B RID: 17931
		public const short Psycho = 466;

		// Token: 0x0400460C RID: 17932
		public const short DeadlySphere = 467;

		// Token: 0x0400460D RID: 17933
		public const short DrManFly = 468;

		// Token: 0x0400460E RID: 17934
		public const short ThePossessed = 469;

		// Token: 0x0400460F RID: 17935
		public const short CrimsonPenguin = 470;

		// Token: 0x04004610 RID: 17936
		public const short GoblinSummoner = 471;

		// Token: 0x04004611 RID: 17937
		public const short ShadowFlameApparition = 472;

		// Token: 0x04004612 RID: 17938
		public const short BigMimicCorruption = 473;

		// Token: 0x04004613 RID: 17939
		public const short BigMimicCrimson = 474;

		// Token: 0x04004614 RID: 17940
		public const short BigMimicHallow = 475;

		// Token: 0x04004615 RID: 17941
		public const short BigMimicJungle = 476;

		// Token: 0x04004616 RID: 17942
		public const short Mothron = 477;

		// Token: 0x04004617 RID: 17943
		public const short MothronEgg = 478;

		// Token: 0x04004618 RID: 17944
		public const short MothronSpawn = 479;

		// Token: 0x04004619 RID: 17945
		public const short Medusa = 480;

		// Token: 0x0400461A RID: 17946
		public const short GreekSkeleton = 481;

		// Token: 0x0400461B RID: 17947
		public const short GraniteGolem = 482;

		// Token: 0x0400461C RID: 17948
		public const short GraniteFlyer = 483;

		// Token: 0x0400461D RID: 17949
		public const short EnchantedNightcrawler = 484;

		// Token: 0x0400461E RID: 17950
		public const short Grubby = 485;

		// Token: 0x0400461F RID: 17951
		public const short Sluggy = 486;

		// Token: 0x04004620 RID: 17952
		public const short Buggy = 487;

		// Token: 0x04004621 RID: 17953
		public const short TargetDummy = 488;

		// Token: 0x04004622 RID: 17954
		public const short BloodZombie = 489;

		// Token: 0x04004623 RID: 17955
		public const short Drippler = 490;

		// Token: 0x04004624 RID: 17956
		public const short PirateShip = 491;

		// Token: 0x04004625 RID: 17957
		public const short PirateShipCannon = 492;

		// Token: 0x04004626 RID: 17958
		public const short LunarTowerStardust = 493;

		// Token: 0x04004627 RID: 17959
		public const short Crawdad = 494;

		// Token: 0x04004628 RID: 17960
		public const short Crawdad2 = 495;

		// Token: 0x04004629 RID: 17961
		public const short GiantShelly = 496;

		// Token: 0x0400462A RID: 17962
		public const short GiantShelly2 = 497;

		// Token: 0x0400462B RID: 17963
		public const short Salamander = 498;

		// Token: 0x0400462C RID: 17964
		public const short Salamander2 = 499;

		// Token: 0x0400462D RID: 17965
		public const short Salamander3 = 500;

		// Token: 0x0400462E RID: 17966
		public const short Salamander4 = 501;

		// Token: 0x0400462F RID: 17967
		public const short Salamander5 = 502;

		// Token: 0x04004630 RID: 17968
		public const short Salamander6 = 503;

		// Token: 0x04004631 RID: 17969
		public const short Salamander7 = 504;

		// Token: 0x04004632 RID: 17970
		public const short Salamander8 = 505;

		// Token: 0x04004633 RID: 17971
		public const short Salamander9 = 506;

		// Token: 0x04004634 RID: 17972
		public const short LunarTowerNebula = 507;

		// Token: 0x04004635 RID: 17973
		public const short LunarTowerVortex = 422;

		// Token: 0x04004636 RID: 17974
		public const short TaxCollector = 441;

		// Token: 0x04004637 RID: 17975
		public const short GiantWalkingAntlion = 508;

		// Token: 0x04004638 RID: 17976
		public const short GiantFlyingAntlion = 509;

		// Token: 0x04004639 RID: 17977
		public const short DuneSplicerHead = 510;

		// Token: 0x0400463A RID: 17978
		public const short DuneSplicerBody = 511;

		// Token: 0x0400463B RID: 17979
		public const short DuneSplicerTail = 512;

		// Token: 0x0400463C RID: 17980
		public const short TombCrawlerHead = 513;

		// Token: 0x0400463D RID: 17981
		public const short TombCrawlerBody = 514;

		// Token: 0x0400463E RID: 17982
		public const short TombCrawlerTail = 515;

		// Token: 0x0400463F RID: 17983
		public const short SolarFlare = 516;

		// Token: 0x04004640 RID: 17984
		public const short LunarTowerSolar = 517;

		// Token: 0x04004641 RID: 17985
		public const short SolarSpearman = 518;

		// Token: 0x04004642 RID: 17986
		public const short SolarGoop = 519;

		// Token: 0x04004643 RID: 17987
		public const short MartianWalker = 520;

		// Token: 0x04004644 RID: 17988
		public const short AncientCultistSquidhead = 521;

		// Token: 0x04004645 RID: 17989
		public const short AncientLight = 522;

		// Token: 0x04004646 RID: 17990
		public const short AncientDoom = 523;

		// Token: 0x04004647 RID: 17991
		public const short DesertGhoul = 524;

		// Token: 0x04004648 RID: 17992
		public const short DesertGhoulCorruption = 525;

		// Token: 0x04004649 RID: 17993
		public const short DesertGhoulCrimson = 526;

		// Token: 0x0400464A RID: 17994
		public const short DesertGhoulHallow = 527;

		// Token: 0x0400464B RID: 17995
		public const short DesertLamiaLight = 528;

		// Token: 0x0400464C RID: 17996
		public const short DesertLamiaDark = 529;

		// Token: 0x0400464D RID: 17997
		public const short DesertScorpionWalk = 530;

		// Token: 0x0400464E RID: 17998
		public const short DesertScorpionWall = 531;

		// Token: 0x0400464F RID: 17999
		public const short DesertBeast = 532;

		// Token: 0x04004650 RID: 18000
		public const short DesertDjinn = 533;

		// Token: 0x04004651 RID: 18001
		public const short DemonTaxCollector = 534;

		// Token: 0x04004652 RID: 18002
		public const short SlimeSpiked = 535;

		// Token: 0x04004653 RID: 18003
		public const short TheBride = 536;

		// Token: 0x04004654 RID: 18004
		public const short SandSlime = 537;

		// Token: 0x04004655 RID: 18005
		public const short SquirrelRed = 538;

		// Token: 0x04004656 RID: 18006
		public const short SquirrelGold = 539;

		// Token: 0x04004657 RID: 18007
		public const short PartyBunny = 540;

		// Token: 0x04004658 RID: 18008
		public const short SandElemental = 541;

		// Token: 0x04004659 RID: 18009
		public const short SandShark = 542;

		// Token: 0x0400465A RID: 18010
		public const short SandsharkCorrupt = 543;

		// Token: 0x0400465B RID: 18011
		public const short SandsharkCrimson = 544;

		// Token: 0x0400465C RID: 18012
		public const short SandsharkHallow = 545;

		// Token: 0x0400465D RID: 18013
		public const short Tumbleweed = 546;

		// Token: 0x0400465E RID: 18014
		public const short DD2AttackerTest = 547;

		// Token: 0x0400465F RID: 18015
		public const short DD2EterniaCrystal = 548;

		// Token: 0x04004660 RID: 18016
		public const short DD2LanePortal = 549;

		// Token: 0x04004661 RID: 18017
		public const short DD2Bartender = 550;

		// Token: 0x04004662 RID: 18018
		public const short DD2Betsy = 551;

		// Token: 0x04004663 RID: 18019
		public const short DD2GoblinT1 = 552;

		// Token: 0x04004664 RID: 18020
		public const short DD2GoblinT2 = 553;

		// Token: 0x04004665 RID: 18021
		public const short DD2GoblinT3 = 554;

		// Token: 0x04004666 RID: 18022
		public const short DD2GoblinBomberT1 = 555;

		// Token: 0x04004667 RID: 18023
		public const short DD2GoblinBomberT2 = 556;

		// Token: 0x04004668 RID: 18024
		public const short DD2GoblinBomberT3 = 557;

		// Token: 0x04004669 RID: 18025
		public const short DD2WyvernT1 = 558;

		// Token: 0x0400466A RID: 18026
		public const short DD2WyvernT2 = 559;

		// Token: 0x0400466B RID: 18027
		public const short DD2WyvernT3 = 560;

		// Token: 0x0400466C RID: 18028
		public const short DD2JavelinstT1 = 561;

		// Token: 0x0400466D RID: 18029
		public const short DD2JavelinstT2 = 562;

		// Token: 0x0400466E RID: 18030
		public const short DD2JavelinstT3 = 563;

		// Token: 0x0400466F RID: 18031
		public const short DD2DarkMageT1 = 564;

		// Token: 0x04004670 RID: 18032
		public const short DD2DarkMageT3 = 565;

		// Token: 0x04004671 RID: 18033
		public const short DD2SkeletonT1 = 566;

		// Token: 0x04004672 RID: 18034
		public const short DD2SkeletonT3 = 567;

		// Token: 0x04004673 RID: 18035
		public const short DD2WitherBeastT2 = 568;

		// Token: 0x04004674 RID: 18036
		public const short DD2WitherBeastT3 = 569;

		// Token: 0x04004675 RID: 18037
		public const short DD2DrakinT2 = 570;

		// Token: 0x04004676 RID: 18038
		public const short DD2DrakinT3 = 571;

		// Token: 0x04004677 RID: 18039
		public const short DD2KoboldWalkerT2 = 572;

		// Token: 0x04004678 RID: 18040
		public const short DD2KoboldWalkerT3 = 573;

		// Token: 0x04004679 RID: 18041
		public const short DD2KoboldFlyerT2 = 574;

		// Token: 0x0400467A RID: 18042
		public const short DD2KoboldFlyerT3 = 575;

		// Token: 0x0400467B RID: 18043
		public const short DD2OgreT2 = 576;

		// Token: 0x0400467C RID: 18044
		public const short DD2OgreT3 = 577;

		// Token: 0x0400467D RID: 18045
		public const short DD2LightningBugT3 = 578;

		// Token: 0x0400467E RID: 18046
		public const short BartenderUnconscious = 579;

		// Token: 0x0400467F RID: 18047
		public const short WalkingAntlion = 580;

		// Token: 0x04004680 RID: 18048
		public const short FlyingAntlion = 581;

		// Token: 0x04004681 RID: 18049
		public const short LarvaeAntlion = 582;

		// Token: 0x04004682 RID: 18050
		public const short FairyCritterPink = 583;

		// Token: 0x04004683 RID: 18051
		public const short FairyCritterGreen = 584;

		// Token: 0x04004684 RID: 18052
		public const short FairyCritterBlue = 585;

		// Token: 0x04004685 RID: 18053
		public const short ZombieMerman = 586;

		// Token: 0x04004686 RID: 18054
		public const short EyeballFlyingFish = 587;

		// Token: 0x04004687 RID: 18055
		public const short Golfer = 588;

		// Token: 0x04004688 RID: 18056
		public const short GolferRescue = 589;

		// Token: 0x04004689 RID: 18057
		public const short TorchZombie = 590;

		// Token: 0x0400468A RID: 18058
		public const short ArmedTorchZombie = 591;

		// Token: 0x0400468B RID: 18059
		public const short GoldGoldfish = 592;

		// Token: 0x0400468C RID: 18060
		public const short GoldGoldfishWalker = 593;

		// Token: 0x0400468D RID: 18061
		public const short WindyBalloon = 594;

		// Token: 0x0400468E RID: 18062
		public const short BlackDragonfly = 595;

		// Token: 0x0400468F RID: 18063
		public const short BlueDragonfly = 596;

		// Token: 0x04004690 RID: 18064
		public const short GreenDragonfly = 597;

		// Token: 0x04004691 RID: 18065
		public const short OrangeDragonfly = 598;

		// Token: 0x04004692 RID: 18066
		public const short RedDragonfly = 599;

		// Token: 0x04004693 RID: 18067
		public const short YellowDragonfly = 600;

		// Token: 0x04004694 RID: 18068
		public const short GoldDragonfly = 601;

		// Token: 0x04004695 RID: 18069
		public const short Seagull = 602;

		// Token: 0x04004696 RID: 18070
		public const short Seagull2 = 603;

		// Token: 0x04004697 RID: 18071
		public const short LadyBug = 604;

		// Token: 0x04004698 RID: 18072
		public const short GoldLadyBug = 605;

		// Token: 0x04004699 RID: 18073
		public const short Maggot = 606;

		// Token: 0x0400469A RID: 18074
		public const short Pupfish = 607;

		// Token: 0x0400469B RID: 18075
		public const short Grebe = 608;

		// Token: 0x0400469C RID: 18076
		public const short Grebe2 = 609;

		// Token: 0x0400469D RID: 18077
		public const short Rat = 610;

		// Token: 0x0400469E RID: 18078
		public const short Owl = 611;

		// Token: 0x0400469F RID: 18079
		public const short WaterStrider = 612;

		// Token: 0x040046A0 RID: 18080
		public const short GoldWaterStrider = 613;

		// Token: 0x040046A1 RID: 18081
		public const short ExplosiveBunny = 614;

		// Token: 0x040046A2 RID: 18082
		public const short Dolphin = 615;

		// Token: 0x040046A3 RID: 18083
		public const short Turtle = 616;

		// Token: 0x040046A4 RID: 18084
		public const short TurtleJungle = 617;

		// Token: 0x040046A5 RID: 18085
		public const short BloodNautilus = 618;

		// Token: 0x040046A6 RID: 18086
		public const short BloodSquid = 619;

		// Token: 0x040046A7 RID: 18087
		public const short GoblinShark = 620;

		// Token: 0x040046A8 RID: 18088
		public const short BloodEelHead = 621;

		// Token: 0x040046A9 RID: 18089
		public const short BloodEelBody = 622;

		// Token: 0x040046AA RID: 18090
		public const short BloodEelTail = 623;

		// Token: 0x040046AB RID: 18091
		public const short Gnome = 624;

		// Token: 0x040046AC RID: 18092
		public const short SeaTurtle = 625;

		// Token: 0x040046AD RID: 18093
		public const short Seahorse = 626;

		// Token: 0x040046AE RID: 18094
		public const short GoldSeahorse = 627;

		// Token: 0x040046AF RID: 18095
		public const short Dandelion = 628;

		// Token: 0x040046B0 RID: 18096
		public const short IceMimic = 629;

		// Token: 0x040046B1 RID: 18097
		public const short BloodMummy = 630;

		// Token: 0x040046B2 RID: 18098
		public const short RockGolem = 631;

		// Token: 0x040046B3 RID: 18099
		public const short MaggotZombie = 632;

		// Token: 0x040046B4 RID: 18100
		public const short BestiaryGirl = 633;

		// Token: 0x040046B5 RID: 18101
		public const short SporeBat = 634;

		// Token: 0x040046B6 RID: 18102
		public const short SporeSkeleton = 635;

		// Token: 0x040046B7 RID: 18103
		public const short HallowBoss = 636;

		// Token: 0x040046B8 RID: 18104
		public const short TownCat = 637;

		// Token: 0x040046B9 RID: 18105
		public const short TownDog = 638;

		// Token: 0x040046BA RID: 18106
		public const short GemSquirrelAmethyst = 639;

		// Token: 0x040046BB RID: 18107
		public const short GemSquirrelTopaz = 640;

		// Token: 0x040046BC RID: 18108
		public const short GemSquirrelSapphire = 641;

		// Token: 0x040046BD RID: 18109
		public const short GemSquirrelEmerald = 642;

		// Token: 0x040046BE RID: 18110
		public const short GemSquirrelRuby = 643;

		// Token: 0x040046BF RID: 18111
		public const short GemSquirrelDiamond = 644;

		// Token: 0x040046C0 RID: 18112
		public const short GemSquirrelAmber = 645;

		// Token: 0x040046C1 RID: 18113
		public const short GemBunnyAmethyst = 646;

		// Token: 0x040046C2 RID: 18114
		public const short GemBunnyTopaz = 647;

		// Token: 0x040046C3 RID: 18115
		public const short GemBunnySapphire = 648;

		// Token: 0x040046C4 RID: 18116
		public const short GemBunnyEmerald = 649;

		// Token: 0x040046C5 RID: 18117
		public const short GemBunnyRuby = 650;

		// Token: 0x040046C6 RID: 18118
		public const short GemBunnyDiamond = 651;

		// Token: 0x040046C7 RID: 18119
		public const short GemBunnyAmber = 652;

		// Token: 0x040046C8 RID: 18120
		public const short HellButterfly = 653;

		// Token: 0x040046C9 RID: 18121
		public const short Lavafly = 654;

		// Token: 0x040046CA RID: 18122
		public const short MagmaSnail = 655;

		// Token: 0x040046CB RID: 18123
		public const short TownBunny = 656;

		// Token: 0x040046CC RID: 18124
		public const short QueenSlimeBoss = 657;

		// Token: 0x040046CD RID: 18125
		public const short QueenSlimeMinionBlue = 658;

		// Token: 0x040046CE RID: 18126
		public const short QueenSlimeMinionPink = 659;

		// Token: 0x040046CF RID: 18127
		public const short QueenSlimeMinionPurple = 660;

		// Token: 0x040046D0 RID: 18128
		public const short EmpressButterfly = 661;

		// Token: 0x040046D1 RID: 18129
		public const short PirateGhost = 662;

		// Token: 0x040046D2 RID: 18130
		public const short Princess = 663;

		// Token: 0x040046D3 RID: 18131
		public const short TorchGod = 664;

		// Token: 0x040046D4 RID: 18132
		public const short ChaosBallTim = 665;

		// Token: 0x040046D5 RID: 18133
		public const short VileSpitEaterOfWorlds = 666;

		// Token: 0x040046D6 RID: 18134
		public const short GoldenSlime = 667;

		// Token: 0x040046D7 RID: 18135
		public const short Deerclops = 668;

		// Token: 0x040046D8 RID: 18136
		public const short Stinkbug = 669;

		// Token: 0x040046D9 RID: 18137
		public const short TownSlimeBlue = 670;

		// Token: 0x040046DA RID: 18138
		public const short ScarletMacaw = 671;

		// Token: 0x040046DB RID: 18139
		public const short BlueMacaw = 672;

		// Token: 0x040046DC RID: 18140
		public const short Toucan = 673;

		// Token: 0x040046DD RID: 18141
		public const short YellowCockatiel = 674;

		// Token: 0x040046DE RID: 18142
		public const short GrayCockatiel = 675;

		// Token: 0x040046DF RID: 18143
		public const short ShimmerSlime = 676;

		// Token: 0x040046E0 RID: 18144
		public const short Shimmerfly = 677;

		// Token: 0x040046E1 RID: 18145
		public const short TownSlimeGreen = 678;

		// Token: 0x040046E2 RID: 18146
		public const short TownSlimeOld = 679;

		// Token: 0x040046E3 RID: 18147
		public const short TownSlimePurple = 680;

		// Token: 0x040046E4 RID: 18148
		public const short TownSlimeRainbow = 681;

		// Token: 0x040046E5 RID: 18149
		public const short TownSlimeRed = 682;

		// Token: 0x040046E6 RID: 18150
		public const short TownSlimeYellow = 683;

		// Token: 0x040046E7 RID: 18151
		public const short TownSlimeCopper = 684;

		// Token: 0x040046E8 RID: 18152
		public const short BoundTownSlimeOld = 685;

		// Token: 0x040046E9 RID: 18153
		public const short BoundTownSlimePurple = 686;

		// Token: 0x040046EA RID: 18154
		public const short BoundTownSlimeYellow = 687;

		// Token: 0x040046EB RID: 18155
		public const short Pufferfish = 688;

		// Token: 0x040046EC RID: 18156
		public const short OwlMimic = 689;

		// Token: 0x040046ED RID: 18157
		public const short StatueMimic = 690;

		// Token: 0x040046EE RID: 18158
		public const short MossZombie = 691;

		// Token: 0x040046EF RID: 18159
		public const short Orca = 692;

		// Token: 0x040046F0 RID: 18160
		public const short LibrarianSkeleton = 693;

		// Token: 0x040046F1 RID: 18161
		public const short WaterBoltMimic = 694;

		// Token: 0x040046F2 RID: 18162
		public const short PalworldCattivaDistressed = 695;

		// Token: 0x040046F3 RID: 18163
		public const short PalworldFoxsparksDistressed = 696;

		// Token: 0x040046F4 RID: 18164
		public static readonly short Count = 697;

		// Token: 0x040046F5 RID: 18165
		public static readonly IdDictionary Search = IdDictionary.Create<NPCID, short>();

		// Token: 0x02000789 RID: 1929
		public static class Sets
		{
			// Token: 0x0600415A RID: 16730 RVA: 0x006AA0AF File Offset: 0x006A82AF
			public static NPCID.Sets.NPCPortraitSelector PrioritizedPortrait()
			{
				return new NPCID.Sets.NPCPortraitSelector();
			}

			// Token: 0x0600415B RID: 16731 RVA: 0x006AA0B6 File Offset: 0x006A82B6
			public static NPCID.Sets.BasicNPCPortrait BasicPortrait(string texturePath)
			{
				return new NPCID.Sets.BasicNPCPortrait(texturePath);
			}

			// Token: 0x0600415C RID: 16732 RVA: 0x006AA0BE File Offset: 0x006A82BE
			public static NPCID.Sets.NPCPortraitSelector.SelectionCondition VariantPortraitCondition(int variantIndex)
			{
				return new NPCID.Sets.NPCPortraitSelector.SelectionCondition(new NPCID.Sets.NPCVariantChecker(variantIndex).Fits);
			}

			// Token: 0x0600415D RID: 16733 RVA: 0x006AA0D4 File Offset: 0x006A82D4
			public static bool ShimmeredPortraitCondition()
			{
				int talkNPC = Main.LocalPlayer.talkNPC;
				return talkNPC >= 0 && talkNPC < Main.maxNPCs && Main.npc[talkNPC].IsShimmerVariant;
			}

			// Token: 0x0600415E RID: 16734 RVA: 0x006AA108 File Offset: 0x006A8308
			public static Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> NPCBestiaryDrawOffsetCreation()
			{
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> redigitEntries = NPCID.Sets.GetRedigitEntries();
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> leinforsEntries = NPCID.Sets.GetLeinforsEntries();
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> groxEntries = NPCID.Sets.GetGroxEntries();
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> dictionary = new Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers>();
				foreach (KeyValuePair<int, NPCID.Sets.NPCBestiaryDrawModifiers> keyValuePair in groxEntries)
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
				foreach (KeyValuePair<int, NPCID.Sets.NPCBestiaryDrawModifiers> keyValuePair2 in leinforsEntries)
				{
					dictionary[keyValuePair2.Key] = keyValuePair2.Value;
				}
				foreach (KeyValuePair<int, NPCID.Sets.NPCBestiaryDrawModifiers> keyValuePair3 in redigitEntries)
				{
					dictionary[keyValuePair3.Key] = keyValuePair3.Value;
				}
				return dictionary;
			}

			// Token: 0x0600415F RID: 16735 RVA: 0x006AA214 File Offset: 0x006A8414
			private static Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> GetRedigitEntries()
			{
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> dictionary = new Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers>();
				int num = 691;
				NPCID.Sets.NPCBestiaryDrawModifiers npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num, npcbestiaryDrawModifiers);
				int num2 = 430;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num2, npcbestiaryDrawModifiers);
				int num3 = 431;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num3, npcbestiaryDrawModifiers);
				int num4 = 432;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num4, npcbestiaryDrawModifiers);
				int num5 = 433;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num5, npcbestiaryDrawModifiers);
				int num6 = 434;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num6, npcbestiaryDrawModifiers);
				int num7 = 435;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num7, npcbestiaryDrawModifiers);
				int num8 = 436;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num8, npcbestiaryDrawModifiers);
				int num9 = 591;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num9, npcbestiaryDrawModifiers);
				int num10 = 449;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num10, npcbestiaryDrawModifiers);
				int num11 = 450;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num11, npcbestiaryDrawModifiers);
				int num12 = 451;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num12, npcbestiaryDrawModifiers);
				int num13 = 452;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num13, npcbestiaryDrawModifiers);
				int num14 = 595;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num14, npcbestiaryDrawModifiers);
				int num15 = 596;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num15, npcbestiaryDrawModifiers);
				int num16 = 597;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num16, npcbestiaryDrawModifiers);
				int num17 = 598;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num17, npcbestiaryDrawModifiers);
				int num18 = 600;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num18, npcbestiaryDrawModifiers);
				int num19 = 495;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num19, npcbestiaryDrawModifiers);
				int num20 = 497;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num20, npcbestiaryDrawModifiers);
				int num21 = 498;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num21, npcbestiaryDrawModifiers);
				int num22 = 500;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num22, npcbestiaryDrawModifiers);
				int num23 = 501;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num23, npcbestiaryDrawModifiers);
				int num24 = 502;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num24, npcbestiaryDrawModifiers);
				int num25 = 503;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num25, npcbestiaryDrawModifiers);
				int num26 = 504;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num26, npcbestiaryDrawModifiers);
				int num27 = 505;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num27, npcbestiaryDrawModifiers);
				int num28 = 506;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num28, npcbestiaryDrawModifiers);
				int num29 = 230;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num29, npcbestiaryDrawModifiers);
				int num30 = 593;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num30, npcbestiaryDrawModifiers);
				int num31 = 158;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num31, npcbestiaryDrawModifiers);
				int num32 = -2;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num32, npcbestiaryDrawModifiers);
				int num33 = 440;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num33, npcbestiaryDrawModifiers);
				int num34 = 568;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num34, npcbestiaryDrawModifiers);
				int num35 = 566;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num35, npcbestiaryDrawModifiers);
				int num36 = 576;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num36, npcbestiaryDrawModifiers);
				int num37 = 558;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num37, npcbestiaryDrawModifiers);
				int num38 = 559;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num38, npcbestiaryDrawModifiers);
				int num39 = 552;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num39, npcbestiaryDrawModifiers);
				int num40 = 553;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num40, npcbestiaryDrawModifiers);
				int num41 = 564;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num41, npcbestiaryDrawModifiers);
				int num42 = 570;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num42, npcbestiaryDrawModifiers);
				int num43 = 555;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num43, npcbestiaryDrawModifiers);
				int num44 = 556;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num44, npcbestiaryDrawModifiers);
				int num45 = 574;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num45, npcbestiaryDrawModifiers);
				int num46 = 561;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num46, npcbestiaryDrawModifiers);
				int num47 = 562;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num47, npcbestiaryDrawModifiers);
				int num48 = 572;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num48, npcbestiaryDrawModifiers);
				int num49 = 689;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num49, npcbestiaryDrawModifiers);
				int num50 = 535;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num50, npcbestiaryDrawModifiers);
				return dictionary;
			}

			// Token: 0x06004160 RID: 16736 RVA: 0x006AA7B0 File Offset: 0x006A89B0
			private static Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> GetGroxEntries()
			{
				return new Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> { 
				{
					692,
					new NPCID.Sets.NPCBestiaryDrawModifiers(0)
					{
						Position = new Vector2(35f, 4f),
						Velocity = 1f,
						PortraitPositionXOverride = new float?((float)5),
						IsWet = true,
						SpriteDirection = new int?(1)
					}
				} };
			}

			// Token: 0x06004161 RID: 16737 RVA: 0x006AA81C File Offset: 0x006A8A1C
			private static Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> GetLeinforsEntries()
			{
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> dictionary = new Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers>();
				int num = -65;
				NPCID.Sets.NPCBestiaryDrawModifiers npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num, npcbestiaryDrawModifiers);
				int num2 = -64;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num2, npcbestiaryDrawModifiers);
				int num3 = -63;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num3, npcbestiaryDrawModifiers);
				int num4 = -62;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num4, npcbestiaryDrawModifiers);
				int num5 = -61;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 4f),
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num5, npcbestiaryDrawModifiers);
				int num6 = -60;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 3f),
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num6, npcbestiaryDrawModifiers);
				int num7 = -59;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num7, npcbestiaryDrawModifiers);
				int num8 = -58;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num8, npcbestiaryDrawModifiers);
				int num9 = -57;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num9, npcbestiaryDrawModifiers);
				int num10 = -56;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num10, npcbestiaryDrawModifiers);
				int num11 = -55;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true,
					Velocity = 1f,
					Scale = 1.1f
				};
				dictionary.Add(num11, npcbestiaryDrawModifiers);
				int num12 = -54;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true,
					Velocity = 1f,
					Scale = 0.9f
				};
				dictionary.Add(num12, npcbestiaryDrawModifiers);
				int num13 = -53;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num13, npcbestiaryDrawModifiers);
				int num14 = -52;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num14, npcbestiaryDrawModifiers);
				int num15 = -51;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num15, npcbestiaryDrawModifiers);
				int num16 = -50;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num16, npcbestiaryDrawModifiers);
				int num17 = -49;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num17, npcbestiaryDrawModifiers);
				int num18 = -48;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num18, npcbestiaryDrawModifiers);
				int num19 = -47;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num19, npcbestiaryDrawModifiers);
				int num20 = -46;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num20, npcbestiaryDrawModifiers);
				int num21 = -45;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num21, npcbestiaryDrawModifiers);
				int num22 = -44;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num22, npcbestiaryDrawModifiers);
				int num23 = -43;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(num23, npcbestiaryDrawModifiers);
				int num24 = -42;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(num24, npcbestiaryDrawModifiers);
				int num25 = -41;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(num25, npcbestiaryDrawModifiers);
				int num26 = -40;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(num26, npcbestiaryDrawModifiers);
				int num27 = -39;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(num27, npcbestiaryDrawModifiers);
				int num28 = -38;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(num28, npcbestiaryDrawModifiers);
				int num29 = -37;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num29, npcbestiaryDrawModifiers);
				int num30 = -36;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num30, npcbestiaryDrawModifiers);
				int num31 = -35;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num31, npcbestiaryDrawModifiers);
				int num32 = -34;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num32, npcbestiaryDrawModifiers);
				int num33 = -33;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num33, npcbestiaryDrawModifiers);
				int num34 = -32;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num34, npcbestiaryDrawModifiers);
				int num35 = -31;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num35, npcbestiaryDrawModifiers);
				int num36 = -30;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num36, npcbestiaryDrawModifiers);
				int num37 = -29;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num37, npcbestiaryDrawModifiers);
				int num38 = -28;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num38, npcbestiaryDrawModifiers);
				int num39 = -27;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num39, npcbestiaryDrawModifiers);
				int num40 = -26;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num40, npcbestiaryDrawModifiers);
				int num41 = -23;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -9f),
					Rotation = 0.75f,
					Scale = 1.2f,
					Hide = true
				};
				dictionary.Add(num41, npcbestiaryDrawModifiers);
				int num42 = -22;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -9f),
					Rotation = 0.75f,
					Scale = 0.8f,
					Hide = true
				};
				dictionary.Add(num42, npcbestiaryDrawModifiers);
				int num43 = -25;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num43, npcbestiaryDrawModifiers);
				int num44 = -24;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num44, npcbestiaryDrawModifiers);
				int num45 = -21;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 5f),
					Scale = 1.2f,
					Hide = true
				};
				dictionary.Add(num45, npcbestiaryDrawModifiers);
				int num46 = -20;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 4f),
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num46, npcbestiaryDrawModifiers);
				int num47 = -19;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 3f),
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num47, npcbestiaryDrawModifiers);
				int num48 = -18;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 2f),
					Scale = 0.8f,
					Hide = true
				};
				dictionary.Add(num48, npcbestiaryDrawModifiers);
				int num49 = -17;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 3f),
					Scale = 1.2f,
					Hide = true
				};
				dictionary.Add(num49, npcbestiaryDrawModifiers);
				int num50 = -16;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 3f),
					Scale = 0.8f,
					Hide = true
				};
				dictionary.Add(num50, npcbestiaryDrawModifiers);
				int num51 = -15;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.2f,
					Hide = true
				};
				dictionary.Add(num51, npcbestiaryDrawModifiers);
				int num52 = -14;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(num52, npcbestiaryDrawModifiers);
				int num53 = -13;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(num53, npcbestiaryDrawModifiers);
				int num54 = -12;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -9f),
					Rotation = 0.75f,
					Scale = 1.2f,
					Hide = true
				};
				dictionary.Add(num54, npcbestiaryDrawModifiers);
				int num55 = -11;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -9f),
					Rotation = 0.75f,
					Scale = 0.8f,
					Hide = true
				};
				dictionary.Add(num55, npcbestiaryDrawModifiers);
				int num56 = 2;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, -15f),
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num56, npcbestiaryDrawModifiers);
				int num57 = 3;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num57, npcbestiaryDrawModifiers);
				int num58 = 4;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(25f, -30f),
					Rotation = 0.7f,
					Frame = new int?(4)
				};
				dictionary.Add(num58, npcbestiaryDrawModifiers);
				int num59 = 5;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 4f),
					Rotation = 1.5f
				};
				dictionary.Add(num59, npcbestiaryDrawModifiers);
				int num60 = 6;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -9f),
					Rotation = 0.75f
				};
				dictionary.Add(num60, npcbestiaryDrawModifiers);
				int num61 = 7;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_7",
					Position = new Vector2(20f, 29f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)10)
				};
				dictionary.Add(num61, npcbestiaryDrawModifiers);
				int num62 = 8;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num62, npcbestiaryDrawModifiers);
				int num63 = 9;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num63, npcbestiaryDrawModifiers);
				int num64 = 10;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_10",
					Position = new Vector2(2f, 24f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)10)
				};
				dictionary.Add(num64, npcbestiaryDrawModifiers);
				int num65 = 11;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num65, npcbestiaryDrawModifiers);
				int num66 = 12;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num66, npcbestiaryDrawModifiers);
				int num67 = 13;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_13",
					Position = new Vector2(40f, 22f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)10)
				};
				dictionary.Add(num67, npcbestiaryDrawModifiers);
				int num68 = 14;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num68, npcbestiaryDrawModifiers);
				int num69 = 15;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num69, npcbestiaryDrawModifiers);
				int num70 = 17;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num70, npcbestiaryDrawModifiers);
				int num71 = 18;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num71, npcbestiaryDrawModifiers);
				int num72 = 19;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num72, npcbestiaryDrawModifiers);
				int num73 = 20;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num73, npcbestiaryDrawModifiers);
				int num74 = 22;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num74, npcbestiaryDrawModifiers);
				int num75 = 25;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num75, npcbestiaryDrawModifiers);
				int num76 = 26;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num76, npcbestiaryDrawModifiers);
				int num77 = 27;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num77, npcbestiaryDrawModifiers);
				int num78 = 28;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num78, npcbestiaryDrawModifiers);
				int num79 = 30;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num79, npcbestiaryDrawModifiers);
				int num80 = 665;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num80, npcbestiaryDrawModifiers);
				int num81 = 31;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num81, npcbestiaryDrawModifiers);
				int num82 = 33;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num82, npcbestiaryDrawModifiers);
				int num83 = 34;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Direction = new int?(1)
				};
				dictionary.Add(num83, npcbestiaryDrawModifiers);
				int num84 = 21;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num84, npcbestiaryDrawModifiers);
				int num85 = 35;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -12f),
					Scale = 0.9f,
					PortraitPositionXOverride = new float?((float)(-1)),
					PortraitPositionYOverride = new float?((float)(-3))
				};
				dictionary.Add(num85, npcbestiaryDrawModifiers);
				int num86 = 36;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num86, npcbestiaryDrawModifiers);
				int num87 = 38;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num87, npcbestiaryDrawModifiers);
				int num88 = 37;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num88, npcbestiaryDrawModifiers);
				int num89 = 39;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_39",
					Position = new Vector2(40f, 23f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)10)
				};
				dictionary.Add(num89, npcbestiaryDrawModifiers);
				int num90 = 40;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num90, npcbestiaryDrawModifiers);
				int num91 = 41;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num91, npcbestiaryDrawModifiers);
				int num92 = 43;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, -6f),
					Rotation = 2.3561945f
				};
				dictionary.Add(num92, npcbestiaryDrawModifiers);
				int num93 = 44;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num93, npcbestiaryDrawModifiers);
				int num94 = 46;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num94, npcbestiaryDrawModifiers);
				int num95 = 47;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num95, npcbestiaryDrawModifiers);
				int num96 = 48;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, -14f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num96, npcbestiaryDrawModifiers);
				int num97 = 49;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -13f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num97, npcbestiaryDrawModifiers);
				int num98 = 50;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 90f),
					PortraitScale = new float?(1.1f),
					PortraitPositionYOverride = new float?((float)70)
				};
				dictionary.Add(num98, npcbestiaryDrawModifiers);
				int num99 = 51;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -13f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num99, npcbestiaryDrawModifiers);
				int num100 = 52;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num100, npcbestiaryDrawModifiers);
				int num101 = 53;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num101, npcbestiaryDrawModifiers);
				int num102 = 54;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num102, npcbestiaryDrawModifiers);
				int num103 = 55;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?((float)7),
					IsWet = true
				};
				dictionary.Add(num103, npcbestiaryDrawModifiers);
				int num104 = 56;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, -6f),
					Rotation = 2.3561945f
				};
				dictionary.Add(num104, npcbestiaryDrawModifiers);
				int num105 = 57;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?((float)6),
					IsWet = true
				};
				dictionary.Add(num105, npcbestiaryDrawModifiers);
				int num106 = 58;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?((float)6),
					IsWet = true
				};
				dictionary.Add(num106, npcbestiaryDrawModifiers);
				int num107 = 60;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -19f),
					PortraitPositionYOverride = new float?((float)(-36))
				};
				dictionary.Add(num107, npcbestiaryDrawModifiers);
				int num108 = 61;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?((float)(-15))
				};
				dictionary.Add(num108, npcbestiaryDrawModifiers);
				int num109 = 62;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -10f),
					PortraitPositionYOverride = new float?((float)(-25))
				};
				dictionary.Add(num109, npcbestiaryDrawModifiers);
				int num110 = 65;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, 4f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)5),
					IsWet = true
				};
				dictionary.Add(num110, npcbestiaryDrawModifiers);
				int num111 = 66;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -6f),
					Scale = 0.9f,
					PortraitPositionYOverride = new float?((float)(-15))
				};
				dictionary.Add(num111, npcbestiaryDrawModifiers);
				int num112 = 68;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-1f, -12f),
					Scale = 0.9f,
					PortraitPositionYOverride = new float?((float)(-3))
				};
				dictionary.Add(num112, npcbestiaryDrawModifiers);
				int num113 = 70;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num113, npcbestiaryDrawModifiers);
				int num114 = 72;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num114, npcbestiaryDrawModifiers);
				int num115 = 73;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num115, npcbestiaryDrawModifiers);
				int num116 = 74;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -14f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num116, npcbestiaryDrawModifiers);
				int num117 = 75;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f)
				};
				dictionary.Add(num117, npcbestiaryDrawModifiers);
				int num118 = 76;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num118, npcbestiaryDrawModifiers);
				int num119 = 77;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num119, npcbestiaryDrawModifiers);
				int num120 = 78;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(num120, npcbestiaryDrawModifiers);
				int num121 = 79;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(num121, npcbestiaryDrawModifiers);
				int num122 = 80;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(num122, npcbestiaryDrawModifiers);
				int num123 = 83;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-4f, -4f),
					Scale = 0.9f,
					PortraitPositionYOverride = new float?((float)(-25))
				};
				dictionary.Add(num123, npcbestiaryDrawModifiers);
				int num124 = 84;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, -11f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)(-28))
				};
				dictionary.Add(num124, npcbestiaryDrawModifiers);
				int num125 = 86;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 6f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)2)
				};
				dictionary.Add(num125, npcbestiaryDrawModifiers);
				int num126 = 87;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_87",
					Position = new Vector2(55f, 15f),
					PortraitPositionXOverride = new float?((float)4),
					PortraitPositionYOverride = new float?((float)10)
				};
				dictionary.Add(num126, npcbestiaryDrawModifiers);
				int num127 = 88;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num127, npcbestiaryDrawModifiers);
				int num128 = 89;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num128, npcbestiaryDrawModifiers);
				int num129 = 90;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num129, npcbestiaryDrawModifiers);
				int num130 = 91;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num130, npcbestiaryDrawModifiers);
				int num131 = 92;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num131, npcbestiaryDrawModifiers);
				int num132 = 93;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, -11f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num132, npcbestiaryDrawModifiers);
				int num133 = 94;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(8f, 0f),
					Rotation = 0.75f
				};
				dictionary.Add(num133, npcbestiaryDrawModifiers);
				int num134 = 95;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_95",
					Position = new Vector2(20f, 28f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)10)
				};
				dictionary.Add(num134, npcbestiaryDrawModifiers);
				int num135 = 96;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num135, npcbestiaryDrawModifiers);
				int num136 = 97;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num136, npcbestiaryDrawModifiers);
				int num137 = 98;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_98",
					Position = new Vector2(40f, 24f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)12)
				};
				dictionary.Add(num137, npcbestiaryDrawModifiers);
				int num138 = 99;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num138, npcbestiaryDrawModifiers);
				int num139 = 100;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num139, npcbestiaryDrawModifiers);
				int num140 = 101;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 6f),
					Rotation = 2.3561945f
				};
				dictionary.Add(num140, npcbestiaryDrawModifiers);
				int num141 = 102;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?((float)6),
					IsWet = true
				};
				dictionary.Add(num141, npcbestiaryDrawModifiers);
				int num142 = 104;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num142, npcbestiaryDrawModifiers);
				int num143 = 105;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num143, npcbestiaryDrawModifiers);
				int num144 = 106;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num144, npcbestiaryDrawModifiers);
				int num145 = 107;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num145, npcbestiaryDrawModifiers);
				int num146 = 108;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num146, npcbestiaryDrawModifiers);
				int num147 = 109;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 35f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num147, npcbestiaryDrawModifiers);
				int num148 = 110;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num148, npcbestiaryDrawModifiers);
				int num149 = 111;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num149, npcbestiaryDrawModifiers);
				int num150 = 112;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num150, npcbestiaryDrawModifiers);
				int num151 = 666;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num151, npcbestiaryDrawModifiers);
				int num152 = 113;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_113",
					Position = new Vector2(56f, 5f),
					PortraitPositionXOverride = new float?((float)10),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num152, npcbestiaryDrawModifiers);
				int num153 = 114;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num153, npcbestiaryDrawModifiers);
				int num154 = 115;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_115",
					Position = new Vector2(56f, 3f),
					PortraitPositionXOverride = new float?((float)55),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num154, npcbestiaryDrawModifiers);
				int num155 = 116;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, -5f),
					PortraitPositionXOverride = new float?((float)4),
					PortraitPositionYOverride = new float?((float)(-26))
				};
				dictionary.Add(num155, npcbestiaryDrawModifiers);
				int num156 = 117;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_117",
					Position = new Vector2(10f, 20f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num156, npcbestiaryDrawModifiers);
				int num157 = 118;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num157, npcbestiaryDrawModifiers);
				int num158 = 119;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num158, npcbestiaryDrawModifiers);
				int num159 = 120;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num159, npcbestiaryDrawModifiers);
				int num160 = 123;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num160, npcbestiaryDrawModifiers);
				int num161 = 124;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num161, npcbestiaryDrawModifiers);
				int num162 = 121;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, -4f),
					PortraitPositionYOverride = new float?((float)(-20))
				};
				dictionary.Add(num162, npcbestiaryDrawModifiers);
				int num163 = 122;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f)
				};
				dictionary.Add(num163, npcbestiaryDrawModifiers);
				int num164 = 125;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-28f, -23f),
					Rotation = -0.75f
				};
				dictionary.Add(num164, npcbestiaryDrawModifiers);
				int num165 = 126;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(28f, 30f),
					Rotation = 2.25f
				};
				dictionary.Add(num165, npcbestiaryDrawModifiers);
				int num166 = 127;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_127",
					Position = new Vector2(0f, 0f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)1)
				};
				dictionary.Add(num166, npcbestiaryDrawModifiers);
				int num167 = 128;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-6f, -2f),
					Rotation = -0.75f,
					Hide = true
				};
				dictionary.Add(num167, npcbestiaryDrawModifiers);
				int num168 = 129;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, 4f),
					Rotation = 0.75f,
					Hide = true
				};
				dictionary.Add(num168, npcbestiaryDrawModifiers);
				int num169 = 130;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 8f),
					Rotation = 2.25f,
					Hide = true
				};
				dictionary.Add(num169, npcbestiaryDrawModifiers);
				int num170 = 131;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-8f, 8f),
					Rotation = -2.25f,
					Hide = true
				};
				dictionary.Add(num170, npcbestiaryDrawModifiers);
				int num171 = 132;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num171, npcbestiaryDrawModifiers);
				int num172 = 133;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -5f),
					PortraitPositionYOverride = new float?((float)(-25))
				};
				dictionary.Add(num172, npcbestiaryDrawModifiers);
				int num173 = 134;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_134",
					Position = new Vector2(60f, 8f),
					PortraitPositionXOverride = new float?((float)3),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num173, npcbestiaryDrawModifiers);
				int num174 = 135;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num174, npcbestiaryDrawModifiers);
				int num175 = 136;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num175, npcbestiaryDrawModifiers);
				int num176 = 137;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, -11f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num176, npcbestiaryDrawModifiers);
				int num177 = 140;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num177, npcbestiaryDrawModifiers);
				int num178 = 142;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)1)
				};
				dictionary.Add(num178, npcbestiaryDrawModifiers);
				int num179 = 146;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num179, npcbestiaryDrawModifiers);
				int num180 = 148;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num180, npcbestiaryDrawModifiers);
				int num181 = 149;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num181, npcbestiaryDrawModifiers);
				int num182 = 150;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -11f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num182, npcbestiaryDrawModifiers);
				int num183 = 151;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -11f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num183, npcbestiaryDrawModifiers);
				int num184 = 152;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, -11f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num184, npcbestiaryDrawModifiers);
				int num185 = 153;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 0f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(num185, npcbestiaryDrawModifiers);
				int num186 = 154;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 0f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(num186, npcbestiaryDrawModifiers);
				int num187 = 155;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(15f, 0f),
					Velocity = 3f,
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(num187, npcbestiaryDrawModifiers);
				int num188 = 156;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					PortraitPositionYOverride = new float?((float)(-15))
				};
				dictionary.Add(num188, npcbestiaryDrawModifiers);
				int num189 = 157;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 5f),
					PortraitPositionXOverride = new float?((float)5),
					PortraitPositionYOverride = new float?((float)10),
					IsWet = true
				};
				dictionary.Add(num189, npcbestiaryDrawModifiers);
				int num190 = 160;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num190, npcbestiaryDrawModifiers);
				int num191 = 158;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -11f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num191, npcbestiaryDrawModifiers);
				int num192 = 159;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num192, npcbestiaryDrawModifiers);
				int num193 = 161;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num193, npcbestiaryDrawModifiers);
				int num194 = 162;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num194, npcbestiaryDrawModifiers);
				int num195 = 163;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num195, npcbestiaryDrawModifiers);
				int num196 = 164;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num196, npcbestiaryDrawModifiers);
				int num197 = 165;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Rotation = -1.6f,
					Velocity = 2f
				};
				dictionary.Add(num197, npcbestiaryDrawModifiers);
				int num198 = 167;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num198, npcbestiaryDrawModifiers);
				int num199 = 168;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num199, npcbestiaryDrawModifiers);
				int num200 = 170;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 5f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)(-12))
				};
				dictionary.Add(num200, npcbestiaryDrawModifiers);
				int num201 = 171;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 5f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)(-12))
				};
				dictionary.Add(num201, npcbestiaryDrawModifiers);
				int num202 = 173;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Rotation = 0.75f,
					Position = new Vector2(0f, -5f)
				};
				dictionary.Add(num202, npcbestiaryDrawModifiers);
				int num203 = 174;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -5f),
					Velocity = 1f
				};
				dictionary.Add(num203, npcbestiaryDrawModifiers);
				int num204 = 175;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, -2f),
					Rotation = 2.3561945f
				};
				dictionary.Add(num204, npcbestiaryDrawModifiers);
				int num205 = 176;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(num205, npcbestiaryDrawModifiers);
				int num206 = 177;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 15f),
					PortraitPositionXOverride = new float?((float)(-4)),
					PortraitPositionYOverride = new float?((float)1),
					Frame = new int?(0)
				};
				dictionary.Add(num206, npcbestiaryDrawModifiers);
				int num207 = 178;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num207, npcbestiaryDrawModifiers);
				int num208 = 179;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 12f),
					PortraitPositionYOverride = new float?((float)(-7))
				};
				dictionary.Add(num208, npcbestiaryDrawModifiers);
				int num209 = 180;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 5f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)(-12))
				};
				dictionary.Add(num209, npcbestiaryDrawModifiers);
				int num210 = 181;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num210, npcbestiaryDrawModifiers);
				int num211 = 185;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num211, npcbestiaryDrawModifiers);
				int num212 = 186;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num212, npcbestiaryDrawModifiers);
				int num213 = 187;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num213, npcbestiaryDrawModifiers);
				int num214 = 188;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num214, npcbestiaryDrawModifiers);
				int num215 = 189;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num215, npcbestiaryDrawModifiers);
				int num216 = 190;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, -15f),
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num216, npcbestiaryDrawModifiers);
				int num217 = 191;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, -15f),
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num217, npcbestiaryDrawModifiers);
				int num218 = 192;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, -15f),
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num218, npcbestiaryDrawModifiers);
				int num219 = 193;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num219, npcbestiaryDrawModifiers);
				int num220 = 194;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num220, npcbestiaryDrawModifiers);
				int num221 = 196;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num221, npcbestiaryDrawModifiers);
				int num222 = 197;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num222, npcbestiaryDrawModifiers);
				int num223 = 198;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num223, npcbestiaryDrawModifiers);
				int num224 = 199;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num224, npcbestiaryDrawModifiers);
				int num225 = 200;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionYOverride = new float?((float)2)
				};
				dictionary.Add(num225, npcbestiaryDrawModifiers);
				int num226 = 201;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num226, npcbestiaryDrawModifiers);
				int num227 = 202;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num227, npcbestiaryDrawModifiers);
				int num228 = 203;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num228, npcbestiaryDrawModifiers);
				int num229 = 206;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num229, npcbestiaryDrawModifiers);
				int num230 = 207;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num230, npcbestiaryDrawModifiers);
				int num231 = 208;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num231, npcbestiaryDrawModifiers);
				int num232 = 209;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num232, npcbestiaryDrawModifiers);
				int num233 = 212;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num233, npcbestiaryDrawModifiers);
				int num234 = 213;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num234, npcbestiaryDrawModifiers);
				int num235 = 214;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num235, npcbestiaryDrawModifiers);
				int num236 = 215;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num236, npcbestiaryDrawModifiers);
				int num237 = 216;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num237, npcbestiaryDrawModifiers);
				int num238 = 221;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f),
					Velocity = 1f
				};
				dictionary.Add(num238, npcbestiaryDrawModifiers);
				int num239 = 222;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 55f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)40)
				};
				dictionary.Add(num239, npcbestiaryDrawModifiers);
				int num240 = 223;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num240, npcbestiaryDrawModifiers);
				int num241 = 224;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -10f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num241, npcbestiaryDrawModifiers);
				int num242 = 226;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, 3f),
					PortraitPositionYOverride = new float?((float)(-15))
				};
				dictionary.Add(num242, npcbestiaryDrawModifiers);
				int num243 = 227;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)(-3))
				};
				dictionary.Add(num243, npcbestiaryDrawModifiers);
				int num244 = 228;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)(-5))
				};
				dictionary.Add(num244, npcbestiaryDrawModifiers);
				int num245 = 229;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num245, npcbestiaryDrawModifiers);
				int num246 = 225;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 0f)
				};
				dictionary.Add(num246, npcbestiaryDrawModifiers);
				int num247 = 230;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num247, npcbestiaryDrawModifiers);
				int num248 = 231;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(num248, npcbestiaryDrawModifiers);
				int num249 = 232;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(num249, npcbestiaryDrawModifiers);
				int num250 = 233;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(num250, npcbestiaryDrawModifiers);
				int num251 = 234;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(num251, npcbestiaryDrawModifiers);
				int num252 = 235;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(num252, npcbestiaryDrawModifiers);
				int num253 = 236;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num253, npcbestiaryDrawModifiers);
				int num254 = 237;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Rotation = -1.6f,
					Velocity = 2f
				};
				dictionary.Add(num254, npcbestiaryDrawModifiers);
				int num255 = 238;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Rotation = -1.6f,
					Velocity = 2f
				};
				dictionary.Add(num255, npcbestiaryDrawModifiers);
				int num256 = 239;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num256, npcbestiaryDrawModifiers);
				int num257 = 240;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Rotation = -1.6f,
					Velocity = 2f
				};
				dictionary.Add(num257, npcbestiaryDrawModifiers);
				int num258 = 241;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?((float)6),
					IsWet = true
				};
				dictionary.Add(num258, npcbestiaryDrawModifiers);
				int num259 = 242;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 10f)
				};
				dictionary.Add(num259, npcbestiaryDrawModifiers);
				int num260 = 243;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 60f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?((float)15)
				};
				dictionary.Add(num260, npcbestiaryDrawModifiers);
				int num261 = 245;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_245",
					Position = new Vector2(2f, 48f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)24)
				};
				dictionary.Add(num261, npcbestiaryDrawModifiers);
				int num262 = 246;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num262, npcbestiaryDrawModifiers);
				int num263 = 247;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num263, npcbestiaryDrawModifiers);
				int num264 = 248;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num264, npcbestiaryDrawModifiers);
				int num265 = 249;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num265, npcbestiaryDrawModifiers);
				int num266 = 250;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -6f),
					PortraitPositionYOverride = new float?((float)(-26))
				};
				dictionary.Add(num266, npcbestiaryDrawModifiers);
				int num267 = 251;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num267, npcbestiaryDrawModifiers);
				int num268 = 252;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 3f),
					Velocity = 0.05f
				};
				dictionary.Add(num268, npcbestiaryDrawModifiers);
				int num269 = 253;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(num269, npcbestiaryDrawModifiers);
				int num270 = 254;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num270, npcbestiaryDrawModifiers);
				int num271 = 255;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)(-2))
				};
				dictionary.Add(num271, npcbestiaryDrawModifiers);
				int num272 = 256;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(num272, npcbestiaryDrawModifiers);
				int num273 = 257;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num273, npcbestiaryDrawModifiers);
				int num274 = 258;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num274, npcbestiaryDrawModifiers);
				int num275 = 259;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_259",
					Position = new Vector2(0f, 25f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)8)
				};
				dictionary.Add(num275, npcbestiaryDrawModifiers);
				int num276 = 260;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_260",
					Position = new Vector2(0f, 25f),
					PortraitPositionXOverride = new float?((float)1),
					PortraitPositionYOverride = new float?((float)4)
				};
				dictionary.Add(num276, npcbestiaryDrawModifiers);
				int num277 = 261;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num277, npcbestiaryDrawModifiers);
				int num278 = 262;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 20f),
					Scale = 0.8f
				};
				dictionary.Add(num278, npcbestiaryDrawModifiers);
				int num279 = 264;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num279, npcbestiaryDrawModifiers);
				int num280 = 263;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num280, npcbestiaryDrawModifiers);
				int num281 = 265;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num281, npcbestiaryDrawModifiers);
				int num282 = 266;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, 5f),
					Frame = new int?(4)
				};
				dictionary.Add(num282, npcbestiaryDrawModifiers);
				int num283 = 268;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, -5f)
				};
				dictionary.Add(num283, npcbestiaryDrawModifiers);
				int num284 = 269;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num284, npcbestiaryDrawModifiers);
				int num285 = 270;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num285, npcbestiaryDrawModifiers);
				int num286 = 271;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num286, npcbestiaryDrawModifiers);
				int num287 = 272;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num287, npcbestiaryDrawModifiers);
				int num288 = 273;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num288, npcbestiaryDrawModifiers);
				int num289 = 274;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num289, npcbestiaryDrawModifiers);
				int num290 = 275;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 2f),
					PortraitPositionYOverride = new float?((float)3),
					Velocity = 1f
				};
				dictionary.Add(num290, npcbestiaryDrawModifiers);
				int num291 = 276;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num291, npcbestiaryDrawModifiers);
				int num292 = 277;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num292, npcbestiaryDrawModifiers);
				int num293 = 278;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num293, npcbestiaryDrawModifiers);
				int num294 = 279;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num294, npcbestiaryDrawModifiers);
				int num295 = 280;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num295, npcbestiaryDrawModifiers);
				int num296 = 287;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num296, npcbestiaryDrawModifiers);
				int num297 = 289;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 10f),
					Direction = new int?(1)
				};
				dictionary.Add(num297, npcbestiaryDrawModifiers);
				int num298 = 290;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, 6f),
					Velocity = 1f
				};
				dictionary.Add(num298, npcbestiaryDrawModifiers);
				int num299 = 291;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num299, npcbestiaryDrawModifiers);
				int num300 = 292;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num300, npcbestiaryDrawModifiers);
				int num301 = 293;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num301, npcbestiaryDrawModifiers);
				int num302 = 294;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num302, npcbestiaryDrawModifiers);
				int num303 = 295;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num303, npcbestiaryDrawModifiers);
				int num304 = 296;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num304, npcbestiaryDrawModifiers);
				int num305 = 297;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -14f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num305, npcbestiaryDrawModifiers);
				int num306 = 298;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -14f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num306, npcbestiaryDrawModifiers);
				int num307 = 299;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num307, npcbestiaryDrawModifiers);
				int num308 = 301;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					PortraitPositionYOverride = new float?((float)(-20)),
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 0.05f
				};
				dictionary.Add(num308, npcbestiaryDrawModifiers);
				int num309 = 303;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num309, npcbestiaryDrawModifiers);
				int num310 = 305;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f
				};
				dictionary.Add(num310, npcbestiaryDrawModifiers);
				int num311 = 306;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f
				};
				dictionary.Add(num311, npcbestiaryDrawModifiers);
				int num312 = 307;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f
				};
				dictionary.Add(num312, npcbestiaryDrawModifiers);
				int num313 = 308;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f
				};
				dictionary.Add(num313, npcbestiaryDrawModifiers);
				int num314 = 309;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f
				};
				dictionary.Add(num314, npcbestiaryDrawModifiers);
				int num315 = 310;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num315, npcbestiaryDrawModifiers);
				int num316 = 311;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num316, npcbestiaryDrawModifiers);
				int num317 = 312;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num317, npcbestiaryDrawModifiers);
				int num318 = 313;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num318, npcbestiaryDrawModifiers);
				int num319 = 314;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num319, npcbestiaryDrawModifiers);
				int num320 = 315;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(14f, 26f),
					Velocity = 2f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num320, npcbestiaryDrawModifiers);
				int num321 = 316;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f)
				};
				dictionary.Add(num321, npcbestiaryDrawModifiers);
				int num322 = 317;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num322, npcbestiaryDrawModifiers);
				int num323 = 318;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -13f),
					PortraitPositionYOverride = new float?((float)(-31))
				};
				dictionary.Add(num323, npcbestiaryDrawModifiers);
				int num324 = 319;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num324, npcbestiaryDrawModifiers);
				int num325 = 320;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num325, npcbestiaryDrawModifiers);
				int num326 = 321;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num326, npcbestiaryDrawModifiers);
				int num327 = 322;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num327, npcbestiaryDrawModifiers);
				int num328 = 323;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num328, npcbestiaryDrawModifiers);
				int num329 = 324;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num329, npcbestiaryDrawModifiers);
				int num330 = 325;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 36f)
				};
				dictionary.Add(num330, npcbestiaryDrawModifiers);
				int num331 = 326;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num331, npcbestiaryDrawModifiers);
				int num332 = 327;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -8f)
				};
				dictionary.Add(num332, npcbestiaryDrawModifiers);
				int num333 = 328;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num333, npcbestiaryDrawModifiers);
				int num334 = 329;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 2f
				};
				dictionary.Add(num334, npcbestiaryDrawModifiers);
				int num335 = 330;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 14f)
				};
				dictionary.Add(num335, npcbestiaryDrawModifiers);
				int num336 = 331;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num336, npcbestiaryDrawModifiers);
				int num337 = 332;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num337, npcbestiaryDrawModifiers);
				int num338 = 337;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num338, npcbestiaryDrawModifiers);
				int num339 = 338;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num339, npcbestiaryDrawModifiers);
				int num340 = 339;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num340, npcbestiaryDrawModifiers);
				int num341 = 340;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num341, npcbestiaryDrawModifiers);
				int num342 = 342;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num342, npcbestiaryDrawModifiers);
				int num343 = 343;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, 25f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num343, npcbestiaryDrawModifiers);
				int num344 = 344;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 90f)
				};
				dictionary.Add(num344, npcbestiaryDrawModifiers);
				int num345 = 345;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-1f, 90f)
				};
				dictionary.Add(num345, npcbestiaryDrawModifiers);
				int num346 = 346;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(30f, 80f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)60)
				};
				dictionary.Add(num346, npcbestiaryDrawModifiers);
				int num347 = 347;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f)
				};
				dictionary.Add(num347, npcbestiaryDrawModifiers);
				int num348 = 348;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Hide = true
				};
				dictionary.Add(num348, npcbestiaryDrawModifiers);
				int num349 = 349;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 18f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num349, npcbestiaryDrawModifiers);
				int num350 = 350;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num350, npcbestiaryDrawModifiers);
				int num351 = 351;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 60f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?((float)30)
				};
				dictionary.Add(num351, npcbestiaryDrawModifiers);
				int num352 = 353;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num352, npcbestiaryDrawModifiers);
				int num353 = 633;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num353, npcbestiaryDrawModifiers);
				int num354 = 354;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num354, npcbestiaryDrawModifiers);
				int num355 = 355;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 2f)
				};
				dictionary.Add(num355, npcbestiaryDrawModifiers);
				int num356 = 356;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 3f),
					PortraitPositionYOverride = new float?((float)1)
				};
				dictionary.Add(num356, npcbestiaryDrawModifiers);
				int num357 = 357;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 2f),
					Velocity = 1f
				};
				dictionary.Add(num357, npcbestiaryDrawModifiers);
				int num358 = 358;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 2f)
				};
				dictionary.Add(num358, npcbestiaryDrawModifiers);
				int num359 = 359;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 18f),
					PortraitPositionYOverride = new float?((float)40)
				};
				dictionary.Add(num359, npcbestiaryDrawModifiers);
				int num360 = 360;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 17f),
					PortraitPositionYOverride = new float?((float)39)
				};
				dictionary.Add(num360, npcbestiaryDrawModifiers);
				int num361 = 362;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num361, npcbestiaryDrawModifiers);
				int num362 = 363;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Hide = true
				};
				dictionary.Add(num362, npcbestiaryDrawModifiers);
				int num363 = 364;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num363, npcbestiaryDrawModifiers);
				int num364 = 365;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Hide = true
				};
				dictionary.Add(num364, npcbestiaryDrawModifiers);
				int num365 = 366;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num365, npcbestiaryDrawModifiers);
				int num366 = 367;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num366, npcbestiaryDrawModifiers);
				int num367 = 368;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num367, npcbestiaryDrawModifiers);
				int num368 = 369;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num368, npcbestiaryDrawModifiers);
				int num369 = 370;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(56f, -4f),
					Direction = new int?(1),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num369, npcbestiaryDrawModifiers);
				int num370 = 371;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num370, npcbestiaryDrawModifiers);
				int num371 = 372;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, 4f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)10),
					PortraitPositionYOverride = new float?((float)(-3))
				};
				dictionary.Add(num371, npcbestiaryDrawModifiers);
				int num372 = 373;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num372, npcbestiaryDrawModifiers);
				int num373 = 374;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num373, npcbestiaryDrawModifiers);
				int num374 = 375;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num374, npcbestiaryDrawModifiers);
				int num375 = 376;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num375, npcbestiaryDrawModifiers);
				int num376 = 379;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num376, npcbestiaryDrawModifiers);
				int num377 = 380;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num377, npcbestiaryDrawModifiers);
				int num378 = 381;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num378, npcbestiaryDrawModifiers);
				int num379 = 382;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num379, npcbestiaryDrawModifiers);
				int num380 = 383;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num380, npcbestiaryDrawModifiers);
				int num381 = 384;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num381, npcbestiaryDrawModifiers);
				int num382 = 385;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num382, npcbestiaryDrawModifiers);
				int num383 = 386;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num383, npcbestiaryDrawModifiers);
				int num384 = 387;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 0f),
					Velocity = 3f
				};
				dictionary.Add(num384, npcbestiaryDrawModifiers);
				int num385 = 388;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Direction = new int?(1)
				};
				dictionary.Add(num385, npcbestiaryDrawModifiers);
				int num386 = 389;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-6f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num386, npcbestiaryDrawModifiers);
				int num387 = 390;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(12f, 0f),
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 2f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)(-12))
				};
				dictionary.Add(num387, npcbestiaryDrawModifiers);
				int num388 = 391;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(16f, 12f),
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 2f,
					PortraitPositionXOverride = new float?((float)3),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num388, npcbestiaryDrawModifiers);
				int num389 = 392;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num389, npcbestiaryDrawModifiers);
				int num390 = 395;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_395",
					Position = new Vector2(-1f, 18f),
					PortraitPositionXOverride = new float?((float)1),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num390, npcbestiaryDrawModifiers);
				int num391 = 393;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num391, npcbestiaryDrawModifiers);
				int num392 = 394;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num392, npcbestiaryDrawModifiers);
				int num393 = 396;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num393, npcbestiaryDrawModifiers);
				int num394 = 397;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num394, npcbestiaryDrawModifiers);
				int num395 = 398;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_398",
					Position = new Vector2(0f, 5f),
					Scale = 0.4f,
					PortraitScale = new float?(0.7f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num395, npcbestiaryDrawModifiers);
				int num396 = 400;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num396, npcbestiaryDrawModifiers);
				int num397 = 401;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num397, npcbestiaryDrawModifiers);
				int num398 = 402;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_402",
					Position = new Vector2(42f, 15f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num398, npcbestiaryDrawModifiers);
				int num399 = 403;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num399, npcbestiaryDrawModifiers);
				int num400 = 404;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num400, npcbestiaryDrawModifiers);
				int num401 = 408;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num401, npcbestiaryDrawModifiers);
				int num402 = 410;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num402, npcbestiaryDrawModifiers);
				int num403 = 411;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 1f
				};
				dictionary.Add(num403, npcbestiaryDrawModifiers);
				int num404 = 412;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_412",
					Position = new Vector2(50f, 28f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)4)
				};
				dictionary.Add(num404, npcbestiaryDrawModifiers);
				int num405 = 413;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num405, npcbestiaryDrawModifiers);
				int num406 = 414;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num406, npcbestiaryDrawModifiers);
				int num407 = 415;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(26f, 0f),
					Velocity = 3f,
					PortraitPositionXOverride = new float?((float)5)
				};
				dictionary.Add(num407, npcbestiaryDrawModifiers);
				int num408 = 416;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 20f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num408, npcbestiaryDrawModifiers);
				int num409 = 417;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 8f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num409, npcbestiaryDrawModifiers);
				int num410 = 418;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 4f)
				};
				dictionary.Add(num410, npcbestiaryDrawModifiers);
				int num411 = 419;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num411, npcbestiaryDrawModifiers);
				int num412 = 420;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f),
					Direction = new int?(1)
				};
				dictionary.Add(num412, npcbestiaryDrawModifiers);
				int num413 = 421;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -1f)
				};
				dictionary.Add(num413, npcbestiaryDrawModifiers);
				int num414 = 422;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 44f),
					Scale = 0.4f,
					PortraitPositionXOverride = new float?((float)2),
					PortraitPositionYOverride = new float?((float)134)
				};
				dictionary.Add(num414, npcbestiaryDrawModifiers);
				int num415 = 423;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 1f
				};
				dictionary.Add(num415, npcbestiaryDrawModifiers);
				int num416 = 424;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, 0f),
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 2f
				};
				dictionary.Add(num416, npcbestiaryDrawModifiers);
				int num417 = 425;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, 0f),
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 2f
				};
				dictionary.Add(num417, npcbestiaryDrawModifiers);
				int num418 = 426;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 8f),
					Velocity = 2f,
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num418, npcbestiaryDrawModifiers);
				int num419 = 427;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionYOverride = new float?((float)(-4))
				};
				dictionary.Add(num419, npcbestiaryDrawModifiers);
				int num420 = 428;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num420, npcbestiaryDrawModifiers);
				int num421 = 429;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num421, npcbestiaryDrawModifiers);
				int num422 = 430;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num422, npcbestiaryDrawModifiers);
				int num423 = 431;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num423, npcbestiaryDrawModifiers);
				int num424 = 432;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num424, npcbestiaryDrawModifiers);
				int num425 = 433;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num425, npcbestiaryDrawModifiers);
				int num426 = 434;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num426, npcbestiaryDrawModifiers);
				int num427 = 435;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num427, npcbestiaryDrawModifiers);
				int num428 = 436;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num428, npcbestiaryDrawModifiers);
				int num429 = 437;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num429, npcbestiaryDrawModifiers);
				int num430 = 439;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num430, npcbestiaryDrawModifiers);
				int num431 = 440;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num431, npcbestiaryDrawModifiers);
				int num432 = 441;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num432, npcbestiaryDrawModifiers);
				int num433 = 442;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -14f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num433, npcbestiaryDrawModifiers);
				int num434 = 443;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num434, npcbestiaryDrawModifiers);
				int num435 = 444;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 2f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num435, npcbestiaryDrawModifiers);
				int num436 = 448;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num436, npcbestiaryDrawModifiers);
				int num437 = 449;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num437, npcbestiaryDrawModifiers);
				int num438 = 450;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num438, npcbestiaryDrawModifiers);
				int num439 = 451;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num439, npcbestiaryDrawModifiers);
				int num440 = 452;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num440, npcbestiaryDrawModifiers);
				int num441 = 453;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num441, npcbestiaryDrawModifiers);
				int num442 = 454;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_454",
					Position = new Vector2(57f, 10f),
					PortraitPositionXOverride = new float?((float)5),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num442, npcbestiaryDrawModifiers);
				int num443 = 455;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num443, npcbestiaryDrawModifiers);
				int num444 = 456;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num444, npcbestiaryDrawModifiers);
				int num445 = 457;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num445, npcbestiaryDrawModifiers);
				int num446 = 458;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num446, npcbestiaryDrawModifiers);
				int num447 = 459;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num447, npcbestiaryDrawModifiers);
				int num448 = 460;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num448, npcbestiaryDrawModifiers);
				int num449 = 461;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num449, npcbestiaryDrawModifiers);
				int num450 = 462;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num450, npcbestiaryDrawModifiers);
				int num451 = 463;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num451, npcbestiaryDrawModifiers);
				int num452 = 464;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num452, npcbestiaryDrawModifiers);
				int num453 = 465;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?((float)6),
					IsWet = true
				};
				dictionary.Add(num453, npcbestiaryDrawModifiers);
				int num454 = 466;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num454, npcbestiaryDrawModifiers);
				int num455 = 467;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num455, npcbestiaryDrawModifiers);
				int num456 = 468;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num456, npcbestiaryDrawModifiers);
				int num457 = 469;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num457, npcbestiaryDrawModifiers);
				int num458 = 470;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num458, npcbestiaryDrawModifiers);
				int num459 = 471;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num459, npcbestiaryDrawModifiers);
				int num460 = 472;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 0f),
					PortraitPositionYOverride = new float?((float)(-30)),
					SpriteDirection = new int?(-1),
					Velocity = 1f
				};
				dictionary.Add(num460, npcbestiaryDrawModifiers);
				int num461 = 476;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num461, npcbestiaryDrawModifiers);
				int num462 = 477;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(25f, 6f),
					PortraitPositionXOverride = new float?((float)10)
				};
				dictionary.Add(num462, npcbestiaryDrawModifiers);
				int num463 = 478;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num463, npcbestiaryDrawModifiers);
				int num464 = 479;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, 4f),
					PortraitPositionYOverride = new float?((float)(-15))
				};
				dictionary.Add(num464, npcbestiaryDrawModifiers);
				int num465 = 481;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num465, npcbestiaryDrawModifiers);
				int num466 = 482;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num466, npcbestiaryDrawModifiers);
				int num467 = 483;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -10f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num467, npcbestiaryDrawModifiers);
				int num468 = 484;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num468, npcbestiaryDrawModifiers);
				int num469 = 487;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(num469, npcbestiaryDrawModifiers);
				int num470 = 486;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(num470, npcbestiaryDrawModifiers);
				int num471 = 485;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(num471, npcbestiaryDrawModifiers);
				int num472 = 489;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num472, npcbestiaryDrawModifiers);
				int num473 = 491;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_491",
					Position = new Vector2(30f, -5f),
					Scale = 0.8f,
					PortraitPositionXOverride = new float?((float)1),
					PortraitPositionYOverride = new float?((float)(-1))
				};
				dictionary.Add(num473, npcbestiaryDrawModifiers);
				int num474 = 492;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num474, npcbestiaryDrawModifiers);
				int num475 = 493;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 44f),
					Scale = 0.4f,
					PortraitPositionXOverride = new float?((float)2),
					PortraitPositionYOverride = new float?((float)134)
				};
				dictionary.Add(num475, npcbestiaryDrawModifiers);
				int num476 = 494;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num476, npcbestiaryDrawModifiers);
				int num477 = 495;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-4f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num477, npcbestiaryDrawModifiers);
				int num478 = 496;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(num478, npcbestiaryDrawModifiers);
				int num479 = 497;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(num479, npcbestiaryDrawModifiers);
				int num480 = 498;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num480, npcbestiaryDrawModifiers);
				int num481 = 499;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num481, npcbestiaryDrawModifiers);
				int num482 = 500;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num482, npcbestiaryDrawModifiers);
				int num483 = 501;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num483, npcbestiaryDrawModifiers);
				int num484 = 502;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num484, npcbestiaryDrawModifiers);
				int num485 = 503;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num485, npcbestiaryDrawModifiers);
				int num486 = 504;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num486, npcbestiaryDrawModifiers);
				int num487 = 505;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num487, npcbestiaryDrawModifiers);
				int num488 = 506;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num488, npcbestiaryDrawModifiers);
				int num489 = 507;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 44f),
					Scale = 0.4f,
					PortraitPositionXOverride = new float?((float)2),
					PortraitPositionYOverride = new float?((float)134)
				};
				dictionary.Add(num489, npcbestiaryDrawModifiers);
				int num490 = 508;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Position = new Vector2(10f, 0f),
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(num490, npcbestiaryDrawModifiers);
				int num491 = 509;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 0f),
					PortraitPositionXOverride = new float?((float)(-10)),
					PortraitPositionYOverride = new float?((float)(-20))
				};
				dictionary.Add(num491, npcbestiaryDrawModifiers);
				int num492 = 510;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_510",
					Position = new Vector2(55f, 18f),
					PortraitPositionXOverride = new float?((float)10),
					PortraitPositionYOverride = new float?((float)12)
				};
				dictionary.Add(num492, npcbestiaryDrawModifiers);
				int num493 = 512;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num493, npcbestiaryDrawModifiers);
				int num494 = 511;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num494, npcbestiaryDrawModifiers);
				int num495 = 513;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_513",
					Position = new Vector2(37f, 24f),
					PortraitPositionXOverride = new float?((float)10),
					PortraitPositionYOverride = new float?((float)17)
				};
				dictionary.Add(num495, npcbestiaryDrawModifiers);
				int num496 = 514;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num496, npcbestiaryDrawModifiers);
				int num497 = 515;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num497, npcbestiaryDrawModifiers);
				int num498 = 516;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num498, npcbestiaryDrawModifiers);
				int num499 = 517;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 44f),
					Scale = 0.4f,
					PortraitPositionXOverride = new float?((float)2),
					PortraitPositionYOverride = new float?((float)135)
				};
				dictionary.Add(num499, npcbestiaryDrawModifiers);
				int num500 = 518;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-17f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num500, npcbestiaryDrawModifiers);
				int num501 = 519;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num501, npcbestiaryDrawModifiers);
				int num502 = 520;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 56f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?((float)10)
				};
				dictionary.Add(num502, npcbestiaryDrawModifiers);
				int num503 = 521;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, 5f),
					PortraitPositionYOverride = new float?((float)(-10)),
					SpriteDirection = new int?(-1),
					Velocity = 0.05f
				};
				dictionary.Add(num503, npcbestiaryDrawModifiers);
				int num504 = 522;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num504, npcbestiaryDrawModifiers);
				int num505 = 523;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num505, npcbestiaryDrawModifiers);
				int num506 = 524;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num506, npcbestiaryDrawModifiers);
				int num507 = 525;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num507, npcbestiaryDrawModifiers);
				int num508 = 526;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num508, npcbestiaryDrawModifiers);
				int num509 = 527;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num509, npcbestiaryDrawModifiers);
				int num510 = 528;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num510, npcbestiaryDrawModifiers);
				int num511 = 529;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num511, npcbestiaryDrawModifiers);
				int num512 = 530;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num512, npcbestiaryDrawModifiers);
				int num513 = 531;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f),
					Velocity = 2f,
					PortraitPositionYOverride = new float?((float)10)
				};
				dictionary.Add(num513, npcbestiaryDrawModifiers);
				int num514 = 532;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num514, npcbestiaryDrawModifiers);
				int num515 = 533;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 5f)
				};
				dictionary.Add(num515, npcbestiaryDrawModifiers);
				int num516 = 534;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num516, npcbestiaryDrawModifiers);
				int num517 = 536;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num517, npcbestiaryDrawModifiers);
				int num518 = 538;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num518, npcbestiaryDrawModifiers);
				int num519 = 539;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num519, npcbestiaryDrawModifiers);
				int num520 = 540;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num520, npcbestiaryDrawModifiers);
				int num521 = 541;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 30f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num521, npcbestiaryDrawModifiers);
				int num522 = 542;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, -3f),
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(num522, npcbestiaryDrawModifiers);
				int num523 = 543;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, -3f),
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(num523, npcbestiaryDrawModifiers);
				int num524 = 544;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, -3f),
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(num524, npcbestiaryDrawModifiers);
				int num525 = 545;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, -3f),
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(num525, npcbestiaryDrawModifiers);
				int num526 = 546;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -3f),
					Direction = new int?(1)
				};
				dictionary.Add(num526, npcbestiaryDrawModifiers);
				int num527 = 547;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num527, npcbestiaryDrawModifiers);
				int num528 = 548;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num528, npcbestiaryDrawModifiers);
				int num529 = 549;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num529, npcbestiaryDrawModifiers);
				int num530 = 550;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)(-2))
				};
				dictionary.Add(num530, npcbestiaryDrawModifiers);
				int num531 = 551;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(95f, -4f)
				};
				dictionary.Add(num531, npcbestiaryDrawModifiers);
				int num532 = 552;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num532, npcbestiaryDrawModifiers);
				int num533 = 553;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num533, npcbestiaryDrawModifiers);
				int num534 = 554;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num534, npcbestiaryDrawModifiers);
				int num535 = 555;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num535, npcbestiaryDrawModifiers);
				int num536 = 556;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num536, npcbestiaryDrawModifiers);
				int num537 = 557;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num537, npcbestiaryDrawModifiers);
				int num538 = 558;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, -2f)
				};
				dictionary.Add(num538, npcbestiaryDrawModifiers);
				int num539 = 559;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, -2f)
				};
				dictionary.Add(num539, npcbestiaryDrawModifiers);
				int num540 = 560;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, -2f)
				};
				dictionary.Add(num540, npcbestiaryDrawModifiers);
				int num541 = 561;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num541, npcbestiaryDrawModifiers);
				int num542 = 562;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num542, npcbestiaryDrawModifiers);
				int num543 = 563;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num543, npcbestiaryDrawModifiers);
				int num544 = 566;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num544, npcbestiaryDrawModifiers);
				int num545 = 567;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(num545, npcbestiaryDrawModifiers);
				int num546 = 568;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num546, npcbestiaryDrawModifiers);
				int num547 = 569;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num547, npcbestiaryDrawModifiers);
				int num548 = 570;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 5f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)2)
				};
				dictionary.Add(num548, npcbestiaryDrawModifiers);
				int num549 = 571;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 5f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?((float)2)
				};
				dictionary.Add(num549, npcbestiaryDrawModifiers);
				int num550 = 572;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num550, npcbestiaryDrawModifiers);
				int num551 = 573;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num551, npcbestiaryDrawModifiers);
				int num552 = 578;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f)
				};
				dictionary.Add(num552, npcbestiaryDrawModifiers);
				int num553 = 574;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(16f, 6f),
					Velocity = 1f
				};
				dictionary.Add(num553, npcbestiaryDrawModifiers);
				int num554 = 575;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(16f, 6f),
					Velocity = 1f
				};
				dictionary.Add(num554, npcbestiaryDrawModifiers);
				int num555 = 576;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 70f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)10),
					PortraitPositionYOverride = new float?(0f),
					PortraitScale = new float?(0.75f)
				};
				dictionary.Add(num555, npcbestiaryDrawModifiers);
				int num556 = 577;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 70f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)10),
					PortraitPositionYOverride = new float?(0f),
					PortraitScale = new float?(0.75f)
				};
				dictionary.Add(num556, npcbestiaryDrawModifiers);
				int num557 = 580;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 0f),
					Scale = 0.9f,
					Velocity = 1f
				};
				dictionary.Add(num557, npcbestiaryDrawModifiers);
				int num558 = 581;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -8f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num558, npcbestiaryDrawModifiers);
				int num559 = 582;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num559, npcbestiaryDrawModifiers);
				int num560 = 585;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 1f),
					Direction = new int?(1)
				};
				dictionary.Add(num560, npcbestiaryDrawModifiers);
				int num561 = 584;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 1f),
					Direction = new int?(1)
				};
				dictionary.Add(num561, npcbestiaryDrawModifiers);
				int num562 = 583;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 1f),
					Direction = new int?(1)
				};
				dictionary.Add(num562, npcbestiaryDrawModifiers);
				int num563 = 586;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num563, npcbestiaryDrawModifiers);
				int num564 = 579;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num564, npcbestiaryDrawModifiers);
				int num565 = 588;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)1)
				};
				dictionary.Add(num565, npcbestiaryDrawModifiers);
				int num566 = 587;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, -14f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num566, npcbestiaryDrawModifiers);
				int num567 = 591;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(9f, 0f)
				};
				dictionary.Add(num567, npcbestiaryDrawModifiers);
				int num568 = 590;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)2)
				};
				dictionary.Add(num568, npcbestiaryDrawModifiers);
				int num569 = 592;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?((float)7),
					IsWet = true
				};
				dictionary.Add(num569, npcbestiaryDrawModifiers);
				int num570 = 593;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num570, npcbestiaryDrawModifiers);
				int num571 = 594;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_594",
					Scale = 0.8f
				};
				dictionary.Add(num571, npcbestiaryDrawModifiers);
				int num572 = 589;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num572, npcbestiaryDrawModifiers);
				int num573 = 602;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num573, npcbestiaryDrawModifiers);
				int num574 = 603;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num574, npcbestiaryDrawModifiers);
				int num575 = 604;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 22f),
					PortraitPositionYOverride = new float?((float)41)
				};
				dictionary.Add(num575, npcbestiaryDrawModifiers);
				int num576 = 605;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 22f),
					PortraitPositionYOverride = new float?((float)41)
				};
				dictionary.Add(num576, npcbestiaryDrawModifiers);
				int num577 = 606;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num577, npcbestiaryDrawModifiers);
				int num578 = 607;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?((float)7),
					IsWet = true
				};
				dictionary.Add(num578, npcbestiaryDrawModifiers);
				int num579 = 608;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num579, npcbestiaryDrawModifiers);
				int num580 = 609;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num580, npcbestiaryDrawModifiers);
				int num581 = 611;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 0f),
					Direction = new int?(-1),
					SpriteDirection = new int?(1)
				};
				dictionary.Add(num581, npcbestiaryDrawModifiers);
				int num582 = 612;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num582, npcbestiaryDrawModifiers);
				int num583 = 613;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num583, npcbestiaryDrawModifiers);
				int num584 = 614;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num584, npcbestiaryDrawModifiers);
				int num585 = 615;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 10f),
					Scale = 0.88f,
					PortraitPositionYOverride = new float?((float)20),
					IsWet = true
				};
				dictionary.Add(num585, npcbestiaryDrawModifiers);
				int num586 = 616;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num586, npcbestiaryDrawModifiers);
				int num587 = 617;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num587, npcbestiaryDrawModifiers);
				int num588 = 618;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(12f, -5f),
					Scale = 0.9f,
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(num588, npcbestiaryDrawModifiers);
				int num589 = 619;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 7f),
					Scale = 0.85f,
					PortraitPositionYOverride = new float?((float)10)
				};
				dictionary.Add(num589, npcbestiaryDrawModifiers);
				int num590 = 620;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 5f),
					Scale = 0.78f,
					Velocity = 1f
				};
				dictionary.Add(num590, npcbestiaryDrawModifiers);
				int num591 = 621;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_621",
					Position = new Vector2(46f, 20f),
					PortraitPositionXOverride = new float?((float)10),
					PortraitPositionYOverride = new float?((float)17)
				};
				dictionary.Add(num591, npcbestiaryDrawModifiers);
				int num592 = 622;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num592, npcbestiaryDrawModifiers);
				int num593 = 623;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num593, npcbestiaryDrawModifiers);
				int num594 = 624;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 2f
				};
				dictionary.Add(num594, npcbestiaryDrawModifiers);
				int num595 = 625;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -12f),
					Velocity = 1f
				};
				dictionary.Add(num595, npcbestiaryDrawModifiers);
				int num596 = 626;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -16f)
				};
				dictionary.Add(num596, npcbestiaryDrawModifiers);
				int num597 = 627;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -16f)
				};
				dictionary.Add(num597, npcbestiaryDrawModifiers);
				int num598 = 628;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Direction = new int?(1)
				};
				dictionary.Add(num598, npcbestiaryDrawModifiers);
				int num599 = 630;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(num599, npcbestiaryDrawModifiers);
				int num600 = 632;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num600, npcbestiaryDrawModifiers);
				int num601 = 631;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.75f
				};
				dictionary.Add(num601, npcbestiaryDrawModifiers);
				int num602 = 634;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Position = new Vector2(0f, -13f),
					PortraitPositionYOverride = new float?((float)(-30))
				};
				dictionary.Add(num602, npcbestiaryDrawModifiers);
				int num603 = 635;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num603, npcbestiaryDrawModifiers);
				int num604 = 636;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 50f),
					PortraitPositionYOverride = new float?((float)30)
				};
				dictionary.Add(num604, npcbestiaryDrawModifiers);
				int num605 = 639;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num605, npcbestiaryDrawModifiers);
				int num606 = 640;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num606, npcbestiaryDrawModifiers);
				int num607 = 641;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num607, npcbestiaryDrawModifiers);
				int num608 = 642;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num608, npcbestiaryDrawModifiers);
				int num609 = 643;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num609, npcbestiaryDrawModifiers);
				int num610 = 644;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num610, npcbestiaryDrawModifiers);
				int num611 = 645;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num611, npcbestiaryDrawModifiers);
				int num612 = 646;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num612, npcbestiaryDrawModifiers);
				int num613 = 647;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num613, npcbestiaryDrawModifiers);
				int num614 = 648;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num614, npcbestiaryDrawModifiers);
				int num615 = 649;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num615, npcbestiaryDrawModifiers);
				int num616 = 650;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num616, npcbestiaryDrawModifiers);
				int num617 = 651;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num617, npcbestiaryDrawModifiers);
				int num618 = 652;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num618, npcbestiaryDrawModifiers);
				int num619 = 637;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.25f
				};
				dictionary.Add(num619, npcbestiaryDrawModifiers);
				int num620 = 638;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num620, npcbestiaryDrawModifiers);
				int num621 = 653;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 3f),
					PortraitPositionYOverride = new float?((float)1)
				};
				dictionary.Add(num621, npcbestiaryDrawModifiers);
				int num622 = 654;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 2f)
				};
				dictionary.Add(num622, npcbestiaryDrawModifiers);
				int num623 = 655;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 17f),
					PortraitPositionYOverride = new float?((float)39)
				};
				dictionary.Add(num623, npcbestiaryDrawModifiers);
				int num624 = 656;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(num624, npcbestiaryDrawModifiers);
				int num625 = 657;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 60f),
					PortraitPositionYOverride = new float?((float)40)
				};
				dictionary.Add(num625, npcbestiaryDrawModifiers);
				int num626 = 660;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, -4f),
					PortraitPositionYOverride = new float?((float)(-20))
				};
				dictionary.Add(num626, npcbestiaryDrawModifiers);
				int num627 = 661;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 3f),
					PortraitPositionYOverride = new float?((float)1)
				};
				dictionary.Add(num627, npcbestiaryDrawModifiers);
				int num628 = 662;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num628, npcbestiaryDrawModifiers);
				int num629 = 663;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?((float)1)
				};
				dictionary.Add(num629, npcbestiaryDrawModifiers);
				dictionary.Add(664, new NPCID.Sets.NPCBestiaryDrawModifiers(0));
				int num630 = 667;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num630, npcbestiaryDrawModifiers);
				int num631 = 668;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 2.5f,
					Position = new Vector2(36f, 40f),
					Scale = 0.9f,
					PortraitPositionXOverride = new float?((float)6),
					PortraitPositionYOverride = new float?((float)50)
				};
				dictionary.Add(num631, npcbestiaryDrawModifiers);
				int num632 = 669;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 22f),
					PortraitPositionYOverride = new float?((float)41)
				};
				dictionary.Add(num632, npcbestiaryDrawModifiers);
				int num633 = 670;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(num633, npcbestiaryDrawModifiers);
				int num634 = 678;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(num634, npcbestiaryDrawModifiers);
				int num635 = 679;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(num635, npcbestiaryDrawModifiers);
				int num636 = 680;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(num636, npcbestiaryDrawModifiers);
				int num637 = 681;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(num637, npcbestiaryDrawModifiers);
				int num638 = 682;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(num638, npcbestiaryDrawModifiers);
				int num639 = 683;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(num639, npcbestiaryDrawModifiers);
				int num640 = 684;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(num640, npcbestiaryDrawModifiers);
				int num641 = 671;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -18f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num641, npcbestiaryDrawModifiers);
				int num642 = 672;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -18f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num642, npcbestiaryDrawModifiers);
				int num643 = 673;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -16f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num643, npcbestiaryDrawModifiers);
				int num644 = 674;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -16f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num644, npcbestiaryDrawModifiers);
				int num645 = 675;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -16f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?((float)(-35))
				};
				dictionary.Add(num645, npcbestiaryDrawModifiers);
				int num646 = 677;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 2f)
				};
				dictionary.Add(num646, npcbestiaryDrawModifiers);
				int num647 = 685;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num647, npcbestiaryDrawModifiers);
				int num648 = 686;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num648, npcbestiaryDrawModifiers);
				int num649 = 687;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0f
				};
				dictionary.Add(num649, npcbestiaryDrawModifiers);
				int num650 = 688;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?((float)7),
					IsWet = true
				};
				dictionary.Add(num650, npcbestiaryDrawModifiers);
				int num651 = 695;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num651, npcbestiaryDrawModifiers);
				int num652 = 696;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num652, npcbestiaryDrawModifiers);
				int num653 = 0;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num653, npcbestiaryDrawModifiers);
				int num654 = 488;
				npcbestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(num654, npcbestiaryDrawModifiers);
				return dictionary;
			}

			// Token: 0x06004162 RID: 16738 RVA: 0x006B1A40 File Offset: 0x006AFC40
			// Note: this type is marked as 'beforefieldinit'.
			static Sets()
			{
			}

			// Token: 0x04006F52 RID: 28498
			public static SetFactory Factory = new SetFactory((int)NPCID.Count);

			// Token: 0x04006F53 RID: 28499
			public static Dictionary<int, int> SpecialSpawningRules = new Dictionary<int, int>
			{
				{ 259, 0 },
				{ 260, 0 },
				{ 175, 0 },
				{ 43, 0 },
				{ 56, 0 },
				{ 101, 0 }
			};

			// Token: 0x04006F54 RID: 28500
			public static Dictionary<int, NPCID.Sets.NPCPortraitProvider> NPCPortraits = new Dictionary<int, NPCID.Sets.NPCPortraitProvider>
			{
				{
					369,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Angler_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Angler"))
				},
				{
					19,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_ArmsDealer_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_ArmsDealer"))
				},
				{
					54,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Clothier_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Clothier"))
				},
				{
					209,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Cyborg_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Cyborg"))
				},
				{
					38,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Demolitionist_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Demolitionist"))
				},
				{
					20,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Dryad_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Dryad"))
				},
				{
					207,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_DyeTrader_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_DyeTrader"))
				},
				{
					107,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_GoblinTinkerer_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_GoblinTinkerer"))
				},
				{
					588,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Golfer_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Golfer"))
				},
				{
					22,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Guide_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Guide"))
				},
				{
					124,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Mechanic_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Mechanic"))
				},
				{
					17,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Merchant_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Merchant"))
				},
				{
					18,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Nurse_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Nurse"))
				},
				{
					37,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_OldMan_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_OldMan"))
				},
				{
					227,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Painter_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Painter"))
				},
				{
					208,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_PartyGirl_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_PartyGirl"))
				},
				{
					229,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Pirate_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Pirate"))
				},
				{
					663,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Princess_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Princess"))
				},
				{
					142,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Santa_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Santa"))
				},
				{
					453,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_SkeletonMerchant_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_SkeletonMerchant"))
				},
				{
					178,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Steampunker_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Steampunker"))
				},
				{
					353,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Stylist_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Stylist"))
				},
				{
					550,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Tavernkeep_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Tavernkeep"))
				},
				{
					441,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_TaxCollector_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_TaxCollector"))
				},
				{
					368,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_TravellingMerchant_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_TravellingMerchant"))
				},
				{
					160,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Truffle_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Truffle"))
				},
				{
					228,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_WitchDoctor_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_WitchDoctor"))
				},
				{
					108,
					NPCID.Sets.PrioritizedPortrait().With(new NPCID.Sets.NPCPortraitSelector.SelectionCondition(NPCID.Sets.ShimmeredPortraitCondition), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Wizard_shimmer")).Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Wizard"))
				},
				{
					633,
					NPCID.Sets.PrioritizedPortrait().With(() => NPCID.Sets.ShimmeredPortraitCondition() && !NPC.ShouldBestiaryGirlBeLycantrope(), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Zoologista_shimmer")).With(() => NPCID.Sets.ShimmeredPortraitCondition() && NPC.ShouldBestiaryGirlBeLycantrope(), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Zoologistb_shimmer"))
						.With(() => !NPCID.Sets.ShimmeredPortraitCondition() && NPC.ShouldBestiaryGirlBeLycantrope(), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Zoologistb"))
						.Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Zoologista"))
				},
				{
					680,
					NPCID.Sets.PrioritizedPortrait().Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_SlimeClumsy"))
				},
				{
					678,
					NPCID.Sets.PrioritizedPortrait().Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_SlimeCool"))
				},
				{
					681,
					NPCID.Sets.PrioritizedPortrait().Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_SlimeDiva"))
				},
				{
					679,
					NPCID.Sets.PrioritizedPortrait().Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_SlimeElder"))
				},
				{
					683,
					NPCID.Sets.PrioritizedPortrait().Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_SlimeMystic"))
				},
				{
					670,
					NPCID.Sets.PrioritizedPortrait().Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_SlimeNerdy"))
				},
				{
					684,
					NPCID.Sets.PrioritizedPortrait().Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_SlimeSquire"))
				},
				{
					682,
					NPCID.Sets.PrioritizedPortrait().Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_SlimeSurly"))
				},
				{
					638,
					NPCID.Sets.PrioritizedPortrait().With(NPCID.Sets.VariantPortraitCondition(0), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Dog_Labrador")).With(NPCID.Sets.VariantPortraitCondition(1), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Dog_PitBull"))
						.With(NPCID.Sets.VariantPortraitCondition(2), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Dog_Beagle"))
						.With(NPCID.Sets.VariantPortraitCondition(3), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Dog_Corgi"))
						.With(NPCID.Sets.VariantPortraitCondition(4), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Dog_Dalmatian"))
						.With(NPCID.Sets.VariantPortraitCondition(5), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Dog_Husky"))
						.Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Dog_Labrador"))
				},
				{
					637,
					NPCID.Sets.PrioritizedPortrait().With(NPCID.Sets.VariantPortraitCondition(0), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Cat_Siamese")).With(NPCID.Sets.VariantPortraitCondition(1), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Cat_Black"))
						.With(NPCID.Sets.VariantPortraitCondition(2), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Cat_Orangetabby"))
						.With(NPCID.Sets.VariantPortraitCondition(3), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Cat_RussianBlue"))
						.With(NPCID.Sets.VariantPortraitCondition(4), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Cat_Silver"))
						.With(NPCID.Sets.VariantPortraitCondition(5), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Cat_White"))
						.Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Cat_Black"))
				},
				{
					656,
					NPCID.Sets.PrioritizedPortrait().With(NPCID.Sets.VariantPortraitCondition(0), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Bunny_White")).With(NPCID.Sets.VariantPortraitCondition(1), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Bunny_Angora"))
						.With(NPCID.Sets.VariantPortraitCondition(2), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Bunny_Dutch"))
						.With(NPCID.Sets.VariantPortraitCondition(3), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Bunny_Flemish"))
						.With(NPCID.Sets.VariantPortraitCondition(4), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Bunny_Lop"))
						.With(NPCID.Sets.VariantPortraitCondition(5), NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Bunny_Silver"))
						.Default(NPCID.Sets.BasicPortrait("Images/TownNPCs/Portraits/Portrait_Bunny_Angora"))
				}
			};

			// Token: 0x04006F55 RID: 28501
			public static Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> NPCBestiaryDrawOffset = NPCID.Sets.NPCBestiaryDrawOffsetCreation();

			// Token: 0x04006F56 RID: 28502
			public static Dictionary<int, NPCDebuffImmunityData> DebuffImmunitySets = new Dictionary<int, NPCDebuffImmunityData>
			{
				{ 0, null },
				{
					1,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{ 2, null },
				{ 3, null },
				{
					4,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					5,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					6,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					7,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					8,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					9,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					10,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					11,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					12,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					13,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31, 30, 375 }
					}
				},
				{
					14,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31, 30, 375 }
					}
				},
				{
					15,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31, 30, 375 }
					}
				},
				{
					16,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					17,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					18,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					19,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					20,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					21,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					22,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					23,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					24,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 24, 31, 323 }
					}
				},
				{
					25,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 26, null },
				{ 27, null },
				{ 28, null },
				{
					29,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					30,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					665,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					31,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					32,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					33,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					34,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					35,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31, 169, 337, 344 }
					}
				},
				{
					36,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					37,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					38,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					39,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					40,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					41,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					42,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					43,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					44,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					45,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					46,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 47, null },
				{
					48,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 49, null },
				{
					50,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{ 51, null },
				{ 52, null },
				{ 53, null },
				{
					54,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					55,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					56,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					57,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					58,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					59,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 323 }
					}
				},
				{
					60,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 24, 323 }
					}
				},
				{
					61,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					62,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 24, 31, 153, 323 }
					}
				},
				{
					63,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					64,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					65,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					66,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 24, 31, 153, 323 }
					}
				},
				{
					67,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					68,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					69,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					70,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 39, 70, 323 }
					}
				},
				{
					71,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					72,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{ 73, null },
				{
					74,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 75, null },
				{
					76,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					77,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{ 78, null },
				{ 79, null },
				{ 80, null },
				{
					81,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					82,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					83,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					84,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					85,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					690,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{ 86, null },
				{
					87,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					88,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					89,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					90,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					91,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					92,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 93, null },
				{
					94,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					95,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					96,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					97,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					98,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					99,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					100,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					101,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 39, 31 }
					}
				},
				{
					102,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					103,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 104, null },
				{
					105,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					106,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					107,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					108,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					109,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					110,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{ 111, null },
				{
					112,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					666,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					113,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 24, 31, 323 }
					}
				},
				{
					114,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 24, 31, 323 }
					}
				},
				{
					115,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					116,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					117,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					118,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					119,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					120,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					121,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					122,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					123,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					124,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					125,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					126,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					127,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31, 169, 337, 344 }
					}
				},
				{
					128,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					129,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					130,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					131,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{ 132, null },
				{ 133, null },
				{
					134,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					135,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					136,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					137,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					138,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					139,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					140,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 323 }
					}
				},
				{
					141,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 70 }
					}
				},
				{
					142,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					143,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 44, 324 }
					}
				},
				{
					144,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 44, 324 }
					}
				},
				{
					145,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 44, 324 }
					}
				},
				{
					146,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					147,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 44, 324 }
					}
				},
				{
					148,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					149,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					150,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 44, 324 }
					}
				},
				{
					151,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 24, 323 }
					}
				},
				{ 152, null },
				{ 153, null },
				{
					154,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 44, 324 }
					}
				},
				{ 155, null },
				{
					156,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 24, 31, 153, 323 }
					}
				},
				{
					157,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 158, null },
				{ 159, null },
				{
					160,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					161,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 44, 324 }
					}
				},
				{ 162, null },
				{
					163,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					164,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					165,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					166,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					167,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 44, 324 }
					}
				},
				{ 168, null },
				{
					169,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31, 44, 324 }
					}
				},
				{ 170, null },
				{ 171, null },
				{
					172,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					173,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					174,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					175,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					176,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					177,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					178,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					179,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{ 180, null },
				{
					181,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					182,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					183,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					184,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 44, 324 }
					}
				},
				{
					185,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 44, 324 }
					}
				},
				{ 186, null },
				{ 187, null },
				{ 188, null },
				{ 189, null },
				{ 190, null },
				{ 191, null },
				{ 192, null },
				{ 193, null },
				{ 194, null },
				{
					195,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 196, null },
				{
					197,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 44, 324 }
					}
				},
				{
					198,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					199,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{ 200, null },
				{
					201,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					202,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					203,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					204,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					205,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					206,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 44, 324 }
					}
				},
				{
					207,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					208,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					209,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					210,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					211,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{ 212, null },
				{ 213, null },
				{ 214, null },
				{ 215, null },
				{
					216,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					217,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					218,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					219,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					220,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					221,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					222,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{ 223, null },
				{ 224, null },
				{
					225,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{ 226, null },
				{
					227,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					228,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					229,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					230,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					231,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					232,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					233,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					234,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					235,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					236,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					237,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					238,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					239,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					240,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					241,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					242,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					243,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31, 44, 324 }
					}
				},
				{
					244,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					245,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					246,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					247,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					248,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					249,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					250,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{ 251, null },
				{ 252, null },
				{
					253,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{ 254, null },
				{ 255, null },
				{
					256,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 257, null },
				{ 258, null },
				{
					259,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					260,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					261,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					262,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					263,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					264,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					265,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					266,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					267,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					268,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 69 }
					}
				},
				{
					269,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					270,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					271,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					272,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					273,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					274,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					275,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					276,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					277,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 323 }
					}
				},
				{
					278,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 323 }
					}
				},
				{
					279,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 323 }
					}
				},
				{
					280,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 323 }
					}
				},
				{
					281,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					282,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					283,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					284,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					285,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					286,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					287,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					288,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					289,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					290,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31, 69 }
					}
				},
				{
					291,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					292,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					293,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					294,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					295,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					296,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					297,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					298,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					671,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					672,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					673,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					674,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					675,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					299,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					300,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					301,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					302,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					303,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					304,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{ 305, null },
				{ 306, null },
				{ 307, null },
				{ 308, null },
				{ 309, null },
				{ 310, null },
				{ 311, null },
				{ 312, null },
				{ 313, null },
				{ 314, null },
				{
					315,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					316,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20, 24, 31, 39, 44, 69, 70, 153, 189, 203,
							204, 323, 324
						}
					}
				},
				{ 317, null },
				{ 318, null },
				{ 319, null },
				{ 320, null },
				{ 321, null },
				{
					322,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					323,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					324,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					325,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{ 326, null },
				{
					327,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					328,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					329,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 323 }
					}
				},
				{
					330,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{ 331, null },
				{ 332, null },
				{
					333,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					334,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					335,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					336,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					337,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					338,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 44, 324 }
					}
				},
				{
					339,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 44, 324 }
					}
				},
				{
					340,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 44, 324 }
					}
				},
				{
					341,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{ 342, null },
				{
					343,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 44, 324 }
					}
				},
				{
					344,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31, 44, 324 }
					}
				},
				{
					345,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31, 44, 324 }
					}
				},
				{
					346,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31, 44, 324 }
					}
				},
				{
					347,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 348, null },
				{ 349, null },
				{ 350, null },
				{
					351,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 44, 324 }
					}
				},
				{
					352,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31, 44, 324 }
					}
				},
				{
					353,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					354,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					355,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					356,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					357,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					358,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					359,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					360,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					361,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					362,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					363,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					364,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					365,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					366,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					367,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					368,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					369,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					370,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					371,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					372,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					373,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					374,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					375,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					376,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					377,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					378,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					379,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					380,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					381,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 382, null },
				{ 383, null },
				{
					384,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{ 385, null },
				{ 386, null },
				{
					387,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					388,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{ 389, null },
				{ 390, null },
				{ 391, null },
				{
					392,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					393,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					394,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					395,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					396,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					397,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					398,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					399,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					400,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					401,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					402,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					403,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					404,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					405,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					406,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					407,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					408,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 409, null },
				{ 410, null },
				{ 411, null },
				{
					412,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					413,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					414,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 415, null },
				{ 416, null },
				{ 417, null },
				{
					418,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{ 419, null },
				{
					420,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					421,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					422,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{ 423, null },
				{ 424, null },
				{ 425, null },
				{ 426, null },
				{ 427, null },
				{
					428,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 429, null },
				{ 430, null },
				{
					431,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 44, 324 }
					}
				},
				{ 432, null },
				{ 433, null },
				{ 434, null },
				{ 435, null },
				{ 436, null },
				{
					437,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					438,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					439,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					440,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					441,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					442,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					443,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					444,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					445,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					446,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					447,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					448,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					449,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					450,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					451,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					452,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					453,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					454,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					455,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					456,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					457,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					458,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					459,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{ 460, null },
				{
					461,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 462, null },
				{ 463, null },
				{ 464, null },
				{
					465,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 466, null },
				{
					467,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{ 468, null },
				{ 469, null },
				{ 470, null },
				{
					471,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31, 153 }
					}
				},
				{
					472,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31, 153 }
					}
				},
				{
					473,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					474,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					475,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					476,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					477,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					478,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					479,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					480,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					481,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					482,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					483,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					484,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					485,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					486,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					487,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					488,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 489, null },
				{ 490, null },
				{
					491,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					492,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					493,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					494,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					495,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					496,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					497,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 498, null },
				{ 499, null },
				{ 500, null },
				{ 501, null },
				{ 502, null },
				{ 503, null },
				{ 504, null },
				{ 505, null },
				{ 506, null },
				{
					507,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{ 508, null },
				{ 509, null },
				{
					510,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					511,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					512,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					513,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					514,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					515,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					516,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					517,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					518,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					519,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					520,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{
					521,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20, 24, 31, 39, 44, 69, 70, 153, 189, 203,
							204, 323, 324
						}
					}
				},
				{
					522,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					523,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 524, null },
				{ 525, null },
				{ 526, null },
				{ 527, null },
				{ 528, null },
				{ 529, null },
				{
					530,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					531,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{ 532, null },
				{
					533,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20, 24, 31, 39, 44, 69, 70, 153, 189, 203,
							204, 323, 324
						}
					}
				},
				{ 534, null },
				{
					535,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{ 536, null },
				{
					537,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					538,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					539,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					540,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					541,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					542,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					543,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					544,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					545,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					546,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					547,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					548,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					549,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					550,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					551,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 24, 31, 323 }
					}
				},
				{
					552,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					553,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					554,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					555,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					556,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					557,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					558,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					559,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					560,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					561,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					562,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					563,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					564,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					565,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					566,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					567,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					568,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					569,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					570,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					571,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					572,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					573,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					574,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					575,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					576,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					577,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					578,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					579,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 580, null },
				{ 581, null },
				{ 582, null },
				{
					583,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					584,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					585,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{ 586, null },
				{ 587, null },
				{
					588,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					589,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 590, null },
				{ 591, null },
				{
					592,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					593,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					594,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					595,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					596,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					597,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					598,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					599,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					600,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					601,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					602,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					603,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					604,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					605,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					606,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					607,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					608,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					609,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					610,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					611,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					689,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					612,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					613,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					614,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					615,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					616,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					617,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					618,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					619,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					620,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					621,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					622,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					623,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					624,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					625,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					626,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					627,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					628,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					629,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 44, 323, 324 }
					}
				},
				{ 630, null },
				{
					631,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 24, 31, 323 }
					}
				},
				{ 632, null },
				{
					633,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{ 634, null },
				{
					635,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20 }
					}
				},
				{
					636,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					637,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					638,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					639,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					640,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					641,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					642,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					643,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					644,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					645,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					646,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					647,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					648,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					649,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					650,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					651,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					652,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					653,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					654,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					655,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					656,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					657,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					658,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					659,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					660,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					661,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					662,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					663,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					668,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					669,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					670,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					678,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					679,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					680,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					681,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					682,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					683,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					684,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					677,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					685,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					686,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					687,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					688,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					692,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					693,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					694,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 20, 31 }
					}
				},
				{
					695,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				},
				{
					696,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[] { 31 }
					}
				}
			};

			// Token: 0x04006F57 RID: 28503
			public static List<int> NormalGoldCritterBestiaryPriority = new List<int>
			{
				46, 540, 614, 303, 337, 443, 74, 297, 298, 671,
				672, 673, 674, 675, 442, 55, 230, 592, 593, 299,
				538, 539, 300, 447, 361, 445, 377, 446, 356, 444,
				357, 448, 595, 596, 597, 598, 599, 600, 601, 626,
				627, 612, 613, 604, 605, 669, 677
			};

			// Token: 0x04006F58 RID: 28504
			public static List<int> BossBestiaryPriority = new List<int>
			{
				664, 4, 5, 50, 535, 13, 14, 15, 266, 267,
				668, 35, 36, 222, 113, 114, 117, 115, 116, 657,
				658, 659, 660, 125, 126, 134, 135, 136, 139, 127,
				128, 131, 129, 130, 262, 263, 264, 636, 245, 246,
				249, 247, 248, 370, 372, 373, 439, 438, 379, 380,
				440, 521, 454, 507, 517, 422, 493, 398, 396, 397,
				400, 401
			};

			// Token: 0x04006F59 RID: 28505
			public static List<int> TownNPCBestiaryPriority = new List<int>
			{
				22, 17, 18, 38, 369, 20, 19, 207, 227, 353,
				633, 550, 588, 107, 228, 124, 54, 108, 178, 229,
				160, 441, 209, 208, 663, 142, 637, 638, 656, 670,
				678, 679, 680, 681, 682, 683, 684, 368, 453, 37,
				687
			};

			// Token: 0x04006F5A RID: 28506
			public static bool[] SpawnOnPlayerCanSpawnInMidairOnSkyblock = NPCID.Sets.Factory.CreateBoolSet(new int[] { 4, 266, 222, 125, 126, 127, 262, 551 });

			// Token: 0x04006F5B RID: 28507
			public static bool[] DontDropDungeonKeysOrSouls = NPCID.Sets.Factory.CreateBoolSet(new int[] { 23 });

			// Token: 0x04006F5C RID: 28508
			public static bool[] DontDoHardmodeScaling = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				5, 13, 14, 15, 267, 113, 114, 115, 116, 117,
				118, 119, 658, 659, 660, 400, 522
			});

			// Token: 0x04006F5D RID: 28509
			public static bool[] ReflectStarShotsInForTheWorthy = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				4, 5, 13, 14, 15, 266, 267, 35, 36, 113,
				114, 115, 116, 117, 118, 119, 125, 126, 134, 135,
				136, 139, 127, 128, 131, 129, 130, 262, 263, 264,
				245, 247, 248, 246, 249, 398, 400, 397, 396, 401
			});

			// Token: 0x04006F5E RID: 28510
			public static bool[] IsTownPet = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				637, 638, 656, 670, 678, 679, 680, 681, 682, 683,
				684
			});

			// Token: 0x04006F5F RID: 28511
			public static bool[] IsTownSlime = NPCID.Sets.Factory.CreateBoolSet(new int[] { 670, 678, 679, 680, 681, 682, 683, 684 });

			// Token: 0x04006F60 RID: 28512
			public static bool[] CanConvertIntoCopperSlimeTownNPC = NPCID.Sets.Factory.CreateBoolSet(new int[] { 1, 302, 335, 336, 333, 334 });

			// Token: 0x04006F61 RID: 28513
			public static List<int> GoldCrittersCollection = new List<int>
			{
				443, 442, 592, 593, 444, 601, 445, 446, 605, 447,
				627, 613, 448, 539
			};

			// Token: 0x04006F62 RID: 28514
			public static bool[] IsGoldCritter = NPCID.Sets.Factory.CreateBoolSet(false, new int[]
			{
				442, 443, 444, 445, 446, 447, 448, 539, 592, 593,
				601, 605, 613, 627
			});

			// Token: 0x04006F63 RID: 28515
			public static bool[] ZappingJellyfish = NPCID.Sets.Factory.CreateBoolSet(new int[] { 63, 64, 103, 242 });

			// Token: 0x04006F64 RID: 28516
			public static bool?[] HunterPotionFriendlyOverride = NPCID.Sets.Factory.CreateCustomSet<bool?>(null, new object[] { 689, false });

			// Token: 0x04006F65 RID: 28517
			public static bool[] CantTakeLunchMoney = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				394, 393, 392, 690, 492, 491, 662, 384, 478, 535,
				658, 659, 660, 128, 131, 129, 130, 139, 267, 247,
				248, 246, 249, 245, 409, 410, 397, 396, 401, 400,
				440, 68, 534, 316
			});

			// Token: 0x04006F66 RID: 28518
			public static Dictionary<int, int> RespawnEnemyID = new Dictionary<int, int>
			{
				{ 492, 0 },
				{ 491, 0 },
				{ 394, 0 },
				{ 393, 0 },
				{ 392, 0 },
				{ 13, 0 },
				{ 14, 0 },
				{ 15, 0 },
				{ 412, 0 },
				{ 413, 0 },
				{ 414, 0 },
				{ 134, 0 },
				{ 135, 0 },
				{ 136, 0 },
				{ 454, 0 },
				{ 455, 0 },
				{ 456, 0 },
				{ 457, 0 },
				{ 458, 0 },
				{ 459, 0 },
				{ 8, 7 },
				{ 9, 7 },
				{ 11, 10 },
				{ 12, 10 },
				{ 40, 39 },
				{ 41, 39 },
				{ 96, 95 },
				{ 97, 95 },
				{ 99, 98 },
				{ 100, 98 },
				{ 88, 87 },
				{ 89, 87 },
				{ 90, 87 },
				{ 91, 87 },
				{ 92, 87 },
				{ 118, 117 },
				{ 119, 117 },
				{ 514, 513 },
				{ 515, 513 },
				{ 511, 510 },
				{ 512, 510 },
				{ 622, 621 },
				{ 623, 621 }
			};

			// Token: 0x04006F67 RID: 28519
			public static Dictionary<int, Vector2> NPCPortraitsCloseUpOffsets = new Dictionary<int, Vector2>
			{
				{
					17,
					new Vector2(6f, 0f)
				},
				{
					18,
					new Vector2(-6f, 0f)
				},
				{
					19,
					new Vector2(0f, 0f)
				},
				{
					20,
					new Vector2(0f, 0f)
				},
				{
					22,
					new Vector2(0f, 0f)
				},
				{
					38,
					new Vector2(0f, -6f)
				},
				{
					54,
					new Vector2(6f, 0f)
				},
				{
					37,
					new Vector2(0f, 0f)
				},
				{
					107,
					new Vector2(0f, 0f)
				},
				{
					108,
					new Vector2(6f, 4f)
				},
				{
					124,
					new Vector2(-6f, 4f)
				},
				{
					142,
					new Vector2(3f, 6f)
				},
				{
					160,
					new Vector2(-6f, 18f)
				},
				{
					178,
					new Vector2(-6f, 12f)
				},
				{
					207,
					new Vector2(0f, 0f)
				},
				{
					208,
					new Vector2(-12f, 0f)
				},
				{
					209,
					new Vector2(0f, 6f)
				},
				{
					227,
					new Vector2(-3f, 0f)
				},
				{
					228,
					new Vector2(9f, 6f)
				},
				{
					229,
					new Vector2(0f, 0f)
				},
				{
					353,
					new Vector2(-6f, 6f)
				},
				{
					368,
					new Vector2(0f, 6f)
				},
				{
					369,
					new Vector2(-6f, -6f)
				},
				{
					441,
					new Vector2(-9f, 0f)
				},
				{
					453,
					new Vector2(-12f, 0f)
				},
				{
					550,
					new Vector2(15f, 12f)
				},
				{
					588,
					new Vector2(3f, 0f)
				},
				{
					633,
					new Vector2(-3f, 0f)
				},
				{
					663,
					new Vector2(0f, -6f)
				},
				{
					637,
					new Vector2(-15f, 8f)
				},
				{
					638,
					new Vector2(-24f, 12f)
				},
				{
					656,
					new Vector2(0f, 0f)
				},
				{
					684,
					new Vector2(-3f, 2f)
				},
				{
					670,
					new Vector2(0f, 2f)
				},
				{
					678,
					new Vector2(-3f, 2f)
				},
				{
					679,
					new Vector2(0f, 2f)
				},
				{
					680,
					new Vector2(-6f, 2f)
				},
				{
					681,
					new Vector2(0f, 2f)
				},
				{
					683,
					new Vector2(-6f, 2f)
				},
				{
					682,
					new Vector2(-3f, 2f)
				}
			};

			// Token: 0x04006F68 RID: 28520
			public static Dictionary<int, Vector2> NPCPortraitsFullBodyRetroOffsets = new Dictionary<int, Vector2>
			{
				{
					18,
					new Vector2(-3f, 0f)
				},
				{
					20,
					new Vector2(-4f, 0f)
				},
				{
					124,
					new Vector2(-6f, 2f)
				},
				{
					178,
					new Vector2(-4f, 0f)
				},
				{
					208,
					new Vector2(-4f, 0f)
				},
				{
					227,
					new Vector2(6f, 0f)
				},
				{
					353,
					new Vector2(-4f, 0f)
				},
				{
					369,
					new Vector2(-4f, 0f)
				},
				{
					441,
					new Vector2(-10f, 0f)
				},
				{
					588,
					new Vector2(-2f, 0f)
				},
				{
					633,
					new Vector2(-6f, 0f)
				},
				{
					637,
					new Vector2(-8f, 0f)
				},
				{
					638,
					new Vector2(-8f, 0f)
				},
				{
					684,
					new Vector2(-12f, 0f)
				},
				{
					670,
					new Vector2(-6f, 0f)
				},
				{
					678,
					new Vector2(-8f, 0f)
				},
				{
					679,
					new Vector2(-6f, 0f)
				},
				{
					680,
					new Vector2(-10f, 0f)
				},
				{
					681,
					new Vector2(-6f, 0f)
				},
				{
					683,
					new Vector2(-6f, 0f)
				},
				{
					682,
					new Vector2(-6f, 0f)
				}
			};

			// Token: 0x04006F69 RID: 28521
			public static int[] TrailingMode = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				439, 0, 440, 0, 370, 1, 372, 1, 373, 1,
				396, 1, 400, 1, 401, 1, 473, 2, 474, 2,
				475, 2, 476, 2, 4, 3, 471, 3, 477, 3,
				479, 3, 120, 4, 137, 4, 138, 4, 94, 5,
				125, 6, 126, 6, 127, 6, 128, 6, 129, 6,
				130, 6, 131, 6, 139, 6, 140, 6, 407, 6,
				420, 6, 425, 6, 427, 6, 426, 6, 581, 6,
				516, 6, 542, 6, 543, 6, 544, 6, 545, 6,
				402, 7, 417, 7, 419, 7, 418, 7, 574, 7,
				575, 7, 519, 7, 521, 7, 522, 7, 546, 7,
				558, 7, 559, 7, 560, 7, 551, 7, 620, 7,
				657, 6, 636, 7, 677, 7, 685, 7
			});

			// Token: 0x04006F6A RID: 28522
			public static bool[] IsDragonfly = NPCID.Sets.Factory.CreateBoolSet(new int[] { 595, 596, 597, 598, 599, 600, 601 });

			// Token: 0x04006F6B RID: 28523
			public static bool[] BelongsToInvasionOldOnesArmy = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				552, 553, 554, 561, 562, 563, 555, 556, 557, 558,
				559, 560, 576, 577, 568, 569, 566, 567, 570, 571,
				572, 573, 548, 549, 564, 565, 574, 575, 551, 578
			});

			// Token: 0x04006F6C RID: 28524
			public static bool[] TeleportationImmune = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				552, 553, 554, 561, 562, 563, 555, 556, 557, 558,
				559, 560, 576, 577, 568, 569, 566, 567, 570, 571,
				572, 573, 548, 549, 564, 565, 574, 575, 551, 578
			});

			// Token: 0x04006F6D RID: 28525
			public static bool[] UsesNewTargeting = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				547, 552, 553, 554, 561, 562, 563, 555, 556, 557,
				558, 559, 560, 576, 577, 568, 569, 566, 567, 570,
				571, 572, 573, 564, 565, 574, 575, 551, 578, 210,
				211, 620, 668
			});

			// Token: 0x04006F6E RID: 28526
			public static bool[] BirdThatCanPoop = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				74, 297, 298, 442, 363, 365, 603, 609, 671, 672,
				673, 674, 675
			});

			// Token: 0x04006F6F RID: 28527
			public static bool[] CritterThatCanTurnOnPlayers = NPCID.Sets.Factory.CreateBoolSet(new int[] { 645, 639, 644, 642, 643, 641, 640, 299, 539, 538 });

			// Token: 0x04006F70 RID: 28528
			public static bool[] TakesDamageFromHostilesWithoutBeingFriendly = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				46, 55, 74, 148, 149, 230, 297, 298, 299, 303,
				355, 356, 358, 359, 360, 361, 362, 363, 364, 365,
				366, 367, 377, 357, 374, 442, 443, 444, 445, 446,
				448, 538, 539, 337, 540, 484, 485, 486, 487, 592,
				593, 595, 596, 597, 598, 599, 600, 601, 602, 603,
				604, 605, 606, 607, 608, 609, 611, 689, 612, 613,
				614, 615, 616, 617, 625, 626, 627, 639, 640, 641,
				642, 643, 644, 645, 646, 647, 648, 649, 650, 651,
				652, 653, 654, 655, 583, 584, 585, 669, 671, 672,
				673, 674, 675, 677, 687, 688
			});

			// Token: 0x04006F71 RID: 28529
			public static bool[] AllNPCs = NPCID.Sets.Factory.CreateBoolSet(true, new int[0]);

			// Token: 0x04006F72 RID: 28530
			public static bool[] HurtingBees = NPCID.Sets.Factory.CreateBoolSet(new int[] { 210, 211 });

			// Token: 0x04006F73 RID: 28531
			public static bool[] CanBeHurtByBees = NPCID.Sets.Factory.CreateBoolSet(true, new int[] { 210, 211, 222 });

			// Token: 0x04006F74 RID: 28532
			public static bool[] ConveyorBeltCollision = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				624, 85, 629, 195, 1, 147, 184, 537, 204, 16,
				59, 71, 535, 225, 676, 303, 335, 336, 333, 334,
				667, 141, 81, 121, 183, 138, 244, 304, 105, 123,
				685, 686, 687, 106, 354, 376, 579, 589, 37, 695,
				696
			});

			// Token: 0x04006F75 RID: 28533
			public static bool[] SlimeCanContainItems = NPCID.Sets.Factory.CreateBoolSet(new int[] { 1, 59, 147, 184, 537 });

			// Token: 0x04006F76 RID: 28534
			public static bool[] FighterUsesDD2PortalAppearEffect = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				552, 553, 554, 561, 562, 563, 555, 556, 557, 576,
				577, 568, 569, 570, 571, 572, 573, 564, 565
			});

			// Token: 0x04006F77 RID: 28535
			public static float[] StatueSpawnedDropRarity = NPCID.Sets.Factory.CreateCustomSet<float>(-1f, new object[]
			{
				480, 0.05f, 82, 0.05f, 86, 0.05f, 48, 0.05f, 490, 0.05f,
				489, 0.05f, 170, 0.05f, 180, 0.05f, 171, 0.05f, 167, 0.25f,
				73, 0.01f, 24, 0.05f, 481, 0.05f, 42, 0.05f, 6, 0.05f,
				2, 0.05f, 49, 0.2f, 3, 0.2f, 58, 0.2f, 21, 0.2f,
				65, 0.2f, 449, 0.2f, 482, 0.2f, 103, 0.2f, 64, 0.2f,
				63, 0.2f, 85, 0f
			});

			// Token: 0x04006F78 RID: 28536
			public static bool[] NoEarlymodeLootWhenSpawnedFromStatue = NPCID.Sets.Factory.CreateBoolSet(new int[] { 480, 82, 86, 170, 180, 171 });

			// Token: 0x04006F79 RID: 28537
			public static bool[] NeedsExpertScaling = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				25, 30, 665, 33, 112, 666, 261, 265, 371, 516,
				519, 397, 396, 398, 491
			});

			// Token: 0x04006F7A RID: 28538
			public static bool[] ProjectileNPC = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				25, 30, 665, 33, 112, 666, 261, 265, 371, 516,
				519
			});

			// Token: 0x04006F7B RID: 28539
			public static bool[] SavesAndLoads = NPCID.Sets.Factory.CreateBoolSet(new int[] { 422, 507, 517, 493 });

			// Token: 0x04006F7C RID: 28540
			public static int[] TrailCacheLength = NPCID.Sets.Factory.CreateIntSet(10, new int[]
			{
				402, 36, 519, 20, 522, 20, 620, 20, 677, 60,
				685, 10
			});

			// Token: 0x04006F7D RID: 28541
			public static bool[] SearchSpawnSlotsInReverse = NPCID.Sets.Factory.CreateBoolSet(new int[] { 222, 245 });

			// Token: 0x04006F7E RID: 28542
			public static bool[] CannotSpawnInSlot0 = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				134, 135, 136, 7, 8, 9, 10, 11, 12, 13,
				14, 15, 39, 40, 41, 87, 88, 89, 90, 91,
				92, 95, 96, 97, 98, 99, 100, 117, 118, 119,
				375, 402, 412, 413, 414, 454, 455, 456, 457, 458,
				459, 510, 511, 512, 513, 514, 515, 621, 622, 623
			});

			// Token: 0x04006F7F RID: 28543
			public static bool[] SkipUpdateInUnsyncedTiles = NPCID.Sets.Factory.CreateBoolSet(true, new int[]
			{
				135, 136, 14, 15, 8, 9, 11, 12, 40, 41,
				96, 97, 99, 100, 118, 119, 622, 623, 511, 512,
				514, 515, 455, 456, 457, 458, 459, 89, 90, 91,
				92, 413, 414
			});

			// Token: 0x04006F80 RID: 28544
			public static bool[] UsesMultiplayerProximitySyncing = NPCID.Sets.Factory.CreateBoolSet(true, new int[] { 398, 397, 396 });

			// Token: 0x04006F81 RID: 28545
			public static bool[] NoMultiplayerSmoothingByType = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				50, 657, 120, 245, 247, 248, 246, 370, 222, 398,
				397, 396, 400, 401, 668, 70, 690
			});

			// Token: 0x04006F82 RID: 28546
			public static bool[] NoMultiplayerSmoothingByAI = NPCID.Sets.Factory.CreateBoolSet(new int[] { 6, 8, 37 });

			// Token: 0x04006F83 RID: 28547
			public static bool[] MPAllowedEnemies = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				4, 13, 50, 126, 125, 134, 127, 128, 131, 129,
				130, 222, 245, 266, 370, 657, 668
			});

			// Token: 0x04006F84 RID: 28548
			public static bool[] TownCritter = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				46, 148, 149, 230, 299, 300, 303, 337, 361, 362,
				364, 366, 367, 443, 445, 447, 538, 539, 540, 583,
				584, 585, 592, 593, 602, 607, 608, 610, 616, 617,
				625, 626, 627, 639, 640, 641, 642, 643, 644, 645,
				646, 647, 648, 649, 650, 651, 652, 687, 688
			});

			// Token: 0x04006F85 RID: 28549
			public static bool[] CountsAsCritter = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				46, 303, 337, 540, 443, 74, 297, 298, 442, 611,
				689, 377, 446, 612, 613, 356, 444, 595, 596, 597,
				598, 599, 600, 601, 604, 605, 357, 448, 374, 484,
				355, 358, 606, 359, 360, 485, 486, 487, 148, 149,
				55, 230, 592, 593, 299, 538, 539, 300, 447, 361,
				445, 362, 363, 364, 365, 367, 366, 583, 584, 585,
				602, 603, 607, 608, 609, 610, 616, 617, 625, 626,
				627, 615, 639, 640, 641, 642, 643, 644, 645, 646,
				647, 648, 649, 650, 651, 652, 653, 654, 655, 661,
				669, 671, 672, 673, 674, 675, 677, 687, 688
			});

			// Token: 0x04006F86 RID: 28550
			public static bool[] HasNoPartyText = NPCID.Sets.Factory.CreateBoolSet(new int[] { 441, 453 });

			// Token: 0x04006F87 RID: 28551
			public static int[] HatOffsetY = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				227, 4, 107, 2, 108, 2, 229, 4, 17, 2,
				38, 8, 160, -10, 208, 2, 142, 2, 124, 2,
				453, 2, 37, 4, 54, 4, 209, 4, 369, 6,
				441, 6, 353, -2, 633, -2, 550, -2, 588, 2,
				663, 2, 637, 0, 638, 0, 656, 4, 670, 0,
				678, 0, 679, 0, 680, 0, 681, 0, 682, 0,
				683, 0, 684, 0
			});

			// Token: 0x04006F88 RID: 28552
			public static int[] FaceEmote = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				17, 101, 18, 102, 19, 103, 20, 104, 22, 105,
				37, 106, 38, 107, 54, 108, 107, 109, 108, 110,
				124, 111, 142, 112, 160, 113, 178, 114, 207, 115,
				208, 116, 209, 117, 227, 118, 228, 119, 229, 120,
				353, 121, 368, 122, 369, 123, 453, 124, 441, 125,
				588, 140, 633, 141, 663, 145
			});

			// Token: 0x04006F89 RID: 28553
			public static int[] ExtraFramesCount = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				17, 9, 18, 9, 19, 9, 20, 7, 22, 10,
				37, 5, 38, 9, 54, 7, 107, 9, 108, 7,
				124, 9, 142, 9, 160, 7, 178, 9, 207, 9,
				208, 9, 209, 10, 227, 9, 228, 10, 229, 10,
				353, 9, 633, 9, 368, 10, 369, 9, 453, 9,
				441, 9, 550, 9, 588, 9, 663, 7, 637, 18,
				638, 11, 656, 20, 670, 6, 678, 6, 679, 6,
				680, 6, 681, 6, 682, 6, 683, 6, 684, 6
			});

			// Token: 0x04006F8A RID: 28554
			public static int[] AttackFrameCount = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				17, 4, 18, 4, 19, 4, 20, 2, 22, 5,
				37, 0, 38, 4, 54, 2, 107, 4, 108, 2,
				124, 4, 142, 4, 160, 2, 178, 4, 207, 4,
				208, 4, 209, 5, 227, 4, 228, 5, 229, 5,
				353, 4, 633, 4, 368, 5, 369, 4, 453, 4,
				441, 4, 550, 4, 588, 4, 663, 2, 637, 0,
				638, 0, 656, 0, 670, 0, 678, 0, 679, 0,
				680, 0, 681, 0, 682, 0, 683, 0, 684, 0
			});

			// Token: 0x04006F8B RID: 28555
			public static int[] DangerDetectRange = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				38, 300, 17, 320, 107, 300, 19, 900, 22, 700,
				124, 800, 228, 800, 178, 900, 18, 300, 229, 1000,
				209, 1000, 54, 700, 108, 700, 160, 700, 20, 1200,
				369, 300, 453, 300, 368, 900, 207, 60, 227, 800,
				208, 400, 142, 500, 441, 50, 353, 60, 633, 100,
				550, 120, 588, 120, 663, 700, 638, 250, 637, 250,
				656, 250, 670, 250, 678, 250, 679, 250, 680, 250,
				681, 250, 682, 250, 683, 250, 684, 250
			});

			// Token: 0x04006F8C RID: 28556
			public static bool[] ShimmerImmunity = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				637, 638, 656, 670, 684, 678, 679, 680, 681, 682,
				683, 356, 669, 676, 244, 677, 594, 667, 662, 5,
				115, 116, 139, 245, 247, 248, 246, 249, 344, 325,
				50, 535, 657, 658, 659, 660, 668, 25, 30, 33,
				70, 72, 665, 666, 112, 516, 517, 518, 519, 520,
				521, 522, 523, 381, 382, 383, 384, 385, 386, 387,
				388, 389, 390, 391, 392, 393, 394, 395, 396, 397,
				398, 399, 400, 401, 402, 403, 404, 405, 406, 407,
				408, 409, 410, 411, 412, 413, 414, 415, 416, 417,
				418, 419, 420, 421, 423, 424, 425, 426, 427, 428,
				429, 548, 549, 551, 552, 553, 554, 555, 556, 557,
				558, 559, 560, 561, 562, 563, 564, 565, 566, 567,
				568, 569, 570, 571, 572, 573, 574, 575, 576, 577,
				578
			});

			// Token: 0x04006F8D RID: 28557
			public static int[] ShimmerTransformToItem = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				651, 182, 644, 182, 650, 178, 643, 178, 649, 179,
				642, 179, 648, 177, 641, 177, 640, 180, 647, 180,
				646, 181, 639, 181, 652, 999, 645, 999, 448, 5341
			});

			// Token: 0x04006F8E RID: 28558
			public static bool[] ShimmerTownTransform = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				22, 17, 18, 227, 207, 633, 588, 208, 369, 353,
				38, 20, 550, 19, 107, 228, 54, 124, 441, 229,
				160, 108, 178, 209, 142, 663, 37, 453, 368
			});

			// Token: 0x04006F8F RID: 28559
			public static int[] ShimmerTransformToNPC = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				3, 21, 132, 202, 186, 201, 187, 21, 188, 21,
				189, 202, 200, 203, 590, 21, 1, 676, 302, 676,
				335, 676, 336, 676, 334, 676, 333, 676, 225, 676,
				141, 676, 16, 676, 147, 676, 184, 676, 537, 676,
				204, 676, 81, 676, 183, 676, 138, 676, 121, 676,
				591, 449, 430, 449, 436, 452, 432, 450, 433, 449,
				434, 449, 435, 451, 614, 677, 74, 677, 297, 677,
				298, 677, 673, 677, 672, 677, 671, 677, 675, 677,
				674, 677, 362, 677, 363, 677, 364, 677, 365, 677,
				608, 677, 609, 677, 602, 677, 603, 677, 611, 677,
				689, 677, 148, 677, 149, 677, 46, 677, 303, 677,
				337, 677, 540, 677, 299, 677, 538, 677, 55, 677,
				607, 677, 615, 677, 625, 677, 626, 677, 688, 677,
				361, 677, 687, 677, 484, 677, 604, 677, 358, 677,
				355, 677, 616, 677, 617, 677, 654, 677, 653, 677,
				655, 677, 585, 677, 584, 677, 583, 677, 595, 677,
				596, 677, 600, 677, 597, 677, 598, 677, 599, 677,
				357, 677, 377, 677, 606, 677, 359, 677, 360, 677,
				367, 677, 366, 677, 300, 677, 610, 677, 612, 677,
				487, 677, 486, 677, 485, 677, 669, 677, 356, 677,
				661, 677, 374, 677, 442, 677, 443, 677, 444, 677,
				601, 677, 445, 677, 592, 677, 446, 677, 605, 677,
				447, 677, 627, 677, 539, 677, 613, 677
			});

			// Token: 0x04006F90 RID: 28560
			public static int[] AttackTime = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				38, 34, 17, 34, 107, 60, 19, 40, 22, 30,
				124, 34, 228, 40, 178, 24, 18, 34, 229, 60,
				209, 60, 54, 60, 108, 30, 160, 60, 20, 600,
				369, 34, 453, 34, 368, 60, 207, 15, 227, 60,
				208, 34, 142, 34, 441, 15, 353, 12, 633, 12,
				550, 34, 588, 20, 663, 60, 638, -1, 637, -1,
				656, -1, 670, -1, 678, -1, 679, -1, 680, -1,
				681, -1, 682, -1, 683, -1, 684, -1
			});

			// Token: 0x04006F91 RID: 28561
			public static int[] AttackAverageChance = NPCID.Sets.Factory.CreateIntSet(1, new int[]
			{
				38, 40, 17, 30, 107, 60, 19, 30, 22, 30,
				124, 30, 228, 50, 178, 50, 18, 60, 229, 40,
				209, 30, 54, 30, 108, 30, 160, 60, 20, 60,
				369, 50, 453, 30, 368, 40, 207, 1, 227, 30,
				208, 50, 142, 50, 441, 1, 353, 1, 633, 1,
				550, 40, 588, 20, 663, 1, 638, 1, 637, 1,
				656, 1, 670, 1, 678, 1, 679, 1, 680, 1,
				681, 1, 682, 1, 683, 1, 684, 1
			});

			// Token: 0x04006F92 RID: 28562
			public static int[] AttackType = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				38, 0, 17, 0, 107, 0, 19, 1, 22, 1,
				124, 0, 228, 1, 178, 1, 18, 0, 229, 1,
				209, 1, 54, 2, 108, 2, 160, 2, 20, 2,
				369, 0, 453, 0, 368, 1, 207, 3, 227, 1,
				208, 0, 142, 0, 441, 3, 353, 3, 633, 0,
				550, 0, 588, 0, 663, 2, 638, -1, 637, -1,
				656, -1, 670, -1, 678, -1, 679, -1, 680, -1,
				681, -1, 682, -1, 683, -1, 684, -1
			});

			// Token: 0x04006F93 RID: 28563
			public static int[] PrettySafe = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				19, 300, 22, 200, 124, 200, 228, 300, 178, 300,
				229, 300, 209, 300, 54, 100, 108, 100, 160, 100,
				20, 200, 368, 200, 227, 200
			});

			// Token: 0x04006F94 RID: 28564
			public static Color[] MagicAuraColor = NPCID.Sets.Factory.CreateCustomSet<Color>(Color.White, new object[]
			{
				54,
				new Color(100, 4, 227, 127),
				108,
				new Color(255, 80, 60, 127),
				160,
				new Color(40, 80, 255, 127),
				20,
				new Color(40, 255, 80, 127),
				663,
				Main.hslToRgb(0.92f, 1f, 0.78f, 127)
			});

			// Token: 0x04006F95 RID: 28565
			public static bool[] DemonEyes = NPCID.Sets.Factory.CreateBoolSet(new int[] { 2, 190, 192, 193, 191, 194, 317, 318 });

			// Token: 0x04006F96 RID: 28566
			public static bool[] Zombies = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				3, 132, 186, 187, 188, 189, 200, 223, 161, 254,
				255, 52, 53, 536, 319, 320, 321, 332, 436, 431,
				432, 433, 434, 435, 331, 430, 590
			});

			// Token: 0x04006F97 RID: 28567
			public static bool[] Skeletons = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				77, 449, 450, 451, 452, 481, 201, 202, 203, 21,
				324, 110, 323, 293, 291, 322, 292, 197, 167, 44,
				635
			});

			// Token: 0x04006F98 RID: 28568
			public static int[] BossHeadTextures = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				4, 0, 13, 2, 344, 3, 370, 4, 246, 5,
				249, 5, 345, 6, 50, 7, 396, 8, 395, 9,
				325, 10, 262, 11, 327, 13, 222, 14, 125, 15,
				126, 20, 346, 17, 127, 18, 35, 19, 68, 19,
				113, 22, 266, 23, 439, 24, 440, 24, 134, 25,
				491, 26, 517, 27, 422, 28, 507, 29, 493, 30,
				549, 35, 564, 32, 565, 32, 576, 33, 577, 33,
				551, 34, 548, 36, 636, 37, 657, 38, 668, 39
			});

			// Token: 0x04006F99 RID: 28569
			public static bool[] PositiveNPCTypesExcludedFromDeathTally = NPCID.Sets.Factory.CreateBoolSet(new int[] { 121, 384, 478, 479, 410, 472, 378 });

			// Token: 0x04006F9A RID: 28570
			public static bool[] ShouldBeCountedAsBossForBestiary = NPCID.Sets.Factory.CreateBoolSet(false, new int[] { 517, 422, 507, 493, 13, 664 });

			// Token: 0x04006F9B RID: 28571
			public static bool[] ShouldBeCountedAsBossForRainbowBoulders = NPCID.Sets.Factory.CreateBoolSet(false, new int[]
			{
				517, 422, 507, 493, 13, 14, 15, 267, 36, 114,
				664, 134, 135, 136, 128, 129, 130, 131, 245, 247,
				248, 246, 491, 492, 392, 395, 394, 393, 564, 565,
				576, 577, 551, 325, 327, 344, 345, 346
			});

			// Token: 0x04006F9C RID: 28572
			public static bool[] DangerThatPreventsOtherDangers = NPCID.Sets.Factory.CreateBoolSet(new int[] { 517, 422, 507, 493, 399, 13, 14, 15 });

			// Token: 0x04006F9D RID: 28573
			public static bool[] MustAlwaysDraw = NPCID.Sets.Factory.CreateBoolSet(new int[] { 113, 114, 115, 116, 126, 125 });

			// Token: 0x04006F9E RID: 28574
			public static int[] ExtraTextureCount = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				38, 1, 17, 1, 107, 0, 19, 0, 22, 0,
				124, 1, 228, 0, 178, 1, 18, 1, 229, 1,
				209, 1, 54, 1, 108, 1, 160, 0, 20, 0,
				369, 1, 453, 1, 368, 1, 207, 1, 227, 1,
				208, 0, 142, 1, 441, 1, 353, 1, 633, 1,
				550, 0, 588, 1, 633, 2, 663, 1, 638, 0,
				637, 0, 656, 0, 670, 0, 678, 0, 679, 0,
				680, 0, 681, 0, 682, 0, 683, 0, 684, 0
			});

			// Token: 0x04006F9F RID: 28575
			public static int[] NPCFramingGroup = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				18, 1, 20, 1, 208, 1, 178, 1, 124, 1,
				353, 1, 633, 1, 369, 2, 160, 3, 637, 4,
				638, 5, 656, 6, 670, 7, 678, 7, 679, 7,
				680, 7, 681, 7, 682, 7, 683, 7, 684, 7
			});

			// Token: 0x04006FA0 RID: 28576
			public static bool[] CanHitPastShimmer = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				535, 5, 13, 14, 15, 666, 267, 36, 210, 211,
				115, 116, 117, 118, 119, 658, 659, 660, 134, 135,
				136, 139, 128, 131, 129, 130, 263, 264, 246, 249,
				247, 248, 371, 372, 373, 566, 567, 440, 522, 523,
				521, 454, 455, 456, 457, 458, 459, 397, 396, 400
			});

			// Token: 0x04006FA1 RID: 28577
			public static int[][] TownNPCsFramingGroups = new int[][]
			{
				new int[]
				{
					0, 0, 0, -2, -2, -2, 0, 0, 0, 0,
					-2, -2, -2, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0
				},
				new int[]
				{
					0, 0, 0, -2, -2, -2, 0, 0, 0, -2,
					-2, -2, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0
				},
				new int[]
				{
					0, 0, 0, -2, -2, -2, 0, 0, -2, -2,
					-2, -2, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0
				},
				new int[]
				{
					0, 0, -2, 0, 0, 0, 0, -2, -2, -2,
					0, 0, 0, 0, -2, -2, 0, 0, 0, 0,
					0, 0
				},
				new int[]
				{
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					2, 2, 4, 6, 4, 2, 2, -2, -4, -6,
					-4, -2, -4, -4, -6, -6, -6, -4
				},
				new int[]
				{
					0, 0, 0, 0, 0, 0, 0, 0, -2, -2,
					-2, 0, 0, -2, -2, 0, 0, 4, 6, 6,
					6, 6, 4, 4, 4, 4, 4, 4
				},
				new int[]
				{
					0, 0, -2, -4, -4, -2, 0, -2, 0, 0,
					2, 4, 6, 4, 2, 0, -2, -4, -6, -6,
					-6, -6, -6, -6, -4, -2
				},
				new int[]
				{
					0, -2, 0, -2, -4, -6, -4, -2, 0, 0,
					2, 2, 4, 2
				}
			};

			// Token: 0x02000AA5 RID: 2725
			public struct NPCBestiaryDrawModifiers
			{
				// Token: 0x06004C03 RID: 19459 RVA: 0x006DA098 File Offset: 0x006D8298
				public NPCBestiaryDrawModifiers(int seriouslyWhyCantStructsHaveParameterlessConstructors)
				{
					this.Position = default(Vector2);
					this.Rotation = 0f;
					this.Scale = 1f;
					this.PortraitScale = new float?((float)1);
					this.Hide = false;
					this.IsWet = false;
					this.Frame = null;
					this.Direction = null;
					this.SpriteDirection = null;
					this.Velocity = 0f;
					this.PortraitPositionXOverride = null;
					this.PortraitPositionYOverride = null;
					this.CustomTexturePath = null;
				}

				// Token: 0x0400781F RID: 30751
				public Vector2 Position;

				// Token: 0x04007820 RID: 30752
				public float? PortraitPositionXOverride;

				// Token: 0x04007821 RID: 30753
				public float? PortraitPositionYOverride;

				// Token: 0x04007822 RID: 30754
				public float Rotation;

				// Token: 0x04007823 RID: 30755
				public float Scale;

				// Token: 0x04007824 RID: 30756
				public float? PortraitScale;

				// Token: 0x04007825 RID: 30757
				public bool Hide;

				// Token: 0x04007826 RID: 30758
				public bool IsWet;

				// Token: 0x04007827 RID: 30759
				public int? Frame;

				// Token: 0x04007828 RID: 30760
				public int? Direction;

				// Token: 0x04007829 RID: 30761
				public int? SpriteDirection;

				// Token: 0x0400782A RID: 30762
				public float Velocity;

				// Token: 0x0400782B RID: 30763
				public string CustomTexturePath;
			}

			// Token: 0x02000AA6 RID: 2726
			public interface NPCPortraitProvider
			{
				// Token: 0x06004C04 RID: 19460
				void GetDrawData(out Texture2D texture, out Rectangle drawFrame);
			}

			// Token: 0x02000AA7 RID: 2727
			public class BasicNPCPortrait : NPCID.Sets.NPCPortraitProvider
			{
				// Token: 0x06004C05 RID: 19461 RVA: 0x006DA130 File Offset: 0x006D8330
				public BasicNPCPortrait(string texturePath)
				{
					this.TexturePath = texturePath;
					this.HorizontalFrames = (this.VerticalFrames = 1);
					this.PaddingX = (this.PaddingY = 0);
					this.FrameX = (this.FrameY = 0);
				}

				// Token: 0x06004C06 RID: 19462 RVA: 0x006DA17C File Offset: 0x006D837C
				public virtual void GetDrawData(out Texture2D texture, out Rectangle drawFrame)
				{
					if (this._image == null)
					{
						this._image = Main.Assets.Request<Texture2D>(this.TexturePath, 1);
					}
					texture = null;
					if (this._image.IsLoaded)
					{
						texture = this._image.Value;
					}
					drawFrame = texture.Frame(this.HorizontalFrames, this.VerticalFrames, this.FrameX, this.FrameY, this.PaddingX, this.PaddingY);
				}

				// Token: 0x0400782C RID: 30764
				public string TexturePath;

				// Token: 0x0400782D RID: 30765
				public int HorizontalFrames;

				// Token: 0x0400782E RID: 30766
				public int VerticalFrames;

				// Token: 0x0400782F RID: 30767
				public int PaddingX;

				// Token: 0x04007830 RID: 30768
				public int PaddingY;

				// Token: 0x04007831 RID: 30769
				public int FrameX;

				// Token: 0x04007832 RID: 30770
				public int FrameY;

				// Token: 0x04007833 RID: 30771
				private Asset<Texture2D> _image;
			}

			// Token: 0x02000AA8 RID: 2728
			public class NPCPortraitSelector : NPCID.Sets.NPCPortraitProvider
			{
				// Token: 0x06004C07 RID: 19463 RVA: 0x006DA1F8 File Offset: 0x006D83F8
				public NPCID.Sets.NPCPortraitSelector With(NPCID.Sets.NPCPortraitSelector.SelectionCondition condition, NPCID.Sets.NPCPortraitProvider portrait)
				{
					this._entries.Add(new NPCID.Sets.NPCPortraitSelector.Entry
					{
						Condition = condition,
						Portrait = portrait
					});
					return this;
				}

				// Token: 0x06004C08 RID: 19464 RVA: 0x006DA22C File Offset: 0x006D842C
				public NPCID.Sets.NPCPortraitSelector Default(NPCID.Sets.NPCPortraitProvider portrait)
				{
					List<NPCID.Sets.NPCPortraitSelector.Entry> entries = this._entries;
					NPCID.Sets.NPCPortraitSelector.Entry entry = default(NPCID.Sets.NPCPortraitSelector.Entry);
					entry.Condition = () => true;
					entry.Portrait = portrait;
					entries.Add(entry);
					return this;
				}

				// Token: 0x06004C09 RID: 19465 RVA: 0x006DA27C File Offset: 0x006D847C
				public void GetDrawData(out Texture2D texture, out Rectangle drawFrame)
				{
					texture = null;
					drawFrame = default(Rectangle);
					NPCID.Sets.NPCPortraitProvider firstValidatedEntry = this.GetFirstValidatedEntry();
					if (firstValidatedEntry != null)
					{
						firstValidatedEntry.GetDrawData(out texture, out drawFrame);
					}
				}

				// Token: 0x06004C0A RID: 19466 RVA: 0x006DA2A8 File Offset: 0x006D84A8
				private NPCID.Sets.NPCPortraitProvider GetFirstValidatedEntry()
				{
					foreach (NPCID.Sets.NPCPortraitSelector.Entry entry in this._entries)
					{
						if (entry.Condition())
						{
							return entry.Portrait;
						}
					}
					return null;
				}

				// Token: 0x06004C0B RID: 19467 RVA: 0x006DA310 File Offset: 0x006D8510
				public NPCPortraitSelector()
				{
				}

				// Token: 0x04007834 RID: 30772
				private List<NPCID.Sets.NPCPortraitSelector.Entry> _entries = new List<NPCID.Sets.NPCPortraitSelector.Entry>();

				// Token: 0x02000B16 RID: 2838
				// (Invoke) Token: 0x06004DBA RID: 19898
				public delegate bool SelectionCondition();

				// Token: 0x02000B17 RID: 2839
				public struct Entry
				{
					// Token: 0x0400791D RID: 31005
					public NPCID.Sets.NPCPortraitSelector.SelectionCondition Condition;

					// Token: 0x0400791E RID: 31006
					public NPCID.Sets.NPCPortraitProvider Portrait;
				}

				// Token: 0x02000B18 RID: 2840
				[CompilerGenerated]
				[Serializable]
				private sealed class <>c
				{
					// Token: 0x06004DBD RID: 19901 RVA: 0x006DC915 File Offset: 0x006DAB15
					// Note: this type is marked as 'beforefieldinit'.
					static <>c()
					{
					}

					// Token: 0x06004DBE RID: 19902 RVA: 0x0000357B File Offset: 0x0000177B
					public <>c()
					{
					}

					// Token: 0x06004DBF RID: 19903 RVA: 0x000379E9 File Offset: 0x00035BE9
					internal bool <Default>b__4_0()
					{
						return true;
					}

					// Token: 0x0400791F RID: 31007
					public static readonly NPCID.Sets.NPCPortraitSelector.<>c <>9 = new NPCID.Sets.NPCPortraitSelector.<>c();

					// Token: 0x04007920 RID: 31008
					public static NPCID.Sets.NPCPortraitSelector.SelectionCondition <>9__4_0;
				}
			}

			// Token: 0x02000AA9 RID: 2729
			public class NPCVariantChecker
			{
				// Token: 0x06004C0C RID: 19468 RVA: 0x006DA323 File Offset: 0x006D8523
				public NPCVariantChecker(int variantToCheck)
				{
					this.VariantToCheck = variantToCheck;
				}

				// Token: 0x06004C0D RID: 19469 RVA: 0x006DA334 File Offset: 0x006D8534
				public bool Fits()
				{
					int talkNPC = Main.LocalPlayer.talkNPC;
					return talkNPC >= 0 && talkNPC < Main.maxNPCs && Main.npc[talkNPC].townNpcVariationIndex == this.VariantToCheck;
				}

				// Token: 0x04007835 RID: 30773
				public int VariantToCheck;
			}

			// Token: 0x02000AAA RID: 2730
			private static class LocalBuffID
			{
				// Token: 0x04007836 RID: 30774
				public const int Confused = 31;

				// Token: 0x04007837 RID: 30775
				public const int Poisoned = 20;

				// Token: 0x04007838 RID: 30776
				public const int OnFire = 24;

				// Token: 0x04007839 RID: 30777
				public const int OnFire3 = 323;

				// Token: 0x0400783A RID: 30778
				public const int ShadowFlame = 153;

				// Token: 0x0400783B RID: 30779
				public const int Daybreak = 189;

				// Token: 0x0400783C RID: 30780
				public const int Frostburn = 44;

				// Token: 0x0400783D RID: 30781
				public const int Frostburn2 = 324;

				// Token: 0x0400783E RID: 30782
				public const int CursedInferno = 39;

				// Token: 0x0400783F RID: 30783
				public const int BetsysCurse = 203;

				// Token: 0x04007840 RID: 30784
				public const int Ichor = 69;

				// Token: 0x04007841 RID: 30785
				public const int Venom = 70;

				// Token: 0x04007842 RID: 30786
				public const int Oiled = 204;

				// Token: 0x04007843 RID: 30787
				public const int BoneJavelin = 169;

				// Token: 0x04007844 RID: 30788
				public const int TentacleSpike = 337;

				// Token: 0x04007845 RID: 30789
				public const int BloodButcherer = 344;

				// Token: 0x04007846 RID: 30790
				public const int Bleeding = 30;

				// Token: 0x04007847 RID: 30791
				public const int Hemorrhage = 375;
			}

			// Token: 0x02000AAB RID: 2731
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004C0E RID: 19470 RVA: 0x006DA36E File Offset: 0x006D856E
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004C0F RID: 19471 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004C10 RID: 19472 RVA: 0x006DA37A File Offset: 0x006D857A
				internal bool <.cctor>b__94_0()
				{
					return NPCID.Sets.ShimmeredPortraitCondition() && !NPC.ShouldBestiaryGirlBeLycantrope();
				}

				// Token: 0x06004C11 RID: 19473 RVA: 0x006DA38D File Offset: 0x006D858D
				internal bool <.cctor>b__94_1()
				{
					return NPCID.Sets.ShimmeredPortraitCondition() && NPC.ShouldBestiaryGirlBeLycantrope();
				}

				// Token: 0x06004C12 RID: 19474 RVA: 0x006DA39D File Offset: 0x006D859D
				internal bool <.cctor>b__94_2()
				{
					return !NPCID.Sets.ShimmeredPortraitCondition() && NPC.ShouldBestiaryGirlBeLycantrope();
				}

				// Token: 0x04007848 RID: 30792
				public static readonly NPCID.Sets.<>c <>9 = new NPCID.Sets.<>c();
			}
		}
	}
}

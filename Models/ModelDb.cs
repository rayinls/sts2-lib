using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Acts;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Models.Exceptions;
using MegaCrit.Sts2.Core.Models.Modifiers;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x0200049A RID: 1178
	[NullableContext(1)]
	[Nullable(0)]
	public static class ModelDb
	{
		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x060047B7 RID: 18359 RVA: 0x00201D08 File Offset: 0x001FFF08
		public static Type[] AllAbstractModelSubtypes
		{
			get
			{
				List<Type> list = new List<Type>();
				list.AddRange(AbstractModelSubtypes.All);
				list.AddRange(ReflectionHelper.GetSubtypesInMods<AbstractModel>());
				return list.ToArray();
			}
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x00201D2C File Offset: 0x001FFF2C
		public static void Init()
		{
			foreach (Type type in ModelDb.AllAbstractModelSubtypes)
			{
				ModelId id = ModelDb.GetId(type);
				AbstractModel abstractModel = (AbstractModel)Activator.CreateInstance(type);
				ModelDb._contentById[id] = abstractModel;
			}
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x00201D74 File Offset: 0x001FFF74
		public static void Inject([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type type)
		{
			if (ModelDb.Contains(type))
			{
				return;
			}
			ModelId id = ModelDb.GetId(type);
			AbstractModel abstractModel = (AbstractModel)Activator.CreateInstance(type);
			ModelDb._contentById[id] = abstractModel;
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x00201DAC File Offset: 0x001FFFAC
		public static void Remove(Type type)
		{
			ModelId id = ModelDb.GetId(type);
			ModelDb._contentById.Remove(id);
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x00201DCC File Offset: 0x001FFFCC
		public static void InitIds()
		{
			foreach (KeyValuePair<ModelId, AbstractModel> keyValuePair in ModelDb._contentById)
			{
				keyValuePair.Value.InitId(keyValuePair.Key);
			}
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x00201E2C File Offset: 0x0020002C
		public static void Preload()
		{
			IEnumerable<CardModel> allCards = ModelDb.AllCards;
			IEnumerable<CardPoolModel> allCharacterCardPools = ModelDb.AllCharacterCardPools;
			IEnumerable<EventModel> allSharedEvents = ModelDb.AllSharedEvents;
			IEnumerable<EventModel> allEvents = ModelDb.AllEvents;
			IEnumerable<RelicModel> allRelics = ModelDb.AllRelics;
			IEnumerable<PotionModel> allPotions = ModelDb.AllPotions;
			IEnumerable<EncounterModel> allEncounters = ModelDb.AllEncounters;
			IReadOnlyList<AchievementModel> achievements = ModelDb.Achievements;
			foreach (CardModel cardModel in ModelDb.AllCards)
			{
				CardPoolModel pool = cardModel.Pool;
				IEnumerable<string> allPortraitPaths = cardModel.AllPortraitPaths;
			}
			foreach (RelicModel relicModel in ModelDb.AllRelics)
			{
				string iconPath = relicModel.IconPath;
			}
			foreach (PowerModel powerModel in ModelDb.AllPowers)
			{
				string iconPath2 = powerModel.IconPath;
				string resolvedBigIconPath = powerModel.ResolvedBigIconPath;
			}
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x00201F34 File Offset: 0x00200134
		public static ModelId GetId<[Nullable(0)] T>() where T : AbstractModel
		{
			return ModelDb.GetId(typeof(T));
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x00201F45 File Offset: 0x00200145
		public static ModelId GetId(Type type)
		{
			return new ModelId(ModelDb.GetCategory(type), ModelDb.GetEntry(type));
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x00201F58 File Offset: 0x00200158
		public static Type GetCategoryType(Type type)
		{
			Type type2 = type;
			while (type2.BaseType != typeof(AbstractModel))
			{
				type2 = type2.BaseType;
			}
			return type2;
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x00201F88 File Offset: 0x00200188
		public static string GetCategory(Type type)
		{
			return ModelId.SlugifyCategory(ModelDb.GetCategoryType(type).Name);
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x00201F9A File Offset: 0x0020019A
		public static string GetEntry(Type type)
		{
			return StringHelper.Slugify(type.Name);
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x00201FA8 File Offset: 0x002001A8
		[return: Nullable(2)]
		public static T GetByIdOrNull<[Nullable(0)] T>(ModelId id) where T : AbstractModel
		{
			AbstractModel abstractModel;
			if (ModelDb._contentById.TryGetValue(id, out abstractModel))
			{
				return (T)((object)abstractModel);
			}
			return default(T);
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x00201FD4 File Offset: 0x002001D4
		public static T GetById<[Nullable(0)] T>(ModelId id) where T : AbstractModel
		{
			T byIdOrNull = ModelDb.GetByIdOrNull<T>(id);
			T t = byIdOrNull;
			if (t == null)
			{
				throw new ModelNotFoundException(id);
			}
			return t;
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x00201FF9 File Offset: 0x002001F9
		public static bool Contains(Type type)
		{
			return ModelDb._contentById.ContainsKey(ModelDb.GetId(type));
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x0020200B File Offset: 0x0020020B
		private static T Get<[Nullable(0)] T>() where T : AbstractModel
		{
			return (T)((object)ModelDb._contentById[ModelDb.GetId<T>()]);
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x00202024 File Offset: 0x00200224
		private static AbstractModel Get(Type type)
		{
			if (!type.IsSubclassOf(typeof(AbstractModel)))
			{
				throw new InvalidOperationException();
			}
			ModelId id = ModelDb.GetId(type);
			AbstractModel abstractModel;
			if (ModelDb._contentById.TryGetValue(id, out abstractModel))
			{
				return abstractModel;
			}
			throw new ModelNotFoundException(id);
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x00202067 File Offset: 0x00200267
		public static T Affliction<[Nullable(0)] T>() where T : AfflictionModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x060047C8 RID: 18376 RVA: 0x00202070 File Offset: 0x00200270
		public static IEnumerable<AfflictionModel> DebugAfflictions
		{
			get
			{
				return from t in ModelDb.AllAbstractModelSubtypes
					where t.IsSubclassOf(typeof(AfflictionModel))
					select (AfflictionModel)ModelDb.Get(t);
			}
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x002020CA File Offset: 0x002002CA
		public static T Enchantment<[Nullable(0)] T>() where T : EnchantmentModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x060047CA RID: 18378 RVA: 0x002020D4 File Offset: 0x002002D4
		public static IEnumerable<EnchantmentModel> DebugEnchantments
		{
			get
			{
				return from t in ModelDb.AllAbstractModelSubtypes
					where t.IsSubclassOf(typeof(EnchantmentModel))
					select (EnchantmentModel)ModelDb.Get(t);
			}
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x0020212E File Offset: 0x0020032E
		public static T Card<[Nullable(0)] T>() where T : CardModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x060047CC RID: 18380 RVA: 0x00202138 File Offset: 0x00200338
		public static IEnumerable<CardModel> AllCards
		{
			get
			{
				IEnumerable<CardModel> enumerable;
				if ((enumerable = ModelDb._allCards) == null)
				{
					IEnumerable<CardModel> enumerable2 = (ModelDb._allCards = ModelDb.AllCardPools.SelectMany((CardPoolModel p) => p.AllCards).Concat(ModelDb.AllCharacters.SelectMany((CharacterModel c) => c.StartingDeck).Distinct<CardModel>()).Distinct<CardModel>());
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x002021B7 File Offset: 0x002003B7
		public static T CardPool<[Nullable(0)] T>() where T : CardPoolModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x060047CE RID: 18382 RVA: 0x002021C0 File Offset: 0x002003C0
		public static IEnumerable<CardPoolModel> AllCardPools
		{
			get
			{
				IEnumerable<CardPoolModel> enumerable;
				if ((enumerable = ModelDb._allCardPools) == null)
				{
					IEnumerable<CardPoolModel> enumerable2 = (ModelDb._allCardPools = ModelDb.AllCharacterCardPools.Concat(ModelDb.AllSharedCardPools).Distinct<CardPoolModel>());
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x060047CF RID: 18383 RVA: 0x002021F4 File Offset: 0x002003F4
		public static IEnumerable<CardPoolModel> AllSharedCardPools
		{
			get
			{
				return new <>z__ReadOnlyArray<CardPoolModel>(new CardPoolModel[]
				{
					ModelDb.CardPool<ColorlessCardPool>(),
					ModelDb.CardPool<CurseCardPool>(),
					ModelDb.CardPool<DeprecatedCardPool>(),
					ModelDb.CardPool<EventCardPool>(),
					ModelDb.CardPool<QuestCardPool>(),
					ModelDb.CardPool<StatusCardPool>(),
					ModelDb.CardPool<TokenCardPool>()
				});
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x060047D0 RID: 18384 RVA: 0x00202244 File Offset: 0x00200444
		public static IEnumerable<CardPoolModel> AllCharacterCardPools
		{
			get
			{
				IEnumerable<CardPoolModel> enumerable;
				if ((enumerable = ModelDb._allCharacterCardPools) == null)
				{
					IEnumerable<CardPoolModel> enumerable2 = (ModelDb._allCharacterCardPools = ModelDb.AllCharacters.Select((CharacterModel c) => c.CardPool));
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x0020228B File Offset: 0x0020048B
		public static T Character<[Nullable(0)] T>() where T : CharacterModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x060047D2 RID: 18386 RVA: 0x00202292 File Offset: 0x00200492
		public static IEnumerable<CharacterModel> AllCharacters
		{
			get
			{
				return new <>z__ReadOnlyArray<CharacterModel>(new CharacterModel[]
				{
					ModelDb.Character<Ironclad>(),
					ModelDb.Character<Silent>(),
					ModelDb.Character<Regent>(),
					ModelDb.Character<Necrobinder>(),
					ModelDb.Character<Defect>()
				});
			}
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x002022C7 File Offset: 0x002004C7
		public static T Event<[Nullable(0)] T>() where T : EventModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x060047D4 RID: 18388 RVA: 0x002022D0 File Offset: 0x002004D0
		public static IEnumerable<EventModel> AllSharedEvents
		{
			get
			{
				IEnumerable<EventModel> enumerable;
				if ((enumerable = ModelDb._allSharedEvents) == null)
				{
					enumerable = (ModelDb._allSharedEvents = new <>z__ReadOnlyArray<EventModel>(new EventModel[]
					{
						ModelDb.Event<BrainLeech>(),
						ModelDb.Event<CrystalSphere>(),
						ModelDb.Event<DollRoom>(),
						ModelDb.Event<FakeMerchant>(),
						ModelDb.Event<PotionCourier>(),
						ModelDb.Event<RanwidTheElder>(),
						ModelDb.Event<RelicTrader>(),
						ModelDb.Event<RoomFullOfCheese>(),
						ModelDb.Event<SelfHelpBook>(),
						ModelDb.Event<SlipperyBridge>(),
						ModelDb.Event<StoneOfAllTime>(),
						ModelDb.Event<Symbiote>(),
						ModelDb.Event<TeaMaster>(),
						ModelDb.Event<TheFutureOfPotions>(),
						ModelDb.Event<TheLegendsWereTrue>(),
						ModelDb.Event<ThisOrThat>(),
						ModelDb.Event<WarHistorianRepy>(),
						ModelDb.Event<WelcomeToWongos>()
					}));
				}
				return enumerable;
			}
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x00202394 File Offset: 0x00200594
		public static T AncientEvent<[Nullable(0)] T>() where T : AncientEventModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x060047D6 RID: 18390 RVA: 0x0020239B File Offset: 0x0020059B
		public static IEnumerable<AncientEventModel> AllAncients
		{
			get
			{
				return ModelDb.Acts.SelectMany((ActModel a) => a.AllAncients).Concat(ModelDb.AllSharedAncients).Distinct<AncientEventModel>();
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x060047D7 RID: 18391 RVA: 0x002023D5 File Offset: 0x002005D5
		public static IEnumerable<AncientEventModel> AllSharedAncients
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<AncientEventModel>(ModelDb.AncientEvent<Darv>());
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x060047D8 RID: 18392 RVA: 0x002023E4 File Offset: 0x002005E4
		public static IEnumerable<EventModel> AllEvents
		{
			get
			{
				IEnumerable<EventModel> enumerable;
				if ((enumerable = ModelDb._allEvents) == null)
				{
					IEnumerable<EventModel> enumerable2 = (ModelDb._allEvents = ModelDb.Acts.SelectMany((ActModel a) => a.AllEvents).Concat(ModelDb.AllSharedEvents).Distinct<EventModel>());
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x0020243A File Offset: 0x0020063A
		public static T Monster<[Nullable(0)] T>() where T : MonsterModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x060047DA RID: 18394 RVA: 0x00202441 File Offset: 0x00200641
		public static IEnumerable<MonsterModel> Monsters
		{
			get
			{
				return ModelDb.Acts.SelectMany((ActModel act) => act.AllMonsters).Distinct<MonsterModel>();
			}
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x00202471 File Offset: 0x00200671
		public static T Encounter<[Nullable(0)] T>() where T : EncounterModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x060047DC RID: 18396 RVA: 0x00202478 File Offset: 0x00200678
		public static IEnumerable<EncounterModel> AllEncounters
		{
			get
			{
				IEnumerable<EncounterModel> enumerable;
				if ((enumerable = ModelDb._allEncounters) == null)
				{
					IEnumerable<EncounterModel> enumerable2 = (ModelDb._allEncounters = ModelDb.Acts.SelectMany((ActModel a) => a.AllEncounters).Distinct<EncounterModel>());
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x002024C4 File Offset: 0x002006C4
		public static T Potion<[Nullable(0)] T>() where T : PotionModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060047DE RID: 18398 RVA: 0x002024CC File Offset: 0x002006CC
		public static IEnumerable<PotionModel> AllPotions
		{
			get
			{
				IEnumerable<PotionModel> enumerable;
				if ((enumerable = ModelDb._allPotions) == null)
				{
					enumerable = (ModelDb._allPotions = from p in ModelDb.AllPotionPools.SelectMany((PotionPoolModel p) => p.AllPotions).Distinct<PotionModel>()
						orderby p.Id.Entry
						select p);
				}
				return enumerable;
			}
		}

		// Token: 0x060047DF RID: 18399 RVA: 0x0020253A File Offset: 0x0020073A
		public static T PotionPool<[Nullable(0)] T>() where T : PotionPoolModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x060047E0 RID: 18400 RVA: 0x00202544 File Offset: 0x00200744
		public static IEnumerable<PotionPoolModel> AllPotionPools
		{
			get
			{
				IEnumerable<PotionPoolModel> enumerable;
				if ((enumerable = ModelDb._allPotionPools) == null)
				{
					IEnumerable<PotionPoolModel> enumerable2 = (ModelDb._allPotionPools = ModelDb.AllCharacterPotionPools.Concat(ModelDb.AllSharedPotionPools).Distinct<PotionPoolModel>());
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060047E1 RID: 18401 RVA: 0x00202578 File Offset: 0x00200778
		public static IEnumerable<PotionPoolModel> AllCharacterPotionPools
		{
			get
			{
				IEnumerable<PotionPoolModel> enumerable;
				if ((enumerable = ModelDb._allCharacterPotionPools) == null)
				{
					IEnumerable<PotionPoolModel> enumerable2 = (ModelDb._allCharacterPotionPools = ModelDb.AllCharacters.Select((CharacterModel c) => c.PotionPool));
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060047E2 RID: 18402 RVA: 0x002025C0 File Offset: 0x002007C0
		public static IEnumerable<RelicPoolModel> AllCharacterRelicPools
		{
			get
			{
				IEnumerable<RelicPoolModel> enumerable;
				if ((enumerable = ModelDb._allCharacterRelicPools) == null)
				{
					IEnumerable<RelicPoolModel> enumerable2 = (ModelDb._allCharacterRelicPools = ModelDb.AllCharacters.Select((CharacterModel c) => c.RelicPool));
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x00202607 File Offset: 0x00200807
		private static IEnumerable<PotionPoolModel> AllSharedPotionPools
		{
			get
			{
				IEnumerable<PotionPoolModel> enumerable;
				if ((enumerable = ModelDb._allSharedPotionPools) == null)
				{
					enumerable = (ModelDb._allSharedPotionPools = new <>z__ReadOnlyArray<PotionPoolModel>(new PotionPoolModel[]
					{
						ModelDb.PotionPool<DeprecatedPotionPool>(),
						ModelDb.PotionPool<EventPotionPool>(),
						ModelDb.PotionPool<SharedPotionPool>(),
						ModelDb.PotionPool<TokenPotionPool>()
					}));
				}
				return enumerable;
			}
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x00202643 File Offset: 0x00200843
		public static T Power<[Nullable(0)] T>() where T : PowerModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060047E5 RID: 18405 RVA: 0x0020264C File Offset: 0x0020084C
		public static IEnumerable<PowerModel> AllPowers
		{
			get
			{
				IEnumerable<PowerModel> enumerable;
				if ((enumerable = ModelDb._allPowers) == null)
				{
					IEnumerable<PowerModel> enumerable2 = (ModelDb._allPowers = from t in ModelDb.AllAbstractModelSubtypes
						where t.IsSubclassOf(typeof(PowerModel))
						select (PowerModel)ModelDb.Get(t));
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x002026B7 File Offset: 0x002008B7
		public static PowerModel DebugPower(Type type)
		{
			return (PowerModel)ModelDb.Get(type);
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x002026C4 File Offset: 0x002008C4
		public static T Relic<[Nullable(0)] T>() where T : RelicModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060047E8 RID: 18408 RVA: 0x002026CC File Offset: 0x002008CC
		public static IEnumerable<RelicModel> AllRelics
		{
			get
			{
				IEnumerable<RelicModel> enumerable;
				if ((enumerable = ModelDb._allRelics) == null)
				{
					enumerable = (ModelDb._allRelics = from r in ModelDb.AllRelicPools.SelectMany((RelicPoolModel p) => p.AllRelics).Concat(ModelDb.AllCharacters.SelectMany((CharacterModel c) => c.StartingRelics)).Distinct<RelicModel>()
						orderby r.Id.Entry
						select r);
				}
				return enumerable;
			}
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x0020276B File Offset: 0x0020096B
		public static T RelicPool<[Nullable(0)] T>() where T : RelicPoolModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x060047EA RID: 18410 RVA: 0x00202772 File Offset: 0x00200972
		public static IEnumerable<RelicPoolModel> AllRelicPools
		{
			get
			{
				return ModelDb.CharacterRelicPools.Concat(ModelDb.AllSharedRelicPools).Distinct<RelicPoolModel>();
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060047EB RID: 18411 RVA: 0x00202788 File Offset: 0x00200988
		public static IEnumerable<RelicPoolModel> CharacterRelicPools
		{
			get
			{
				return ModelDb.AllCharacters.Select((CharacterModel c) => c.RelicPool);
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060047EC RID: 18412 RVA: 0x002027B3 File Offset: 0x002009B3
		private static IEnumerable<RelicPoolModel> AllSharedRelicPools
		{
			get
			{
				return new <>z__ReadOnlyArray<RelicPoolModel>(new RelicPoolModel[]
				{
					ModelDb.RelicPool<DeprecatedRelicPool>(),
					ModelDb.RelicPool<EventRelicPool>(),
					ModelDb.RelicPool<FallbackRelicPool>(),
					ModelDb.RelicPool<SharedRelicPool>()
				});
			}
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x002027E0 File Offset: 0x002009E0
		public static T Orb<[Nullable(0)] T>() where T : OrbModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x002027E8 File Offset: 0x002009E8
		[return: Nullable(2)]
		public static OrbModel DebugOrb(Type type)
		{
			OrbModel orbModel;
			try
			{
				orbModel = (OrbModel)ModelDb.Get(type);
			}
			catch
			{
				orbModel = null;
			}
			return orbModel;
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x0020281C File Offset: 0x00200A1C
		public static IEnumerable<OrbModel> Orbs
		{
			get
			{
				return new <>z__ReadOnlyArray<OrbModel>(new OrbModel[]
				{
					ModelDb.Orb<LightningOrb>(),
					ModelDb.Orb<FrostOrb>(),
					ModelDb.Orb<DarkOrb>(),
					ModelDb.Orb<PlasmaOrb>()
				});
			}
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x00202849 File Offset: 0x00200A49
		public static T Act<[Nullable(0)] T>() where T : ActModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x00202850 File Offset: 0x00200A50
		public static IEnumerable<ActModel> Acts
		{
			get
			{
				return new <>z__ReadOnlyArray<ActModel>(new ActModel[]
				{
					ModelDb.Act<Overgrowth>(),
					ModelDb.Act<Hive>(),
					ModelDb.Act<Glory>(),
					ModelDb.Act<Underdocks>()
				});
			}
		}

		// Token: 0x060047F2 RID: 18418 RVA: 0x0020287D File Offset: 0x00200A7D
		public static T Singleton<[Nullable(0)] T>() where T : SingletonModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x060047F3 RID: 18419 RVA: 0x00202884 File Offset: 0x00200A84
		public static T Achievement<[Nullable(0)] T>() where T : AchievementModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060047F4 RID: 18420 RVA: 0x0020288C File Offset: 0x00200A8C
		public static IReadOnlyList<AchievementModel> Achievements
		{
			get
			{
				if (ModelDb._achievements == null)
				{
					ModelDb._achievements = new List<AchievementModel>();
					foreach (Type type in ModelDb.AllAbstractModelSubtypes)
					{
						if (type.IsSubclassOf(typeof(AchievementModel)))
						{
							ModelDb._achievements.Add((AchievementModel)ModelDb.Get(type));
						}
					}
				}
				return ModelDb._achievements;
			}
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x002028EE File Offset: 0x00200AEE
		public static T Modifier<[Nullable(0)] T>() where T : ModifierModel
		{
			return ModelDb.Get<T>();
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060047F6 RID: 18422 RVA: 0x002028F8 File Offset: 0x00200AF8
		public static IReadOnlyList<ModifierModel> GoodModifiers
		{
			get
			{
				return new <>z__ReadOnlyArray<ModifierModel>(new ModifierModel[]
				{
					ModelDb.Modifier<Draft>(),
					ModelDb.Modifier<SealedDeck>(),
					ModelDb.Modifier<Hoarder>(),
					ModelDb.Modifier<Specialized>(),
					ModelDb.Modifier<Insanity>(),
					ModelDb.Modifier<AllStar>(),
					ModelDb.Modifier<Flight>(),
					ModelDb.Modifier<Vintage>(),
					ModelDb.Modifier<CharacterCards>()
				});
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060047F7 RID: 18423 RVA: 0x0020295C File Offset: 0x00200B5C
		public static IReadOnlyList<ModifierModel> BadModifiers
		{
			get
			{
				return new <>z__ReadOnlyArray<ModifierModel>(new ModifierModel[]
				{
					ModelDb.Modifier<DeadlyEvents>(),
					ModelDb.Modifier<CursedRun>(),
					ModelDb.Modifier<BigGameHunter>(),
					ModelDb.Modifier<Midas>(),
					ModelDb.Modifier<Murderous>(),
					ModelDb.Modifier<NightTerrors>(),
					ModelDb.Modifier<Terminal>()
				});
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060047F8 RID: 18424 RVA: 0x002029AC File Offset: 0x00200BAC
		public static IReadOnlyList<IReadOnlySet<ModifierModel>> MutuallyExclusiveModifiers
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IReadOnlySet<ModifierModel>>(new HashSet<ModifierModel>
				{
					ModelDb.Modifier<SealedDeck>(),
					ModelDb.Modifier<Draft>(),
					ModelDb.Modifier<Insanity>()
				});
			}
		}

		// Token: 0x04001AE8 RID: 6888
		private const int _initialCapacity = 4096;

		// Token: 0x04001AE9 RID: 6889
		private static readonly Dictionary<ModelId, AbstractModel> _contentById = new Dictionary<ModelId, AbstractModel>(4096);

		// Token: 0x04001AEA RID: 6890
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<CardModel> _allCards;

		// Token: 0x04001AEB RID: 6891
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<CardPoolModel> _allCardPools;

		// Token: 0x04001AEC RID: 6892
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<CardPoolModel> _allCharacterCardPools;

		// Token: 0x04001AED RID: 6893
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<EventModel> _allSharedEvents;

		// Token: 0x04001AEE RID: 6894
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<EventModel> _allEvents;

		// Token: 0x04001AEF RID: 6895
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<EncounterModel> _allEncounters;

		// Token: 0x04001AF0 RID: 6896
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<PotionModel> _allPotions;

		// Token: 0x04001AF1 RID: 6897
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<PotionPoolModel> _allPotionPools;

		// Token: 0x04001AF2 RID: 6898
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<PotionPoolModel> _allCharacterPotionPools;

		// Token: 0x04001AF3 RID: 6899
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<RelicPoolModel> _allCharacterRelicPools;

		// Token: 0x04001AF4 RID: 6900
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<PotionPoolModel> _allSharedPotionPools;

		// Token: 0x04001AF5 RID: 6901
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<PowerModel> _allPowers;

		// Token: 0x04001AF6 RID: 6902
		[Nullable(new byte[] { 2, 1 })]
		private static IEnumerable<RelicModel> _allRelics;

		// Token: 0x04001AF7 RID: 6903
		[Nullable(new byte[] { 2, 1 })]
		private static List<AchievementModel> _achievements;
	}
}

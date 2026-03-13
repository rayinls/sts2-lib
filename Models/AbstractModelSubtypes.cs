using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MegaCrit.Sts2.Core.Models.Achievements;
using MegaCrit.Sts2.Core.Models.Acts;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Afflictions.Mocks;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Cards.Mocks;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.Enchantments.Mocks;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Models.Encounters.Mocks;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Models.Events.Mocks;
using MegaCrit.Sts2.Core.Models.Modifiers;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Monsters.Mocks;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Powers.Mocks;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Models.Singleton;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x020004A5 RID: 1189
	public static class AbstractModelSubtypes
	{
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004973 RID: 18803 RVA: 0x00205912 File Offset: 0x00203B12
		public static int Count
		{
			get
			{
				return 1599;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06004974 RID: 18804 RVA: 0x00205919 File Offset: 0x00203B19
		public static IReadOnlyList<Type> All
		{
			get
			{
				return AbstractModelSubtypes._subtypes;
			}
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x00205920 File Offset: 0x00203B20
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2063", Justification = "The list only contains types stored with the correct DynamicallyAccessedMembers attribute, enforced by source generation.")]
		[return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		public static Type Get(int i)
		{
			return AbstractModelSubtypes._subtypes[i];
		}

		// Token: 0x04001B3D RID: 6973
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t0 = typeof(Play20CardsSingleTurnAchievement);

		// Token: 0x04001B3E RID: 6974
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1 = typeof(SkillIronclad1Achievement);

		// Token: 0x04001B3F RID: 6975
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t2 = typeof(SkillIronclad2Achievement);

		// Token: 0x04001B40 RID: 6976
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t3 = typeof(SkillNecrobinder1Achievement);

		// Token: 0x04001B41 RID: 6977
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t4 = typeof(SkillNecrobinder2Achievement);

		// Token: 0x04001B42 RID: 6978
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t5 = typeof(SkillRegent1Achievement);

		// Token: 0x04001B43 RID: 6979
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t6 = typeof(SkillRegent2Achievement);

		// Token: 0x04001B44 RID: 6980
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t7 = typeof(SkillSilent1Achievement);

		// Token: 0x04001B45 RID: 6981
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t8 = typeof(SkillSilent2Achievement);

		// Token: 0x04001B46 RID: 6982
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t9 = typeof(Glory);

		// Token: 0x04001B47 RID: 6983
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t10 = typeof(Hive);

		// Token: 0x04001B48 RID: 6984
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t11 = typeof(Overgrowth);

		// Token: 0x04001B49 RID: 6985
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t12 = typeof(Underdocks);

		// Token: 0x04001B4A RID: 6986
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t13 = typeof(Bound);

		// Token: 0x04001B4B RID: 6987
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t14 = typeof(Entangled);

		// Token: 0x04001B4C RID: 6988
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t15 = typeof(Galvanized);

		// Token: 0x04001B4D RID: 6989
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t16 = typeof(Hexed);

		// Token: 0x04001B4E RID: 6990
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t17 = typeof(MockNoUnplayableAffliction);

		// Token: 0x04001B4F RID: 6991
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t18 = typeof(MockSelfDamageAffliction);

		// Token: 0x04001B50 RID: 6992
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t19 = typeof(MockUselessAffliction);

		// Token: 0x04001B51 RID: 6993
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t20 = typeof(Ringing);

		// Token: 0x04001B52 RID: 6994
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t21 = typeof(Smog);

		// Token: 0x04001B53 RID: 6995
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t22 = typeof(ColorlessCardPool);

		// Token: 0x04001B54 RID: 6996
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t23 = typeof(CurseCardPool);

		// Token: 0x04001B55 RID: 6997
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t24 = typeof(DefectCardPool);

		// Token: 0x04001B56 RID: 6998
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t25 = typeof(DeprecatedCardPool);

		// Token: 0x04001B57 RID: 6999
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t26 = typeof(EventCardPool);

		// Token: 0x04001B58 RID: 7000
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t27 = typeof(IroncladCardPool);

		// Token: 0x04001B59 RID: 7001
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t28 = typeof(MockCardPool);

		// Token: 0x04001B5A RID: 7002
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t29 = typeof(NecrobinderCardPool);

		// Token: 0x04001B5B RID: 7003
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t30 = typeof(QuestCardPool);

		// Token: 0x04001B5C RID: 7004
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t31 = typeof(RegentCardPool);

		// Token: 0x04001B5D RID: 7005
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t32 = typeof(SilentCardPool);

		// Token: 0x04001B5E RID: 7006
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t33 = typeof(StatusCardPool);

		// Token: 0x04001B5F RID: 7007
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t34 = typeof(TokenCardPool);

		// Token: 0x04001B60 RID: 7008
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t35 = typeof(Abrasive);

		// Token: 0x04001B61 RID: 7009
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t36 = typeof(Accelerant);

		// Token: 0x04001B62 RID: 7010
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t37 = typeof(Accuracy);

		// Token: 0x04001B63 RID: 7011
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t38 = typeof(Acrobatics);

		// Token: 0x04001B64 RID: 7012
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t39 = typeof(AdaptiveStrike);

		// Token: 0x04001B65 RID: 7013
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t40 = typeof(Adrenaline);

		// Token: 0x04001B66 RID: 7014
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t41 = typeof(Afterimage);

		// Token: 0x04001B67 RID: 7015
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t42 = typeof(Afterlife);

		// Token: 0x04001B68 RID: 7016
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t43 = typeof(Aggression);

		// Token: 0x04001B69 RID: 7017
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t44 = typeof(Alchemize);

		// Token: 0x04001B6A RID: 7018
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t45 = typeof(Alignment);

		// Token: 0x04001B6B RID: 7019
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t46 = typeof(AllForOne);

		// Token: 0x04001B6C RID: 7020
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t47 = typeof(Anger);

		// Token: 0x04001B6D RID: 7021
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t48 = typeof(Anointed);

		// Token: 0x04001B6E RID: 7022
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t49 = typeof(Anticipate);

		// Token: 0x04001B6F RID: 7023
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t50 = typeof(Apotheosis);

		// Token: 0x04001B70 RID: 7024
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t51 = typeof(Apparition);

		// Token: 0x04001B71 RID: 7025
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t52 = typeof(Armaments);

		// Token: 0x04001B72 RID: 7026
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t53 = typeof(Arsenal);

		// Token: 0x04001B73 RID: 7027
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t54 = typeof(AscendersBane);

		// Token: 0x04001B74 RID: 7028
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t55 = typeof(AshenStrike);

		// Token: 0x04001B75 RID: 7029
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t56 = typeof(Assassinate);

		// Token: 0x04001B76 RID: 7030
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t57 = typeof(AstralPulse);

		// Token: 0x04001B77 RID: 7031
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t58 = typeof(Automation);

		// Token: 0x04001B78 RID: 7032
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t59 = typeof(Backflip);

		// Token: 0x04001B79 RID: 7033
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t60 = typeof(Backstab);

		// Token: 0x04001B7A RID: 7034
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t61 = typeof(BadLuck);

		// Token: 0x04001B7B RID: 7035
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t62 = typeof(BallLightning);

		// Token: 0x04001B7C RID: 7036
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t63 = typeof(BansheesCry);

		// Token: 0x04001B7D RID: 7037
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t64 = typeof(Barrage);

		// Token: 0x04001B7E RID: 7038
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t65 = typeof(Barricade);

		// Token: 0x04001B7F RID: 7039
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t66 = typeof(Bash);

		// Token: 0x04001B80 RID: 7040
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t67 = typeof(BattleTrance);

		// Token: 0x04001B81 RID: 7041
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t68 = typeof(BeaconOfHope);

		// Token: 0x04001B82 RID: 7042
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t69 = typeof(BeamCell);

		// Token: 0x04001B83 RID: 7043
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t70 = typeof(BeatDown);

		// Token: 0x04001B84 RID: 7044
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t71 = typeof(BeatIntoShape);

		// Token: 0x04001B85 RID: 7045
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t72 = typeof(Beckon);

		// Token: 0x04001B86 RID: 7046
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t73 = typeof(Begone);

		// Token: 0x04001B87 RID: 7047
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t74 = typeof(BelieveInYou);

		// Token: 0x04001B88 RID: 7048
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t75 = typeof(BiasedCognition);

		// Token: 0x04001B89 RID: 7049
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t76 = typeof(BigBang);

		// Token: 0x04001B8A RID: 7050
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t77 = typeof(BlackHole);

		// Token: 0x04001B8B RID: 7051
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t78 = typeof(BladeDance);

		// Token: 0x04001B8C RID: 7052
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t79 = typeof(BladeOfInk);

		// Token: 0x04001B8D RID: 7053
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t80 = typeof(BlightStrike);

		// Token: 0x04001B8E RID: 7054
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t81 = typeof(Bloodletting);

		// Token: 0x04001B8F RID: 7055
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t82 = typeof(BloodWall);

		// Token: 0x04001B90 RID: 7056
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t83 = typeof(Bludgeon);

		// Token: 0x04001B91 RID: 7057
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t84 = typeof(Blur);

		// Token: 0x04001B92 RID: 7058
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t85 = typeof(Bodyguard);

		// Token: 0x04001B93 RID: 7059
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t86 = typeof(BodySlam);

		// Token: 0x04001B94 RID: 7060
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t87 = typeof(Bolas);

		// Token: 0x04001B95 RID: 7061
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t88 = typeof(Bombardment);

		// Token: 0x04001B96 RID: 7062
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t89 = typeof(BoneShards);

		// Token: 0x04001B97 RID: 7063
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t90 = typeof(BoostAway);

		// Token: 0x04001B98 RID: 7064
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t91 = typeof(BootSequence);

		// Token: 0x04001B99 RID: 7065
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t92 = typeof(BorrowedTime);

		// Token: 0x04001B9A RID: 7066
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t93 = typeof(BouncingFlask);

		// Token: 0x04001B9B RID: 7067
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t94 = typeof(Brand);

		// Token: 0x04001B9C RID: 7068
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t95 = typeof(Break);

		// Token: 0x04001B9D RID: 7069
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t96 = typeof(Breakthrough);

		// Token: 0x04001B9E RID: 7070
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t97 = typeof(BrightestFlame);

		// Token: 0x04001B9F RID: 7071
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t98 = typeof(BubbleBubble);

		// Token: 0x04001BA0 RID: 7072
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t99 = typeof(Buffer);

		// Token: 0x04001BA1 RID: 7073
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t100 = typeof(BulkUp);

		// Token: 0x04001BA2 RID: 7074
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t101 = typeof(BulletTime);

		// Token: 0x04001BA3 RID: 7075
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t102 = typeof(Bully);

		// Token: 0x04001BA4 RID: 7076
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t103 = typeof(Bulwark);

		// Token: 0x04001BA5 RID: 7077
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t104 = typeof(BundleOfJoy);

		// Token: 0x04001BA6 RID: 7078
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t105 = typeof(Burn);

		// Token: 0x04001BA7 RID: 7079
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t106 = typeof(BurningPact);

		// Token: 0x04001BA8 RID: 7080
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t107 = typeof(Burst);

		// Token: 0x04001BA9 RID: 7081
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t108 = typeof(Bury);

		// Token: 0x04001BAA RID: 7082
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t109 = typeof(ByrdonisEgg);

		// Token: 0x04001BAB RID: 7083
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t110 = typeof(ByrdSwoop);

		// Token: 0x04001BAC RID: 7084
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t111 = typeof(Calamity);

		// Token: 0x04001BAD RID: 7085
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t112 = typeof(Calcify);

		// Token: 0x04001BAE RID: 7086
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t113 = typeof(CalculatedGamble);

		// Token: 0x04001BAF RID: 7087
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t114 = typeof(CallOfTheVoid);

		// Token: 0x04001BB0 RID: 7088
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t115 = typeof(Caltrops);

		// Token: 0x04001BB1 RID: 7089
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t116 = typeof(Capacitor);

		// Token: 0x04001BB2 RID: 7090
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t117 = typeof(CaptureSpirit);

		// Token: 0x04001BB3 RID: 7091
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t118 = typeof(Cascade);

		// Token: 0x04001BB4 RID: 7092
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t119 = typeof(Catastrophe);

		// Token: 0x04001BB5 RID: 7093
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t120 = typeof(CelestialMight);

		// Token: 0x04001BB6 RID: 7094
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t121 = typeof(Chaos);

		// Token: 0x04001BB7 RID: 7095
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t122 = typeof(Charge);

		// Token: 0x04001BB8 RID: 7096
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t123 = typeof(ChargeBattery);

		// Token: 0x04001BB9 RID: 7097
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t124 = typeof(ChildOfTheStars);

		// Token: 0x04001BBA RID: 7098
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t125 = typeof(Chill);

		// Token: 0x04001BBB RID: 7099
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t126 = typeof(Cinder);

		// Token: 0x04001BBC RID: 7100
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t127 = typeof(Clash);

		// Token: 0x04001BBD RID: 7101
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t128 = typeof(Claw);

		// Token: 0x04001BBE RID: 7102
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t129 = typeof(Cleanse);

		// Token: 0x04001BBF RID: 7103
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t130 = typeof(CloakAndDagger);

		// Token: 0x04001BC0 RID: 7104
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t131 = typeof(CloakOfStars);

		// Token: 0x04001BC1 RID: 7105
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t132 = typeof(Clumsy);

		// Token: 0x04001BC2 RID: 7106
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t133 = typeof(ColdSnap);

		// Token: 0x04001BC3 RID: 7107
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t134 = typeof(CollisionCourse);

		// Token: 0x04001BC4 RID: 7108
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t135 = typeof(Colossus);

		// Token: 0x04001BC5 RID: 7109
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t136 = typeof(Comet);

		// Token: 0x04001BC6 RID: 7110
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t137 = typeof(Compact);

		// Token: 0x04001BC7 RID: 7111
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t138 = typeof(CompileDriver);

		// Token: 0x04001BC8 RID: 7112
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t139 = typeof(Conflagration);

		// Token: 0x04001BC9 RID: 7113
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t140 = typeof(Conqueror);

		// Token: 0x04001BCA RID: 7114
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t141 = typeof(ConsumingShadow);

		// Token: 0x04001BCB RID: 7115
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t142 = typeof(Convergence);

		// Token: 0x04001BCC RID: 7116
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t143 = typeof(Coolant);

		// Token: 0x04001BCD RID: 7117
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t144 = typeof(Coolheaded);

		// Token: 0x04001BCE RID: 7118
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t145 = typeof(Coordinate);

		// Token: 0x04001BCF RID: 7119
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t146 = typeof(CorrosiveWave);

		// Token: 0x04001BD0 RID: 7120
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t147 = typeof(Corruption);

		// Token: 0x04001BD1 RID: 7121
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t148 = typeof(CosmicIndifference);

		// Token: 0x04001BD2 RID: 7122
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t149 = typeof(Countdown);

		// Token: 0x04001BD3 RID: 7123
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t150 = typeof(CrashLanding);

		// Token: 0x04001BD4 RID: 7124
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t151 = typeof(CreativeAi);

		// Token: 0x04001BD5 RID: 7125
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t152 = typeof(CrescentSpear);

		// Token: 0x04001BD6 RID: 7126
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t153 = typeof(CrimsonMantle);

		// Token: 0x04001BD7 RID: 7127
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t154 = typeof(Cruelty);

		// Token: 0x04001BD8 RID: 7128
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t155 = typeof(CrushUnder);

		// Token: 0x04001BD9 RID: 7129
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t156 = typeof(CurseOfTheBell);

		// Token: 0x04001BDA RID: 7130
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t157 = typeof(DaggerSpray);

		// Token: 0x04001BDB RID: 7131
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t158 = typeof(DaggerThrow);

		// Token: 0x04001BDC RID: 7132
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t159 = typeof(DanseMacabre);

		// Token: 0x04001BDD RID: 7133
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t160 = typeof(DarkEmbrace);

		// Token: 0x04001BDE RID: 7134
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t161 = typeof(Darkness);

		// Token: 0x04001BDF RID: 7135
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t162 = typeof(DarkShackles);

		// Token: 0x04001BE0 RID: 7136
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t163 = typeof(Dash);

		// Token: 0x04001BE1 RID: 7137
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t164 = typeof(Dazed);

		// Token: 0x04001BE2 RID: 7138
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t165 = typeof(DeadlyPoison);

		// Token: 0x04001BE3 RID: 7139
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t166 = typeof(Deathbringer);

		// Token: 0x04001BE4 RID: 7140
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t167 = typeof(DeathMarch);

		// Token: 0x04001BE5 RID: 7141
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t168 = typeof(DeathsDoor);

		// Token: 0x04001BE6 RID: 7142
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t169 = typeof(Debilitate);

		// Token: 0x04001BE7 RID: 7143
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t170 = typeof(Debris);

		// Token: 0x04001BE8 RID: 7144
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t171 = typeof(Debt);

		// Token: 0x04001BE9 RID: 7145
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t172 = typeof(Decay);

		// Token: 0x04001BEA RID: 7146
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t173 = typeof(DecisionsDecisions);

		// Token: 0x04001BEB RID: 7147
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t174 = typeof(DefendDefect);

		// Token: 0x04001BEC RID: 7148
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t175 = typeof(DefendIronclad);

		// Token: 0x04001BED RID: 7149
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t176 = typeof(DefendNecrobinder);

		// Token: 0x04001BEE RID: 7150
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t177 = typeof(DefendRegent);

		// Token: 0x04001BEF RID: 7151
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t178 = typeof(DefendSilent);

		// Token: 0x04001BF0 RID: 7152
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t179 = typeof(Defile);

		// Token: 0x04001BF1 RID: 7153
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t180 = typeof(Deflect);

		// Token: 0x04001BF2 RID: 7154
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t181 = typeof(Defragment);

		// Token: 0x04001BF3 RID: 7155
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t182 = typeof(Defy);

		// Token: 0x04001BF4 RID: 7156
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t183 = typeof(Delay);

		// Token: 0x04001BF5 RID: 7157
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t184 = typeof(Demesne);

		// Token: 0x04001BF6 RID: 7158
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t185 = typeof(DemonForm);

		// Token: 0x04001BF7 RID: 7159
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t186 = typeof(DemonicShield);

		// Token: 0x04001BF8 RID: 7160
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t187 = typeof(DeprecatedCard);

		// Token: 0x04001BF9 RID: 7161
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t188 = typeof(Devastate);

		// Token: 0x04001BFA RID: 7162
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t189 = typeof(DevourLife);

		// Token: 0x04001BFB RID: 7163
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t190 = typeof(Dirge);

		// Token: 0x04001BFC RID: 7164
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t191 = typeof(Discovery);

		// Token: 0x04001BFD RID: 7165
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t192 = typeof(Disintegration);

		// Token: 0x04001BFE RID: 7166
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t193 = typeof(Dismantle);

		// Token: 0x04001BFF RID: 7167
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t194 = typeof(Distraction);

		// Token: 0x04001C00 RID: 7168
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t195 = typeof(DodgeAndRoll);

		// Token: 0x04001C01 RID: 7169
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t196 = typeof(Dominate);

		// Token: 0x04001C02 RID: 7170
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t197 = typeof(DoubleEnergy);

		// Token: 0x04001C03 RID: 7171
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t198 = typeof(Doubt);

		// Token: 0x04001C04 RID: 7172
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t199 = typeof(DrainPower);

		// Token: 0x04001C05 RID: 7173
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t200 = typeof(DramaticEntrance);

		// Token: 0x04001C06 RID: 7174
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t201 = typeof(Dredge);

		// Token: 0x04001C07 RID: 7175
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t202 = typeof(DrumOfBattle);

		// Token: 0x04001C08 RID: 7176
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t203 = typeof(Dualcast);

		// Token: 0x04001C09 RID: 7177
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t204 = typeof(DualWield);

		// Token: 0x04001C0A RID: 7178
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t205 = typeof(DyingStar);

		// Token: 0x04001C0B RID: 7179
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t206 = typeof(EchoForm);

		// Token: 0x04001C0C RID: 7180
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t207 = typeof(EchoingSlash);

		// Token: 0x04001C0D RID: 7181
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t208 = typeof(Eidolon);

		// Token: 0x04001C0E RID: 7182
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t209 = typeof(EndOfDays);

		// Token: 0x04001C0F RID: 7183
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t210 = typeof(EnergySurge);

		// Token: 0x04001C10 RID: 7184
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t211 = typeof(EnfeeblingTouch);

		// Token: 0x04001C11 RID: 7185
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t212 = typeof(Enlightenment);

		// Token: 0x04001C12 RID: 7186
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t213 = typeof(Enthralled);

		// Token: 0x04001C13 RID: 7187
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t214 = typeof(Entrench);

		// Token: 0x04001C14 RID: 7188
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t215 = typeof(Entropy);

		// Token: 0x04001C15 RID: 7189
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t216 = typeof(Envenom);

		// Token: 0x04001C16 RID: 7190
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t217 = typeof(Equilibrium);

		// Token: 0x04001C17 RID: 7191
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t218 = typeof(Eradicate);

		// Token: 0x04001C18 RID: 7192
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t219 = typeof(EscapePlan);

		// Token: 0x04001C19 RID: 7193
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t220 = typeof(EternalArmor);

		// Token: 0x04001C1A RID: 7194
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t221 = typeof(EvilEye);

		// Token: 0x04001C1B RID: 7195
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t222 = typeof(ExpectAFight);

		// Token: 0x04001C1C RID: 7196
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t223 = typeof(Expertise);

		// Token: 0x04001C1D RID: 7197
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t224 = typeof(Expose);

		// Token: 0x04001C1E RID: 7198
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t225 = typeof(Exterminate);

		// Token: 0x04001C1F RID: 7199
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t226 = typeof(FallingStar);

		// Token: 0x04001C20 RID: 7200
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t227 = typeof(FanOfKnives);

		// Token: 0x04001C21 RID: 7201
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t228 = typeof(Fasten);

		// Token: 0x04001C22 RID: 7202
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t229 = typeof(Fear);

		// Token: 0x04001C23 RID: 7203
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t230 = typeof(Feed);

		// Token: 0x04001C24 RID: 7204
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t231 = typeof(FeedingFrenzy);

		// Token: 0x04001C25 RID: 7205
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t232 = typeof(FeelNoPain);

		// Token: 0x04001C26 RID: 7206
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t233 = typeof(Feral);

		// Token: 0x04001C27 RID: 7207
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t234 = typeof(Fetch);

		// Token: 0x04001C28 RID: 7208
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t235 = typeof(FiendFire);

		// Token: 0x04001C29 RID: 7209
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t236 = typeof(FightMe);

		// Token: 0x04001C2A RID: 7210
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t237 = typeof(FightThrough);

		// Token: 0x04001C2B RID: 7211
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t238 = typeof(Finesse);

		// Token: 0x04001C2C RID: 7212
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t239 = typeof(Finisher);

		// Token: 0x04001C2D RID: 7213
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t240 = typeof(Fisticuffs);

		// Token: 0x04001C2E RID: 7214
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t241 = typeof(FlakCannon);

		// Token: 0x04001C2F RID: 7215
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t242 = typeof(FlameBarrier);

		// Token: 0x04001C30 RID: 7216
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t243 = typeof(Flanking);

		// Token: 0x04001C31 RID: 7217
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t244 = typeof(FlashOfSteel);

		// Token: 0x04001C32 RID: 7218
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t245 = typeof(Flatten);

		// Token: 0x04001C33 RID: 7219
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t246 = typeof(Flechettes);

		// Token: 0x04001C34 RID: 7220
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t247 = typeof(FlickFlack);

		// Token: 0x04001C35 RID: 7221
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t248 = typeof(FocusedStrike);

		// Token: 0x04001C36 RID: 7222
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t249 = typeof(FollowThrough);

		// Token: 0x04001C37 RID: 7223
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t250 = typeof(Folly);

		// Token: 0x04001C38 RID: 7224
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t251 = typeof(Footwork);

		// Token: 0x04001C39 RID: 7225
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t252 = typeof(ForbiddenGrimoire);

		// Token: 0x04001C3A RID: 7226
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t253 = typeof(ForegoneConclusion);

		// Token: 0x04001C3B RID: 7227
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t254 = typeof(ForgottenRitual);

		// Token: 0x04001C3C RID: 7228
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t255 = typeof(FranticEscape);

		// Token: 0x04001C3D RID: 7229
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t256 = typeof(Friendship);

		// Token: 0x04001C3E RID: 7230
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t257 = typeof(Ftl);

		// Token: 0x04001C3F RID: 7231
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t258 = typeof(Fuel);

		// Token: 0x04001C40 RID: 7232
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t259 = typeof(Furnace);

		// Token: 0x04001C41 RID: 7233
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t260 = typeof(Fusion);

		// Token: 0x04001C42 RID: 7234
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t261 = typeof(GammaBlast);

		// Token: 0x04001C43 RID: 7235
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t262 = typeof(GangUp);

		// Token: 0x04001C44 RID: 7236
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t263 = typeof(GatherLight);

		// Token: 0x04001C45 RID: 7237
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t264 = typeof(Genesis);

		// Token: 0x04001C46 RID: 7238
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t265 = typeof(GeneticAlgorithm);

		// Token: 0x04001C47 RID: 7239
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t266 = typeof(GiantRock);

		// Token: 0x04001C48 RID: 7240
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t267 = typeof(Glacier);

		// Token: 0x04001C49 RID: 7241
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t268 = typeof(Glasswork);

		// Token: 0x04001C4A RID: 7242
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t269 = typeof(Glimmer);

		// Token: 0x04001C4B RID: 7243
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t270 = typeof(GlimpseBeyond);

		// Token: 0x04001C4C RID: 7244
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t271 = typeof(Glitterstream);

		// Token: 0x04001C4D RID: 7245
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t272 = typeof(Glow);

		// Token: 0x04001C4E RID: 7246
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t273 = typeof(GoForTheEyes);

		// Token: 0x04001C4F RID: 7247
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t274 = typeof(GoldAxe);

		// Token: 0x04001C50 RID: 7248
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t275 = typeof(GrandFinale);

		// Token: 0x04001C51 RID: 7249
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t276 = typeof(Grapple);

		// Token: 0x04001C52 RID: 7250
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t277 = typeof(Graveblast);

		// Token: 0x04001C53 RID: 7251
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t278 = typeof(GraveWarden);

		// Token: 0x04001C54 RID: 7252
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t279 = typeof(Greed);

		// Token: 0x04001C55 RID: 7253
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t280 = typeof(Guards);

		// Token: 0x04001C56 RID: 7254
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t281 = typeof(GuidingStar);

		// Token: 0x04001C57 RID: 7255
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t282 = typeof(Guilty);

		// Token: 0x04001C58 RID: 7256
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t283 = typeof(GunkUp);

		// Token: 0x04001C59 RID: 7257
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t284 = typeof(Hailstorm);

		// Token: 0x04001C5A RID: 7258
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t285 = typeof(HammerTime);

		// Token: 0x04001C5B RID: 7259
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t286 = typeof(HandOfGreed);

		// Token: 0x04001C5C RID: 7260
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t287 = typeof(HandTrick);

		// Token: 0x04001C5D RID: 7261
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t288 = typeof(Hang);

		// Token: 0x04001C5E RID: 7262
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t289 = typeof(Haunt);

		// Token: 0x04001C5F RID: 7263
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t290 = typeof(Havoc);

		// Token: 0x04001C60 RID: 7264
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t291 = typeof(Haze);

		// Token: 0x04001C61 RID: 7265
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t292 = typeof(Headbutt);

		// Token: 0x04001C62 RID: 7266
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t293 = typeof(HeavenlyDrill);

		// Token: 0x04001C63 RID: 7267
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t294 = typeof(Hegemony);

		// Token: 0x04001C64 RID: 7268
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t295 = typeof(HeirloomHammer);

		// Token: 0x04001C65 RID: 7269
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t296 = typeof(HelixDrill);

		// Token: 0x04001C66 RID: 7270
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t297 = typeof(HelloWorld);

		// Token: 0x04001C67 RID: 7271
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t298 = typeof(Hellraiser);

		// Token: 0x04001C68 RID: 7272
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t299 = typeof(Hemokinesis);

		// Token: 0x04001C69 RID: 7273
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t300 = typeof(HiddenCache);

		// Token: 0x04001C6A RID: 7274
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t301 = typeof(HiddenDaggers);

		// Token: 0x04001C6B RID: 7275
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t302 = typeof(HiddenGem);

		// Token: 0x04001C6C RID: 7276
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t303 = typeof(HighFive);

		// Token: 0x04001C6D RID: 7277
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t304 = typeof(Hologram);

		// Token: 0x04001C6E RID: 7278
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t305 = typeof(Hotfix);

		// Token: 0x04001C6F RID: 7279
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t306 = typeof(HowlFromBeyond);

		// Token: 0x04001C70 RID: 7280
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t307 = typeof(HuddleUp);

		// Token: 0x04001C71 RID: 7281
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t308 = typeof(Hyperbeam);

		// Token: 0x04001C72 RID: 7282
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t309 = typeof(IAmInvincible);

		// Token: 0x04001C73 RID: 7283
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t310 = typeof(IceLance);

		// Token: 0x04001C74 RID: 7284
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t311 = typeof(Ignition);

		// Token: 0x04001C75 RID: 7285
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t312 = typeof(Impatience);

		// Token: 0x04001C76 RID: 7286
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t313 = typeof(Impervious);

		// Token: 0x04001C77 RID: 7287
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t314 = typeof(Infection);

		// Token: 0x04001C78 RID: 7288
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t315 = typeof(InfernalBlade);

		// Token: 0x04001C79 RID: 7289
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t316 = typeof(Inferno);

		// Token: 0x04001C7A RID: 7290
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t317 = typeof(InfiniteBlades);

		// Token: 0x04001C7B RID: 7291
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t318 = typeof(Inflame);

		// Token: 0x04001C7C RID: 7292
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t319 = typeof(Injury);

		// Token: 0x04001C7D RID: 7293
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t320 = typeof(Intercept);

		// Token: 0x04001C7E RID: 7294
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t321 = typeof(Invoke);

		// Token: 0x04001C7F RID: 7295
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t322 = typeof(IronWave);

		// Token: 0x04001C80 RID: 7296
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t323 = typeof(Iteration);

		// Token: 0x04001C81 RID: 7297
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t324 = typeof(JackOfAllTrades);

		// Token: 0x04001C82 RID: 7298
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t325 = typeof(Jackpot);

		// Token: 0x04001C83 RID: 7299
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t326 = typeof(Juggernaut);

		// Token: 0x04001C84 RID: 7300
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t327 = typeof(Juggling);

		// Token: 0x04001C85 RID: 7301
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t328 = typeof(KinglyKick);

		// Token: 0x04001C86 RID: 7302
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t329 = typeof(KinglyPunch);

		// Token: 0x04001C87 RID: 7303
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t330 = typeof(KnifeTrap);

		// Token: 0x04001C88 RID: 7304
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t331 = typeof(Knockdown);

		// Token: 0x04001C89 RID: 7305
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t332 = typeof(KnockoutBlow);

		// Token: 0x04001C8A RID: 7306
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t333 = typeof(KnowThyPlace);

		// Token: 0x04001C8B RID: 7307
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t334 = typeof(LanternKey);

		// Token: 0x04001C8C RID: 7308
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t335 = typeof(Largesse);

		// Token: 0x04001C8D RID: 7309
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t336 = typeof(LeadingStrike);

		// Token: 0x04001C8E RID: 7310
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t337 = typeof(Leap);

		// Token: 0x04001C8F RID: 7311
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t338 = typeof(LegionOfBone);

		// Token: 0x04001C90 RID: 7312
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t339 = typeof(LegSweep);

		// Token: 0x04001C91 RID: 7313
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t340 = typeof(Lethality);

		// Token: 0x04001C92 RID: 7314
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t341 = typeof(Lift);

		// Token: 0x04001C93 RID: 7315
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t342 = typeof(LightningRod);

		// Token: 0x04001C94 RID: 7316
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t343 = typeof(Loop);

		// Token: 0x04001C95 RID: 7317
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t344 = typeof(Luminesce);

		// Token: 0x04001C96 RID: 7318
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t345 = typeof(LunarBlast);

		// Token: 0x04001C97 RID: 7319
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t346 = typeof(MachineLearning);

		// Token: 0x04001C98 RID: 7320
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t347 = typeof(MadScience);

		// Token: 0x04001C99 RID: 7321
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t348 = typeof(MakeItSo);

		// Token: 0x04001C9A RID: 7322
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t349 = typeof(Malaise);

		// Token: 0x04001C9B RID: 7323
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t350 = typeof(Mangle);

		// Token: 0x04001C9C RID: 7324
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t351 = typeof(ManifestAuthority);

		// Token: 0x04001C9D RID: 7325
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t352 = typeof(MasterOfStrategy);

		// Token: 0x04001C9E RID: 7326
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t353 = typeof(MasterPlanner);

		// Token: 0x04001C9F RID: 7327
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t354 = typeof(Maul);

		// Token: 0x04001CA0 RID: 7328
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t355 = typeof(Mayhem);

		// Token: 0x04001CA1 RID: 7329
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t356 = typeof(Melancholy);

		// Token: 0x04001CA2 RID: 7330
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t357 = typeof(MementoMori);

		// Token: 0x04001CA3 RID: 7331
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t358 = typeof(Metamorphosis);

		// Token: 0x04001CA4 RID: 7332
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t359 = typeof(MeteorShower);

		// Token: 0x04001CA5 RID: 7333
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t360 = typeof(MeteorStrike);

		// Token: 0x04001CA6 RID: 7334
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t361 = typeof(Mimic);

		// Token: 0x04001CA7 RID: 7335
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t362 = typeof(MindBlast);

		// Token: 0x04001CA8 RID: 7336
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t363 = typeof(MindRot);

		// Token: 0x04001CA9 RID: 7337
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t364 = typeof(MinionDiveBomb);

		// Token: 0x04001CAA RID: 7338
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t365 = typeof(MinionSacrifice);

		// Token: 0x04001CAB RID: 7339
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t366 = typeof(MinionStrike);

		// Token: 0x04001CAC RID: 7340
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t367 = typeof(Mirage);

		// Token: 0x04001CAD RID: 7341
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t368 = typeof(Misery);

		// Token: 0x04001CAE RID: 7342
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t369 = typeof(MockAttackCard);

		// Token: 0x04001CAF RID: 7343
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t370 = typeof(MockCurseCard);

		// Token: 0x04001CB0 RID: 7344
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t371 = typeof(MockPowerCard);

		// Token: 0x04001CB1 RID: 7345
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t372 = typeof(MockQuestCard);

		// Token: 0x04001CB2 RID: 7346
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t373 = typeof(MockSkillCard);

		// Token: 0x04001CB3 RID: 7347
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t374 = typeof(MockStatusCard);

		// Token: 0x04001CB4 RID: 7348
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t375 = typeof(Modded);

		// Token: 0x04001CB5 RID: 7349
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t376 = typeof(MoltenFist);

		// Token: 0x04001CB6 RID: 7350
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t377 = typeof(MomentumStrike);

		// Token: 0x04001CB7 RID: 7351
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t378 = typeof(MonarchsGaze);

		// Token: 0x04001CB8 RID: 7352
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t379 = typeof(Monologue);

		// Token: 0x04001CB9 RID: 7353
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t380 = typeof(MultiCast);

		// Token: 0x04001CBA RID: 7354
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t381 = typeof(Murder);

		// Token: 0x04001CBB RID: 7355
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t382 = typeof(NecroMastery);

		// Token: 0x04001CBC RID: 7356
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t383 = typeof(NegativePulse);

		// Token: 0x04001CBD RID: 7357
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t384 = typeof(NeowsFury);

		// Token: 0x04001CBE RID: 7358
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t385 = typeof(Neurosurge);

		// Token: 0x04001CBF RID: 7359
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t386 = typeof(Neutralize);

		// Token: 0x04001CC0 RID: 7360
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t387 = typeof(NeutronAegis);

		// Token: 0x04001CC1 RID: 7361
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t388 = typeof(Nightmare);

		// Token: 0x04001CC2 RID: 7362
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t389 = typeof(NoEscape);

		// Token: 0x04001CC3 RID: 7363
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t390 = typeof(Normality);

		// Token: 0x04001CC4 RID: 7364
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t391 = typeof(Nostalgia);

		// Token: 0x04001CC5 RID: 7365
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t392 = typeof(NoxiousFumes);

		// Token: 0x04001CC6 RID: 7366
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t393 = typeof(Null);

		// Token: 0x04001CC7 RID: 7367
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t394 = typeof(Oblivion);

		// Token: 0x04001CC8 RID: 7368
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t395 = typeof(Offering);

		// Token: 0x04001CC9 RID: 7369
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t396 = typeof(Omnislice);

		// Token: 0x04001CCA RID: 7370
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t397 = typeof(OneTwoPunch);

		// Token: 0x04001CCB RID: 7371
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t398 = typeof(Orbit);

		// Token: 0x04001CCC RID: 7372
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t399 = typeof(Outbreak);

		// Token: 0x04001CCD RID: 7373
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t400 = typeof(Outmaneuver);

		// Token: 0x04001CCE RID: 7374
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t401 = typeof(Overclock);

		// Token: 0x04001CCF RID: 7375
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t402 = typeof(PactsEnd);

		// Token: 0x04001CD0 RID: 7376
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t403 = typeof(Pagestorm);

		// Token: 0x04001CD1 RID: 7377
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t404 = typeof(PaleBlueDot);

		// Token: 0x04001CD2 RID: 7378
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t405 = typeof(Panache);

		// Token: 0x04001CD3 RID: 7379
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t406 = typeof(PanicButton);

		// Token: 0x04001CD4 RID: 7380
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t407 = typeof(Parry);

		// Token: 0x04001CD5 RID: 7381
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t408 = typeof(Parse);

		// Token: 0x04001CD6 RID: 7382
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t409 = typeof(ParticleWall);

		// Token: 0x04001CD7 RID: 7383
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t410 = typeof(Patter);

		// Token: 0x04001CD8 RID: 7384
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t411 = typeof(Peck);

		// Token: 0x04001CD9 RID: 7385
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t412 = typeof(PerfectedStrike);

		// Token: 0x04001CDA RID: 7386
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t413 = typeof(PhantomBlades);

		// Token: 0x04001CDB RID: 7387
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t414 = typeof(PhotonCut);

		// Token: 0x04001CDC RID: 7388
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t415 = typeof(PiercingWail);

		// Token: 0x04001CDD RID: 7389
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t416 = typeof(Pillage);

		// Token: 0x04001CDE RID: 7390
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t417 = typeof(PillarOfCreation);

		// Token: 0x04001CDF RID: 7391
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t418 = typeof(Pinpoint);

		// Token: 0x04001CE0 RID: 7392
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t419 = typeof(PoisonedStab);

		// Token: 0x04001CE1 RID: 7393
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t420 = typeof(Poke);

		// Token: 0x04001CE2 RID: 7394
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t421 = typeof(PommelStrike);

		// Token: 0x04001CE3 RID: 7395
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t422 = typeof(PoorSleep);

		// Token: 0x04001CE4 RID: 7396
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t423 = typeof(Pounce);

		// Token: 0x04001CE5 RID: 7397
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t424 = typeof(PreciseCut);

		// Token: 0x04001CE6 RID: 7398
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t425 = typeof(Predator);

		// Token: 0x04001CE7 RID: 7399
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t426 = typeof(Prepared);

		// Token: 0x04001CE8 RID: 7400
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t427 = typeof(PrepTime);

		// Token: 0x04001CE9 RID: 7401
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t428 = typeof(PrimalForce);

		// Token: 0x04001CEA RID: 7402
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t429 = typeof(Production);

		// Token: 0x04001CEB RID: 7403
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t430 = typeof(Prolong);

		// Token: 0x04001CEC RID: 7404
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t431 = typeof(Prophesize);

		// Token: 0x04001CED RID: 7405
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t432 = typeof(Protector);

		// Token: 0x04001CEE RID: 7406
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t433 = typeof(Prowess);

		// Token: 0x04001CEF RID: 7407
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t434 = typeof(PullAggro);

		// Token: 0x04001CF0 RID: 7408
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t435 = typeof(PullFromBelow);

		// Token: 0x04001CF1 RID: 7409
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t436 = typeof(Purity);

		// Token: 0x04001CF2 RID: 7410
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t437 = typeof(Putrefy);

		// Token: 0x04001CF3 RID: 7411
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t438 = typeof(Pyre);

		// Token: 0x04001CF4 RID: 7412
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t439 = typeof(Quadcast);

		// Token: 0x04001CF5 RID: 7413
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t440 = typeof(Quasar);

		// Token: 0x04001CF6 RID: 7414
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t441 = typeof(Radiate);

		// Token: 0x04001CF7 RID: 7415
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t442 = typeof(Rage);

		// Token: 0x04001CF8 RID: 7416
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t443 = typeof(Rainbow);

		// Token: 0x04001CF9 RID: 7417
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t444 = typeof(Rally);

		// Token: 0x04001CFA RID: 7418
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t445 = typeof(Rampage);

		// Token: 0x04001CFB RID: 7419
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t446 = typeof(Rattle);

		// Token: 0x04001CFC RID: 7420
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t447 = typeof(Reanimate);

		// Token: 0x04001CFD RID: 7421
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t448 = typeof(Reap);

		// Token: 0x04001CFE RID: 7422
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t449 = typeof(ReaperForm);

		// Token: 0x04001CFF RID: 7423
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t450 = typeof(Reave);

		// Token: 0x04001D00 RID: 7424
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t451 = typeof(Reboot);

		// Token: 0x04001D01 RID: 7425
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t452 = typeof(Rebound);

		// Token: 0x04001D02 RID: 7426
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t453 = typeof(RefineBlade);

		// Token: 0x04001D03 RID: 7427
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t454 = typeof(Reflect);

		// Token: 0x04001D04 RID: 7428
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t455 = typeof(Reflex);

		// Token: 0x04001D05 RID: 7429
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t456 = typeof(Refract);

		// Token: 0x04001D06 RID: 7430
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t457 = typeof(Regret);

		// Token: 0x04001D07 RID: 7431
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t458 = typeof(Relax);

		// Token: 0x04001D08 RID: 7432
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t459 = typeof(Rend);

		// Token: 0x04001D09 RID: 7433
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t460 = typeof(Resonance);

		// Token: 0x04001D0A RID: 7434
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t461 = typeof(Restlessness);

		// Token: 0x04001D0B RID: 7435
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t462 = typeof(Ricochet);

		// Token: 0x04001D0C RID: 7436
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t463 = typeof(RightHandHand);

		// Token: 0x04001D0D RID: 7437
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t464 = typeof(RipAndTear);

		// Token: 0x04001D0E RID: 7438
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t465 = typeof(RocketPunch);

		// Token: 0x04001D0F RID: 7439
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t466 = typeof(RollingBoulder);

		// Token: 0x04001D10 RID: 7440
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t467 = typeof(RoyalGamble);

		// Token: 0x04001D11 RID: 7441
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t468 = typeof(Royalties);

		// Token: 0x04001D12 RID: 7442
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t469 = typeof(Rupture);

		// Token: 0x04001D13 RID: 7443
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t470 = typeof(Sacrifice);

		// Token: 0x04001D14 RID: 7444
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t471 = typeof(Salvo);

		// Token: 0x04001D15 RID: 7445
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t472 = typeof(Scavenge);

		// Token: 0x04001D16 RID: 7446
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t473 = typeof(Scourge);

		// Token: 0x04001D17 RID: 7447
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t474 = typeof(Scrape);

		// Token: 0x04001D18 RID: 7448
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t475 = typeof(Scrawl);

		// Token: 0x04001D19 RID: 7449
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t476 = typeof(SculptingStrike);

		// Token: 0x04001D1A RID: 7450
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t477 = typeof(Seance);

		// Token: 0x04001D1B RID: 7451
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t478 = typeof(SecondWind);

		// Token: 0x04001D1C RID: 7452
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t479 = typeof(SecretTechnique);

		// Token: 0x04001D1D RID: 7453
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t480 = typeof(SecretWeapon);

		// Token: 0x04001D1E RID: 7454
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t481 = typeof(SeekerStrike);

		// Token: 0x04001D1F RID: 7455
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t482 = typeof(SeekingEdge);

		// Token: 0x04001D20 RID: 7456
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t483 = typeof(SentryMode);

		// Token: 0x04001D21 RID: 7457
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t484 = typeof(SerpentForm);

		// Token: 0x04001D22 RID: 7458
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t485 = typeof(SetupStrike);

		// Token: 0x04001D23 RID: 7459
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t486 = typeof(SevenStars);

		// Token: 0x04001D24 RID: 7460
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t487 = typeof(Severance);

		// Token: 0x04001D25 RID: 7461
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t488 = typeof(Shadowmeld);

		// Token: 0x04001D26 RID: 7462
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t489 = typeof(ShadowShield);

		// Token: 0x04001D27 RID: 7463
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t490 = typeof(ShadowStep);

		// Token: 0x04001D28 RID: 7464
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t491 = typeof(Shame);

		// Token: 0x04001D29 RID: 7465
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t492 = typeof(SharedFate);

		// Token: 0x04001D2A RID: 7466
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t493 = typeof(Shatter);

		// Token: 0x04001D2B RID: 7467
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t494 = typeof(ShiningStrike);

		// Token: 0x04001D2C RID: 7468
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t495 = typeof(Shiv);

		// Token: 0x04001D2D RID: 7469
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t496 = typeof(Shockwave);

		// Token: 0x04001D2E RID: 7470
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t497 = typeof(Shroud);

		// Token: 0x04001D2F RID: 7471
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t498 = typeof(ShrugItOff);

		// Token: 0x04001D30 RID: 7472
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t499 = typeof(SicEm);

		// Token: 0x04001D31 RID: 7473
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t500 = typeof(SignalBoost);

		// Token: 0x04001D32 RID: 7474
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t501 = typeof(Skewer);

		// Token: 0x04001D33 RID: 7475
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t502 = typeof(Skim);

		// Token: 0x04001D34 RID: 7476
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t503 = typeof(SleightOfFlesh);

		// Token: 0x04001D35 RID: 7477
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t504 = typeof(Slice);

		// Token: 0x04001D36 RID: 7478
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t505 = typeof(Slimed);

		// Token: 0x04001D37 RID: 7479
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t506 = typeof(Sloth);

		// Token: 0x04001D38 RID: 7480
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t507 = typeof(Smokestack);

		// Token: 0x04001D39 RID: 7481
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t508 = typeof(Snakebite);

		// Token: 0x04001D3A RID: 7482
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t509 = typeof(Snap);

		// Token: 0x04001D3B RID: 7483
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t510 = typeof(Sneaky);

		// Token: 0x04001D3C RID: 7484
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t511 = typeof(SolarStrike);

		// Token: 0x04001D3D RID: 7485
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t512 = typeof(Soot);

		// Token: 0x04001D3E RID: 7486
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t513 = typeof(Soul);

		// Token: 0x04001D3F RID: 7487
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t514 = typeof(SoulStorm);

		// Token: 0x04001D40 RID: 7488
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t515 = typeof(SovereignBlade);

		// Token: 0x04001D41 RID: 7489
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t516 = typeof(Sow);

		// Token: 0x04001D42 RID: 7490
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t517 = typeof(SpectrumShift);

		// Token: 0x04001D43 RID: 7491
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t518 = typeof(Speedster);

		// Token: 0x04001D44 RID: 7492
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t519 = typeof(Spinner);

		// Token: 0x04001D45 RID: 7493
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t520 = typeof(SpiritOfAsh);

		// Token: 0x04001D46 RID: 7494
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t521 = typeof(Spite);

		// Token: 0x04001D47 RID: 7495
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t522 = typeof(Splash);

		// Token: 0x04001D48 RID: 7496
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t523 = typeof(SpoilsMap);

		// Token: 0x04001D49 RID: 7497
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t524 = typeof(SpoilsOfBattle);

		// Token: 0x04001D4A RID: 7498
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t525 = typeof(SporeMind);

		// Token: 0x04001D4B RID: 7499
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t526 = typeof(Spur);

		// Token: 0x04001D4C RID: 7500
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t527 = typeof(Squash);

		// Token: 0x04001D4D RID: 7501
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t528 = typeof(Squeeze);

		// Token: 0x04001D4E RID: 7502
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t529 = typeof(Stack);

		// Token: 0x04001D4F RID: 7503
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t530 = typeof(Stampede);

		// Token: 0x04001D50 RID: 7504
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t531 = typeof(Stardust);

		// Token: 0x04001D51 RID: 7505
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t532 = typeof(Stoke);

		// Token: 0x04001D52 RID: 7506
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t533 = typeof(Stomp);

		// Token: 0x04001D53 RID: 7507
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t534 = typeof(StoneArmor);

		// Token: 0x04001D54 RID: 7508
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t535 = typeof(Storm);

		// Token: 0x04001D55 RID: 7509
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t536 = typeof(StormOfSteel);

		// Token: 0x04001D56 RID: 7510
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t537 = typeof(Strangle);

		// Token: 0x04001D57 RID: 7511
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t538 = typeof(Stratagem);

		// Token: 0x04001D58 RID: 7512
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t539 = typeof(StrikeDefect);

		// Token: 0x04001D59 RID: 7513
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t540 = typeof(StrikeIronclad);

		// Token: 0x04001D5A RID: 7514
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t541 = typeof(StrikeNecrobinder);

		// Token: 0x04001D5B RID: 7515
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t542 = typeof(StrikeRegent);

		// Token: 0x04001D5C RID: 7516
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t543 = typeof(StrikeSilent);

		// Token: 0x04001D5D RID: 7517
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t544 = typeof(Subroutine);

		// Token: 0x04001D5E RID: 7518
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t545 = typeof(SuckerPunch);

		// Token: 0x04001D5F RID: 7519
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t546 = typeof(SummonForth);

		// Token: 0x04001D60 RID: 7520
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t547 = typeof(Sunder);

		// Token: 0x04001D61 RID: 7521
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t548 = typeof(Supercritical);

		// Token: 0x04001D62 RID: 7522
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t549 = typeof(Supermassive);

		// Token: 0x04001D63 RID: 7523
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t550 = typeof(Suppress);

		// Token: 0x04001D64 RID: 7524
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t551 = typeof(Survivor);

		// Token: 0x04001D65 RID: 7525
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t552 = typeof(SweepingBeam);

		// Token: 0x04001D66 RID: 7526
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t553 = typeof(SweepingGaze);

		// Token: 0x04001D67 RID: 7527
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t554 = typeof(SwordBoomerang);

		// Token: 0x04001D68 RID: 7528
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t555 = typeof(SwordSage);

		// Token: 0x04001D69 RID: 7529
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t556 = typeof(Synchronize);

		// Token: 0x04001D6A RID: 7530
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t557 = typeof(Synthesis);

		// Token: 0x04001D6B RID: 7531
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t558 = typeof(Tactician);

		// Token: 0x04001D6C RID: 7532
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t559 = typeof(TagTeam);

		// Token: 0x04001D6D RID: 7533
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t560 = typeof(Tank);

		// Token: 0x04001D6E RID: 7534
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t561 = typeof(Taunt);

		// Token: 0x04001D6F RID: 7535
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t562 = typeof(TearAsunder);

		// Token: 0x04001D70 RID: 7536
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t563 = typeof(Tempest);

		// Token: 0x04001D71 RID: 7537
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t564 = typeof(Terraforming);

		// Token: 0x04001D72 RID: 7538
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t565 = typeof(TeslaCoil);

		// Token: 0x04001D73 RID: 7539
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t566 = typeof(TheBomb);

		// Token: 0x04001D74 RID: 7540
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t567 = typeof(TheGambit);

		// Token: 0x04001D75 RID: 7541
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t568 = typeof(TheHunt);

		// Token: 0x04001D76 RID: 7542
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t569 = typeof(TheScythe);

		// Token: 0x04001D77 RID: 7543
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t570 = typeof(TheSealedThrone);

		// Token: 0x04001D78 RID: 7544
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t571 = typeof(TheSmith);

		// Token: 0x04001D79 RID: 7545
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t572 = typeof(ThinkingAhead);

		// Token: 0x04001D7A RID: 7546
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t573 = typeof(Thrash);

		// Token: 0x04001D7B RID: 7547
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t574 = typeof(ThrummingHatchet);

		// Token: 0x04001D7C RID: 7548
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t575 = typeof(Thunder);

		// Token: 0x04001D7D RID: 7549
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t576 = typeof(Thunderclap);

		// Token: 0x04001D7E RID: 7550
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t577 = typeof(TimesUp);

		// Token: 0x04001D7F RID: 7551
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t578 = typeof(ToolsOfTheTrade);

		// Token: 0x04001D80 RID: 7552
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t579 = typeof(ToricToughness);

		// Token: 0x04001D81 RID: 7553
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t580 = typeof(Toxic);

		// Token: 0x04001D82 RID: 7554
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t581 = typeof(Tracking);

		// Token: 0x04001D83 RID: 7555
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t582 = typeof(Transfigure);

		// Token: 0x04001D84 RID: 7556
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t583 = typeof(TrashToTreasure);

		// Token: 0x04001D85 RID: 7557
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t584 = typeof(Tremble);

		// Token: 0x04001D86 RID: 7558
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t585 = typeof(TrueGrit);

		// Token: 0x04001D87 RID: 7559
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t586 = typeof(Turbo);

		// Token: 0x04001D88 RID: 7560
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t587 = typeof(TwinStrike);

		// Token: 0x04001D89 RID: 7561
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t588 = typeof(Tyranny);

		// Token: 0x04001D8A RID: 7562
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t589 = typeof(UltimateDefend);

		// Token: 0x04001D8B RID: 7563
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t590 = typeof(UltimateStrike);

		// Token: 0x04001D8C RID: 7564
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t591 = typeof(Undeath);

		// Token: 0x04001D8D RID: 7565
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t592 = typeof(Unleash);

		// Token: 0x04001D8E RID: 7566
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t593 = typeof(Unmovable);

		// Token: 0x04001D8F RID: 7567
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t594 = typeof(Unrelenting);

		// Token: 0x04001D90 RID: 7568
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t595 = typeof(Untouchable);

		// Token: 0x04001D91 RID: 7569
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t596 = typeof(UpMySleeve);

		// Token: 0x04001D92 RID: 7570
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t597 = typeof(Uppercut);

		// Token: 0x04001D93 RID: 7571
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t598 = typeof(Uproar);

		// Token: 0x04001D94 RID: 7572
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t599 = typeof(Veilpiercer);

		// Token: 0x04001D95 RID: 7573
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t600 = typeof(Venerate);

		// Token: 0x04001D96 RID: 7574
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t601 = typeof(Vicious);

		// Token: 0x04001D97 RID: 7575
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t602 = typeof(Void);

		// Token: 0x04001D98 RID: 7576
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t603 = typeof(VoidForm);

		// Token: 0x04001D99 RID: 7577
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t604 = typeof(Volley);

		// Token: 0x04001D9A RID: 7578
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t605 = typeof(Voltaic);

		// Token: 0x04001D9B RID: 7579
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t606 = typeof(WasteAway);

		// Token: 0x04001D9C RID: 7580
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t607 = typeof(WellLaidPlans);

		// Token: 0x04001D9D RID: 7581
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t608 = typeof(Whirlwind);

		// Token: 0x04001D9E RID: 7582
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t609 = typeof(Whistle);

		// Token: 0x04001D9F RID: 7583
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t610 = typeof(WhiteNoise);

		// Token: 0x04001DA0 RID: 7584
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t611 = typeof(Wish);

		// Token: 0x04001DA1 RID: 7585
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t612 = typeof(Wisp);

		// Token: 0x04001DA2 RID: 7586
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t613 = typeof(Wound);

		// Token: 0x04001DA3 RID: 7587
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t614 = typeof(WraithForm);

		// Token: 0x04001DA4 RID: 7588
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t615 = typeof(Writhe);

		// Token: 0x04001DA5 RID: 7589
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t616 = typeof(WroughtInWar);

		// Token: 0x04001DA6 RID: 7590
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t617 = typeof(Zap);

		// Token: 0x04001DA7 RID: 7591
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t618 = typeof(Defect);

		// Token: 0x04001DA8 RID: 7592
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t619 = typeof(Deprived);

		// Token: 0x04001DA9 RID: 7593
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t620 = typeof(Ironclad);

		// Token: 0x04001DAA RID: 7594
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t621 = typeof(Necrobinder);

		// Token: 0x04001DAB RID: 7595
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t622 = typeof(RandomCharacter);

		// Token: 0x04001DAC RID: 7596
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t623 = typeof(Regent);

		// Token: 0x04001DAD RID: 7597
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t624 = typeof(Silent);

		// Token: 0x04001DAE RID: 7598
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t625 = typeof(Adroit);

		// Token: 0x04001DAF RID: 7599
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t626 = typeof(Clone);

		// Token: 0x04001DB0 RID: 7600
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t627 = typeof(Corrupted);

		// Token: 0x04001DB1 RID: 7601
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t628 = typeof(DeprecatedEnchantment);

		// Token: 0x04001DB2 RID: 7602
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t629 = typeof(Favored);

		// Token: 0x04001DB3 RID: 7603
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t630 = typeof(Glam);

		// Token: 0x04001DB4 RID: 7604
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t631 = typeof(Goopy);

		// Token: 0x04001DB5 RID: 7605
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t632 = typeof(Imbued);

		// Token: 0x04001DB6 RID: 7606
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t633 = typeof(Instinct);

		// Token: 0x04001DB7 RID: 7607
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t634 = typeof(MockFreeEnchantment);

		// Token: 0x04001DB8 RID: 7608
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t635 = typeof(Momentum);

		// Token: 0x04001DB9 RID: 7609
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t636 = typeof(Nimble);

		// Token: 0x04001DBA RID: 7610
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t637 = typeof(PerfectFit);

		// Token: 0x04001DBB RID: 7611
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t638 = typeof(RoyallyApproved);

		// Token: 0x04001DBC RID: 7612
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t639 = typeof(Sharp);

		// Token: 0x04001DBD RID: 7613
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t640 = typeof(Slither);

		// Token: 0x04001DBE RID: 7614
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t641 = typeof(SlumberingEssence);

		// Token: 0x04001DBF RID: 7615
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t642 = typeof(SoulsPower);

		// Token: 0x04001DC0 RID: 7616
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t643 = typeof(Sown);

		// Token: 0x04001DC1 RID: 7617
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t644 = typeof(Spiral);

		// Token: 0x04001DC2 RID: 7618
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t645 = typeof(Steady);

		// Token: 0x04001DC3 RID: 7619
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t646 = typeof(Swift);

		// Token: 0x04001DC4 RID: 7620
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t647 = typeof(TezcatarasEmber);

		// Token: 0x04001DC5 RID: 7621
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t648 = typeof(Vigorous);

		// Token: 0x04001DC6 RID: 7622
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t649 = typeof(AxebotsNormal);

		// Token: 0x04001DC7 RID: 7623
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t650 = typeof(BattlewornDummyEventEncounter);

		// Token: 0x04001DC8 RID: 7624
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t651 = typeof(BowlbugsNormal);

		// Token: 0x04001DC9 RID: 7625
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t652 = typeof(BowlbugsWeak);

		// Token: 0x04001DCA RID: 7626
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t653 = typeof(BygoneEffigyElite);

		// Token: 0x04001DCB RID: 7627
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t654 = typeof(ByrdonisElite);

		// Token: 0x04001DCC RID: 7628
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t655 = typeof(CeremonialBeastBoss);

		// Token: 0x04001DCD RID: 7629
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t656 = typeof(ChompersNormal);

		// Token: 0x04001DCE RID: 7630
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t657 = typeof(ConstructMenagerieNormal);

		// Token: 0x04001DCF RID: 7631
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t658 = typeof(CorpseSlugsNormal);

		// Token: 0x04001DD0 RID: 7632
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t659 = typeof(CorpseSlugsWeak);

		// Token: 0x04001DD1 RID: 7633
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t660 = typeof(CubexConstructNormal);

		// Token: 0x04001DD2 RID: 7634
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t661 = typeof(CultistsNormal);

		// Token: 0x04001DD3 RID: 7635
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t662 = typeof(DecimillipedeElite);

		// Token: 0x04001DD4 RID: 7636
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t663 = typeof(DenseVegetationEventEncounter);

		// Token: 0x04001DD5 RID: 7637
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t664 = typeof(DeprecatedEncounter);

		// Token: 0x04001DD6 RID: 7638
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t665 = typeof(DevotedSculptorWeak);

		// Token: 0x04001DD7 RID: 7639
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t666 = typeof(DoormakerBoss);

		// Token: 0x04001DD8 RID: 7640
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t667 = typeof(EntomancerElite);

		// Token: 0x04001DD9 RID: 7641
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t668 = typeof(ExoskeletonsNormal);

		// Token: 0x04001DDA RID: 7642
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t669 = typeof(ExoskeletonsWeak);

		// Token: 0x04001DDB RID: 7643
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t670 = typeof(FabricatorNormal);

		// Token: 0x04001DDC RID: 7644
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t671 = typeof(FakeMerchantEventEncounter);

		// Token: 0x04001DDD RID: 7645
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t672 = typeof(FlyconidNormal);

		// Token: 0x04001DDE RID: 7646
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t673 = typeof(FogmogNormal);

		// Token: 0x04001DDF RID: 7647
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t674 = typeof(FossilStalkerNormal);

		// Token: 0x04001DE0 RID: 7648
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t675 = typeof(FrogKnightNormal);

		// Token: 0x04001DE1 RID: 7649
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t676 = typeof(FuzzyWurmCrawlerWeak);

		// Token: 0x04001DE2 RID: 7650
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t677 = typeof(GlobeHeadNormal);

		// Token: 0x04001DE3 RID: 7651
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t678 = typeof(GremlinMercNormal);

		// Token: 0x04001DE4 RID: 7652
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t679 = typeof(HauntedShipNormal);

		// Token: 0x04001DE5 RID: 7653
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t680 = typeof(HunterKillerNormal);

		// Token: 0x04001DE6 RID: 7654
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t681 = typeof(InfestedPrismsElite);

		// Token: 0x04001DE7 RID: 7655
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t682 = typeof(InkletsNormal);

		// Token: 0x04001DE8 RID: 7656
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t683 = typeof(KaiserCrabBoss);

		// Token: 0x04001DE9 RID: 7657
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t684 = typeof(KnightsElite);

		// Token: 0x04001DEA RID: 7658
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t685 = typeof(KnowledgeDemonBoss);

		// Token: 0x04001DEB RID: 7659
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t686 = typeof(LagavulinMatriarchBoss);

		// Token: 0x04001DEC RID: 7660
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t687 = typeof(LivingFogNormal);

		// Token: 0x04001DED RID: 7661
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t688 = typeof(LouseProgenitorNormal);

		// Token: 0x04001DEE RID: 7662
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t689 = typeof(MawlerNormal);

		// Token: 0x04001DEF RID: 7663
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t690 = typeof(MechaKnightElite);

		// Token: 0x04001DF0 RID: 7664
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t691 = typeof(MockArtifactEncounter);

		// Token: 0x04001DF1 RID: 7665
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t692 = typeof(MockBossEncounter);

		// Token: 0x04001DF2 RID: 7666
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t693 = typeof(MockEliteEncounter);

		// Token: 0x04001DF3 RID: 7667
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t694 = typeof(MockMonsterEncounter);

		// Token: 0x04001DF4 RID: 7668
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t695 = typeof(MockNoRewardsEncounter);

		// Token: 0x04001DF5 RID: 7669
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t696 = typeof(MockPlatingEncounter);

		// Token: 0x04001DF6 RID: 7670
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t697 = typeof(MockTwoMonsterEncounter);

		// Token: 0x04001DF7 RID: 7671
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t698 = typeof(MysteriousKnightEventEncounter);

		// Token: 0x04001DF8 RID: 7672
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t699 = typeof(MytesNormal);

		// Token: 0x04001DF9 RID: 7673
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t700 = typeof(NibbitsNormal);

		// Token: 0x04001DFA RID: 7674
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t701 = typeof(NibbitsWeak);

		// Token: 0x04001DFB RID: 7675
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t702 = typeof(OvergrowthCrawlers);

		// Token: 0x04001DFC RID: 7676
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t703 = typeof(OvicopterNormal);

		// Token: 0x04001DFD RID: 7677
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t704 = typeof(OwlMagistrateNormal);

		// Token: 0x04001DFE RID: 7678
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t705 = typeof(PhantasmalGardenersElite);

		// Token: 0x04001DFF RID: 7679
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t706 = typeof(PhrogParasiteElite);

		// Token: 0x04001E00 RID: 7680
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t707 = typeof(PunchConstructNormal);

		// Token: 0x04001E01 RID: 7681
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t708 = typeof(PunchOffEventEncounter);

		// Token: 0x04001E02 RID: 7682
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t709 = typeof(QueenBoss);

		// Token: 0x04001E03 RID: 7683
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t710 = typeof(RubyRaidersNormal);

		// Token: 0x04001E04 RID: 7684
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t711 = typeof(ScrollsOfBitingNormal);

		// Token: 0x04001E05 RID: 7685
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t712 = typeof(ScrollsOfBitingWeak);

		// Token: 0x04001E06 RID: 7686
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t713 = typeof(SeapunkWeak);

		// Token: 0x04001E07 RID: 7687
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t714 = typeof(SewerClamNormal);

		// Token: 0x04001E08 RID: 7688
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t715 = typeof(ShrinkerBeetleWeak);

		// Token: 0x04001E09 RID: 7689
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t716 = typeof(SkulkingColonyElite);

		// Token: 0x04001E0A RID: 7690
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t717 = typeof(SlimedBerserkerNormal);

		// Token: 0x04001E0B RID: 7691
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t718 = typeof(SlimesNormal);

		// Token: 0x04001E0C RID: 7692
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t719 = typeof(SlimesWeak);

		// Token: 0x04001E0D RID: 7693
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t720 = typeof(SlitheringStranglerNormal);

		// Token: 0x04001E0E RID: 7694
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t721 = typeof(SludgeSpinnerWeak);

		// Token: 0x04001E0F RID: 7695
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t722 = typeof(SlumberingBeetleNormal);

		// Token: 0x04001E10 RID: 7696
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t723 = typeof(SnappingJaxfruitNormal);

		// Token: 0x04001E11 RID: 7697
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t724 = typeof(SoulFyshBoss);

		// Token: 0x04001E12 RID: 7698
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t725 = typeof(SoulNexusElite);

		// Token: 0x04001E13 RID: 7699
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t726 = typeof(SpinyToadNormal);

		// Token: 0x04001E14 RID: 7700
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t727 = typeof(TerrorEelElite);

		// Token: 0x04001E15 RID: 7701
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t728 = typeof(TestSubjectBoss);

		// Token: 0x04001E16 RID: 7702
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t729 = typeof(TheArchitectEventEncounter);

		// Token: 0x04001E17 RID: 7703
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t730 = typeof(TheInsatiableBoss);

		// Token: 0x04001E18 RID: 7704
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t731 = typeof(TheKinBoss);

		// Token: 0x04001E19 RID: 7705
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t732 = typeof(TheLostAndForgottenNormal);

		// Token: 0x04001E1A RID: 7706
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t733 = typeof(TheObscuraNormal);

		// Token: 0x04001E1B RID: 7707
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t734 = typeof(ThievingHopperWeak);

		// Token: 0x04001E1C RID: 7708
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t735 = typeof(ToadpolesNormal);

		// Token: 0x04001E1D RID: 7709
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t736 = typeof(ToadpolesWeak);

		// Token: 0x04001E1E RID: 7710
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t737 = typeof(TunnelerNormal);

		// Token: 0x04001E1F RID: 7711
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t738 = typeof(TunnelerWeak);

		// Token: 0x04001E20 RID: 7712
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t739 = typeof(TurretOperatorWeak);

		// Token: 0x04001E21 RID: 7713
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t740 = typeof(TwoTailedRatsNormal);

		// Token: 0x04001E22 RID: 7714
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t741 = typeof(VantomBoss);

		// Token: 0x04001E23 RID: 7715
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t742 = typeof(VineShamblerNormal);

		// Token: 0x04001E24 RID: 7716
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t743 = typeof(WaterfallGiantBoss);

		// Token: 0x04001E25 RID: 7717
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t744 = typeof(AbyssalBaths);

		// Token: 0x04001E26 RID: 7718
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t745 = typeof(Amalgamator);

		// Token: 0x04001E27 RID: 7719
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t746 = typeof(AromaOfChaos);

		// Token: 0x04001E28 RID: 7720
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t747 = typeof(BattlewornDummy);

		// Token: 0x04001E29 RID: 7721
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t748 = typeof(BrainLeech);

		// Token: 0x04001E2A RID: 7722
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t749 = typeof(Bugslayer);

		// Token: 0x04001E2B RID: 7723
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t750 = typeof(ByrdonisNest);

		// Token: 0x04001E2C RID: 7724
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t751 = typeof(ColorfulPhilosophers);

		// Token: 0x04001E2D RID: 7725
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t752 = typeof(ColossalFlower);

		// Token: 0x04001E2E RID: 7726
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t753 = typeof(CrystalSphere);

		// Token: 0x04001E2F RID: 7727
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t754 = typeof(Darv);

		// Token: 0x04001E30 RID: 7728
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t755 = typeof(DenseVegetation);

		// Token: 0x04001E31 RID: 7729
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t756 = typeof(DeprecatedAncientEvent);

		// Token: 0x04001E32 RID: 7730
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t757 = typeof(DeprecatedEvent);

		// Token: 0x04001E33 RID: 7731
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t758 = typeof(DollRoom);

		// Token: 0x04001E34 RID: 7732
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t759 = typeof(DoorsOfLightAndDark);

		// Token: 0x04001E35 RID: 7733
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t760 = typeof(DrowningBeacon);

		// Token: 0x04001E36 RID: 7734
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t761 = typeof(EndlessConveyor);

		// Token: 0x04001E37 RID: 7735
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t762 = typeof(FakeMerchant);

		// Token: 0x04001E38 RID: 7736
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t763 = typeof(FieldOfManSizedHoles);

		// Token: 0x04001E39 RID: 7737
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t764 = typeof(GraveOfTheForgotten);

		// Token: 0x04001E3A RID: 7738
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t765 = typeof(HungryForMushrooms);

		// Token: 0x04001E3B RID: 7739
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t766 = typeof(InfestedAutomaton);

		// Token: 0x04001E3C RID: 7740
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t767 = typeof(JungleMazeAdventure);

		// Token: 0x04001E3D RID: 7741
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t768 = typeof(MegaCrit.Sts2.Core.Models.Events.LostWisp);

		// Token: 0x04001E3E RID: 7742
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t769 = typeof(LuminousChoir);

		// Token: 0x04001E3F RID: 7743
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t770 = typeof(MockEventModel);

		// Token: 0x04001E40 RID: 7744
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t771 = typeof(MorphicGrove);

		// Token: 0x04001E41 RID: 7745
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t772 = typeof(Neow);

		// Token: 0x04001E42 RID: 7746
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t773 = typeof(Nonupeipe);

		// Token: 0x04001E43 RID: 7747
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t774 = typeof(Orobas);

		// Token: 0x04001E44 RID: 7748
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t775 = typeof(Pael);

		// Token: 0x04001E45 RID: 7749
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t776 = typeof(PotionCourier);

		// Token: 0x04001E46 RID: 7750
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t777 = typeof(PunchOff);

		// Token: 0x04001E47 RID: 7751
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t778 = typeof(RanwidTheElder);

		// Token: 0x04001E48 RID: 7752
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t779 = typeof(Reflections);

		// Token: 0x04001E49 RID: 7753
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t780 = typeof(RelicTrader);

		// Token: 0x04001E4A RID: 7754
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t781 = typeof(RoomFullOfCheese);

		// Token: 0x04001E4B RID: 7755
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t782 = typeof(RoundTeaParty);

		// Token: 0x04001E4C RID: 7756
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t783 = typeof(SapphireSeed);

		// Token: 0x04001E4D RID: 7757
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t784 = typeof(SelfHelpBook);

		// Token: 0x04001E4E RID: 7758
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t785 = typeof(SlipperyBridge);

		// Token: 0x04001E4F RID: 7759
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t786 = typeof(SpiralingWhirlpool);

		// Token: 0x04001E50 RID: 7760
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t787 = typeof(SpiritGrafter);

		// Token: 0x04001E51 RID: 7761
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t788 = typeof(StoneOfAllTime);

		// Token: 0x04001E52 RID: 7762
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t789 = typeof(SunkenStatue);

		// Token: 0x04001E53 RID: 7763
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t790 = typeof(SunkenTreasury);

		// Token: 0x04001E54 RID: 7764
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t791 = typeof(Symbiote);

		// Token: 0x04001E55 RID: 7765
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t792 = typeof(TabletOfTruth);

		// Token: 0x04001E56 RID: 7766
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t793 = typeof(Tanx);

		// Token: 0x04001E57 RID: 7767
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t794 = typeof(TeaMaster);

		// Token: 0x04001E58 RID: 7768
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t795 = typeof(Tezcatara);

		// Token: 0x04001E59 RID: 7769
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t796 = typeof(TheArchitect);

		// Token: 0x04001E5A RID: 7770
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t797 = typeof(TheFutureOfPotions);

		// Token: 0x04001E5B RID: 7771
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t798 = typeof(TheLanternKey);

		// Token: 0x04001E5C RID: 7772
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t799 = typeof(TheLegendsWereTrue);

		// Token: 0x04001E5D RID: 7773
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t800 = typeof(ThisOrThat);

		// Token: 0x04001E5E RID: 7774
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t801 = typeof(TinkerTime);

		// Token: 0x04001E5F RID: 7775
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t802 = typeof(TrashHeap);

		// Token: 0x04001E60 RID: 7776
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t803 = typeof(Trial);

		// Token: 0x04001E61 RID: 7777
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t804 = typeof(UnrestSite);

		// Token: 0x04001E62 RID: 7778
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t805 = typeof(Vakuu);

		// Token: 0x04001E63 RID: 7779
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t806 = typeof(WarHistorianRepy);

		// Token: 0x04001E64 RID: 7780
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t807 = typeof(WaterloggedScriptorium);

		// Token: 0x04001E65 RID: 7781
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t808 = typeof(WelcomeToWongos);

		// Token: 0x04001E66 RID: 7782
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t809 = typeof(Wellspring);

		// Token: 0x04001E67 RID: 7783
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t810 = typeof(WhisperingHollow);

		// Token: 0x04001E68 RID: 7784
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t811 = typeof(WoodCarvings);

		// Token: 0x04001E69 RID: 7785
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t812 = typeof(ZenWeaver);

		// Token: 0x04001E6A RID: 7786
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t813 = typeof(AllStar);

		// Token: 0x04001E6B RID: 7787
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t814 = typeof(BigGameHunter);

		// Token: 0x04001E6C RID: 7788
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t815 = typeof(CharacterCards);

		// Token: 0x04001E6D RID: 7789
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t816 = typeof(CursedRun);

		// Token: 0x04001E6E RID: 7790
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t817 = typeof(DeadlyEvents);

		// Token: 0x04001E6F RID: 7791
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t818 = typeof(DeprecatedModifier);

		// Token: 0x04001E70 RID: 7792
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t819 = typeof(Draft);

		// Token: 0x04001E71 RID: 7793
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t820 = typeof(Flight);

		// Token: 0x04001E72 RID: 7794
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t821 = typeof(Hoarder);

		// Token: 0x04001E73 RID: 7795
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t822 = typeof(Insanity);

		// Token: 0x04001E74 RID: 7796
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t823 = typeof(Midas);

		// Token: 0x04001E75 RID: 7797
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t824 = typeof(Murderous);

		// Token: 0x04001E76 RID: 7798
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t825 = typeof(NightTerrors);

		// Token: 0x04001E77 RID: 7799
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t826 = typeof(SealedDeck);

		// Token: 0x04001E78 RID: 7800
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t827 = typeof(Specialized);

		// Token: 0x04001E79 RID: 7801
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t828 = typeof(Terminal);

		// Token: 0x04001E7A RID: 7802
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t829 = typeof(Vintage);

		// Token: 0x04001E7B RID: 7803
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t830 = typeof(Architect);

		// Token: 0x04001E7C RID: 7804
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t831 = typeof(AssassinRubyRaider);

		// Token: 0x04001E7D RID: 7805
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t832 = typeof(Axebot);

		// Token: 0x04001E7E RID: 7806
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t833 = typeof(AxeRubyRaider);

		// Token: 0x04001E7F RID: 7807
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t834 = typeof(BattleFriendV1);

		// Token: 0x04001E80 RID: 7808
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t835 = typeof(BattleFriendV2);

		// Token: 0x04001E81 RID: 7809
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t836 = typeof(BattleFriendV3);

		// Token: 0x04001E82 RID: 7810
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t837 = typeof(BigDummy);

		// Token: 0x04001E83 RID: 7811
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t838 = typeof(BowlbugEgg);

		// Token: 0x04001E84 RID: 7812
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t839 = typeof(BowlbugNectar);

		// Token: 0x04001E85 RID: 7813
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t840 = typeof(BowlbugRock);

		// Token: 0x04001E86 RID: 7814
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t841 = typeof(BowlbugSilk);

		// Token: 0x04001E87 RID: 7815
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t842 = typeof(BruteRubyRaider);

		// Token: 0x04001E88 RID: 7816
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t843 = typeof(BygoneEffigy);

		// Token: 0x04001E89 RID: 7817
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t844 = typeof(Byrdonis);

		// Token: 0x04001E8A RID: 7818
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t845 = typeof(MegaCrit.Sts2.Core.Models.Monsters.Byrdpip);

		// Token: 0x04001E8B RID: 7819
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t846 = typeof(CalcifiedCultist);

		// Token: 0x04001E8C RID: 7820
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t847 = typeof(CeremonialBeast);

		// Token: 0x04001E8D RID: 7821
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t848 = typeof(Chomper);

		// Token: 0x04001E8E RID: 7822
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t849 = typeof(CorpseSlug);

		// Token: 0x04001E8F RID: 7823
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t850 = typeof(CrossbowRubyRaider);

		// Token: 0x04001E90 RID: 7824
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t851 = typeof(Crusher);

		// Token: 0x04001E91 RID: 7825
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t852 = typeof(CubexConstruct);

		// Token: 0x04001E92 RID: 7826
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t853 = typeof(DampCultist);

		// Token: 0x04001E93 RID: 7827
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t854 = typeof(DecimillipedeSegmentBack);

		// Token: 0x04001E94 RID: 7828
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t855 = typeof(DecimillipedeSegmentFront);

		// Token: 0x04001E95 RID: 7829
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t856 = typeof(DecimillipedeSegmentMiddle);

		// Token: 0x04001E96 RID: 7830
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t857 = typeof(DevotedSculptor);

		// Token: 0x04001E97 RID: 7831
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t858 = typeof(Door);

		// Token: 0x04001E98 RID: 7832
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t859 = typeof(Doormaker);

		// Token: 0x04001E99 RID: 7833
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t860 = typeof(Entomancer);

		// Token: 0x04001E9A RID: 7834
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t861 = typeof(Exoskeleton);

		// Token: 0x04001E9B RID: 7835
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t862 = typeof(EyeWithTeeth);

		// Token: 0x04001E9C RID: 7836
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t863 = typeof(Fabricator);

		// Token: 0x04001E9D RID: 7837
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t864 = typeof(FakeMerchantMonster);

		// Token: 0x04001E9E RID: 7838
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t865 = typeof(FatGremlin);

		// Token: 0x04001E9F RID: 7839
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t866 = typeof(FlailKnight);

		// Token: 0x04001EA0 RID: 7840
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t867 = typeof(Flyconid);

		// Token: 0x04001EA1 RID: 7841
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t868 = typeof(Fogmog);

		// Token: 0x04001EA2 RID: 7842
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t869 = typeof(FossilStalker);

		// Token: 0x04001EA3 RID: 7843
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t870 = typeof(FrogKnight);

		// Token: 0x04001EA4 RID: 7844
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t871 = typeof(FuzzyWurmCrawler);

		// Token: 0x04001EA5 RID: 7845
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t872 = typeof(GasBomb);

		// Token: 0x04001EA6 RID: 7846
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t873 = typeof(GlobeHead);

		// Token: 0x04001EA7 RID: 7847
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t874 = typeof(GremlinMerc);

		// Token: 0x04001EA8 RID: 7848
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t875 = typeof(Guardbot);

		// Token: 0x04001EA9 RID: 7849
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t876 = typeof(HauntedShip);

		// Token: 0x04001EAA RID: 7850
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t877 = typeof(HunterKiller);

		// Token: 0x04001EAB RID: 7851
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t878 = typeof(InfestedPrism);

		// Token: 0x04001EAC RID: 7852
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t879 = typeof(Inklet);

		// Token: 0x04001EAD RID: 7853
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t880 = typeof(KinFollower);

		// Token: 0x04001EAE RID: 7854
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t881 = typeof(KinPriest);

		// Token: 0x04001EAF RID: 7855
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t882 = typeof(KnowledgeDemon);

		// Token: 0x04001EB0 RID: 7856
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t883 = typeof(LagavulinMatriarch);

		// Token: 0x04001EB1 RID: 7857
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t884 = typeof(LeafSlimeM);

		// Token: 0x04001EB2 RID: 7858
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t885 = typeof(LeafSlimeS);

		// Token: 0x04001EB3 RID: 7859
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t886 = typeof(LivingFog);

		// Token: 0x04001EB4 RID: 7860
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t887 = typeof(LivingShield);

		// Token: 0x04001EB5 RID: 7861
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t888 = typeof(LouseProgenitor);

		// Token: 0x04001EB6 RID: 7862
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t889 = typeof(MagiKnight);

		// Token: 0x04001EB7 RID: 7863
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t890 = typeof(Mawler);

		// Token: 0x04001EB8 RID: 7864
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t891 = typeof(MechaKnight);

		// Token: 0x04001EB9 RID: 7865
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t892 = typeof(MockArtifactMonster);

		// Token: 0x04001EBA RID: 7866
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t893 = typeof(MockAttackAndSummonMinionMonster);

		// Token: 0x04001EBB RID: 7867
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t894 = typeof(MockAttackMonster);

		// Token: 0x04001EBC RID: 7868
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t895 = typeof(MockIntangibleMonster);

		// Token: 0x04001EBD RID: 7869
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t896 = typeof(MockPlatingMonster);

		// Token: 0x04001EBE RID: 7870
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t897 = typeof(MockReattachMonster);

		// Token: 0x04001EBF RID: 7871
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t898 = typeof(MultiAttackMoveMonster);

		// Token: 0x04001EC0 RID: 7872
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t899 = typeof(MysteriousKnight);

		// Token: 0x04001EC1 RID: 7873
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t900 = typeof(Myte);

		// Token: 0x04001EC2 RID: 7874
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t901 = typeof(Nibbit);

		// Token: 0x04001EC3 RID: 7875
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t902 = typeof(Noisebot);

		// Token: 0x04001EC4 RID: 7876
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t903 = typeof(OneHpMonster);

		// Token: 0x04001EC5 RID: 7877
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t904 = typeof(Osty);

		// Token: 0x04001EC6 RID: 7878
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t905 = typeof(Ovicopter);

		// Token: 0x04001EC7 RID: 7879
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t906 = typeof(OwlMagistrate);

		// Token: 0x04001EC8 RID: 7880
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t907 = typeof(MegaCrit.Sts2.Core.Models.Monsters.PaelsLegion);

		// Token: 0x04001EC9 RID: 7881
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t908 = typeof(Parafright);

		// Token: 0x04001ECA RID: 7882
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t909 = typeof(PhantasmalGardener);

		// Token: 0x04001ECB RID: 7883
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t910 = typeof(PhrogParasite);

		// Token: 0x04001ECC RID: 7884
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t911 = typeof(PunchConstruct);

		// Token: 0x04001ECD RID: 7885
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t912 = typeof(Queen);

		// Token: 0x04001ECE RID: 7886
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t913 = typeof(Rocket);

		// Token: 0x04001ECF RID: 7887
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t914 = typeof(ScrollOfBiting);

		// Token: 0x04001ED0 RID: 7888
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t915 = typeof(Seapunk);

		// Token: 0x04001ED1 RID: 7889
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t916 = typeof(SewerClam);

		// Token: 0x04001ED2 RID: 7890
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t917 = typeof(ShrinkerBeetle);

		// Token: 0x04001ED3 RID: 7891
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t918 = typeof(SingleAttackMoveMonster);

		// Token: 0x04001ED4 RID: 7892
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t919 = typeof(SkulkingColony);

		// Token: 0x04001ED5 RID: 7893
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t920 = typeof(SlimedBerserker);

		// Token: 0x04001ED6 RID: 7894
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t921 = typeof(SlitheringStrangler);

		// Token: 0x04001ED7 RID: 7895
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t922 = typeof(SludgeSpinner);

		// Token: 0x04001ED8 RID: 7896
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t923 = typeof(SlumberingBeetle);

		// Token: 0x04001ED9 RID: 7897
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t924 = typeof(SnappingJaxfruit);

		// Token: 0x04001EDA RID: 7898
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t925 = typeof(SneakyGremlin);

		// Token: 0x04001EDB RID: 7899
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t926 = typeof(SoulFysh);

		// Token: 0x04001EDC RID: 7900
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t927 = typeof(SoulNexus);

		// Token: 0x04001EDD RID: 7901
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t928 = typeof(SpectralKnight);

		// Token: 0x04001EDE RID: 7902
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t929 = typeof(SpinyToad);

		// Token: 0x04001EDF RID: 7903
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t930 = typeof(Stabbot);

		// Token: 0x04001EE0 RID: 7904
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t931 = typeof(TenHpMonster);

		// Token: 0x04001EE1 RID: 7905
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t932 = typeof(TerrorEel);

		// Token: 0x04001EE2 RID: 7906
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t933 = typeof(TestSubject);

		// Token: 0x04001EE3 RID: 7907
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t934 = typeof(TheAdversaryMkOne);

		// Token: 0x04001EE4 RID: 7908
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t935 = typeof(TheAdversaryMkThree);

		// Token: 0x04001EE5 RID: 7909
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t936 = typeof(TheAdversaryMkTwo);

		// Token: 0x04001EE6 RID: 7910
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t937 = typeof(TheForgotten);

		// Token: 0x04001EE7 RID: 7911
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t938 = typeof(TheInsatiable);

		// Token: 0x04001EE8 RID: 7912
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t939 = typeof(TheLost);

		// Token: 0x04001EE9 RID: 7913
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t940 = typeof(TheObscura);

		// Token: 0x04001EEA RID: 7914
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t941 = typeof(ThievingHopper);

		// Token: 0x04001EEB RID: 7915
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t942 = typeof(Toadpole);

		// Token: 0x04001EEC RID: 7916
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t943 = typeof(TorchHeadAmalgam);

		// Token: 0x04001EED RID: 7917
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t944 = typeof(ToughEgg);

		// Token: 0x04001EEE RID: 7918
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t945 = typeof(TrackerRubyRaider);

		// Token: 0x04001EEF RID: 7919
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t946 = typeof(Tunneler);

		// Token: 0x04001EF0 RID: 7920
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t947 = typeof(TurretOperator);

		// Token: 0x04001EF1 RID: 7921
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t948 = typeof(TwigSlimeM);

		// Token: 0x04001EF2 RID: 7922
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t949 = typeof(TwigSlimeS);

		// Token: 0x04001EF3 RID: 7923
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t950 = typeof(TwoTailedRat);

		// Token: 0x04001EF4 RID: 7924
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t951 = typeof(Vantom);

		// Token: 0x04001EF5 RID: 7925
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t952 = typeof(VineShambler);

		// Token: 0x04001EF6 RID: 7926
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t953 = typeof(WaterfallGiant);

		// Token: 0x04001EF7 RID: 7927
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t954 = typeof(Wriggler);

		// Token: 0x04001EF8 RID: 7928
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t955 = typeof(Zapbot);

		// Token: 0x04001EF9 RID: 7929
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t956 = typeof(DarkOrb);

		// Token: 0x04001EFA RID: 7930
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t957 = typeof(FrostOrb);

		// Token: 0x04001EFB RID: 7931
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t958 = typeof(GlassOrb);

		// Token: 0x04001EFC RID: 7932
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t959 = typeof(LightningOrb);

		// Token: 0x04001EFD RID: 7933
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t960 = typeof(PlasmaOrb);

		// Token: 0x04001EFE RID: 7934
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t961 = typeof(DefectPotionPool);

		// Token: 0x04001EFF RID: 7935
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t962 = typeof(DeprecatedPotionPool);

		// Token: 0x04001F00 RID: 7936
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t963 = typeof(EventPotionPool);

		// Token: 0x04001F01 RID: 7937
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t964 = typeof(IroncladPotionPool);

		// Token: 0x04001F02 RID: 7938
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t965 = typeof(NecrobinderPotionPool);

		// Token: 0x04001F03 RID: 7939
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t966 = typeof(RegentPotionPool);

		// Token: 0x04001F04 RID: 7940
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t967 = typeof(SharedPotionPool);

		// Token: 0x04001F05 RID: 7941
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t968 = typeof(SilentPotionPool);

		// Token: 0x04001F06 RID: 7942
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t969 = typeof(TokenPotionPool);

		// Token: 0x04001F07 RID: 7943
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t970 = typeof(Ashwater);

		// Token: 0x04001F08 RID: 7944
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t971 = typeof(AttackPotion);

		// Token: 0x04001F09 RID: 7945
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t972 = typeof(BeetleJuice);

		// Token: 0x04001F0A RID: 7946
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t973 = typeof(BlessingOfTheForge);

		// Token: 0x04001F0B RID: 7947
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t974 = typeof(BlockPotion);

		// Token: 0x04001F0C RID: 7948
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t975 = typeof(BloodPotion);

		// Token: 0x04001F0D RID: 7949
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t976 = typeof(BoneBrew);

		// Token: 0x04001F0E RID: 7950
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t977 = typeof(BottledPotential);

		// Token: 0x04001F0F RID: 7951
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t978 = typeof(Clarity);

		// Token: 0x04001F10 RID: 7952
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t979 = typeof(ColorlessPotion);

		// Token: 0x04001F11 RID: 7953
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t980 = typeof(CosmicConcoction);

		// Token: 0x04001F12 RID: 7954
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t981 = typeof(CunningPotion);

		// Token: 0x04001F13 RID: 7955
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t982 = typeof(CureAll);

		// Token: 0x04001F14 RID: 7956
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t983 = typeof(DeprecatedPotion);

		// Token: 0x04001F15 RID: 7957
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t984 = typeof(DexterityPotion);

		// Token: 0x04001F16 RID: 7958
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t985 = typeof(DistilledChaos);

		// Token: 0x04001F17 RID: 7959
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t986 = typeof(DropletOfPrecognition);

		// Token: 0x04001F18 RID: 7960
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t987 = typeof(Duplicator);

		// Token: 0x04001F19 RID: 7961
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t988 = typeof(EnergyPotion);

		// Token: 0x04001F1A RID: 7962
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t989 = typeof(EntropicBrew);

		// Token: 0x04001F1B RID: 7963
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t990 = typeof(EssenceOfDarkness);

		// Token: 0x04001F1C RID: 7964
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t991 = typeof(ExplosiveAmpoule);

		// Token: 0x04001F1D RID: 7965
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t992 = typeof(FairyInABottle);

		// Token: 0x04001F1E RID: 7966
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t993 = typeof(FirePotion);

		// Token: 0x04001F1F RID: 7967
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t994 = typeof(FlexPotion);

		// Token: 0x04001F20 RID: 7968
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t995 = typeof(FocusPotion);

		// Token: 0x04001F21 RID: 7969
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t996 = typeof(Fortifier);

		// Token: 0x04001F22 RID: 7970
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t997 = typeof(FoulPotion);

		// Token: 0x04001F23 RID: 7971
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t998 = typeof(FruitJuice);

		// Token: 0x04001F24 RID: 7972
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t999 = typeof(FyshOil);

		// Token: 0x04001F25 RID: 7973
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1000 = typeof(GamblersBrew);

		// Token: 0x04001F26 RID: 7974
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1001 = typeof(GhostInAJar);

		// Token: 0x04001F27 RID: 7975
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1002 = typeof(GigantificationPotion);

		// Token: 0x04001F28 RID: 7976
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1003 = typeof(GlowwaterPotion);

		// Token: 0x04001F29 RID: 7977
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1004 = typeof(HeartOfIron);

		// Token: 0x04001F2A RID: 7978
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1005 = typeof(KingsCourage);

		// Token: 0x04001F2B RID: 7979
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1006 = typeof(LiquidBronze);

		// Token: 0x04001F2C RID: 7980
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1007 = typeof(LiquidMemories);

		// Token: 0x04001F2D RID: 7981
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1008 = typeof(LuckyTonic);

		// Token: 0x04001F2E RID: 7982
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1009 = typeof(MazalethsGift);

		// Token: 0x04001F2F RID: 7983
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1010 = typeof(OrobicAcid);

		// Token: 0x04001F30 RID: 7984
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1011 = typeof(PoisonPotion);

		// Token: 0x04001F31 RID: 7985
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1012 = typeof(PotionOfBinding);

		// Token: 0x04001F32 RID: 7986
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1013 = typeof(PotionOfCapacity);

		// Token: 0x04001F33 RID: 7987
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1014 = typeof(PotionOfDoom);

		// Token: 0x04001F34 RID: 7988
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1015 = typeof(PotionShapedRock);

		// Token: 0x04001F35 RID: 7989
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1016 = typeof(PotOfGhouls);

		// Token: 0x04001F36 RID: 7990
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1017 = typeof(PowderedDemise);

		// Token: 0x04001F37 RID: 7991
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1018 = typeof(PowerPotion);

		// Token: 0x04001F38 RID: 7992
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1019 = typeof(RadiantTincture);

		// Token: 0x04001F39 RID: 7993
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1020 = typeof(RegenPotion);

		// Token: 0x04001F3A RID: 7994
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1021 = typeof(ShacklingPotion);

		// Token: 0x04001F3B RID: 7995
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1022 = typeof(ShipInABottle);

		// Token: 0x04001F3C RID: 7996
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1023 = typeof(SkillPotion);

		// Token: 0x04001F3D RID: 7997
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1024 = typeof(SneckoOil);

		// Token: 0x04001F3E RID: 7998
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1025 = typeof(SoldiersStew);

		// Token: 0x04001F3F RID: 7999
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1026 = typeof(SpeedPotion);

		// Token: 0x04001F40 RID: 8000
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1027 = typeof(StableSerum);

		// Token: 0x04001F41 RID: 8001
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1028 = typeof(StarPotion);

		// Token: 0x04001F42 RID: 8002
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1029 = typeof(StrengthPotion);

		// Token: 0x04001F43 RID: 8003
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1030 = typeof(SwiftPotion);

		// Token: 0x04001F44 RID: 8004
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1031 = typeof(TouchOfInsanity);

		// Token: 0x04001F45 RID: 8005
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1032 = typeof(VulnerablePotion);

		// Token: 0x04001F46 RID: 8006
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1033 = typeof(WeakPotion);

		// Token: 0x04001F47 RID: 8007
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1034 = typeof(AccelerantPower);

		// Token: 0x04001F48 RID: 8008
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1035 = typeof(AccuracyPower);

		// Token: 0x04001F49 RID: 8009
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1036 = typeof(AdaptablePower);

		// Token: 0x04001F4A RID: 8010
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1037 = typeof(AfterimagePower);

		// Token: 0x04001F4B RID: 8011
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1038 = typeof(AggressionPower);

		// Token: 0x04001F4C RID: 8012
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1039 = typeof(AnticipatePower);

		// Token: 0x04001F4D RID: 8013
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1040 = typeof(ArsenalPower);

		// Token: 0x04001F4E RID: 8014
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1041 = typeof(ArtifactPower);

		// Token: 0x04001F4F RID: 8015
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1042 = typeof(AsleepPower);

		// Token: 0x04001F50 RID: 8016
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1043 = typeof(AutomationPower);

		// Token: 0x04001F51 RID: 8017
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1044 = typeof(BackAttackLeftPower);

		// Token: 0x04001F52 RID: 8018
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1045 = typeof(BackAttackRightPower);

		// Token: 0x04001F53 RID: 8019
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1046 = typeof(BarricadePower);

		// Token: 0x04001F54 RID: 8020
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1047 = typeof(BattlewornDummyTimeLimitPower);

		// Token: 0x04001F55 RID: 8021
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1048 = typeof(BeaconOfHopePower);

		// Token: 0x04001F56 RID: 8022
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1049 = typeof(BiasedCognitionPower);

		// Token: 0x04001F57 RID: 8023
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1050 = typeof(BlackHolePower);

		// Token: 0x04001F58 RID: 8024
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1051 = typeof(BladeOfInkPower);

		// Token: 0x04001F59 RID: 8025
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1052 = typeof(BlockNextTurnPower);

		// Token: 0x04001F5A RID: 8026
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1053 = typeof(BlurPower);

		// Token: 0x04001F5B RID: 8027
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1054 = typeof(BufferPower);

		// Token: 0x04001F5C RID: 8028
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1055 = typeof(BurrowedPower);

		// Token: 0x04001F5D RID: 8029
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1056 = typeof(BurstPower);

		// Token: 0x04001F5E RID: 8030
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1057 = typeof(CalamityPower);

		// Token: 0x04001F5F RID: 8031
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1058 = typeof(CalcifyPower);

		// Token: 0x04001F60 RID: 8032
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1059 = typeof(CallOfTheVoidPower);

		// Token: 0x04001F61 RID: 8033
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1060 = typeof(ChainsOfBindingPower);

		// Token: 0x04001F62 RID: 8034
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1061 = typeof(ChildOfTheStarsPower);

		// Token: 0x04001F63 RID: 8035
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1062 = typeof(ClarityPower);

		// Token: 0x04001F64 RID: 8036
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1063 = typeof(ColossusPower);

		// Token: 0x04001F65 RID: 8037
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1064 = typeof(ConfusedPower);

		// Token: 0x04001F66 RID: 8038
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1065 = typeof(ConquerorPower);

		// Token: 0x04001F67 RID: 8039
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1066 = typeof(ConstrictPower);

		// Token: 0x04001F68 RID: 8040
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1067 = typeof(ConsumingShadowPower);

		// Token: 0x04001F69 RID: 8041
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1068 = typeof(CoolantPower);

		// Token: 0x04001F6A RID: 8042
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1069 = typeof(CoordinatePower);

		// Token: 0x04001F6B RID: 8043
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1070 = typeof(CorrosiveWavePower);

		// Token: 0x04001F6C RID: 8044
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1071 = typeof(CorruptionPower);

		// Token: 0x04001F6D RID: 8045
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1072 = typeof(CountdownPower);

		// Token: 0x04001F6E RID: 8046
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1073 = typeof(CoveredPower);

		// Token: 0x04001F6F RID: 8047
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1074 = typeof(CrabRagePower);

		// Token: 0x04001F70 RID: 8048
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1075 = typeof(CreativeAiPower);

		// Token: 0x04001F71 RID: 8049
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1076 = typeof(CrimsonMantlePower);

		// Token: 0x04001F72 RID: 8050
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1077 = typeof(CrueltyPower);

		// Token: 0x04001F73 RID: 8051
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1078 = typeof(CrushUnderPower);

		// Token: 0x04001F74 RID: 8052
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1079 = typeof(CuriousPower);

		// Token: 0x04001F75 RID: 8053
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1080 = typeof(CurlUpPower);

		// Token: 0x04001F76 RID: 8054
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1081 = typeof(DampenPower);

		// Token: 0x04001F77 RID: 8055
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1082 = typeof(DanseMacabrePower);

		// Token: 0x04001F78 RID: 8056
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1083 = typeof(DarkEmbracePower);

		// Token: 0x04001F79 RID: 8057
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1084 = typeof(DarkShacklesPower);

		// Token: 0x04001F7A RID: 8058
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1085 = typeof(DebilitatePower);

		// Token: 0x04001F7B RID: 8059
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1086 = typeof(DemesnePower);

		// Token: 0x04001F7C RID: 8060
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1087 = typeof(DemisePower);

		// Token: 0x04001F7D RID: 8061
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1088 = typeof(DemonFormPower);

		// Token: 0x04001F7E RID: 8062
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1089 = typeof(DevourLifePower);

		// Token: 0x04001F7F RID: 8063
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1090 = typeof(DexterityPower);

		// Token: 0x04001F80 RID: 8064
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1091 = typeof(DiamondDiademPower);

		// Token: 0x04001F81 RID: 8065
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1092 = typeof(DieForYouPower);

		// Token: 0x04001F82 RID: 8066
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1093 = typeof(DisintegrationPower);

		// Token: 0x04001F83 RID: 8067
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1094 = typeof(DoomPower);

		// Token: 0x04001F84 RID: 8068
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1095 = typeof(DoorRevivalPower);

		// Token: 0x04001F85 RID: 8069
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1096 = typeof(DoubleDamagePower);

		// Token: 0x04001F86 RID: 8070
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1097 = typeof(DrawCardsNextTurnPower);

		// Token: 0x04001F87 RID: 8071
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1098 = typeof(DrumOfBattlePower);

		// Token: 0x04001F88 RID: 8072
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1099 = typeof(DuplicationPower);

		// Token: 0x04001F89 RID: 8073
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1100 = typeof(DyingStarPower);

		// Token: 0x04001F8A RID: 8074
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1101 = typeof(EchoFormPower);

		// Token: 0x04001F8B RID: 8075
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1102 = typeof(EnergyNextTurnPower);

		// Token: 0x04001F8C RID: 8076
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1103 = typeof(EnfeeblingTouchPower);

		// Token: 0x04001F8D RID: 8077
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1104 = typeof(EnragePower);

		// Token: 0x04001F8E RID: 8078
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1105 = typeof(EntropyPower);

		// Token: 0x04001F8F RID: 8079
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1106 = typeof(EnvenomPower);

		// Token: 0x04001F90 RID: 8080
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1107 = typeof(EscapeArtistPower);

		// Token: 0x04001F91 RID: 8081
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1108 = typeof(FanOfKnivesPower);

		// Token: 0x04001F92 RID: 8082
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1109 = typeof(FastenPower);

		// Token: 0x04001F93 RID: 8083
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1110 = typeof(FeedingFrenzyPower);

		// Token: 0x04001F94 RID: 8084
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1111 = typeof(FeelNoPainPower);

		// Token: 0x04001F95 RID: 8085
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1112 = typeof(FeralPower);

		// Token: 0x04001F96 RID: 8086
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1113 = typeof(FlameBarrierPower);

		// Token: 0x04001F97 RID: 8087
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1114 = typeof(FlankingPower);

		// Token: 0x04001F98 RID: 8088
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1115 = typeof(FlexPotionPower);

		// Token: 0x04001F99 RID: 8089
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1116 = typeof(FlutterPower);

		// Token: 0x04001F9A RID: 8090
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1117 = typeof(FocusedStrikePower);

		// Token: 0x04001F9B RID: 8091
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1118 = typeof(FocusPower);

		// Token: 0x04001F9C RID: 8092
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1119 = typeof(ForbiddenGrimoirePower);

		// Token: 0x04001F9D RID: 8093
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1120 = typeof(ForegoneConclusionPower);

		// Token: 0x04001F9E RID: 8094
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1121 = typeof(FrailPower);

		// Token: 0x04001F9F RID: 8095
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1122 = typeof(FreeAttackPower);

		// Token: 0x04001FA0 RID: 8096
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1123 = typeof(FreePowerPower);

		// Token: 0x04001FA1 RID: 8097
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1124 = typeof(FreeSkillPower);

		// Token: 0x04001FA2 RID: 8098
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1125 = typeof(FriendshipPower);

		// Token: 0x04001FA3 RID: 8099
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1126 = typeof(FurnacePower);

		// Token: 0x04001FA4 RID: 8100
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1127 = typeof(GalvanicPower);

		// Token: 0x04001FA5 RID: 8101
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1128 = typeof(GenesisPower);

		// Token: 0x04001FA6 RID: 8102
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1129 = typeof(GigantificationPower);

		// Token: 0x04001FA7 RID: 8103
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1130 = typeof(GrapplePower);

		// Token: 0x04001FA8 RID: 8104
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1131 = typeof(GravityPower);

		// Token: 0x04001FA9 RID: 8105
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1132 = typeof(GuardedPower);

		// Token: 0x04001FAA RID: 8106
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1133 = typeof(HailstormPower);

		// Token: 0x04001FAB RID: 8107
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1134 = typeof(HammerTimePower);

		// Token: 0x04001FAC RID: 8108
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1135 = typeof(HangPower);

		// Token: 0x04001FAD RID: 8109
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1136 = typeof(HardenedShellPower);

		// Token: 0x04001FAE RID: 8110
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1137 = typeof(HardToKillPower);

		// Token: 0x04001FAF RID: 8111
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1138 = typeof(HatchPower);

		// Token: 0x04001FB0 RID: 8112
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1139 = typeof(HauntPower);

		// Token: 0x04001FB1 RID: 8113
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1140 = typeof(HeistPower);

		// Token: 0x04001FB2 RID: 8114
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1141 = typeof(HelicalDartPower);

		// Token: 0x04001FB3 RID: 8115
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1142 = typeof(HelloWorldPower);

		// Token: 0x04001FB4 RID: 8116
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1143 = typeof(HellraiserPower);

		// Token: 0x04001FB5 RID: 8117
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1144 = typeof(HexPower);

		// Token: 0x04001FB6 RID: 8118
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1145 = typeof(HighVoltagePower);

		// Token: 0x04001FB7 RID: 8119
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1146 = typeof(HotfixPower);

		// Token: 0x04001FB8 RID: 8120
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1147 = typeof(IllusionPower);

		// Token: 0x04001FB9 RID: 8121
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1148 = typeof(ImbalancedPower);

		// Token: 0x04001FBA RID: 8122
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1149 = typeof(ImprovementPower);

		// Token: 0x04001FBB RID: 8123
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1150 = typeof(InfernoPower);

		// Token: 0x04001FBC RID: 8124
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1151 = typeof(InfestedPower);

		// Token: 0x04001FBD RID: 8125
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1152 = typeof(InfiniteBladesPower);

		// Token: 0x04001FBE RID: 8126
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1153 = typeof(IntangiblePower);

		// Token: 0x04001FBF RID: 8127
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1154 = typeof(InterceptPower);

		// Token: 0x04001FC0 RID: 8128
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1155 = typeof(IterationPower);

		// Token: 0x04001FC1 RID: 8129
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1156 = typeof(JuggernautPower);

		// Token: 0x04001FC2 RID: 8130
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1157 = typeof(JugglingPower);

		// Token: 0x04001FC3 RID: 8131
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1158 = typeof(KnockdownPower);

		// Token: 0x04001FC4 RID: 8132
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1159 = typeof(LeadershipPower);

		// Token: 0x04001FC5 RID: 8133
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1160 = typeof(LethalityPower);

		// Token: 0x04001FC6 RID: 8134
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1161 = typeof(LightningRodPower);

		// Token: 0x04001FC7 RID: 8135
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1162 = typeof(LoopPower);

		// Token: 0x04001FC8 RID: 8136
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1163 = typeof(MachineLearningPower);

		// Token: 0x04001FC9 RID: 8137
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1164 = typeof(MagicBombPower);

		// Token: 0x04001FCA RID: 8138
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1165 = typeof(ManglePower);

		// Token: 0x04001FCB RID: 8139
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1166 = typeof(MasterPlannerPower);

		// Token: 0x04001FCC RID: 8140
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1167 = typeof(MayhemPower);

		// Token: 0x04001FCD RID: 8141
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1168 = typeof(MindRotPower);

		// Token: 0x04001FCE RID: 8142
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1169 = typeof(MinionPower);

		// Token: 0x04001FCF RID: 8143
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1170 = typeof(MockCloneCardsOnPlayPower);

		// Token: 0x04001FD0 RID: 8144
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1171 = typeof(MockFreeCardsPower);

		// Token: 0x04001FD1 RID: 8145
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1172 = typeof(MockGainBlockOnAttackPower);

		// Token: 0x04001FD2 RID: 8146
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1173 = typeof(MockInvincibleOnDeathPower);

		// Token: 0x04001FD3 RID: 8147
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1174 = typeof(MockModifyEnergyCostPower);

		// Token: 0x04001FD4 RID: 8148
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1175 = typeof(MockModifyStarCostPower);

		// Token: 0x04001FD5 RID: 8149
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1176 = typeof(MockPreventDeathPower);

		// Token: 0x04001FD6 RID: 8150
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1177 = typeof(MockRevivePower);

		// Token: 0x04001FD7 RID: 8151
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1178 = typeof(MockTemporaryStrengthLossPower);

		// Token: 0x04001FD8 RID: 8152
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1179 = typeof(MonarchsGazePower);

		// Token: 0x04001FD9 RID: 8153
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1180 = typeof(MonarchsGazeStrengthDownPower);

		// Token: 0x04001FDA RID: 8154
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1181 = typeof(MonologuePower);

		// Token: 0x04001FDB RID: 8155
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1182 = typeof(NecroMasteryPower);

		// Token: 0x04001FDC RID: 8156
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1183 = typeof(NemesisPower);

		// Token: 0x04001FDD RID: 8157
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1184 = typeof(NeurosurgePower);

		// Token: 0x04001FDE RID: 8158
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1185 = typeof(NightmarePower);

		// Token: 0x04001FDF RID: 8159
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1186 = typeof(NoBlockPower);

		// Token: 0x04001FE0 RID: 8160
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1187 = typeof(NoDrawPower);

		// Token: 0x04001FE1 RID: 8161
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1188 = typeof(NostalgiaPower);

		// Token: 0x04001FE2 RID: 8162
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1189 = typeof(NoxiousFumesPower);

		// Token: 0x04001FE3 RID: 8163
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1190 = typeof(OblivionPower);

		// Token: 0x04001FE4 RID: 8164
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1191 = typeof(OneTwoPunchPower);

		// Token: 0x04001FE5 RID: 8165
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1192 = typeof(OrbitPower);

		// Token: 0x04001FE6 RID: 8166
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1193 = typeof(OutbreakPower);

		// Token: 0x04001FE7 RID: 8167
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1194 = typeof(PagestormPower);

		// Token: 0x04001FE8 RID: 8168
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1195 = typeof(PainfulStabsPower);

		// Token: 0x04001FE9 RID: 8169
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1196 = typeof(PaleBlueDotPower);

		// Token: 0x04001FEA RID: 8170
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1197 = typeof(PanachePower);

		// Token: 0x04001FEB RID: 8171
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1198 = typeof(PaperCutsPower);

		// Token: 0x04001FEC RID: 8172
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1199 = typeof(ParryPower);

		// Token: 0x04001FED RID: 8173
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1200 = typeof(PersonalHivePower);

		// Token: 0x04001FEE RID: 8174
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1201 = typeof(PhantomBladesPower);

		// Token: 0x04001FEF RID: 8175
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1202 = typeof(PiercingWailPower);

		// Token: 0x04001FF0 RID: 8176
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1203 = typeof(PillarOfCreationPower);

		// Token: 0x04001FF1 RID: 8177
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1204 = typeof(PlatingPower);

		// Token: 0x04001FF2 RID: 8178
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1205 = typeof(PlowPower);

		// Token: 0x04001FF3 RID: 8179
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1206 = typeof(PoisonPower);

		// Token: 0x04001FF4 RID: 8180
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1207 = typeof(PossessSpeedPower);

		// Token: 0x04001FF5 RID: 8181
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1208 = typeof(PossessStrengthPower);

		// Token: 0x04001FF6 RID: 8182
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1209 = typeof(PrepTimePower);

		// Token: 0x04001FF7 RID: 8183
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1210 = typeof(PyrePower);

		// Token: 0x04001FF8 RID: 8184
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1211 = typeof(RadiancePower);

		// Token: 0x04001FF9 RID: 8185
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1212 = typeof(RagePower);

		// Token: 0x04001FFA RID: 8186
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1213 = typeof(RampartPower);

		// Token: 0x04001FFB RID: 8187
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1214 = typeof(RavenousPower);

		// Token: 0x04001FFC RID: 8188
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1215 = typeof(ReaperFormPower);

		// Token: 0x04001FFD RID: 8189
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1216 = typeof(ReattachPower);

		// Token: 0x04001FFE RID: 8190
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1217 = typeof(ReboundPower);

		// Token: 0x04001FFF RID: 8191
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1218 = typeof(ReflectPower);

		// Token: 0x04002000 RID: 8192
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1219 = typeof(RegenPower);

		// Token: 0x04002001 RID: 8193
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1220 = typeof(ReptileTrinketPower);

		// Token: 0x04002002 RID: 8194
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1221 = typeof(RetainHandPower);

		// Token: 0x04002003 RID: 8195
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1222 = typeof(RingingPower);

		// Token: 0x04002004 RID: 8196
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1223 = typeof(RitualPower);

		// Token: 0x04002005 RID: 8197
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1224 = typeof(RollingBoulderPower);

		// Token: 0x04002006 RID: 8198
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1225 = typeof(RoyaltiesPower);

		// Token: 0x04002007 RID: 8199
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1226 = typeof(RupturePower);

		// Token: 0x04002008 RID: 8200
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1227 = typeof(SandpitPower);

		// Token: 0x04002009 RID: 8201
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1228 = typeof(SeekingEdgePower);

		// Token: 0x0400200A RID: 8202
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1229 = typeof(SelfFormingClayPower);

		// Token: 0x0400200B RID: 8203
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1230 = typeof(SentryModePower);

		// Token: 0x0400200C RID: 8204
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1231 = typeof(SerpentFormPower);

		// Token: 0x0400200D RID: 8205
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1232 = typeof(SetupStrikePower);

		// Token: 0x0400200E RID: 8206
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1233 = typeof(ShacklingPotionPower);

		// Token: 0x0400200F RID: 8207
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1234 = typeof(ShadowmeldPower);

		// Token: 0x04002010 RID: 8208
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1235 = typeof(ShadowStepPower);

		// Token: 0x04002011 RID: 8209
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1236 = typeof(ShriekPower);

		// Token: 0x04002012 RID: 8210
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1237 = typeof(ShrinkPower);

		// Token: 0x04002013 RID: 8211
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1238 = typeof(ShroudPower);

		// Token: 0x04002014 RID: 8212
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1239 = typeof(SicEmPower);

		// Token: 0x04002015 RID: 8213
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1240 = typeof(SignalBoostPower);

		// Token: 0x04002016 RID: 8214
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1241 = typeof(SkittishPower);

		// Token: 0x04002017 RID: 8215
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1242 = typeof(SleightOfFleshPower);

		// Token: 0x04002018 RID: 8216
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1243 = typeof(SlipperyPower);

		// Token: 0x04002019 RID: 8217
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1244 = typeof(SlothPower);

		// Token: 0x0400201A RID: 8218
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1245 = typeof(SlowPower);

		// Token: 0x0400201B RID: 8219
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1246 = typeof(SlumberPower);

		// Token: 0x0400201C RID: 8220
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1247 = typeof(SmoggyPower);

		// Token: 0x0400201D RID: 8221
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1248 = typeof(SmokestackPower);

		// Token: 0x0400201E RID: 8222
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1249 = typeof(SneakyPower);

		// Token: 0x0400201F RID: 8223
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1250 = typeof(SoarPower);

		// Token: 0x04002020 RID: 8224
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1251 = typeof(SpectrumShiftPower);

		// Token: 0x04002021 RID: 8225
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1252 = typeof(SpeedPotionPower);

		// Token: 0x04002022 RID: 8226
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1253 = typeof(SpeedsterPower);

		// Token: 0x04002023 RID: 8227
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1254 = typeof(SpinnerPower);

		// Token: 0x04002024 RID: 8228
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1255 = typeof(SpiritOfAshPower);

		// Token: 0x04002025 RID: 8229
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1256 = typeof(StampedePower);

		// Token: 0x04002026 RID: 8230
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1257 = typeof(StarNextTurnPower);

		// Token: 0x04002027 RID: 8231
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1258 = typeof(SteamEruptionPower);

		// Token: 0x04002028 RID: 8232
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1259 = typeof(StockPower);

		// Token: 0x04002029 RID: 8233
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1260 = typeof(StormPower);

		// Token: 0x0400202A RID: 8234
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1261 = typeof(StranglePower);

		// Token: 0x0400202B RID: 8235
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1262 = typeof(StratagemPower);

		// Token: 0x0400202C RID: 8236
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1263 = typeof(StrengthPower);

		// Token: 0x0400202D RID: 8237
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1264 = typeof(SubroutinePower);

		// Token: 0x0400202E RID: 8238
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1265 = typeof(SuckPower);

		// Token: 0x0400202F RID: 8239
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1266 = typeof(SummonNextTurnPower);

		// Token: 0x04002030 RID: 8240
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1267 = typeof(SurprisePower);

		// Token: 0x04002031 RID: 8241
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1268 = typeof(SurroundedPower);

		// Token: 0x04002032 RID: 8242
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1269 = typeof(SwipePower);

		// Token: 0x04002033 RID: 8243
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1270 = typeof(SwordSagePower);

		// Token: 0x04002034 RID: 8244
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1271 = typeof(SynchronizePower);

		// Token: 0x04002035 RID: 8245
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1272 = typeof(TagTeamPower);

		// Token: 0x04002036 RID: 8246
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1273 = typeof(TangledPower);

		// Token: 0x04002037 RID: 8247
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1274 = typeof(TankPower);

		// Token: 0x04002038 RID: 8248
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1275 = typeof(TenderPower);

		// Token: 0x04002039 RID: 8249
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1276 = typeof(TerritorialPower);

		// Token: 0x0400203A RID: 8250
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1277 = typeof(TheBombPower);

		// Token: 0x0400203B RID: 8251
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1278 = typeof(TheGambitPower);

		// Token: 0x0400203C RID: 8252
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1279 = typeof(TheHuntPower);

		// Token: 0x0400203D RID: 8253
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1280 = typeof(TheSealedThronePower);

		// Token: 0x0400203E RID: 8254
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1281 = typeof(ThieveryPower);

		// Token: 0x0400203F RID: 8255
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1282 = typeof(ThornsPower);

		// Token: 0x04002040 RID: 8256
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1283 = typeof(ThunderPower);

		// Token: 0x04002041 RID: 8257
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1284 = typeof(ToolsOfTheTradePower);

		// Token: 0x04002042 RID: 8258
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1285 = typeof(ToricToughnessPower);

		// Token: 0x04002043 RID: 8259
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1286 = typeof(TrackingPower);

		// Token: 0x04002044 RID: 8260
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1287 = typeof(TrashToTreasurePower);

		// Token: 0x04002045 RID: 8261
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1288 = typeof(TyrannyPower);

		// Token: 0x04002046 RID: 8262
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1289 = typeof(UnmovablePower);

		// Token: 0x04002047 RID: 8263
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1290 = typeof(VeilpiercerPower);

		// Token: 0x04002048 RID: 8264
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1291 = typeof(ViciousPower);

		// Token: 0x04002049 RID: 8265
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1292 = typeof(VigorPower);

		// Token: 0x0400204A RID: 8266
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1293 = typeof(VitalSparkPower);

		// Token: 0x0400204B RID: 8267
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1294 = typeof(VoidFormPower);

		// Token: 0x0400204C RID: 8268
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1295 = typeof(VulnerablePower);

		// Token: 0x0400204D RID: 8269
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1296 = typeof(WasteAwayPower);

		// Token: 0x0400204E RID: 8270
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1297 = typeof(WeakPower);

		// Token: 0x0400204F RID: 8271
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1298 = typeof(WellLaidPlansPower);

		// Token: 0x04002050 RID: 8272
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1299 = typeof(WraithFormPower);

		// Token: 0x04002051 RID: 8273
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1300 = typeof(DefectRelicPool);

		// Token: 0x04002052 RID: 8274
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1301 = typeof(DeprecatedRelicPool);

		// Token: 0x04002053 RID: 8275
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1302 = typeof(EventRelicPool);

		// Token: 0x04002054 RID: 8276
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1303 = typeof(FallbackRelicPool);

		// Token: 0x04002055 RID: 8277
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1304 = typeof(IroncladRelicPool);

		// Token: 0x04002056 RID: 8278
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1305 = typeof(NecrobinderRelicPool);

		// Token: 0x04002057 RID: 8279
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1306 = typeof(RegentRelicPool);

		// Token: 0x04002058 RID: 8280
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1307 = typeof(SharedRelicPool);

		// Token: 0x04002059 RID: 8281
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1308 = typeof(SilentRelicPool);

		// Token: 0x0400205A RID: 8282
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1309 = typeof(Akabeko);

		// Token: 0x0400205B RID: 8283
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1310 = typeof(AlchemicalCoffer);

		// Token: 0x0400205C RID: 8284
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1311 = typeof(AmethystAubergine);

		// Token: 0x0400205D RID: 8285
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1312 = typeof(Anchor);

		// Token: 0x0400205E RID: 8286
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1313 = typeof(ArcaneScroll);

		// Token: 0x0400205F RID: 8287
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1314 = typeof(ArchaicTooth);

		// Token: 0x04002060 RID: 8288
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1315 = typeof(ArtOfWar);

		// Token: 0x04002061 RID: 8289
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1316 = typeof(Astrolabe);

		// Token: 0x04002062 RID: 8290
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1317 = typeof(BagOfMarbles);

		// Token: 0x04002063 RID: 8291
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1318 = typeof(BagOfPreparation);

		// Token: 0x04002064 RID: 8292
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1319 = typeof(BeatingRemnant);

		// Token: 0x04002065 RID: 8293
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1320 = typeof(BeautifulBracelet);

		// Token: 0x04002066 RID: 8294
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1321 = typeof(Bellows);

		// Token: 0x04002067 RID: 8295
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1322 = typeof(BeltBuckle);

		// Token: 0x04002068 RID: 8296
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1323 = typeof(BigHat);

		// Token: 0x04002069 RID: 8297
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1324 = typeof(BigMushroom);

		// Token: 0x0400206A RID: 8298
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1325 = typeof(BiiigHug);

		// Token: 0x0400206B RID: 8299
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1326 = typeof(BingBong);

		// Token: 0x0400206C RID: 8300
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1327 = typeof(BlackBlood);

		// Token: 0x0400206D RID: 8301
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1328 = typeof(BlackStar);

		// Token: 0x0400206E RID: 8302
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1329 = typeof(BlessedAntler);

		// Token: 0x0400206F RID: 8303
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1330 = typeof(BloodSoakedRose);

		// Token: 0x04002070 RID: 8304
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1331 = typeof(BloodVial);

		// Token: 0x04002071 RID: 8305
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1332 = typeof(BoneFlute);

		// Token: 0x04002072 RID: 8306
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1333 = typeof(BoneTea);

		// Token: 0x04002073 RID: 8307
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1334 = typeof(Bookmark);

		// Token: 0x04002074 RID: 8308
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1335 = typeof(BookOfFiveRings);

		// Token: 0x04002075 RID: 8309
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1336 = typeof(BookRepairKnife);

		// Token: 0x04002076 RID: 8310
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1337 = typeof(BoomingConch);

		// Token: 0x04002077 RID: 8311
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1338 = typeof(BoundPhylactery);

		// Token: 0x04002078 RID: 8312
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1339 = typeof(BowlerHat);

		// Token: 0x04002079 RID: 8313
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1340 = typeof(Bread);

		// Token: 0x0400207A RID: 8314
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1341 = typeof(BrilliantScarf);

		// Token: 0x0400207B RID: 8315
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1342 = typeof(Brimstone);

		// Token: 0x0400207C RID: 8316
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1343 = typeof(BronzeScales);

		// Token: 0x0400207D RID: 8317
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1344 = typeof(BurningBlood);

		// Token: 0x0400207E RID: 8318
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1345 = typeof(BurningSticks);

		// Token: 0x0400207F RID: 8319
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1346 = typeof(MegaCrit.Sts2.Core.Models.Relics.Byrdpip);

		// Token: 0x04002080 RID: 8320
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1347 = typeof(CallingBell);

		// Token: 0x04002081 RID: 8321
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1348 = typeof(Candelabra);

		// Token: 0x04002082 RID: 8322
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1349 = typeof(CaptainsWheel);

		// Token: 0x04002083 RID: 8323
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1350 = typeof(Cauldron);

		// Token: 0x04002084 RID: 8324
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1351 = typeof(CentennialPuzzle);

		// Token: 0x04002085 RID: 8325
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1352 = typeof(Chandelier);

		// Token: 0x04002086 RID: 8326
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1353 = typeof(CharonsAshes);

		// Token: 0x04002087 RID: 8327
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1354 = typeof(ChemicalX);

		// Token: 0x04002088 RID: 8328
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1355 = typeof(ChoicesParadox);

		// Token: 0x04002089 RID: 8329
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1356 = typeof(ChosenCheese);

		// Token: 0x0400208A RID: 8330
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1357 = typeof(Circlet);

		// Token: 0x0400208B RID: 8331
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1358 = typeof(Claws);

		// Token: 0x0400208C RID: 8332
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1359 = typeof(CloakClasp);

		// Token: 0x0400208D RID: 8333
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1360 = typeof(CrackedCore);

		// Token: 0x0400208E RID: 8334
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1361 = typeof(Crossbow);

		// Token: 0x0400208F RID: 8335
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1362 = typeof(CursedPearl);

		// Token: 0x04002090 RID: 8336
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1363 = typeof(DarkstonePeriapt);

		// Token: 0x04002091 RID: 8337
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1364 = typeof(DataDisk);

		// Token: 0x04002092 RID: 8338
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1365 = typeof(DaughterOfTheWind);

		// Token: 0x04002093 RID: 8339
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1366 = typeof(DelicateFrond);

		// Token: 0x04002094 RID: 8340
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1367 = typeof(DemonTongue);

		// Token: 0x04002095 RID: 8341
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1368 = typeof(DeprecatedRelic);

		// Token: 0x04002096 RID: 8342
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1369 = typeof(DiamondDiadem);

		// Token: 0x04002097 RID: 8343
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1370 = typeof(DingyRug);

		// Token: 0x04002098 RID: 8344
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1371 = typeof(DistinguishedCape);

		// Token: 0x04002099 RID: 8345
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1372 = typeof(DivineDestiny);

		// Token: 0x0400209A RID: 8346
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1373 = typeof(DivineRight);

		// Token: 0x0400209B RID: 8347
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1374 = typeof(DollysMirror);

		// Token: 0x0400209C RID: 8348
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1375 = typeof(DragonFruit);

		// Token: 0x0400209D RID: 8349
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1376 = typeof(DreamCatcher);

		// Token: 0x0400209E RID: 8350
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1377 = typeof(Driftwood);

		// Token: 0x0400209F RID: 8351
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1378 = typeof(DustyTome);

		// Token: 0x040020A0 RID: 8352
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1379 = typeof(Ectoplasm);

		// Token: 0x040020A1 RID: 8353
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1380 = typeof(ElectricShrymp);

		// Token: 0x040020A2 RID: 8354
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1381 = typeof(EmberTea);

		// Token: 0x040020A3 RID: 8355
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1382 = typeof(EmotionChip);

		// Token: 0x040020A4 RID: 8356
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1383 = typeof(EmptyCage);

		// Token: 0x040020A5 RID: 8357
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1384 = typeof(EternalFeather);

		// Token: 0x040020A6 RID: 8358
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1385 = typeof(FakeAnchor);

		// Token: 0x040020A7 RID: 8359
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1386 = typeof(FakeBloodVial);

		// Token: 0x040020A8 RID: 8360
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1387 = typeof(FakeHappyFlower);

		// Token: 0x040020A9 RID: 8361
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1388 = typeof(FakeLeesWaffle);

		// Token: 0x040020AA RID: 8362
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1389 = typeof(FakeMango);

		// Token: 0x040020AB RID: 8363
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1390 = typeof(FakeMerchantsRug);

		// Token: 0x040020AC RID: 8364
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1391 = typeof(FakeOrichalcum);

		// Token: 0x040020AD RID: 8365
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1392 = typeof(FakeSneckoEye);

		// Token: 0x040020AE RID: 8366
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1393 = typeof(FakeStrikeDummy);

		// Token: 0x040020AF RID: 8367
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1394 = typeof(FakeVenerableTeaSet);

		// Token: 0x040020B0 RID: 8368
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1395 = typeof(FencingManual);

		// Token: 0x040020B1 RID: 8369
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1396 = typeof(FestivePopper);

		// Token: 0x040020B2 RID: 8370
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1397 = typeof(Fiddle);

		// Token: 0x040020B3 RID: 8371
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1398 = typeof(ForgottenSoul);

		// Token: 0x040020B4 RID: 8372
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1399 = typeof(FragrantMushroom);

		// Token: 0x040020B5 RID: 8373
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1400 = typeof(FresnelLens);

		// Token: 0x040020B6 RID: 8374
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1401 = typeof(FrozenEgg);

		// Token: 0x040020B7 RID: 8375
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1402 = typeof(FuneraryMask);

		// Token: 0x040020B8 RID: 8376
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1403 = typeof(FurCoat);

		// Token: 0x040020B9 RID: 8377
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1404 = typeof(GalacticDust);

		// Token: 0x040020BA RID: 8378
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1405 = typeof(GamblingChip);

		// Token: 0x040020BB RID: 8379
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1406 = typeof(GamePiece);

		// Token: 0x040020BC RID: 8380
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1407 = typeof(GhostSeed);

		// Token: 0x040020BD RID: 8381
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1408 = typeof(Girya);

		// Token: 0x040020BE RID: 8382
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1409 = typeof(GlassEye);

		// Token: 0x040020BF RID: 8383
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1410 = typeof(Glitter);

		// Token: 0x040020C0 RID: 8384
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1411 = typeof(GnarledHammer);

		// Token: 0x040020C1 RID: 8385
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1412 = typeof(GoldenCompass);

		// Token: 0x040020C2 RID: 8386
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1413 = typeof(GoldenPearl);

		// Token: 0x040020C3 RID: 8387
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1414 = typeof(GoldPlatedCables);

		// Token: 0x040020C4 RID: 8388
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1415 = typeof(Gorget);

		// Token: 0x040020C5 RID: 8389
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1416 = typeof(GremlinHorn);

		// Token: 0x040020C6 RID: 8390
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1417 = typeof(HandDrill);

		// Token: 0x040020C7 RID: 8391
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1418 = typeof(HappyFlower);

		// Token: 0x040020C8 RID: 8392
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1419 = typeof(HelicalDart);

		// Token: 0x040020C9 RID: 8393
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1420 = typeof(HistoryCourse);

		// Token: 0x040020CA RID: 8394
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1421 = typeof(HornCleat);

		// Token: 0x040020CB RID: 8395
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1422 = typeof(IceCream);

		// Token: 0x040020CC RID: 8396
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1423 = typeof(InfusedCore);

		// Token: 0x040020CD RID: 8397
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1424 = typeof(IntimidatingHelmet);

		// Token: 0x040020CE RID: 8398
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1425 = typeof(IronClub);

		// Token: 0x040020CF RID: 8399
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1426 = typeof(IvoryTile);

		// Token: 0x040020D0 RID: 8400
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1427 = typeof(JeweledMask);

		// Token: 0x040020D1 RID: 8401
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1428 = typeof(JewelryBox);

		// Token: 0x040020D2 RID: 8402
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1429 = typeof(JossPaper);

		// Token: 0x040020D3 RID: 8403
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1430 = typeof(JuzuBracelet);

		// Token: 0x040020D4 RID: 8404
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1431 = typeof(Kifuda);

		// Token: 0x040020D5 RID: 8405
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1432 = typeof(Kunai);

		// Token: 0x040020D6 RID: 8406
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1433 = typeof(Kusarigama);

		// Token: 0x040020D7 RID: 8407
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1434 = typeof(Lantern);

		// Token: 0x040020D8 RID: 8408
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1435 = typeof(LargeCapsule);

		// Token: 0x040020D9 RID: 8409
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1436 = typeof(LastingCandy);

		// Token: 0x040020DA RID: 8410
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1437 = typeof(LavaLamp);

		// Token: 0x040020DB RID: 8411
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1438 = typeof(LavaRock);

		// Token: 0x040020DC RID: 8412
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1439 = typeof(LeadPaperweight);

		// Token: 0x040020DD RID: 8413
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1440 = typeof(LeafyPoultice);

		// Token: 0x040020DE RID: 8414
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1441 = typeof(LeesWaffle);

		// Token: 0x040020DF RID: 8415
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1442 = typeof(LetterOpener);

		// Token: 0x040020E0 RID: 8416
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1443 = typeof(LizardTail);

		// Token: 0x040020E1 RID: 8417
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1444 = typeof(LoomingFruit);

		// Token: 0x040020E2 RID: 8418
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1445 = typeof(LordsParasol);

		// Token: 0x040020E3 RID: 8419
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1446 = typeof(LostCoffer);

		// Token: 0x040020E4 RID: 8420
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1447 = typeof(MegaCrit.Sts2.Core.Models.Relics.LostWisp);

		// Token: 0x040020E5 RID: 8421
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1448 = typeof(LuckyFysh);

		// Token: 0x040020E6 RID: 8422
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1449 = typeof(LunarPastry);

		// Token: 0x040020E7 RID: 8423
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1450 = typeof(Mango);

		// Token: 0x040020E8 RID: 8424
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1451 = typeof(MassiveScroll);

		// Token: 0x040020E9 RID: 8425
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1452 = typeof(MawBank);

		// Token: 0x040020EA RID: 8426
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1453 = typeof(MealTicket);

		// Token: 0x040020EB RID: 8427
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1454 = typeof(MeatCleaver);

		// Token: 0x040020EC RID: 8428
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1455 = typeof(MeatOnTheBone);

		// Token: 0x040020ED RID: 8429
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1456 = typeof(MembershipCard);

		// Token: 0x040020EE RID: 8430
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1457 = typeof(MercuryHourglass);

		// Token: 0x040020EF RID: 8431
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1458 = typeof(Metronome);

		// Token: 0x040020F0 RID: 8432
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1459 = typeof(MiniatureCannon);

		// Token: 0x040020F1 RID: 8433
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1460 = typeof(MiniatureTent);

		// Token: 0x040020F2 RID: 8434
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1461 = typeof(MiniRegent);

		// Token: 0x040020F3 RID: 8435
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1462 = typeof(MoltenEgg);

		// Token: 0x040020F4 RID: 8436
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1463 = typeof(MrStruggles);

		// Token: 0x040020F5 RID: 8437
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1464 = typeof(MummifiedHand);

		// Token: 0x040020F6 RID: 8438
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1465 = typeof(MusicBox);

		// Token: 0x040020F7 RID: 8439
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1466 = typeof(MysticLighter);

		// Token: 0x040020F8 RID: 8440
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1467 = typeof(NeowsTorment);

		// Token: 0x040020F9 RID: 8441
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1468 = typeof(NewLeaf);

		// Token: 0x040020FA RID: 8442
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1469 = typeof(NinjaScroll);

		// Token: 0x040020FB RID: 8443
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1470 = typeof(Nunchaku);

		// Token: 0x040020FC RID: 8444
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1471 = typeof(NutritiousOyster);

		// Token: 0x040020FD RID: 8445
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1472 = typeof(NutritiousSoup);

		// Token: 0x040020FE RID: 8446
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1473 = typeof(OddlySmoothStone);

		// Token: 0x040020FF RID: 8447
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1474 = typeof(OldCoin);

		// Token: 0x04002100 RID: 8448
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1475 = typeof(OrangeDough);

		// Token: 0x04002101 RID: 8449
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1476 = typeof(Orichalcum);

		// Token: 0x04002102 RID: 8450
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1477 = typeof(OrnamentalFan);

		// Token: 0x04002103 RID: 8451
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1478 = typeof(Orrery);

		// Token: 0x04002104 RID: 8452
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1479 = typeof(PaelsBlood);

		// Token: 0x04002105 RID: 8453
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1480 = typeof(PaelsClaw);

		// Token: 0x04002106 RID: 8454
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1481 = typeof(PaelsEye);

		// Token: 0x04002107 RID: 8455
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1482 = typeof(PaelsFlesh);

		// Token: 0x04002108 RID: 8456
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1483 = typeof(PaelsGrowth);

		// Token: 0x04002109 RID: 8457
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1484 = typeof(PaelsHorn);

		// Token: 0x0400210A RID: 8458
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1485 = typeof(MegaCrit.Sts2.Core.Models.Relics.PaelsLegion);

		// Token: 0x0400210B RID: 8459
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1486 = typeof(PaelsTears);

		// Token: 0x0400210C RID: 8460
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1487 = typeof(PaelsTooth);

		// Token: 0x0400210D RID: 8461
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1488 = typeof(PaelsWing);

		// Token: 0x0400210E RID: 8462
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1489 = typeof(PandorasBox);

		// Token: 0x0400210F RID: 8463
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1490 = typeof(Pantograph);

		// Token: 0x04002110 RID: 8464
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1491 = typeof(PaperKrane);

		// Token: 0x04002111 RID: 8465
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1492 = typeof(PaperPhrog);

		// Token: 0x04002112 RID: 8466
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1493 = typeof(ParryingShield);

		// Token: 0x04002113 RID: 8467
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1494 = typeof(Pear);

		// Token: 0x04002114 RID: 8468
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1495 = typeof(Pendulum);

		// Token: 0x04002115 RID: 8469
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1496 = typeof(PenNib);

		// Token: 0x04002116 RID: 8470
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1497 = typeof(Permafrost);

		// Token: 0x04002117 RID: 8471
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1498 = typeof(PetrifiedToad);

		// Token: 0x04002118 RID: 8472
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1499 = typeof(PhilosophersStone);

		// Token: 0x04002119 RID: 8473
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1500 = typeof(PhylacteryUnbound);

		// Token: 0x0400211A RID: 8474
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1501 = typeof(Planisphere);

		// Token: 0x0400211B RID: 8475
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1502 = typeof(Pocketwatch);

		// Token: 0x0400211C RID: 8476
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1503 = typeof(PollinousCore);

		// Token: 0x0400211D RID: 8477
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1504 = typeof(Pomander);

		// Token: 0x0400211E RID: 8478
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1505 = typeof(PotionBelt);

		// Token: 0x0400211F RID: 8479
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1506 = typeof(PowerCell);

		// Token: 0x04002120 RID: 8480
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1507 = typeof(PrayerWheel);

		// Token: 0x04002121 RID: 8481
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1508 = typeof(PrecariousShears);

		// Token: 0x04002122 RID: 8482
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1509 = typeof(PreciseScissors);

		// Token: 0x04002123 RID: 8483
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1510 = typeof(PreservedFog);

		// Token: 0x04002124 RID: 8484
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1511 = typeof(PrismaticGem);

		// Token: 0x04002125 RID: 8485
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1512 = typeof(PumpkinCandle);

		// Token: 0x04002126 RID: 8486
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1513 = typeof(PunchDagger);

		// Token: 0x04002127 RID: 8487
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1514 = typeof(RadiantPearl);

		// Token: 0x04002128 RID: 8488
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1515 = typeof(RainbowRing);

		// Token: 0x04002129 RID: 8489
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1516 = typeof(RazorTooth);

		// Token: 0x0400212A RID: 8490
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1517 = typeof(RedMask);

		// Token: 0x0400212B RID: 8491
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1518 = typeof(RedSkull);

		// Token: 0x0400212C RID: 8492
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1519 = typeof(Regalite);

		// Token: 0x0400212D RID: 8493
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1520 = typeof(RegalPillow);

		// Token: 0x0400212E RID: 8494
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1521 = typeof(ReptileTrinket);

		// Token: 0x0400212F RID: 8495
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1522 = typeof(RingingTriangle);

		// Token: 0x04002130 RID: 8496
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1523 = typeof(RingOfTheDrake);

		// Token: 0x04002131 RID: 8497
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1524 = typeof(RingOfTheSnake);

		// Token: 0x04002132 RID: 8498
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1525 = typeof(RippleBasin);

		// Token: 0x04002133 RID: 8499
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1526 = typeof(RoyalPoison);

		// Token: 0x04002134 RID: 8500
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1527 = typeof(RoyalStamp);

		// Token: 0x04002135 RID: 8501
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1528 = typeof(RuinedHelmet);

		// Token: 0x04002136 RID: 8502
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1529 = typeof(RunicCapacitor);

		// Token: 0x04002137 RID: 8503
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1530 = typeof(RunicPyramid);

		// Token: 0x04002138 RID: 8504
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1531 = typeof(Sai);

		// Token: 0x04002139 RID: 8505
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1532 = typeof(SandCastle);

		// Token: 0x0400213A RID: 8506
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1533 = typeof(ScreamingFlagon);

		// Token: 0x0400213B RID: 8507
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1534 = typeof(ScrollBoxes);

		// Token: 0x0400213C RID: 8508
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1535 = typeof(SeaGlass);

		// Token: 0x0400213D RID: 8509
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1536 = typeof(SealOfGold);

		// Token: 0x0400213E RID: 8510
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1537 = typeof(SelfFormingClay);

		// Token: 0x0400213F RID: 8511
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1538 = typeof(SereTalon);

		// Token: 0x04002140 RID: 8512
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1539 = typeof(Shovel);

		// Token: 0x04002141 RID: 8513
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1540 = typeof(Shuriken);

		// Token: 0x04002142 RID: 8514
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1541 = typeof(SignetRing);

		// Token: 0x04002143 RID: 8515
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1542 = typeof(SilverCrucible);

		// Token: 0x04002144 RID: 8516
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1543 = typeof(SlingOfCourage);

		// Token: 0x04002145 RID: 8517
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1544 = typeof(SmallCapsule);

		// Token: 0x04002146 RID: 8518
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1545 = typeof(SneckoEye);

		// Token: 0x04002147 RID: 8519
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1546 = typeof(SneckoSkull);

		// Token: 0x04002148 RID: 8520
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1547 = typeof(Sozu);

		// Token: 0x04002149 RID: 8521
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1548 = typeof(SparklingRouge);

		// Token: 0x0400214A RID: 8522
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1549 = typeof(SpikedGauntlets);

		// Token: 0x0400214B RID: 8523
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1550 = typeof(StoneCalendar);

		// Token: 0x0400214C RID: 8524
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1551 = typeof(StoneCracker);

		// Token: 0x0400214D RID: 8525
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1552 = typeof(StoneHumidifier);

		// Token: 0x0400214E RID: 8526
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1553 = typeof(Storybook);

		// Token: 0x0400214F RID: 8527
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1554 = typeof(Strawberry);

		// Token: 0x04002150 RID: 8528
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1555 = typeof(StrikeDummy);

		// Token: 0x04002151 RID: 8529
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1556 = typeof(SturdyClamp);

		// Token: 0x04002152 RID: 8530
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1557 = typeof(SwordOfJade);

		// Token: 0x04002153 RID: 8531
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1558 = typeof(SwordOfStone);

		// Token: 0x04002154 RID: 8532
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1559 = typeof(SymbioticVirus);

		// Token: 0x04002155 RID: 8533
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1560 = typeof(TanxsWhistle);

		// Token: 0x04002156 RID: 8534
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1561 = typeof(TeaOfDiscourtesy);

		// Token: 0x04002157 RID: 8535
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1562 = typeof(TheAbacus);

		// Token: 0x04002158 RID: 8536
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1563 = typeof(TheBoot);

		// Token: 0x04002159 RID: 8537
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1564 = typeof(TheCourier);

		// Token: 0x0400215A RID: 8538
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1565 = typeof(ThrowingAxe);

		// Token: 0x0400215B RID: 8539
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1566 = typeof(Tingsha);

		// Token: 0x0400215C RID: 8540
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1567 = typeof(TinyMailbox);

		// Token: 0x0400215D RID: 8541
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1568 = typeof(ToastyMittens);

		// Token: 0x0400215E RID: 8542
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1569 = typeof(Toolbox);

		// Token: 0x0400215F RID: 8543
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1570 = typeof(TouchOfOrobas);

		// Token: 0x04002160 RID: 8544
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1571 = typeof(ToughBandages);

		// Token: 0x04002161 RID: 8545
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1572 = typeof(ToxicEgg);

		// Token: 0x04002162 RID: 8546
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1573 = typeof(ToyBox);

		// Token: 0x04002163 RID: 8547
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1574 = typeof(TriBoomerang);

		// Token: 0x04002164 RID: 8548
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1575 = typeof(TungstenRod);

		// Token: 0x04002165 RID: 8549
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1576 = typeof(TuningFork);

		// Token: 0x04002166 RID: 8550
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1577 = typeof(TwistedFunnel);

		// Token: 0x04002167 RID: 8551
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1578 = typeof(UnceasingTop);

		// Token: 0x04002168 RID: 8552
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1579 = typeof(UndyingSigil);

		// Token: 0x04002169 RID: 8553
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1580 = typeof(UnsettlingLamp);

		// Token: 0x0400216A RID: 8554
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1581 = typeof(Vajra);

		// Token: 0x0400216B RID: 8555
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1582 = typeof(Vambrace);

		// Token: 0x0400216C RID: 8556
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1583 = typeof(VelvetChoker);

		// Token: 0x0400216D RID: 8557
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1584 = typeof(VenerableTeaSet);

		// Token: 0x0400216E RID: 8558
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1585 = typeof(VeryHotCocoa);

		// Token: 0x0400216F RID: 8559
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1586 = typeof(VexingPuzzlebox);

		// Token: 0x04002170 RID: 8560
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1587 = typeof(VitruvianMinion);

		// Token: 0x04002171 RID: 8561
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1588 = typeof(WarHammer);

		// Token: 0x04002172 RID: 8562
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1589 = typeof(WarPaint);

		// Token: 0x04002173 RID: 8563
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1590 = typeof(Whetstone);

		// Token: 0x04002174 RID: 8564
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1591 = typeof(WhisperingEarring);

		// Token: 0x04002175 RID: 8565
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1592 = typeof(WhiteBeastStatue);

		// Token: 0x04002176 RID: 8566
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1593 = typeof(WhiteStar);

		// Token: 0x04002177 RID: 8567
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1594 = typeof(WingCharm);

		// Token: 0x04002178 RID: 8568
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1595 = typeof(WongoCustomerAppreciationBadge);

		// Token: 0x04002179 RID: 8569
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1596 = typeof(WongosMysteryTicket);

		// Token: 0x0400217A RID: 8570
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1597 = typeof(YummyCookie);

		// Token: 0x0400217B RID: 8571
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		private static readonly Type _t1598 = typeof(MultiplayerScalingModel);

		// Token: 0x0400217C RID: 8572
		private static readonly Type[] _subtypes = new Type[]
		{
			AbstractModelSubtypes._t0,
			AbstractModelSubtypes._t1,
			AbstractModelSubtypes._t2,
			AbstractModelSubtypes._t3,
			AbstractModelSubtypes._t4,
			AbstractModelSubtypes._t5,
			AbstractModelSubtypes._t6,
			AbstractModelSubtypes._t7,
			AbstractModelSubtypes._t8,
			AbstractModelSubtypes._t9,
			AbstractModelSubtypes._t10,
			AbstractModelSubtypes._t11,
			AbstractModelSubtypes._t12,
			AbstractModelSubtypes._t13,
			AbstractModelSubtypes._t14,
			AbstractModelSubtypes._t15,
			AbstractModelSubtypes._t16,
			AbstractModelSubtypes._t17,
			AbstractModelSubtypes._t18,
			AbstractModelSubtypes._t19,
			AbstractModelSubtypes._t20,
			AbstractModelSubtypes._t21,
			AbstractModelSubtypes._t22,
			AbstractModelSubtypes._t23,
			AbstractModelSubtypes._t24,
			AbstractModelSubtypes._t25,
			AbstractModelSubtypes._t26,
			AbstractModelSubtypes._t27,
			AbstractModelSubtypes._t28,
			AbstractModelSubtypes._t29,
			AbstractModelSubtypes._t30,
			AbstractModelSubtypes._t31,
			AbstractModelSubtypes._t32,
			AbstractModelSubtypes._t33,
			AbstractModelSubtypes._t34,
			AbstractModelSubtypes._t35,
			AbstractModelSubtypes._t36,
			AbstractModelSubtypes._t37,
			AbstractModelSubtypes._t38,
			AbstractModelSubtypes._t39,
			AbstractModelSubtypes._t40,
			AbstractModelSubtypes._t41,
			AbstractModelSubtypes._t42,
			AbstractModelSubtypes._t43,
			AbstractModelSubtypes._t44,
			AbstractModelSubtypes._t45,
			AbstractModelSubtypes._t46,
			AbstractModelSubtypes._t47,
			AbstractModelSubtypes._t48,
			AbstractModelSubtypes._t49,
			AbstractModelSubtypes._t50,
			AbstractModelSubtypes._t51,
			AbstractModelSubtypes._t52,
			AbstractModelSubtypes._t53,
			AbstractModelSubtypes._t54,
			AbstractModelSubtypes._t55,
			AbstractModelSubtypes._t56,
			AbstractModelSubtypes._t57,
			AbstractModelSubtypes._t58,
			AbstractModelSubtypes._t59,
			AbstractModelSubtypes._t60,
			AbstractModelSubtypes._t61,
			AbstractModelSubtypes._t62,
			AbstractModelSubtypes._t63,
			AbstractModelSubtypes._t64,
			AbstractModelSubtypes._t65,
			AbstractModelSubtypes._t66,
			AbstractModelSubtypes._t67,
			AbstractModelSubtypes._t68,
			AbstractModelSubtypes._t69,
			AbstractModelSubtypes._t70,
			AbstractModelSubtypes._t71,
			AbstractModelSubtypes._t72,
			AbstractModelSubtypes._t73,
			AbstractModelSubtypes._t74,
			AbstractModelSubtypes._t75,
			AbstractModelSubtypes._t76,
			AbstractModelSubtypes._t77,
			AbstractModelSubtypes._t78,
			AbstractModelSubtypes._t79,
			AbstractModelSubtypes._t80,
			AbstractModelSubtypes._t81,
			AbstractModelSubtypes._t82,
			AbstractModelSubtypes._t83,
			AbstractModelSubtypes._t84,
			AbstractModelSubtypes._t85,
			AbstractModelSubtypes._t86,
			AbstractModelSubtypes._t87,
			AbstractModelSubtypes._t88,
			AbstractModelSubtypes._t89,
			AbstractModelSubtypes._t90,
			AbstractModelSubtypes._t91,
			AbstractModelSubtypes._t92,
			AbstractModelSubtypes._t93,
			AbstractModelSubtypes._t94,
			AbstractModelSubtypes._t95,
			AbstractModelSubtypes._t96,
			AbstractModelSubtypes._t97,
			AbstractModelSubtypes._t98,
			AbstractModelSubtypes._t99,
			AbstractModelSubtypes._t100,
			AbstractModelSubtypes._t101,
			AbstractModelSubtypes._t102,
			AbstractModelSubtypes._t103,
			AbstractModelSubtypes._t104,
			AbstractModelSubtypes._t105,
			AbstractModelSubtypes._t106,
			AbstractModelSubtypes._t107,
			AbstractModelSubtypes._t108,
			AbstractModelSubtypes._t109,
			AbstractModelSubtypes._t110,
			AbstractModelSubtypes._t111,
			AbstractModelSubtypes._t112,
			AbstractModelSubtypes._t113,
			AbstractModelSubtypes._t114,
			AbstractModelSubtypes._t115,
			AbstractModelSubtypes._t116,
			AbstractModelSubtypes._t117,
			AbstractModelSubtypes._t118,
			AbstractModelSubtypes._t119,
			AbstractModelSubtypes._t120,
			AbstractModelSubtypes._t121,
			AbstractModelSubtypes._t122,
			AbstractModelSubtypes._t123,
			AbstractModelSubtypes._t124,
			AbstractModelSubtypes._t125,
			AbstractModelSubtypes._t126,
			AbstractModelSubtypes._t127,
			AbstractModelSubtypes._t128,
			AbstractModelSubtypes._t129,
			AbstractModelSubtypes._t130,
			AbstractModelSubtypes._t131,
			AbstractModelSubtypes._t132,
			AbstractModelSubtypes._t133,
			AbstractModelSubtypes._t134,
			AbstractModelSubtypes._t135,
			AbstractModelSubtypes._t136,
			AbstractModelSubtypes._t137,
			AbstractModelSubtypes._t138,
			AbstractModelSubtypes._t139,
			AbstractModelSubtypes._t140,
			AbstractModelSubtypes._t141,
			AbstractModelSubtypes._t142,
			AbstractModelSubtypes._t143,
			AbstractModelSubtypes._t144,
			AbstractModelSubtypes._t145,
			AbstractModelSubtypes._t146,
			AbstractModelSubtypes._t147,
			AbstractModelSubtypes._t148,
			AbstractModelSubtypes._t149,
			AbstractModelSubtypes._t150,
			AbstractModelSubtypes._t151,
			AbstractModelSubtypes._t152,
			AbstractModelSubtypes._t153,
			AbstractModelSubtypes._t154,
			AbstractModelSubtypes._t155,
			AbstractModelSubtypes._t156,
			AbstractModelSubtypes._t157,
			AbstractModelSubtypes._t158,
			AbstractModelSubtypes._t159,
			AbstractModelSubtypes._t160,
			AbstractModelSubtypes._t161,
			AbstractModelSubtypes._t162,
			AbstractModelSubtypes._t163,
			AbstractModelSubtypes._t164,
			AbstractModelSubtypes._t165,
			AbstractModelSubtypes._t166,
			AbstractModelSubtypes._t167,
			AbstractModelSubtypes._t168,
			AbstractModelSubtypes._t169,
			AbstractModelSubtypes._t170,
			AbstractModelSubtypes._t171,
			AbstractModelSubtypes._t172,
			AbstractModelSubtypes._t173,
			AbstractModelSubtypes._t174,
			AbstractModelSubtypes._t175,
			AbstractModelSubtypes._t176,
			AbstractModelSubtypes._t177,
			AbstractModelSubtypes._t178,
			AbstractModelSubtypes._t179,
			AbstractModelSubtypes._t180,
			AbstractModelSubtypes._t181,
			AbstractModelSubtypes._t182,
			AbstractModelSubtypes._t183,
			AbstractModelSubtypes._t184,
			AbstractModelSubtypes._t185,
			AbstractModelSubtypes._t186,
			AbstractModelSubtypes._t187,
			AbstractModelSubtypes._t188,
			AbstractModelSubtypes._t189,
			AbstractModelSubtypes._t190,
			AbstractModelSubtypes._t191,
			AbstractModelSubtypes._t192,
			AbstractModelSubtypes._t193,
			AbstractModelSubtypes._t194,
			AbstractModelSubtypes._t195,
			AbstractModelSubtypes._t196,
			AbstractModelSubtypes._t197,
			AbstractModelSubtypes._t198,
			AbstractModelSubtypes._t199,
			AbstractModelSubtypes._t200,
			AbstractModelSubtypes._t201,
			AbstractModelSubtypes._t202,
			AbstractModelSubtypes._t203,
			AbstractModelSubtypes._t204,
			AbstractModelSubtypes._t205,
			AbstractModelSubtypes._t206,
			AbstractModelSubtypes._t207,
			AbstractModelSubtypes._t208,
			AbstractModelSubtypes._t209,
			AbstractModelSubtypes._t210,
			AbstractModelSubtypes._t211,
			AbstractModelSubtypes._t212,
			AbstractModelSubtypes._t213,
			AbstractModelSubtypes._t214,
			AbstractModelSubtypes._t215,
			AbstractModelSubtypes._t216,
			AbstractModelSubtypes._t217,
			AbstractModelSubtypes._t218,
			AbstractModelSubtypes._t219,
			AbstractModelSubtypes._t220,
			AbstractModelSubtypes._t221,
			AbstractModelSubtypes._t222,
			AbstractModelSubtypes._t223,
			AbstractModelSubtypes._t224,
			AbstractModelSubtypes._t225,
			AbstractModelSubtypes._t226,
			AbstractModelSubtypes._t227,
			AbstractModelSubtypes._t228,
			AbstractModelSubtypes._t229,
			AbstractModelSubtypes._t230,
			AbstractModelSubtypes._t231,
			AbstractModelSubtypes._t232,
			AbstractModelSubtypes._t233,
			AbstractModelSubtypes._t234,
			AbstractModelSubtypes._t235,
			AbstractModelSubtypes._t236,
			AbstractModelSubtypes._t237,
			AbstractModelSubtypes._t238,
			AbstractModelSubtypes._t239,
			AbstractModelSubtypes._t240,
			AbstractModelSubtypes._t241,
			AbstractModelSubtypes._t242,
			AbstractModelSubtypes._t243,
			AbstractModelSubtypes._t244,
			AbstractModelSubtypes._t245,
			AbstractModelSubtypes._t246,
			AbstractModelSubtypes._t247,
			AbstractModelSubtypes._t248,
			AbstractModelSubtypes._t249,
			AbstractModelSubtypes._t250,
			AbstractModelSubtypes._t251,
			AbstractModelSubtypes._t252,
			AbstractModelSubtypes._t253,
			AbstractModelSubtypes._t254,
			AbstractModelSubtypes._t255,
			AbstractModelSubtypes._t256,
			AbstractModelSubtypes._t257,
			AbstractModelSubtypes._t258,
			AbstractModelSubtypes._t259,
			AbstractModelSubtypes._t260,
			AbstractModelSubtypes._t261,
			AbstractModelSubtypes._t262,
			AbstractModelSubtypes._t263,
			AbstractModelSubtypes._t264,
			AbstractModelSubtypes._t265,
			AbstractModelSubtypes._t266,
			AbstractModelSubtypes._t267,
			AbstractModelSubtypes._t268,
			AbstractModelSubtypes._t269,
			AbstractModelSubtypes._t270,
			AbstractModelSubtypes._t271,
			AbstractModelSubtypes._t272,
			AbstractModelSubtypes._t273,
			AbstractModelSubtypes._t274,
			AbstractModelSubtypes._t275,
			AbstractModelSubtypes._t276,
			AbstractModelSubtypes._t277,
			AbstractModelSubtypes._t278,
			AbstractModelSubtypes._t279,
			AbstractModelSubtypes._t280,
			AbstractModelSubtypes._t281,
			AbstractModelSubtypes._t282,
			AbstractModelSubtypes._t283,
			AbstractModelSubtypes._t284,
			AbstractModelSubtypes._t285,
			AbstractModelSubtypes._t286,
			AbstractModelSubtypes._t287,
			AbstractModelSubtypes._t288,
			AbstractModelSubtypes._t289,
			AbstractModelSubtypes._t290,
			AbstractModelSubtypes._t291,
			AbstractModelSubtypes._t292,
			AbstractModelSubtypes._t293,
			AbstractModelSubtypes._t294,
			AbstractModelSubtypes._t295,
			AbstractModelSubtypes._t296,
			AbstractModelSubtypes._t297,
			AbstractModelSubtypes._t298,
			AbstractModelSubtypes._t299,
			AbstractModelSubtypes._t300,
			AbstractModelSubtypes._t301,
			AbstractModelSubtypes._t302,
			AbstractModelSubtypes._t303,
			AbstractModelSubtypes._t304,
			AbstractModelSubtypes._t305,
			AbstractModelSubtypes._t306,
			AbstractModelSubtypes._t307,
			AbstractModelSubtypes._t308,
			AbstractModelSubtypes._t309,
			AbstractModelSubtypes._t310,
			AbstractModelSubtypes._t311,
			AbstractModelSubtypes._t312,
			AbstractModelSubtypes._t313,
			AbstractModelSubtypes._t314,
			AbstractModelSubtypes._t315,
			AbstractModelSubtypes._t316,
			AbstractModelSubtypes._t317,
			AbstractModelSubtypes._t318,
			AbstractModelSubtypes._t319,
			AbstractModelSubtypes._t320,
			AbstractModelSubtypes._t321,
			AbstractModelSubtypes._t322,
			AbstractModelSubtypes._t323,
			AbstractModelSubtypes._t324,
			AbstractModelSubtypes._t325,
			AbstractModelSubtypes._t326,
			AbstractModelSubtypes._t327,
			AbstractModelSubtypes._t328,
			AbstractModelSubtypes._t329,
			AbstractModelSubtypes._t330,
			AbstractModelSubtypes._t331,
			AbstractModelSubtypes._t332,
			AbstractModelSubtypes._t333,
			AbstractModelSubtypes._t334,
			AbstractModelSubtypes._t335,
			AbstractModelSubtypes._t336,
			AbstractModelSubtypes._t337,
			AbstractModelSubtypes._t338,
			AbstractModelSubtypes._t339,
			AbstractModelSubtypes._t340,
			AbstractModelSubtypes._t341,
			AbstractModelSubtypes._t342,
			AbstractModelSubtypes._t343,
			AbstractModelSubtypes._t344,
			AbstractModelSubtypes._t345,
			AbstractModelSubtypes._t346,
			AbstractModelSubtypes._t347,
			AbstractModelSubtypes._t348,
			AbstractModelSubtypes._t349,
			AbstractModelSubtypes._t350,
			AbstractModelSubtypes._t351,
			AbstractModelSubtypes._t352,
			AbstractModelSubtypes._t353,
			AbstractModelSubtypes._t354,
			AbstractModelSubtypes._t355,
			AbstractModelSubtypes._t356,
			AbstractModelSubtypes._t357,
			AbstractModelSubtypes._t358,
			AbstractModelSubtypes._t359,
			AbstractModelSubtypes._t360,
			AbstractModelSubtypes._t361,
			AbstractModelSubtypes._t362,
			AbstractModelSubtypes._t363,
			AbstractModelSubtypes._t364,
			AbstractModelSubtypes._t365,
			AbstractModelSubtypes._t366,
			AbstractModelSubtypes._t367,
			AbstractModelSubtypes._t368,
			AbstractModelSubtypes._t369,
			AbstractModelSubtypes._t370,
			AbstractModelSubtypes._t371,
			AbstractModelSubtypes._t372,
			AbstractModelSubtypes._t373,
			AbstractModelSubtypes._t374,
			AbstractModelSubtypes._t375,
			AbstractModelSubtypes._t376,
			AbstractModelSubtypes._t377,
			AbstractModelSubtypes._t378,
			AbstractModelSubtypes._t379,
			AbstractModelSubtypes._t380,
			AbstractModelSubtypes._t381,
			AbstractModelSubtypes._t382,
			AbstractModelSubtypes._t383,
			AbstractModelSubtypes._t384,
			AbstractModelSubtypes._t385,
			AbstractModelSubtypes._t386,
			AbstractModelSubtypes._t387,
			AbstractModelSubtypes._t388,
			AbstractModelSubtypes._t389,
			AbstractModelSubtypes._t390,
			AbstractModelSubtypes._t391,
			AbstractModelSubtypes._t392,
			AbstractModelSubtypes._t393,
			AbstractModelSubtypes._t394,
			AbstractModelSubtypes._t395,
			AbstractModelSubtypes._t396,
			AbstractModelSubtypes._t397,
			AbstractModelSubtypes._t398,
			AbstractModelSubtypes._t399,
			AbstractModelSubtypes._t400,
			AbstractModelSubtypes._t401,
			AbstractModelSubtypes._t402,
			AbstractModelSubtypes._t403,
			AbstractModelSubtypes._t404,
			AbstractModelSubtypes._t405,
			AbstractModelSubtypes._t406,
			AbstractModelSubtypes._t407,
			AbstractModelSubtypes._t408,
			AbstractModelSubtypes._t409,
			AbstractModelSubtypes._t410,
			AbstractModelSubtypes._t411,
			AbstractModelSubtypes._t412,
			AbstractModelSubtypes._t413,
			AbstractModelSubtypes._t414,
			AbstractModelSubtypes._t415,
			AbstractModelSubtypes._t416,
			AbstractModelSubtypes._t417,
			AbstractModelSubtypes._t418,
			AbstractModelSubtypes._t419,
			AbstractModelSubtypes._t420,
			AbstractModelSubtypes._t421,
			AbstractModelSubtypes._t422,
			AbstractModelSubtypes._t423,
			AbstractModelSubtypes._t424,
			AbstractModelSubtypes._t425,
			AbstractModelSubtypes._t426,
			AbstractModelSubtypes._t427,
			AbstractModelSubtypes._t428,
			AbstractModelSubtypes._t429,
			AbstractModelSubtypes._t430,
			AbstractModelSubtypes._t431,
			AbstractModelSubtypes._t432,
			AbstractModelSubtypes._t433,
			AbstractModelSubtypes._t434,
			AbstractModelSubtypes._t435,
			AbstractModelSubtypes._t436,
			AbstractModelSubtypes._t437,
			AbstractModelSubtypes._t438,
			AbstractModelSubtypes._t439,
			AbstractModelSubtypes._t440,
			AbstractModelSubtypes._t441,
			AbstractModelSubtypes._t442,
			AbstractModelSubtypes._t443,
			AbstractModelSubtypes._t444,
			AbstractModelSubtypes._t445,
			AbstractModelSubtypes._t446,
			AbstractModelSubtypes._t447,
			AbstractModelSubtypes._t448,
			AbstractModelSubtypes._t449,
			AbstractModelSubtypes._t450,
			AbstractModelSubtypes._t451,
			AbstractModelSubtypes._t452,
			AbstractModelSubtypes._t453,
			AbstractModelSubtypes._t454,
			AbstractModelSubtypes._t455,
			AbstractModelSubtypes._t456,
			AbstractModelSubtypes._t457,
			AbstractModelSubtypes._t458,
			AbstractModelSubtypes._t459,
			AbstractModelSubtypes._t460,
			AbstractModelSubtypes._t461,
			AbstractModelSubtypes._t462,
			AbstractModelSubtypes._t463,
			AbstractModelSubtypes._t464,
			AbstractModelSubtypes._t465,
			AbstractModelSubtypes._t466,
			AbstractModelSubtypes._t467,
			AbstractModelSubtypes._t468,
			AbstractModelSubtypes._t469,
			AbstractModelSubtypes._t470,
			AbstractModelSubtypes._t471,
			AbstractModelSubtypes._t472,
			AbstractModelSubtypes._t473,
			AbstractModelSubtypes._t474,
			AbstractModelSubtypes._t475,
			AbstractModelSubtypes._t476,
			AbstractModelSubtypes._t477,
			AbstractModelSubtypes._t478,
			AbstractModelSubtypes._t479,
			AbstractModelSubtypes._t480,
			AbstractModelSubtypes._t481,
			AbstractModelSubtypes._t482,
			AbstractModelSubtypes._t483,
			AbstractModelSubtypes._t484,
			AbstractModelSubtypes._t485,
			AbstractModelSubtypes._t486,
			AbstractModelSubtypes._t487,
			AbstractModelSubtypes._t488,
			AbstractModelSubtypes._t489,
			AbstractModelSubtypes._t490,
			AbstractModelSubtypes._t491,
			AbstractModelSubtypes._t492,
			AbstractModelSubtypes._t493,
			AbstractModelSubtypes._t494,
			AbstractModelSubtypes._t495,
			AbstractModelSubtypes._t496,
			AbstractModelSubtypes._t497,
			AbstractModelSubtypes._t498,
			AbstractModelSubtypes._t499,
			AbstractModelSubtypes._t500,
			AbstractModelSubtypes._t501,
			AbstractModelSubtypes._t502,
			AbstractModelSubtypes._t503,
			AbstractModelSubtypes._t504,
			AbstractModelSubtypes._t505,
			AbstractModelSubtypes._t506,
			AbstractModelSubtypes._t507,
			AbstractModelSubtypes._t508,
			AbstractModelSubtypes._t509,
			AbstractModelSubtypes._t510,
			AbstractModelSubtypes._t511,
			AbstractModelSubtypes._t512,
			AbstractModelSubtypes._t513,
			AbstractModelSubtypes._t514,
			AbstractModelSubtypes._t515,
			AbstractModelSubtypes._t516,
			AbstractModelSubtypes._t517,
			AbstractModelSubtypes._t518,
			AbstractModelSubtypes._t519,
			AbstractModelSubtypes._t520,
			AbstractModelSubtypes._t521,
			AbstractModelSubtypes._t522,
			AbstractModelSubtypes._t523,
			AbstractModelSubtypes._t524,
			AbstractModelSubtypes._t525,
			AbstractModelSubtypes._t526,
			AbstractModelSubtypes._t527,
			AbstractModelSubtypes._t528,
			AbstractModelSubtypes._t529,
			AbstractModelSubtypes._t530,
			AbstractModelSubtypes._t531,
			AbstractModelSubtypes._t532,
			AbstractModelSubtypes._t533,
			AbstractModelSubtypes._t534,
			AbstractModelSubtypes._t535,
			AbstractModelSubtypes._t536,
			AbstractModelSubtypes._t537,
			AbstractModelSubtypes._t538,
			AbstractModelSubtypes._t539,
			AbstractModelSubtypes._t540,
			AbstractModelSubtypes._t541,
			AbstractModelSubtypes._t542,
			AbstractModelSubtypes._t543,
			AbstractModelSubtypes._t544,
			AbstractModelSubtypes._t545,
			AbstractModelSubtypes._t546,
			AbstractModelSubtypes._t547,
			AbstractModelSubtypes._t548,
			AbstractModelSubtypes._t549,
			AbstractModelSubtypes._t550,
			AbstractModelSubtypes._t551,
			AbstractModelSubtypes._t552,
			AbstractModelSubtypes._t553,
			AbstractModelSubtypes._t554,
			AbstractModelSubtypes._t555,
			AbstractModelSubtypes._t556,
			AbstractModelSubtypes._t557,
			AbstractModelSubtypes._t558,
			AbstractModelSubtypes._t559,
			AbstractModelSubtypes._t560,
			AbstractModelSubtypes._t561,
			AbstractModelSubtypes._t562,
			AbstractModelSubtypes._t563,
			AbstractModelSubtypes._t564,
			AbstractModelSubtypes._t565,
			AbstractModelSubtypes._t566,
			AbstractModelSubtypes._t567,
			AbstractModelSubtypes._t568,
			AbstractModelSubtypes._t569,
			AbstractModelSubtypes._t570,
			AbstractModelSubtypes._t571,
			AbstractModelSubtypes._t572,
			AbstractModelSubtypes._t573,
			AbstractModelSubtypes._t574,
			AbstractModelSubtypes._t575,
			AbstractModelSubtypes._t576,
			AbstractModelSubtypes._t577,
			AbstractModelSubtypes._t578,
			AbstractModelSubtypes._t579,
			AbstractModelSubtypes._t580,
			AbstractModelSubtypes._t581,
			AbstractModelSubtypes._t582,
			AbstractModelSubtypes._t583,
			AbstractModelSubtypes._t584,
			AbstractModelSubtypes._t585,
			AbstractModelSubtypes._t586,
			AbstractModelSubtypes._t587,
			AbstractModelSubtypes._t588,
			AbstractModelSubtypes._t589,
			AbstractModelSubtypes._t590,
			AbstractModelSubtypes._t591,
			AbstractModelSubtypes._t592,
			AbstractModelSubtypes._t593,
			AbstractModelSubtypes._t594,
			AbstractModelSubtypes._t595,
			AbstractModelSubtypes._t596,
			AbstractModelSubtypes._t597,
			AbstractModelSubtypes._t598,
			AbstractModelSubtypes._t599,
			AbstractModelSubtypes._t600,
			AbstractModelSubtypes._t601,
			AbstractModelSubtypes._t602,
			AbstractModelSubtypes._t603,
			AbstractModelSubtypes._t604,
			AbstractModelSubtypes._t605,
			AbstractModelSubtypes._t606,
			AbstractModelSubtypes._t607,
			AbstractModelSubtypes._t608,
			AbstractModelSubtypes._t609,
			AbstractModelSubtypes._t610,
			AbstractModelSubtypes._t611,
			AbstractModelSubtypes._t612,
			AbstractModelSubtypes._t613,
			AbstractModelSubtypes._t614,
			AbstractModelSubtypes._t615,
			AbstractModelSubtypes._t616,
			AbstractModelSubtypes._t617,
			AbstractModelSubtypes._t618,
			AbstractModelSubtypes._t619,
			AbstractModelSubtypes._t620,
			AbstractModelSubtypes._t621,
			AbstractModelSubtypes._t622,
			AbstractModelSubtypes._t623,
			AbstractModelSubtypes._t624,
			AbstractModelSubtypes._t625,
			AbstractModelSubtypes._t626,
			AbstractModelSubtypes._t627,
			AbstractModelSubtypes._t628,
			AbstractModelSubtypes._t629,
			AbstractModelSubtypes._t630,
			AbstractModelSubtypes._t631,
			AbstractModelSubtypes._t632,
			AbstractModelSubtypes._t633,
			AbstractModelSubtypes._t634,
			AbstractModelSubtypes._t635,
			AbstractModelSubtypes._t636,
			AbstractModelSubtypes._t637,
			AbstractModelSubtypes._t638,
			AbstractModelSubtypes._t639,
			AbstractModelSubtypes._t640,
			AbstractModelSubtypes._t641,
			AbstractModelSubtypes._t642,
			AbstractModelSubtypes._t643,
			AbstractModelSubtypes._t644,
			AbstractModelSubtypes._t645,
			AbstractModelSubtypes._t646,
			AbstractModelSubtypes._t647,
			AbstractModelSubtypes._t648,
			AbstractModelSubtypes._t649,
			AbstractModelSubtypes._t650,
			AbstractModelSubtypes._t651,
			AbstractModelSubtypes._t652,
			AbstractModelSubtypes._t653,
			AbstractModelSubtypes._t654,
			AbstractModelSubtypes._t655,
			AbstractModelSubtypes._t656,
			AbstractModelSubtypes._t657,
			AbstractModelSubtypes._t658,
			AbstractModelSubtypes._t659,
			AbstractModelSubtypes._t660,
			AbstractModelSubtypes._t661,
			AbstractModelSubtypes._t662,
			AbstractModelSubtypes._t663,
			AbstractModelSubtypes._t664,
			AbstractModelSubtypes._t665,
			AbstractModelSubtypes._t666,
			AbstractModelSubtypes._t667,
			AbstractModelSubtypes._t668,
			AbstractModelSubtypes._t669,
			AbstractModelSubtypes._t670,
			AbstractModelSubtypes._t671,
			AbstractModelSubtypes._t672,
			AbstractModelSubtypes._t673,
			AbstractModelSubtypes._t674,
			AbstractModelSubtypes._t675,
			AbstractModelSubtypes._t676,
			AbstractModelSubtypes._t677,
			AbstractModelSubtypes._t678,
			AbstractModelSubtypes._t679,
			AbstractModelSubtypes._t680,
			AbstractModelSubtypes._t681,
			AbstractModelSubtypes._t682,
			AbstractModelSubtypes._t683,
			AbstractModelSubtypes._t684,
			AbstractModelSubtypes._t685,
			AbstractModelSubtypes._t686,
			AbstractModelSubtypes._t687,
			AbstractModelSubtypes._t688,
			AbstractModelSubtypes._t689,
			AbstractModelSubtypes._t690,
			AbstractModelSubtypes._t691,
			AbstractModelSubtypes._t692,
			AbstractModelSubtypes._t693,
			AbstractModelSubtypes._t694,
			AbstractModelSubtypes._t695,
			AbstractModelSubtypes._t696,
			AbstractModelSubtypes._t697,
			AbstractModelSubtypes._t698,
			AbstractModelSubtypes._t699,
			AbstractModelSubtypes._t700,
			AbstractModelSubtypes._t701,
			AbstractModelSubtypes._t702,
			AbstractModelSubtypes._t703,
			AbstractModelSubtypes._t704,
			AbstractModelSubtypes._t705,
			AbstractModelSubtypes._t706,
			AbstractModelSubtypes._t707,
			AbstractModelSubtypes._t708,
			AbstractModelSubtypes._t709,
			AbstractModelSubtypes._t710,
			AbstractModelSubtypes._t711,
			AbstractModelSubtypes._t712,
			AbstractModelSubtypes._t713,
			AbstractModelSubtypes._t714,
			AbstractModelSubtypes._t715,
			AbstractModelSubtypes._t716,
			AbstractModelSubtypes._t717,
			AbstractModelSubtypes._t718,
			AbstractModelSubtypes._t719,
			AbstractModelSubtypes._t720,
			AbstractModelSubtypes._t721,
			AbstractModelSubtypes._t722,
			AbstractModelSubtypes._t723,
			AbstractModelSubtypes._t724,
			AbstractModelSubtypes._t725,
			AbstractModelSubtypes._t726,
			AbstractModelSubtypes._t727,
			AbstractModelSubtypes._t728,
			AbstractModelSubtypes._t729,
			AbstractModelSubtypes._t730,
			AbstractModelSubtypes._t731,
			AbstractModelSubtypes._t732,
			AbstractModelSubtypes._t733,
			AbstractModelSubtypes._t734,
			AbstractModelSubtypes._t735,
			AbstractModelSubtypes._t736,
			AbstractModelSubtypes._t737,
			AbstractModelSubtypes._t738,
			AbstractModelSubtypes._t739,
			AbstractModelSubtypes._t740,
			AbstractModelSubtypes._t741,
			AbstractModelSubtypes._t742,
			AbstractModelSubtypes._t743,
			AbstractModelSubtypes._t744,
			AbstractModelSubtypes._t745,
			AbstractModelSubtypes._t746,
			AbstractModelSubtypes._t747,
			AbstractModelSubtypes._t748,
			AbstractModelSubtypes._t749,
			AbstractModelSubtypes._t750,
			AbstractModelSubtypes._t751,
			AbstractModelSubtypes._t752,
			AbstractModelSubtypes._t753,
			AbstractModelSubtypes._t754,
			AbstractModelSubtypes._t755,
			AbstractModelSubtypes._t756,
			AbstractModelSubtypes._t757,
			AbstractModelSubtypes._t758,
			AbstractModelSubtypes._t759,
			AbstractModelSubtypes._t760,
			AbstractModelSubtypes._t761,
			AbstractModelSubtypes._t762,
			AbstractModelSubtypes._t763,
			AbstractModelSubtypes._t764,
			AbstractModelSubtypes._t765,
			AbstractModelSubtypes._t766,
			AbstractModelSubtypes._t767,
			AbstractModelSubtypes._t768,
			AbstractModelSubtypes._t769,
			AbstractModelSubtypes._t770,
			AbstractModelSubtypes._t771,
			AbstractModelSubtypes._t772,
			AbstractModelSubtypes._t773,
			AbstractModelSubtypes._t774,
			AbstractModelSubtypes._t775,
			AbstractModelSubtypes._t776,
			AbstractModelSubtypes._t777,
			AbstractModelSubtypes._t778,
			AbstractModelSubtypes._t779,
			AbstractModelSubtypes._t780,
			AbstractModelSubtypes._t781,
			AbstractModelSubtypes._t782,
			AbstractModelSubtypes._t783,
			AbstractModelSubtypes._t784,
			AbstractModelSubtypes._t785,
			AbstractModelSubtypes._t786,
			AbstractModelSubtypes._t787,
			AbstractModelSubtypes._t788,
			AbstractModelSubtypes._t789,
			AbstractModelSubtypes._t790,
			AbstractModelSubtypes._t791,
			AbstractModelSubtypes._t792,
			AbstractModelSubtypes._t793,
			AbstractModelSubtypes._t794,
			AbstractModelSubtypes._t795,
			AbstractModelSubtypes._t796,
			AbstractModelSubtypes._t797,
			AbstractModelSubtypes._t798,
			AbstractModelSubtypes._t799,
			AbstractModelSubtypes._t800,
			AbstractModelSubtypes._t801,
			AbstractModelSubtypes._t802,
			AbstractModelSubtypes._t803,
			AbstractModelSubtypes._t804,
			AbstractModelSubtypes._t805,
			AbstractModelSubtypes._t806,
			AbstractModelSubtypes._t807,
			AbstractModelSubtypes._t808,
			AbstractModelSubtypes._t809,
			AbstractModelSubtypes._t810,
			AbstractModelSubtypes._t811,
			AbstractModelSubtypes._t812,
			AbstractModelSubtypes._t813,
			AbstractModelSubtypes._t814,
			AbstractModelSubtypes._t815,
			AbstractModelSubtypes._t816,
			AbstractModelSubtypes._t817,
			AbstractModelSubtypes._t818,
			AbstractModelSubtypes._t819,
			AbstractModelSubtypes._t820,
			AbstractModelSubtypes._t821,
			AbstractModelSubtypes._t822,
			AbstractModelSubtypes._t823,
			AbstractModelSubtypes._t824,
			AbstractModelSubtypes._t825,
			AbstractModelSubtypes._t826,
			AbstractModelSubtypes._t827,
			AbstractModelSubtypes._t828,
			AbstractModelSubtypes._t829,
			AbstractModelSubtypes._t830,
			AbstractModelSubtypes._t831,
			AbstractModelSubtypes._t832,
			AbstractModelSubtypes._t833,
			AbstractModelSubtypes._t834,
			AbstractModelSubtypes._t835,
			AbstractModelSubtypes._t836,
			AbstractModelSubtypes._t837,
			AbstractModelSubtypes._t838,
			AbstractModelSubtypes._t839,
			AbstractModelSubtypes._t840,
			AbstractModelSubtypes._t841,
			AbstractModelSubtypes._t842,
			AbstractModelSubtypes._t843,
			AbstractModelSubtypes._t844,
			AbstractModelSubtypes._t845,
			AbstractModelSubtypes._t846,
			AbstractModelSubtypes._t847,
			AbstractModelSubtypes._t848,
			AbstractModelSubtypes._t849,
			AbstractModelSubtypes._t850,
			AbstractModelSubtypes._t851,
			AbstractModelSubtypes._t852,
			AbstractModelSubtypes._t853,
			AbstractModelSubtypes._t854,
			AbstractModelSubtypes._t855,
			AbstractModelSubtypes._t856,
			AbstractModelSubtypes._t857,
			AbstractModelSubtypes._t858,
			AbstractModelSubtypes._t859,
			AbstractModelSubtypes._t860,
			AbstractModelSubtypes._t861,
			AbstractModelSubtypes._t862,
			AbstractModelSubtypes._t863,
			AbstractModelSubtypes._t864,
			AbstractModelSubtypes._t865,
			AbstractModelSubtypes._t866,
			AbstractModelSubtypes._t867,
			AbstractModelSubtypes._t868,
			AbstractModelSubtypes._t869,
			AbstractModelSubtypes._t870,
			AbstractModelSubtypes._t871,
			AbstractModelSubtypes._t872,
			AbstractModelSubtypes._t873,
			AbstractModelSubtypes._t874,
			AbstractModelSubtypes._t875,
			AbstractModelSubtypes._t876,
			AbstractModelSubtypes._t877,
			AbstractModelSubtypes._t878,
			AbstractModelSubtypes._t879,
			AbstractModelSubtypes._t880,
			AbstractModelSubtypes._t881,
			AbstractModelSubtypes._t882,
			AbstractModelSubtypes._t883,
			AbstractModelSubtypes._t884,
			AbstractModelSubtypes._t885,
			AbstractModelSubtypes._t886,
			AbstractModelSubtypes._t887,
			AbstractModelSubtypes._t888,
			AbstractModelSubtypes._t889,
			AbstractModelSubtypes._t890,
			AbstractModelSubtypes._t891,
			AbstractModelSubtypes._t892,
			AbstractModelSubtypes._t893,
			AbstractModelSubtypes._t894,
			AbstractModelSubtypes._t895,
			AbstractModelSubtypes._t896,
			AbstractModelSubtypes._t897,
			AbstractModelSubtypes._t898,
			AbstractModelSubtypes._t899,
			AbstractModelSubtypes._t900,
			AbstractModelSubtypes._t901,
			AbstractModelSubtypes._t902,
			AbstractModelSubtypes._t903,
			AbstractModelSubtypes._t904,
			AbstractModelSubtypes._t905,
			AbstractModelSubtypes._t906,
			AbstractModelSubtypes._t907,
			AbstractModelSubtypes._t908,
			AbstractModelSubtypes._t909,
			AbstractModelSubtypes._t910,
			AbstractModelSubtypes._t911,
			AbstractModelSubtypes._t912,
			AbstractModelSubtypes._t913,
			AbstractModelSubtypes._t914,
			AbstractModelSubtypes._t915,
			AbstractModelSubtypes._t916,
			AbstractModelSubtypes._t917,
			AbstractModelSubtypes._t918,
			AbstractModelSubtypes._t919,
			AbstractModelSubtypes._t920,
			AbstractModelSubtypes._t921,
			AbstractModelSubtypes._t922,
			AbstractModelSubtypes._t923,
			AbstractModelSubtypes._t924,
			AbstractModelSubtypes._t925,
			AbstractModelSubtypes._t926,
			AbstractModelSubtypes._t927,
			AbstractModelSubtypes._t928,
			AbstractModelSubtypes._t929,
			AbstractModelSubtypes._t930,
			AbstractModelSubtypes._t931,
			AbstractModelSubtypes._t932,
			AbstractModelSubtypes._t933,
			AbstractModelSubtypes._t934,
			AbstractModelSubtypes._t935,
			AbstractModelSubtypes._t936,
			AbstractModelSubtypes._t937,
			AbstractModelSubtypes._t938,
			AbstractModelSubtypes._t939,
			AbstractModelSubtypes._t940,
			AbstractModelSubtypes._t941,
			AbstractModelSubtypes._t942,
			AbstractModelSubtypes._t943,
			AbstractModelSubtypes._t944,
			AbstractModelSubtypes._t945,
			AbstractModelSubtypes._t946,
			AbstractModelSubtypes._t947,
			AbstractModelSubtypes._t948,
			AbstractModelSubtypes._t949,
			AbstractModelSubtypes._t950,
			AbstractModelSubtypes._t951,
			AbstractModelSubtypes._t952,
			AbstractModelSubtypes._t953,
			AbstractModelSubtypes._t954,
			AbstractModelSubtypes._t955,
			AbstractModelSubtypes._t956,
			AbstractModelSubtypes._t957,
			AbstractModelSubtypes._t958,
			AbstractModelSubtypes._t959,
			AbstractModelSubtypes._t960,
			AbstractModelSubtypes._t961,
			AbstractModelSubtypes._t962,
			AbstractModelSubtypes._t963,
			AbstractModelSubtypes._t964,
			AbstractModelSubtypes._t965,
			AbstractModelSubtypes._t966,
			AbstractModelSubtypes._t967,
			AbstractModelSubtypes._t968,
			AbstractModelSubtypes._t969,
			AbstractModelSubtypes._t970,
			AbstractModelSubtypes._t971,
			AbstractModelSubtypes._t972,
			AbstractModelSubtypes._t973,
			AbstractModelSubtypes._t974,
			AbstractModelSubtypes._t975,
			AbstractModelSubtypes._t976,
			AbstractModelSubtypes._t977,
			AbstractModelSubtypes._t978,
			AbstractModelSubtypes._t979,
			AbstractModelSubtypes._t980,
			AbstractModelSubtypes._t981,
			AbstractModelSubtypes._t982,
			AbstractModelSubtypes._t983,
			AbstractModelSubtypes._t984,
			AbstractModelSubtypes._t985,
			AbstractModelSubtypes._t986,
			AbstractModelSubtypes._t987,
			AbstractModelSubtypes._t988,
			AbstractModelSubtypes._t989,
			AbstractModelSubtypes._t990,
			AbstractModelSubtypes._t991,
			AbstractModelSubtypes._t992,
			AbstractModelSubtypes._t993,
			AbstractModelSubtypes._t994,
			AbstractModelSubtypes._t995,
			AbstractModelSubtypes._t996,
			AbstractModelSubtypes._t997,
			AbstractModelSubtypes._t998,
			AbstractModelSubtypes._t999,
			AbstractModelSubtypes._t1000,
			AbstractModelSubtypes._t1001,
			AbstractModelSubtypes._t1002,
			AbstractModelSubtypes._t1003,
			AbstractModelSubtypes._t1004,
			AbstractModelSubtypes._t1005,
			AbstractModelSubtypes._t1006,
			AbstractModelSubtypes._t1007,
			AbstractModelSubtypes._t1008,
			AbstractModelSubtypes._t1009,
			AbstractModelSubtypes._t1010,
			AbstractModelSubtypes._t1011,
			AbstractModelSubtypes._t1012,
			AbstractModelSubtypes._t1013,
			AbstractModelSubtypes._t1014,
			AbstractModelSubtypes._t1015,
			AbstractModelSubtypes._t1016,
			AbstractModelSubtypes._t1017,
			AbstractModelSubtypes._t1018,
			AbstractModelSubtypes._t1019,
			AbstractModelSubtypes._t1020,
			AbstractModelSubtypes._t1021,
			AbstractModelSubtypes._t1022,
			AbstractModelSubtypes._t1023,
			AbstractModelSubtypes._t1024,
			AbstractModelSubtypes._t1025,
			AbstractModelSubtypes._t1026,
			AbstractModelSubtypes._t1027,
			AbstractModelSubtypes._t1028,
			AbstractModelSubtypes._t1029,
			AbstractModelSubtypes._t1030,
			AbstractModelSubtypes._t1031,
			AbstractModelSubtypes._t1032,
			AbstractModelSubtypes._t1033,
			AbstractModelSubtypes._t1034,
			AbstractModelSubtypes._t1035,
			AbstractModelSubtypes._t1036,
			AbstractModelSubtypes._t1037,
			AbstractModelSubtypes._t1038,
			AbstractModelSubtypes._t1039,
			AbstractModelSubtypes._t1040,
			AbstractModelSubtypes._t1041,
			AbstractModelSubtypes._t1042,
			AbstractModelSubtypes._t1043,
			AbstractModelSubtypes._t1044,
			AbstractModelSubtypes._t1045,
			AbstractModelSubtypes._t1046,
			AbstractModelSubtypes._t1047,
			AbstractModelSubtypes._t1048,
			AbstractModelSubtypes._t1049,
			AbstractModelSubtypes._t1050,
			AbstractModelSubtypes._t1051,
			AbstractModelSubtypes._t1052,
			AbstractModelSubtypes._t1053,
			AbstractModelSubtypes._t1054,
			AbstractModelSubtypes._t1055,
			AbstractModelSubtypes._t1056,
			AbstractModelSubtypes._t1057,
			AbstractModelSubtypes._t1058,
			AbstractModelSubtypes._t1059,
			AbstractModelSubtypes._t1060,
			AbstractModelSubtypes._t1061,
			AbstractModelSubtypes._t1062,
			AbstractModelSubtypes._t1063,
			AbstractModelSubtypes._t1064,
			AbstractModelSubtypes._t1065,
			AbstractModelSubtypes._t1066,
			AbstractModelSubtypes._t1067,
			AbstractModelSubtypes._t1068,
			AbstractModelSubtypes._t1069,
			AbstractModelSubtypes._t1070,
			AbstractModelSubtypes._t1071,
			AbstractModelSubtypes._t1072,
			AbstractModelSubtypes._t1073,
			AbstractModelSubtypes._t1074,
			AbstractModelSubtypes._t1075,
			AbstractModelSubtypes._t1076,
			AbstractModelSubtypes._t1077,
			AbstractModelSubtypes._t1078,
			AbstractModelSubtypes._t1079,
			AbstractModelSubtypes._t1080,
			AbstractModelSubtypes._t1081,
			AbstractModelSubtypes._t1082,
			AbstractModelSubtypes._t1083,
			AbstractModelSubtypes._t1084,
			AbstractModelSubtypes._t1085,
			AbstractModelSubtypes._t1086,
			AbstractModelSubtypes._t1087,
			AbstractModelSubtypes._t1088,
			AbstractModelSubtypes._t1089,
			AbstractModelSubtypes._t1090,
			AbstractModelSubtypes._t1091,
			AbstractModelSubtypes._t1092,
			AbstractModelSubtypes._t1093,
			AbstractModelSubtypes._t1094,
			AbstractModelSubtypes._t1095,
			AbstractModelSubtypes._t1096,
			AbstractModelSubtypes._t1097,
			AbstractModelSubtypes._t1098,
			AbstractModelSubtypes._t1099,
			AbstractModelSubtypes._t1100,
			AbstractModelSubtypes._t1101,
			AbstractModelSubtypes._t1102,
			AbstractModelSubtypes._t1103,
			AbstractModelSubtypes._t1104,
			AbstractModelSubtypes._t1105,
			AbstractModelSubtypes._t1106,
			AbstractModelSubtypes._t1107,
			AbstractModelSubtypes._t1108,
			AbstractModelSubtypes._t1109,
			AbstractModelSubtypes._t1110,
			AbstractModelSubtypes._t1111,
			AbstractModelSubtypes._t1112,
			AbstractModelSubtypes._t1113,
			AbstractModelSubtypes._t1114,
			AbstractModelSubtypes._t1115,
			AbstractModelSubtypes._t1116,
			AbstractModelSubtypes._t1117,
			AbstractModelSubtypes._t1118,
			AbstractModelSubtypes._t1119,
			AbstractModelSubtypes._t1120,
			AbstractModelSubtypes._t1121,
			AbstractModelSubtypes._t1122,
			AbstractModelSubtypes._t1123,
			AbstractModelSubtypes._t1124,
			AbstractModelSubtypes._t1125,
			AbstractModelSubtypes._t1126,
			AbstractModelSubtypes._t1127,
			AbstractModelSubtypes._t1128,
			AbstractModelSubtypes._t1129,
			AbstractModelSubtypes._t1130,
			AbstractModelSubtypes._t1131,
			AbstractModelSubtypes._t1132,
			AbstractModelSubtypes._t1133,
			AbstractModelSubtypes._t1134,
			AbstractModelSubtypes._t1135,
			AbstractModelSubtypes._t1136,
			AbstractModelSubtypes._t1137,
			AbstractModelSubtypes._t1138,
			AbstractModelSubtypes._t1139,
			AbstractModelSubtypes._t1140,
			AbstractModelSubtypes._t1141,
			AbstractModelSubtypes._t1142,
			AbstractModelSubtypes._t1143,
			AbstractModelSubtypes._t1144,
			AbstractModelSubtypes._t1145,
			AbstractModelSubtypes._t1146,
			AbstractModelSubtypes._t1147,
			AbstractModelSubtypes._t1148,
			AbstractModelSubtypes._t1149,
			AbstractModelSubtypes._t1150,
			AbstractModelSubtypes._t1151,
			AbstractModelSubtypes._t1152,
			AbstractModelSubtypes._t1153,
			AbstractModelSubtypes._t1154,
			AbstractModelSubtypes._t1155,
			AbstractModelSubtypes._t1156,
			AbstractModelSubtypes._t1157,
			AbstractModelSubtypes._t1158,
			AbstractModelSubtypes._t1159,
			AbstractModelSubtypes._t1160,
			AbstractModelSubtypes._t1161,
			AbstractModelSubtypes._t1162,
			AbstractModelSubtypes._t1163,
			AbstractModelSubtypes._t1164,
			AbstractModelSubtypes._t1165,
			AbstractModelSubtypes._t1166,
			AbstractModelSubtypes._t1167,
			AbstractModelSubtypes._t1168,
			AbstractModelSubtypes._t1169,
			AbstractModelSubtypes._t1170,
			AbstractModelSubtypes._t1171,
			AbstractModelSubtypes._t1172,
			AbstractModelSubtypes._t1173,
			AbstractModelSubtypes._t1174,
			AbstractModelSubtypes._t1175,
			AbstractModelSubtypes._t1176,
			AbstractModelSubtypes._t1177,
			AbstractModelSubtypes._t1178,
			AbstractModelSubtypes._t1179,
			AbstractModelSubtypes._t1180,
			AbstractModelSubtypes._t1181,
			AbstractModelSubtypes._t1182,
			AbstractModelSubtypes._t1183,
			AbstractModelSubtypes._t1184,
			AbstractModelSubtypes._t1185,
			AbstractModelSubtypes._t1186,
			AbstractModelSubtypes._t1187,
			AbstractModelSubtypes._t1188,
			AbstractModelSubtypes._t1189,
			AbstractModelSubtypes._t1190,
			AbstractModelSubtypes._t1191,
			AbstractModelSubtypes._t1192,
			AbstractModelSubtypes._t1193,
			AbstractModelSubtypes._t1194,
			AbstractModelSubtypes._t1195,
			AbstractModelSubtypes._t1196,
			AbstractModelSubtypes._t1197,
			AbstractModelSubtypes._t1198,
			AbstractModelSubtypes._t1199,
			AbstractModelSubtypes._t1200,
			AbstractModelSubtypes._t1201,
			AbstractModelSubtypes._t1202,
			AbstractModelSubtypes._t1203,
			AbstractModelSubtypes._t1204,
			AbstractModelSubtypes._t1205,
			AbstractModelSubtypes._t1206,
			AbstractModelSubtypes._t1207,
			AbstractModelSubtypes._t1208,
			AbstractModelSubtypes._t1209,
			AbstractModelSubtypes._t1210,
			AbstractModelSubtypes._t1211,
			AbstractModelSubtypes._t1212,
			AbstractModelSubtypes._t1213,
			AbstractModelSubtypes._t1214,
			AbstractModelSubtypes._t1215,
			AbstractModelSubtypes._t1216,
			AbstractModelSubtypes._t1217,
			AbstractModelSubtypes._t1218,
			AbstractModelSubtypes._t1219,
			AbstractModelSubtypes._t1220,
			AbstractModelSubtypes._t1221,
			AbstractModelSubtypes._t1222,
			AbstractModelSubtypes._t1223,
			AbstractModelSubtypes._t1224,
			AbstractModelSubtypes._t1225,
			AbstractModelSubtypes._t1226,
			AbstractModelSubtypes._t1227,
			AbstractModelSubtypes._t1228,
			AbstractModelSubtypes._t1229,
			AbstractModelSubtypes._t1230,
			AbstractModelSubtypes._t1231,
			AbstractModelSubtypes._t1232,
			AbstractModelSubtypes._t1233,
			AbstractModelSubtypes._t1234,
			AbstractModelSubtypes._t1235,
			AbstractModelSubtypes._t1236,
			AbstractModelSubtypes._t1237,
			AbstractModelSubtypes._t1238,
			AbstractModelSubtypes._t1239,
			AbstractModelSubtypes._t1240,
			AbstractModelSubtypes._t1241,
			AbstractModelSubtypes._t1242,
			AbstractModelSubtypes._t1243,
			AbstractModelSubtypes._t1244,
			AbstractModelSubtypes._t1245,
			AbstractModelSubtypes._t1246,
			AbstractModelSubtypes._t1247,
			AbstractModelSubtypes._t1248,
			AbstractModelSubtypes._t1249,
			AbstractModelSubtypes._t1250,
			AbstractModelSubtypes._t1251,
			AbstractModelSubtypes._t1252,
			AbstractModelSubtypes._t1253,
			AbstractModelSubtypes._t1254,
			AbstractModelSubtypes._t1255,
			AbstractModelSubtypes._t1256,
			AbstractModelSubtypes._t1257,
			AbstractModelSubtypes._t1258,
			AbstractModelSubtypes._t1259,
			AbstractModelSubtypes._t1260,
			AbstractModelSubtypes._t1261,
			AbstractModelSubtypes._t1262,
			AbstractModelSubtypes._t1263,
			AbstractModelSubtypes._t1264,
			AbstractModelSubtypes._t1265,
			AbstractModelSubtypes._t1266,
			AbstractModelSubtypes._t1267,
			AbstractModelSubtypes._t1268,
			AbstractModelSubtypes._t1269,
			AbstractModelSubtypes._t1270,
			AbstractModelSubtypes._t1271,
			AbstractModelSubtypes._t1272,
			AbstractModelSubtypes._t1273,
			AbstractModelSubtypes._t1274,
			AbstractModelSubtypes._t1275,
			AbstractModelSubtypes._t1276,
			AbstractModelSubtypes._t1277,
			AbstractModelSubtypes._t1278,
			AbstractModelSubtypes._t1279,
			AbstractModelSubtypes._t1280,
			AbstractModelSubtypes._t1281,
			AbstractModelSubtypes._t1282,
			AbstractModelSubtypes._t1283,
			AbstractModelSubtypes._t1284,
			AbstractModelSubtypes._t1285,
			AbstractModelSubtypes._t1286,
			AbstractModelSubtypes._t1287,
			AbstractModelSubtypes._t1288,
			AbstractModelSubtypes._t1289,
			AbstractModelSubtypes._t1290,
			AbstractModelSubtypes._t1291,
			AbstractModelSubtypes._t1292,
			AbstractModelSubtypes._t1293,
			AbstractModelSubtypes._t1294,
			AbstractModelSubtypes._t1295,
			AbstractModelSubtypes._t1296,
			AbstractModelSubtypes._t1297,
			AbstractModelSubtypes._t1298,
			AbstractModelSubtypes._t1299,
			AbstractModelSubtypes._t1300,
			AbstractModelSubtypes._t1301,
			AbstractModelSubtypes._t1302,
			AbstractModelSubtypes._t1303,
			AbstractModelSubtypes._t1304,
			AbstractModelSubtypes._t1305,
			AbstractModelSubtypes._t1306,
			AbstractModelSubtypes._t1307,
			AbstractModelSubtypes._t1308,
			AbstractModelSubtypes._t1309,
			AbstractModelSubtypes._t1310,
			AbstractModelSubtypes._t1311,
			AbstractModelSubtypes._t1312,
			AbstractModelSubtypes._t1313,
			AbstractModelSubtypes._t1314,
			AbstractModelSubtypes._t1315,
			AbstractModelSubtypes._t1316,
			AbstractModelSubtypes._t1317,
			AbstractModelSubtypes._t1318,
			AbstractModelSubtypes._t1319,
			AbstractModelSubtypes._t1320,
			AbstractModelSubtypes._t1321,
			AbstractModelSubtypes._t1322,
			AbstractModelSubtypes._t1323,
			AbstractModelSubtypes._t1324,
			AbstractModelSubtypes._t1325,
			AbstractModelSubtypes._t1326,
			AbstractModelSubtypes._t1327,
			AbstractModelSubtypes._t1328,
			AbstractModelSubtypes._t1329,
			AbstractModelSubtypes._t1330,
			AbstractModelSubtypes._t1331,
			AbstractModelSubtypes._t1332,
			AbstractModelSubtypes._t1333,
			AbstractModelSubtypes._t1334,
			AbstractModelSubtypes._t1335,
			AbstractModelSubtypes._t1336,
			AbstractModelSubtypes._t1337,
			AbstractModelSubtypes._t1338,
			AbstractModelSubtypes._t1339,
			AbstractModelSubtypes._t1340,
			AbstractModelSubtypes._t1341,
			AbstractModelSubtypes._t1342,
			AbstractModelSubtypes._t1343,
			AbstractModelSubtypes._t1344,
			AbstractModelSubtypes._t1345,
			AbstractModelSubtypes._t1346,
			AbstractModelSubtypes._t1347,
			AbstractModelSubtypes._t1348,
			AbstractModelSubtypes._t1349,
			AbstractModelSubtypes._t1350,
			AbstractModelSubtypes._t1351,
			AbstractModelSubtypes._t1352,
			AbstractModelSubtypes._t1353,
			AbstractModelSubtypes._t1354,
			AbstractModelSubtypes._t1355,
			AbstractModelSubtypes._t1356,
			AbstractModelSubtypes._t1357,
			AbstractModelSubtypes._t1358,
			AbstractModelSubtypes._t1359,
			AbstractModelSubtypes._t1360,
			AbstractModelSubtypes._t1361,
			AbstractModelSubtypes._t1362,
			AbstractModelSubtypes._t1363,
			AbstractModelSubtypes._t1364,
			AbstractModelSubtypes._t1365,
			AbstractModelSubtypes._t1366,
			AbstractModelSubtypes._t1367,
			AbstractModelSubtypes._t1368,
			AbstractModelSubtypes._t1369,
			AbstractModelSubtypes._t1370,
			AbstractModelSubtypes._t1371,
			AbstractModelSubtypes._t1372,
			AbstractModelSubtypes._t1373,
			AbstractModelSubtypes._t1374,
			AbstractModelSubtypes._t1375,
			AbstractModelSubtypes._t1376,
			AbstractModelSubtypes._t1377,
			AbstractModelSubtypes._t1378,
			AbstractModelSubtypes._t1379,
			AbstractModelSubtypes._t1380,
			AbstractModelSubtypes._t1381,
			AbstractModelSubtypes._t1382,
			AbstractModelSubtypes._t1383,
			AbstractModelSubtypes._t1384,
			AbstractModelSubtypes._t1385,
			AbstractModelSubtypes._t1386,
			AbstractModelSubtypes._t1387,
			AbstractModelSubtypes._t1388,
			AbstractModelSubtypes._t1389,
			AbstractModelSubtypes._t1390,
			AbstractModelSubtypes._t1391,
			AbstractModelSubtypes._t1392,
			AbstractModelSubtypes._t1393,
			AbstractModelSubtypes._t1394,
			AbstractModelSubtypes._t1395,
			AbstractModelSubtypes._t1396,
			AbstractModelSubtypes._t1397,
			AbstractModelSubtypes._t1398,
			AbstractModelSubtypes._t1399,
			AbstractModelSubtypes._t1400,
			AbstractModelSubtypes._t1401,
			AbstractModelSubtypes._t1402,
			AbstractModelSubtypes._t1403,
			AbstractModelSubtypes._t1404,
			AbstractModelSubtypes._t1405,
			AbstractModelSubtypes._t1406,
			AbstractModelSubtypes._t1407,
			AbstractModelSubtypes._t1408,
			AbstractModelSubtypes._t1409,
			AbstractModelSubtypes._t1410,
			AbstractModelSubtypes._t1411,
			AbstractModelSubtypes._t1412,
			AbstractModelSubtypes._t1413,
			AbstractModelSubtypes._t1414,
			AbstractModelSubtypes._t1415,
			AbstractModelSubtypes._t1416,
			AbstractModelSubtypes._t1417,
			AbstractModelSubtypes._t1418,
			AbstractModelSubtypes._t1419,
			AbstractModelSubtypes._t1420,
			AbstractModelSubtypes._t1421,
			AbstractModelSubtypes._t1422,
			AbstractModelSubtypes._t1423,
			AbstractModelSubtypes._t1424,
			AbstractModelSubtypes._t1425,
			AbstractModelSubtypes._t1426,
			AbstractModelSubtypes._t1427,
			AbstractModelSubtypes._t1428,
			AbstractModelSubtypes._t1429,
			AbstractModelSubtypes._t1430,
			AbstractModelSubtypes._t1431,
			AbstractModelSubtypes._t1432,
			AbstractModelSubtypes._t1433,
			AbstractModelSubtypes._t1434,
			AbstractModelSubtypes._t1435,
			AbstractModelSubtypes._t1436,
			AbstractModelSubtypes._t1437,
			AbstractModelSubtypes._t1438,
			AbstractModelSubtypes._t1439,
			AbstractModelSubtypes._t1440,
			AbstractModelSubtypes._t1441,
			AbstractModelSubtypes._t1442,
			AbstractModelSubtypes._t1443,
			AbstractModelSubtypes._t1444,
			AbstractModelSubtypes._t1445,
			AbstractModelSubtypes._t1446,
			AbstractModelSubtypes._t1447,
			AbstractModelSubtypes._t1448,
			AbstractModelSubtypes._t1449,
			AbstractModelSubtypes._t1450,
			AbstractModelSubtypes._t1451,
			AbstractModelSubtypes._t1452,
			AbstractModelSubtypes._t1453,
			AbstractModelSubtypes._t1454,
			AbstractModelSubtypes._t1455,
			AbstractModelSubtypes._t1456,
			AbstractModelSubtypes._t1457,
			AbstractModelSubtypes._t1458,
			AbstractModelSubtypes._t1459,
			AbstractModelSubtypes._t1460,
			AbstractModelSubtypes._t1461,
			AbstractModelSubtypes._t1462,
			AbstractModelSubtypes._t1463,
			AbstractModelSubtypes._t1464,
			AbstractModelSubtypes._t1465,
			AbstractModelSubtypes._t1466,
			AbstractModelSubtypes._t1467,
			AbstractModelSubtypes._t1468,
			AbstractModelSubtypes._t1469,
			AbstractModelSubtypes._t1470,
			AbstractModelSubtypes._t1471,
			AbstractModelSubtypes._t1472,
			AbstractModelSubtypes._t1473,
			AbstractModelSubtypes._t1474,
			AbstractModelSubtypes._t1475,
			AbstractModelSubtypes._t1476,
			AbstractModelSubtypes._t1477,
			AbstractModelSubtypes._t1478,
			AbstractModelSubtypes._t1479,
			AbstractModelSubtypes._t1480,
			AbstractModelSubtypes._t1481,
			AbstractModelSubtypes._t1482,
			AbstractModelSubtypes._t1483,
			AbstractModelSubtypes._t1484,
			AbstractModelSubtypes._t1485,
			AbstractModelSubtypes._t1486,
			AbstractModelSubtypes._t1487,
			AbstractModelSubtypes._t1488,
			AbstractModelSubtypes._t1489,
			AbstractModelSubtypes._t1490,
			AbstractModelSubtypes._t1491,
			AbstractModelSubtypes._t1492,
			AbstractModelSubtypes._t1493,
			AbstractModelSubtypes._t1494,
			AbstractModelSubtypes._t1495,
			AbstractModelSubtypes._t1496,
			AbstractModelSubtypes._t1497,
			AbstractModelSubtypes._t1498,
			AbstractModelSubtypes._t1499,
			AbstractModelSubtypes._t1500,
			AbstractModelSubtypes._t1501,
			AbstractModelSubtypes._t1502,
			AbstractModelSubtypes._t1503,
			AbstractModelSubtypes._t1504,
			AbstractModelSubtypes._t1505,
			AbstractModelSubtypes._t1506,
			AbstractModelSubtypes._t1507,
			AbstractModelSubtypes._t1508,
			AbstractModelSubtypes._t1509,
			AbstractModelSubtypes._t1510,
			AbstractModelSubtypes._t1511,
			AbstractModelSubtypes._t1512,
			AbstractModelSubtypes._t1513,
			AbstractModelSubtypes._t1514,
			AbstractModelSubtypes._t1515,
			AbstractModelSubtypes._t1516,
			AbstractModelSubtypes._t1517,
			AbstractModelSubtypes._t1518,
			AbstractModelSubtypes._t1519,
			AbstractModelSubtypes._t1520,
			AbstractModelSubtypes._t1521,
			AbstractModelSubtypes._t1522,
			AbstractModelSubtypes._t1523,
			AbstractModelSubtypes._t1524,
			AbstractModelSubtypes._t1525,
			AbstractModelSubtypes._t1526,
			AbstractModelSubtypes._t1527,
			AbstractModelSubtypes._t1528,
			AbstractModelSubtypes._t1529,
			AbstractModelSubtypes._t1530,
			AbstractModelSubtypes._t1531,
			AbstractModelSubtypes._t1532,
			AbstractModelSubtypes._t1533,
			AbstractModelSubtypes._t1534,
			AbstractModelSubtypes._t1535,
			AbstractModelSubtypes._t1536,
			AbstractModelSubtypes._t1537,
			AbstractModelSubtypes._t1538,
			AbstractModelSubtypes._t1539,
			AbstractModelSubtypes._t1540,
			AbstractModelSubtypes._t1541,
			AbstractModelSubtypes._t1542,
			AbstractModelSubtypes._t1543,
			AbstractModelSubtypes._t1544,
			AbstractModelSubtypes._t1545,
			AbstractModelSubtypes._t1546,
			AbstractModelSubtypes._t1547,
			AbstractModelSubtypes._t1548,
			AbstractModelSubtypes._t1549,
			AbstractModelSubtypes._t1550,
			AbstractModelSubtypes._t1551,
			AbstractModelSubtypes._t1552,
			AbstractModelSubtypes._t1553,
			AbstractModelSubtypes._t1554,
			AbstractModelSubtypes._t1555,
			AbstractModelSubtypes._t1556,
			AbstractModelSubtypes._t1557,
			AbstractModelSubtypes._t1558,
			AbstractModelSubtypes._t1559,
			AbstractModelSubtypes._t1560,
			AbstractModelSubtypes._t1561,
			AbstractModelSubtypes._t1562,
			AbstractModelSubtypes._t1563,
			AbstractModelSubtypes._t1564,
			AbstractModelSubtypes._t1565,
			AbstractModelSubtypes._t1566,
			AbstractModelSubtypes._t1567,
			AbstractModelSubtypes._t1568,
			AbstractModelSubtypes._t1569,
			AbstractModelSubtypes._t1570,
			AbstractModelSubtypes._t1571,
			AbstractModelSubtypes._t1572,
			AbstractModelSubtypes._t1573,
			AbstractModelSubtypes._t1574,
			AbstractModelSubtypes._t1575,
			AbstractModelSubtypes._t1576,
			AbstractModelSubtypes._t1577,
			AbstractModelSubtypes._t1578,
			AbstractModelSubtypes._t1579,
			AbstractModelSubtypes._t1580,
			AbstractModelSubtypes._t1581,
			AbstractModelSubtypes._t1582,
			AbstractModelSubtypes._t1583,
			AbstractModelSubtypes._t1584,
			AbstractModelSubtypes._t1585,
			AbstractModelSubtypes._t1586,
			AbstractModelSubtypes._t1587,
			AbstractModelSubtypes._t1588,
			AbstractModelSubtypes._t1589,
			AbstractModelSubtypes._t1590,
			AbstractModelSubtypes._t1591,
			AbstractModelSubtypes._t1592,
			AbstractModelSubtypes._t1593,
			AbstractModelSubtypes._t1594,
			AbstractModelSubtypes._t1595,
			AbstractModelSubtypes._t1596,
			AbstractModelSubtypes._t1597,
			AbstractModelSubtypes._t1598
		};
	}
}

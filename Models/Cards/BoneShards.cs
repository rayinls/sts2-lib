using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008BA RID: 2234
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BoneShards : CardModel
	{
		// Token: 0x060067BA RID: 26554 RVA: 0x00255FAF File Offset: 0x002541AF
		public BoneShards()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001AFD RID: 6909
		// (get) Token: 0x060067BB RID: 26555 RVA: 0x00255FBC File Offset: 0x002541BC
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001AFE RID: 6910
		// (get) Token: 0x060067BC RID: 26556 RVA: 0x00255FC9 File Offset: 0x002541C9
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001AFF RID: 6911
		// (get) Token: 0x060067BD RID: 26557 RVA: 0x00255FCC File Offset: 0x002541CC
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001B00 RID: 6912
		// (get) Token: 0x060067BE RID: 26558 RVA: 0x00255FDB File Offset: 0x002541DB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new OstyDamageVar(9m, ValueProp.Move),
					new BlockVar(9m, ValueProp.Move)
				});
			}
		}

		// Token: 0x060067BF RID: 26559 RVA: 0x00256008 File Offset: 0x00254208
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				await DamageCmd.Attack(base.DynamicVars.OstyDamage.BaseValue).FromOsty(base.Owner.Osty, this).TargetingAllOpponents(base.CombatState)
					.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
					.Execute(choiceContext);
				await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
				if (base.Owner.IsOstyAlive)
				{
					await CreatureCmd.Kill(base.Owner.Osty, false);
				}
			}
		}

		// Token: 0x060067C0 RID: 26560 RVA: 0x0025605B File Offset: 0x0025425B
		protected override void OnUpgrade()
		{
			base.DynamicVars.OstyDamage.UpgradeValueBy(3m);
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}

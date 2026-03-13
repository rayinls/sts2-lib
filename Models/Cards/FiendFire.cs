using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200094C RID: 2380
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FiendFire : CardModel
	{
		// Token: 0x06006AB6 RID: 27318 RVA: 0x0025B9BB File Offset: 0x00259BBB
		public FiendFire()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x06006AB7 RID: 27319 RVA: 0x0025B9C8 File Offset: 0x00259BC8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x06006AB8 RID: 27320 RVA: 0x0025B9DB File Offset: 0x00259BDB
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x06006AB9 RID: 27321 RVA: 0x0025B9E3 File Offset: 0x00259BE3
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NGroundFireVfx.AssetPaths;
			}
		}

		// Token: 0x06006ABA RID: 27322 RVA: 0x0025B9EC File Offset: 0x00259BEC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.ToList<CardModel>();
			int cardCount = list.Count;
			foreach (CardModel cardModel in list)
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			float scale = 0.8f;
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(cardCount).FromCard(this)
				.Targeting(cardPlay.Target)
				.BeforeDamage(delegate
				{
					NGroundFireVfx ngroundFireVfx = NGroundFireVfx.Create(cardPlay.Target, VfxColor.Red);
					if (ngroundFireVfx == null)
					{
						return Task.CompletedTask;
					}
					SfxCmd.Play("event:/sfx/characters/attack_fire", 1f);
					ngroundFireVfx.Scale = Vector2.One * scale;
					NCombatRoom instance = NCombatRoom.Instance;
					if (instance != null)
					{
						instance.CombatVfxContainer.AddChildSafely(ngroundFireVfx);
					}
					scale += 0.1f;
					return Task.CompletedTask;
				})
				.Execute(choiceContext);
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x0025BA3F File Offset: 0x00259C3F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}

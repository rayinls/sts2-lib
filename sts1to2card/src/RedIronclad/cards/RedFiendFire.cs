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
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedFiendFire : CardModel
	{
		public RedFiendFire()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// (get) Token: 0x06006AB7 RID: 27319 RVA: 0x0025B9C8 File Offset: 0x00259BC8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new DamageVar(7m, ValueProp.Move) };
			}
		}

		// (get) Token: 0x06006AB8 RID: 27320 RVA: 0x0025B9DB File Offset: 0x00259BDB
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new CardKeyword[] { CardKeyword.Exhaust };
			}
		}

		// (get) Token: 0x06006AB9 RID: 27321 RVA: 0x0025B9E3 File Offset: 0x00259BE3
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NGroundFireVfx.AssetPaths;
			}
		}

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

		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}

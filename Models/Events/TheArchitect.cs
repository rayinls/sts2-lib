using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007F4 RID: 2036
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheArchitect : EventModel
	{
		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x060062B0 RID: 25264 RVA: 0x0024C372 File Offset: 0x0024A572
		public override EventLayoutType LayoutType
		{
			get
			{
				return EventLayoutType.Combat;
			}
		}

		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x060062B1 RID: 25265 RVA: 0x0024C375 File Offset: 0x0024A575
		public override EncounterModel CanonicalEncounter
		{
			get
			{
				return ModelDb.Encounter<TheArchitectEventEncounter>();
			}
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x060062B2 RID: 25266 RVA: 0x0024C37C File Offset: 0x0024A57C
		protected override string LocTable
		{
			get
			{
				return "ancients";
			}
		}

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x060062B3 RID: 25267 RVA: 0x0024C383 File Offset: 0x0024A583
		// (set) Token: 0x060062B4 RID: 25268 RVA: 0x0024C38B File Offset: 0x0024A58B
		[Nullable(2)]
		private AncientDialogue Dialogue
		{
			[NullableContext(2)]
			get
			{
				return this._dialogue;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._dialogue = value;
			}
		}

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x060062B5 RID: 25269 RVA: 0x0024C39A File Offset: 0x0024A59A
		// (set) Token: 0x060062B6 RID: 25270 RVA: 0x0024C3A2 File Offset: 0x0024A5A2
		private int CurrentLineIndex
		{
			get
			{
				return this._currentLineIndex;
			}
			set
			{
				base.AssertMutable();
				this._currentLineIndex = value;
			}
		}

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x060062B7 RID: 25271 RVA: 0x0024C3B1 File Offset: 0x0024A5B1
		// (set) Token: 0x060062B8 RID: 25272 RVA: 0x0024C3B9 File Offset: 0x0024A5B9
		[Nullable(2)]
		private NSpeechBubbleVfx SpeechBubble
		{
			[NullableContext(2)]
			get
			{
				return this._speechBubble;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._speechBubble = value;
			}
		}

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x060062B9 RID: 25273 RVA: 0x0024C3C8 File Offset: 0x0024A5C8
		// (set) Token: 0x060062BA RID: 25274 RVA: 0x0024C3D0 File Offset: 0x0024A5D0
		[Nullable(2)]
		private Creature ArchitectCreature
		{
			[NullableContext(2)]
			get
			{
				return this._architectCreature;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._architectCreature = value;
			}
		}

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x060062BB RID: 25275 RVA: 0x0024C3DF File Offset: 0x0024A5DF
		// (set) Token: 0x060062BC RID: 25276 RVA: 0x0024C3E7 File Offset: 0x0024A5E7
		private int Score
		{
			get
			{
				return this._score;
			}
			set
			{
				base.AssertMutable();
				this._score = value;
			}
		}

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x060062BD RID: 25277 RVA: 0x0024C3F6 File Offset: 0x0024A5F6
		public override IEnumerable<LocString> GameInfoOptions
		{
			get
			{
				return Array.Empty<LocString>();
			}
		}

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x060062BE RID: 25278 RVA: 0x0024C3FD File Offset: 0x0024A5FD
		private bool IsOnLastLine
		{
			get
			{
				return this.Dialogue == null || this.CurrentLineIndex >= this.Dialogue.Lines.Count - 1;
			}
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x0024C426 File Offset: 0x0024A626
		protected override void SetInitialEventState(bool isPreFinished)
		{
			this.SetEventState(TheArchitect._emptyLocString, this.GenerateInitialOptionsWrapper());
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x0024C439 File Offset: 0x0024A639
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			this.LoadDialogue();
			if (this.Dialogue == null || this.Dialogue.Lines.Count == 0)
			{
				return new <>z__ReadOnlySingleElementList<EventOption>(this.CreateProceedOption());
			}
			this.CurrentLineIndex = 0;
			return new <>z__ReadOnlySingleElementList<EventOption>(this.CreateOptionForCurrentLine());
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x0024C47C File Offset: 0x0024A67C
		public override void OnRoomEnter()
		{
			StatsManager.RefreshGlobalStats();
			NCombatRoom instance = NCombatRoom.Instance;
			Creature creature;
			if (instance == null)
			{
				creature = null;
			}
			else
			{
				NCreature ncreature = instance.CreatureNodes.FirstOrDefault((NCreature n) => n.Entity.Side == CombatSide.Enemy);
				creature = ((ncreature != null) ? ncreature.Entity : null);
			}
			this.ArchitectCreature = creature;
			this.Score = ScoreUtility.CalculateScore(base.Owner.RunState, true);
			if (LocalContext.IsMe(base.Owner))
			{
				if (this.ArchitectCreature != null)
				{
					MegaAnimationState architectAnimationState = this.GetArchitectAnimationState();
					if (architectAnimationState != null)
					{
						architectAnimationState.SetAnimation("_tracks/head_reading", true, 1);
					}
				}
				AncientDialogue dialogue = this.Dialogue;
				if (dialogue != null)
				{
					IReadOnlyList<AncientDialogueLine> lines = dialogue.Lines;
					if (lines != null && lines.Count > 0)
					{
						base.ClearCurrentOptions();
					}
				}
			}
			TaskHelper.RunSafely(this.PlayCurrentLine());
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x0024C549 File Offset: 0x0024A749
		public void TriggerVictory()
		{
			if (LocalContext.IsMe(base.Owner))
			{
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance == null)
				{
					return;
				}
				instance.SetWaitingForOtherPlayersOverlayVisible(false);
			}
		}

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x060062C3 RID: 25283 RVA: 0x0024C568 File Offset: 0x0024A768
		public AncientDialogueSet DialogueSet
		{
			get
			{
				if (this._dialogueSet == null)
				{
					this._dialogueSet = TheArchitect.DefineDialogues();
					this._dialogueSet.PopulateLocKeys(base.Id.Entry);
				}
				return this._dialogueSet;
			}
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x0024C599 File Offset: 0x0024A799
		private static string CharKey<[Nullable(0)] T>() where T : CharacterModel
		{
			return ModelDb.Character<T>().Id.Entry;
		}

		// Token: 0x060062C5 RID: 25285 RVA: 0x0024C5B0 File Offset: 0x0024A7B0
		private static AncientDialogueSet DefineDialogues()
		{
			AncientDialogueSet ancientDialogueSet = new AncientDialogueSet();
			ancientDialogueSet.FirstVisitEverDialogue = null;
			AncientDialogueSet ancientDialogueSet2 = ancientDialogueSet;
			Dictionary<string, IReadOnlyList<AncientDialogue>> dictionary = new Dictionary<string, IReadOnlyList<AncientDialogue>>();
			string text = TheArchitect.CharKey<Ironclad>();
			dictionary[text] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "", "" })
				{
					VisitIndex = new int?(0),
					EndAttackers = ArchitectAttackers.Both
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(1),
					EndAttackers = ArchitectAttackers.Both
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(2),
					EndAttackers = ArchitectAttackers.Both
				}
			});
			string text2 = TheArchitect.CharKey<Silent>();
			dictionary[text2] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "" })
				{
					VisitIndex = new int?(0),
					StartAttackers = ArchitectAttackers.Player,
					EndAttackers = ArchitectAttackers.Both
				},
				new AncientDialogue(new string[] { "" })
				{
					VisitIndex = new int?(1),
					StartAttackers = ArchitectAttackers.Player,
					EndAttackers = ArchitectAttackers.Both
				},
				new AncientDialogue(new string[] { "" })
				{
					VisitIndex = new int?(2),
					StartAttackers = ArchitectAttackers.Player,
					EndAttackers = ArchitectAttackers.Architect
				},
				new AncientDialogue(new string[] { "" })
				{
					VisitIndex = new int?(3),
					StartAttackers = ArchitectAttackers.Player,
					EndAttackers = ArchitectAttackers.Architect
				}
			});
			string text3 = TheArchitect.CharKey<Defect>();
			dictionary[text3] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(0),
					EndAttackers = ArchitectAttackers.Both
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(1),
					EndAttackers = ArchitectAttackers.Both
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(2),
					EndAttackers = ArchitectAttackers.Both
				}
			});
			string text4 = TheArchitect.CharKey<Necrobinder>();
			dictionary[text4] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "", "" })
				{
					VisitIndex = new int?(0),
					EndAttackers = ArchitectAttackers.Both
				},
				new AncientDialogue(new string[] { "", "" })
				{
					VisitIndex = new int?(1),
					EndAttackers = ArchitectAttackers.Both
				},
				new AncientDialogue(new string[] { "", "" })
				{
					VisitIndex = new int?(2),
					EndAttackers = ArchitectAttackers.Both
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(3),
					EndAttackers = ArchitectAttackers.Both
				}
			});
			string text5 = TheArchitect.CharKey<Regent>();
			dictionary[text5] = new <>z__ReadOnlyArray<AncientDialogue>(new AncientDialogue[]
			{
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(0),
					EndAttackers = ArchitectAttackers.Both
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(1),
					EndAttackers = ArchitectAttackers.Both
				},
				new AncientDialogue(new string[] { "", "", "" })
				{
					VisitIndex = new int?(2),
					EndAttackers = ArchitectAttackers.Both
				}
			});
			ancientDialogueSet2.CharacterDialogues = dictionary;
			ancientDialogueSet.AgnosticDialogues = Array.Empty<AncientDialogue>();
			return ancientDialogueSet;
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x0024C9E8 File Offset: 0x0024ABE8
		private void LoadDialogue()
		{
			CharacterStats statsForCharacter = SaveManager.Instance.Progress.GetStatsForCharacter(base.Owner.Character.Id);
			int num = ((statsForCharacter != null) ? statsForCharacter.TotalWins : 0);
			int wins = SaveManager.Instance.Progress.Wins;
			List<AncientDialogue> list = this.DialogueSet.GetValidDialogues(base.Owner.Character.Id, num, wins, false).ToList<AncientDialogue>();
			this.Dialogue = base.Rng.NextItem<AncientDialogue>(list);
		}

		// Token: 0x060062C7 RID: 25287 RVA: 0x0024CA68 File Offset: 0x0024AC68
		private EventOption CreateOptionForCurrentLine()
		{
			AncientDialogueLine ancientDialogueLine = this.Dialogue.Lines[this.CurrentLineIndex];
			EventOption eventOption;
			if (this.IsOnLastLine)
			{
				eventOption = this.CreateProceedOption();
			}
			else
			{
				LocString locString;
				if (ancientDialogueLine.NextButtonText != null)
				{
					locString = ancientDialogueLine.NextButtonText;
				}
				else if (ancientDialogueLine.Speaker == AncientDialogueSpeaker.Ancient)
				{
					locString = TheArchitect._respondLocString;
				}
				else
				{
					locString = TheArchitect._continueLocString;
				}
				Func<Task> func = new Func<Task>(this.AdvanceDialogue);
				LocString locString2 = locString;
				LocString emptyLocString = TheArchitect._emptyLocString;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 2);
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry);
				defaultInterpolatedStringHandler.AppendLiteral(".dialogue.");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.CurrentLineIndex);
				eventOption = new EventOption(this, func, locString2, emptyLocString, defaultInterpolatedStringHandler.ToStringAndClear(), Array.Empty<IHoverTip>()).ThatWontSaveToChoiceHistory();
			}
			return eventOption;
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x0024CB28 File Offset: 0x0024AD28
		private EventOption CreateProceedOption()
		{
			return new EventOption(this, new Func<Task>(this.WinRun), "PROCEED", false, false, Array.Empty<IHoverTip>()).ThatWontSaveToChoiceHistory();
		}

		// Token: 0x060062C9 RID: 25289 RVA: 0x0024CB50 File Offset: 0x0024AD50
		private async Task AdvanceDialogue()
		{
			int currentLineIndex = this.CurrentLineIndex;
			this.CurrentLineIndex = currentLineIndex + 1;
			IEnumerable<EventOption> enumerable;
			if (this.CurrentLineIndex < this.Dialogue.Lines.Count)
			{
				await this.PlayCurrentLine();
				enumerable = new <>z__ReadOnlySingleElementList<EventOption>(this.CreateOptionForCurrentLine());
			}
			else
			{
				enumerable = new <>z__ReadOnlySingleElementList<EventOption>(this.CreateProceedOption());
			}
			this.SetEventState(TheArchitect._emptyLocString, enumerable);
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x0024CB94 File Offset: 0x0024AD94
		private async Task WinRun()
		{
			if (LocalContext.IsMe(base.Owner))
			{
				await this.AnimPlayerAttackIfNecessary(this.Dialogue.EndAttackers);
				await this.AnimArchitectAttackIfNecessary(this.Dialogue.EndAttackers);
				if (base.Owner.RunState.Players.Count > 1)
				{
					NCombatRoom instance = NCombatRoom.Instance;
					if (instance != null)
					{
						instance.SetWaitingForOtherPlayersOverlayVisible(true);
					}
				}
				RunManager.Instance.ActChangeSynchronizer.SetLocalPlayerReady();
			}
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x0024CBD8 File Offset: 0x0024ADD8
		private async Task<bool> AnimPlayerAttackIfNecessary(ArchitectAttackers attackers)
		{
			bool flag;
			if (attackers != ArchitectAttackers.Player && attackers != ArchitectAttackers.Both)
			{
				flag = false;
			}
			else if (this.ArchitectCreature == null)
			{
				flag = false;
			}
			else
			{
				List<string> shuffledVfx = base.Owner.Character.GetArchitectAttackVfx();
				base.Rng.Shuffle<string>(shuffledVfx);
				int[] damageParts = TheArchitect.DivideWildly(this.Score, shuffledVfx.Count, base.Rng);
				NCombatRoom instance = NCombatRoom.Instance;
				Control vfxContainer = ((instance != null) ? instance.CombatVfxContainer : null);
				for (int i = 0; i < shuffledVfx.Count; i++)
				{
					bool isFinalHit = i == shuffledVfx.Count - 1;
					await CreatureCmd.TriggerAnim(base.Owner.Creature, "Attack", 0.1f);
					Control control = vfxContainer;
					if (control != null)
					{
						control.AddChildSafely(NDamageNumVfx.Create(this.ArchitectCreature, damageParts[i], false));
					}
					Control control2 = vfxContainer;
					if (control2 != null)
					{
						control2.AddChildSafely(NHitSparkVfx.Create(this.ArchitectCreature, false));
					}
					VfxCmd.PlayOnCreatureCenter(this.ArchitectCreature, shuffledVfx[i]);
					await CreatureCmd.TriggerAnim(this.ArchitectCreature, "Hit", 0f);
					if (isFinalHit)
					{
						NGame instance2 = NGame.Instance;
						if (instance2 != null)
						{
							instance2.ScreenShake(ShakeStrength.Strong, ShakeDuration.Normal, -1f);
						}
					}
					else
					{
						NGame instance3 = NGame.Instance;
						if (instance3 != null)
						{
							instance3.ScreenShake(ShakeStrength.Weak, ShakeDuration.Short, -1f);
						}
					}
					if (!isFinalHit)
					{
						await Cmd.Wait(0.1f, false);
					}
				}
				await Cmd.Wait(2f, false);
				flag = true;
			}
			return flag;
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x0024CC24 File Offset: 0x0024AE24
		private static int[] DivideWildly(int total, int parts, Rng rng)
		{
			if (parts <= 0)
			{
				return Array.Empty<int>();
			}
			if (parts == 1)
			{
				return new int[] { total };
			}
			double[] array = new double[parts];
			int num = rng.NextInt(parts);
			int num2;
			do
			{
				num2 = rng.NextInt(parts);
			}
			while (num2 == num);
			for (int i = 0; i < parts; i++)
			{
				if (i == num)
				{
					array[i] = (double)rng.NextFloat(2f, 3f);
				}
				else if (i == num2)
				{
					array[i] = (double)rng.NextFloat(0.1f, 0.5f);
				}
				else
				{
					array[i] = (double)rng.NextFloat(0.7f, 1.3f);
				}
			}
			double num3 = array.Sum();
			int[] array2 = new int[parts];
			int num4 = 0;
			for (int j = 0; j < parts - 1; j++)
			{
				array2[j] = Math.Max(1, (int)((double)total * array[j] / num3));
				num4 += array2[j];
			}
			array2[parts - 1] = Math.Max(1, total - num4);
			return array2;
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x0024CD18 File Offset: 0x0024AF18
		private async Task AnimArchitectAttackIfNecessary(ArchitectAttackers attackers)
		{
			if (attackers - ArchitectAttackers.Architect <= 1)
			{
				if (this.ArchitectCreature != null)
				{
					await CreatureCmd.TriggerAnim(this.ArchitectCreature, "Attack", 0.5f);
					VfxCmd.PlayOnCreature(base.Owner.Creature, "vfx/vfx_attack_lightning");
					NCombatRoom instance = NCombatRoom.Instance;
					if (instance != null)
					{
						instance.CombatVfxContainer.AddChildSafely(NFireBurstVfx.Create(base.Owner.Creature, 1f));
					}
					await Cmd.Wait(0.5f, false);
				}
			}
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x0024CD64 File Offset: 0x0024AF64
		private async Task PlayCurrentLine()
		{
			if (LocalContext.IsMe(base.Owner))
			{
				if (this.SpeechBubble != null)
				{
					TaskHelper.RunSafely(this.SpeechBubble.AnimOut());
					this.SpeechBubble = null;
				}
				if (this.Dialogue != null && this.CurrentLineIndex < this.Dialogue.Lines.Count)
				{
					AncientDialogueLine line = this.Dialogue.Lines[this.CurrentLineIndex];
					if (line.LineText != null)
					{
						Creature speaker = this.GetSpeaker(line.Speaker);
						if (speaker != null)
						{
							if (this.CurrentLineIndex == 0)
							{
								if (this.Dialogue.StartAttackers != ArchitectAttackers.None || this.Dialogue.EndAttackers != ArchitectAttackers.None)
								{
									await Cmd.Wait(0.75f, false);
								}
								bool flag = await this.AnimPlayerAttackIfNecessary(this.Dialogue.StartAttackers);
								if (this.ArchitectCreature != null)
								{
									if (!flag)
									{
										await Cmd.Wait(1f, false);
									}
									MegaAnimationState state = this.GetArchitectAnimationState();
									MegaAnimationState megaAnimationState = state;
									if (megaAnimationState != null)
									{
										megaAnimationState.SetAnimation("_tracks/head_stop_reading", false, 1);
									}
									await Cmd.Wait(0.5f, false);
									MegaAnimationState megaAnimationState2 = state;
									if (megaAnimationState2 != null)
									{
										megaAnimationState2.SetAnimation("_tracks/head_normal", true, 1);
									}
									state = null;
								}
								await this.AnimArchitectAttackIfNecessary(this.Dialogue.StartAttackers);
							}
							this.ShowSpeechBubble(line, speaker);
							if (this.CurrentLineIndex == 0)
							{
								this.SetEventState(TheArchitect._emptyLocString, new <>z__ReadOnlySingleElementList<EventOption>(this.CreateOptionForCurrentLine()));
							}
						}
					}
				}
			}
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x0024CDA7 File Offset: 0x0024AFA7
		private void ShowSpeechBubble(AncientDialogueLine line, Creature speaker)
		{
			this.SpeechBubble = TalkCmd.Play(line.LineText, speaker, double.MaxValue, VfxColor.White);
		}

		// Token: 0x060062D0 RID: 25296 RVA: 0x0024CDC8 File Offset: 0x0024AFC8
		[NullableContext(2)]
		private Creature GetSpeaker(AncientDialogueSpeaker speaker)
		{
			Creature creature;
			if (speaker != AncientDialogueSpeaker.Ancient)
			{
				if (speaker != AncientDialogueSpeaker.Character)
				{
					creature = null;
				}
				else
				{
					creature = base.Owner.Creature;
				}
			}
			else
			{
				creature = this.ArchitectCreature;
			}
			return creature;
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x0024CDF9 File Offset: 0x0024AFF9
		[NullableContext(2)]
		private MegaAnimationState GetArchitectAnimationState()
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance == null)
			{
				return null;
			}
			NCreature creatureNode = instance.GetCreatureNode(this.ArchitectCreature);
			if (creatureNode == null)
			{
				return null;
			}
			MegaSprite spineController = creatureNode.SpineController;
			if (spineController == null)
			{
				return null;
			}
			return spineController.GetAnimationState();
		}

		// Token: 0x040024E4 RID: 9444
		private const string _locTable = "ancients";

		// Token: 0x040024E5 RID: 9445
		private static readonly LocString _emptyLocString = new LocString("ancients", "PROCEED.description");

		// Token: 0x040024E6 RID: 9446
		private static readonly LocString _continueLocString = new LocString("ancients", "THE_ARCHITECT.CONTINUE");

		// Token: 0x040024E7 RID: 9447
		private static readonly LocString _respondLocString = new LocString("ancients", "THE_ARCHITECT.RESPOND");

		// Token: 0x040024E8 RID: 9448
		[Nullable(2)]
		private AncientDialogue _dialogue;

		// Token: 0x040024E9 RID: 9449
		private int _currentLineIndex;

		// Token: 0x040024EA RID: 9450
		[Nullable(2)]
		private NSpeechBubbleVfx _speechBubble;

		// Token: 0x040024EB RID: 9451
		[Nullable(2)]
		private Creature _architectCreature;

		// Token: 0x040024EC RID: 9452
		private int _score;

		// Token: 0x040024ED RID: 9453
		[Nullable(2)]
		private AncientDialogueSet _dialogueSet;
	}
}

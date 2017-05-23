using HearthStone.Library.CardRecords;
using HearthStone.Library.CommunicationInfrastructure.Event.Managers;
using HearthStone.Library.Effectors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HearthStone.Library
{
    public class Game
    {
        public int GameID { get; private set; }
        public GamePlayer GamePlayer1 { get; private set; }
        public GamePlayer GamePlayer2 { get; private set; }

        private int roundCount;
        public int RoundCount
        {
            get { return roundCount; }
            set
            {
                roundCount = value;
                OnRoundCountChanged?.Invoke(this);
            }
        }

        public Field Field1 { get; private set; }
        public Field Field2 { get; private set; }

        private int currentGamePlayerID;
        public int CurrentGamePlayerID
        {
            get { return currentGamePlayerID; }
            set
            {
                currentGamePlayerID = value;
                OnCurrentGamePlayerID_Changed?.Invoke(this);
            }
        }

        public GameCardManager GameCardManager { get; private set; }

        public event Action<Game> OnRoundCountChanged;
        public event Action<Game> OnRoundStart;
        public event Action<Game> OnRoundEnd;
        public event Action<Game> OnCurrentGamePlayerID_Changed;
        public event Action<Game, int> OnGameOver;

        public delegate void UseCardEventHandler(Game game, GamePlayer gamePlayer, CardRecord cardRecord);
        public event UseCardEventHandler OnUseCard;

        public GameEventManager EventManager { get; private set; }

        public Game(int gameID, Player player1, Player player2, Deck player1Deck, Deck player2Deck)
        {
            GameCardManager = new GameCardManager();
            GameCardManager.BindGame(this);

            GameID = gameID;
            GamePlayer1 = new GamePlayer(player1, new Hero(1, 30, 30), CreateGameDeck(1, player1Deck));
            GamePlayer1.BindGame(this);
            GamePlayer2 = new GamePlayer(player2, new Hero(2, 30, 30), CreateGameDeck(2, player2Deck));
            GamePlayer2.BindGame(this);
            RoundCount = 0;
            Random randomGenerator = new Random();
            if (randomGenerator.NextDouble() > 0.5)
            {
                CurrentGamePlayerID = 1;
                GamePlayer1.Draw(3);
                GamePlayer2.Draw(4);
            }
            else
            {
                CurrentGamePlayerID = 2;
                GamePlayer1.Draw(4);
                GamePlayer2.Draw(3);
            }
            Field1 = new Field(1);
            Field1.BindGame(this);
            Field2 = new Field(2);
            Field2.BindGame(this);

            GamePlayer1.OnHasChangedHandChanged += DetectGamePlayerChangeHand;
            GamePlayer2.OnHasChangedHandChanged += DetectGamePlayerChangeHand;

            EventManager = new GameEventManager(this);
        }

        public Game(int gameID, GamePlayer gamePlayer1, GamePlayer gamePlayer2, int roundCount, int currentGamePlayerID, GameCardManager gameCardManager)
        {
            GameID = gameID;
            GamePlayer1 = gamePlayer1;
            GamePlayer2 = gamePlayer2;
            RoundCount = roundCount;
            CurrentGamePlayerID = currentGamePlayerID;
            Field1 = new Field(1);
            Field1.BindGame(this);
            Field2 = new Field(2);
            Field2.BindGame(this);
            GameCardManager = gameCardManager;
            EventManager = new GameEventManager(this);
        }
        public GameDeck CreateGameDeck(int gameDeckID, Deck deck)
        {
            List<int> cardRecordIDs = new List<int>();
            foreach (Card card in deck.Cards)
            {
                cardRecordIDs.Add(GameCardManager.CreateCardRecord(card).CardRecordID);
            }
            GameDeck gameDeck = new GameDeck(gameDeckID, cardRecordIDs);
            gameDeck.Shuffle(100);
            return gameDeck;
        }
        private void DetectGamePlayerChangeHand(GamePlayer gamePlayer)
        {
            if(GamePlayer1.HasChangedHand && GamePlayer2.HasChangedHand)
            {
                RoundStart();
            }
        }

        public void EndRound()
        {
            OnRoundEnd?.Invoke(this);
            if(CurrentGamePlayerID == 1)
            {
                CurrentGamePlayerID = 2;
            }
            else
            {
                CurrentGamePlayerID = 1;
            }
            RoundStart();
        }
        private void RoundStart()
        {
            RoundCount++;
            GamePlayer player = (CurrentGamePlayerID == 1) ? GamePlayer1 : GamePlayer2;
            player.ManaCrystal++;
            player.RemainedManaCrystal = player.ManaCrystal;
            player.Hero.AttackCountInThisTurn = 0;
            Field field = (CurrentGamePlayerID == 1) ? Field1 : Field2;
            foreach(var card in field.Cards(GameCardManager))
            {
                (card as ServantCardRecord).AttackCountInThisTurn = 0;
            }
            OnRoundStart?.Invoke(this);
            player.Draw(1);
        }
        public int SelectGamePlayerID(int playerID)
        {
            if(GamePlayer1.Player.PlayerID == playerID)
            {
                return 1;
            }
            else if(GamePlayer2.Player.PlayerID == playerID)
            {
                return 2;
            }
            else
            {
                return -1;
            }
        }
        public void GameOverTest()
        {
            if(GamePlayer1.Hero.RemainedHP <= 0 && GamePlayer2.Hero.RemainedHP <= 0)
            {
                OnGameOver?.Invoke(this, 0);
            }
            else if(GamePlayer1.Hero.RemainedHP <= 0)
            {
                OnGameOver?.Invoke(this, 1);
            }
            else if(GamePlayer2.Hero.RemainedHP <= 0)
            {
                OnGameOver?.Invoke(this, 2);
            }
        }

        private void UseCard(GamePlayer gamePlayer, CardRecord cardRecord)
        {
            gamePlayer.RemainedManaCrystal -= cardRecord.ManaCost;
            gamePlayer.RemoveHandCard(cardRecord.CardRecordID);
            OnUseCard?.Invoke(this, gamePlayer, cardRecord);
        }
        private bool CanUseCard(GamePlayer gamePlayer, CardRecord cardRecord)
        {
            if (gamePlayer.HandCardIDs.Contains(cardRecord.CardRecordID) && gamePlayer.RemainedManaCrystal >= cardRecord.ManaCost)
                return true;
            else
                return false;
        }

        public bool TargetDisplayServant(int gamePlayerID, int servantCardRecordID, int positionIndex, int targetID, bool isTargetServant)
        {
            if (CurrentGamePlayerID != gamePlayerID || gamePlayerID != 1 && gamePlayerID != 2)
                return false;
            Field field = (gamePlayerID == 1) ? Field1 : Field2;
            if (!field.DisplayCheck(positionIndex))
                return false;
            CardRecord servantCardRecord;
            if (!GameCardManager.FindCard(servantCardRecordID, out servantCardRecord) || !(servantCardRecord is ServantCardRecord))
                return false;
            if (!servantCardRecord.Effectors(GameCardManager).Any(x => x is TargetEffector || (isTargetServant && x is MinionTargetEffector)))
                return false;
            GamePlayer gamePlayer = (gamePlayerID == 1) ? GamePlayer1 : GamePlayer2;
            if (CanUseCard(gamePlayer, servantCardRecord))
            {
                if(isTargetServant)
                {
                    CardRecord targetCardRecord;
                    if(GameCardManager.FindCard(targetID, out targetCardRecord) && targetCardRecord is ServantCardRecord)
                    {
                        UseCard(gamePlayer, servantCardRecord);
                        foreach (var effector in servantCardRecord.Effectors(GameCardManager))
                        {
                            if(effector is TargetEffector)
                            {
                                (effector as TargetEffector).AffectServant(targetCardRecord as ServantCardRecord, gamePlayer);
                            }
                            else if(effector is MinionTargetEffector)
                            {
                                (effector as MinionTargetEffector).AffectServant(targetCardRecord as ServantCardRecord, gamePlayer);
                            }
                            else if(effector is AutoExecutetEffector)
                            {
                                (effector as AutoExecutetEffector).Affect(gamePlayer);
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (targetID != 1 && targetID != 2)
                        return false;
                    else
                    {
                        UseCard(gamePlayer, servantCardRecord);
                        Hero hero = (targetID == 1) ? GamePlayer1.Hero : GamePlayer2.Hero;
                        foreach (var effector in servantCardRecord.Effectors(GameCardManager))
                        {
                            if (effector is TargetEffector)
                            {
                                (effector as TargetEffector).AffectHero(hero, gamePlayer);
                            }
                            else if (effector is AutoExecutetEffector)
                            {
                                (effector as AutoExecutetEffector).Affect(gamePlayer);
                            }
                        }
                    }
                }
                (servantCardRecord as ServantCardRecord).IsDisplayInThisTurn = true;
                field.AddCard(servantCardRecordID, positionIndex);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool NonTargetDisplayServant(int gamePlayerID, int servantCardRecordID, int positionIndex)
        {
            if (CurrentGamePlayerID != gamePlayerID || gamePlayerID != 1 && gamePlayerID != 2)
                return false;
            Field field = (gamePlayerID == 1) ? Field1 : Field2;
            if (!field.DisplayCheck(positionIndex))
                return false;
            CardRecord servantCardRecord;
            if (!GameCardManager.FindCard(servantCardRecordID, out servantCardRecord) || !(servantCardRecord is ServantCardRecord))
                return false;
            if (servantCardRecord.Effectors(GameCardManager).Any(x => x is TargetEffector || x is MinionTargetEffector))
                return false;
            GamePlayer gamePlayer = (gamePlayerID == 1) ? GamePlayer1 : GamePlayer2;
            if (CanUseCard(gamePlayer, servantCardRecord))
            {
                UseCard(gamePlayer, servantCardRecord);
                foreach (var effector in servantCardRecord.Effectors(GameCardManager))
                {
                    if (effector is AutoExecutetEffector)
                    {
                        (effector as AutoExecutetEffector).Affect(gamePlayer);
                    }
                }
                (servantCardRecord as ServantCardRecord).IsDisplayInThisTurn = true;
                field.AddCard(servantCardRecordID, positionIndex);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TargetCastSpell(int gamePlayerID, int spellCardRecordID, int targetID, bool isTargetServant)
        {
            if (CurrentGamePlayerID != gamePlayerID || gamePlayerID != 1 && gamePlayerID != 2)
                return false;
            CardRecord spellCardRecord;
            if (!GameCardManager.FindCard(spellCardRecordID, out spellCardRecord) || !(spellCardRecord is SpellCardRecord))
                return false;
            if (!spellCardRecord.Effectors(GameCardManager).Any(x => x is TargetEffector || (isTargetServant && x is MinionTargetEffector)))
                return false;
            GamePlayer gamePlayer = (gamePlayerID == 1) ? GamePlayer1 : GamePlayer2;
            if (CanUseCard(gamePlayer, spellCardRecord))
            {
                if (isTargetServant)
                {
                    CardRecord targetCardRecord;
                    if (GameCardManager.FindCard(targetID, out targetCardRecord) && targetCardRecord is ServantCardRecord)
                    {
                        UseCard(gamePlayer, spellCardRecord);
                        foreach (var effector in spellCardRecord.Effectors(GameCardManager))
                        {
                            if (effector is TargetEffector)
                            {
                                (effector as TargetEffector).AffectServant(targetCardRecord as ServantCardRecord, gamePlayer);
                            }
                            else if (effector is MinionTargetEffector)
                            {
                                (effector as MinionTargetEffector).AffectServant(targetCardRecord as ServantCardRecord, gamePlayer);
                            }
                            else if (effector is AutoExecutetEffector)
                            {
                                (effector as AutoExecutetEffector).Affect(gamePlayer);
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (targetID != 1 && targetID != 2)
                        return false;
                    else
                    {
                        UseCard(gamePlayer, spellCardRecord);
                        Hero hero = (targetID == 1) ? GamePlayer1.Hero : GamePlayer2.Hero;
                        foreach (var effector in spellCardRecord.Effectors(GameCardManager))
                        {
                            if (effector is TargetEffector)
                            {
                                (effector as TargetEffector).AffectHero(hero, gamePlayer);
                            }
                            else if (effector is AutoExecutetEffector)
                            {
                                (effector as AutoExecutetEffector).Affect(gamePlayer);
                            }
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool NonTargeCasttSpell(int gamePlayerID, int spellCardRecordID)
        {
            if (CurrentGamePlayerID != gamePlayerID || gamePlayerID != 1 && gamePlayerID != 2)
                return false;
            CardRecord spellCardRecord;
            if (!GameCardManager.FindCard(spellCardRecordID, out spellCardRecord) || !(spellCardRecord is SpellCardRecord))
                return false;
            if (spellCardRecord.Effectors(GameCardManager).Any(x => x is TargetEffector || x is MinionTargetEffector))
                return false;
            GamePlayer gamePlayer = (gamePlayerID == 1) ? GamePlayer1 : GamePlayer2;
            if (CanUseCard(gamePlayer, spellCardRecord))
            {
                UseCard(gamePlayer, spellCardRecord);
                foreach (var effector in spellCardRecord.Effectors(GameCardManager))
                {
                    if (effector is AutoExecutetEffector)
                    {
                        (effector as AutoExecutetEffector).Affect(gamePlayer);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TargetEquipWeapon(int gamePlayerID, int weaponCardRecordID, int targetID, bool isTargetServant)
        {
            if (CurrentGamePlayerID != gamePlayerID || gamePlayerID != 1 && gamePlayerID != 2)
                return false;
            CardRecord weaponCardRecord;
            if (!GameCardManager.FindCard(weaponCardRecordID, out weaponCardRecord) || !(weaponCardRecord is WeaponCardRecord))
                return false;
            if (!weaponCardRecord.Effectors(GameCardManager).Any(x => x is TargetEffector || x is MinionTargetEffector))
                return false;
            GamePlayer gamePlayer = (gamePlayerID == 1) ? GamePlayer1 : GamePlayer2;
            if (CanUseCard(gamePlayer, weaponCardRecord))
            {
                if (isTargetServant)
                {
                    CardRecord targetCardRecord;
                    if (GameCardManager.FindCard(targetID, out targetCardRecord) && targetCardRecord is ServantCardRecord)
                    {
                        UseCard(gamePlayer, weaponCardRecord);
                        foreach (var effector in weaponCardRecord.Effectors(GameCardManager))
                        {
                            if (effector is TargetEffector)
                            {
                                (effector as TargetEffector).AffectServant(targetCardRecord as ServantCardRecord, gamePlayer);
                            }
                            else if (effector is MinionTargetEffector)
                            {
                                (effector as MinionTargetEffector).AffectServant(targetCardRecord as ServantCardRecord, gamePlayer);
                            }
                            else if (effector is AutoExecutetEffector)
                            {
                                (effector as AutoExecutetEffector).Affect(gamePlayer);
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (targetID != 1 && targetID != 2)
                        return false;
                    else
                    {
                        UseCard(gamePlayer, weaponCardRecord);
                        Hero hero = (targetID == 1) ? GamePlayer1.Hero : GamePlayer2.Hero;
                        foreach (var effector in weaponCardRecord.Effectors(GameCardManager))
                        {
                            if (effector is TargetEffector)
                            {
                                (effector as TargetEffector).AffectHero(hero, gamePlayer);
                            }
                            else if (effector is AutoExecutetEffector)
                            {
                                (effector as AutoExecutetEffector).Affect(gamePlayer);
                            }
                        }
                    }
                }
                gamePlayer.Hero.WeaponCardRecordID = weaponCardRecordID;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool NonTargetEquipWeapon(int gamePlayerID, int weaponCardRecordID)
        {
            if (CurrentGamePlayerID != gamePlayerID || gamePlayerID != 1 && gamePlayerID != 2)
                return false;
            CardRecord weaponCardRecord;
            if (!GameCardManager.FindCard(weaponCardRecordID, out weaponCardRecord) || !(weaponCardRecord is WeaponCardRecord))
                return false;
            if (weaponCardRecord.Effectors(GameCardManager).Any(x => x is TargetEffector || x is MinionTargetEffector))
                return false;
            GamePlayer gamePlayer = (gamePlayerID == 1) ? GamePlayer1 : GamePlayer2;
            if (CanUseCard(gamePlayer, weaponCardRecord))
            {
                UseCard(gamePlayer, weaponCardRecord);
                foreach (var effector in weaponCardRecord.Effectors(GameCardManager))
                {
                    if (effector is AutoExecutetEffector)
                    {
                        (effector as AutoExecutetEffector).Affect(gamePlayer);
                    }
                }
                gamePlayer.Hero.WeaponCardRecordID = weaponCardRecordID;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ServantAttack(int gamePlayerID, int servantCardRecordID, int targetID, bool isTargetServant)
        {
            if (CurrentGamePlayerID != gamePlayerID || gamePlayerID != 1 && gamePlayerID != 2)
                return false;
            CardRecord servantCardRecord;
            if (!GameCardManager.FindCard(servantCardRecordID, out servantCardRecord) || !(servantCardRecord is ServantCardRecord))
                return false;
            GamePlayer gamePlayer = (gamePlayerID == 1) ? GamePlayer1 : GamePlayer2;
            if (isTargetServant)
            {
                CardRecord targetCardRecord;
                if (GameCardManager.FindCard(targetID, out targetCardRecord) && targetCardRecord is ServantCardRecord)
                {
                    return (servantCardRecord as ServantCardRecord).AttackServant(targetCardRecord as ServantCardRecord, gamePlayer);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (targetID != 1 && targetID != 2)
                    return false;
                else
                {
                    Hero hero = (targetID == 1) ? GamePlayer1.Hero : GamePlayer2.Hero;
                    return (servantCardRecord as ServantCardRecord).AttackHero(hero, gamePlayer);
                }
            }
        }
        public bool HeroAttack(int gamePlayerID, int targetID, bool isTargetServant)
        {
            if (CurrentGamePlayerID != gamePlayerID || gamePlayerID != 1 && gamePlayerID != 2)
                return false;
            GamePlayer gamePlayer = (gamePlayerID == 1) ? GamePlayer1 : GamePlayer2;
            if (isTargetServant)
            {
                CardRecord targetCardRecord;
                if (GameCardManager.FindCard(targetID, out targetCardRecord) && targetCardRecord is ServantCardRecord)
                {
                    return gamePlayer.Hero.AttackServant(targetCardRecord as ServantCardRecord, gamePlayer);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (targetID != 1 && targetID != 2)
                    return false;
                else
                {
                    Hero hero = (targetID == 1) ? GamePlayer1.Hero : GamePlayer2.Hero;
                    return gamePlayer.Hero.AttackHero(hero, gamePlayer);
                }
            }
        }
        public Field OpponentField(int gamePlayerID)
        {
            if(gamePlayerID == 1)
            {
                return Field2;
            }
            else if(gamePlayerID == 2)
            {
                return Field1;
            }
            else
            {
                return null;
            }
        }
        public Field SelfField(int gamePlayerID)
        {
            if (gamePlayerID == 1)
            {
                return Field1;
            }
            else if (gamePlayerID == 2)
            {
                return Field2;
            }
            else
            {
                return null;
            }
        }
        public GamePlayer OpponentGamePlayer(int gamePlayerID)
        {
            if (gamePlayerID == 1)
            {
                return GamePlayer2;
            }
            else if (gamePlayerID == 2)
            {
                return GamePlayer1;
            }
            else
            {
                return null;
            }
        }
        public GamePlayer SelfGamePlayer(int gamePlayerID)
        {
            if (gamePlayerID == 1)
            {
                return GamePlayer1;
            }
            else if (gamePlayerID == 2)
            {
                return GamePlayer2;
            }
            else
            {
                return null;
            }
        }
    }
}

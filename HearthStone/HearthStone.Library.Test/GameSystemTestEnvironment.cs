using System;
using System.Collections.Generic;
using System.Linq;
using HearthStone.Library.CardRecords;
using System.Text;

namespace HearthStone.Library.Test
{
    public static class GameSystemTestEnvironment
    {
        public static Game EmptyGame(int currentGamePlayerID, int roundCount)
        {
            Deck deck1 = new Deck(1, "deck1", 0), deck2 = new Deck(2, "deck2", 0);
            Game game = new Game(1, new Player(1, "player1"), new Player(2, "player2"), deck1, deck2);
            game.CurrentGamePlayerID = currentGamePlayerID;
            game.RoundCount = roundCount;
            return game;
        }
        public static List<ServantCardRecord> GameWithServantCardRecordState(Game game, List<int> cardIDs)
        {
            List<ServantCardRecord> records = new List<ServantCardRecord>();
            foreach(int cardID in cardIDs)
            {
                Card card;
                if(CardManager.Instance.FindCard(cardID, out card))
                {
                    records.Add(game.GameCardManager.CreateCardRecord(card) as ServantCardRecord);
                }
            }
            return records;
        }
        public static List<SpellCardRecord> GameWithSpellCardRecordState(Game game, List<int> cardIDs)
        {
            List<SpellCardRecord> records = new List<SpellCardRecord>();
            foreach (int cardID in cardIDs)
            {
                Card card;
                if (CardManager.Instance.FindCard(cardID, out card))
                {
                    records.Add(game.GameCardManager.CreateCardRecord(card) as SpellCardRecord);
                }
            }
            return records;
        }
        public static List<WeaponCardRecord> GameWithWeaponCardRecordState(Game game, List<int> cardIDs)
        {
            List<WeaponCardRecord> records = new List<WeaponCardRecord>();
            foreach (int cardID in cardIDs)
            {
                Card card;
                if (CardManager.Instance.FindCard(cardID, out card))
                {
                    records.Add(game.GameCardManager.CreateCardRecord(card) as WeaponCardRecord);
                }
            }
            return records;
        }
        public static void GameWithFieldState(Game game, List<int> field1CardRecordIDs, List<int> field2CardRecordIDs)
        {
            foreach(int fieldCardRecordID in field1CardRecordIDs.Reverse<int>())
            {
                game.Field1.AddCard(fieldCardRecordID, 0);
            }
            foreach (int fieldCardRecordID in field2CardRecordIDs.Reverse<int>())
            {
                game.Field2.AddCard(fieldCardRecordID, 0);
            }
        }
        public static void GameWithGamePlayerManaCrystalState(Game game, int gamePlayerID, int manaCrystal, int remainedManaCrystal)
        {
            GamePlayer player = game.SelfGamePlayer(gamePlayerID);
            player.ManaCrystal = manaCrystal;
            player.RemainedManaCrystal = remainedManaCrystal;
        }
        public static void GameWithGamePlayerDeckState(Game game, int gamePlayerID, List<int> deckCardRecordIDs)
        {
            GamePlayer player = game.SelfGamePlayer(gamePlayerID);
            foreach(int cardRecordID in deckCardRecordIDs)
            {
                player.Deck.AddCard(cardRecordID);
            }
        }
        public static void GameWithGamePlayerHandState(Game game, int gamePlayerID, List<int> handCardRecordIDs)
        {
            GamePlayer player = game.SelfGamePlayer(gamePlayerID);
            foreach (int cardRecordID in handCardRecordIDs)
            {
                player.AddHandCard(cardRecordID);
            }
        }
        public static void GameWithGamePlayerHeroState(Game game, int gamePlayerID, int attack, int attackCountInThisTurn, int hp, int remainedHP, int weaponCardRecordID)
        {
            Hero hero = game.SelfGamePlayer(gamePlayerID).Hero;
            hero.Attack = attack;
            hero.AttackCountInThisTurn = attackCountInThisTurn;
            hero.HP = hp;
            hero.RemainedHP = remainedHP;
            hero.WeaponCardRecordID = weaponCardRecordID;
        }
    }
}

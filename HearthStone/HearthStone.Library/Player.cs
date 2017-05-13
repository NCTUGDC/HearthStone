using HearthStone.Library.CommunicationInfrastructure.Event.Managers;
using HearthStone.Library.CommunicationInfrastructure.Operation.Managers;
using HearthStone.Library.CommunicationInfrastructure.Response.Managers;
using HearthStone.Protocol;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace HearthStone.Library
{
    public class Player
    {
        public EndPoint EndPoint { get; set; }
        public int PlayerID { get; private set; }
        public IPAddress LastConnectedIPAddress { get; set; }
        public string Account { get; private set; }
        public string Nickname { get; private set; }
        private Dictionary<int, Deck> deckDictionary = new Dictionary<int, Deck>();
        public IEnumerable<Deck> Decks { get { return deckDictionary.Values.ToArray(); } }

        public PlayerEventManager EventManager { get; private set; }
        public PlayerOperationManager OperationManager { get; private set; }
        public PlayerResponseManager ResponseManager { get; private set; }

        public delegate void DeckChangedEventHandler(Deck deck, DataChangeCode changeCode);
        private event DeckChangedEventHandler onDeckChanged;
        public event DeckChangedEventHandler OnDeckChanged { add { onDeckChanged += value; } remove { onDeckChanged -= value; } }

        public Player(int playerID, IPAddress lastConnectedIPAddress, string account, string nickname)
        {
            PlayerID = playerID;
            LastConnectedIPAddress = lastConnectedIPAddress;
            Account = account;
            Nickname = nickname;

            EventManager = new PlayerEventManager(this);
            OperationManager = new PlayerOperationManager(this);
            ResponseManager = new PlayerResponseManager(this);
        }
        public Player(int playerID, string nickname)
        {
            PlayerID = playerID;
            Nickname = nickname;
        }
        public override string ToString()
        {
            return $"PlayerID: {PlayerID}";
        }
        public bool FindDeck(int deckID, out Deck deck)
        {
            if (deckDictionary.ContainsKey(deckID))
            {
                deck = deckDictionary[deckID];
                return true;
            }
            else
            {
                deck = null;
                return false;
            }
        }
        public void LoadDeck(Deck deck)
        {
            if(!deckDictionary.ContainsKey(deck.DeckID))
            {
                deckDictionary.Add(deck.DeckID, deck);
                onDeckChanged?.Invoke(deck, DataChangeCode.Add);
            }
        }
        public bool RemoveDeck(int deckID)
        {
            if (deckDictionary.ContainsKey(deckID))
            {
                Deck deck = deckDictionary[deckID];
                deckDictionary.Remove(deckID);
                onDeckChanged?.Invoke(deck, DataChangeCode.Remove);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

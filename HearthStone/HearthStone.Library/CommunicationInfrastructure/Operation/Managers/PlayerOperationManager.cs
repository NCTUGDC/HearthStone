using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers;
using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.FetchDataParameters;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Managers
{
    public class PlayerOperationManager
    {
        private readonly Player player;
        private readonly Dictionary<PlayerOperationCode, OperationHandler<Player, PlayerOperationCode>> operationTable = new Dictionary<PlayerOperationCode, OperationHandler<Player, PlayerOperationCode>>();
        public PlayerFetchDataBroker FetchDataBroker { get; private set; }

        internal PlayerOperationManager(Player player)
        {
            this.player = player;
            FetchDataBroker = new PlayerFetchDataBroker(player);

            operationTable.Add(PlayerOperationCode.FetchData, FetchDataBroker);
            operationTable.Add(PlayerOperationCode.CreateDeck, new CreateDeckHandler(player));
            operationTable.Add(PlayerOperationCode.DeleteDeck, new DeleteDeckHandler(player));
            operationTable.Add(PlayerOperationCode.AddCardToDeck, new AddCardToDeckHandler(player));
            operationTable.Add(PlayerOperationCode.RemoveCardFromDeck, new RemoveCardFromDeckHandler(player));
            operationTable.Add(PlayerOperationCode.FindOpponent, new FindOpponentHandler(player));

            operationTable.Add(PlayerOperationCode.SwapHands, new SwapHandsHandler(player));
            operationTable.Add(PlayerOperationCode.TargetDisplayServant, new TargetDisplayServantHandler(player));
            operationTable.Add(PlayerOperationCode.NonTargetDisplayServant, new NonTargetDisplayServantHandler(player));
            operationTable.Add(PlayerOperationCode.TargetCastSpell, new TargetCastSpellHandler(player));
            operationTable.Add(PlayerOperationCode.NonTargeCasttSpell, new NonTargeCasttSpellHandler(player));
            operationTable.Add(PlayerOperationCode.EquipWeapon, new EquipWeaponHandler(player));
            operationTable.Add(PlayerOperationCode.ServantAttackHero, new ServantAttackHeroHandler(player));
            operationTable.Add(PlayerOperationCode.ServantAttackServant, new ServantAttackServantHandler(player));
            operationTable.Add(PlayerOperationCode.HeroAttackHero, new HeroAttackHeroHandler(player));
            operationTable.Add(PlayerOperationCode.HeroAttackServant, new HeroAttackServantHandler(player));
            operationTable.Add(PlayerOperationCode.EndTurn, new EndTurnHandler(player));
        }
        internal bool Operate(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (operationTable[operationCode].Handle(operationCode, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"PlayerOperation Error: {operationCode} from Player: {player.PlayerID}\nErrorMessahe: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow PlayerOperation:{operationCode} from Player: {player.PlayerID}";
                return false;
            }
        }
        internal void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            player.EndPoint.OperationManager.SendPlayerOperation(player, operationCode, parameters);
        }

        internal void SendFetchDataOperation(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)FetchDataParameterCode.Parameters, parameters }
            };
            SendOperation(PlayerOperationCode.FetchData, fetchDataParameters);
        }

        public void CreateDeck(string deckName)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)CreateDeckParameterCode.DeckName, deckName }
            };
            SendOperation(PlayerOperationCode.CreateDeck, parameters);
        }
        public void DeleteDeck(int deckID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)DeleteDeckParameterCode.DeckID, deckID }
            };
            SendOperation(PlayerOperationCode.DeleteDeck, parameters);
        }
        public void AddCardToDeck(int deckID, int cardID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)AddCardToDeckParameterCode.DeckID, deckID },
                { (byte)AddCardToDeckParameterCode.CardID, cardID }
            };
            SendOperation(PlayerOperationCode.AddCardToDeck, parameters);
        }
        public void RemoveCardFromDeck(int deckID, int cardID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)RemoveCardFromDeckParameterCode.DeckID, deckID },
                { (byte)RemoveCardFromDeckParameterCode.CardID, cardID }
            };
            SendOperation(PlayerOperationCode.RemoveCardFromDeck, parameters);
        }
        public void FindOpponent(int deckID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)FindOpponentParameterCode.DeckID, deckID }
            };
            SendOperation(PlayerOperationCode.FindOpponent, parameters);
        }
        public void SwapHands(int gameID, int[] swapCardRecordIDs)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)SwapHandsParameterCode.GameID, gameID },
                { (byte)SwapHandsParameterCode.SwapCardRecordID_Array, swapCardRecordIDs }
            };
            SendOperation(PlayerOperationCode.SwapHands, parameters);
        }
        public void TargetDisplayServant(int gameID, int servantCardRecordID, int positionIndex, int targetCardRecordID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)TargetDisplayServantParameterCode.GameID, gameID },
                { (byte)TargetDisplayServantParameterCode.ServantCardRecordID, servantCardRecordID },
                { (byte)TargetDisplayServantParameterCode.PositionIndex, positionIndex },
                { (byte)TargetDisplayServantParameterCode.TargetCardRecordID, targetCardRecordID }
            };
            SendOperation(PlayerOperationCode.TargetDisplayServant, parameters);
        }
        public void NonTargetDisplayServant(int gameID, int servantCardRecordID, int positionIndex)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)NonTargetDisplayServantParameterCode.GameID, gameID },
                { (byte)NonTargetDisplayServantParameterCode.ServantCardRecordID, servantCardRecordID },
                { (byte)NonTargetDisplayServantParameterCode.PositionIndex, positionIndex }
            };
            SendOperation(PlayerOperationCode.NonTargetDisplayServant, parameters);
        }
        public void TargetCastSpell(int gameID, int spellCardRecordID, int targetCardRecordID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)TargetCastSpellParameterCode.GameID, gameID },
                { (byte)TargetCastSpellParameterCode.SpellCardRecordID, spellCardRecordID },
                { (byte)TargetCastSpellParameterCode.TargetCardRecordID, targetCardRecordID },
            };
            SendOperation(PlayerOperationCode.TargetCastSpell, parameters);
        }
        public void NonTargeCasttSpell(int gameID, int spellCardRecordID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)NonTargeCasttSpellParameterCode.GameID, gameID },
                { (byte)NonTargeCasttSpellParameterCode.SpellCardRecordID, spellCardRecordID }
            };
            SendOperation(PlayerOperationCode.NonTargeCasttSpell, parameters);
        }
        public void EquipWeapon(int gameID, int weaponCardRecordID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)EquipWeaponParameterCode.GameID, gameID },
                { (byte)EquipWeaponParameterCode.WeaponCardRecordID, weaponCardRecordID }
            };
            SendOperation(PlayerOperationCode.EquipWeapon, parameters);
        }
        public void ServantAttackHero(int gameID, int servantCardRecordID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)ServantAttackHeroParameterCode.GameID, gameID },
                { (byte)ServantAttackHeroParameterCode.ServantCardRecordID, servantCardRecordID }
            };
            SendOperation(PlayerOperationCode.ServantAttackHero, parameters);
        }
        public void ServantAttackServant(int gameID, int servantCardRecordID, int targetCardRecordID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)ServantAttackServantParameterCode.GameID, gameID },
                { (byte)ServantAttackServantParameterCode.ServantCardRecordID, servantCardRecordID },
                { (byte)ServantAttackServantParameterCode.TargetCardRecordID, targetCardRecordID }
            };
            SendOperation(PlayerOperationCode.ServantAttackServant, parameters);
        }
        public void HeroAttackHero(int gameID, int[] swapCardRecordIDs)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)HeroAttackHeroParameterCode.GameID, gameID }
            };
            SendOperation(PlayerOperationCode.HeroAttackHero, parameters);
        }
        public void HeroAttackServant(int gameID, int servantCardRecordID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)HeroAttackServantParameterCode.GameID, gameID },
                { (byte)HeroAttackServantParameterCode.ServantCardRecordID, servantCardRecordID }
            };
            SendOperation(PlayerOperationCode.HeroAttackServant, parameters);
        }
        public void EndTurn(int gameID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)EndTurnParameterCode.GameID, gameID }
            };
            SendOperation(PlayerOperationCode.EndTurn, parameters);
        }
    }
}

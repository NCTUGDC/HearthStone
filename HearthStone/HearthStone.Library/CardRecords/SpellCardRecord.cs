﻿using System;

namespace HearthStone.Library.CardRecords
{
    public class SpellCardRecord : CardRecord
    {
        public SpellCardRecord() { }
        public SpellCardRecord(int cardRecordID, Card card) : base(cardRecordID, card)
        {
        }
    }
}
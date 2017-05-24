using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class FieldUnitTest
    {
        [TestMethod]
        public void ConstructorTestMethod1()
        {
            Field field = new Field();
        
            Assert.IsNotNull(field);
        }
        [TestMethod]
        public void ConstructorTestMethod2()
        {
            Field field = new Field(1);

            Assert.IsNotNull(field);
            Assert.AreEqual(field.FieldID, 1);
        }

        [TestMethod]
        public void AddCardTestMethod1()
        {
            Field field = new Field(1);
            field.AddCard(5, 0);

            var cards = field.FieldCards.GetEnumerator();
            Assert.IsTrue(cards.MoveNext());
            Assert.AreEqual(cards.Current.CardRecordID, 5);
            Assert.AreEqual(cards.Current.PositionIndex, 0);
        }

        [TestMethod]
        public void AddCardTestMethod2()
        {
            Field field = new Field(1);
            field.AddCard(5, 3);

            var cards = field.FieldCards.GetEnumerator();
            Assert.IsFalse(cards.MoveNext());
        }

        [TestMethod]
        public void AddCardTestMethod3()
        {
            Field field = new Field(1);
            Assert.IsTrue(field.AddCard(5, 0));
            var cards = field.FieldCards.GetEnumerator();
            Assert.IsTrue(cards.MoveNext());
            int eventCallCounter = 0;

            field.OnCardChanged += (fieldCard, chageCode) =>
            {
                Assert.AreEqual(chageCode, Protocol.DataChangeCode.Update);
                eventCallCounter++;
            };

        }

        [TestMethod]
        public void AddCardTestMethod4()
        {
            Field field = new Field(1);
            int eventCallCounter_add = 0;
            int eventCallCounter_update = 0;
            field.OnCardChanged += (fieldCard, chageCode) =>
            {
                if (chageCode == Protocol.DataChangeCode.Update)
                    eventCallCounter_update++;
                else
                    eventCallCounter_add++;
            };
            Assert.IsTrue(field.AddCard(5, 0));
            Assert.IsTrue(field.UpdateCard(5, 1));
            Assert.IsTrue(field.AddCard(6, 0));

            Assert.AreEqual(eventCallCounter_add, 2);
            Assert.AreEqual(eventCallCounter_update, 2);
        }

        [TestMethod]
        public void RemoveCardTestMethod1()
        {
            Field field = new Field(1);
            field.AddCard(5, 0);
            field.RemoveCard(5);

            var cards = field.FieldCards.GetEnumerator();            
            Assert.IsFalse(cards.MoveNext());

        }

        [TestMethod]
        public void RemoveCardTestMethod2()
        {
            Field field = new Field(1);
            field.AddCard(5, 0);
            field.AddCard(3, 1);
            field.RemoveCard(6);

            var cards = field.FieldCards.GetEnumerator();
            Assert.IsTrue(cards.MoveNext());
            Assert.AreEqual(cards.Current.CardRecordID, 5);
            Assert.AreEqual(cards.Current.PositionIndex, 0);
            Assert.IsTrue(cards.MoveNext());
            Assert.AreEqual(cards.Current.CardRecordID, 3);
            Assert.AreEqual(cards.Current.PositionIndex, 1);

        }

        [TestMethod]
        public void RemoveCardTestMethod3()
        {
            Field field = new Field(1);
            field.AddCard(5, 0);
            field.AddCard(3, 1);
            field.RemoveCard(5);

            var cards = field.FieldCards.GetEnumerator();
            Assert.IsTrue(cards.MoveNext());
            Assert.AreEqual(cards.Current.CardRecordID, 3);
            Assert.AreEqual(cards.Current.PositionIndex, 0);
        }

        [TestMethod]
        public void RemoveCardTestMethod4()
        {
            Field field = new Field(1);
            field.AddCard(5, 0);
            field.AddCard(3, 1);
            field.RemoveCard(3);

            var cards = field.FieldCards.GetEnumerator();
            Assert.IsTrue(cards.MoveNext());
            Assert.AreEqual(cards.Current.CardRecordID, 5);
            Assert.AreEqual(cards.Current.PositionIndex, 0);
        }

        [TestMethod]
        public void RemoveCardTestMethod5()
        {
            Field field = new Field(1);

            int eventCallCounter_remove = 0;
            int eventCallCounter_update = 0;
            field.OnCardChanged += (fieldCard, chageCode) =>
            {
                if (chageCode == Protocol.DataChangeCode.Remove)
                    eventCallCounter_remove++;
                else
                    eventCallCounter_update++;
            };

            field.AddCard(5, 0);
            field.AddCard(3, 1);
            field.RemoveCard(3);

            Assert.AreEqual(eventCallCounter_remove, 1);
            Assert.AreEqual(eventCallCounter_update, 2);
        }

        [TestMethod]
        public void RemoveCardTestMethod6()
        {
            Field field = new Field(1);

            int eventCallCounter_remove = 0;
            int eventCallCounter_update = 0;
            field.OnCardChanged += (fieldCard, chageCode) =>
            {
                if (chageCode == Protocol.DataChangeCode.Remove)
                    eventCallCounter_remove++;
                else
                    eventCallCounter_update++;
            };

            field.AddCard(5, 0);
            field.AddCard(3, 1);
            field.RemoveCard(5);

            Assert.AreEqual(eventCallCounter_remove, 1);
            Assert.AreEqual(eventCallCounter_update, 3);
        }

        [TestMethod]
        public void UpdateCardTestMethod1()
        {
            Field field = new Field(1);
            Assert.IsTrue(field.AddCard(5, 0));
            Assert.IsTrue(field.UpdateCard(5, 2));

            var cards = field.FieldCards.GetEnumerator();
            Assert.IsTrue(cards.MoveNext());
            Assert.AreEqual(cards.Current.CardRecordID, 5);
            Assert.AreEqual(cards.Current.PositionIndex, 2);

        }

        [TestMethod]
        public void UpdateCardTestMethod2()
        {
            Field field = new Field(1);
            Assert.IsTrue(field.AddCard(5, 0));
            Assert.IsFalse(field.UpdateCard(6, 2));

            var cards = field.FieldCards.GetEnumerator();
            Assert.IsTrue(cards.MoveNext());
            Assert.AreEqual(cards.Current.CardRecordID, 5);
            Assert.AreEqual(cards.Current.PositionIndex, 0);
        }


        [TestMethod]
        public void BindGameTestMethod()
        {
            var game = GameUnitTest.InitialGameStatus();
            Field field = new Field(1);
            field.BindGame(game);
            Assert.AreEqual(field.Game, game);
        }

        [TestMethod]
        public void AnyTauntServantTestMethod()
        {
            Field field = new Field(1);
            var game = GameUnitTest.InitialGameStatus();
            field.BindGame(game);
            Assert.IsTrue(field.AddCard(5, 0));
            Assert.IsFalse(field.AnyTauntServant());
        }

        [TestMethod]
        public void DisplayCheckTestMethod1()
        {
            Field field = new Field(1);
            field.AddCard(5, 0);

            Assert.IsTrue(field.DisplayCheck(0));
        }

        [TestMethod]
        public void DisplayCheckTestMethod2()
        {
            Field field = new Field(1);
            field.AddCard(5, 0);

            Assert.IsFalse(field.DisplayCheck(-1));
        }

        [TestMethod]
        public void DisplayCheckTestMethod3()
        {
            Field field = new Field(1);
            for (int i = 0; i < 7; i++)
                field.AddCard(i, i);

            Assert.IsFalse(field.DisplayCheck(3));
        }

        [TestMethod]
        public void FindCardWithPositionIndexTestMethod1()
        {
            Field field = new Field(1);
            field.AddCard(5, 0);

            int cardID = 2;
            Assert.IsFalse(field.FindCardWithPositionIndex(2, out cardID));
        }

        [TestMethod]
        public void FindCardWithPositionIndexTestMethod2()
        {
            Field field = new Field(1);
            field.AddCard(5, 0);

            int cardID = 2;
            Assert.IsTrue(field.FindCardWithPositionIndex(0, out cardID));
        }
    }
}

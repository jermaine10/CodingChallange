using System;
using System.Linq;
using System.Collections.Generic;
public class PokerHand : IComparable
{

    public enum HandRank
    {
        HIGHCARD,
        PAIR,
        TWOPAIR,
        THREEOFAKIND,
        STRAIGHT,
        FLUSH,
        FULLHOUSE,
        FOUROFAKIND,
        STRAIGHTFLUSH
    }
    List<Card> cards = new List<Card>();


    HandRank handRank;
    string h;
    public PokerHand(string hand)
    {
        foreach (var x in hand.Split(' '))
        {
            cards.Add(new Card(x));
        }
        cards = cards.OrderByDescending(d => d).ToList();

        h = string.Join(" ", cards.Select(x => x.card));
        CalculateHands();
    }

    private void CalculateHands()
    {
        if (IsStraightFlush()) handRank = HandRank.STRAIGHTFLUSH;
        else if (IsFourOfAKind()) handRank = HandRank.FOUROFAKIND;
        else if (IsFullHouse()) handRank = HandRank.FULLHOUSE;
        else if (IsFlush()) handRank = HandRank.FLUSH;
        else if (IsStraight()) handRank = HandRank.STRAIGHT;
        else if (IsThreeOfAKind()) handRank = HandRank.THREEOFAKIND;
        else if (IsTwoPair()) handRank = HandRank.TWOPAIR;
        else if (IsPair()) handRank = HandRank.PAIR;
        else handRank = HandRank.HIGHCARD;
    }

    private bool IsTwoPair()
    {
        int card1 = cards[0].GetValue();
        int card2 = cards[1].GetValue();
        int card3 = cards[2].GetValue();
        int card4 = cards[3].GetValue();
        int card5 = cards[4].GetValue();
        if ((card1 == card2 && card3 == card4) || (card2 == card3 && card4 == card5) ||
            (card1 == card2 && card4 == card5))
            return true;
        return false;
    }

    private bool IsPair()
    {
        for (int i = 0; i < cards.Count - 1; i++)
        {
            Card c = cards[i];
            if (c.GetValue() == cards[i + 1].GetValue())
                return true;
        }
        return false;
    }

    private bool IsThreeOfAKind()
    {
        if ((cards[0].GetValue() == cards[2].GetValue()) ||
             (cards[1].GetValue() == cards[3].GetValue())
           || (cards[2].GetValue() == cards[4].GetValue()))
            return true;

        return false;
    }

    private bool IsFullHouse()
    {
        if ((cards[0].GetValue() == cards[2].GetValue() && cards[3].GetValue() == cards[4].GetValue()) ||
            (cards[0].GetValue() == cards[1].GetValue() && cards[2].GetValue() == cards[4].GetValue())
            )
            return true;
        return false;
    }

    private bool IsFourOfAKind()
    {
        if (cards[0].GetValue() == cards[3].GetValue() || cards[1].GetValue() == cards[4].GetValue())
            return true;
        return false;
    }

    private bool IsStraightFlush()
    {
        return IsFlush() == IsStraight() && IsFlush() == true;
    }

    public bool IsFlush()
    {
        var tempRank = cards[0].GetRank();
        foreach (var x in cards)
        {
            if (tempRank != x.GetRank())
                return false;
        }
        return true;
    }
    public bool IsStraight()
    {
        var tempCard = cards[0].GetValue();
        for (int i = 1; i < cards.Count; i++)
        {
            if (tempCard - cards[i].GetValue() != i && tempCard - cards[i].GetValue() - 7 - i != 1)
                return false;

        }
        return true;
    }

    public HandRank GetHandRank()
    {
        return handRank;
    }


    public int CompareTo(object obj)
    {
        var val = (PokerHand)obj;
        if (this.GetHandRank() > val.GetHandRank()) return -1;
        else if (this.GetHandRank() < val.GetHandRank()) return 1;
        else
        {
            if (GetHandRank() == HandRank.FLUSH || GetHandRank() == HandRank.HIGHCARD)
            {
                return CompareHighCard(cards, val.cards);
            }
            else if (GetHandRank() == HandRank.STRAIGHT || GetHandRank() == HandRank.STRAIGHTFLUSH)
            {
                return CompareStraightCard(cards.OrderBy(x => x).ToList(), val.cards.OrderBy(x => x).ToList());
            }
            else if (GetHandRank() == HandRank.FOUROFAKIND)
            {
                return CompareFourofAKind(cards, val.cards);
            }
            else if (GetHandRank() == HandRank.THREEOFAKIND || GetHandRank() == HandRank.FULLHOUSE)
            {
                return CompareThreeOfAKind(cards, val.cards);
            }
            else if (GetHandRank() == HandRank.TWOPAIR)
            {
                return CompareTwoPairs(cards, val.cards);
            }
            else if (GetHandRank() == HandRank.PAIR)
            {
                return ComparePairs(cards, val.cards);
            }
            return 0;
        }
    }

    private int CompareStraightCard(List<Card> list1, List<Card> list2)
    {
        var x = list1.Sum(d => d.GetValue()) + 10;
        var y = list2.Sum(d => d.GetValue()) + 10;
        var val1 = x == 28 ? 10 : x;
        var val2 = y == 28 ? 10 : y;
        if (val1 > val2) return -1;
        else if (val1 < val2) return 1;
        else return 0;

    }

    private int CompareThreeOfAKind(List<Card> cards1, List<Card> cards2)
    {
        List<Card> leftoverCards1 = new List<Card>();
        List<Card> leftoverCards2 = new List<Card>();
        int pairValue1 = cards1[2].GetValue(), pairValue2 = cards2[2].GetValue();
        for (int i = 0; i < cards1.Count; i++)
        {
            Card c = cards1[i];
            if (i < cards1.Count - 2)
            {
                if (c.GetValue() == cards1[i + 1].GetValue() && c.GetValue() == cards[i + 2].GetValue())
                {
                    i += 2;
                    continue;
                }
            }
            leftoverCards1.Add(c);

        }
        for (int i = 0; i < cards2.Count; i++)
        {
            Card c = cards2[i];
            if (i < cards1.Count - 2)
            {

                if (c.GetValue() == cards2[i + 1].GetValue() && c.GetValue() == cards2[i + 2].GetValue())
                {
                    i += 2;
                    continue;
                }
            }
            leftoverCards2.Add(c);
        }
        if (pairValue1 > pairValue2) return -1;
        else if (pairValue1 < pairValue2) return 1;
        else return CompareHighCard(leftoverCards1, leftoverCards2);
    }

    private int ComparePairs(List<Card> cards1, List<Card> cards2)
    {
        List<Card> leftoverCards1 = new List<Card>();
        List<Card> leftoverCards2 = new List<Card>();
        int pairValue1 = -1, pairValue2 = -1;
        for (int i = 0; i < cards1.Count; i++)
        {
            Card c = cards1[i];
            if (i < cards1.Count - 1 && pairValue1 == -1)
            {
                if (c.GetValue() == cards1[i + 1].GetValue())
                {
                    pairValue1 = c.GetValue();

                    i += 1;
                    continue;

                }
            }
            leftoverCards1.Add(c);

        }
        for (int i = 0; i < cards2.Count; i++)
        {
            Card c = cards2[i];
            if (i < cards2.Count - 1 && pairValue2 == -1)
            {
                if (c.GetValue() == cards2[i + 1].GetValue())
                {
                    pairValue2 = c.GetValue();
                    i += 1;
                    continue;
                }
            }
            leftoverCards2.Add(c);
        }
        if (pairValue1 > pairValue2) return -1;
        else if (pairValue1 < pairValue2) return 1;
        else return CompareHighCard(leftoverCards1, leftoverCards2);
    }

    private int CompareTwoPairs(List<Card> cards1, List<Card> cards2)
    {
        List<int> pairList1 = new List<int>();
        List<int> pairList2 = new List<int>();
        int highcard1 = 0, highcard2 = 0;
        for (int i = 0; i < cards1.Count; i++)
        {
            Card c = cards1[i];
            if (pairList1.Count < 2)
            {
                if (c.GetValue() == cards1[i + 1].GetValue())
                {
                    pairList1.Add(c.GetValue());
                    i += 1;
                    continue;
                }
            }
            highcard1 = c.GetValue();

        }
        for (int i = 0; i < cards2.Count; i++)
        {
            Card c = cards2[i];
            if (pairList2.Count < 2)
            {
                if (c.GetValue() == cards2[i + 1].GetValue())
                {
                    pairList2.Add(c.GetValue());
                    i += 1;
                    continue;
                }
            }
            highcard2 = c.GetValue();

        }

        if (pairList1[0] > pairList2[0]) return -1;
        else if (pairList1[0] < pairList2[0]) return 1;
        else if (pairList1[1] > pairList2[1]) return -1;
        else if (pairList1[1] < pairList2[1]) return 1;
        else if (highcard1 > highcard2) return -1;
        else if (highcard1 < highcard2) return 1;
        else return 0;
    }

    private int CompareFourofAKind(List<Card> cards1, List<Card> cards2)
    {
        int highCard1, highCard2;
        if (cards1[2].GetValue() > cards2[2].GetValue()) return -1;
        else if (cards1[2].GetValue() < cards2[2].GetValue()) return 1;
        if (cards1[0].GetValue() == cards1[1].GetValue())
            highCard1 = cards1[4].GetValue();
        else
            highCard1 = cards1[0].GetValue();
        if (cards2[0].GetValue() == cards2[1].GetValue())
            highCard2 = cards2[4].GetValue();
        else
            highCard2 = cards2[0].GetValue();
        if (highCard1 > highCard2) return -1;
        else if (highCard1 < highCard2) return 1;
        return 0;
    }

    private int CompareHighCard(List<Card> cards1, List<Card> cards2)
    {
        for (int i = 0; i < cards1.Count; i++)
        {
            if (cards1[i].GetValue() > cards2[i].GetValue()) return -1;
            else if (cards1[i].GetValue() < cards2[i].GetValue()) return 1;
        }
        return 0;
    }

    private class Card : IComparable
    {

        private int value;
        private int rank;

        public string card;
        private const string values = "23456789TJQKA";
        private const string suits = "CDHS";

        public Card(string s)
        {
            AssignValue(s);
            card = s;
        }

        private void AssignValue(string s)
        {
            rank = suits.IndexOf(s[1]);
            value = values.IndexOf(s[0]);
        }

        public int GetValue()
        {
            return value;
        }

        public int GetRank()
        {
            return rank;
        }

        public int CompareTo(object obj)
        {
            Card val = (Card)obj;
            if (this.value < val.value) return -1;
            else if (this.value > val.value) return 1;
            else return 0;
        }
    }

}

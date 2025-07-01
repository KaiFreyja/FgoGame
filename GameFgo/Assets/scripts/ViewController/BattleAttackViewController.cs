using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleAttackViewController : ViewController
{
    /// <summary>
    /// 取消鍵
    /// </summary>
    public Button btnCancel = null;

    /// <summary>
    /// card 的定位點
    /// </summary>
    public GameObject[] cardNode = null;

    /// <summary>
    /// 選擇的卡片
    /// </summary>
    Card[] selectCards = new Card[3];

    public Action<Card[]> onSelectFin = null;

    protected override void init()
    {
        base.init();
        clearCards();
        btnCancel.onClick.AddListener(()=>
        {
            close();
        });
    }

    public void showCards(Card[] cards, Master[] players)
    {
        selectCards = new Card[3];
        clearCards();

        for (int i = 0; i < cards.Length; i++)
        {
            var card = cards[i];
            Master master = null;
            foreach (var a in players)
            {
                if (a.id == card.mid)
                {
                    master = a;
                    break;
                }
            }
            string cardName = "";
            switch (card.color)
            {
                case Card.CardColor.RED:
                    cardName = "red_card";
                    break;

                case Card.CardColor.BULE:
                    cardName = "blue_card";
                    break;

                case Card.CardColor.GREEN:
                    cardName = "green_card";
                    break;
            }

            GameObject cardObj = GameObject.Instantiate(Resources.Load<GameObject>(Config.RESOURCE_CARD_PATH + cardName));
            cardObj.transform.SetParent(cardNode[i].transform,false);
            cardObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            var cardui = cardObj.GetComponent<BattleAttackCard>();
            cardui.showCard(card, master);
            int index = i;
            cardui.onChangedSelect = (bool isSelect) =>
            {
                if (isSelect)
                {
                    for (int i = 0; i < selectCards.Length; i++)
                    {
                        if (selectCards[i] == null)
                        {
                            selectCards[i] = card;
                            cardui.SetSortNum(i + 1);

                            if (i == selectCards.Length - 1)
                            {
                                ///跳畫面
                                backAndNextView();
                            }
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < selectCards.Length; i++)
                    {
                        if (selectCards[i] == card)
                        {
                            selectCards[i] = null;
                            break;
                        }
                    }
                }
            };
        }
    }

    private void clearCards()
    {
        //cards = null;
        foreach (var a in cardNode)
        {
            if (a.transform.childCount > 0)
            {
                GameObject.Destroy(a.transform.GetChild(0).gameObject);
            }
        }
    }

    private void backAndNextView()
    {
        onSelectFin.Invoke(selectCards);
        close();
    }

}

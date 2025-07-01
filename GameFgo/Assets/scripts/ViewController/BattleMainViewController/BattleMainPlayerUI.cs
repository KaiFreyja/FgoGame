using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class BattleMainPlayerUI : MonoBehaviour
{
    /// <summary>
    /// ì™ëú
    /// </summary>
    public Image imgHead = null;
    /// <summary>
    /// icon
    /// </summary>
    public Image imgIcon = null;
    /// <summary>
    /// name
    /// </summary>
    public Text labName = null;
    /// <summary>
    /// hp
    /// </summary>
    public Text labHp = null;
    /// <summary>
    /// hp bar
    /// </summary>
    public UguiBar barHp = null;
    /// <summary>
    /// np 
    /// </summary>
    public Text labNp = null;
    /// <summary>
    /// np bar
    /// </summary>
    public UguiBar barNp = null;
    /// <summary>
    /// ãZî\à¬Á‰
    /// </summary>
    public Button[] btnSkills = null;

    /// <summary>
    /// Ë˚é¶ödà éëêu
    /// </summary>
    /// <param name="player"></param>
    public void ShowMaster(Master player)
    {
        if (player == null)
        {
            return;
        }

        imgHead.sprite = Resources.Load<Sprite>(Config.RESOURCE_HEAD_PATH + player.resourceHead);
        imgIcon.sprite = Resources.Load<Sprite>(Config.RESOURCE_PROFESSION_PATH + player.professionId);

        labName.text = player.name;
        labHp.text = player.hp.ToString();
        barHp.SetValue(player.hp, player.maxHp);
        labNp.text = player.np + "%";
        barNp.SetValue(player.np, 100);
    }
}

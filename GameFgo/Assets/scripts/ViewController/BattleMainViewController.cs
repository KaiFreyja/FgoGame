using UnityEngine;
using UnityEngine.UI;

public class BattleMainViewController : ViewController
{
    /// <summary>
    /// 敵方單位的欄位
    /// </summary>
    public BattleMainEnemyUI[] enemyUI = null;

    /// <summary>
    /// 我方單位
    /// </summary>
    public BattleMainPlayerUI[] playerUI = null;

    /// <summary>
    /// 回合顯示
    /// </summary>
    public Text labTurn = null;
    /// <summary>
    /// 敵方數
    /// </summary>
    public Text labEnemy = null;
    /// <summary>
    /// 關卡資訊
    /// </summary>
    public Text labBattle = null;

    /// <summary>
    /// 出牌鍵
    /// </summary>
    public Button btnAttack = null;
    /// <summary>
    /// 戰鬥清單
    /// </summary>
    public Button btnBattleMenu = null;
    /// <summary>
    /// 玩家技能
    /// </summary>
    public Button btnPlayerSkill = null;

    protected override void init()
    {
        base.init();

        btnAttack.onClick.AddListener(() => { Debug.Log("出牌"); });
        btnBattleMenu.onClick.AddListener(() => { Debug.Log("戰鬥清單"); });
        btnPlayerSkill.onClick.AddListener(() => { Debug.Log("玩家技能"); });
    }

    protected override void open(object obj)
    {
        base.open(obj);
    }

    /// <summary>
    /// 顯示回合數
    /// </summary>
    private void showTurn()
    {
        if (labTurn == null)
            return;

        int num = 1;
        labTurn.text = num + "回合";
    }

    /// <summary>
    /// 顯示敵方數
    /// </summary>
    private void showEnemy()
    {
        if (labEnemy == null)
            return;
        int num = 3;
        labEnemy.text = "還剩" + num + "名";

    }

    /// <summary>
    /// 顯示關卡
    /// </summary>
    private void showBattle()
    {
        if (labBattle == null)
            return;

        int max = 3;
        int now = 1;
        labBattle.text = now + "/" + max;
    }

}

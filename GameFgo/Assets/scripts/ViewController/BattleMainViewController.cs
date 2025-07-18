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

    /// <summary>
    /// 當目標選擇發生改變時
    /// </summary>
    public System.Action<int> onChangedSelectEnemy = null;

    protected override void init()
    {
        base.init();

        btnAttack.onClick.AddListener(() => 
        { 
            Debug.Log("出牌");
            ViewController.GetViewController<BattleAttackViewController>().show();
        });
        btnBattleMenu.onClick.AddListener(() => { Debug.Log("戰鬥清單"); });
        btnPlayerSkill.onClick.AddListener(() => { Debug.Log("玩家技能"); });
        for (int i = 0; i < enemyUI.Length; i++)
        {
            int index = i;
            enemyUI[index].onChangedSelect = (bool isOn) =>
            {
                if (isOn)
                {
                    onChangedSelectEnemy?.Invoke(index);
                }
            };
        }
    }

    protected override void open(object obj)
    {
        base.open(obj);
    }

    /// <summary>
    /// 顯示回合數
    /// </summary>
    public void showTurn(int num)
    {
        if (labTurn == null)
            return;
        labTurn.text = num + "回合";
    }

    /// <summary>
    /// 顯示敵方數
    /// </summary>
    public void showEnemy(int num)
    {
        if (labEnemy == null)
            return;
        labEnemy.text = "還剩" + num + "名";

    }

    /// <summary>
    /// 顯示關卡
    /// </summary>
    public void showBattle(int max,int now)
    {
        if (labBattle == null)
            return;
        labBattle.text = now + "/" + max;
    }

    public void showPlayer(Master[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            var player = players[i];
            var ui = playerUI[i];
            ui.gameObject.SetActive(player != null);

            ui.ShowMaster(player);
        }
    }

    public void showEnemy(Master[] enemys)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            var enemy = enemys[i];
            var ui = enemyUI[i];
            ui.gameObject.SetActive(enemy != null);

            ui.ShowMaster(enemy);
        }
    }

    public void settingSelctTarget(int index)
    {
        enemyUI[index].select.isOn = true;
    }
}

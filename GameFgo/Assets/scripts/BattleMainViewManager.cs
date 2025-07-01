using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.Rendering.GPUSort;

public class BattleMainViewManager : MonoBehaviour
{
    BattleMain main = null;
    BattleMainViewController battleMainViewController = null;
    BattleAttackViewController battleAttackViewController = null;
    LockViewController lockViewController = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    /// <summary>
    /// 單位目標
    /// </summary>
    int selectEnemy = 0;

    void Start()
    {
        main = GetComponent<BattleMain>();

        battleMainViewController = ViewController.GetViewController<BattleMainViewController>();
        battleAttackViewController = ViewController.GetViewController<BattleAttackViewController>();
        lockViewController = ViewController.GetViewController<LockViewController>();
        battleMainViewController.show();
        battleAttackViewController.close();
        lockViewController.close();
        battleMainViewController.onChangedSelectEnemy = (int index) =>
        {
            selectEnemy = index;
        };

        main.OnChangedBattleState += (BattleMain.BattleState state) =>
        {
            lockViewController.show();
            switch (state)
            {
                case BattleMain.BattleState.INTO_ROUND:
                    int maxMissionsNum = main.GetMaxMissionsNum();
                    int nowMissionsNum = main.GetNowMissionsNum();
                    int nowRound = main.GetRoundNum();
                    ///取得我方上場單位
                    Master[] players = main.GetCurrentPlayerMaster();
                    //取得敵方上場單位
                    Master[] enemys = main.GetCurrentEnemyMaster();
                    
                    if (enemys[selectEnemy] == null)
                    {
                        for (int i = 0; i < enemys.Length; i++)
                        {
                            if (enemys[i] != null)
                            {
                                selectEnemy = i;
                                break;
                            }
                        }
                    }
                    //取得這回合的牌
                    Card[] cards = main.GetNowRoundCard();
                    battleMainViewController.settingSelctTarget(selectEnemy);
                    battleMainViewController.showTurn(nowRound);
                    battleMainViewController.showEnemy(enemys.Length);
                    battleMainViewController.showBattle(maxMissionsNum, nowMissionsNum);
                    battleMainViewController.showPlayer(players);
                    battleMainViewController.showEnemy(enemys);
                    battleAttackViewController.showCards(cards, players);
                    battleAttackViewController.onSelectFin = (Card[] cards) =>
                    {
                        Debug.Log("出牌~~~");
                        main.PlayerCards(cards, enemys[selectEnemy]);
                    };
                    break;

                case BattleMain.BattleState.PLAYER_ACTION_WAIT:
                    lockViewController.close();
                    break;

                case BattleMain.BattleState.PLAYER_ACTION_ANIMATION:
                    Debug.Log("播放玩家動畫");
                    isTiming = true;
                    actions = main.GetPlayerBattleAction();
                    break;

                case BattleMain.BattleState.ENEMY_ACTION_ANIMATION:
                    Debug.Log("撥放敵方動畫");
                    isTiming = true;
                    actions = main.GetEnemyBattleAction();
                    break;
            }
        };

        main.GameStart();
    }

    BattleAction[] actions = null;

    bool isTiming = false;
    float dtTime = 0;
    // Update is called once per frame
    void Update()
    {
        if (isTiming)
        {
            dtTime += Time.deltaTime;
            if (dtTime > 2)
            {
                isTiming = false;
                dtTime = 0;
                switch (main.state)
                {
                    case BattleMain.BattleState.PLAYER_ACTION_ANIMATION:
                        main.PlayerAnimationFin();
                        break;

                    case BattleMain.BattleState.ENEMY_ACTION_ANIMATION:
                        main.EnemyAnimationFin();
                        break;
                }
            }
        }
    }
}

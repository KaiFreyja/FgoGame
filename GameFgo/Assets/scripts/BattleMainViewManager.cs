using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleMainViewManager : MonoBehaviour
{
    BattleMain main = null;
    BattleMainViewController battleMainViewController = null;
    BattleAttackViewController battleAttackViewController = null;
    LockViewController lockViewController = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    BattleScenceViewController battleScenceViewController = null;
    
    /// <summary>
    /// 單位目標
    /// </summary>
    int selectEnemy = 0;


    Master[] players = new Master[3];
    //取得敵方上場單位
    Master[] enemys = new Master[3];

    void Start()
    {
        main = GetComponent<BattleMain>();
        battleScenceViewController = GetComponent<BattleScenceViewController>();

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
                case BattleMain.BattleState.INIT_BATTLE:
                    battleScenceViewController.LoadAllModelResource(main.GetAllPlayer());
                    break;

                case BattleMain.BattleState.INTO_ROUND:
                    int maxMissionsNum = main.GetMaxMissionsNum();
                    int nowMissionsNum = main.GetNowMissionsNum();
                    int nowRound = main.GetRoundNum();
                    ///取得我方上場單位
                    players = main.GetCurrentPlayerMaster();
                    players = new List<Master>(players).ToArray();
                    //取得敵方上場單位
                    enemys = main.GetCurrentEnemyMaster();
                    enemys = new List<Master>(enemys).ToArray();

                    battleScenceViewController.LoadPlayerTeam(players);
                    battleScenceViewController.LoadEnemyTeams(enemys);

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
                    actions = main.GetPlayerBattleAction();

                    if (actions.Length == 0)
                    {
                        main.PlayerAnimationFin();
                    }
                    else
                    {
                        battleScenceViewController.PlayerAni(actions, players, enemys, () =>
                        {
                            main.PlayerAnimationFin();
                        });
                    }
                    break;

                case BattleMain.BattleState.ENEMY_ACTION_ANIMATION:
                    Debug.Log("撥放敵方動畫");
                    actions = main.GetEnemyBattleAction();
                    if (actions.Length == 0)
                    {
                        main.EnemyAnimationFin();
                    }
                    else
                    {
                        battleScenceViewController.EnemyAni(actions, players, enemys, () =>
                        {
                            main.EnemyAnimationFin();
                        });
                    }
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

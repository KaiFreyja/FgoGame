using UnityEngine;
using System.Collections.Generic;
using System;

public class BattleScenceViewController : MonoBehaviour
{
    public GameObject[] playerNodes = null;
    public GameObject[] enemyNodes = null;
    public GameObject[] playerTeam = new GameObject[3];
    public GameObject[] enemysTeam = new GameObject[3];

    Dictionary<string, GameObject> modelResources = new Dictionary<string, GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadAllModelResource(Master[] master)
    {
        for (int i = 0; i < master.Length; i++)
        {
            string resource = master[i].resourceModel;
            GameObject g = (GameObject)Resources.Load("Model/" + resource);
            g.name = resource;
            modelResources.Add(resource, g);
        }
    }

    public void LoadPlayerTeam(Master[] master)
    {
        for (int i = 0; i < master.Length; i++)
        {
            var node = playerNodes[i];

            if (master[i] == null && playerTeam[i] != null)
            {
                GameObject.Destroy(playerTeam[i]);
                continue;
            }

            if (master[i] == null)
            {
                continue;
            }

            if (master[i] != null && playerTeam[i] != null && master[i].resourceModel != playerTeam[i].name)
            {
                GameObject.Destroy(playerTeam[i]);
            }

            if (playerTeam[i] != null && master[i] != null && playerTeam[i].name == master[i].resourceModel)
            {
                continue;
            }

            var model = GameObject.Instantiate(modelResources[master[i].resourceModel]);
            model.name = master[i].resourceModel;
            model.transform.SetParent(node.transform);
            model.transform.localPosition = Vector3.zero;
            model.transform.localEulerAngles = new Vector3(0, -120, 0);
            model.transform.localScale = 2 * Vector3.one;
            model.SetActive(true);
            model.GetComponent<PlayerController>().node = node;
            playerTeam[i] = model;
        }
    }

    public void LoadEnemyTeams(Master[] master)
    {
        for (int i = 0; i < master.Length; i++)
        {
            var node = enemyNodes[i];

            if (master[i] == null && enemysTeam[i] != null)
            {
                GameObject.Destroy(enemysTeam[i]);
                continue;
            }

            if (master[i] == null)
            {
                continue;
            }

            if (master[i] != null && enemysTeam[i] != null && master[i].resourceModel != enemysTeam[i].name)
            {
                GameObject.Destroy(enemysTeam[i]);
            }

            if (enemysTeam[i] != null && master[i] != null && enemysTeam[i].name == master[i].resourceModel)
            {
                GameObject.Destroy(enemysTeam[i]);
            }

            var model = (GameObject)GameObject.Instantiate(Resources.Load("Model/" + master[i].resourceModel));
            model.name = master[i].resourceModel;
            model.transform.SetParent(node.transform);
            model.transform.localPosition = Vector3.zero;
            model.transform.localEulerAngles = new Vector3(0, 90, 0);
            model.transform.localScale = 0.5f * Vector3.one;
            model.SetActive(true);
            model.GetComponent<NightmareController>().node = node;
            model.GetComponent<NightmareController>().Idle();
            enemysTeam[i] = model;
            
        }
    }

    int index = 0;
    public void PlayerAni(BattleAction[] battleActions, Master[] players, Master[] enemys,Action actionFin)
    {
        foreach (var a in enemys)
        {
            Debug.Log("enemys : " + a);
        }
        BattleAction battleAction = battleActions[index];
        Master player = battleAction.attacker;
        Master enemy = battleAction.target;
        int playerindex = 0;
        int enemyindex = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null)
            {
                continue;
            }
            if (player.id.Equals(players[i].id))
            {
                playerindex = i;
                break;
            }
        }
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i] == null)
            {
                continue;
            }
            if (enemy.id.Equals(enemys[i].id))
            {
                enemyindex = i;
                break;
            }
        }
        var controller = playerTeam[playerindex].GetComponent<PlayerController>();
        controller.Target = enemysTeam[enemyindex];

        Action action = () =>
        {
            index++;
            if (index < battleActions.Length)
            {
                PlayerAni(battleActions, players, enemys, actionFin);
            }
            else
            {
                index = 0;
                actionFin.Invoke();
            }
        };

        switch (battleAction.color)
        {
            case Card.CardColor.RED:
                controller.ATK_B(battleAction,action);
                break;

            case Card.CardColor.BULE:
                controller.ATK_A(battleAction,action);
                break;

            case Card.CardColor.GREEN:
                controller.ATK_Q(battleAction,action);
                break;
        }
    }

    public void EnemyAni(BattleAction[] battleActions, Master[] players, Master[] enemys, Action actionFin)
    {
        Debug.Log("EnemyAni : " + battleActions.Length);

        BattleAction battleAction = battleActions[index];
        Master player = battleAction.target;
        Master enemy = battleAction.attacker;

        int playerindex = 0;
        int enemyindex = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (player == players[i])
            {
                playerindex = i;
                break;
            }
        }
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemy == enemys[i])
            {
                enemyindex = i;
                break;
            }
        }

        var controller = enemysTeam[enemyindex].GetComponent<NightmareController>();
        controller.Target = playerTeam[playerindex];

        Action action = () =>
        {
            index++;
            if (index < battleActions.Length)
            {
                EnemyAni(battleActions, players, enemys, actionFin);
            }
            else
            {
                index = 0;
                actionFin.Invoke();
            }
        };

        switch (battleAction.color)
        {
            case Card.CardColor.RED:
                controller.ATK_B(battleAction, action);
                break;

            case Card.CardColor.BULE:
                controller.ATK_A(battleAction, action);
                break;

            case Card.CardColor.GREEN:
                controller.ATK_Q(battleAction, action);
                break;
        }
    }
}

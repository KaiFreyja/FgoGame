using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameObject Target;
    public GameObject node;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Start()
    {
 
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Idle()
    {
        //Debug.Log("Idle");
        animator.Play("Fight_Idle");
    }

    public void Die()
    {
       // Debug.Log("Die");
        animator.Play("Grab_Throw_B");
    }

    public void Hit()
    {
       // Debug.Log("Hit");
        animator.Play("Hit_F");
    }

    public void ATK_B(BattleAction battleAction,Action action)
    {
       // Debug.Log("ATK_B");
        animator.Play("Quickstep_F");
        // 假設目標在 X 軸 2f 處
        Vector3 targetPos = Target.transform.position;
        // 跑過去 0.5 秒，然後執行攻擊
        transform.DOMove(targetPos, 0.5f).OnComplete(() =>
        {
            animator.Play("Atk_K_1");

            if (battleAction.isTargetDead)
            {
                Target.GetComponent<NightmareController>().Die();
            }
            else
            {
                Target.GetComponent<NightmareController>().Hit();
            }

            DOVirtual.DelayedCall(0.5f, () =>
            {
                targetPos = node.transform.position;
                transform.DOMove(targetPos, 0.5f).OnComplete(() =>
                {
                    action.Invoke();
                });
            });
        });

    }

    public void ATK_A(BattleAction battleAction,Action action)
    {
       // Debug.Log("ATK_A");
        animator.Play("Quickstep_F");
        // 假設目標在 X 軸 2f 處
        Vector3 targetPos = Target.transform.position;
        // 跑過去 0.5 秒，然後執行攻擊
        transform.DOMove(targetPos, 0.5f).OnComplete(() =>
        {
            animator.Play("Atk_P_1");
            if (battleAction.isTargetDead)
            {
                Target.GetComponent<NightmareController>().Die();
            }
            else
            {
                Target.GetComponent<NightmareController>().Hit();
            }
            DOVirtual.DelayedCall(0.5f, () =>
            {
                targetPos = node.transform.position;
                transform.DOMove(targetPos, 0.5f).OnComplete(() =>
                {
                    action.Invoke();
                });
            });
        });
    }

    public void ATK_Q(BattleAction battleAction,Action action)
    {
       // Debug.Log("ATK_Q");
        animator.Play("Quickstep_F");
        // 假設目標在 X 軸 2f 處
        Vector3 targetPos = Target.transform.position;
        // 跑過去 0.5 秒，然後執行攻擊
        transform.DOMove(targetPos, 0.5f).OnComplete(() =>
        {
            animator.Play("Grab_Start");
            if (battleAction.isTargetDead)
            {
                Target.GetComponent<NightmareController>().Die();
            }
            else
            {
                Target.GetComponent<NightmareController>().Hit();
            }
            DOVirtual.DelayedCall(0.5f, () =>
            {
                targetPos = node.transform.position;
                transform.DOMove(targetPos, 0.5f).OnComplete(() =>
                {
                    action.Invoke();
                });
            });
        });
    }
}

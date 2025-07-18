using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NightmareController : MonoBehaviour
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
    //Dictionary<KeyCode, Action> aniState = new Dictionary<KeyCode, Action>();
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
        animator.Play("idle01");
    }

    public void Die()
    {
        //Debug.Log("Die");
        animator.Play("die");
    }

    public void Hit()
    {
        //Debug.Log("Hit");
        animator.Play("getHit");
    }

    public void ATK_B(BattleAction battleAction,Action action)
    {
        //Debug.Log("ATK_B");
        animator.Play("Run");
        // 假設目標在 X 軸 2f 處
        Vector3 targetPos = Target.transform.position;        
        // 跑過去 0.5 秒，然後執行攻擊
        transform.DOMove(targetPos, 0.5f).OnComplete(() =>
        {
            animator.Play("Horn Attack");
            if (battleAction.isTargetDead)
            {
                Target.GetComponent<PlayerController>().Die();
            }
            else
            {
                Target.GetComponent<PlayerController>().Hit();
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

    public void ATK_A(BattleAction battleAction, Action action)
    {
        //Debug.Log("ATK_A");
        animator.Play("Run");
        // 假設目標在 X 軸 2f 處
        Vector3 targetPos = Target.transform.position;
        // 跑過去 0.5 秒，然後執行攻擊
        transform.DOMove(targetPos, 0.5f).OnComplete(() =>
        {
            animator.Play("Claw Attack");
            if (battleAction.isTargetDead)
            {
                Target.GetComponent<PlayerController>().Die();
            }
            else
            {
                Target.GetComponent<PlayerController>().Hit();
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

    public void ATK_Q(BattleAction battleAction, Action action)
    {
        //Debug.Log("ATK_Q");
        animator.Play("Run");
        // 假設目標在 X 軸 2f 處
        Vector3 targetPos = Target.transform.position;
        // 跑過去 0.5 秒，然後執行攻擊
        transform.DOMove(targetPos, 0.5f).OnComplete(() =>
        {
            animator.Play("Basic Attack");
            if (battleAction.isTargetDead)
            {
                Target.GetComponent<PlayerController>().Die();
            }
            else
            {
                Target.GetComponent<PlayerController>().Hit();
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
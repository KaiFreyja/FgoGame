using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleAttackCard : MonoBehaviour
{
    /// <summary>
    /// ����
    /// </summary>
    public Image imgHead = null;

    /// <summary>
    /// �I��
    /// </summary>
    public Toggle select = null;

    /// <summary>
    /// �I���I�Ɏ�
    /// </summary>
    public Text selectIndex = null;

    public void showCard(Card card, Master master)
    {
        imgHead.sprite = Resources.Load<Sprite>(Config.RESOURCE_HEAD_PATH + master.resourceHead);
    }

    public Action<bool> onChangedSelect = null;

    private void Start()
    {
        select.onValueChanged.AddListener((bool isOn) => { onChangedSelect?.Invoke(isOn); });
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="num"></param>
    public void SetSortNum(int num)
    {
        selectIndex.text = num.ToString();
    }
}

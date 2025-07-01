using UnityEngine;
using UnityEngine.UI;

public class BattleMainEnemyUI : MonoBehaviour
{
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
    /// bar
    /// </summary>
    public UguiBar barHp = null;

    /// <summary>
    /// ñ⁄ïWçΩíË
    /// </summary>
    public Toggle select = null;

    /// <summary>
    /// Ë˚é¶ödà éëêu
    /// </summary>
    /// <param name="enemy"></param>
    public void ShowMaster(Master enemy)
    {
        if (enemy == null)
        {
            return;
        }

        imgIcon.sprite = Resources.Load<Sprite>(Config.RESOURCE_PROFESSION_PATH + enemy.professionId);
        labName.text = enemy.name;
        labHp.text = enemy.hp.ToString();
        barHp.SetValue(enemy.hp, enemy.maxHp);
    }

    public System.Action<bool> onChangedSelect = null;

    private void Start()
    {
        select.onValueChanged.AddListener((bool isOn) => { onChangedSelect?.Invoke(isOn); });
    }
}

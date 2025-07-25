using UnityEngine;
using UnityEngine.UI;

public class UguiBar : MonoBehaviour
{
    public Image background = null; 
    public Image bar = null;

    public float value
    {
        set
        {
            _value = value;
            _value = Mathf.Clamp(_value, 0f, 1f);
            updateBar(_value);
        }
        get
        {
            return _value;
        }
    }

    private float _value = 0;

    /// <summary>
    /// 更新畫面
    /// </summary>
    /// <param name="value"></param>
    private void updateBar(float value)
    {
        if (bar == null)
        {
            return;
        }
        bar.fillAmount = value;
    }

    /// <summary>
    /// 自動計算
    /// </summary>
    /// <param name="current"></param>
    /// <param name="max"></param>
    public void SetValue(int current, int max)
    {
        value = (float)current / Mathf.Max(1, max);
    }
}

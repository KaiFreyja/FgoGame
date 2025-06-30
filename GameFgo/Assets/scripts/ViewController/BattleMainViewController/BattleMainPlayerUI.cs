using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class BattleMainPlayerUI : MonoBehaviour
{
    /// <summary>
    /// “ª‘œ
    /// </summary>
    public Image imgHead = null;
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
    /// hp bar
    /// </summary>
    public UguiBar barHp = null;
    /// <summary>
    /// np 
    /// </summary>
    public Text labNp = null;
    /// <summary>
    /// np bar
    /// </summary>
    public UguiBar barNp = null;
    /// <summary>
    /// ‹Z”\ˆÂçä
    /// </summary>
    public Button[] btnSkills = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < btnSkills.Length; i++)
        {
            int index = i;
            btnSkills[i].onClick.AddListener(() =>
            {
                Debug.Log("skill : " + index);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

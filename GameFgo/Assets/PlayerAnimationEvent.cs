using UnityEngine;

public class AnimationEventPlayer : MonoBehaviour
{
    public GameObject effectPrefab = null;
    public GameObject pointLeftHead = null;
    public GameObject pointRightHead = null;
    public GameObject pointLeftFoot = null;
    public GameObject pointRightFoot = null;

    public void AtkB()
    {
        Debug.Log("ATK_B");

        playAni(pointRightFoot);
    }

    public void AtkA()
    {
        Debug.Log("ATK_A");

        playAni(pointLeftHead);
    }

    public void AtkQ()
    {
        Debug.Log("ATK_Q");

        playAni(pointLeftHead);
    }

    private void playAni(GameObject point)
    {
        if (effectPrefab == null)
            return;

        if (point == null)
            return;

        if (point.transform.childCount > 0)
        {
            point.transform.GetChild(0).gameObject.SetActive(false);
            point.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            var g = GameObject.Instantiate(effectPrefab);
            g.transform.SetParent(point.transform);
            g.transform.localPosition = Vector3.zero;
            g.transform.localRotation = Quaternion.identity;
            g.transform.localScale = 0.1f * Vector3.one;
        }
    }
}

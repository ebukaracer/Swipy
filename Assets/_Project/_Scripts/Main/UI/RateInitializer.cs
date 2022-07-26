using UnityEngine;
using UnityEngine.UI;

class RateInitializer : MonoBehaviour
{
    readonly Image[] images = new Image[3];

    [SerializeField]
    Sprite image;


    private void Awake()
    {
        var count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            images[i] = transform.GetChild(i).GetComponent<Image>();
        }

    }

    public void SetRate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            images[i].sprite = image;
        }
    }
}

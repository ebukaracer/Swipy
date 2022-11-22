using UnityEngine;
using UnityEngine.UI;

internal class RateInitializer : MonoBehaviour
{
    private readonly Image[] _images = new Image[3];

    [SerializeField] private Sprite image;


    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _images[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    public void SetRate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _images[i].sprite = image;
        }
    }
}

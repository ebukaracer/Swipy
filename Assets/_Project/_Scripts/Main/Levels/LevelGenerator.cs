using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    GridLayoutGroup gridLayout;

    LevelController levelController;

    [Space(10)]

    [Header("Spawn Properties")]

    [SerializeField]
    Transform levelButttonPrefab;

    [SerializeField]
    int spawnAmount;

    private void Start()
    {
        levelController = GetComponent<LevelController>();

        Destroy(GetComponent<GridLayoutGroup>(), 1f);

        var childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
            levelController.InitializeLevelButtons(i);
    }


    void AddGridLayout()
    {
        gridLayout = gameObject.AddComponent<GridLayoutGroup>();

        gridLayout.padding.top = 80;

        gridLayout.cellSize = new Vector2(80, 80);
        gridLayout.spacing = new Vector2(30, 70);

        gridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
        gridLayout.childAlignment = TextAnchor.UpperCenter;
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = 3;
    }

    public void SpawnLevels()
    {
        AddGridLayout();

        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(levelButttonPrefab, transform);
        }
    }

    public void ClearLevels()
    {
        var childCount = transform.childCount;

        gridLayout = GetComponent<GridLayoutGroup>();

        for (int i = childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);

        if (gridLayout != null)
            DestroyImmediate(gridLayout);
    }
}
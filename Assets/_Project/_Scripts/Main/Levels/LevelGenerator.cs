using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-4)]
internal class LevelGenerator : MonoBehaviour
{
    private GridLayoutGroup _gridLayout;
    private LevelController _levelController;

    [Space(10), Header("SPAWN PROPERTIES")]
    [SerializeField] private Transform levelBtnPrefab;
    [SerializeField] private int spawnAmount;


    private void Awake()
    {
        _levelController = GetComponent<LevelController>();

        for (int i = 0; i < transform.childCount; i++)
            _levelController.InitializeLevelButtons(i);
    }

    private void AddGridLayout()
    {
        if (TryGetComponent(out _gridLayout))
            return;

        _gridLayout = gameObject.AddComponent<GridLayoutGroup>();

        _gridLayout.padding.top = 80;

        _gridLayout.cellSize = new Vector2(80, 80);
        _gridLayout.spacing = new Vector2(30, 70);

        _gridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
        _gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
        _gridLayout.childAlignment = TextAnchor.UpperCenter;
        _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayout.constraintCount = 3;
    }

    #region Editor prototype
    public void SpawnLevels()
    {
        AddGridLayout();

        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(levelBtnPrefab, transform);
        }
    }

    public void ClearLevels()
    {
        var childCount = transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);

        if (TryGetComponent(out _gridLayout))
            DestroyImmediate(_gridLayout);
    }
    #endregion
}
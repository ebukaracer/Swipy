using Racer.SaveSystem;
using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class DragController : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    ParticleSystem spawnFx;
    Camera mainCam;

    SwipeInterpolation interpolation;
    TimeController timeCounter;

    public event Action OnDestroyed;
    public event Action<Directions> OnMovedToDirection;

    IEnumerator coroutineMove;

    bool isX;
    bool isY;

    bool isDraggingX;
    bool isDraggingY;

    bool hasDestroyed;

    [HideInInspector]
    public bool isSwiping;

    bool hasCheckedDir;

    //X,Y screen axis
    Vector3 offset;

    [SerializeField]
    float deactivateOffset = 7f;

    [SerializeField]
    float destroyOffset = 25f;

    [SerializeField]
    float sqrDistance = .25f;

    Vector3 initialPosition;
    Vector3 finalPos;

    Vector2 mouseAxes;
    Vector2 screenPos;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        rb = GetComponent<Rigidbody2D>();

        spawnFx = GetComponentInChildren<ParticleSystem>();

        interpolation = GetComponent<SwipeInterpolation>();

        VfxInit();

        mainCam = Camera.main;

        screenPos = mainCam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        initialPosition = transform.position;

        sqrDistance *= sqrDistance;
    }


    private void Start()
    {
        timeCounter = TimeController.Instance;
    }

    private void VfxInit()
    {
        var fxMain = spawnFx.main;

        if (SaveManager.GetInt("Particles") == 1)
        {
            fxMain.playOnAwake = true;

            if (!spawnFx.isPlaying)
            {
                spawnFx.Play();
            }
        }
        else
        {
            fxMain.playOnAwake = false;
        }
    }


    private void OnMouseDrag()
    {
        if (isSwiping)
            return;

        finalPos = GetScreenToWorld() - offset;

        ConstraintAxes(finalPos);
    }

    // Read on
    private void OnMouseDown() => offset = GetScreenToWorld() - transform.position;

    private void OnMouseUp()
    {
        mouseAxes.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));


        if (mouseAxes.sqrMagnitude > sqrDistance && !isSwiping)
        {
            rb.AddForce((transform.position * mouseAxes.magnitude) * 25f, ForceMode2D.Impulse);

            if (rb.velocity.normalized.sqrMagnitude <= 0)
                return;

            isSwiping = true;

            timeCounter.StartTimer();
        }
        else
        {
            if (!isSwiping)
                ReturnToInitialPos();
        }


        // TODO:
        //Kinda not needed
        //if (rb.velocity.magnitude > 1f)
        //{
        //    rb.AddForce(transform.position * 10f, ForceMode2D.Impulse);

        //    Logging.Log("Added");
        //}
    }



    Vector3 GetScreenToWorld()
    {
        return mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private Vector2 LimitPosition() => new Vector2(screenPos.x - boxCollider.size.x / 2, screenPos.y - boxCollider.size.y / 2);


    /// <summary>
    /// Caution: Spaghetti code below!
    /// </summary>
    private void ConstraintAxes(Vector3 finalPos)
    {
        if (finalPos.x > (initialPosition.x + .1f) && !isY && !isDraggingY)
        {
            isDraggingX = true;
            isX = true;

            finalPos.y = initialPosition.y;
            transform.position = finalPos;
        }

        else if (finalPos.x < (initialPosition.x - .1f) && !isY && !isDraggingY)
        {
            isDraggingX = true;
            isX = true;

            finalPos.y = initialPosition.y;
            transform.position = finalPos;
        }
        else
            isX = false;


        if (finalPos.y > (initialPosition.y + .1f) && !isX && !isDraggingX)
        {
            isDraggingY = true;
            isY = true;

            finalPos.x = initialPosition.x;
            transform.position = finalPos;
        }

        else if (finalPos.y < (initialPosition.y - .1f) && !isX && !isDraggingX)
        {
            isDraggingY = true;
            isY = true;

            finalPos.x = initialPosition.x;
            transform.position = finalPos;
        }
        else
            isY = false;
    }



    /// <summary>
    /// This prevents object snapping immediately the mouse is released
    /// </summary>
    private void ReturnToInitialPos()
    {
        isDraggingX = false;
        isDraggingY = false;

        isX = false;
        isY = false;

        if (coroutineMove != null)
            StopCoroutine(coroutineMove);

        coroutineMove = interpolation.MoveToOriginalPosition(initialPos: transform.position, finalPos: initialPosition);

        StartCoroutine(coroutineMove);

    }

    void DeactivateSlide()
    {
        OnDestroyed?.Invoke();

        isSwiping = false;

        timeCounter.EndTimer();

        hasDestroyed = true;


    }

    void DestroySlide()
    {
        if (!hasDestroyed)
            return;

        if (Mathf.Abs(transform.position.x) > destroyOffset ||
            Mathf.Abs(transform.position.y) >= destroyOffset)
        {
            //Destroy(gameObject);
            ObjectPool.Instance.AddObject(gameObject);
        }
    }

    private void Update()
    {
        DestroySlide();

        if (!isSwiping)
            return;

        if (!hasCheckedDir)
        {
            OnMovedToDirection?.Invoke(CheckSlideDirection(transform.position));

            hasCheckedDir = true;
        }

        if (Math.Abs(transform.position.x) >= LimitPosition().x + deactivateOffset ||
            Math.Abs(transform.position.y) >= LimitPosition().y + deactivateOffset &&
            !hasDestroyed)
        {

            DeactivateSlide();
        }
    }

    Directions CheckSlideDirection(Vector2 slideDir)
    {
        if (slideDir.x > 0)
            return Directions.RIGHT;
        else if (slideDir.x < 0)
            return Directions.LEFT;
        else if (slideDir.y > 0)
            return Directions.UP;
        else if (slideDir.y < 0)
            return Directions.DOWN;

        return 0;
    }

    private void OnDisable()
    {
        ResetToDefault();
    }

    private void ResetToDefault()
    {
        initialPosition = default;

        finalPos = default;

        mouseAxes = default;

        screenPos = default;

        offset = default;

        isX = default;

        isY = default;

        hasDestroyed = default;

        hasCheckedDir = default;

        isDraggingX = default;

        isDraggingY = default;

        isSwiping = default;

        OnMovedToDirection = default;

        OnDestroyed = default;
    }
}
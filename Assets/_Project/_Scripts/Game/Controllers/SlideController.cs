using Racer.SaveSystem;
using System;
using System.Collections;
using Racer.Utilities;
using UnityEngine;

[SelectionBase]
internal class SlideController : MonoBehaviour
{
    private IEnumerator _coroutineMove;

    private Camera _mainCam;
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;
    private ParticleSystem _spawnFx;

    private SlideTween _slideTween;
    private TimeController _timeCounter;

    private bool _isVfx;
    private bool _isX;
    private bool _isY;
    private bool _isDraggingX;
    private bool _isDraggingY;
    private bool _hasDestroyed;
    private bool _hasCheckedDir;

    private Vector3 _initialPosition;
    private Vector3 _finalPos;
    private Vector2 _mouseAxes;
    private Vector2 _screenPos;
    private Vector3 _offset;

    private bool _isSwiping;

    public event Action<Direction> OnMovedToDirection;
    public event Action OnDestroyed;

    [SerializeField] private float deactivateOffset = 7f;
    [SerializeField] private float destroyOffset = 25f;
    [SerializeField] private float sqrDistance = .25f;


    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _spawnFx = GetComponentInChildren<ParticleSystem>();
        _slideTween = GetComponent<SlideTween>();
        _mainCam = Utility.CameraMain;

        _screenPos = _mainCam.ScreenToWorldPoint(new Vector2
            (Screen.width, Screen.height));

        InitVfx();
    }

    private void OnEnable()
    {
        _initialPosition = transform.position;
        sqrDistance *= sqrDistance;

        PlayVfx();
    }

    private void Start()
    {
        _timeCounter = TimeController.Instance;
    }

    private void Update()
    {
        DestroySlide();

        if (!_isSwiping)
            return;

        if (!_hasCheckedDir)
        {
            OnMovedToDirection?.Invoke(CheckSlideDirection(transform.position));
            _hasCheckedDir = true;
        }

        if (Math.Abs(transform.position.x) >= LimitPosition().x + deactivateOffset ||
            Math.Abs(transform.position.y) >= LimitPosition().y + deactivateOffset &&
            !_hasDestroyed)
        {
            DeactivateSlide();
        }
    }

    private void OnMouseDrag()
    {
        if (_isSwiping)
            return;

        _finalPos = GetScreenToWorld() - _offset;

        ConstrainAxes(_finalPos);
    }

    private void OnMouseDown()
    {
        _offset = GetScreenToWorld() - transform.position;
    }

    private void OnMouseUp()
    {
        _mouseAxes.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (_mouseAxes.sqrMagnitude > sqrDistance && !_isSwiping)
        {
            _rb.AddForce((transform.position * _mouseAxes.magnitude) * 25f, ForceMode2D.Impulse);

            if (_rb.velocity.normalized.sqrMagnitude <= 0)
                return;

            _isSwiping = true;

            _timeCounter.StartTimer();
        }
        else
        {
            if (!_isSwiping)
                ReturnToInitialPos();
        }
    }

    /// <summary>
    ///  Constrains to an axis the player is dragging towards.
    /// </summary>
    private void ConstrainAxes(Vector3 finalPos)
    {
        if (finalPos.x > (_initialPosition.x + .1f) && !_isY && !_isDraggingY)
        {
            _isDraggingX = true;
            _isX = true;

            finalPos.y = _initialPosition.y;
            transform.position = finalPos;
        }

        else if (finalPos.x < (_initialPosition.x - .1f) && !_isY && !_isDraggingY)
        {
            _isDraggingX = true;
            _isX = true;

            finalPos.y = _initialPosition.y;
            transform.position = finalPos;
        }
        else
            _isX = false;


        if (finalPos.y > (_initialPosition.y + .1f) && !_isX && !_isDraggingX)
        {
            _isDraggingY = true;
            _isY = true;

            finalPos.x = _initialPosition.x;
            transform.position = finalPos;
        }

        else if (finalPos.y < (_initialPosition.y - .1f) && !_isX && !_isDraggingX)
        {
            _isDraggingY = true;
            _isY = true;

            finalPos.x = _initialPosition.x;
            transform.position = finalPos;
        }
        else
            _isY = false;
    }

    /// <summary>
    /// Prevents object from snapping, immediately the mouse is released.
    /// </summary>
    private void ReturnToInitialPos()
    {
        _isDraggingX = false;
        _isDraggingY = false;

        _isX = false;
        _isY = false;

        if (_coroutineMove != null)
            StopCoroutine(_coroutineMove);

        _coroutineMove = _slideTween.MovePosition(initialPos: transform.position, finalPos: _initialPosition);

        StartCoroutine(_coroutineMove);
    }

    private Vector3 GetScreenToWorld()
    {
        return _mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private Vector2 LimitPosition()
    {
        Vector2 size;
        return new Vector2(_screenPos.x - (size = _boxCollider.size).x / 2,
            _screenPos.y - size.y / 2);
    }

    private void DeactivateSlide()
    {
        OnDestroyed?.Invoke();

        _isSwiping = false;

        _timeCounter.EndTimer();

        _hasDestroyed = true;
    }

    private void DestroySlide()
    {
        if (!_hasDestroyed)
            return;

        if (Mathf.Abs(transform.position.x) > destroyOffset ||
            Mathf.Abs(transform.position.y) >= destroyOffset)
        {
            ObjectPool.Instance.AddObject(gameObject);
        }
    }

    private static Direction CheckSlideDirection(Vector2 slideDir)
    {
        if (slideDir.x > 0)
            return Direction.Right;
        if (slideDir.x < 0)
            return Direction.Left;
        if (slideDir.y > 0)
            return Direction.Up;
        return slideDir.y < 0 ? Direction.Down : 0;
    }

    private void InitVfx()
    {
        var fxMain = _spawnFx.main;

        _isVfx = fxMain.playOnAwake = SaveSystem.GetData<int>("Vfx") == 0;
    }

    public void PlayVfx()
    {
        if (!_spawnFx.isPlaying && _isVfx)
            _spawnFx.Play();
    }

    private void OnDisable()
    {
        ResetToDefault();
    }

    private void ResetToDefault()
    {
        _initialPosition = default;
        _finalPos = default;
        _mouseAxes = default;
        _screenPos = default;
        _offset = default;
        _isX = default;
        _isY = default;
        _hasDestroyed = default;
        _hasCheckedDir = default;
        _isDraggingX = default;
        _isDraggingY = default;
        _isSwiping = default;
        OnMovedToDirection = default;
        OnDestroyed = default;
    }
}
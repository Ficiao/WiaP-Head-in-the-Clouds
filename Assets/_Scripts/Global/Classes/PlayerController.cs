using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    [Header("GROUND CHECK")]
    [SerializeField] private float _groundCastLength;
    [SerializeField] private float _groundCastSize;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Animator _playerAnimator;
    private Vector3 _lastPosition;
    private float _currentHorizontalSpeed, _currentVerticalSpeed;
    private bool _isGrounded = false;
    private bool _canJump = true;
    private Collider2D _ownedCollider;
    private Platformer.MovingPlatform _draggingPlatform = null;

    public Vector3 Velocity { get; private set; }
    public FrameInput CurrentInput { get; private set; }
    public bool JumpingThisFrame { get; private set; }
    public bool LandingThisFrame { get; private set; }
    public Vector3 RawMovement { get; private set; }

    private bool _active;
    void Awake()
    {
        Invoke(nameof(Activate), 0.5f);

        _ownedCollider = GetComponent<BoxCollider2D>();
    }
    public void Activate() =>  _active = true;
    public void Deactivate() => _active = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.gameObject.layer)
        {
            case 6:
                break;
            case 7:
                break;
            case 8:
                _currentVerticalSpeed = 0f;
                _isGrounded = false;
                break;
            case 9:

                break;

            default:
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.collider.gameObject.layer)
        {
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;

            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
#if (UNITY_EDITOR)
        if (EditorApplication.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_ownedCollider.bounds.center, -1 * transform.up * _groundCastLength);
            Gizmos.DrawWireCube(_ownedCollider.bounds.center + (-1 * transform.up * _groundCastLength), _ownedCollider.bounds.size * _groundCastSize);
        }
#endif
    }

    private void Update()
    {
        GatherInput();
    }

    private void FixedUpdate() {
        RaycastHit2D[] hits;
        _isGrounded = false;
        hits = Physics2D.BoxCastAll(_ownedCollider.bounds.center, _ownedCollider.bounds.size * _groundCastSize, 0, Vector2.down, _groundCastLength, _layerMask);
        _isGrounded = false;
        _canJump = false;
        foreach(RaycastHit2D hit in hits)
        {
            _isGrounded = true;
            _canJump = true;
            Platformer.MovingPlatform movingPlatform= hit.transform.GetComponent<Platformer.MovingPlatform>();
            if (movingPlatform == null)
            {
                _draggingPlatform?.FreePlayer();
                _draggingPlatform = null;
            }
            else if (movingPlatform != _draggingPlatform)
            {
                _draggingPlatform?.FreePlayer();
                _draggingPlatform = movingPlatform;
                _draggingPlatform.DragPlayer(transform);
            }
            break;
        }
        //if (hits.Length > 0)
        //{
        //    _isGrounded = true;
        //    _canJump = true;
        //}
        //else
        //{
        //    _isGrounded = false;
        //    _canJump = false;
        //}

        if (transform.localPosition.y < -100) transform.localPosition = new Vector3(0, 0, 0);

        Velocity = (transform.position - _lastPosition) / Time.fixedDeltaTime;
        _lastPosition = transform.position;

        if (!_active)
        {
            CurrentInput = new FrameInput
            {
                JumpDown = false,
                JumpUp = false,
                X = 0f
            };
            CalculateGravity();
            CalculateWalk();
            MoveCharacter();
            _playerAnimator.SetBool("Running", false);
            return;
        }

        CalculateWalk(); 
        CalculateJumpApex();
        CalculateGravity(); 
        CalculateJump();

        MoveCharacter(); 

        if(CurrentInput.X != 0) _playerAnimator.SetBool("Running", true);
        else _playerAnimator.SetBool("Running", false);
    }

    #region Gather Input

    private void GatherInput() {
        CurrentInput = new FrameInput {
            JumpDown = Input.GetKeyDown(KeyCode.Space) && _canJump,
            JumpUp = Input.GetKeyUp(KeyCode.Space),
            X = (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0)
        };
        if (CurrentInput.JumpDown) {
            _lastJumpPressed = Time.time;
            _canJump = false;
        }
    }

    #endregion

    #region Walk

    [Header("WALKING")] [SerializeField] private float _acceleration = 90;
    [SerializeField] private float _moveClamp = 13;
    [SerializeField] private float _deAcceleration = 60f;
    [SerializeField] private float _apexBonus = 2;

    private void CalculateWalk() {
        if (CurrentInput.X != 0) {
            _currentHorizontalSpeed += CurrentInput.X * _acceleration * Time.fixedDeltaTime;

            _currentHorizontalSpeed = Mathf.Clamp(_currentHorizontalSpeed, -_moveClamp, _moveClamp);

            var apexBonus = Mathf.Sign(CurrentInput.X) * _apexBonus * _apexPoint;
            _currentHorizontalSpeed += apexBonus * Time.fixedDeltaTime;
        }
        else {
            _currentHorizontalSpeed = Mathf.MoveTowards(_currentHorizontalSpeed, 0, _deAcceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Gravity

    [Header("GRAVITY")] [SerializeField] private float _fallClamp = -40f;
    [SerializeField] private float _minFallSpeed = 80f;
    [SerializeField] private float _maxFallSpeed = 120f;
    private float _fallSpeed;

    private void CalculateGravity() {
        if (_isGrounded)
        {
            if (_currentVerticalSpeed < 0) _currentVerticalSpeed = 0;
        }
        else
        {
            var fallSpeed = _endedJumpEarly && _currentVerticalSpeed > 0 ? _fallSpeed * _jumpEndEarlyGravityModifier : _fallSpeed;

            _currentVerticalSpeed -= fallSpeed * Time.fixedDeltaTime;

            if (_currentVerticalSpeed < _fallClamp) _currentVerticalSpeed = _fallClamp;
        }   
    }

    #endregion

    #region Jump

    [Header("JUMPING")] [SerializeField] private float _jumpHeight = 30;
    [SerializeField] private float _jumpApexThreshold = 10f;
    [SerializeField] private float _coyoteTimeThreshold = 0.1f;
    [SerializeField] private float _jumpBuffer = 0.1f;
    [SerializeField] private float _jumpEndEarlyGravityModifier = 3;
    private bool _coyoteUsable;
    private bool _endedJumpEarly = true;
    private float _apexPoint; 
    private float _lastJumpPressed;
    private bool CanUseCoyote => _coyoteTimeThreshold > Time.time;
    private bool HasBufferedJump => _lastJumpPressed + _jumpBuffer > Time.time;

    private void CalculateJumpApex() {

            _apexPoint = Mathf.InverseLerp(_jumpApexThreshold, 0, Mathf.Abs(Velocity.y));
            _fallSpeed = Mathf.Lerp(_minFallSpeed, _maxFallSpeed, _apexPoint);
    }

    private void CalculateJump() {
        if (CurrentInput.JumpDown && CanUseCoyote || HasBufferedJump) {
            _currentVerticalSpeed = _jumpHeight;
            _endedJumpEarly = false;
            _coyoteUsable = false;
            JumpingThisFrame = true;
        }
        else {
            JumpingThisFrame = false;
        }

        if (CurrentInput.JumpUp && !_endedJumpEarly && Velocity.y > 0) {
            _endedJumpEarly = true;
        }

    }

    #endregion

    #region Move

    [Header("MOVE")] [SerializeField, Tooltip("Raising this value increases collision accuracy at the cost of performance.")]
    private int _freeColliderIterations = 10;
    private List<Collider2D> _disabledColliders = new List<Collider2D>();

    private void MoveCharacter() {
        var pos = transform.position;
        RawMovement = new Vector3(_currentHorizontalSpeed, _currentVerticalSpeed); 
        var move = RawMovement * Time.fixedDeltaTime;
        var furthestPoint = pos + move;

        transform.position = Vector3.MoveTowards(transform.position, furthestPoint, Vector3.Distance(transform.position, furthestPoint) + 1);
    }

    #endregion
}

public struct FrameInput
{
    public float X, Y;
    public bool JumpDown;
    public bool JumpUp;
}

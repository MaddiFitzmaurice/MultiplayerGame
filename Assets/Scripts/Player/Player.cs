using UnityEngine;

public class Player : MonoBehaviour
{
    #region EXTERNAL DATA
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _speedCurve;
    #endregion

    #region INTERNAL DATA
    // Components
    private Rigidbody2D _rb;
    // Movement
    private Vector2 _moveDir;
    private float _inputTimer = 0f;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EventManager.EventSubscribe(EventType.PLAYER_MOVE, H_Movement);
    }

    private void OnDisable()
    {
        EventManager.EventUnsubscribe(EventType.PLAYER_MOVE, H_Movement);
    }

    private void Update()
    {
        InputTimer();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void InputTimer()
    {
        if (_moveDir != Vector2.zero)
        {
            _inputTimer += Time.deltaTime;
        }
        else
        {
            _inputTimer = 0f;
        }
    }

    private void Movement()
    {   
        _rb.linearVelocity = _speed * _speedCurve.Evaluate(_inputTimer) * _moveDir;
    }

    #region EVENT HANDLERS
    private void H_Movement(object data)
    {
        if (data is Vector2 moveDir)
        {
            _moveDir = moveDir;
        }
        else
        {
            Debug.LogError("Error: Did not receive Vector2");
        }
    }
    #endregion

}

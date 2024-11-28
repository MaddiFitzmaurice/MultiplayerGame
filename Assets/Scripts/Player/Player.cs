using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    #region EXTERNAL DATA
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _speedCurve;
    #endregion

    #region INTERNAL DATA
    // Components
    private Rigidbody2D _rb;
    private Camera _cam;
    // Movement
    private Vector2 _moveDir;
    private Vector3 _rawMousePos;
    private float _inputTimer = 0f;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
    }

    public override void OnNetworkSpawn()
    {
        transform.position = new Vector2(Random.Range(-7, 8), Random.Range(-4, 5));
    }

    private void OnEnable()
    {
        EventManager.EventSubscribe(EventType.PLAYER_MOVE, H_Movement);
        EventManager.EventSubscribe(EventType.PLAYER_LOOK, H_Look);

    }

    private void OnDisable()
    {
        EventManager.EventUnsubscribe(EventType.PLAYER_MOVE, H_Movement);
        EventManager.EventUnsubscribe(EventType.PLAYER_LOOK, H_Look);
    }

    private void Update()
    {
        if (!IsOwner) { return; }

        InputTimer();
        Look();
    }

    private void FixedUpdate()
    {
        if (!IsOwner) { return; }

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

    private void Look()
    {
        _rawMousePos.z = _cam.nearClipPlane;

        Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(_rawMousePos);

        Vector2 dir = mouseWorldPos - transform.position;

        transform.up = dir.normalized;
        
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

    private void H_Look(object data)
    {
        if (data is Vector2 rawMousePos)
        {
            _rawMousePos = rawMousePos;
        }
        else
        {
            Debug.LogError("Error: Did not receive Vector2");
        }
    }
    #endregion

}

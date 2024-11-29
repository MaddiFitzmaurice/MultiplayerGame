using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, InputActions.IGameplayActions
{
    private InputActions _inputs;

    private void Awake()
    {
        _inputs = new InputActions();
        _inputs.Gameplay.SetCallbacks(this);

        EventManager.EventInitialise(EventType.PLAYER_MOVE);
    }

    private void OnEnable()
    {
        _inputs.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _inputs.Gameplay.Disable(); 
    }

    private void Start()
    {
        //Cursor.visible = false;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        EventManager.EventTrigger(EventType.PLAYER_MOVE, context.ReadValue<Vector2>());
    }

    public void OnSlap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("SLAP!");
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        EventManager.EventTrigger(EventType.PLAYER_LOOK, context.ReadValue<Vector2>());
    }
}

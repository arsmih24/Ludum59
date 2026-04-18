using UnityEngine;
using UnityEngine.InputSystem;
using PlayerSystem;

public class InputListener : MonoBehaviour
{
    private MainInputActions _mainInputActions;
    private PlayerInvoker _invoker;
    private Vector2 _inputValue;

    public void Construct(PlayerInvoker invoker)
    {
        _invoker = invoker;
    }

    private void Awake()
    {
        _mainInputActions = new MainInputActions();
        Bind();
        _mainInputActions.Enable();
    }

    private void FixedUpdate()
    {
        _invoker.InvokeMove(_inputValue);
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        _inputValue = context.ReadValue<Vector2>();
    }

    private void OnStop(InputAction.CallbackContext context) 
    {
        _invoker.InvokeStop();
        _inputValue = Vector2.zero;
    }

    private void Bind()
    {
        _mainInputActions.Game.Move.performed += OnMovement;
        _mainInputActions.Game.Stop.performed += OnStop;
    }

    private void Expose()
    {
        _mainInputActions.Game.Move.performed -= OnMovement;
        _mainInputActions.Game.Stop.performed -= OnStop;
    }

    private void OnDestroy()
    {
        _mainInputActions.Disable();
        Expose();
    }
}
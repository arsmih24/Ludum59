using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [Space]
    [SerializeField] private AudioClip mouseClickClip;
    [SerializeField, Range(0f, 1f)] float mouseClickVolume;
    [Space]
    [SerializeField] private AudioClip pauseClip;
    [SerializeField, Range(0f, 1f)] float pauseVolume;

    private MainInputActions _mainInputActions;
    private Invoker _invoker;
    private Vector2 _inputValue;

    public void Construct(Invoker invoker)
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
        _invoker.InvokeUpdateDirectionView();
        _invoker.InvokeUpdateCoordinatesView();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        _inputValue = context.ReadValue<Vector2>();
    }

    private void OnStop(InputAction.CallbackContext context) 
    {
        _invoker.InvokeStopMovement();
        _inputValue = Vector2.zero;
    }

    private void OnPause(InputAction.CallbackContext context) 
    {
        audioSource.volume = pauseVolume;
        audioSource.PlayOneShot(pauseClip);

        _invoker.InvokeSetPause();
    }

    private void OnClick(InputAction.CallbackContext context) 
    {
        audioSource.volume = mouseClickVolume;
        audioSource.PlayOneShot(mouseClickClip);
    }

    private void Bind()
    {
        _mainInputActions.Game.Move.performed += OnMovement;
        _mainInputActions.Game.Stop.performed += OnStop;
        _mainInputActions.Game.Pause.performed += OnPause;
        _mainInputActions.Game.Click.performed += OnClick;
    }

    private void Expose()
    {
        _mainInputActions.Game.Move.performed -= OnMovement;
        _mainInputActions.Game.Stop.performed -= OnStop;
        _mainInputActions.Game.Pause.performed -= OnPause;
        _mainInputActions.Game.Click.performed -= OnClick;
    }

    private void OnDestroy()
    {
        _mainInputActions.Disable();
        Expose();
    }
}
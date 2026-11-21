using UnityEngine;
using UnityEngine.InputSystem;

public class SwingPusher : MonoBehaviour
{
    [SerializeField, Min(1f)] private float pushForce = 8f;
    [SerializeField] private Rigidbody _rigidbody;

    private InputReadingSystem _inputReadingSystem;

    private void Awake()
    {
        _inputReadingSystem = new InputReadingSystem();
    }

    private void OnEnable()
    {
        if (_inputReadingSystem != null)
        {
            _inputReadingSystem.Enable();
            _inputReadingSystem.Player.Swing.started += Push;
        }
    }

    private void OnDisable()
    {
        if (_inputReadingSystem != null)
        {
            _inputReadingSystem.Player.Swing.started -= Push;
            _inputReadingSystem.Disable();
        }
    }

    private void Push(InputAction.CallbackContext ctx)
    {
        _rigidbody.AddForce(transform.forward * pushForce, ForceMode.Impulse);
    }


}

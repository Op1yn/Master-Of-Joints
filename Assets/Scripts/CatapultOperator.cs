using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class CatapultOperator : MonoBehaviour
{
    [SerializeField] private GameObject _stone;
    [SerializeField] private Transform _spawnPointStone;
    [SerializeField] private HingeJoint _hinge;
    [SerializeField] private float _throwingAngle = 60f;
    [SerializeField] private float _reloadAngle = 0f;

    private float _loadingTime = 2f;
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
            _inputReadingSystem.Player.Throw.started += Throw;
            _inputReadingSystem.Player.Load.started += Load;
        }
    }

    private void OnDisable()
    {
        if (_inputReadingSystem != null)
        {
            _inputReadingSystem.Player.Throw.started -= Throw;
            _inputReadingSystem.Player.Load.started -= Load;
            _inputReadingSystem.Disable();
        }
    }

    private void Throw(InputAction.CallbackContext ctx)
    {
        SetAngle(_throwingAngle);
    }

    private void Load(InputAction.CallbackContext ctx)
    {
        SetAngle(_reloadAngle);
        Invoke(nameof(PutStone), _loadingTime);

    }

    private void SetAngle(float angle)
    {
        JointSpring spring = _hinge.spring;
        spring.targetPosition = angle;
        _hinge.spring = spring;
    }

    private void PutStone()
    {
        Instantiate(_stone, _spawnPointStone);
    }
}

using UnityEngine;

[RequireComponent(typeof(MovementBehaviour))]
public class CrouchAbilityBehaviour : MonoBehaviour
{
    [SerializeField]
    [Range(0, 5)]
    private float _crouchingSpeed;

    private MovementBehaviour _movementBehaviour;

    private void Start()
    {
        _movementBehaviour = GetComponent<MovementBehaviour>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _movementBehaviour.SetActiveMovementSpeed(_crouchingSpeed);
        if (Input.GetKeyUp(KeyCode.LeftShift))
            _movementBehaviour.ResetActiveMovementSpeed();
    }
}
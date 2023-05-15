using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementBehaviour : MonoBehaviour
{
    [Range(0, 20)]
    [SerializeField]
    private float movementSpeed;
    
    private Rigidbody2D _rb;
    private Vector2 _newVelocity;
    private float activeMovespeed;
    private float _horizontalInput;
    private float _verticalInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        ResetActiveMovementSpeed();
    }

    private void Update()
    {
        HandleInput();

        _rb.velocity = _newVelocity;
    }

    public void SetActiveMovementSpeed(float speedToSet)
    {
        activeMovespeed = speedToSet;
    }

    public void ResetActiveMovementSpeed()
    {
        SetActiveMovementSpeed(movementSpeed);
    }

    private void HandleInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        _newVelocity = new Vector2(_horizontalInput, _verticalInput);
        _newVelocity = Vector2.ClampMagnitude(_newVelocity, 1);
        _newVelocity *= activeMovespeed;
    }

    private void OnValidate()
    {
        ResetActiveMovementSpeed();
    }
}
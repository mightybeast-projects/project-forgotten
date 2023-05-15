using UnityEngine;

[RequireComponent(typeof(MovementBehaviour))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class DashAbilityBehaviour : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField]
    private float _dashSpeed;

    [Range(0, 2)]
    [SerializeField]
    private float _dashLength;

    [Range(0, 5)]
    [SerializeField]
    private float _dashCooldown;

    private MovementBehaviour _movementBehaviour;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collision;
    private Color _defaultColor;
    private Color _dashColor;
    private float _dashCounter;
    private float _dashCoolCounter;
    private bool _rotating;

    private void Start()
    {
        _movementBehaviour = GetComponent<MovementBehaviour>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collision = GetComponent<Collider2D>();
        _defaultColor = _spriteRenderer.color;
        _dashColor = new Color(_defaultColor.r, _defaultColor.g, _defaultColor.b, 0.4f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            if (CanDash())
                StartDash();

        if (_dashCounter > 0)
        {
            _dashCounter -= Time.deltaTime;

            if (_dashCounter <= 0)
                EndDash();
        }

        if (_dashCoolCounter > 0)
            _dashCoolCounter -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (_dashCounter > 0)
            transform.RotateAround(transform.position, transform.forward, _dashLength * 360);
        else transform.rotation = Quaternion.identity;
    }

    private void StartDash()
    {
        _movementBehaviour.SetActiveMovementSpeed(_dashSpeed);
        _dashCounter = _dashLength;
        _spriteRenderer.color = _dashColor;
        _collision.enabled = false;
    }

    private void EndDash()
    {
        _movementBehaviour.ResetActiveMovementSpeed();
        _dashCoolCounter = _dashCooldown;
        _spriteRenderer.color = _defaultColor;
        _collision.enabled = true;
    }

    private bool CanDash()
    {
        return _dashCoolCounter <= 0 && _dashCounter <= 0;
    }
}
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Vector3 _velocity;
    private float _speed;
    private float _minSpeed;
    private float _accellerationRate;
    private float _decellerationRate;

    public void Instantiate(Vector3 velocity, float speed)
    {
        _velocity = velocity;
        _speed = speed;
        _minSpeed = _speed / 10;
    }

    public void SetAccellerationRate(float accellerationRate)
    {
        _accellerationRate = accellerationRate;
    }

    public void SetDecellerationRate(float decellerationRate)
    {
        _decellerationRate = decellerationRate;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _speed += _accellerationRate;
        _speed -= _decellerationRate;
        if (_speed < _minSpeed) _speed = _minSpeed;

        transform.position += _velocity.normalized * Time.deltaTime * _speed;
    }
}
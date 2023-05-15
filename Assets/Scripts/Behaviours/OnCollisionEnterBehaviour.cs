using UnityEngine;
using UnityEngine.Events;

public class OnCollisionEnterBehaviour : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _action;

    private void OnCollisionEnter2D(Collision2D other)
    {
        _action.Invoke();
    }
}
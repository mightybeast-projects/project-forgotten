using Cinemachine;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _bossTransform;

    private Animator _animator;
    private CinemachineVirtualCamera _camera;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void SwitchShakeStatus()
    {
        bool shakeStatus = _animator.GetBool("Shake");
        _animator.SetBool("Shake", !shakeStatus);
    }

    public void SwitchTargetToBoss()
    {
        _camera.Follow = _bossTransform;
    }
}
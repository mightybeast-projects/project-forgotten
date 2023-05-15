using UnityEngine;

public class SignatureManagerBehaviour : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchSignature()
    {
        bool currentSignature = _animator.GetBool("Signature_2_2");
        _animator.SetBool("Signature_2_2", !currentSignature);
    }
}
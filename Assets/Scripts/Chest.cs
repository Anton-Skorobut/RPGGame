using System;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _animator.SetTrigger("open");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(PLAYER_TAG))
        {
            Open();
        }
    }
}
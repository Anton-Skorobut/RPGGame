using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Bridge : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField]
    private GameObject _fireObject;
    
    private Rigidbody[] _rigidbodies;
    private NavMeshObstacle _navMeshObstacle;
    
    private float _minForceValue = 150;
    private float _maxForceValue = 200;

    private void Awake()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void Break()
    {
        // Вырезаем отверстие в навмеш (чтобы игрок там больше не смог пройти)
        _navMeshObstacle.enabled = true;
        
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = false;

            // Генерируем случайную силу.
            var forceValue = Random.Range(_minForceValue, _maxForceValue);

            // Генерируем случайное направление.
            var direction = Random.insideUnitSphere;
            
            // Придаем силу каждому rigidbody.
            rigidbody.AddForce(direction * forceValue);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(PLAYER_TAG) && collider.GetComponent<Outline>().OutlineWidth != 2f)
        {
            Break();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (_navMeshObstacle.enabled == false)
        {
            _fireObject.gameObject.SetActive(true);
        }
    }
}
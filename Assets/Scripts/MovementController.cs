using System;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private const string POTION = "Potion";
    
    private NavMeshAgent _navMeshAgent;
    private Vector3 _destination;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Перемещаем персонажа в направлении _destination.
        _navMeshAgent.SetDestination(_destination);
        
        // TODO: Получите точку, по которой кликнули мышью и задайте ее вектор в поле _destination.
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                _destination = hitInfo.point;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == POTION)
        {
            Destroy(collider.gameObject);
            GetComponent<Outline>().OutlineWidth = 2f;
        }
    }
}
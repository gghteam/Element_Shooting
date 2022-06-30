using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour
{
    public bool isFly { get; private set; } = false;
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask mapLayer;

    private void FixedUpdate()
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach(Collider2D colider in coliders)
        {
            Debug.Log(colider.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEmpty : MonoBehaviour
{
    public Vector2 size;
    [SerializeField]
    private LayerMask mapLayer;

    private void FixedUpdate()
    {
        Collider2D colider = Physics2D.OverlapBox(transform.position, size, 0, mapLayer);

        if (colider != null)
        {
            if (colider.CompareTag("Bat")) return;
            colider.GetComponent<Enemy>().DieEnemy();
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, size);
    }
}

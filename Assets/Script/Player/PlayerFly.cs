using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour
{
    public bool isFly { get; private set; } = false;
    public Vector2 size;
    [SerializeField]
    private LayerMask mapLayer;

    [SerializeField]
    private bool _isColType = true;

    private void FixedUpdate()
    {
        if(_isColType)
        {
            Collider2D colider = Physics2D.OverlapBox(transform.position, size, 0, mapLayer);

            if (colider != null)
            {
                if (colider.CompareTag("Empty")) isFly = true;
                else isFly = false;
            }
        }
        else
        {
            Collider2D circleCol = Physics2D.OverlapCircle(transform.position, size.x, mapLayer);

            if (circleCol != null)
            {
                if (circleCol.CompareTag("Empty")) isFly = true;
                else isFly = false;
            }
        }

        
    }


    /*
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    
#endif
    */

    void OnDrawGizmos()
    {
        if(_isColType)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, size);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, size.x);
        }
        
    }
}

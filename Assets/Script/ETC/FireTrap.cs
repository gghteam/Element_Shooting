using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    protected bool _isFire = true;
    public bool IsFire
    {
        set { _isFire = value; }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&_isFire)
        {
            IHittable hittable = collision.GetComponent<IHittable>();
            hittable?.GetHit(1, gameObject);
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactScript : PoolableMono
{
    private AudioSource _audioSoruce;
    private void Awake()
    {
        _audioSoruce = GetComponent<AudioSource>();
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {
        //do nothing here!
    }

    public override void Reset()
    {
        transform.localRotation = Quaternion.identity;
    }

    public virtual void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
        if (_audioSoruce != null && _audioSoruce.clip != null)
        {
            _audioSoruce.Play();
        }
    }

    public void DestroyAfterAnimation()
    {
        PoolManager.Instance.Despawn(this.gameObject);
    }

}

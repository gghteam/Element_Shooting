using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public abstract void CreateEffect();
    public abstract void CompletePrevEffect();
    protected virtual void OnDestroy()
    {
        CompletePrevEffect();
    }
}

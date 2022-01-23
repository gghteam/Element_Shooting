using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathEffect : Effect
{    
    [field:SerializeField]
    public UnityEvent DeathCallback { get; set; }


    public override void CompletePrevEffect()
    {

    }

    public override void CreateEffect()
    {

        DeathCallback.Invoke();
        
    }
}

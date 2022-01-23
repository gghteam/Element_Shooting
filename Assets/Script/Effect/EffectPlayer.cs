using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField]
    private List<Effect> _EffectToPlay = null;

    public void PlayEffect()
    {
        FinishEffect();
        foreach (Effect effect in _EffectToPlay)
        {
            effect.CreateEffect();
        }
    }

    private void FinishEffect()
    {
        foreach(Effect effect in _EffectToPlay)
        {
            effect.CompletePrevEffect();
        }
    }
}

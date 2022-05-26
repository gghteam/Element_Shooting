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

    public void Shield()
    {
        //GameManager.Instance.shield.Check();
        /*
        if (transform.parent.name == "Water_Crital")
            GameManager.Instance.shield.blue.SetActive(true);
        else
            GameManager.Instance.shield.red.SetActive(true);
        */
    }
    private void FinishEffect()
    {
        foreach(Effect effect in _EffectToPlay)
        {
            effect.CompletePrevEffect();
        }
    }
}

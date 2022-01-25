using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementAnimation : MonoBehaviour
{
    private PlayerController playerController;
    private Animator ani;
    private Material material;
    Conditions conditions;
    Conditions currutCondition = Conditions.Fire;
    private void Awake() {
        playerController = GetComponentInParent<PlayerController>();
        ani = GetComponent<Animator>();
        material = GetComponent<Material>();
    }
    private void Start() {
        conditions = GameManager.Instance.playerController.GetCondition;
        ChangedElements();
    }
    public void ChangedElements()
    {
        playerController._isElement = true;
        currutCondition = conditions;
        conditions = GameManager.Instance.playerController.GetCondition;
        StartCoroutine(ElementOff(conditions,currutCondition));
    }
    private IEnumerator ElementOff(Conditions Nextconditions,Conditions currentCondition)
    {
        switch(currentCondition)
        {
            case Conditions.Fire:
            ani.Play("Fire_Element_Off_Animation");
            break;
            case Conditions.Water:
            ani.Play("Water_Element_Off_Animation");
            break;
            case Conditions.Wind:
            ani.Play("Wind_Element_Off_Animation");
            break;
            case Conditions.Stone:
            break;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(ElementOnToIdle(Nextconditions));

    }
    private IEnumerator ElementOnToIdle(Conditions conditions)
    {
        switch(conditions)
        {
            case Conditions.Fire:
                ani.Play("Fire_Element_On_Animation");
                yield return new WaitForSeconds(1f);   
                ani.Play("Fire_Element_Idle_Animation");
            break;
            case Conditions.Water:
                ani.Play("Water_Element_On_Animation");
                yield return new WaitForSeconds(1f);   
                ani.Play("Water_Element_Idle_Animation");
            break;
            case Conditions.Wind:
                ani.Play("Wind_Element_On_Animation");
                yield return new WaitForSeconds(1f);
                ani.Play("Wind_Element_Idle_Animation");
            break;
            case Conditions.Stone:
                yield return new WaitForSeconds(1f);   
            break;
        }
        playerController._isElement = false;
    }
}

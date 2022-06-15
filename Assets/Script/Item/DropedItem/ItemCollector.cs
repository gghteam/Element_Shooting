using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class ItemCollector : MonoBehaviour
{
    private int _resourceLayer;

    private void Awake()
    {
        _resourceLayer = LayerMask.NameToLayer("Resource");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == _resourceLayer)
        {
            Resource res = collision.gameObject.GetComponent<Resource>();
            if(res!=null)
            {
                switch(res.ResourceData.ResourceType)
                {
                    case ResourceTypeEnum.Coin:
                        break;
                    case ResourceTypeEnum.MonsterDropItem:
                        GameManager.Instance.ItemInventoryCase.InputItemCase(res.ResourceData);
                        res.PickUpResource();
                        break;
                    case ResourceTypeEnum.MagicSpell:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

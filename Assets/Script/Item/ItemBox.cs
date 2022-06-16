using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;
using Random = UnityEngine.Random;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    private ItemBoxDataSO _itemBoxData;

    private bool[] _dropedItemIndex = new bool[5];
    private int _boxInItemCount = 0;

    private Animator _animator;
    private readonly int _openHasStr = Animator.StringToHash("Open");
    
    private bool _isOpenBox;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        _boxInItemCount = _itemBoxData.itemList.Count;
        for(int i = 0;i<_dropedItemIndex.Length;i++)
        {
            _dropedItemIndex[i] = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(OpenBox());
        }
    }

    private IEnumerator OpenBox()
    {
        if(_isOpenBox==false)
        {
            _isOpenBox = true;
            _animator.SetTrigger(_openHasStr);

            yield return new WaitForSeconds(0.5f);

            int dropItemCount = _itemBoxData.maxDropItem;
            dropItemCount = Mathf.Clamp(dropItemCount, 1, _boxInItemCount);
            int dropIndex = 0;
            int i = 0;

            while(true)
            {
                if(i>=dropItemCount)
                {
                    break;
                }
                dropIndex = Random.Range(0, _boxInItemCount);
                if (_dropedItemIndex[dropIndex])
                {
                    continue;
                }
                _dropedItemIndex[dropIndex] = true;

                DropItem(dropIndex);

                Debug.Log(_itemBoxData.itemList[dropIndex]);

                i++;
            }
        }
    }

    void DropItem(int idx)
    {
        GameObject prefab = PoolManager.Instance.GetPooledObject((int)PooledIndex.Item);
        Item dropItem = prefab.GetComponent<Item>();
        dropItem.ItemData = _itemBoxData.itemList[idx];
        prefab.transform.position = transform.position;
        prefab.SetActive(true);

        Vector2 randomOffset = Random.insideUnitCircle;

        dropItem.SpawnInBox(transform.position + (Vector3)randomOffset* 1.2f,power:1f,time:0.8f);
    }
}

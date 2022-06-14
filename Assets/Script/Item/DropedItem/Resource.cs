using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : PoolableMono
{
    [field:SerializeField]
    public ResourceDataSO ResourceData { get; set; }

    private AudioSource _audioSource;
    private Collider2D _collider2d;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider2d = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetValue();
    }
    public void SetValue()
    {
        _spriteRenderer.sprite = ResourceData.itemSprite;
        _audioSource.clip = ResourceData.useSound;
    }
    public void PickUpResource()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        _collider2d.enabled = false;
        _spriteRenderer.enabled = false;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length + 0.3f);
        PoolManager.Instance.Despawn(this.gameObject);
    }

    public void DestroyResource()
    {
        gameObject.SetActive(false);
        PoolManager.Instance.Despawn(this.gameObject);
    }

    public override void Reset()
    {
        _spriteRenderer.enabled = true;
        _collider2d.enabled = true;
    }
}

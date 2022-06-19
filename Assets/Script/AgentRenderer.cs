using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRenderer : MonoBehaviour
{
    private EnemyAiBrain _enemyAiBrain;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _enemyAiBrain = GetComponentInParent<EnemyAiBrain>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _enemyAiBrain.OnPointerPositionChanged += FaceDirection;
    }
    public void FaceDirection(Vector2 pointerInput)
    {
        Debug.Log("asd");
        Vector3 direction = (Vector3)pointerInput - transform.position;
        Vector3 result = Vector3.Cross(Vector2.up, direction);

        if (result.z > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (result.z < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}

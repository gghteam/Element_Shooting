using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementData : MonoBehaviour
{
    [field: SerializeField]
    public Vector2 dir {get; set;}
    [field: SerializeField]
    public Vector2 pointOfInterest {get; set;}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAgentInput
{
    public Action<Vector2> OnMovementKeyPressd { get; set;}
    public Action<Vector2> OnPointerPositionChanged { get; set;}
    public Action OnFireButtonPress { get; set;}
    public Action OnFireButtonReleased { get; set;}
}

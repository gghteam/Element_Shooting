using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent GetItemKeyPress { get; set; }

    private void Update()
    {
        InputGetItemKeyPress();
    }

    private void InputGetItemKeyPress()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            GetItemKeyPress?.Invoke();
        }
    }
}

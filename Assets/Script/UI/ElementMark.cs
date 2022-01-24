using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementMark : MonoBehaviour
{
    [SerializeField]
    private Gradient[] gradients;
    [SerializeField]
    private Image image;
    private void Start() {
        image = GetComponent<Image>();
    }
}

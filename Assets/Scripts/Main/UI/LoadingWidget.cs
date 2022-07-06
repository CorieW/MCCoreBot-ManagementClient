using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingWidget : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _rotationSpeed;

    private bool _loading;
    public bool Loading {
        get { return _loading; }
        set {
            _loading = value;
            _image.enabled = value;
            transform.localEulerAngles = Vector3.zero;
        }
    }

    private void Awake()
    {        
        Loading = false;
    }

    private void Update()
    {
        if (Loading) transform.localEulerAngles += -Vector3.forward * _rotationSpeed * Time.deltaTime;
    }
}
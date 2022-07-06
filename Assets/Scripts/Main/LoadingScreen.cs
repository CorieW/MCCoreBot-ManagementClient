using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _fadeScreenImg;
    [SerializeField] private LoadingWidget _loadingWidget;

    public bool FadeScreenEnabled { get; set; }

    public void SetLoadingScreenVisibility(bool visible)
    {
        _fadeScreenImg.enabled = FadeScreenEnabled ? visible : false;
        _loadingWidget.Loading = visible;
    }
}

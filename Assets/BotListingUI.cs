using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotListingUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Image _mcHeadSkinImage;
    [SerializeField] protected Text _mcUsernameText;

    public void SetUsername(string mcUsername)
    {
        _mcUsernameText.text = mcUsername;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Block
{
    public string name;
    //* Notice how there is no ID. That's because that'll easily be retrieveable through the registry.
    public string materialType;
}

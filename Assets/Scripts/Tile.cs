using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    private Color color;

    //index in spellqueue spelltypes
    private int spellType;

    private bool isUsed;

    internal void InitializeRandom()
    {   
        Initialize(SpellQueue.instance.GetRandomSpellType());
    }

    internal void Initialize(int newSpellType)
    {
        spellType = newSpellType;
        color = SpellQueue.instance.spellTypes[spellType];
        renderer.material.color = color;
        isUsed = false;
    }

    internal void MarkForUse()
    {
        color = Color.gray;
        renderer.material.color = color;
        isUsed = true;
    }

    public int GetSpellType()
    {
        return spellType;
    }

    public bool IsUsed()
    {
        return isUsed;
    }
}

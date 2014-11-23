using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	public Material defaultMaterial;
    private Material material;

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
        material = SpellQueue.instance.spellTypes[spellType];
        renderer.material = material;
        isUsed = false;
    }

    internal void MarkForUse()
    {
		renderer.material = defaultMaterial;
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

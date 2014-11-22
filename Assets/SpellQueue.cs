using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellQueue : MonoBehaviour
{
    public GameObject sourceTile;

    private int tileCount = 6;

    List<GameObject> tiles;
    private float tileDistance = 1.1f;

    private float triggerTime = 2f;

    public Color[] spellTypes;

    public static SpellQueue instance;

    private int[] chargeValues;

    void Awake()
    {
        instance = this;

        chargeValues = new int[spellTypes.Length];
    }

    // Use this for initialization
    void Start()
    {
        tiles = new List<GameObject>();

        for (int i = 0; i < tileCount; i++)
        {
            GameObject newTile = GameObject.Instantiate(sourceTile) as GameObject;
            tiles.Add(newTile);
            tiles[i].transform.parent = transform;
            tiles[i].transform.localPosition = - Vector3.left * i * tileDistance;
            tiles[i].transform.localRotation = Quaternion.identity;
            tiles[i].GetComponent<Tile>().InitializeRandom();
        }
    }

    public void AddChargeFromTile(int index)
    {
        StopCoroutine("SpellTrigger");

        int spellType = tiles[index].GetComponent<Tile>().GetSpellType();

        if (CanAddChargeOfType(spellType))
        {
            Debug.Log("can add type " + spellType);
            StartCoroutine("SpellTrigger");
            chargeValues[spellType]++;
            tiles[index].GetComponent<Tile>().MarkForUse();
        }
        else
        {
            Debug.Log("cannot add type " + spellType + " to ");
            for (int i = 0; i < chargeValues.Length; i++)
            {
                Debug.Log(chargeValues[i]);
            }
            // this means that we have another type charged and we cancel it
        }        
    }

    private bool CanAddChargeOfType(int spellType)
    {
        for (int i = 0; i < chargeValues.Length; i++)
        {
            if (chargeValues[i] != 0 && i != spellType)
            {
                return false;
            }
        }

        return true;
    }

    internal int GetRandomSpellType()
    {
        return Random.Range(0, spellTypes.Length);
    }

    private IEnumerator SpellTrigger()
    {
        Debug.Log("spell trigger started");
        yield return new WaitForSeconds(triggerTime);
        CastSpell();
    }

    private void CastSpell()
    {
        for (int i = 0; i < chargeValues.Length; i++)
        {
            // if we have found that spell that has charge
            if (chargeValues[i] > 0)
            {
                chargeValues[i] = 0;
                CastSpecificSpell(i);
            }
        }
    }

    private void CastSpecificSpell(int spellType)
    {
        Debug.Log("Spell cast: " + spellType);
        // cast the spell having the key [spellType], from the collections of spells (monobeh) on the object
    }
}
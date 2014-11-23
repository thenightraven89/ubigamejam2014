using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellQueue : MonoBehaviour
{
    public GameObject sourceTile;

    private int tileCount = 6;

    List<GameObject> tiles;
    private float tileDistance = 1.1f;

    // time you have to wait in order to trigger spell
    private float triggerTime = 0.3f;

    public Material[] spellTypes;
	public SpellLevels[] spells;

    public static SpellQueue instance;

    private int[] chargeValues;

    public Transform tileSpawnPoint;
	public Transform spellSpawnPoint;

    void Awake()
    {
        instance = this;

        chargeValues = new int[spellTypes.Length];
    }

    // Use this for initialization
    void Start()
    {
        tiles = new List<GameObject>();

        StartCoroutine("AddMissingTiles");

        //for (int i = 0; i < tileCount; i++)
        //{
        //    GameObject newTile = GameObject.Instantiate(sourceTile) as GameObject;
        //    tiles.Add(newTile);
        //    tiles[i].transform.parent = transform;
        //    tiles[i].transform.localPosition = tileSpawnPoint.localPosition;
        //    tiles[i].transform.localRotation = Quaternion.identity;
        //    tiles[i].GetComponent<Tile>().InitializeRandom();

        //    LeanTween.moveLocalX(tiles[i], i * tileDistance, 0.5f);
        //}
    }

    public void AddChargeFromTile(int index)
    {
        StopCoroutine("SpellTrigger");

        int spellType = tiles[index].GetComponent<Tile>().GetSpellType();

        tiles[index].GetComponent<Tile>().MarkForUse();

        if (CanAddChargeOfType(spellType))
        {
            Debug.Log("can add type " + spellType);
            StartCoroutine("SpellTrigger");
            chargeValues[spellType]++;
            
        }
        else
        {
            Debug.Log("cannot add type " + spellType);
            // this means that we have another type charged and we cancel it
            StopCoroutine("CancelCasting");
            StartCoroutine("CancelCasting");
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
        int rnd = Random.Range(0, spellTypes.Length * 2);
        if (rnd < spellTypes.Length)
        {
            return rnd;
        }
        else
        {
            return 1;
        }
    }

    private IEnumerator SpellTrigger()
    {
        Debug.Log("spell trigger started");
        yield return new WaitForSeconds(triggerTime);
        CastSpell();
    }

    private float punishmentTime = 1f;

    private IEnumerator CancelCasting()
    {
        Debug.Log("cancelled");

        //gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, 3f, gameObject.transform.localPosition.z);
        LeanTween.moveLocalY(gameObject, 0.5f, 0.2f).setEase(LeanTweenType.easeShake);

        yield return new WaitForSeconds(punishmentTime);
        ClearUsedTiles();
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
		Instantiate(spells[spellType].spellLevels[chargeValues[spellType]],
		            spellSpawnPoint.position, spellSpawnPoint.rotation);
        ClearUsedTiles();
        // cast the spell having the key [spellType], from the collections of spells (monobeh) on the object
    }

    private float tileRefreshTime = 0.2f;
    private float tileRefreshDelay = 0.1f;

    private void ClearUsedTiles()
    {
        // reverse removal
        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            if (tiles[i].GetComponent<Tile>().IsUsed())
            {
                GameObject.Destroy(tiles[i]);
                tiles.RemoveAt(i);
            }
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            LeanTween.moveLocalX(tiles[i], i * tileDistance, tileRefreshTime).setEase(LeanTweenType.easeOutQuad);
        }

		for (int i = 0; i < chargeValues.Length; i++)
		{
			chargeValues[i]=0;
		}

        StartCoroutine("AddMissingTiles");
    }

    private IEnumerator AddMissingTiles()
    {
        while (tiles.Count < tileCount)
        {
            AddRandomTile();
            yield return new WaitForSeconds(tileRefreshDelay);
        }
    }

    private void AddRandomTile()
    {
        GameObject newTile = GameObject.Instantiate(sourceTile) as GameObject;
        tiles.Add(newTile);
        newTile.transform.parent = transform;
        newTile.transform.localPosition = tileSpawnPoint.localPosition;
        newTile.transform.localRotation = Quaternion.identity;
        newTile.GetComponent<Tile>().InitializeRandom();

        LeanTween.moveLocalX(newTile, (tiles.Count - 1) * tileDistance, tileRefreshTime).setEase(LeanTweenType.easeOutQuad);
    }
}
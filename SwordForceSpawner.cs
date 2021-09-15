using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordForceSpawner : MonoBehaviour
{
    protected void SpawnSF(GameObject _prefabSF)
    {
        GameObject SwordForce = Instantiate(_prefabSF, transform) as GameObject;
    }
}

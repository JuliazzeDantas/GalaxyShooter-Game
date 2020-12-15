using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour
{
    void Start()
    {
        Destruction();   
    }
    private void Destruction()
    {
        StartCoroutine(EnemyDown());
    }

    private IEnumerator EnemyDown()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}

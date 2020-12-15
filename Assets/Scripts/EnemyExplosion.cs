using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * 1 * Time.deltaTime);
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

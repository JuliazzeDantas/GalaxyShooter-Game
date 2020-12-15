using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        Action(); //Chama a função Action abaixo
    }

    private void Action()
    {
        transform.Translate(Vector3.up * _laserSpeed); // faço o lazer se mover para cima com a velocidade estipulada
        if (transform.position.y > 6)
        {
            Destroy(this.gameObject); // se o tiro sai da tela, ele é destruído
        }
    }
}

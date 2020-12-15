using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //life
    private int _life = 2;

    //speed
    [SerializeField]
    private float _speed = 1;

    //explosion
    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    //UIManagae 
    private UIManager _uiManager;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>(); //faz a variavel pegar pode acessar e usar as componentes do tipo UIManager
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime); // o inimigo se movimenta para baixo a todo instante
        if (transform.position.y < -8.5f) //se ele some do campo de vião
        {
            transform.position = new Vector3(Random.Range(-7.75f, 7.75f), 6.5f, 0); //reaparece no top da tela em um posição x aleatoria
        }
        if(_uiManager._menuOn == true)
        {
            Destroy(this.gameObject); //se o menu estiver ligado todos os inimigos somem
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //Método para identificar colisões
    {
        if (other.CompareTag("Player")) // se ele colidir com o player other = player
        {
            Player player = other.GetComponent<Player>(); //passa para a variavel player os componentes de other
            player.Damage(); //o player toma dano
            _life = 0;
        }
        else if (other.CompareTag("Laser")) // se ele colidir com o laser, other =  laser
        {
            Laser laser = other.GetComponent<Laser>(); //passa para a variavel player os componentes de laser
            Destroy(laser.gameObject); //destroi o laser
            _life--;
        }
        if (_life <= 0) // se a nave chega a 0 de vida
        {
            SelfDestruction();
        }
    }

    public void SelfDestruction() //se a nave chega a 0 de vida simula-se a destruição dela e soma no score
    {
        _uiManager.UpdateScore(); // soma 10 no score
        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity); // instancia a animação de destruição da nave inimiga
        Destroy(this.gameObject); // e destroi esse objeto
    }
}
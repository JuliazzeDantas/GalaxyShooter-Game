using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    // Start is called before the first frame update

    //UIManagae (usado para contagem de score)
    private UIManager _uiManager;

    //audio
    [SerializeField]
    AudioClip _clip; //uso a variavel to tipo audioclip, pois o som será reproduzido depois que o objeto for destruido

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>(); //faz a variavel pegar todos os componentes do tipo UIManager
    }

    // Update is called once per frame
    void Update()
    {
        Movement();// chamo o método Movement
        if (_uiManager._menuOn == true)
        {
            Destroy(this.gameObject); //se o menu estiver ligado, todos os inimigos somem
        }
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);// o powerup irá para baixo com uma velocidade de "_speed"
        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);//caso ele não seja pego pelo player, será destruído qunado sair da tela
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // caso ocorra colisão, esse método é acionado
    {
        if (other.CompareTag("Player")) // caso ele colida com algum objeto com a tag Player, os seguintes passos serão executados
         {
            Player player = other.GetComponent<Player>(); //a variavel player receberá os componentes do gameObject Player
            if(player != null) //evitamos erros em tentar variaveis que não existam, caso o player seja null
            {   
                if(this.CompareTag("TripleShot"))
                {
                    player.TripleShotPowerOn(); //chamamos um método do Script do Player para ligar o TripleShot
                }
                else if(this.CompareTag("Speed"))
                {
                    player.SpeedBoostPowerOn(); // chamamos um método do Script do Player para ligar o SpeedBooster
                }
                else if(this.CompareTag("Shield"))
                {
                    player.ShieldActivation(); //chamamos um método do Script do Player para ligar o shield
                }
                
            }
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject); //o icone é destruído
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //life
    private int _life = 3;
    [SerializeField]
    private GameObject _playerDestructionPrefab;

    //Damage
    [SerializeField]
    private GameObject[] _damage;
    private int _choice;

    //speed
    private float _speed = 6.0f;
    
    //shoot
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    
    //tripleshot
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private bool _canTripleShot = false;

    //shield
    [SerializeField]
    private GameObject _shield;
    private bool _shieldOn;

    // UIManager
    private UIManager _uiManager;

    //Audio
    private AudioSource _audioSource; 

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0); //o Objeto é iniciado em (0,0,0)
        _shieldOn = false; //inicializa a vairavel como falso para o player poder tomar dano

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>(); //Passa para o _uiManager os componentes de Canvas

        _audioSource = GetComponent<AudioSource>();

        if(_uiManager != null) //se o _uiManager for diferente de null chama afunção
        {
            _uiManager.UpdateLives(_life); //faz aparecer 3 naves como vida do player
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); // a cada Update chama o método Movement criado abaixo
        if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetMouseButtonDown(0))) // caso clique em "space" ou no botão esquerdo do mouse o método shot será chamado
        {
            Shot(); // a cada Update chama o método shot criado abaixo
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //pego, do teclado, o pressionamento das setas horizontais
        float verticalInput = Input.GetAxis("Vertical");//pego, do teclado, o pressionamento das setas verticais

       //uso o transform.Translate para movimentar o Objeto
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime); // faço o transform.translate receber um vetor de 3 coordenadas apontando para a direita, um valor entre -1 e 1, a velocidade que quero que se movimente e um delta t
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime); // faço o transform.translate receber um vetor de 3 coordenadas apontando para a direita, um valor entre -1 e 1, a velocidade que quero que se movimente e um delta t

        if (transform.position.y > 0) // no eixo y, limitamos o player até metade da tela
        {
            transform.position = new Vector3(transform.position.x, 0, 0); //caso ele passe de zero, o maderemos de volta para (x,0,0)
        }
        else if (transform.position.y < -4.2)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0); //caso ele passe de 4.2, o maderemos de volta para (x,0,0)
        }

        if (transform.position.x > 9.3) //no eixo x, fizemos o Player "teletransportar para o outro lado da tela quando cruzar o limite visível
        {
            transform.position = new Vector3(-9.3f, transform.position.y, 0); // Caso passe do limite da tela, mandaremos para o outro lado dela
        }
        else if (transform.position.x < -9.3)
        {
            transform.position = new Vector3(9.3f, transform.position.y, 0); // Caso passe do limite da tela, mandaremos para o outro lado dela
        }
    }

    private void Shot()
    {
        if(Time.time >= _canFire) //vejo se o tempo do jogo é maior ou igual que o tempo de permissão para atirar
        {
            _audioSource.Play();
            if (_canTripleShot) //caso a nave esteja com o powerup de tiro triplo, chamaremos a método de tripleshot
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity); // instancio o laser Triplo sempre que o canTripleShot estiver ativo e eu apertar "space" ou o botão do mouse
            }
            else{
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.95f, 0), Quaternion.identity); // instancio o laser sempre que eu apertar "space" ou o botão do mouse
            }
            _canFire = Time.time + _fireRate; //coloco no canFire qual será o próximo tempo em que o jogador poderá atirar
        }

    }

    public void TripleShotPowerOn() // ligando o tripleshot
    {
        _canTripleShot = true; // aciona a permissão para que o tripleShot funcione
        StartCoroutine(TripleShotDownRotine()); // abre o sistem de cooldown do tripleshot
    }

    private IEnumerator TripleShotDownRotine() //lembrar que para usar o "yield return new WaitForSeconds" deve ter um método do tipo "IEnumerator"
    {
        yield return new WaitForSeconds(10.0f); // tempo estipulado para o powerup ter efeito = 7 segundos
        //a linha de codigo acima interrompe a linhas de códigos subsequentes por um determinado tempo estipulado 
        _canTripleShot = false; // depois dos 7 segundos, desabilita o tripleShot
    }

    public void SpeedBoostPowerOn() //ligando o speedboost
    {
        _speed = 12.0f; //incrementa a velocidade
        StartCoroutine(SpeedBoostDownRotine()); //abre o sistema de cooldown do speedboost
    }
    private IEnumerator SpeedBoostDownRotine() //lembrar que para usar o "yield return new WaitForSeconds" deve ter um método do tipo "IEnumerator"
    {
        yield return new WaitForSeconds(10.0f); //tempo estipulado de efeito = 5 segundos
        _speed = 4.0f; //volta a ter a velocidade antiga
    }

    public void ShieldActivation() //Método para a ativação do escudo
    {
        _shieldOn = true; //muda a variave chega o escudo quando toma dano para true
        _shield.SetActive(true); // ativa o escudo isualmente
    }

    public void Damage() //método acionado quando o player toma dnao
    {
        if (_shieldOn == true) //se, ao tomar dano, essa variavel for true
        {
            _shieldOn = false; //muda ela para false
            _shield.SetActive(false); //e desabilita o escudo
        }
        else //caso o _shieldOn for false o player toma dano
        {
            _life--; //fere a vida do usuario
            if(_life == 2)// faz aperecer feridas na nave conforme perde vida
            {
                _choice = Random.Range(0, 2); // escolhe uma das duas animações para aprecer de forma rondomica
                _damage[_choice].SetActive(true); // ativa sua animação
            }
            if(_life == 1) // aparece mais uma ferida antes de explodir
            {
                _choice = (_choice - 1) * (-1);  // meio de saber qual era a primeira animação de ferida 
                _damage[_choice].SetActive(true); //ativa a outra animação
            }

            _uiManager.UpdateLives(_life); // atualiza a imagem de vida na tela
            if (_life <= 0) //o usuario morre quando tem 0 ou menos de vida
            {
                SelfDestruction();
            }
        }
    }

    public void SelfDestruction()
    {
        Instantiate(_playerDestructionPrefab, transform.position, Quaternion.identity); //instancia a explosão do usuario 
        _uiManager.EnableMenu();
        Destroy(this.gameObject); //destroy a imagem do player durante a explosão
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    //animator
    private Animator _anima;
    // Start is called before the first frame update
    void Start()
    {
        _anima = GetComponent<Animator>(); //faz o anima reeber os componentes do animator do objeto
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) // se clicar no A Ativa o turnLeft
        {
            _anima.SetBool("Turn_Right", false);
            _anima.SetBool("Turn_Left", true);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) //se soltar o A volta para o idle
        {
            _anima.SetBool("Turn_Left", false);
            _anima.SetBool("Turn_Right", false);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) //se clicar no D Ativa o turnRight
        {
            _anima.SetBool("Turn_Left", false);
            _anima.SetBool("Turn_Right", true);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) //se soltar o D volta para o idle
        {
            _anima.SetBool("Turn_Left", false);
            _anima.SetBool("Turn_Right", false);
        
                }
    }
}

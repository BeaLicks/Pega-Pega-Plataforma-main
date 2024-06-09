using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caracter1 : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public GameObject arrow; // Referência ao objeto flecha
    public bool isJumping;
    public bool doubleJump;
    public bool hasArrow; // Indica se este personagem tem a flecha

    private Rigidbody2D rig;
    private Animator anim;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (arrow != null)
        {
            arrow.SetActive(hasArrow); // Ativa ou desativa a flecha com base na variável hasArrow
        }

        // Desabilita o controle do personagem no início
        this.enabled = false;
    }

    void Update()
    {
        Move();
        Jump();
        CheckArrowTransfer();
    }

    void Move()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            movement = new Vector3(1f, 0f, 0f);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement = new Vector3(-1f, 0f, 0f);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        transform.position += movement * Time.deltaTime * Speed;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("Jump", true);
            }
            else if (doubleJump)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = false;
            }
        }
    }

    void CheckArrowTransfer()
    {
        // Encontrar o objeto do personagem 2
        GameObject character2 = GameObject.FindGameObjectWithTag("Player2");
        if (character2 != null)
        {
            // Verificar se o personagem 2 tem a flecha e se este personagem está atrás dele
            if (hasArrow && transform.position.x < character2.transform.position.x)
            {
                Caracter2 character2Script = character2.GetComponent<Caracter2>();
                if (character2Script != null)
                {
                    // Transferir a flecha para o personagem 2
                    character2Script.hasArrow = true;
                    hasArrow = false;

                    if (arrow != null)
                    {
                        arrow.SetActive(false);
                    }

                    if (character2Script.arrow != null)
                    {
                        character2Script.arrow.SetActive(true);
                    }
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }
}
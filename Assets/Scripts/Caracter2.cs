using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caracter2 : MonoBehaviour
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

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement = new Vector3(1f, 0f, 0f);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
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
        // Encontrar o objeto do personagem 1
        GameObject character1 = GameObject.FindGameObjectWithTag("Player1");
        if (character1 != null)
        {
            // Verificar se o personagem 1 tem a flecha e se este personagem está atrás dele
            if (hasArrow && transform.position.x < character1.transform.position.x)
            {
                Caracter1 character1Script = character1.GetComponent<Caracter1>();
                if (character1Script != null)
                {
                    // Transferir a flecha para o personagem 1
                    character1Script.hasArrow = true;
                    hasArrow = false;

                    if (arrow != null)
                    {
                        arrow.SetActive(false);
                    }

                    if (character1Script.arrow != null)
                    {
                        character1Script.arrow.SetActive(true);
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
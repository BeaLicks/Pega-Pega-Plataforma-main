using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisorPlayers : MonoBehaviour

{
    public float crossingDistance = 2f; // Distância mínima para ativar o cruzamento
    public GameObject otherPlayer;

    private bool isCrossing = false;
    private Rigidbody2D rb;
    private Rigidbody2D otherRb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        otherRb = otherPlayer.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verifique se os jogadores estão próximos o suficiente para cruzar e estão indo na mesma direção
        if (Vector2.Distance(transform.position, otherPlayer.transform.position) < crossingDistance)
        {
            Vector2 playerDirection = rb.velocity.normalized;
            Vector2 otherPlayerDirection = otherRb.velocity.normalized;

            // Verifique se os jogadores estão indo na mesma direção (ou se ambos estão parados)
            if (Vector2.Dot(playerDirection, otherPlayerDirection) > 0 || (playerDirection == Vector2.zero && otherPlayerDirection == Vector2.zero))
            {
                // Ative o cruzamento
                isCrossing = true;

                // Desative temporariamente a colisão entre os jogadores
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), otherPlayer.GetComponent<Collider2D>(), true);
            }
            else
            {
                isCrossing = false;
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), otherPlayer.GetComponent<Collider2D>(), false);
            }
        }
        else
        {
            isCrossing = false;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), otherPlayer.GetComponent<Collider2D>(), false);
        }
    }

    void LateUpdate()
    {
        // Se os jogadores estiverem cruzando, ajuste a posição deles
        if (isCrossing)
        {
            // Mova um dos jogadores para frente ou para trás (dependendo da direção) para permitir a passagem
            Vector2 playerDirection = rb.velocity.normalized;
            transform.Translate(playerDirection * Time.deltaTime);
        }
    }
}

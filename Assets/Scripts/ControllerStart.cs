using UnityEngine;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
    public GameObject startScreen; // Referência ao painel da tela de início
    public Caracter1 character1; // Referência ao script do personagem 1
    public Caracter2 character2; // Referência ao script do personagem 2

    void Update()
    {
        // Verifica se qualquer tecla especificada é pressionada
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        // Desativa a tela de início
        startScreen.SetActive(false);

        // Habilita o controle dos personagens
        character1.enabled = true;
        character2.enabled = true;
    }
}
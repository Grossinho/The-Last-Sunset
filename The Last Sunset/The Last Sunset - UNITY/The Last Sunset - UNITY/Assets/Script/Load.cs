using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public Text m_Text;
    

    void Start()
    {        
        StartCoroutine(LoadScene());

    }
    

    IEnumerator LoadScene()
    {
        yield return null;

        // Comece a carregar a cena que você especificar
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Jogo");


        // Não deixe a cena ser ativada até que 
        asyncOperation.allowSceneActivation = false;

        
        // Quando o carregamento ainda está em andamento, produza a barra de texto e progresso
        while (!asyncOperation.isDone)
        {
            // gera o progresso atual
            //m_Text.text = "Carregando: " + (asyncOperation.progress * 100) + "%";

            // Verifique se o carregamento terminou 
            if (asyncOperation.progress >= 0.9f)
            {
                /// Altere o texto para mostrar que a cena está pronta 
                m_Text.text = "Pressione 'Espaço' para continuar";
                // Espere você pressionar a tecla espaço para ativar a cena 
                if (Input.GetKeyDown(KeyCode.Space))
                    // Ativa a cena 
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}

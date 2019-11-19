using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CinematicEffects;


public class GameController : MonoBehaviour
{
    public static GameController instancia;
    [SerializeField] Text textoDistancia, textoMusica;
    [SerializeField] RectTransform posTextoMusica;
    [SerializeField] Transform carroPos;
    [SerializeField] float aumentoDistancia, velocidadeTexto;
    [SerializeField] AudioSource aud;
    public float distancia;
    Vector3 posInicial, textoPosInicial;
    [SerializeField] float tempo = 3;
    
    [SerializeField] UnityEngine.Rendering.PostProcessing.PostProcessLayer post;

    Bloom bloom;
    LensAberrations lensAberrations;

    Camera cam;
    public float zoom = 90;
    public float normal = 60;
    float smooth = 5;
    bool isZoomed = false;
    bool paused = false;



    void Awake()
    {
        if (instancia == null) instancia = this;
        else if (instancia != this) Destroy(this.gameObject);
    }

    private void Start()
    {
        cam = Camera.main;
        bloom = cam.GetComponent<Bloom>();
        textoMusica.text = "Colete fitas para ouvir alguma coisa!";
        posInicial = carroPos.position;

        textoPosInicial = textoMusica.transform.localPosition;
        



    }
    private void Update()
    {
        distancia = Mathf.Round(Vector3.Distance(posInicial, carroPos.position) * aumentoDistancia);
        textoDistancia.text = distancia.ToString();

        textoMusica.text = aud.clip.name.ToString();
        textoMusica.transform.localPosition += new Vector3(-velocidadeTexto * Time.deltaTime, 0, 0);

        if (textoMusica.transform.localPosition.x < textoPosInicial.x - textoMusica.text.Length * 15)
        {
            textoMusica.transform.localPosition = textoPosInicial;
        }

        Pausar();
        
    }

    public void SaveRecord()
    {
        PlayerPrefs.SetFloat("newrecord", distancia);
    }

    public void nitro(float pot)
    {
        bloom.setIntensity(pot);
    }


    public void zom( bool valor)
    {
        isZoomed = valor;

        if (isZoomed == true)
        {
           cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom, Time.deltaTime * smooth);
            //lensAberrations.setDistortion(-50);

        }
        else if(!isZoomed)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normal, Time.deltaTime * smooth);
            //lensAberrations.setDistortion(0);
        }        
    }


    public void LigaPost(bool simNao)
    {

        
        post.enabled = simNao;
    }





    public void Pausar()
    {
        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            paused = !paused;
            
        }
    }
}




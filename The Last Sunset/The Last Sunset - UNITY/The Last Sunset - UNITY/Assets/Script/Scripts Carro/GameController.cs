﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CinematicEffects;


public class GameController : MonoBehaviour
{
    #region Variaveis
    [SerializeField] Text textoDistancia, textoMusica, textoVelocidade;
    [SerializeField] RectTransform posTextoMusica;
    [SerializeField] Transform carroPos, player, vanPolicia;
    [SerializeField] float aumentoDistancia, velocidadeTexto;
    [SerializeField] float tempo = 3;
    [SerializeField] AudioSource aud;
    [SerializeField] Image[] policiaBarra;
    [SerializeField] UnityEngine.Rendering.PostProcessing.PostProcessLayer post;
    [SerializeField] Image piscaAHUD, piscaVHUD, blur;
    [SerializeField] GameObject painelPause, painelGameover;
    LensAberrations lensAberrations;
    Bloom bloom;
    Camera cam;
    Vector3 posInicial, textoPosInicial;
    public float distancia;
    public float zoom = 90;
    public float normal = 60;
    float smooth = 5, contador;
    bool isZoomed;
    public static bool paused, tocandoFita;
    bool sireneHUD, GameOver, umavez;
    #endregion

    #region Singleton

    public static GameController instancia;

    void Awake()
    {
        if (instancia == null) instancia = this;
        else if (instancia != this) Destroy(this.gameObject);
    }

    #endregion


    private void Start()
    {
        isZoomed = false;
        paused = false;
        tocandoFita = false;
        contador = 10;
        umavez = false;
        GameOver = false;
        sireneHUD = true;
        cam = Camera.main;
        bloom = cam.GetComponent<Bloom>();
        posInicial = carroPos.position;

        textoPosInicial = textoMusica.transform.localPosition;
        GameObject.FindGameObjectWithTag("Music").GetComponent<Musicmenu>().StopMusic();    

    }

    private void OnLevelWasLoaded(int level)
    {
        
    }

    private void Update()
    {
        distancia = Mathf.Round(Vector3.Distance(posInicial, carroPos.position) * aumentoDistancia);
        textoDistancia.text = distancia.ToString();
        textoVelocidade.text =  ((int) Veiculo.Velocidade).ToString() + "KM/h";

        textoMusica.text = aud.clip.name.ToString();
        textoMusica.transform.localPosition += new Vector3(-velocidadeTexto * Time.deltaTime, 0, 0);

        if (textoMusica.transform.localPosition.x < textoPosInicial.x - textoMusica.text.Length * 15)
        {
            textoMusica.transform.localPosition = textoPosInicial;
        }

        for (int i = 0; i < 2; i++)
        {
            policiaBarra[i]. fillAmount = 1 - ((Mathf.Abs(player.position.z) - Mathf.Abs(vanPolicia.position.z) - 10) / 500);
            if (i >= 2)
                i = 0;
        }

        if (policiaBarra[0].fillAmount >= 0.5 && piscaVHUD.enabled)
        {
            piscaAHUD.enabled = false;
        }
        else if (policiaBarra[0].fillAmount >= 0.5)
            piscaAHUD.enabled = true;

        
        if (policiaBarra[0].fillAmount >= 0.5 && sireneHUD)
        {
            StartCoroutine(Pisca());
            sireneHUD = false;
        }




        if (Input.GetKeyDown(KeyCode.Escape) && !GameOver)
        {
            Pausar(false);
        }

        
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

    public void FitaTocando(bool v)
    {
        tocandoFita = v;
    }



    public void Pausar(bool gameover)
    {
        paused = !paused;

        if (paused)
        {
            
            blur.enabled = true;
            if (gameover)
            {


                painelGameover.SetActive(true);
                GameOver = true;

                if (!umavez)
                {
                    StartCoroutine(Morreu());
                    umavez = true;
                }

                /*
                 * for (float x = 100f; x >= 0f; x -= 0.1f * Time.DeltaTime)
                {
                    Debug.Log(x);
                    Time.timeScale = x;
                }
                */
            }
            else { painelPause.SetActive(true);
                Time.timeScale = 0f;
            }
        }
        else
        {
            Time.timeScale = 1;
            painelPause.SetActive(false);
            blur.enabled = false;
        }




    }

    IEnumerator Pisca()
    {
        
        yield return new WaitForSeconds(0.3f);
        piscaVHUD.enabled = !piscaVHUD.enabled;
        if (policiaBarra[0].fillAmount <= 0.49)
        {
            sireneHUD = true;
            piscaAHUD.enabled = false;
            piscaVHUD.enabled = false;
        }
        else
            StartCoroutine(Pisca());
    }

    IEnumerator Morreu()
    {
        yield return new WaitForSeconds(0.02f);
        if (--contador > 0)
        {
            Time.timeScale -= 0.1f;
            StartCoroutine(Morreu());
        }
        else Time.timeScale = 0;
    }
}




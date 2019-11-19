using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Veiculo : MonoBehaviour
{
    public Transform[] MeshRodas;
    public WheelCollider[] ColisorRodas;
    private GameObject mafia;
    private Rigidbody corpoRigido;
    private float angulo, direcao;
    [SerializeField] float Velocidade, pesoVeiculo = 1500;
    [SerializeField] float limiteLateral;
    [SerializeField] float rotationZ, sensitivityZ, curva;
    [SerializeField] float carregaNitro;
    bool perdaVelo = false;

    public Image BarraNitro;
    public Image Velocimetro;
    [Range(20, 500)]
    public float NitroCheio = 100, velocidadeNitro = 250;
    [HideInInspector]
    public float NitroAtual;
    private bool semNitro = false;
    private float velocidadeCaminhando, velocidadeCorrendo;
    [SerializeField] float tempo;




    void Start()
    {
       
        corpoRigido = GetComponent<Rigidbody>();
        corpoRigido.mass = pesoVeiculo;
        
    }
    void Update()
    {

        if (mafia == null)
            mafia = GameObject.FindWithTag("Mafia");

        corpoRigido.velocity = transform.forward * Velocidade;
        Velocidade += 1f* Time.deltaTime;
        

        direcao = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Horizontal") > 0.7f || Input.GetAxis("Horizontal") < -0.7f)
        {
            angulo = Mathf.Lerp(angulo, direcao, Time.deltaTime * 4);
        }
        else
        {
            angulo = Mathf.Lerp(angulo, direcao, Time.deltaTime * 2);
        }


        colisaoLateral();
        SistemaDeNitro();
        AplicaBarra();

        if(Velocidade >= 180)
        {
            Velocidade = 180;
        }

    }
    void FixedUpdate()
    {
        lockedRotation();
        ColisorRodas[0].steerAngle = angulo * 40;
        ColisorRodas[1].steerAngle = angulo * 40;
        //
        
        

        for (int x = 0; x < ColisorRodas.Length; x++)
        {
            Quaternion quat;
            Vector3 pos;
            ColisorRodas[x].GetWorldPose(out pos, out quat);
            MeshRodas[x].position = pos;
            MeshRodas[x].rotation = quat;
        }

        
    }


    public void colisaoLateral()
    {
        
        
          if (Mathf.Abs(transform.position.x) > limiteLateral)
          {

            transform.position = new Vector3(limiteLateral * Mathf.Sign(transform.position.x), transform.position.y, transform.position.z);
            transform.localEulerAngles = new Vector3(0, 0, 0);
            
             
                
             perdeVelAcostamento();   
               
          }
        

    }

    void perdeVelAcostamento()
    {
        perdaVelo = true;
        if (perdaVelo == true && Velocidade > 30f)
        {
            Velocidade -= 2f;
            if (Velocidade == 30f)
            {


                perdaVelo = false;


            }

            if (perdaVelo == false)
            {

                Velocidade = 30f;

            }
        }
    }

    void ColisaoCarro()
    {
        perdaVelo = true;
        if (perdaVelo == true && Velocidade > 20f)
        {
            Velocidade -= 3f;
            if (Velocidade == 20f)
            {


                perdaVelo = false;


            }

          
        }
    }


    void lockedRotation()
    {
        rotationZ += Input.GetAxis("Horizontal") * sensitivityZ;
        rotationZ = Mathf.Clamp(rotationZ, -45, 45);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationZ, transform.localEulerAngles.z) * Time.deltaTime * curva;
    }



    void SistemaDeNitro()
    {
        float multEuler = ((1 / NitroCheio) * NitroAtual);
        if (NitroAtual >= NitroCheio)
        {
            NitroAtual = NitroCheio;
        }
        else
        {
            NitroAtual += Time.deltaTime * ( Velocidade / carregaNitro) * Mathf.Pow(2.718f, multEuler);
        }
        if (NitroAtual <= 0)
        {
            NitroAtual = 0;
            semNitro = true;
        }
        if (semNitro == true && NitroAtual >= (NitroCheio * 0.15f))
        {
            semNitro = false;
        }
        if (Input.GetKey(KeyCode.Space) && semNitro == false)
        {

            NitroAtual -= Time.deltaTime * (Velocidade / 3) * Mathf.Pow(2.718f, multEuler);
            Velocidade += 0.1f;
            GameController.instancia.nitro(1f);
            GameController.instancia.zom(true);
           // GameController.instancia.LigaPost(0);
        }
        else
        {
             // GameController.instancia.LigaPost(3);
              GameController.instancia.nitro(0f);
              GameController.instancia.zom(false);
                
        }

    }

    void AplicaBarra()
    {
        BarraNitro.fillAmount = ((1 / NitroCheio) * NitroAtual);
        Velocimetro.fillAmount = Velocidade  /tempo;
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Veiculo"))
        {
            
            ColisaoCarro();

        }

        
           
    }

}
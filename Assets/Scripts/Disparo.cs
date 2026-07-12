using JetBrains.Annotations;
using UnityEngine;
using TMPro; // NUEVO: Necesario para modificar textos en pantalla
using System.Collections; // NUEVO: Necesario para las Corrutinas (esperas de tiempo)

public class Disparo : MonoBehaviour
{
    public Camera camara;
    public int dano = 2;
    public float alcance = 100f;
    public float cadencia = 0.5f;
    public AudioClip sonidoDisparo;
    public GameObject destello;
    public Light PuntDisparo;
    
    // Variables para munición
    public int municionMax = 10;
    private int municionActual;
    public float tiempoRecarga = 2f;
    private bool recargando = false;
    public TextMeshProUGUI textoMunicion; // Aquí arrastraremos el texto del Canvas

    private AudioSource fuente;
    private float proximo = 0f;

    void Start()
    {
        fuente = GetComponent<AudioSource>();
        if (destello != null) destello.SetActive(false);
        if (PuntDisparo != null) PuntDisparo.intensity = 0;

        // Iniciar con cargador lleno
        municionActual = municionMax;
        ActualizarTextoMunicion();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        //Si está recargando, no permitimos hacer nada más
        if (recargando) return;

        // Botón de recarga manual o recarga automática si no hay balas
        if (Input.GetKeyDown(KeyCode.R) || (municionActual <= 0 && Input.GetMouseButtonDown(0)))
        {
            if (municionActual < municionMax) // Solo recarga si no está lleno
            {
                StartCoroutine(Recargar());
            }
            return;
        }

        // Modificado para comprobar si hay balas
        if (Input.GetMouseButtonDown(0) && Time.time >= proximo && municionActual > 0)
        {
            proximo = Time.time + cadencia;
            disparo();
            
            // Restar bala
            municionActual--;
            ActualizarTextoMunicion();
        }
    }

    void disparo()
    {
        if (sonidoDisparo != null) fuente.PlayOneShot(sonidoDisparo);
        if (destello != null && PuntDisparo != null)
        {
            destello.SetActive(true);
            PuntDisparo.intensity = 15;
            Invoke("ApagarMuzzle", 0.05f);
        }

        Ray ray = camara.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, alcance))
        {
            Vida v = hit.collider.GetComponentInParent<Vida>();
            if (v != null) v.recibirDano(dano);
        }
    }

    void ApagarMuzzle()
    {
        if (destello != null && PuntDisparo != null)
        {
            destello.SetActive(false);
            PuntDisparo.intensity = 0;
        }
    }

    // Función con espera para la recarga
    IEnumerator Recargar()
    {
        recargando = true;
        if (textoMunicion != null) textoMunicion.text = "Recargando...";
        
        yield return new WaitForSeconds(tiempoRecarga); // Espera 2 segundos
        
        municionActual = municionMax;
        recargando = false;
        ActualizarTextoMunicion();
    }

    // Actualizar la interfaz
    void ActualizarTextoMunicion()
    {
        if (textoMunicion != null)
            textoMunicion.text = "Balas: " + municionActual + " / " + municionMax;
    }
}
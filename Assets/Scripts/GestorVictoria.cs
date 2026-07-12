using UnityEngine;
using TMPro;

public class GestorVictoria : MonoBehaviour
{
    [Header("Interfaz")]
    public GameObject panelVictoria;
    public TextMeshProUGUI textoContador;
    
    [Header("Sonido")] // NUEVO: Sección para el audio
    public AudioSource audioSource; // Arrastra el AudioSource aquí
    public AudioClip sonidoVictoria; // Arrastra tu archivo de sonido aquí
    
    private bool haHabidoEnemigos = false;

    void Start()
    {
        if (panelVictoria != null) panelVictoria.SetActive(false);
    }

    void Update()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        GameObject[] enemigos2 = GameObject.FindGameObjectsWithTag("Enemigov.2");
        if (textoContador != null) textoContador.text = "Enemigos: " + enemigos.Length+"\nEnemigos \ntoxicos: "+enemigos2.Length;
        if (enemigos.Length > 0) haHabidoEnemigos = true;

        if (haHabidoEnemigos && enemigos.Length == 0)
        {
            GanarNivel();
        }
    }

    void GanarNivel()
    {
        if (panelVictoria != null) panelVictoria.SetActive(true);
        
        // NUEVO: Reproducir sonido de victoria
        if (audioSource != null && sonidoVictoria != null)
        {
            audioSource.PlayOneShot(sonidoVictoria);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        enabled = false; 
    }
}
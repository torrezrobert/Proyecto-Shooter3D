using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // para la imagen de daño y barra de vida

public class Vida : MonoBehaviour
{
    public int vidaMax = 3;
    public bool esJugador = false;
    private int vidaActual;

    // NUEVO: Variables de Interfaz (UI)
    public GameObject panelGameOver; 
    public Image imagenDano; 
    private Color colorDano = new Color(1f, 0f, 0f, 0.5f); // Rojo transparente
    public float velDesvanecimientoDano = 3f;
    
    [Header("Interfaz Barra de Vida")]
    public Image imagenBarraVida; // BARRA DE VIDA

    void Start()
    {
        vidaActual = vidaMax;
        if (panelGameOver != null) panelGameOver.SetActive(false); // Asegura que el panel empiece apagado
        ActualizarBarra(); // Llena la barra al iniciar
    }

    public void recibirDano(int cantidad)
    {
        vidaActual -= cantidad;
        ActualizarBarra(); // Actualiza la barra al recibir daño
        
        //  pantallazo rojo si es el jugador
        if (esJugador && imagenDano != null) 
        {
            imagenDano.color = colorDano;
        }

        if (vidaActual <= 0) Morir();
    }

    void Morir()
    {
        if (esJugador)
        {
            //  menú de Game Over
            if (panelGameOver != null) panelGameOver.SetActive(true);
            Time.timeScale = 0f; // Pausa el juego
            Cursor.lockState = CursorLockMode.None; // Libera el ratón
            Cursor.visible = true; // Muestra el ratón para poder dar clic
        }
        else
        {
            Destroy(gameObject); // Enemigo muere
        }
    }

    public int VidaActual()
    {
        return vidaActual;
    }

    void Update()
    {
        // Hacer que la imagen roja desaparezca poco a poco
        if (esJugador && imagenDano != null && imagenDano.color.a > 0)
        {
            imagenDano.color = Color.Lerp(imagenDano.color, Color.clear, velDesvanecimientoDano * Time.deltaTime);
        }
    }

    //  Esta función la conectaremos al Botón "Reintentar"
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f; // Despausamos el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void curar(int cantidad)
    {
        // Sumamos la vida
        vidaActual += cantidad;

        // Aseguramos que no pase de la vida máxima
        if (vidaActual > vidaMax)
        {
             vidaActual = vidaMax;
        }
    
        // Refresca la barra de vida al curarse
        ActualizarBarra(); 
    }

    // Esta es la función que actualiza el dibujo de la barra
    void ActualizarBarra()
    {
        if (imagenBarraVida != null)
        {
            // Convierte la vida en un decimal entre 0 y 1 para el Fill Amount
            imagenBarraVida.fillAmount = (float)vidaActual / vidaMax;
        }
    }
}
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    [Header("Configuración del Generador")]
    public GameObject enemigoPrefab; // Aquí pondremos el "molde" del monstruo
    public float tiempoAparicion = 5f; // Cada cuántos segundos aparece uno nuevo
    
    [Header("Lugares de Aparición")]
    public Transform[] puntosDeGeneracion; // Una lista de puntos donde pueden aparecer

    private float temporizador = 0f;

    void Update()
    {
        // Sumamos el tiempo real que va transcurriendo en el juego
        temporizador += Time.deltaTime;

        // Si el tiempo acumulado supera nuestro límite, creamos un enemigo
        if (temporizador >= tiempoAparicion)
        {
            CrearEnemigo();
            temporizador = 0f; // Reiniciamos el cronómetro
        }
    }

    void CrearEnemigo()
    {
        // Seguro contra errores: revisa que no falte el prefab o los puntos
        if (enemigoPrefab == null || puntosDeGeneracion.Length == 0) return;

        // Elige un número al azar basado en la cantidad de puntos que tengamos
        int puntoAleatorio = Random.Range(0, puntosDeGeneracion.Length);
        
        // Obtenemos la posición exacta del punto que ganó el sorteo
        Transform puntoElegido = puntosDeGeneracion[puntoAleatorio];

        // "Instantiate" es la palabra mágica en Unity para crear un objeto de la nada
        Instantiate(enemigoPrefab, puntoElegido.position, puntoElegido.rotation);
    }
}
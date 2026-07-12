using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    // Guarda todas las luces (Point Light) que están dentro de esta farola.
    private Light[] luces;

    // Guarda la intensidad original de cada luz para restaurarla después del parpadeo.
    private float[] intensidadOriginal;

    [Header("Tiempo entre parpadeos")]

    // Tiempo mínimo que esperará antes de volver a parpadear.
    public float tiempoMin = 8f;

    // Tiempo máximo que esperará antes de volver a parpadear.
    public float tiempoMax = 20f;

    void Start()
    {   
        luces = GetComponentsInChildren<Light>();

        Debug.Log("Luces encontradas: " + luces.Length);
        // Busca automáticamente todas las luces que están dentro de este objeto.
        // No importa si son 1, 2, 4 o más.
        luces = GetComponentsInChildren<Light>();

        // Crea un arreglo del mismo tamaño que la cantidad de luces encontradas.
        intensidadOriginal = new float[luces.Length];

        // Guarda la intensidad original de cada luz.
        for (int i = 0; i < luces.Length; i++)
            intensidadOriginal[i] = luces[i].intensity;

        // Inicia el proceso de parpadeo.
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        // Se ejecutará para siempre mientras exista la farola.
        while (true)
        {
            // Espera un tiempo aleatorio antes del siguiente parpadeo.
            yield return new WaitForSeconds(Random.Range(tiempoMin, tiempoMax));

            // Decide cuántas veces parpadeará esta vez.
            // Puede ser entre 2 y 5 veces.
            int veces = Random.Range(2, 6);

            // Repite el efecto de parpadeo.
            for (int i = 0; i < veces; i++)
            {
                // Baja la intensidad de todas las luces
                // simulando un fallo eléctrico.
                foreach (Light l in luces)
                    l.intensity = Random.Range(0.2f, 0.8f);

                // Mantiene la intensidad baja unas milésimas.
                yield return new WaitForSeconds(Random.Range(0.03f, 0.08f));

                // Devuelve cada luz a su intensidad original.
                for (int j = 0; j < luces.Length; j++)
                    luces[j].intensity = intensidadOriginal[j];

                // Espera un instante antes del siguiente parpadeo.
                yield return new WaitForSeconds(Random.Range(0.03f, 0.08f));
            }
        }
    }
}

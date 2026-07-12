 using UnityEngine;
using System.Collections; //  para usar los temporizadores (corrutinas)

public class Botiquin : MonoBehaviour
{
    public int cantidadCura = 2;
    public float tiempoReaparicion = 30f; // segundos que tarda en volver

    private Collider miColisionador;
    private Renderer[] misRenderers; // arreglo ya que el botiquin tiene varias partes visuales

    void Start()
    {
        // referencias al inicio para no buscarlas cada vez
        miColisionador = GetComponent<Collider>();
        misRenderers = GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider jug) //para cuando el jugador se acerque al botiquin
    {
        if (jug.CompareTag("Player"))
        {
            Vida v = jug.GetComponent<Vida>();
            if (v != null)
            {
                if (v.VidaActual() < v.vidaMax)// esto hace que no curecuando tiene la vida al maximo
                {
                    v.curar(cantidadCura);
                    // iniciamos el temporizador para que vuelva a aparecer
                    StartCoroutine(RecargarBotiquin());
                }
                
            }
        }
    }

    //  corrutina que maneja el tiempo sin congelar el juego
    IEnumerator RecargarBotiquin()
    {
        // se apaga el colisionador para evitar curar 2 veces
        miColisionador.enabled = false;
        
        // apagamos todos los gráficos para que se vuelva invisible(foreach recorre los elementos de unarreglo uno por uno)
        foreach (Renderer r in misRenderers)
        {
            r.enabled = false;
        }

        // aqui esperamos el tiempo de reaparicion(yield hace que la ejecucion pare por el tiempo y continue lo demas)
        yield return new WaitForSeconds(tiempoReaparicion);

        //volvemos a encender todo
        miColisionador.enabled = true;
        foreach (Renderer r in misRenderers)
        {
            r.enabled = true;
        }
    }
}

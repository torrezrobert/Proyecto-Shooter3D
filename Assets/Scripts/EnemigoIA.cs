using UnityEngine;
using UnityEngine.AI;

public class EnemigoIA : MonoBehaviour
{
    private Transform jugador; //coordenadas del jugador
    private NavMeshAgent agente; // enemigo

    [Header("Estadísticas de Ataque")]
    public float cadenciaDisparo = 2f; 
    private float proximoDisparo = 0f; 
    public int dano = 1; 
    public float distanciaAtaque = 15f; 

    [Header("Efectos")]
    public LineRenderer lineaDisparo; // linea que traza el disparo

    // Referencia al sonido y al componente
    public AudioSource audioSource; 
    public AudioClip sonidoDisparo;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        
        // Si no asignaste el AudioSource manualmente en el inspector, lo busca solo
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        
        GameObject objJugador = GameObject.FindGameObjectWithTag("Player");//el objetivo es aquel con tag Player
        if (objJugador != null) jugador = objJugador.transform;// coordenadas del jugador
    }

    void Update()
    {
        if (jugador == null || Time.timeScale == 0f) return;

        agente.SetDestination(jugador.position);// asignamos la posicion del jugador al enemigo para que lo persiga
        float distancia = Vector3.Distance(transform.position, jugador.position);
        transform.LookAt(new Vector3(jugador.position.x, transform.position.y, jugador.position.z));

        if (distancia <= distanciaAtaque && Time.time >= proximoDisparo)
        {
            proximoDisparo = Time.time + cadenciaDisparo;
            Disparar();
        }
    }

    void Disparar()
    {
        // Reproducir sonido
        if (audioSource != null && sonidoDisparo != null)
        {
            // playone... permite que el sonido se reproduzca sin interrumpir otros anteriores
            audioSource.PlayOneShot(sonidoDisparo);
        }

        Vector3 direccion = (jugador.position - transform.position).normalized;
        Vector3 origenRayo = transform.position + Vector3.up * 0.5f; 

        if (lineaDisparo != null)
        {
            lineaDisparo.enabled = true;
            lineaDisparo.SetPosition(0, origenRayo);
        }

        if (Physics.Raycast(origenRayo, direccion, out RaycastHit hit, distanciaAtaque))
        {
            if (lineaDisparo != null) lineaDisparo.SetPosition(1, hit.point);
            if (hit.collider.CompareTag("Player")) //comparamos si es player
            {
                Vida v = hit.collider.GetComponentInParent<Vida>();
                PrimeraPersona pp=hit.collider.GetComponentInParent<PrimeraPersona>();
                
                if (v != null)
                {
                    v.recibirDano(dano);
                    // verifica si el que ataca es la v2 del enemigo con el tag
                    if(agente.CompareTag("Enemigov.2" )&& pp!=null)pp.CambiarVel(4f);
                
                }
                
            }

            
            
        }
        else
        {
            if (lineaDisparo != null) lineaDisparo.SetPosition(1, origenRayo + (direccion * distanciaAtaque));
        }

        Invoke("ApagarRayo", 0.1f);
    }

    void ApagarRayo()
    {
        if (lineaDisparo != null) lineaDisparo.enabled = false;
    }
}
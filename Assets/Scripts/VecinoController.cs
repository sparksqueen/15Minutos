using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VecinoController : MonoBehaviour
{
    public Transform puntoEntrada;
    public Transform puntoCocina;
    public Transform puntoSalida;

    public float velocidad = 2f;
    public string tagObjetosCocina = "KitchenObject";

    public GameObject burbujaDialogo;
    public TMP_Text textoDialogo;

    public List<string> frasesDialogo = new List<string>
    {
        "¡Hola vecino!",
        "¿Tenés un poco de azúcar?",
        "Gracias... voy a buscar algo...",
        "¡Esta cocina es un desastre!",
        "Mejor me voy, ¡qué mugre!"
    };

    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        burbujaDialogo.SetActive(false);
        StartCoroutine(AparecerATiempo());
    }

    IEnumerator AparecerATiempo()
    {
        yield return new WaitForSeconds(300); // 5 minutos

        // Para pruebas: podés comentar la línea de arriba y usar esto:
        // yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));

        gameObject.SetActive(true);
        transform.position = puntoEntrada.position;
        yield return CaminarA(puntoCocina.position);

        yield return MostrarDialogo();

        DesordenarObjetos();

        yield return new WaitForSeconds(1f);

        yield return CaminarA(puntoSalida.position);

        Destroy(gameObject); // O SetActive(false)
    }

    IEnumerator CaminarA(Vector3 destino)
    {
        Vector3 dir;
        while (Vector3.Distance(transform.position, destino) > 0.1f)
        {
            dir = (destino - transform.position).normalized;

            // Movimiento con detección de colisiones
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 0.3f);
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                // Si choca, intenta bordear
                Vector3 desvio = Vector3.Cross(dir, Vector3.forward).normalized * 0.2f;
                transform.position += (dir + desvio) * velocidad * Time.deltaTime;
            }
            else
            {
                transform.position += dir * velocidad * Time.deltaTime;
            }

            ActualizarAnimacion(dir);
            yield return null;
        }

        ActualizarAnimacion(Vector2.zero);
    }

    void ActualizarAnimacion(Vector2 direccion)
    {
        if (animator == null) return;
        animator.SetFloat("MoveX", direccion.x);
        animator.SetFloat("MoveY", direccion.y);
        animator.SetBool("IsMoving", direccion != Vector2.zero);
    }

    IEnumerator MostrarDialogo()
    {
        burbujaDialogo.SetActive(true);

        foreach (string frase in frasesDialogo)
        {
            textoDialogo.text = "";
            foreach (char letra in frase)
            {
                textoDialogo.text += letra;
                yield return new WaitForSeconds(0.03f);
            }
            yield return new WaitForSeconds(1.2f);
        }

        burbujaDialogo.SetActive(false);
    }

    void DesordenarObjetos()
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(tagObjetosCocina);
        foreach (GameObject obj in objetos)
        {
            if (Random.value > 0.5f) // Desordena solo algunos
            {
                Vector2 offset = Random.insideUnitCircle * 0.5f;
                obj.transform.position += (Vector3)offset;
            }
        }
    }
}

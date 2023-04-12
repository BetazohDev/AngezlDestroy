/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f; // velocidad de movimiento del enemigo
    public float attackDistance = 1f; // distancia mínima para atacar al jugador
    public int damage = 10; // daño que hace el enemigo al jugador

    private Animator animator; // componente Animator del enemigo
    private Rigidbody2D rb2d; // componente Rigidbody2D del enemigo
    private Transform target; // referencia al objeto del jugador
    private bool isAttacking = false; // indicador de si el enemigo está atacando o no
    private bool isHurt = false; // indicador de si el enemigo ha sido herido

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform; // encontrar el objeto del jugador por la etiqueta
    }

    private void Update()
    {
        if (!isHurt) // si no está herido, el enemigo puede moverse y atacar
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position); // distancia al jugador

            if (distanceToTarget <= attackDistance && !isAttacking) // si el jugador está dentro de la distancia de ataque
            {
                isAttacking = true; // el enemigo está atacando
                StartCoroutine(Attack()); // atacar al jugador después de un tiempo
            }
            else if (distanceToTarget > attackDistance) // si el jugador está fuera de la distancia de ataque
            {
                rb2d.velocity = new Vector2(target.position.x - transform.position.x, 0).normalized * speed; // moverse hacia el jugador
                animator.SetBool("isWalking", true); // activar la animación de caminar
            }
            else // el enemigo está en la distancia de ataque, pero ya está atacando
            {
                rb2d.velocity = Vector2.zero; // detener el movimiento
                animator.SetBool("isWalking", false); // desactivar la animación de caminar
            }
        }
    }

    private IEnumerator Attack()
    {
        animator.SetBool("isWalking", false); // desactivar la animación de caminar
        animator.SetTrigger("Attack"); // activar la animación de ataque
        target.GetComponent<PlayerController>().TakeDamage(damage); // causar daño al jugador
        yield return new WaitForSeconds(1f); // tiempo de espera antes de volver a atacar
        isAttacking = false; // el enemigo puede atacar de nuevo
    }

    public void TakeDamage()
    {
        isHurt = true; // el enemigo está herido
        animator.SetBool("isWalking", false); // desactivar la animación de caminar
        animator.SetTrigger("Hurt"); // activar la animación de dolor
        rb2d.velocity = Vector2.zero; // detener el movimiento
        Destroy(gameObject, 1f); // destruir el objeto del enemigo después de un tiempo
    }
}
*/
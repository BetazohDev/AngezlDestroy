using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; 
    public bool atacando;
    public bool patada;
    public Animator ani;

    private float Gravedad;
    private float Ypos;
    private float Ypos_Piso;
    public bool inground;
    public bool saltando;
    public int Fases;
    public float AlturaSalto;
    public float PotenciaSalto;
    public float Fallen;
	
    public SpriteRenderer spr;
    private float delay;
    private int sky;
    public Collider2D boundsCollider;
    private Vector3 lastPosition;
	
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
	  lastPosition = transform.position;
    }

    public void Mover()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !atacando && !saltando && inground)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            ani.SetBool("run", true);
        }
        else
        {
            ani.SetBool("run", false); // Falta ";" al finalizar la línea
        }
        if (Input.GetKey(KeyCode.DownArrow) && !atacando && !saltando && inground)
        {
            transform.Translate(Vector3.up * -speed * Time.deltaTime);
            ani.SetBool("run", true);
        }
        if (Input.GetKey(KeyCode.RightArrow) && !atacando)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime); // Corregido "up" a "right" para moverse a la derecha
            transform.rotation = Quaternion.Euler(0, 0, 0);
            ani.SetBool("run", true);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !atacando)
        {
            transform.Translate(Vector3.left * -speed * Time.deltaTime); // Corregido "up" a "right" para moverse a la izquierda
            transform.rotation = Quaternion.Euler(0, 180, 0);
            ani.SetBool("run", true);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.C) && !saltando && inground) // Corregido KeyCode.KeyCode.C a KeyCode.C
        {
            Ypos_Piso = transform.position.y; // Corregido "positions" a "position"
            saltando = true;
            inground = false;
            ani.SetBool("jump", true); // Corregido para activar la animación de salto
        }
        if (saltando)
        {
            switch (Fases)
            {
                case 0:

                    Gravedad = AlturaSalto;
                    Fases = 1;

                    break;

                case 1: 

                    if (Gravedad > 0)
                    {
                        Gravedad -= PotenciaSalto * Time.deltaTime; 
                    }
                    else
                    {
                        Fases = 2; // Corregido ":" a ";"
                    }


                    break;
            }
        }
    }

    void SetTransformY(float n) // Corregido "SetTransform" a "SetTransformY"
    {
        transform.position = new Vector3(transform.position.x, n, transform.position.z); // Corregido "positions" a "position"
    }

    public void Detector_Piso()
{
    if (!saltando && !atacando)
    {
        sky = 0;

        if (Ypos == Ypos_Piso)
        {
            inground = true;
        }
        ani.SetBool("jump", false);
    }
    else
    {
        ani.SetBool("jump", true);
    }
    if (inground)
    {
        Gravedad = 0;
        Fases = 0;

    }
    else
    {
        switch (Fases)
        {
            case 2:

                Gravedad = 0;
                Fases = 3;

                break;

            case 3:
                if (transform.position.y >= Ypos_Piso)
                {
                    if (Gravedad > -10)
                    {
                        Gravedad -= AlturaSalto / Fallen * Time.deltaTime;
                    }
                    else
                    {
                        saltando = false;
                        inground = true;
                        SetTransformY(Ypos_Piso);
                        Fases = 0;
                    }
                }

                break;
        }
    }
    if (!inground && !patada)
    {
        if (transform.position.y > Ypos)
        {
            ani.SetFloat("gravedad", 1);

        }
        if (transform.position.y < Ypos)
        {
            ani.SetFloat("gravedad", 0);

            switch (sky)
            {
                case 0:
                    ani.Play("Base Layer Jump", 0, 0);
                    sky++;
                    break;
            }
        }
    }

    Ypos = transform.position.y;
}
public void Finish_Ani()
{
    atacando = false;
    patada = false; 
}

public void Golpe()
{
    if (Input.GetKeyDown(KeyCode.X))
    {
        delay = 1;
        if (!saltando)
        {
            if (!atacando)
            {
                atacando = true;
                ani.SetTrigger("hit");
            }
        }
    }
    if (delay > 0)
    {
        spr.sortingOrder = 1;
        delay -= 2 * Time.deltaTime;

    }
    else
    {
        spr.sortingOrder = 0;
        delay = 0;
    }
}

// Update is called once per frame
void Update()
{
    Detector_Piso();
    Jump();
    Golpe();    
}

void FixedUpdate()
{
    Mover();
    transform.Translate(Vector3.up * Gravedad * Time.deltaTime);

            Vector3 newPosition = transform.position;
        Vector3 moveDirection = newPosition - lastPosition;

        float halfWidth = GetComponent<Collider2D>().bounds.extents.x;

        if (boundsCollider.bounds.Contains(newPosition)) {
            lastPosition = newPosition;
        } else {
            // Si el personaje se mueve en la dirección de un borde del collider, establece su posición en la posición más cercana al borde.
            if (moveDirection.x > 0) {
                newPosition.x = Mathf.Min(newPosition.x, boundsCollider.bounds.max.x - halfWidth);
            } else if (moveDirection.x < 0) {
                newPosition.x = Mathf.Max(newPosition.x, boundsCollider.bounds.min.x + halfWidth);
            }

            if (moveDirection.y > 0) {
                newPosition.y = Mathf.Min(newPosition.y, boundsCollider.bounds.max.y - halfWidth);
            } else if (moveDirection.y < 0) {
                newPosition.y = Mathf.Max(newPosition.y, boundsCollider.bounds.min.y + halfWidth);
            }

            transform.position = newPosition;
            lastPosition = transform.position;
        }
}
    
}

/*

public void TakeDamage(int damage)
{
    currentHealth -= damage; // restar el daño a la salud actual del jugador

    if (currentHealth <= 0) // si la salud actual del jugador llega a cero
    {
        Die(); // llamar a la función Die para terminar el juego
    }
}

private void Die()
{
    // aquí se podría implementar alguna animación o efecto visual para la muerte del jugador
    Debug.Log("Game Over"); // mostrar un mensaje en la consola
    Time.timeScale = 0; // detener el tiempo del juego
}*/
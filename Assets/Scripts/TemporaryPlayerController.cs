using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is not the final Controller it as been taken from Labyrinth 2. It is temporary there to let other
/// works in their part while the official PlayerController is not done.
/// </summary>
public class TemporaryPlayerController : MonoBehaviour
{
    private float speed = 3.0f;    // Vitesse de déplacement vers l'avant/arrière
    private float turnSpeed = 60.0f;  // Vitesse de rotation
    public float strafeSpeed = 5.0f;  // Vitesse de déplacement latéral (gauche/droite)

    private float horizontalInput;
    private float forwardInput;

    private Rigidbody rb;  // Référence au Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtenir le Rigidbody du joueur
    }

    // Utiliser FixedUpdate pour les interactions physiques
    private void FixedUpdate()
    {
        // Obtenir les entrées utilisateur
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Déplacer l'objet en utilisant le Rigidbody pour respecter la physique
        Vector3 move = transform.forward * forwardInput * speed * Time.fixedDeltaTime;  // Mouvement vers l'avant/en arrière
        Vector3 strafe = transform.right * horizontalInput * strafeSpeed * Time.fixedDeltaTime;  // Mouvement latéral (gauche/droite)

        // Appliquer le mouvement
        // Rigidbody.MovePosition est utilisé pour déplacer un objet tout en respectant la physique d'Unity.
        rb.MovePosition(rb.position + move + strafe);

        // Rotation directe du joueur sur l'axe Y (rotation en fonction de l'entrée horizontale)
        Vector3 rotation = new Vector3(0f, horizontalInput * turnSpeed * Time.fixedDeltaTime, 0f);

        // Appliquer la rotation directement à l'axe Y
        rb.transform.Rotate(rotation);
    }
}


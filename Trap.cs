using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CharacterMove characterMove = other.GetComponent<CharacterMove>();

            if (characterMove != null)
            {
                characterMove.Trapped();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CharacterMove characterMove = other.GetComponent<CharacterMove>();

            if (characterMove != null)
            {
                characterMove.UnTrapped();
            }
        }
    }
}

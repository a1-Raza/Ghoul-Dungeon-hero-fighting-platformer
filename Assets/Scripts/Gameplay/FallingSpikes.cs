using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    [SerializeField] bool fallWhenPlayerNear = true;
    [SerializeField] float xDistanceToPlayerToFall = 1;
    [SerializeField] float yDistanceToPlayerToFall = 10;
    [SerializeField] float despawnTime = 3;

    Rigidbody2D spikesRigidbody;
    Collider2D spikesCollider;
    GameObject player;

    bool isFalling = false;

    // Start is called before the first frame update
    void Start()
    {
        spikesRigidbody = GetComponent<Rigidbody2D>();
        spikesCollider = GetComponent<Collider2D>();
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (fallWhenPlayerNear)
        {
            var playerIsCloseEnough = (Mathf.Abs(transform.position.x - player.transform.position.x) <= xDistanceToPlayerToFall) && (transform.position.y - yDistanceToPlayerToFall <= player.transform.position.y && player.transform.position.y <= transform.position.y);
            if (playerIsCloseEnough)
            {
                isFalling = true;
            }
        }

        HandleFalling();

        if (spikesRigidbody.velocity.y != 0)
        {
            StartCoroutine(DespawnAfterTime());
        }
    }

    void HandleFalling()
    {
        if (isFalling)
        {
            spikesRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            spikesRigidbody.bodyType = RigidbodyType2D.Static;
        }
    }

    IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}

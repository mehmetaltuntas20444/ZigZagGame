using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{

    //start the courutine when player triggers
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            TileManager.Instance.SpawnTiles();
            StartCoroutine(FallingDown());
        }
    }


    //after 1 second player pass a tile set that tile kinematic to false and make them  start falling down. and also after one second set their kinematic true again to use them again later on and set their active state to false
    IEnumerator FallingDown()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(1);
        switch (gameObject.name)
        {
            case "LeftTile":
                TileManager.Instance.LeftTiles.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
            case "ForwardTile":
                TileManager.Instance.ForwardTiles.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
        }
    }
}

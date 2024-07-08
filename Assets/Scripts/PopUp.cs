using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public GameObject Object;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player")
        {
            Object.transform.GetChild(0).gameObject.SetActive(true);
            Object.transform.GetChild(1).gameObject.SetActive(false);

            if (Object.name == "Checkpoint" || Object.name == "Finish"){
                Object.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "Player")
        {
            Object.transform.GetChild(0).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
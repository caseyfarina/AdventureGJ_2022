using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject Player;
    public GameObject Teleport;
    public GameObject SecondTeleport;
    public GameObject ThirdTeleport;
    public GameObject Respawn;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.CompareTag("Teleporter"))
        {
            CharacterController cc = Player.GetComponent<CharacterController>();
            cc.enabled = false;
            Player.transform.position = Teleport.transform.position;
            cc.enabled = true;
        }
        if (Col.gameObject.CompareTag("Teleporter2"))
        {
            CharacterController cc = Player.GetComponent<CharacterController>();
            cc.enabled = false;
            Player.transform.position = SecondTeleport.transform.position;
            cc.enabled = true;
        }
        if (Col.gameObject.CompareTag("Teleporter3"))
        {
            CharacterController cc = Player.GetComponent<CharacterController>();
            cc.enabled = false;
            Player.transform.position = ThirdTeleport.transform.position;
            cc.enabled = true;
        }
        if (Col.gameObject.CompareTag("FallTeleport"))
        {
            CharacterController cc = Player.GetComponent<CharacterController>();
            cc.enabled = false;
            Player.transform.position = Respawn.transform.position;
            cc.enabled = true;
        }
    }
   
}

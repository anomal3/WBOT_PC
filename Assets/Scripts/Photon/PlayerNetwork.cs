using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerNetwork : MonoBehaviour {

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private MonoBehaviour[] playerControlScripts;

    private delegate void UpdateUI(int newHealth);
    private event UpdateUI updateUI;

    private PhotonView photonView;
    public int playerHealth = 2000;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        Initialize();
    }


    private void Initialize()
    {
        if (photonView.isMine)
        {
            //Do surf here
        }
        else
        {
            //Handle functionallity for non-local charter
            //Disable it's camera
            playerCamera.SetActive(false);

            //Disable it's control scripts
            foreach(MonoBehaviour m in playerControlScripts)
            {
                m.enabled = false;
            }
        }


    }
    public int rndBullet = 0;

   
    private void Update()
    {
        rndBullet = Random.Range(250, 390);


        if (!photonView.isMine)
        {
            return;
        }
        //Deduct health by 5
        if(Input.GetKeyDown(KeyCode.E))
        {
            playerHealth -= rndBullet;
        }

    }

    private void OnPhotonViewSerialize(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            //send data
            stream.SendNext(playerHealth);
        }

        else if (stream.isReading)
        {
            playerHealth = (int)stream.ReceiveNext();
        }

    }



}

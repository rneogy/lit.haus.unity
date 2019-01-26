using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    FirefighterController fc;
    ArsonistController ac;

    TMPro.TextMeshProUGUI resource;
    TMPro.TextMeshProUGUI numResource;

    public Cinemachine.CinemachineVirtualCamera cm;

    bool isFirefighter = false;

    void Awake()
    {
        fc = GetComponent<FirefighterController>();
        ac = GetComponent<ArsonistController>();
    }

    void Start() {
        cm.enabled = isLocalPlayer;
        if (isLocalPlayer) {
            resource = GameObject.Find("Resource").GetComponent<TMPro.TextMeshProUGUI>();
            numResource = GameObject.Find("NumResource").GetComponent<TMPro.TextMeshProUGUI>();            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                CmdSetFirefighter(!isFirefighter);
            }

            resource.text = isFirefighter ? "Water:" : "Matches:";
            numResource.text = isFirefighter ? "" + fc.getNumWater() : "" + ac.getNumMatches();
        }

        fc.enabled = isFirefighter;
        ac.enabled = !isFirefighter;
    }

    [Command]
    void CmdSetFirefighter(bool isFirefighter) {
        this.isFirefighter = isFirefighter;
        RpcSetFirefighter(isFirefighter);
    }

    [ClientRpc]
    void RpcSetFirefighter(bool isFirefighter) {
        this.isFirefighter = isFirefighter;
    }
}

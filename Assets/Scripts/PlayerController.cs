using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    FirefighterController fc;
    ArsonistController ac;
    // Start is called before the first frame update
    void Awake()
    {
        fc = GetComponent<FirefighterController>();
        ac = GetComponent<ArsonistController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                fc.enabled = !fc.enabled;
                ac.enabled = !ac.enabled;
            }
        }
    }
}

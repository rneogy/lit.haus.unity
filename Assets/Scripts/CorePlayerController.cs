using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CorePlayerController : NetworkBehaviour
{
    FirefighterController fc;
    ArsonistController ac;

    public RuntimeAnimatorController NinjaAnimator;
    public RuntimeAnimatorController FirefighterAnimator;

    public GameObject cam;

    TMPro.TextMeshProUGUI resource;
    TMPro.TextMeshProUGUI numResource;

    bool isFirefighter = false;

    private Animator animator;
    private Rigidbody2D rb2d;
    private string face = "front";

    void Awake()
    {   
        fc = GetComponent<FirefighterController>();
        ac = GetComponent<ArsonistController>();
        animator = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start() {
        if (isLocalPlayer) {
            resource = GameObject.Find("Resource").GetComponent<TMPro.TextMeshProUGUI>();
            numResource = GameObject.Find("NumResource").GetComponent<TMPro.TextMeshProUGUI>();            
        } else {
            Destroy(cam);
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


        float x = rb2d.velocity.x;
        float y = rb2d.velocity.y;

        if (x < 0) {
            if (y < x) {
                animator.Play("front_walk");
                face = "front";
            } else if (y > -x) {
                animator.Play("back_walk");
                face = "back";
            } else {
                animator.Play("left_walk");
                face = "left";
            }
        } else if (x > 0) {
            if (y < -x) {
                animator.Play("front_walk");
                face = "front";
            } else if (y > x) {
                animator.Play("back_walk");
                face = "back";
            } else {
                animator.Play("right_walk");
                face = "right";
            }
        } else if (y > 0) {
            animator.Play("back_walk");
            face = "back";
        } else if (y < 0) {
            animator.Play("front_walk");
            face = "front";
        } else {
            animator.Play(face + "_idle");
        }
    }

    [Command]
    void CmdSetFirefighter(bool isFirefighter) {
        this.isFirefighter = isFirefighter;
        RpcSetFirefighter(isFirefighter);
    }

    [ClientRpc]
    void RpcSetFirefighter(bool isFirefighter) {
        this.isFirefighter = isFirefighter;
        if (isFirefighter) {
            animator.runtimeAnimatorController = FirefighterAnimator;
        } else {
            animator.runtimeAnimatorController = NinjaAnimator;
        }
    }
}

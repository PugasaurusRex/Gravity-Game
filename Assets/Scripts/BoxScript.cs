using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    PlayerController PC;
    Rigidbody2D Rig;

    // Start is called before the first frame update
    void Start()
    {
        PC = GameObject.Find("Character").GetComponent<PlayerController>();
        Rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PC.Rig.gravityScale == 2)
        {
            Rig.gravityScale = 1;
        }
        else
        {
            Rig.gravityScale = -1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public GameObject Menu;
    Menu MenuScript;

    public GameObject Character;
    PlayerController PC;

    // Game variables
    public TMP_Text score;

    // Map Objects
    public GameObject floor;
    public GameObject roof;

    // Traps


    // Start is called before the first frame update
    void Start()
    {
        PC = Character.GetComponent<PlayerController>();
        MenuScript = Menu.GetComponent<Menu>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(PC.transform.position.x - 12,0);
        if (Time.timeScale != 0 && MenuScript.GameStart)
        {
            score.text = "" + (int)PC.transform.position.x;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Instantiate(floor, new Vector2((int)PC.transform.position.x + 11, -4.5f), Quaternion.identity);
            Instantiate(roof, new Vector2((int)PC.transform.position.x + 11, 4.5f), Quaternion.Euler(new Vector3(0, 0, 180)));
        }
        Destroy(collision.gameObject);
    }
}

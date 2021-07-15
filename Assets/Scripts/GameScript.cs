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
    public int scoreval = 0;

    // Map Objects
    public GameObject floor;
    public GameObject roof;

    // Traps
    public GameObject[] traps;

    public float dmin = 3; // min seconds between traps
    public float dmax = 5; // max seconds between traps

    bool canTrap = true;

    // Start is called before the first frame update
    void Start()
    {
        PC = Character.GetComponent<PlayerController>();
        MenuScript = Menu.GetComponent<Menu>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(PC.transform.position.x - 12, 0);
        if (Time.timeScale != 0)
        {
            scoreval = (int)PC.transform.position.x;
            score.text = "" + scoreval;

            if (canTrap)
            {
                canTrap = false;
                float temp = Random.Range(dmin, dmax);
                StartCoroutine(SpawnTrap(temp));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Instantiate(floor, new Vector2((int)PC.transform.position.x + 12, -4.5f), Quaternion.identity);
            Instantiate(roof, new Vector2((int)PC.transform.position.x + 12, 4.5f), Quaternion.Euler(new Vector3(0, 0, 180)));
        }
        Destroy(collision.gameObject);
    }

    IEnumerator SpawnTrap(float waitTime)
    {
        int temp = Random.Range(0, 2); // Choose a trap
        int pos = Random.Range(0, 2);

        switch (temp)
        {
            case 0: // spike
                if(pos == 1)
                    Instantiate(traps[0], new Vector2((int)PC.transform.position.x + 11, 2.46f), Quaternion.identity);
                else
                    Instantiate(traps[0], new Vector2((int)PC.transform.position.x + 11, -2.46f), Quaternion.Euler(new Vector3(0, 0, 180)));
                break;
            case 1: // laser
                if (pos == 1)
                    Instantiate(traps[1], new Vector2((int)PC.transform.position.x + 11, 1.29f), Quaternion.identity);
                else
                    Instantiate(traps[1], new Vector2((int)PC.transform.position.x + 11, -1.29f), Quaternion.Euler(new Vector3(0, 0, 180)));
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(waitTime);
        canTrap = true;
    }
}

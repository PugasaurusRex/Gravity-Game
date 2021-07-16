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
    bool canTrap = true;

    // Difficulty
    int trapRange = 4;
    float dmin = 3; // min seconds between traps
    float dmax = 5; // max seconds between traps
    int dif = 0;

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

            if (dif == 0)
            {
                dif = 1;
                trapRange = 4;
                dmin = 1;
                dmax = 2;
            }
            else if (dif == 1 && scoreval > 500)
            {
                dif = 2;
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
        int temp = Random.Range(0, trapRange); // Choose a trap
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
            case 2: // box
                if (pos == 1)
                    Instantiate(traps[2], new Vector2((int)PC.transform.position.x + 11, 3.49f), Quaternion.identity);
                else
                    Instantiate(traps[2], new Vector2((int)PC.transform.position.x + 11, -3.49f), Quaternion.Euler(new Vector3(0, 0, 180)));
                break;
            case 3: // mid laser
                if (pos == 1)
                    Instantiate(traps[3], new Vector2((int)PC.transform.position.x + 11, 3.37f), Quaternion.identity);
                else
                    Instantiate(traps[3], new Vector2((int)PC.transform.position.x + 11, -3.37f), Quaternion.Euler(new Vector3(0, 0, 180)));
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(waitTime);
        canTrap = true;
    }
}

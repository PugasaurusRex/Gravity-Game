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
    bool canTrap = false;

    // Difficulty
    int trapRange = 1;
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
                trapRange = 1;
                dmin = 3;
                dmax = 6;
                canTrap = true;
            }
            else if (dif == 1 && scoreval > 50)
            {
                dif = 2;
                trapRange = 2;
                dmin = 3;
                dmax = 5;
            }
            else if (dif == 2 && scoreval > 100)
            {
                dif = 3;
                trapRange = 3;
                dmin = 2;
                dmax = 5;
            }
            else if (dif == 3 && scoreval > 250)
            {
                dif = 4;
                trapRange = 4;
                dmin = 2;
                dmax = 4;
            }
            else if (dif == 4 && scoreval > 500)
            {
                dif = 5;
                dmin = 2;
                dmax = 3;
            }
            else if (dif == 5 && scoreval > 1000)
            {
                dif = 6;
                dmin = 1;
                dmax = 3;
            }
            else if(dif == 6 && scoreval > 2000)
            {
                dif = 7;
                dmin = 1;
                dmax = 2;
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
                int num = Random.Range(1, 5);
                for(int i = 0; i < num; i++)
                {
                    if (pos == 1)
                        Instantiate(traps[0], new Vector2((int)PC.transform.position.x + 11 + i, 2.46f), Quaternion.identity);
                    else
                        Instantiate(traps[0], new Vector2((int)PC.transform.position.x + 11 + i, -2.46f), Quaternion.Euler(new Vector3(0, 0, 180)));
                }
                if (num > 1)
                    waitTime += 1;
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

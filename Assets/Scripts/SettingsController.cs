using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    // Keycode Dictionary
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    // Key Colors
    private GameObject currentKey;
    private Color32 normal = new Color(255, 255, 255, 255);
    private Color32 selected = new Color(255, 0, 0, 255);

    // Controls Textbox
    public TMP_Text jump;

    // Start is called before the first frame update
    void Start()
    {
        // Add Keys to dictionary
        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));

        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TMP_Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
    }

    public void Back()
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
            currentKey = null;
        }
        SetText();
    }

    public void SetText()
    {
        //jump.text = PlayerPrefs.GetString("Jump", "Space");
    }
}

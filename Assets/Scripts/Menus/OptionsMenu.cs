using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject inputField;
    private TMP_InputField sensitivityInput;
    public MainMenuBehavior mainMenuBehavior;
    // Start is called before the first frame update
    public void Start()
    {
        sensitivityInput = inputField.GetComponent<TMP_InputField>();
        var sensitivity = PlayerPrefs.GetInt("sensitivity", 30);
        sensitivityInput.text = sensitivity.ToString();
    }
    public void Save()
    {
        var sensitivity = int.Parse(sensitivityInput.text);
        PlayerPrefs.SetInt("sensitivity", sensitivity);
        mainMenuBehavior.ReturnToStart();
    }


}

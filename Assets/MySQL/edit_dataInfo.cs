using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class edit_dataInfo : MonoBehaviour
{
    public Button m_YourFirstButton;
    public GameObject _gameObject;
    // Start is called before the first frame update
    void Start()
    {
        m_YourFirstButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
        //_gameObject.SetActive(false);
    }
}

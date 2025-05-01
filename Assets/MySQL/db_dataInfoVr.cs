using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class db_dataInfoVr : MonoBehaviour
{
    public enum e_language
    {
        en,
        th
    };

    [System.Serializable]
    public struct infoText
    {
        public Text InfoHeading;
        public Text InfoContent;
    };

    [System.Serializable]
    public struct infoString
    {
        public string InfoHeading;
        public string InfoContent;
    };

    string _tagname;
    string _username;

    [SerializeField] private GameObject _InfoHeading;
    [SerializeField] private GameObject _InfoContent;
    [SerializeField] private GameObject _InteriorInfoHeading;
    [SerializeField] private GameObject _InteriorInfoContent;
    [SerializeField] private GameObject _InfoContainer;
    [SerializeField] private GameObject _InteriorInfoContainer;
    [SerializeField] private e_language _language;

    private infoString EN_InfoString = new infoString();
    private infoString TH_InfoString = new infoString();
    private infoText _InfoText = new infoText();
    private infoText _InteriorInfoText = new infoText();
    private infoText _ExteriorInfoText = new infoText();

    public Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _tagname = "guest_info";
        _username = "guest";
        _language = e_language.en;

        _ExteriorInfoText.InfoHeading = _InfoHeading.GetComponentInChildren<Text>();
        _ExteriorInfoText.InfoContent = _InfoContent.GetComponentInChildren<Text>();
        _InteriorInfoText.InfoHeading = _InteriorInfoHeading.GetComponentInChildren<Text>();
        _InteriorInfoText.InfoContent = _InteriorInfoContent.GetComponentInChildren<Text>();

        // Set UI elements based on current scene
        UpdateUIBasedOnScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 30f);

            if (Physics.Raycast(ray, out hit, 100))
            {
                _tagname = hit.transform.gameObject.tag;
            }
        }

        UpdateInterval();
    }

    void UpdateInterval()
    {
        // Check which scene is currently active
        bool isInterior = SceneManager.GetActiveScene().name == "interiorCar";

        if (isInterior)
        {
            _InfoText = _InteriorInfoText;
            _InfoContainer.SetActive(false);
            _InteriorInfoContainer.SetActive(true);
        }
        else
        {
            _InfoText = _ExteriorInfoText;
            _InfoContainer.SetActive(true);
            _InteriorInfoContainer.SetActive(false);
        }

        var datadb = FindObjectOfType<db_connect>().getData_byUser_byPathId_string(_username, _tagname);

        if (datadb.is_query)
        {
            _tagname = null;
            try
            {
                EN_InfoString.InfoHeading = datadb.part_name_en.ToString();
                EN_InfoString.InfoContent = datadb.part_text_en.ToString();
                TH_InfoString.InfoHeading = datadb.part_name_th.ToString();
                TH_InfoString.InfoContent = datadb.part_text_th.ToString();

                print(datadb.part_name_en.ToString());
            }
            catch (Exception exception)
            {
                string ex = exception.ToString();
                print(ex);
            }
        }

        if (_language == e_language.en)
        {
            _InfoText.InfoHeading.text = EN_InfoString.InfoHeading;
            _InfoText.InfoContent.text = EN_InfoString.InfoContent;
        }
        else
        {
            _InfoText.InfoHeading.text = TH_InfoString.InfoHeading;
            _InfoText.InfoContent.text = TH_InfoString.InfoContent;
        }
    }

    public void EN_changeLanguageInfo()
    {
        _language = e_language.en;
    }

    public void TH_changeLanguageInfo()
    {
        _language = e_language.th;
    }

    public void ChangeToGuest()
    {
        _tagname = "guest_info";
    }

    public void LoadInteriorCarScene()
    {
        SceneManager.LoadScene("interiorCar");
    }

    public void LoadExteriorCarScene()
    {
        SceneManager.LoadScene("exteriorCar");
    }

    private void UpdateUIBasedOnScene()
    {
        bool isInterior = SceneManager.GetActiveScene().name == "interiorCar";

        if (isInterior)
        {
            _InfoContainer.SetActive(false);
            _InteriorInfoContainer.SetActive(true);
        }
        else
        {
            _InfoContainer.SetActive(true);
            _InteriorInfoContainer.SetActive(false);
        }
    }
}

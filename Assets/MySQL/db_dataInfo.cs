using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class db_dataInfo : MonoBehaviour
{
    public enum e_language // your custom enumeration
    {
        en,
        th
    };

    struct infoText // your custom enumeration
    {
        public Text InfoHeading;
        public Text InfoContent;
    };

    struct infoString // your custom enumeration
    {
        public string InfoHeading;
        public string InfoContent;
    };

    string _tagname;
    string _username;
    //bool _isAdmin;
    //float updateInterval = 0.2f;

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

    public Camera InteriorCamera;
    public Camera ExteriorCamera;

    // Start is called before the first frame update
    void Start()
    {
        _tagname = "guest_info";
        _username = "guest";
        _language = e_language.en;
        //_isAdmin  = false;

        //InvokeRepeating("UpdateInterval", updateInterval, updateInterval);
        _ExteriorInfoText.InfoHeading = _InfoHeading.GetComponentInChildren<Text>();
        _ExteriorInfoText.InfoContent = _InfoContent.GetComponentInChildren<Text>();
        _InteriorInfoText.InfoHeading = _InteriorInfoHeading.GetComponentInChildren<Text>();
        _InteriorInfoText.InfoContent = _InteriorInfoContent.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private GameObject FindChildGameObjectByName(GameObject topParentGameObject, string gameObjectName)
    {
        for (int i = 0; i < topParentGameObject.transform.childCount; i++)
        {
            if (topParentGameObject.transform.GetChild(i).name == gameObjectName)
            {
                return topParentGameObject.transform.GetChild(i).gameObject;
            }

            GameObject tmp = FindChildGameObjectByName(topParentGameObject.transform.GetChild(i).gameObject, gameObjectName);
            if (tmp != null)
            {
                return tmp;
            }
        }
        return null;
    }
    void UpdateInterval()
    {




    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MySql.Data.MySqlClient;
using System.Data;
using System;
using UnityEngine.UI;

public class db_connect : MonoBehaviour
{
    [Header("Database Properties")]
    public string HostInternal = "192.168.99.12";
    public string HostDNS = "96890a52c7fb.sn.mynetname.net";
    private string HostSelection = null;
    public uint Port = 3306;
    public string User = "atlearning";
    public string Password = "passworD1234";
    public string Database = "active_learning_db";
    public bool testGetData = true;
    private bool db_connected = false;
    private bool db_exiting = false;
    public string NAS_part = "";

    MySqlConnection connection = null;

    [SerializeField] private GameObject _db_status;

    //data parameter from db
    public struct dataDB
    {
        public int id;
        public string part_id;
        public string part_name_en;
        public string part_name_th;
        public string part_text_en;
        public string part_text_th;
        public string part_image_path;
        public string part_video_path;
        public string part_sound_path;
        public bool is_query;
    };

    

    //float updateInterval = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        HostSelection = HostInternal;
        //InvokeRepeating("UpdateInterval", updateInterval, updateInterval);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInterval();
    }

    void UpdateInterval()
    {
        Text newText = _db_status.GetComponentInChildren<Text>();
        //use this as the secondary update.
        if (!connectToDB())
        {
            print("MySQL - Reconnecting");
            newText.text = "Disconnected";
            db_connected = false;

        }
        else if(!db_connected)
        {
            print("MySQL - Connected");
            newText.text = "Connected";
            db_connected = true;
        }
            
    }

    public void set_db_connected(bool isconnect)
    {
        db_connected = isconnect;
    }

    public void set_db_exiting()
    {
        db_exiting = true;
    }

    private bool connectToDB()
    {
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        builder.Server = HostSelection;
        builder.Port = Port;
        builder.UserID = User;
        builder.Password = Password;
        builder.Database = Database;

        if(db_exiting)
        {
            connection.Close();
            return false;
        }
        else
        {
            try
            {
                MySqlConnection connectionCheck;
                connectionCheck = new MySqlConnection(builder.ToString());
                connectionCheck.Open();
                connectionCheck.Close();

                if(connection == null)
                    connection = new MySqlConnection(builder.ToString());

                if(!db_connected)
                    connection.Open();

                return true;

            }
            catch (MySqlException exception)
            {

                if (HostSelection == HostInternal)
                {
                    print("change Host to HostDNS");
                    HostSelection = HostDNS;
                }
                else
                {
                    print("change HostDNS to Host");
                    HostSelection = HostInternal;
                }

                //print(exception.Message);
                switch (exception.Number)
                {
                    case 0:
                        print("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        print("Invalid username/password, please try again");
                        break;
                }
                connection.Close();
                return false;
            }
        }


    }

    //// query data from db segment code ////
    // get data
    public dataDB getData_byUser_byPathId_string(string user, string tag)
    {
        dataDB datadb;
        datadb.id = -1;
        datadb.part_id = null;
        datadb.part_name_en = null;
        datadb.part_name_th = null;
        datadb.part_text_en = null;
        datadb.part_text_th = null;
        datadb.part_image_path = null;
        datadb.part_video_path = null;
        datadb.part_sound_path = null;
        datadb.is_query = false;

        string user_query_str = "tb_part_" + user;
        string tag_query_str  = "'" + tag + "'";

        string sql = "SELECT * FROM "+ user_query_str + " WHERE part_id = " + tag_query_str;
        using var cmd = new MySqlCommand(sql, connection);

        if (db_connected && (!db_exiting))
        {
            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    datadb.id = rdr.GetInt32(0);
                    datadb.part_id = rdr.GetString(1);
                    datadb.part_name_en = rdr.GetString(2);
                    datadb.part_name_th = rdr.GetString(3);
                    datadb.part_text_en = rdr.GetString(4);
                    datadb.part_text_th = rdr.GetString(5);
                    datadb.part_image_path = rdr.GetString(6);
                    datadb.part_video_path = rdr.GetString(7);
                    datadb.part_sound_path = rdr.GetString(8);
                    datadb.is_query = true;
                }
                else
                {
                    //print(user_query_str);
                }
                rdr.Close();
            }
            catch (Exception exception)
            {
                string ex = exception.ToString();
                //print(ex);
            }
        }

        return datadb;
        
    }
    //set data
    private bool setData_byUser_byPathId_string(string user, string tag)
    {
        return false;
    }

    private string getDataPathFromSave(string path)
    {
        string resuit = null;

        return resuit;
    }

    ////Display segment code ////
    private void setData_toTextBox_string(string data)
    {

    }

    private void setData_toImage_StringPart(string data)
    {

    }
    private void setData_toVideo_StringPart(string data)
    {

    }
    private void setData_toSound_StringPart(string data)
    {

    }
}

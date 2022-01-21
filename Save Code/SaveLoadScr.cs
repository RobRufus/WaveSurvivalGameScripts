using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveLoadS
{
    [Serializable]
    public struct GameState
    {
        //public ScoreControlerScr eventSystem;
        public objPosState playerPos;
        public objPosState[] enemies;
        public int score, waves, enemyCount, levelNum;
        public bool flag;
        public string playerType;

        //ScoreControlerScr eventSystem, 

        public GameState(int score, int waves, int enemyCount, bool flag, int levelNum, objPosState playerPos, objPosState[] enemies, string playerType)
        {
            this.score = score;
            this.waves = waves;
            this.enemyCount = enemyCount;
            this.flag = flag;
            this.levelNum = levelNum;
            this.playerPos = playerPos;
            this.enemies = enemies;
            this.playerType = playerType;
        }
    }



    [Serializable]
    public struct objPosState
    {
        public Vector3 position;
        public Quaternion rotation;

        public objPosState(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }







    public class SaveLoadScr : MonoBehaviour
    {
        public SpawnWaveScr eventSys;
        public ScoreControlerScr scorCon;
        public GameObject playerTOSET;
        public GameObject playerNinja;
        public GameObject playerPaladin;
        public GameObject enemyTOSPAWN;

        [SerializeField] private objPosState player;
        [SerializeField] private objPosState[] enemies;
        [SerializeField] private int score, waves, enemyCount, levelNum;
        [SerializeField] private bool flag;
        [SerializeField] private string playerType;

        private const string SAVEGAME_FILE = "Assets/Saves/savegame.xml";


        private void Awake()
        {
            //if save file exists AND on a playable level then load from file
            Time.timeScale = 1;

        }



        public void CallSaveGame()
        {
            if (System.IO.File.Exists("savegame.xml"))
            {
                File.Delete(Application.dataPath + "Assets/Saves/savegame.xml");
                //UnityEditor.AssetDatabase.Refresh();
            }
            GetVariables();
            Save(SAVEGAME_FILE);
            print("saved.");


            Destroy(GameObject.Find("MainMenuCanvas"));
            Destroy(GameObject.Find("HighScoreControler"));
            SceneManager.LoadScene(0);
        }

        public void CallLoadGame()
        {
            LoadLvlNum(SAVEGAME_FILE);
            print("opened scene");
        }


        private void GetVariables()
        {
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            enemies = new objPosState[enemyList.Length];
            //eventSystem = GameObject.FindObjectOfType<ScoreControlerScr>();

            

            //set player save var to the player position
            player = new objPosState(playerTOSET.transform.position, playerTOSET.transform.rotation);

            //get wave flag
            flag = eventSys.checkWaveFlag();

            //get score
            score = scorCon.getScore();

            //get waves
            waves = scorCon.getWave();

            //get enemy count
            enemyCount = enemyList.Length;

            //get current level
            levelNum = SceneManager.GetActiveScene().buildIndex;


            for (int i = 0; i < enemyList.Length; i++)
            {
                enemies[i] = new objPosState(enemyList[i].transform.position, enemyList[i].transform.rotation);
                Debug.Log(i);
            }

            playerType = playerTOSET.name;

        }


        private void Save(string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            GameState state = new GameState(score, waves, enemyCount, flag, levelNum, player, enemies, playerType);
            XmlSerializer serializer = new XmlSerializer(typeof(GameState));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, state);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(filename);
            }
        }





        public void LoadLvlNum(string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);
            string xmlString = xmlDocument.OuterXml;

            GameState state;
            using (StringReader read = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameState));
                using (XmlReader reader = new XmlTextReader(read))
                {
                    state = (GameState)serializer.Deserialize(reader);
                }
            }
            SceneManager.LoadScene(state.levelNum);
        }








        public void Load(string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);
            string xmlString = xmlDocument.OuterXml;

            GameState state;
            using (StringReader read = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameState));
                using (XmlReader reader = new XmlTextReader(read))
                {
                    state = (GameState)serializer.Deserialize(reader);
                }
            }

            
            Debug.Log("playerType " + state.playerType);
            //set player location
            playerTOSET.transform.position = state.playerPos.position;
            playerTOSET.transform.rotation = state.playerPos.rotation;
            

            //spawn enemies at location
            for (int i = 0; i < state.enemies.Length; i++)
            {
                Instantiate(enemyTOSPAWN, state.enemies[i].position, state.enemies[i].rotation);
            }

            //set variables
            eventSys.spawnEnemiesOnLoad(state.enemyCount, state.flag);
            scorCon.SetVarsOnLoad(state.score, state.waves);
        }









        public String fetchCharType() 
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("Assets/Saves/savegame.xml");
            string xmlString = xmlDocument.OuterXml;

            GameState state;
            using (StringReader read = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameState));
                using (XmlReader reader = new XmlTextReader(read))
                {
                    state = (GameState)serializer.Deserialize(reader);
                }
            }
            return state.playerType;
        }

    }

}
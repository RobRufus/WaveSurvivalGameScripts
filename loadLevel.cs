using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;
using System.IO;

namespace SaveLoadS
{
    public class loadLevel : MonoBehaviour
    {

        public void LoadLvl()
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

            FindObjectOfType<RemoteHighScoreManager>().charselect(state.playerType);

            DontDestroyOnLoad(this);

            //change scene
            SceneManager.LoadScene(state.levelNum);

            StartCoroutine(onLoad());

        }

        public IEnumerator onLoad()
        {

            yield return new WaitForSeconds(0.1f);
            FindObjectOfType<SaveLoadScr>().Load("Assets/Saves/savegame.xml");
        }
        

    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class LoadProfile : MonoBehaviour {
    public GameObject contentBox;
    public GameObject scrollView;
    public Button buttonPrefab;
    private float buttonHeight;
    private bool open;
    public InputField nameInput;
    public InputField ageInput;
    public InputField weightInput;
    public InputField heightInput;
    public Toggle maleToggle;
    public Toggle femaleToggle;

    void Start()
    {
        RectTransform rTrans = (RectTransform)buttonPrefab.transform.GetComponent<RectTransform>();
        buttonHeight = rTrans.sizeDelta.y;
        open = false;
        scrollView.SetActive(false);
    }

    private List<string> LoadXmlFiles()
    {
        DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] fileList = info.GetFiles();
        List<string> fileNames = new List<string>();

        foreach (FileInfo file in fileList)
        {
            string name = file.Name;
            name = name.Remove(name.Length - 4);
            fileNames.Add(name);
        }

        return fileNames;
    }

    public void ShowUserProfiles()
    {
        if (open)
        {
            return;
        }
        else
        {
            open = true;
            scrollView.SetActive(true);
        }

        List<string> fileNames = LoadXmlFiles();
        List<string> playerNames = new List<string>();
        int count = 0;

        foreach (string fileName in fileNames)
        {
            string[] split = fileName.Split('-');
            string playerName = split[0];
            playerNames.Add(playerName);

            Button b = (Button) Instantiate(buttonPrefab);
            b.transform.position -= new Vector3(0, buttonHeight * count, 0); // Button height
            b.transform.SetParent(contentBox.transform, false);
            Text t = b.GetComponentInChildren<Text>();
            t.text = playerName;
            ColorBlock cb = b.colors;

            if (split[2] == "M")
            {
                cb.normalColor = Color.blue;
                cb.highlightedColor = Color.blue;
            }
            else
            {
                cb.normalColor = Color.magenta;
                cb.highlightedColor = Color.magenta;
            }

            b.colors = cb;
            string captured = fileName;
            b.onClick.AddListener(() => FillProfile(captured + ".xml"));

            count++;
        }

        RectTransform rTrans = (RectTransform)contentBox.transform.GetComponent<RectTransform>();
        rTrans.sizeDelta = new Vector2(rTrans.sizeDelta.x, count * buttonHeight);
    }

    private void FillProfile(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Player));
        FileStream stream = new FileStream(Application.persistentDataPath + "\\" + fileName, FileMode.Open);
        Player p = serializer.Deserialize(stream) as Player;
        stream.Close();
        Debug.Log(fileName);

        CreatePlayerInfo cp = CreatePlayerInfo.instance;
        cp.setAge(p.getAge().ToString());
        cp.setGender(p.getGender().ToString());
        cp.setHeight(p.getHeight().ToString());
        cp.setName(p.getName());
        cp.setWeight(p.getWeight().ToString());
         
        ageInput.text = p.getAge().ToString();
        heightInput.text = p.getHeight().ToString();
        nameInput.text = p.getName();
        weightInput.text = p.getWeight().ToString();

        if (p.getGender() == Gender.Male)
        {
            maleToggle.isOn = true;
        }
        else
        {
            femaleToggle.isOn = true;
        }
    }
}

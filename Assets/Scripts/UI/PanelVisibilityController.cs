using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelVisibilityController : MonoBehaviour {
    public List<Image> images;

    public static PanelVisibilityController instance;

    void Awake()
    {
        instance = this;
        images = new List<Image>();
    }

    public void addPanel(Image i) //setup
    {
        this.images.Add(i);
        i.enabled = false;

		if (Application.loadedLevelName == "aantalSpelersScreen")
		{
			if (images.Count == 4)
			{
				DataLoad.instance.setPlayers();
			}
		}
    }

    public void setVisible(Image i) //hides panel
    {
        foreach (Image image in images)
        {
            if (image.enabled)
            {
                image.enabled = false;
            }
        }

        i.enabled = !i.enabled;
    }
}
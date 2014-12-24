using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelVisibilityController : MonoBehaviour {
    private List<Image> images;

    public static PanelVisibilityController instance;

    void Awake()
    {
        instance = this;
        images = new List<Image>();
    }

    public void addPanel(Image i)
    {
        this.images.Add(i);
        i.enabled = false;
    }

    public void setVisible(Image i)
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

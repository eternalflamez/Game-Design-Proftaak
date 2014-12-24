using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelVisibility : MonoBehaviour {
    private Image image;

    void Start()
    {
        image = this.transform.GetComponent<Image>();
        PanelVisibilityController.instance.addPanel(image);
    }

    public void setPanelVisibility()
    {
        PanelVisibilityController.instance.setVisible(image);
    }
}

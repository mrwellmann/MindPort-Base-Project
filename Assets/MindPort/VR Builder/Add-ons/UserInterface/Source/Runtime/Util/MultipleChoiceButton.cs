using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VRBuilder.UI.Utils
{
    public class MultipleChoiceButton : MonoBehaviour
    {
        public TMP_Text textField;
        public Image image;

        public void SetButtonContent(string text)
        {
            textField.gameObject.SetActive(true);
            image.gameObject.SetActive(false);
            textField.text = text;
            GetComponent<Button>().targetGraphic = GetComponent<Image>();
        }

        public void SetButtonContent(Sprite sprite)
        {
            textField.gameObject.SetActive(false);
            image.gameObject.SetActive(true);
            image.sprite = sprite;
            GetComponent<Button>().targetGraphic = image;
        }

    }
}

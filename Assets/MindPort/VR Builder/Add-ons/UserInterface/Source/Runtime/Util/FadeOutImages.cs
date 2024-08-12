using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VRBuilder.UI.Utils
{
    public class FadeOutImages : MonoBehaviour
    {
        public event UnityAction OnFinsihedFade;

        public enum FadeOutState
        {
            Idle,
            FadeOut,
            FadeIn
        }
        public FadeOutState State = FadeOutState.Idle;
        public float FadeSpeed = 1f;

        public bool ColorFade = false;
        public Color FadeToColor = Color.black;
        protected Color[] savedColors;

        protected float currentAlpha = 1f;
        protected Image[] myImages;

        void Awake()
        {
            if (myImages == null || myImages.Length <= 0)
                myImages = this.GetComponentsInChildren<Image>(true);

            if (ColorFade && myImages != null && myImages.Length > 0)
            {
                savedColors = new Color[myImages.Length];
                for (int i = 0; i < myImages.Length; i++)
                {
                    savedColors[i] = myImages[i].color;
                }
            }

            currentAlpha = 1f;
        }

        void Update()
        {
            switch (State)
            {
                case FadeOutState.FadeOut:
                    currentAlpha -= Time.deltaTime * FadeSpeed;
                    for (int i = 0; i < myImages.Length; i++)
                    {
                        if (ColorFade)
                        {
                            myImages[i].color = Color.Lerp(FadeToColor, savedColors[i], currentAlpha);
                        }
                        else
                        {
                            myImages[i].color = new Color(myImages[i].color.r, myImages[i].color.g, myImages[i].color.b, currentAlpha);
                        }
                    }
                    if (currentAlpha <= 0f)
                    {
                        State = FadeOutState.Idle;
                        OnFinsihedFade?.Invoke();
                    }
                    break;
                case FadeOutState.FadeIn:
                    currentAlpha += Time.deltaTime * FadeSpeed;
                    for (int i = 0; i < myImages.Length; i++)
                    {
                        if (ColorFade)
                        {
                            myImages[i].color = Color.Lerp(FadeToColor, savedColors[i], currentAlpha);
                        }
                        else
                        {
                            myImages[i].color = new Color(myImages[i].color.r, myImages[i].color.g, myImages[i].color.b, currentAlpha);
                        }
                    }
                    if (currentAlpha >= 1f)
                    {
                        State = FadeOutState.Idle;
                        OnFinsihedFade?.Invoke();
                    }
                    break;
            }
        }

        public bool IsFading()
        {
            return State != FadeOutState.Idle;
        }

        public void StartFadeOut()
        {
            State = FadeOutState.FadeOut;
            if (currentAlpha <= 0f)
            {
                currentAlpha = 1f;
                for (int i = 0; myImages != null && i < myImages.Length; i++)
                {
                    if (ColorFade)
                    {
                        myImages[i].color = savedColors[i];
                    }
                    else
                    {
                        myImages[i].color = new Color(myImages[i].color.r, myImages[i].color.g, myImages[i].color.b, 1f);
                    }
                }
            }
        }

        public void StartFadeIn()
        {
            State = FadeOutState.FadeIn;
            if (currentAlpha >= 1f)
            {
                currentAlpha = 0f;
                for (int i = 0; myImages != null && i < myImages.Length; i++)
                {
                    if (ColorFade)
                    {
                        myImages[i].color = FadeToColor;
                    }
                    else
                    {
                        myImages[i].color = new Color(myImages[i].color.r, myImages[i].color.g, myImages[i].color.b, 0f);
                    }
                }
            }
        }

        public void Reset()
        {
            State = FadeOutState.Idle;
            for (int i = 0; myImages != null && i < myImages.Length; i++)
            {
                if (ColorFade)
                {
                    myImages[i].color = savedColors[i];
                }
                else
                {
                    myImages[i].color = new Color(myImages[i].color.r, myImages[i].color.g, myImages[i].color.b, 1f);
                }
            }
        }
    }
}

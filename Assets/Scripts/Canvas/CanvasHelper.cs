using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// Canvas helper @C Adriaan
/// https://forum.unity.com/threads/canvashelper-resizes-a-recttransform-to-iphone-xs-safe-area.521107/
/// 
/// Description
/// 
/// -Resize the canvas inside the phone safe area;
/// 
/// </summary>

namespace CanvasActions
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasHelper : MonoBehaviour
    {
        #region "Private members"
        private static List<CanvasHelper> helpers = new List<CanvasHelper>();

        public static UnityEvent OnResolutionOrOrientationChanged = new UnityEvent();

        private static bool screenChangeVarsInitialized = false;
        private static ScreenOrientation lastOrientation = ScreenOrientation.LandscapeLeft;
        private static Vector2 lastResolution = Vector2.zero;
        private static Rect lastSafeArea = Rect.zero;

        private Canvas canvas;
        private RectTransform safeAreaTransform;

        private Vector2 anchorMin;
        private Vector2 anchorMax;
        #endregion


        void Awake()
        {
            if (!helpers.Contains(this))
                helpers.Add(this);

            canvas = GetComponent<Canvas>();

            safeAreaTransform = transform.Find("SafeArea") as RectTransform;

            if (!screenChangeVarsInitialized)
            {
                lastOrientation = Screen.orientation;
                lastResolution.x = Screen.width;
                lastResolution.y = Screen.height;
                lastSafeArea = Screen.safeArea;

                screenChangeVarsInitialized = true;
            }

            ApplySafeArea();
        }

        void Update()
        {
            if (helpers[0] != this)
                return;

            if (Application.isMobilePlatform && Screen.orientation != lastOrientation)
                OrientationChanged();

            if (Screen.safeArea != lastSafeArea)
                SafeAreaChanged();

            if (Screen.width != lastResolution.x || Screen.height != lastResolution.y)
                ResolutionChanged();
        }

        void ApplySafeArea()
        {
            if (safeAreaTransform == null)
                return;
            
            var safeArea = Screen.safeArea;
            var diff = Mathf.Abs(Screen.safeArea.width - Screen.width);

            if (safeArea.x > 0)
            {
                anchorMin = safeArea.position;

                if(diff - safeArea.x > 0)
                {
                    anchorMax = safeArea.position + safeArea.size;
                }
                else
                {
                    anchorMax = safeArea.position + safeArea.size - new Vector2(diff, 0);
                }              
            }
            else
            {
                anchorMin = safeArea.position + new Vector2(diff, 0);
                anchorMax = safeArea.position + safeArea.size;
            }

            anchorMin.x /= canvas.pixelRect.width;
            anchorMin.y /= canvas.pixelRect.height;
            anchorMax.x /= canvas.pixelRect.width;
            anchorMax.y /= canvas.pixelRect.height;

            safeAreaTransform.anchorMin = anchorMin;
            safeAreaTransform.anchorMax = anchorMax;
        }


        #region "Changed Handlers"
        private static void OrientationChanged()
        {
            lastOrientation = Screen.orientation;
            lastResolution.x = Screen.width;
            lastResolution.y = Screen.height;

            OnResolutionOrOrientationChanged.Invoke();
        }
        private static void ResolutionChanged()
        {
            lastResolution.x = Screen.width;
            lastResolution.y = Screen.height;

            OnResolutionOrOrientationChanged.Invoke();
        }
        private static void SafeAreaChanged()
        {
            lastSafeArea = Screen.safeArea;

            for (int i = 0; i < helpers.Count; i++)
            {
                helpers[i].ApplySafeArea();
            }
        }
        #endregion


        void OnDestroy()
        {
            if (helpers != null && helpers.Contains(this))
                helpers.Remove(this);
        }
    }
}
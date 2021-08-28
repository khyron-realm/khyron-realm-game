using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add animation to the Line renderer

namespace AnimationsScripts
{
    public class LineRendererAnimation : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        [SerializeField]
        private Texture[] texture;

        [SerializeField]
        private float fps = 30f;

        private int animationStep;

        private float fpsCounter;

        void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }


        // Update is called once per frame
        void Update()
        {
            fpsCounter += Time.deltaTime;

            if (fpsCounter >= 1f / fps)
            {
                animationStep++;
                if (animationStep == texture.Length)
                {
                    animationStep = 0;
                }

                _lineRenderer.material.SetTexture("_MainTex", texture[animationStep]);

                fpsCounter = 0f;
            }
        }
    }
}
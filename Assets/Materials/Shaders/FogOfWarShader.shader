Shader "Hidden/FogOfWarShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _SecondaryTex("Secondary Texture", 2D) = "white" {}
        _WorldTransparency("Float with range", Range(0.0, 1.0)) = 0.5
        _ExploredPathVisibility("Float with range", Range(0.0, 1.0)) = 0.25
        _FOVVisibility("Float with range", Range(0.0, 1.0)) = 0.4
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent+1"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _SecondaryTex;
            float _WorldTransparency;
            float _ExploredPathVisibility;
            float _FOVVisibility;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) + tex2D(_SecondaryTex, i.uv);

                col.a = _WorldTransparency - _ExploredPathVisibility * col.r - _FOVVisibility * col.b;
               
                return fixed4(0,0,0,col.a);
            }
            ENDCG
        }
    }
}
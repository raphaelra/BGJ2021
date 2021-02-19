Shader "Toon/Fragment Cel-Shader"
{
    Properties
    {
        _Color ("Base Color", Color) = (1,1,1,1)
        _UnlitColor ("Shadow Color", Color) = (0.5,0.5,0.5,1)
        _UnlitThreshold ("Shadow Range", Range(0,1)) = 0.1
    }
 
    SubShader
    {
    Tags{"RenderType"="Opaque"}
    LOD 200
 
        Pass
        {
        Tags {"LightMode" = "ForwardBase"}
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
            float4 _LightColor0;
           
 
            float4 _Color;
            float4 _UnlitColor;
            float _UnlitThreshold;
 
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
 
            v2f vert (appdata IN)
            {
                v2f OUT;
                OUT.pos = UnityObjectToClipPos(IN.vertex);
 
                float4x4 modelMatrix = unity_ObjectToWorld;
                float4x4 modelMatrixInverse = unity_WorldToObject;
 
                OUT.posWorld = mul(modelMatrix, IN.vertex);
                OUT.normalDir = normalize(
                    mul(float4(IN.normal, 0.0), modelMatrixInverse).xyz
                );
 
                return OUT;
            }
 
            float4 frag (v2f IN) : COLOR
            {
                float3 normalDirection = normalize(IN.normalDir);
                float3 lightDirection;
                float attenuation;
                float3 fragmentColor;
 
 
                attenuation = 1.0;
                lightDirection = normalize(_WorldSpaceLightPos0).xyz;
                fragmentColor = _LightColor0.rgb * _UnlitColor.rgb * _Color.rgb;
 
 
                if (attenuation * max(0.0, dot(normalDirection, lightDirection)) >= _UnlitThreshold) {
                    fragmentColor = _LightColor0.rgb * _Color.rgb; // lit fragment color
                }
 
                return float4(fragmentColor, 1.0);
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
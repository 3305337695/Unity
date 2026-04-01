Shader "Unlit/Jelly"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        
        // 碰撞参数
        _ImpactPos ("Impact Position", Vector) = (0,0,0,0)
        _ImpactTime ("Impact Time", Float) = -100
        _ImpactStr ("Impact Strength", Float) = 0
        
        // 形变控制
        _WaveFreq ("Wave Frequency", Float) = 30
        _WaveSpeed ("Wave Speed", Float) = 10
        _DecaySpeed ("Decay Speed", Float) = 4
        _Radius ("Effect Radius", Float) = 3
        _Amplitude ("Max Amplitude", Float) = 0.15
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            // 纹理
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            
            // 碰撞参数
            float4 _ImpactPos;
            float _ImpactTime;
            float _ImpactStr;
            
            // 形变控制
            float _WaveFreq;
            float _WaveSpeed;
            float _DecaySpeed;
            float _Radius;
            float _Amplitude;

            v2f vert (appdata v)
            {
                v2f o;
                
                // 计算世界坐标
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                
                // === 弹性形变计算 ===
                float time = _Time.y - _ImpactTime;
                
                // 只处理碰撞后的时间
                if (time > 0 && _ImpactStr > 0)
                {
                    float2 impactCenter = _ImpactPos.xy;
                    float2 toVertex = worldPos.xy - impactCenter;
                    float dist = length(toVertex);
                    
                    // 距离衰减（只在半径内形变）
                    float radiusMask = 1 - saturate(dist / _Radius);
                    
                    // 时间衰减
                    float timeDecay = exp(-time * _DecaySpeed);
                    
                    // 波纹效果：sin波向外传播
                    float wave = sin(dist * _WaveFreq - time * _WaveSpeed);
                    
                    // 综合形变强度
                    float deformation = wave * radiusMask * timeDecay * _ImpactStr * _Amplitude;
                    
                    // 应用位移（沿碰撞方向推开/拉回）
                    float2 dir = normalize(toVertex + 0.0001); // 防除零
                    worldPos.xy += dir * deformation;
                }
                
                // 转换到裁剪空间
                o.vertex = UnityWorldToClipPos(float4(worldPos, 1));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}

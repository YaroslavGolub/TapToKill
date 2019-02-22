Shader "Custom/Background/SkyboxLerpColor"
{
    Properties
    {
        _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)
		_Range("_Range",Range(0,1))=0.5
		_Power("_Power",Range(0,10)) = 1
		_Brightness("_Brightness", Range(0,2)) = 1        
    }
    SubShader
    {
        Tags { "Queue"="Background" }
		LOD 100

		Pass{
			ZWrite Off
			Cull Back

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

			fixed4 _Color1;
			fixed4 _Color2;
			fixed _Range;
			fixed _Power;
			fixed _Brightness;

			struct IN {
				float4 vertex : POSITION;
				float3 texcoord : TEXCOORD0;
			};

			struct OUT {
				float4 vertex : SV_POSITION;
				float3 uv : TEXCOORD0;
			};

			OUT vert(IN i) {
				OUT o;
				o.vertex = UnityObjectToClipPos(i.vertex);
				o.uv = i.texcoord;
				return o;
			}

			fixed4 frag (OUT i) : COLOR {
				fixed4 lerpedColor = lerp(_Color1, _Color2, clamp(i.uv.y + pow(_Range,_Power), 0, 1));
				return lerpedColor * _Brightness;
			}
			ENDCG
		}        
    }
    FallBack "Diffuse"
}

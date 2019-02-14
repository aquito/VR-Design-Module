
Shader "Mimesys/RgbScifiSurfaceShader3" {
	Properties {
		_HologramColor("Hologram color", Color) = (0,0,1,1)
		_FresnelStrength("Fresnel strength", Range(0, 1)) = 0.5299146
		_FresnelNormalFactor("Fresnel normal factor", Range(0, 2)) = 0

//		_Color ("Color", Color) = (1,1,1,.5)
//		_MainTex ("Albedo (RGB)", 2D) = "white" {}
//		_Glossiness ("Smoothness", Range(0,1)) = 0.5
//		_Metallic ("Metallic", Range(0,1)) = 0.0

//		_BlendWeights0("Weight 0", Range(0,1)) = 1.0
//		_BlendWeights1("Weight 1", Range(0,1)) = 0.0
//		_BlendWeights2("Weight 2", Range(0,1)) = 0.0

		_Texture("Texture (RGB)", 2D) = "white" {}
	}
	SubShader {
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM

#pragma vertex vert  
#pragma fragment frag 

#include "UnityCG.cginc"

			sampler2D _Texture;
			uniform float4x4 _World2Cam0;
			uniform float4x4 _World2Cam1;
			uniform float4x4 _World2Cam2;
			uniform float4 _HologramColor;
			//uniform float4 _Color;
			uniform float _FresnelStrength;
			uniform float _FresnelNormalFactor;

			struct vertexInput {
				float4 vertex : POSITION;
				float4 blendWeights1 : TEXCOORD0;
				float4 blendWeights2 : TEXCOORD1;
				float4 posWorld : TEXCOORD2;
				fixed4 color : COLOR;
				float3 normal : NORMAL;
			};

			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 uv0: TEXCOORD0;
				float4 uv1: TEXCOORD1;
				float4 uv2: TEXCOORD2;
				float3 normalDir : TEXCOORD3;
				float4 posWorld: TEXCOORD4;
				//float4 color : COLOR;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;
				
				output.pos = UnityObjectToClipPos(input.vertex);

				float totalWeight = input.blendWeights1.x + input.blendWeights1.y + input.blendWeights2.x;
				
				output.uv0 = mul(_World2Cam0, input.vertex);
				output.uv0 = (output.uv0 / output.uv0.z + 1) / 2;
				output.uv0.y = output.uv0.y / 3.0f;
				output.uv0.w = input.blendWeights1.x / totalWeight;
				
				output.uv1 = mul(_World2Cam1, input.vertex);
				output.uv1 = (output.uv1 / output.uv1.z + 1) / 2;
				output.uv1.y = output.uv1.y / 3.0f + 0.33333f;
				output.uv1.w = input.blendWeights1.y / totalWeight;
				
				output.uv2 = mul(_World2Cam2, input.vertex);
				output.uv2 = (output.uv2 / output.uv2.z + 1) / 2;
				output.uv2.y = output.uv2.y / 3.0f + 0.666666667f;
				output.uv2.w = input.blendWeights2.x / totalWeight;

				output.normalDir = UnityObjectToWorldNormal(input.normal);
				output.posWorld = mul(unity_ObjectToWorld, input.vertex);

				return output;

				
			}

			float4 frag(vertexOutput input) : COLOR
			{
				fixed4 color0 = tex2D(_Texture, input.uv0.xy);				
				fixed4 color1 = tex2D(_Texture, input.uv1.xy);
				fixed4 color2 = tex2D(_Texture, input.uv2.xy);		
				float val0 = input.uv0.w;
				float val1 = input.uv1.w;
				float val2 = input.uv2.w;

				fixed4 result = color0 * val0*.5f + color1 * val1*.5f + color2 * val2*.5f;

				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - input.posWorld.xyz);
				input.normalDir = normalize(input.normalDir);
				float3 emissive = (((pow(1.0 - max(0, dot(input.normalDir, viewDirection)), _FresnelNormalFactor)*_HologramColor.rgb)*_FresnelStrength));

				//fixed4 result = float4(1,0,0,1) * val0 + float4(0 ,1, 0, 1) * val1 + float4(0,0,1, 1) * val2;
				return fixed4(emissive.x + result.rgb.x, emissive.y + result.rgb.y, emissive.z + result.rgb.z, _HologramColor.a);

			//	return fixed4(val0, val1, val2, 1);
			//	return float4(input.uv0);
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}

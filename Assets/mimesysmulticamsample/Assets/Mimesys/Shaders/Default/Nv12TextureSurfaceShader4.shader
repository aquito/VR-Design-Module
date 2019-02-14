﻿
Shader "Mimesys/Nv12TextureSurfaceShader4" {
	Properties {
//		_Color ("Color", Color) = (1,1,1,1)
//		_MainTex ("Albedo (RGB)", 2D) = "white" {}
//		_Glossiness ("Smoothness", Range(0,1)) = 0.5
//		_Metallic ("Metallic", Range(0,1)) = 0.0

//		_BlendWeights0("Weight 0", Range(0,1)) = 1.0
//		_BlendWeights1("Weight 1", Range(0,1)) = 0.0
//		_BlendWeights2("Weight 2", Range(0,1)) = 0.0

		_TextureY("TextureY (NV12)", 2D) = "white" {}
		_TextureUV("TextureUV (NV12)", 2D) = "white" {}
	}
	SubShader {
		Cull Off

		Pass {
			CGPROGRAM

#pragma vertex vert  
#pragma fragment frag 

			sampler2D _TextureY;
			sampler2D _TextureUV;
			uniform float4x4 _World2Cam0;
			uniform float4x4 _World2Cam1;
			uniform float4x4 _World2Cam2;
			uniform float4x4 _World2Cam3;

			struct vertexInput {
				float4 vertex : POSITION;
				float4 blendWeights1 : TEXCOORD0;
				float4 blendWeights2 : TEXCOORD1;
				//fixed4 color : COLOR;
			};

			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 uv0: TEXCOORD0;
				float4 uv1: TEXCOORD1;
				float4 uv2: TEXCOORD2;
				float4 uv3: TEXCOORD3;
				//float4 color : COLOR;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;
							
				output.pos = UnityObjectToClipPos(input.vertex);

				float totalWeight = input.blendWeights1.x + input.blendWeights1.y + input.blendWeights2.x + input.blendWeights2.y;
				
				output.uv0 = mul(_World2Cam0, input.vertex);
				output.uv0 = (output.uv0 / output.uv0.z + 1) / 2;
				output.uv0.y = output.uv0.y / 4.0f;
				output.uv0.w = input.blendWeights1.x / totalWeight;
				
				output.uv1 = mul(_World2Cam1, input.vertex);
				output.uv1 = (output.uv1 / output.uv1.z + 1) / 2;
				output.uv1.y = output.uv1.y / 4.0f + 0.25f;
				output.uv1.w = input.blendWeights1.y / totalWeight;
				
				output.uv2 = mul(_World2Cam2, input.vertex);
				output.uv2 = (output.uv2 / output.uv2.z + 1) / 2;
				output.uv2.y = output.uv2.y / 4.0f + 0.5f;
				output.uv2.w = input.blendWeights2.x / totalWeight;

				output.uv3 = mul(_World2Cam3, input.vertex);
				output.uv3 = (output.uv3 / output.uv3.z + 1) / 2;
				output.uv3.y = output.uv3.y / 4.0f + 0.75f;
				output.uv3.w = input.blendWeights2.y / totalWeight;

				return output;				
			}

			fixed4 getPixel(float2 uv)
			{
				float y = tex2D(_TextureY, uv).r;
				float u = tex2D(_TextureUV, uv).r - 0.5;
				float v = tex2D(_TextureUV, uv).g - 0.5;
				// The numbers are just YUV to RGB conversion constants
				float r = y + 1.13983*v;
				float g = y - 0.39465*u - 0.58060*v;
				float b = y + 2.03211*u;

				//r = r * sContrastValue + sBrightnessValue;
				//g = g * sContrastValue + sBrightnessValue;
				//b = b * sContrastValue + sBrightnessValue;
				// We finally set the RGB color of our pixel
				return fixed4(r, g, b, 1.0);
			}

			float4 frag(vertexOutput input) : COLOR
			{
				float val0 = input.uv0.w;   
				float val1 = input.uv1.w;
				float val2 = input.uv2.w;
				float val3 = input.uv3.w;

				fixed4 color0 = getPixel(input.uv0.xy);
				fixed4 color1 = getPixel(input.uv1.xy);
				fixed4 color2 = getPixel(input.uv2.xy);
				fixed4 color3 = getPixel(input.uv3.xy);

				return color0*val0  + color1*val1 + color2*val2 + color3*val3;
			}			

			ENDCG
		}
	}
	FallBack "Diffuse"
}

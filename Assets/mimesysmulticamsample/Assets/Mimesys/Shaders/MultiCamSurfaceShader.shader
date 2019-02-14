// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MultiCamSurfaceShader" {
	Properties {
//		_Color ("Color", Color) = (1,1,1,1)
//		_MainTex ("Albedo (RGB)", 2D) = "white" {}
//		_Glossiness ("Smoothness", Range(0,1)) = 0.5
//		_Metallic ("Metallic", Range(0,1)) = 0.0

		_BlendWeights0("Weight 0", Range(0,1)) = 1.0
		_BlendWeights1("Weight 1", Range(0,1)) = 0.0
		_BlendWeights2("Weight 2", Range(0,1)) = 0.0

		_Texture0("Texture Cam 0 (RGB)", 2D) = "white" {}
		_Texture1("Texture Cam 1 (RGB)", 2D) = "white" {}
		_Texture2("Texture Cam 2 (RGB)", 2D) = "white" {}
	}
	SubShader {
		Cull Off

		Pass {
			CGPROGRAM

#pragma vertex vert  
#pragma fragment frag 

			sampler2D _Texture0;
			uniform float4x4 _World2Cam0;
			sampler2D _Texture1;
			uniform float4x4 _World2Cam1;
			sampler2D _Texture2;
			uniform float4x4 _World2Cam2;

			uniform float _BlendWeights0;
			uniform float _BlendWeights1;
			uniform float _BlendWeights2;

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
				//float4 color : COLOR;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;

				//input.blendWeights1.x = _BlendWeights0;
				//input.blendWeights1.y = _BlendWeights1;
				//input.blendWeights2.x = _BlendWeights2;

				//input.vertex.x *= -1;

				output.pos = UnityObjectToClipPos(input.vertex);

				float totalWeight = input.blendWeights1.x + input.blendWeights1.y + input.blendWeights2.x;
				output.uv0 = mul(_World2Cam0, input.vertex);
				output.uv0 = ((output.uv0 / output.uv0.z) + 1) / 2 ;
				output.uv0.y = 1- output.uv0.y;
				output.uv0.w = input.blendWeights1.x / totalWeight;
				output.uv1 = mul(_World2Cam1, input.vertex);
				output.uv1 = ((output.uv1 / output.uv1.z) + 1) / 2;
				output.uv1.y = 1 - output.uv1.y;
				output.uv1.w = input.blendWeights1.y / totalWeight;
				output.uv2 = mul(_World2Cam2, input.vertex); 
				output.uv2 = ((output.uv2 / output.uv2.z) + 1) / 2;
				output.uv2.y = 1 - output.uv2.y;
				output.uv2.w = input.blendWeights2.x / totalWeight;

				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				fixed4 color0 = tex2D(_Texture0, input.uv0.xy);				
				fixed4 color1 = tex2D(_Texture1, input.uv1.xy);
				fixed4 color2 = tex2D(_Texture2, input.uv2.xy);		
				float val0 = input.uv0.w;
				float val1 = input.uv1.w;
				float val2 = input.uv2.w;
				/*
				if (color0.x <0.05 && color0.y <0.05 && color0.z <0.05)
				{
					val0 = 0;
					float remainingWeight = val1 + val2;
					if (remainingWeight == 0)
						val0 = 1;
					else
					{
						val1 = val1 / remainingWeight;
						val2 = input.uv2.w / remainingWeight;
					}
				}
					//color0 = fixed4(1, 1, 1, 0);
				if (color1.x <0.05 && color1.y <0.05 && color1.z <0.05)
				{
					val1 = 0;
					float remainingWeight = val0 + val2;
					if (remainingWeight == 0)
						val1 = 1;
					else
					{
						val0 = val0 / remainingWeight;
						val2 = val2 / remainingWeight;
					}
				}
					//color1 = fixed4(1, 1, 1,0);
				if (color2.x <0.05 && color2.y <0.05 && color2.z <0.05)
				{
					val2 = 0;
					float remainingWeight = val0 + val1;
					if (remainingWeight == 0)
						val1 = 2;
					else
					{
						val0 = val0 / remainingWeight;
						val1 = val1 / remainingWeight;
					}
				}
				*/
					//color2 = fixed4(1, 1, 1, 0);
				fixed4 result = color0 * val0 + color1 * val1 + color2 * val2;
				return fixed4(result.x, result.y, result.z, result.w);

			//	return float4(input.uv0);
			}

			ENDCG
		}
/*		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
	//	#pragma vertex vert
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct vertexInput {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 texcoord : TEXCOORD0;
		};

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		Input vert(inout vertexInput input)
		{
			Input output;

			float4 camCoord = ComputeScreenPos(UnityObjectToClipPos(input.vertex));
			output.uv_MainTex = float2(1.0f, 1.0f);//float2(camCoord.x / camCoord.z, camCoord.y / camCoord.z);

			return output;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Albedo = c.rgb;
			o.Albedo = fixed3(IN.uv_MainTex.x, IN.uv_MainTex.y, 1);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 1;//c.a;
		}
		ENDCG
*/
	}
	FallBack "Diffuse"
}

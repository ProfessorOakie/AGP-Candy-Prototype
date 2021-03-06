﻿Shader "Custom/HealthFilling" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_FillColor("Fill Color", Color) = (1,0,0,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		float _FillPercent;
		int _FillAxis;
		float _FillAxisLength;

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _FillColor;

		struct Input {
			float2 uv_MainTex;
			float3 localPos;
		};

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.localPos = v.vertex.xyz;
		}


		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutput  o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			

			// TODO: calculate the 1.7f number (the half height) through code rather then hardcode it.
			float fillHeight = (-_FillPercent * _FillAxisLength) + (_FillAxisLength * 0.5f) - 0.001f;

			if (_FillAxis == 0)
			{
				if (IN.localPos.x > fillHeight)
					c = _FillColor;
			}
			else if(_FillAxis == 1)
			{
				if (IN.localPos.y > fillHeight)
					c = _FillColor;
			}
			else if (_FillAxis == 2)
			{
				if (IN.localPos.z > fillHeight)
					c = _FillColor;
			}
			else if (_FillAxis == 3)
			{
				if (IN.localPos.x < -fillHeight)
					c = _FillColor;
			}
			else if (_FillAxis == 4)
			{
				if (IN.localPos.y < -fillHeight)
					c = _FillColor;
			}
			else if (_FillAxis == 5)
			{
				if (IN.localPos.z < -fillHeight)
					c = _FillColor;
			}
			
			
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

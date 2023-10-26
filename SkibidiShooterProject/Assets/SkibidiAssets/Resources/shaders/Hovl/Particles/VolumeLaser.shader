Shader "Hovl/Particles/VolumeLaser" {
	Properties {
		[HideInInspector] _StartPoint ("StartPoint", Vector) = (0,1,0,0)
		_StartDistance ("Start Distance", Float) = 2
		_StartRound ("Start Round", Float) = 6
		[Toggle] _UseEndRound ("Use End Round", Float) = 1
		[HideInInspector] _EndPoint ("EndPoint", Vector) = (-10,1,0,0)
		_EndDistance ("End Distance", Float) = 2
		_EndRound ("End Round", Float) = 6
		_Distance ("Distance", Float) = 10
		_MainTex ("MainTex", 2D) = "white" {}
		_DissolveNoise ("Dissolve Noise", 2D) = "white" {}
		_MainTexTilingXYNoiseTilingZW ("MainTex Tiling XY Noise Tiling ZW", Vector) = (1,1,1,1)
		_SpeedMainTexUVNoiseZW ("Speed MainTex U/V + Noise Z/W", Vector) = (0,0,0,0)
		_Emission ("Emission", Float) = 2
		_Color ("Color", Vector) = (1,1,1,1)
		_Cutoff ("Mask Clip Value", Float) = 0.5
		_Dissolve ("Dissolve", Range(0, 1)) = 1
		_VertexPower ("Vertex Power", Float) = 0.3
		_TextureVertexPower ("Texture Vertex Power", Float) = 0.2
		[HideInInspector] _Scale ("Scale", Float) = 1
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}
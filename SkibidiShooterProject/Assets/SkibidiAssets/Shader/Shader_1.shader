Shader "" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_SrcBlend ("SrcBlend", Float) = 5
		_DstBlend ("DstBlend", Float) = 10
		_ZWrite ("ZWrite", Float) = 1
		_ZTest ("ZTest", Float) = 4
		_Cull ("Cull", Float) = 0
		_ZBias ("ZBias", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
}
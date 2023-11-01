Shader "SineVFX/TopDownEffects/DissolveParticleAdvanced" {
	Properties {
		_FinalColor ("Final Color", Vector) = (1,1,1,1)
		_FinalPower ("Final Power", Range(0, 60)) = 4
		_FinalOpacityPower ("Final Opacity Power", Range(0, 10)) = 1
		_FinalOpacityExp ("Final Opacity Exp", Range(0.2, 8)) = 1
		[Toggle(_RAMPENABLED_ON)] _RampEnabled ("Ramp Enabled", Float) = 0
		_Ramp ("Ramp", 2D) = "white" {}
		_RampColorTint ("Ramp Color Tint", Vector) = (1,1,1,1)
		_RampAffectedByDynamics ("Ramp Affected By Dynamics", Range(0, 1)) = 1
		_RampOffsetMultiply ("Ramp Offset Multiply", Float) = 1
		_RampOffsetExp ("Ramp Offset Exp", Range(0.2, 8)) = 1
		_RampIgnoreVertexColor ("Ramp Ignore Vertex Color", Range(0, 1)) = 0
		_CustomColorMask ("Custom Color Mask", 2D) = "white" {}
		_CustomColorMaskChannels ("Custom Color Mask Channels", Vector) = (1,0,0,0)
		_CustomColorMaskSwitch ("Custom Color Mask Switch", Range(0, 1)) = 0
		_CustomColorMaskAffectedByDynamics ("Custom Color Mask Affected By Dynamics", Range(0, 1)) = 1
		_MainMask ("Main Mask", 2D) = "white" {}
		_MainMaskScaleU ("Main Mask Scale U", Float) = 1
		_MainMaskScaleV ("Main Mask Scale V", Float) = 1
		_MainMaskChannelsMultiply ("Main Mask Channels Multiply", Vector) = (1,0,0,0)
		_MainMaskScrollSpeedU ("Main Mask Scroll Speed U", Float) = 0
		_MainMaskScrollSpeedV ("Main Mask Scroll Speed V", Float) = 0
		_MainMaskExp ("Main Mask Exp", Range(0.2, 4)) = 1
		_SecondMask ("Second Mask", 2D) = "white" {}
		_SecondMaskScaleU ("Second Mask Scale U", Float) = 1
		_SecondMaskScaleV ("Second Mask Scale V", Float) = 1
		_SecondMaskProfile ("Second Mask Profile", 2D) = "white" {}
		_SecondMaskNegate ("Second Mask Negate", Range(0, 1)) = 1
		_SecondMaskVSMoveU ("Second Mask VS Move U", Range(0, 1)) = 0
		_SecondMaskVSMoveV ("Second Mask VS Move V", Range(0, 1)) = 0
		_SecondMaskAffectsDistortion ("Second Mask Affects Distortion", Range(0, 1)) = 0
		_SecondMaskAffectsNoise01Negate ("Second Mask Affects Noise 01 Negate", Range(0, 1)) = 0
		_SecondMaskAffectsRamp ("Second Mask Affects Ramp", Range(0, 1)) = 0
		_SecondMaskBoostsEmission ("Second Mask Boosts Emission", Range(0, 40)) = 0
		_SecondMaskFractSwitch ("Second Mask Fract Switch", Range(0, 1)) = 0
		_SecondMaskFractShrink ("Second Mask Fract Shrink", Float) = 1
		_Noise01 ("Noise 01", 2D) = "white" {}
		_Noise01ScaleU ("Noise 01 Scale U", Float) = 1
		_Noise01ScaleV ("Noise 01 Scale V", Float) = 1
		_Noise01Negate ("Noise 01 Negate", Range(0, 1)) = 0
		_Noise01Exp ("Noise 01 Exp", Range(0.2, 8)) = 1
		_Noise01ScrollSpeedU ("Noise 01 Scroll Speed U", Float) = 0
		_Noise01ScrollSpeedV ("Noise 01 Scroll Speed V", Float) = 0
		_Noise01RandomMin ("Noise 01 Random Min", Range(0.5, 1)) = 0.9
		_Noise01RandomMax ("Noise 01 Random Max", Range(1, 1.5)) = 1.1
		[Toggle(_NOISE01RADIAL_ON)] _Noise01Radial ("Noise 01 Radial", Float) = 0
		_Noise01RadialScaleU ("Noise 01 Radial Scale U", Float) = 1
		_Noise01RadialScaleV ("Noise 01 Radial Scale V", Float) = 1
		_DissolveTexture ("Dissolve Texture", 2D) = "white" {}
		_DissolveTextureFlipSwitch ("Dissolve Texture Flip Switch", Range(0, 1)) = 1
		_DissolveTextureScaleU ("Dissolve Texture Scale U", Float) = 1
		_DissolveTextureScaleV ("Dissolve Texture Scale V", Float) = 1
		_DissolveTextureRandomMin ("Dissolve Texture Random Min", Range(0.5, 1)) = 0.9
		_DissolveTextureRandomMax ("Dissolve Texture Random Max", Range(1, 1.5)) = 1.1
		[Toggle(_DISSOLVETEXTURERADIAL_ON)] _DissolveTextureRadial ("Dissolve Texture Radial", Float) = 0
		_DissolveTextureRadialScaleU ("Dissolve Texture Radial Scale U", Float) = 1
		_DissolveTextureRadialScaleV ("Dissolve Texture Radial Scale V", Float) = 1
		_DissolveExp ("Dissolve Exp", Float) = 2
		_DissolveExpReversed ("Dissolve Exp Reversed", Float) = 2
		_DissolveGlowAmount ("Dissolve Glow Amount", Range(0, 120)) = 0
		_DissolveGlowPower ("Dissolve Glow Power", Range(0.2, 8)) = 1
		_DissolveGlowOffset ("Dissolve Glow Offset", Range(-1, 1)) = 0.125
		_DissolveGlowAffectsRamp ("Dissolve Glow Affects Ramp", Range(0, 1)) = 1
		_DissolveMask ("Dissolve Mask", 2D) = "black" {}
		_DissolveMaskSteepness ("Dissolve Mask Steepness", Range(0, 10)) = 0
		[Toggle(_DISTORTIONENABLED_ON)] _DistortionEnabled ("Distortion Enabled", Float) = 1
		_Distortion ("Distortion", 2D) = "white" {}
		_DistortionScaleU ("Distortion Scale U", Float) = 1
		_DistortionScaleV ("Distortion Scale V", Float) = 1
		_DistortionPower ("Distortion Power", Range(-1, 1)) = 0
		_DistortionRemapMin ("Distortion Remap Min", Range(-1, 0)) = 0
		_DistortionRemapMax ("Distortion Remap Max", Range(1, 2)) = 1
		_DistortionExp ("Distortion Exp", Range(0.2, 8)) = 1
		[Toggle(_DISTORTIONUVORSPHERICAL_ON)] _DistortionUVorSpherical ("Distortion UV or Spherical", Float) = 0
		_DistortionSpheticalStyle ("Distortion Sphetical Style", Range(0, 1)) = 0
		_DistortionU ("Distortion U", Range(0, 1)) = 1
		_DistortionV ("Distortion V", Range(0, 1)) = 1
		_DistortionScrollSpeedU ("Distortion Scroll Speed U", Float) = 0
		_DistortionScrollSpeedV ("Distortion Scroll Speed V", Float) = 0
		[Toggle(_DISTORTIONRADIAL_ON)] _DistortionRadial ("Distortion Radial", Float) = 0
		_DistortionRadialScaleU ("Distortion Radial Scale U", Float) = 1
		_DistortionRadialScaleV ("Distortion Radial Scale V", Float) = 1
		_DistortionRadialOldMethodSwitch ("Distortion Radial Old Method Switch", Range(0, 1)) = 0
		_DistortionMask ("Distortion Mask", 2D) = "white" {}
		_DistortionMaskNegate ("Distortion Mask Negate", Range(0, 1)) = 0
		_DistortionMaskVSMoveU ("Distortion Mask VS Move U", Range(0, 1)) = 0
		_DistortionMaskVSMoveV ("Distortion Mask VS Move V", Range(0, 1)) = 0
		[Toggle(_EMISSIONVERTEXSTREAMENABLED_ON)] _EmissionVertexStreamEnabled ("Emission Vertex Stream Enabled", Float) = 0
		[Toggle(_SOFTPARTICLESENABLED_ON)] _SoftParticlesEnabled ("Soft Particles Enabled", Float) = 0
		_SoftParticlesDistance ("Soft Particles Distance", Float) = 0.2
		[HideInInspector] _tex4coord3 ("", 2D) = "white" {}
		[HideInInspector] _tex4coord2 ("", 2D) = "white" {}
		[HideInInspector] _tex4coord ("", 2D) = "white" {}
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	//CustomEditor "ASEMaterialInspector"
}
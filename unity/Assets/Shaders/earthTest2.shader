Shader "EarthTest"
{

      //Earth Shader created by Julien Lynge @ Fragile Earth Studios
      //Upgrade of a shader originally put together in Strumpy Shader Editor by Clamps
      //Feel free to use and share this shader, but please include this attribution

	Properties 
	{
_mainTex("_mainTex", 2D) = "black" {}
_LightScale("_LightScale", Float) = 3
_LightColor("_LightColor", 2D) = "black" {}
_AtmosFalloff("_AtmosFalloff", Float) = 1.8
_AtmosNear("_AtmosNear", Color) = (0.3391624,0.6232321,0.6492537,1)
_AtmosFar("_AtmosFar", Color) = (0.03731346,0.9596077,1,1)

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert
#pragma target 2.0


sampler2D _mainTex;
float _LightScale;
sampler2D _LightColor;
float _AtmosFalloff;
float4 _AtmosNear;
float4 _AtmosFar;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
float4 Luminance0= Luminance( light.xyz ).xxxx;
float4 Multiply0=float4( s.Albedo.x, s.Albedo.y, s.Albedo.z, 1.0 ) * Luminance0;
float4 Invert0= float4(1.0, 1.0, 1.0, 1.0) - Luminance0;
float4 Multiply2=Invert0 * float4( s.Emission.x, s.Emission.y, s.Emission.z, 1.0 );
float4 Add0=Multiply0 + Multiply2;
return Add0;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float3 viewDir;
float2 uv_mainTex;
float2 uv_LightColor;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);


			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Fresnel0_1_NoInput = float4(0,0,1,1);
float4 Fresnel0=(1.0 - dot( normalize( float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ).xyz), normalize( Fresnel0_1_NoInput.xyz ) )).xxxx;
float4 Pow0=pow(Fresnel0,_AtmosFalloff.xxxx);
float4 Saturate0=saturate(Pow0);
float4 Lerp0=lerp(_AtmosNear,_AtmosFar,Saturate0);
float4 Multiply0=Lerp0 * Saturate0;
float4 Sampled2D1=tex2D(_mainTex,IN.uv_mainTex.xy);
float4 Add0=Multiply0 + Sampled2D1;
float4 Sampled2D0=tex2D(_LightColor,IN.uv_LightColor.xy);
//float4 Multiply1=Sampled2D0 * _LightScale.xxxx;

o.Albedo = Add0;
o.Emission = tex2D(_LightColor,IN.uv_LightColor.xy).r * _LightScale * 0.5f;
o.Custom = tex2D(_LightColor,IN.uv_LightColor.xy).r * _LightScale;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}
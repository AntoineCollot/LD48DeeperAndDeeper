  Shader "Custom/LightRay" {
  Properties {
	_Color ("Color", Color) = (1,1,1,1)
    }
    SubShader {
      Tags { "RenderType" = "Transparent" }
	       
	  	  Blend One One
      CGPROGRAM
      #pragma surface surf Lambert
	   #pragma multi_compile_fog

      struct Input {
          float4 color : COLOR;
      };
	  fixed4 _Color;
	  
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = _Color;
      }
      ENDCG
    }
    Fallback "Diffuse"
  }
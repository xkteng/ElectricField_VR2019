Shader "Custom/SphereSurface"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Radius("Radius",Range(0,10))=1
        _ScaleFactor("ScaleFactor",Range(0,10))=1


    }
    SubShader
    {
    	Pass {
        Tags { 
        "RenderType"="Opaque" 
        "LightMode" = "ForwardBase"
    }
        LOD 200

        CGPROGRAM

        #pragma vertex vert
        #pragma fragment  frag
        #include "UnityCG.cginc" 
        #include "Lighting.cginc"


        struct v2f{
        	float4 pos:SV_POSITION;
        	float3 worldNormal:TEXCOORD0;
        };
        
        fixed4 _Color;
        fixed _Radius;
        fixed _ScaleFactor;

       v2f vert(appdata_base v){
       		v2f o;

       		fixed temp=pow(_Radius,2)-pow(v.vertex.x/_ScaleFactor,2)-pow(v.vertex.y/_ScaleFactor,2);
       		if (temp>=0)
       		{
       			v.vertex.z=sqrt(temp)*_ScaleFactor;
          o.worldNormal=UnityObjectToWorldDir(fixed3(v.vertex.x,v.vertex.y,v.vertex.z));
       		}else{
            o.worldNormal=UnityObjectToWorldDir(fixed3(0,0,1));
          }
       		o.pos=UnityObjectToClipPos(v.vertex);
       		return o;
       }

       fixed4 frag(v2f i):SV_Target{
       		fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
       		fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
       		float3 diffuse = _Color.rgb * _LightColor0.rgb * (dot(i.worldNormal,lightDir)*0.5+0.5);
       		return float4(ambient+diffuse,1.0);

       }

        
        ENDCG
       		}
    }

    FallBack "Diffuse"
}

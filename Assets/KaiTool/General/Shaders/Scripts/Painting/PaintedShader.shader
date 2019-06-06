// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "KaiTool/PaintedShader" {
    Properties {
        _Albedo("Albedo",color)=(1,1,1,1)
        _MainTex("Base (RGB)",2D) = "white"{}
        _Metallic("Metallic",Range(0,1))=0.0
        _Smoothness("Smoothness",Range(0,1))=1.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
       

        half4 _Albedo;
        half _Metallic;
        half _Smoothness;

        uniform int _PointLength;
        uniform float4 _Points[1000];
        uniform float _PaintingCoeffcient;
        uniform float _PaintRadius;

        struct Input {
            float2 uv_MainTex;
            fixed3 pos;
          // / float4 pos;
        };

        void vert(inout appdata_full v,out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input,o);
            o.pos=mul(unity_ObjectToWorld, v.vertex).xyz;
           //o.pos=v.vertex.xyz;
        }

        void surf (Input IN, inout SurfaceOutputStandard o) {
            half4 c = tex2D(_MainTex,IN.uv_MainTex);
            half dis=distance(IN.pos,_Points[0].xyz);
            o.Metallic=_Metallic;
            o.Smoothness=_Smoothness;
            o.Albedo = _Albedo.xyz*c;
            o.Alpha = c.a;
            if(dis<_PaintRadius&&dis>_PaintRadius*9.0/10.0){
                o.Albedo=float3(0,0,1);
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}
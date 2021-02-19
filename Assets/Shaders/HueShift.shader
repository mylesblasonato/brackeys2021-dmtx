// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "HSB_HSV_Colorpicker" {
    Properties{
        _TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
        _MainTex("Texture", 2D) = "white" {}
        _HueShift("HueShift", Range(0,1)) = 0
        _Sat("Saturation", Range(0, 1)) = 0
    }
        SubShader{

            Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0

                #include "UnityCG.cginc"

                float3 rgb_to_hsv_no_clip(float3 RGB)
                {
                    float3 HSV = 0;

                    float minChannel, maxChannel;

                    maxChannel = max(RGB.x, RGB.y);
                    minChannel = min(RGB.x, RGB.y);

                    maxChannel = max(RGB.z, maxChannel);
                    minChannel = min(RGB.z, minChannel);

                    HSV.z = maxChannel;

                    float delta = maxChannel - minChannel;             //Delta RGB value

                    //if ( delta > 0 )
                    //{                    // If gray, leave H  S at zero
                        HSV.y = delta / HSV.z;
                        float3 delRGB = (HSV.zzz - RGB + 3 * delta) / (6 * delta);
                        if (RGB.x == HSV.z) HSV.x = delRGB.z - delRGB.y;
                        if (RGB.y == HSV.z) HSV.x = (1.0 / 3.0) + delRGB.x - delRGB.z;
                        if (RGB.z == HSV.z) HSV.x = (2.0 / 3.0) + delRGB.y - delRGB.x;
                        //}

                        return (HSV);
                    }

                    float3 hsv_to_rgb(float3 HSV)
                    {
                        float var_h = HSV.x * 6;
                        //float var_i = floor(var_h);   // Or ... var_i = floor( var_h )
                        float var_1 = HSV.z * (1.0 - HSV.y);
                        float var_2 = HSV.z * (1.0 - HSV.y * (var_h - floor(var_h)));
                        float var_3 = HSV.z * (1.0 - HSV.y * (1 - (var_h - floor(var_h))));

                        float3 RGB = float3(HSV.z, var_1, var_2);

                        if (var_h < 5) { RGB = float3(var_3, var_1, HSV.z); }
                        if (var_h < 4) { RGB = float3(var_1, var_2, HSV.z); }
                        if (var_h < 3) { RGB = float3(var_1, HSV.z, var_3); }
                        if (var_h < 2) { RGB = float3(var_2, HSV.z, var_1); }
                        if (var_h < 1) { RGB = float3(HSV.z, var_3, var_1); }

                        return (RGB);
                    }

                    struct v2f {
                        float4  pos : SV_POSITION;
                        float2  uv : TEXCOORD0;
                    };

                    float4 _MainTex_ST;

                    v2f vert(appdata_base v)
                    {
                        v2f o;
                        o.pos = UnityObjectToClipPos(v.vertex);
                        o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                        return o;
                    }

                    sampler2D _MainTex;
                    float _HueShift;
                    float _Sat;

                    half4 frag(v2f i) : COLOR
                    {
                        half4 col = tex2D(_MainTex, i.uv);

                        float3 hsv = rgb_to_hsv_no_clip(col.xyz);
                        hsv.x += _HueShift;
                        //hsv.y *= _Sat;

                        if (hsv.x > 1.0) { hsv.x -= 1.0; }
                        return half4(half3(hsv_to_rgb(hsv)), col.a);
                    }

                    ENDCG
                }
        }
            Fallback "Particles/Alpha Blended"
}
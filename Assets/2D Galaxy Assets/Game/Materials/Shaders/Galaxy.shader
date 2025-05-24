Shader "FX/Galaxy"
{
    Properties
    {
        _MainTex ("Texture Sampler (UV Only)", 2D) = "grey" {}
        _Zoom ("Zoom", Float) = 800
        _Color ("Main Color", Color) = (1,1,1,1)	
        [Toggle(CLAMPOUT)] _CLAMPOUT("Clamp Output with Main Color", Float) = 0
        _Scroll ("Scrolling direction (x,y,z) * w * time", Vector) = (3, 1, .6, .01)
        _Center ("Center Position (x, y, z, time)", Vector) = (1, .3, .5, 0)
        _Rotation ("Rotation (x,y,z)*w angles", Vector) = (35, 25, 75, .1)
        _Iterations ("Iterations", Range(1, 30)) = 17
        _Volsteps ("Volumetric Steps", Range(1,40)) = 20
        _Formuparam ("Formuparam", Float) = 530
        _StepSize ("Step Size", Float) = 130
        _Tile ("Tile", Float) = 700
        _Brightness ("Brightness", Float) = 2
        _Darkmatter ("Dark Matter", Float) = 25
        _Distfading ("Distance Fading", Float) = 68
        _Saturation ("Saturation", Float) = 85
    }

    SubShader
    {
        Tags { "Queue"="Geometry" "RenderType"="Opaque" }
        LOD 200
        Cull Off

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0
        #pragma multi_compile __ CLAMPOUT

        sampler2D _MainTex;
        fixed4 _Color;
        float4 _Scroll;
        float4 _Center;
        float4 _Rotation;
        float _Zoom;
        float _Formuparam;
        float _StepSize;
        float _Tile;
        float _Brightness;
        float _Darkmatter;
        float _Distfading;
        float _Saturation;
        int _Iterations;
        int _Volsteps;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 col = float3(0, 0, 0);
            float zoom = _Zoom / 1000.0;
            float2 uv = IN.uv_MainTex - 0.5;
            float3 dir = normalize(float3(uv * zoom, 1.0));
            float time = _Center.w + _Time.y;

            float brightness = _Brightness / 1000.0;
            float stepSize = _StepSize / 1000.0;
            float3 tile = abs(float3(_Tile, _Tile, _Tile)) / 1000.0;
            float formparam = _Formuparam / 1000.0;
            float darkmatter = _Darkmatter / 100.0;
            float distFade = _Distfading / 100.0;

            float3 from = _Center.xyz + _Scroll.xyz * _Scroll.w * time;

            float3 rot = radians(_Rotation.xyz * _Rotation.w);
            if (length(rot) > 0.0)
            {
                float2x2 rx = float2x2(cos(rot.x), sin(rot.x), -sin(rot.x), cos(rot.x));
                float2x2 ry = float2x2(cos(rot.y), sin(rot.y), -sin(rot.y), cos(rot.y));
                float2x2 rz = float2x2(cos(rot.z), sin(rot.z), -sin(rot.z), cos(rot.z));
                dir.xy = mul(rz, dir.xy);
                dir.xz = mul(ry, dir.xz);
                dir.yz = mul(rx, dir.yz);
                from.xy = mul(rz, from.xy);
                from.xz = mul(ry, from.xz);
                from.yz = mul(rx, from.yz);
            }

            float s = 0.1, fade = 1.0;
            float3 v = float3(0, 0, 0);

            for (int r = 0; r < _Volsteps; r++)
            {
                float3 p = abs(from + s * dir * 0.5);
                p = abs(tile - fmod(p, tile * 2.0));
                float pa = 0.0, a = 0.0;

                for (int i = 0; i < _Iterations; i++)
                {
                    p = abs(p) / dot(p, p) - formparam;
                    a += abs(length(p) - pa);
                    pa = length(p);
                }

                float dm = max(0.0, darkmatter - a * a * 0.001);
                if (r > 6) fade *= 1.0 - dm;

                a *= a * a;
                v += fade;
                v += float3(s, s*s, s*s*s*s) * a * brightness * fade;
                fade *= distFade;
                s += stepSize;
            }

            float len = length(v);
            v = lerp(float3(len, len, len), v, _Saturation / 100.0);
            v *= _Color.rgb * 0.01;

            #ifdef CLAMPOUT
                v = clamp(v, float3(0, 0, 0), _Color.rgb);
            #endif

            o.Albedo = 0;
            o.Emission = v * 0.01;
            o.Metallic = 0;
            o.Smoothness = 0;
        }
        ENDCG
    }
    FallBack Off
}

// murasanca

Shader "murasanca/IES" // Image Effect Shader
{
    Properties
    {
        _MainTex("Texture",2D)="white"{}
        bS("Blur Size",int)=64
        sS("Square Size",int)=1
        tM("Time Multiplier",int)=1
        wF("Wave Frequency",int)=8
        wS("Wave Speed",int)=1
    }
    SubShader
    {
        LOD 512
        Tags{"RenderType"="Transparent"}
        
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex v    // Vertex
            #pragma fragment f // Fragment

            #include "UnityCG.cginc"
            
            int
                bS,                   // Blur Size
                sS,                  // Square Size
                tM,                 // Time Multiplier
                wF,                // Wave Frequency
                wS;               // Wave Speed
            float4 _MainTex_ST;  // Texture Transform
            sampler2D _MainTex; // Texture Sampler

            struct a // Apply
            {
                float2 u:TEXCOORD0; // UV Coordinates
                float4             //
                    c:COLOR,      // Vertex Color
                    v:POSITION;  // Vertex Position
            };

            struct v2f // Vertex to Fragment
            {
                float2 u:TEXCOORD0;   // UV Coordinates
                float4               //
                    c:COLOR,        // Vertex Color
                    v:SV_POSITION; // Screen Space Position
            };

            v2f v(a i)
            {
                v2f o;
                o.v=UnityObjectToClipPos(i.v);
                o.u=TRANSFORM_TEX(i.u,_MainTex);
                o.c=i.c;
                return o;
            }
            
            float3 AB(sampler2D t,float2 u,float bR) // Apply Blur
            {
                float2 o=.001*bR;         // Offset
                float3 r=tex2D(t,u).rgb; // Result

                r+=tex2D(t,u+float2(o.x,0)).rgb;
                r+=tex2D(t,u-float2(o.x,0)).rgb;
                r+=tex2D(t,u+float2(0,o.y)).rgb;
                r+=tex2D(t,u-float2(0,o.y)).rgb;

                return r/5;
            }

            fixed4 f(v2f i):SV_Target // Fragment
            {
                float2 u=i.u;
                float2 c=float2(.5,.5);  // Center
                float2 d=abs(u-c);      // Distance
                float mD=max(d.x,d.y); // Max Distance

                bool iS= // In Square
                (
                    d.x<=.5*sS
                    &&
                    d.y<=.5*sS
                );

                float bF=smoothstep(0,1,(mD-.5*sS)/(1-.5*sS)); // Blur Factor
                                                              //
                float w=sin((_Time.y*wS-mD*wF)*tM);          // Wave
                float wE=.5*smoothstep(0,1,w);              // Wave Effect

                if(iS)
                    return tex2D(_MainTex,u)*i.c;
                else
                {
                    float3 b=AB(_MainTex,u,(bF+wE)*bS); // Blur
                    float4 tC=tex2D(_MainTex,u);       // Texture Color
                                                      //
                    float a=smoothstep(0.0,1.0,mD);  // Alpha
                    return float4(b,a*tC.a)*i.c;
                }
            }
            ENDCG
        }
    }
    Fallback"Diffuse"
}

// murasanca
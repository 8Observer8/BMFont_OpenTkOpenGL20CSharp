#version 110

uniform sampler2D uSampler;
uniform vec4 uColor;

varying vec2 vTexCoord;

void main()
{
    // Texel color look up based on interpolated UV value in vTexCoord
    vec4 c = texture2D(uSampler, vTexCoord);
    // Tint the textured area, and leave transparent area as defined by the texture
    vec3 r = vec3(c) * (1.0-uColor.a) + vec3(uColor) * uColor.a;
    vec4 result = vec4(r, c.a);
    gl_FragColor = result;
}
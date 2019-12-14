#version 110

attribute vec3 aPosition;
attribute vec2 aTexCoord;
varying vec2 vTexCoord;
uniform mat4 uVPMatrix;
uniform mat4 uModelMatrix;

void main()
{
    gl_Position = uVPMatrix * uModelMatrix * vec4(aPosition, 1.0);
    vTexCoord = aTexCoord;
}
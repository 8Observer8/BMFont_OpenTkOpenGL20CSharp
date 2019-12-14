#version 110

attribute vec3 aPosition;
uniform mat4 uVPMatrix;
uniform mat4 uModelMatrix;

void main()
{
    gl_Position = uVPMatrix * uModelMatrix * vec4(aPosition, 1.0);
}

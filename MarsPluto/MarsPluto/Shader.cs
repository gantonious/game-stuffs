using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.IO;
using OpenTK;

namespace MarsPluto
{
    class ShaderCompilationFailedException : Exception
    {
        public ShaderCompilationFailedException(string message = "") : base(message) { }

    }

    class Shader : IDisposable
    {
        private int shaderProgramID;
        private int vertexShaderID;
        private int fragmentShaderID;
        private Dictionary<string, int> uniforms = new Dictionary<string, int>();

        public Shader(string vertexShaderPath, string fragmentShaderPath)
        {
            vertexShaderID = loadShader(vertexShaderPath, ShaderType.VertexShader);
            fragmentShaderID = loadShader(fragmentShaderPath, ShaderType.FragmentShader);
            shaderProgramID = GL.CreateProgram();
            GL.AttachShader(shaderProgramID, vertexShaderID);
            GL.AttachShader(shaderProgramID, fragmentShaderID);
            bindAttributes();
            GL.LinkProgram(shaderProgramID);
            GL.ValidateProgram(shaderProgramID);
            getUniforms();
        }

        public void bindAttributes()
        {
            bindAttribute(0, "position");
            bindAttribute(1, "textureCoords");
        }

        public void getUniforms()
        {
            uniforms["transformationMatrix"] = getUniform("transformationMatrix");
        }

        public void loadWorldRef(Matrix4 matrix)
        {
            loadMatrix(uniforms["transformationMatrix"], matrix);
        }

        public void loadOrthoMatrix(Matrix4 matrix)
        {
            loadMatrix(uniforms["orthoMatrix"], matrix);
        }

        public int getUniform(string uniformName)
        {
            return GL.GetUniformLocation(shaderProgramID, uniformName);
        }

        public void loadArrayOfFloats(int location, float[] floats)
        {
            
        }
        public void loadVector(int location, Vector3 vector)
        {
            GL.Uniform3(location, vector.X, vector.Y, vector.Z);
        }

        public void loadMatrix(int location, Matrix4 matrix)
        {
            GL.UniformMatrix4(location, false, ref matrix);
        }

        public void start()
        {
            GL.UseProgram(shaderProgramID);
        }

        public void stop()
        {
            GL.UseProgram(0);
        }

        public void bindAttribute(int attributeID, string variable)
        {
            GL.BindAttribLocation(shaderProgramID, attributeID, variable);
        }

        public int loadShader(string filename, ShaderType shaderType)
        {

            string fileContent = "";
            try
            {
                using (StreamReader file = new StreamReader(filename))
                {
                    fileContent = file.ReadToEnd();
                }
                   
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found");
            }

            int shaderID = GL.CreateShader(shaderType);
            GL.ShaderSource(shaderID, fileContent);
            GL.CompileShader(shaderID);

            int output;
            GL.GetShader(shaderID, ShaderParameter.CompileStatus, out output);
            
            if (output != 1) throw new ShaderCompilationFailedException(GL.GetShaderInfoLog(shaderID));

            return shaderID;

        }

        public void Dispose()
        {
            GL.UseProgram(0);
            GL.DetachShader(shaderProgramID, vertexShaderID);
            GL.DetachShader(shaderProgramID, fragmentShaderID);
            GL.DeleteShader(vertexShaderID);
            GL.DeleteShader(fragmentShaderID);
            GL.DeleteProgram(shaderProgramID);
        }
    }
}

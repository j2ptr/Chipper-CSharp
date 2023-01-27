using System;
using Chipper_CSharp.renderer;
using GLFW;
using static Chipper_CSharp.OpenGL.GL;
using static Chipper_CSharp.renderer.Renderer;

namespace Chipper_CSharp
{
    class Program {
        public static void Main(string[] args)
        {
            //Initiating our renderer class & window
            Renderer r = new Renderer();
            r.CreateWindow(10, "Chipper");

            //Useless loop for Window testing
            while (!Glfw.WindowShouldClose(r.Canvas) == true){
                Glfw.WaitEvents();
            }
        }
    }
}
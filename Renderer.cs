using System;
using System.Collections;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using GLFW;
using static Chipper_CSharp.OpenGL.GL;

namespace Chipper_CSharp.renderer
{
    public class Renderer
    {
        //creating class variables for interop between functions
        public Window Canvas { get; set; }
        public static Vector2 WindowSize { get; set; }

        private int scale;

        //Take note! The resolution of the Chip-8 is widescreen, 64w x 32h, but the original device and ROM's are coded to work with 64 columns and 32 rows, IE, it's rendered 'sideways' in the hardware.
        private static readonly int cols = 64;
        private static readonly int rows = 32;

        BitArray display = new BitArray(cols * rows, false);

        public void CreateWindow(int scale, string title)
        {
            //Window size is guided by scaling based on 64x32 Chip-8 specifications (Hard coded in several places further down)
            WindowSize = new Vector2((64 * scale), (32 * scale));
            this.scale = scale;
            //Initialize Glfw
            Glfw.Init();

            //Windowing options
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Focused, true);
            Glfw.WindowHint(Hint.Resizable, false);

            this.Canvas = Glfw.CreateWindow(64 * scale, 32 * scale, title, GLFW.Monitor.None, Window.None);

            //check for Windowing error
            if (Canvas == Window.None)
            {
                //Window creation error
                return;
            }

            //Calculate screensize and set the window to be in the middle of the primary monitor
            Rectangle screen = Glfw.PrimaryMonitor.WorkArea;
            int x = (screen.Width - (64 * scale)) / 2;
            int y = (screen.Height - (32 * scale)) / 2;
            Glfw.SetWindowPosition(Canvas, x, y);

            //Focus window
            Glfw.MakeContextCurrent(Canvas);
            Import(Glfw.GetProcAddress);

            glViewport(0, 0, 64 * scale, 32 * scale);

            //0 or 1 to disable / enable VSync
            Glfw.SwapInterval(0);

        }

        public static void CloseWindow()
        {
            Glfw.Terminate();
        }

        //function to set the pixels in our previously initiated display array
        public bool SetPixel(int x, int y)
        {
            //The technical reference tells us if a pixel is positioned outside of the display array bounds we must wrap it to the opposite side
            if (x > cols)
            {
                x -= cols;
            } else if (x < 0)
            {
                x += cols;
            }

            if (y > rows) {
                y -= rows;
            } else if (y < 0)
            {
                y += rows;
            }
            //Calculates the pixel location
            int pixelLoc = x + (y * cols);
            //Sets the display bit in the bit array by XOR'ing it with a 1 or true
            this.display[pixelLoc] ^= true;
            //return whether the display erased or created a pixel in this instruction (true == erased; false == no action)
            //this will be important for future instructions
            return !this.display[pixelLoc];
        }
        //Self explanatory, resets the display array
        public void Clear()
        {
            this.display = new BitArray(cols * rows, false);
        }




    }
}


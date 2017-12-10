using HeatWave;
using System;
using System.Diagnostics;
using System.Threading;
using System.IO;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace HeatWaveTest
{
    class Program
    {
        public static void Main()
        {
            SceneManager sceneManager = new SceneManager(700, 400);
            sceneManager.PushScene(new MainGame(sceneManager));
            sceneManager.Run();
        }        
    }
}

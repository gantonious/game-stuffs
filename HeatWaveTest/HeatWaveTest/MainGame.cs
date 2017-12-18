using HeatWave;
using HeatWave.Graphics;
using HeatWave.Audio;
using System.IO;
using System.Collections.Generic;
using HeatWaveTest.EntityFramework;
using HeatWaveTest.EntityFramework.Components;
using HeatWaveTest.EntityFramework.Systems;

namespace HeatWaveTest
{
    public class MainGame : Scene
    { 
        private Renderer renderer;
        private List<Source> sources;
        private AudioManager audioManager;
        private Engine engine;

        public MainGame(SceneManager sceneManager) : base(sceneManager)
        {
            Initialize();
        }

        private void Initialize()
        {
            audioManager = new AudioManager();
            renderer = new ImmediateModeRenderer();
            sources = new List<Source>();

            engine = new Engine();
            engine.RegisterLogicalSystem(new MovementSystem());
            engine.RegisterLogicalSystem(new ScriptingSystem());
            engine.RegisterLogicalSystem(new InputSystem());
            engine.RegisterLogicalSystem(new RainSystem());
            //engine.RegisterLogicalSystem(new CollisionSystem());
            engine.RegisterLogicalSystem(new CollisionHandler());
            //engine.RegisterRenderingSystem(new RenderingSystem(renderer));

            var playerEntity = engine.CreateEntity();
            playerEntity.AddComponent(new Position(0, 0));
            playerEntity.AddComponent(new Input(new KeyboardAndMouse()));
            playerEntity.AddComponent(new Velocity(0, 0));
            playerEntity.AddComponent(new SpriteComponent(new Sprite(0, 0, 20, 20, SceneManager.AssetManager.LoadTexture("Assets/Images/heart.png"))));
            playerEntity.AddComponent(new Player());

            for (int i = 1; i < 100000; i++)
            {
                var rainEntity = engine.CreateEntity();
                rainEntity.AddComponent(new Position(i * 40, 0));
                rainEntity.AddComponent(new Velocity(0, 1));
                rainEntity.AddComponent(new SpriteComponent(new Sprite(0, 0, 20, 20, SceneManager.AssetManager.LoadTexture("Assets/Images/heart.png"))));
                rainEntity.AddComponent(new Rain());
            }

            sources.Add(new Source(SceneManager.AssetManager.LoadWave("Assets/Audio/main game.wav")));
            //sources[0].Play();
            sources[0].SetPosition(350, 200);
            sources[0].ReferenceDistance = 200f;
            //sources[0].SetVelocity(0, 0);
        }

        public override void Update(double delta)
        {
            engine.Update(delta);
        }
     
        public override void Render()
        {
            engine.Render();
            SceneManager.Title = "fps: " + SceneManager.RenderFrequency;
        }
    }
}

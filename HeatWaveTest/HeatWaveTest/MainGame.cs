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
        private List<Sprite> sprites;
        private List<Source> sources;
        private double lastAngle = -1;
        private AudioManager audioManager;
        private List<Entity> entities;
        private MovementSystem movementSystem;
        private RenderingSystem renderingSystem;
        private ScriptingSystem scriptingSystem;
        private InputSystem inputSystem;
        private CollisionSystem collisionSystem;
        private CollisionHandler collisionHandler;

        public MainGame(SceneManager sceneManager) : base(sceneManager)
        {
            Initialize();
        }

        private void Initialize()
        {
            audioManager = new AudioManager();
            renderer = new ImmediateModeRenderer();
            sprites = new List<Sprite>();
            sources = new List<Source>();
            entities = new List<Entity>();

            string script = "";

            using (StreamReader file = new StreamReader("Assets/Scripts/movement_script.lua"))
            {
                script = file.ReadToEnd();
            }

            Entity entity = new Entity(0);
            entity.AddComponent(new Position(0, 0));
            entity.AddComponent(new Input(new KeyboardAndMouse()));
            entity.AddComponent(new Velocity(0, 0));
            entity.AddComponent(new SpriteComponent(new Sprite(0, 0, 20, 20, SceneManager.AssetManager.LoadTexture("Assets/Images/heart.png"))));
            entity.AddComponent(new Player());
            entities.Add(entity);

            for (int i = 1; i < 2000; i++)
            {
                entity = new Entity(i);
                entity.AddComponent(new Position(i * 40, 0));
                entity.AddComponent(new Velocity(0, 1));
                //entity.AddComponent(new SpriteComponent(new Sprite(0, 0, 20, 20, SceneManager.AssetManager.LoadTexture("Assets/Images/heart.png"))));
                entity.AddComponent(new Rain());
                entities.Add(entity);
            }

            movementSystem = new MovementSystem();
            scriptingSystem = new ScriptingSystem();
            renderingSystem = new RenderingSystem(renderer);
            inputSystem = new InputSystem();
            collisionSystem = new CollisionSystem();
            collisionHandler = new CollisionHandler();

            sources.Add(new Source(SceneManager.AssetManager.LoadWave("Assets/Audio/main game.wav")));
            sources[0].Play();
            sources[0].SetPosition(350, 200);
            sources[0].ReferenceDistance = 200f;
            //sources[0].SetVelocity(0, 0);

        }

        public override void Update(double delta) {
            Position pos = entities[0].GetComponent<Position>();
            audioManager.SetListenerVelocity(pos.X, pos.Y);

            collisionSystem.Begin();
            foreach (Entity entity in entities)
            {
                inputSystem.Process(entity);
                movementSystem.Process(entity);
                scriptingSystem.Process(entity);
                collisionSystem.Process(entity);
                collisionHandler.Process(entity);
            }
            collisionSystem.End();
        }
     
        public override void Render()
        {
            renderingSystem.Begin();
            foreach (Entity entity in entities) renderingSystem.Process(entity);
            renderingSystem.End();
        }

        
    }
}

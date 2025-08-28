using Arch.Core;
using Arch.System;

namespace Lunar
{
    public abstract class GameControllerBase
    {
        protected World _mainWorld;
        protected Group<float> _systems;

        public void Initialize()
        {
            _mainWorld = World.Create();
            _systems = CreateSystems(_mainWorld);

            SetEvents(_mainWorld);
            _systems.Initialize();
        }

        public void Update(float deltaTime)
        {
            _systems.BeforeUpdate(in deltaTime);
            _systems.Update(in deltaTime);
        }

        public void LateUpdate(float deltaTime)
        {
            _systems.AfterUpdate(in deltaTime);
        }

        public void Dispose()
        {
            _systems.Dispose();
            _mainWorld.Dispose();
        }

        /// <summary>
        ///     Define the system to register
        /// </summary>
        protected abstract Group<float> CreateSystems(World world);

        /// <summary>
        ///     Define event binding
        /// </summary>
        protected abstract void SetEvents(World world);
    }
}
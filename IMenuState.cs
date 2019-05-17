using System;
using System.Collections.Generic;

namespace csharp_sfml
{
    public interface IMenuState : IGameState
    {
        // void Update();
        // void Render();
        // bool OnEnter();
        // bool OnExit();
        // string GetStateID();
        void SetCallBacks();
    }
}

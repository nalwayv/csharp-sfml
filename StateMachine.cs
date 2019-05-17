using System.Collections.Generic;

namespace csharp_sfml
{
    public class StateMachine
    {
        List<IGameState> gamestates;

        public StateMachine()
        {
            gamestates = new List<IGameState>();
        }

        /// <summary>
        ///   Push a new state onto the stack
        /// </summary>
        public void PushState(IGameState state)
        {
            gamestates.Add(state);

            // call its onEnter
            var lastID = gamestates.Count - 1;
            gamestates[lastID].OnEnter();

        }

        /// <summary>
        ///   Pop last state
        /// </summary>
        public void PopState()
        {
            if (gamestates.Count == 0) return;

            var lastID = gamestates.Count - 1;

            if (gamestates[lastID].OnEnter())
            {
                gamestates.RemoveAt(lastID);
            }
        }

        /// <summary>
        ///   Change current state
        /// </summary>
        public void ChangeState(IGameState state)
        {
            if (gamestates.Count != 0)
            {

                var lastID = gamestates.Count - 1;

                // trying to change to the same state.
                if (state.GetStateID() == gamestates[lastID].GetStateID())
                {
                    return;
                }

                if (gamestates[lastID].OnExit())
                {
                    gamestates.RemoveAt(lastID);
                }
            }

            // add and call its onEnter
            gamestates.Add(state);
            var lastIdx = gamestates.Count - 1;
            gamestates[lastIdx].OnEnter();
        }

        /// <summary>
        ///   Update current state
        /// </summary>
        public void Update()
        {
            if (gamestates.Count != 0)
            {
                var lastID = gamestates.Count - 1;
                gamestates[lastID].Update();
            }
        }

        /// <summary>
        ///   Render current state
        /// </summary>
        public void Render()
        {
            if (gamestates.Count != 0)
            {
                var lastID = gamestates.Count - 1;
                gamestates[lastID].Render();
            }
        }
    }
}

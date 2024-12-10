using System;
using System.Collections.Generic;
using Controllers;
using Core;
using GameStates;
using So;
using UI.PopUps;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private GameStateBase _currentState;
        
        private UIState _uIState;
        private InGameState _inGameState;
        
        public PopUpController popUpController;
        public SoundController soundController;
        public GamePlayConfigurations gamePlayConfigurations;

        private Dictionary<Type, GameStateBase> _states;

        public void Start()
        {
            _states = new Dictionary<Type, GameStateBase>
            {
                { typeof(UIState), _uIState },
                { typeof(InGameState), _inGameState }
            };

            _inGameState.Starter(this);
            _uIState.Starter(this);

            SwitchState(_uIState);
        }

        public void Update()
        {
            _currentState.Update();
        }
        
        public GameStateBase GetState<T>() where T : GameStateBase
        {
            return _states.GetValueOrDefault(typeof(T));
        }
        public void SwitchStates<T>() where T : GameStateBase
        {
            if (_states.TryGetValue(typeof(T), out var newState))
            {
                SwitchState(newState);
            }
            else
            {
                Debug.LogWarning($"State of type {typeof(T).Name} is not registered.");
            }
        }
        
        private void SwitchState(GameStateBase uIState)
        {
            _currentState?.Exit();
            _currentState = uIState;
            _currentState.Enter();
        }

    }
}
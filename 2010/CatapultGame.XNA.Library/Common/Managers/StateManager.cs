using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IStateManager
	{
		void Initialize();
		void SetState(CatapultState nextState);
		CatapultState CatapultState { get; }
	}

	public class StateManager : IStateManager
	{
		private CatapultState currentState;

		public void Initialize()
		{
			currentState = CatapultState.Rolling;
		}

		public void SetState(CatapultState nextState)
		{
			currentState = nextState;
		}

		public CatapultState CatapultState { get { return currentState; } }
	}
}
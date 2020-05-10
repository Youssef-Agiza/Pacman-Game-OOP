#pragma once

#include <memory>
#include <stack>

#include "State.h"

//namespace PacmanCS

//{
	typedef std::unique_ptr<State> StateRef;

	class StateMachine
	{
	public:
		StateMachine() { }
		~StateMachine() { }

		void AddState(StateRef newState, bool isReplacing = true);
		void RemoveState();

		void ProcessStateChanges();

		StateRef &GetStatus();

	private:
		std::stack<StateRef> state;
		StateRef NewS;

		bool removing;
		bool adding, replacing;
	};
//}
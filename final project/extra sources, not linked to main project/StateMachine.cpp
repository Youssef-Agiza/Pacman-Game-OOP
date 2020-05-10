#include "StateMachine.h"

//namespace PacmanCS
//{
	void StateMachine::AddState(StateRef newState, bool isReplacing)
	{
		this->adding= true;
		this->replacing = isReplacing;

		this->NewS= std::move(newState);
	}

	void StateMachine::RemoveState()
	{
		this->removing = true;
	}

	void StateMachine::ProcessStateChanges()
	{
		if (this->removing && !this->state.empty())
		{
			this->state.pop();

			if (!this->state.empty())
			{
				this->state.top()->Resume();
			}

			this->removing= false;
		}

		if (this->adding)
		{
			if (!this->state.empty())
			{
				if (this->replacing)
				{
					this->state.pop();
				}
				else
				{
					this->state.top()->Pause();
				}
			}

			this->state.push(std::move(this->NewS));
			this->state.top()->Init();
			this->adding = false;
		}
	}

	StateRef &StateMachine::GetStatus()
	{
		return this->state.top();
	}
//}
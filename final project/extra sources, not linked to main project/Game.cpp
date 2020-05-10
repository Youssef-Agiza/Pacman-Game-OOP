#include "Game.h"
#include "SplashScreen.h"

//namespace PacmanCS

//{
	Game::Game(int width, int height, std::string title, sf::RenderWindow* w)
	{
		mData->machine = new StateMachine();
		mData->assets = new MenusM();

		mData->window = w;
		mData->window->create(sf::VideoMode(width, height), title, sf::Style::Close | sf::Style::Titlebar);
		mData->machine->AddState(StateRef(new SplashState(this->mData)));

		this->Run();
	}

	void Game::Run()
	{
		float newTime, frameTime, LinearCaluculation;

		float currentTime = this->clock.getElapsedTime().asSeconds();
		float accumulator = 0.0f;

		while (this->mData->window->isOpen())
		{
			this->mData->machine->ProcessStateChanges();

			newTime = this->clock.getElapsedTime().asSeconds();
			frameTime = newTime - currentTime;

			if (frameTime > 0.25f)
			{
				frameTime = 0.25f;
			}

			currentTime = newTime;
			accumulator += frameTime;

			while (accumulator >= dt)
			{
				this->mData->machine->GetStatus()->HandleInput();
				this->mData->machine->GetStatus()->Update(dt);

				accumulator -= dt;
			}

			LinearCaluculation = accumulator / dt;
			this->mData->machine->GetStatus()->Draw(LinearCaluculation);
		}
	}
//}
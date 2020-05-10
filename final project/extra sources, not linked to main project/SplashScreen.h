#pragma once

#include <SFML/Graphics.hpp>
#include "State.h"
#include "Game.h"

//namespace PacmanCS

//{
	class SplashState : public State
	{
	public:
		SplashState(Info data);

		void Init();

		bool HandleInput();
		void Update(float dt);
		void Draw(float dt);

	private:
		Info mData;

		sf::Clock clock;

		sf::Sprite mbckgrnd;
	};
//}
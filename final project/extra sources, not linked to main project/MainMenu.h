#pragma once

#include <SFML/Graphics.hpp>
#include "State.h"
//#include"GameManager.h"

#include "Game.h"
//namespace PacmanCS {
	class MainMenuState : public State
	{
	public:
		MainMenuState(Info data);

		void Init();

		bool HandleInput();
		void Update(float dt);
		void Draw(float dt);

	private:
		Info mData;
		sf::Sprite mbckgrnd;
		sf::Sprite mTitle;
		sf::Sprite mplayb;
	};
//}
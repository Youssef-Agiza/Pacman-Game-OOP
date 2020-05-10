#pragma once

#include <memory>
#include <string>
#include <SFML/Graphics.hpp>
#include "StateMachine.h"
#include "MenusM.h"
#include "InputManager.h"

//namespace PacmanCS

	struct GameData
	{
		StateMachine *machine;
		sf::RenderWindow *window;
		MenusM *assets;
		InputManager input;
	};

	typedef std::shared_ptr<GameData> Info;

	class Game
	{
	public:
		Game(int width, int height, std::string title, sf::RenderWindow* window );

	private:
		
		const float dt = 1.0f / 60.0f;
		sf::Clock clock;

		Info mData = std::make_shared<GameData>();

		void Run();
	};
//}
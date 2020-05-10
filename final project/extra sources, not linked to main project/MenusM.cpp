#include <SFML/Graphics.hpp>
#include "MenusM.h"

//namespace PacmanCS


//{
	void MenusM::LoadTexture(std::string name, std::string fileName)
	{
		sf::Texture T;

		if (T.loadFromFile(fileName))
		{
			this->_textures[name] = T;
		}
	}

	sf::Texture &MenusM::GetTexture(std::string name)
	{
		return this->_textures.at(name);
	}

	void MenusM::LoadFont(std::string name, std::string fileName)
	{
		sf::Font font;

		if (font.loadFromFile(fileName))
		{
			this->_fonts[name] = font;
		}
	}

	sf::Font &MenusM::GetFont(std::string name)
	{
		return this->_fonts.at(name);
	}
//}
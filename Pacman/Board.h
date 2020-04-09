#pragma once
#include"SFML/Graphics.hpp"
#include <string>
#include <iostream>
namespace PacmanCS {

	class Board
	{
	private:
		sf::Texture mWallTexture;
		sf::Texture mRoadTexture; //change this name later
		sf::RectangleShape** mShape;
		const int mRows;
		const int mCols;
	public:
		Board(int rows, int cols, std::string wall, std::string road = "");
		//~Board(); //implement later
		void drawOnWindow(sf::RenderWindow& w);
		void setTextures( int** arr);

	};

}
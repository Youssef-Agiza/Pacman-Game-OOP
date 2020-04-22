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
		const int mNumOfRows;
		const int mNumOfCols;
		const float mTileSize;
		const float mPositionOnWindow;

		void intializeShape();
	public:
		Board(int numNumOfRows, int numNumOfCols, float tileSize,float posOnWindow);
		
		//setters
		Board& setRoadTexture(std::string fileName);
		Board& setWallTexture(std::string fileName);
		

		//getters
		float getPositionOneWindow()const;
		float getTileSize()const;
		int getNumOfRows()const;
		int getNumOfRows()const;


		void drawOnWindow(sf::RenderWindow& w);
		void setTextures( int** arr);

	};

}
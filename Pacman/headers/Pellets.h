#pragma once
#include <cstddef>
#include<vector>
#include "Pacman.h"
#include "Audio.h"
#include"SFML/Graphics.hpp"
using namespace std;
using namespace sf;


class Pellets
{
private:
	vector <vector<int>> arrScore;
	Texture mPelletTexture;
	Texture mPowerTexture;
	Texture mBlack;
	Texture mTransparent;
	int score;
	
public:
	Pellets(vector <vector<int>> v);
	void drawPellets(RenderWindow &w, RectangleShape** mShape);
	void intersectPellets(Pacman* P);
	void incrementLife(Pacman* P);
	int  getScore() const;
	void incrementScore(int x);
	void isPowerPelletEaten(Pacman* P);
	~Pellets();
};


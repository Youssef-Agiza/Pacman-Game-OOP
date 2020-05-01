#pragma once
#include <cstddef>
#include<vector>
#include"SFML/Graphics.hpp"
using namespace std;
using namespace sf;


class Pellets
{
private:
	int** arrScore = NULL;
	Texture mPelletTexture;
	Texture mPowerTexture;
	Texture mBlack;
	Texture mTransparent;
	int score;
	Text T;
	Font font;
	Text Num;
	Text L;
	Text life;
public:
	Pellets(vector <vector<int>> v);
	void drawPellets(RenderWindow &w, RectangleShape** mShape);
	void intersectPellets(int r, int c);
	void drawText(RenderWindow& w, int x);
	~Pellets();
};


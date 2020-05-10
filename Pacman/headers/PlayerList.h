#pragma once
#include "Common.h"
#include <list>
#include<sstream>
using namespace std;
struct PlayerData
{
	std::string userName;
	std::string email;
	int highScore;

	PlayerData(std::string n,std::string e,int score ):
		userName(n),email(e),highScore(score)
	{}

};


class PlayerList
{
private:
	std::string fileName;
	std::ifstream ins;
	std::ofstream outs;
	std::list<PlayerData> playerList;
public:
	PlayerList(std::string fileName);
 ~PlayerList();

 void LoadFiletoList();
 void loadListToFile();
 std::list<PlayerData>& getList();
 PlayerData& getHighestScore()const;
 void addPlayer(PlayerData);
 /*bool search(PlayerData player)const;
 void updateHighScore(PlayerData);*/


};


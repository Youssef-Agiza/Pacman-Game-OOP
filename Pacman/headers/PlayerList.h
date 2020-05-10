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
	int matchesWon;
	int matchesLost;

	PlayerData(std::string n,std::string e,int score,int wins,int loses ):
		userName(n),email(e),matchesWon(wins),matchesLost(loses),highScore(score)
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


};


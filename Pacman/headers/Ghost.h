#pragma once
#include "Character.h"
#include "Board.h"

class Ghost :public Character  {

private:
    unsigned int mIntialRow;
    unsigned int mIntialCol;
    bool mVulenrable; //freight mood

public:
    Ghost(int intialRow, int intialColumn, float size, float position);
  
    Ghost& setIntialRow(unsigned int row);
    Ghost& setIntialCol(unsigned int col);
    unsigned int getIntialRow()const;
    unsigned int getIntialCol()const;

    Ghost& setVulenrable(bool v);
    bool getVulenrable()const;

    void resetPosition()override;
    void die()override;
};


#pragma once
#include <SFML/Audio.hpp>
#include <string>
namespace PacmanCS {

	class Audio //singelton pattern
	{
	private:
		sf::Music mMusic;
		std::string mSoundtracksPath;
		Audio();
		Audio(const Audio&)=delete;
		void operator =(const Audio&) = delete;
		static Audio* instance; 
	public:
	

		static Audio* getInstance();
		Audio& setPath(std::string);
		bool  playDeath(bool loop=false, std::string track="pacman_death.wav");
		bool playBeginning( bool loop = false,std::string track = "pacman_beginning.wav");
		bool playEatGhost(bool loop=false, std::string track = "pacman_eatghost.wav");
		bool playChomp(bool loop = false, std::string track = "pacman_chomp.wav");
		void stop();
		sf::SoundSource::Status getStatus()const;
	};
}
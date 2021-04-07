# End of studies project : Sound waves' modelisation in a virtual environment

## Introduction :

The aim of the project is to create a simulator which allows users to visualize sound waves’ trajectories and energy loss.

## Features :

- visualizing sounds with
	- colored spheres for movement and energy loss
	- colored rays for trajectories
- create new sounds and chose its parameters (frequency, origin, number of spheres)
- replay old ones (if the api is launched)
- set up the simulation by
	- changing the environment (workspace, cinema, test)
	- changing materials properties (acoustic absorption, position)
	- adding new elements to the chosen scene
	

## How to use the simulation :

This simulation uses Unity 2020.1.13f.

To save sounds' data and replay previous sounds you must install Docker in order to launch the API: https://www.docker.com/

To start the API use the following command : ```docker-compose up```
To stop the API use the following command : ```docker-compose down```




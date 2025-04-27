**Computer Engineering**
*Object Oriented Programming*
2025

Practical Work 1 - Airport Landing Simulator 

-----------------Group 9-------------------

Members:

Mario Wenceslao Rivera Córdoba 

Iñaki Quireza Iriberri

Javier López Ranero

## 1. Introduction
   
  This document describes and presents our design of the implementation of an airport simulation
  
  This simulation manages different airplanes landing in a new airport, taking into account the runway management and some variables like fuel or aircraft type

  In this document we explain the design choices, the structure and classes, the challenges we faced and our final conclusions after developing this project

## 2. Description

  ### Overview of the system

  This project is a simulator of an airport with different airplanes landing on it

  This simulation works with ticks, each tick is 15min. Each ariplane moves closer to the airport every tick and use fuel depending on variables as speed or fuel consumption

  The planes land on the airport when a runway is free, when landing the runway is occupied and no other airplane can use it. When the plane lands the runway becomes free again

  You can add flights by hand or by a csv file. All actions are available on the menu

  The different types of airplanes are: Cargo, Passenger and Private. Each one has their unique characteristic in addition to those of the airplanes in general

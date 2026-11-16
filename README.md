# City Bus Driving Simulator 🚌

A 3D bus driving simulation developed using Unity and C#.

## Project Overview

City Bus Driving Simulator is a personal Unity project developed to explore vehicle physics, realistic driving mechanics, and interactive camera systems in a 3D urban environment.

The project was developed from scratch, starting with the planning of the game structure and continuing with vehicle control, camera management, user interface, and environment design.

## Features

### Vehicle Physics & Controls
- Rigidbody-based vehicle physics
- Four-wheel WheelCollider system
- Rear-wheel drive (RWD)
- Acceleration and braking mechanics
- Forward and reverse movement
- Steering control
- Synchronized steering wheel animation
- Wheel and rim rotation
- Center of mass adjustment for improved vehicle stability
- Maximum speed limitation

### Multiple Camera Perspectives
- Driver's seat view
- Top view
- Bird's-eye view
- Wheel view
- Camera switching using the C key
- Single-camera system with multiple camera positions

### Driving Interface (HUD)
- Real-time speed display in km/h
- Dynamic speed indicator colors:
  - Green: Below 30 km/h
  - Yellow: 30–59 km/h
  - Red: 60 km/h and above
- Drive, Reverse, and Neutral indicators (D, R, N)

### Environment
- 3D urban environment
- Customized city map
- Bus stop locations
- Night-time driving environment

## Technologies Used

- Unity
- C#
- Rigidbody
- WheelCollider
- TextMeshPro
- Unity Input System / Input Manager

## Scripts & Implementation

### VehicleController.cs
Handles the main vehicle mechanics, including motor torque, braking, steering, wheel physics, and vehicle speed limitations.

### CameraController.cs
Manages multiple camera perspectives using a single camera. Players can switch between predefined camera positions by pressing C.

### SpeedoMeter.cs
Implements the driving HUD by displaying real-time vehicle speed, movement direction, and speed-based visual indicators.

## Screenshots

<img width="1246" height="643" alt="image" src="https://github.com/user-attachments/assets/752ef424-3bad-462e-a92e-c3d613dbfbee" />
<img width="1242" height="644" alt="image" src="https://github.com/user-attachments/assets/25d8d3d5-9efc-4f22-98e7-fb40f99577ea" />
<img width="1241" height="643" alt="image" src="https://github.com/user-attachments/assets/1e846071-afcf-47c5-a6f0-8c80b61eda9d" />
<img width="1233" height="638" alt="image" src="https://github.com/user-attachments/assets/82512051-5989-4095-8c1e-09537902803b" />

## Development Status

**In Development**

The current version focuses on vehicle driving mechanics, camera systems, and the urban environment.

Passenger interactions and mission systems have not yet been implemented.

## Third-Party Credits

The project uses selected third-party assets:

- **Demo City** – Versatile Studio ([Unity Asset Store](https://assetstore.unity.com/packages/3d/environments/urban/demo-city-by-versatile-studio-mobile-friendly-269772))
- **Street Vehicles Pack – Autobus Tram** – Vladislav Simakov ([Unity Asset Store](https://assetstore.unity.com/packages/3d/vehicles/land/street-vehicles-pack-autobus-tram-213421))
- **BUS-STATION** – sherif shawky ([Sketchfab](https://sketchfab.com/3d-models/bus-station-fca169385b04409ca208f87b84796f07)), licensed under [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/). Some original parts were removed; no new elements were added.
- **Bus Model – Mercedes-Benz MB O-371R 1989 Coach Bus (possible source)** – joao3DModels ([CGTrader](https://www.cgtrader.com/free-3d-models/vehicle/bus/merecedes-benz-mb-o-371r-1989-coach-bus)). The downloaded model contains the creator name "joao3D" and its filename includes the matching CGTrader product ID (2263056). The exact source has not been independently verified.

Third-party assets remain the property of their respective creators. Original asset files are not included in this repository.

## Repository Scope

This repository focuses on original C# scripts, vehicle control systems, camera management, and gameplay development.

Third-party assets that cannot be redistributed under their applicable licenses are excluded.






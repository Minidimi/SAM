# Code for SAM visualization
Unity code for receiving ROS messages from the SAM robot and visualizing the SPIRIT scene.
## Requirements
+ PC with Windows and Unity (Tested with Windows 10 and Unity 2021.3.9f1)
+ [Setup of the Mixed Reality Toolkit in Unity](https://learn.microsoft.com/en-us/training/paths/beginner-hololens-2-tutorials/)
+ [Setup of Unity Robotics Hub in Unity and the on the ROS server](https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/setup.md)
## Build and Deployment
To deploy the application on the hololens follow these steps:
+ Make sure the Hololens is connected to the same network as the PC used for deployment
+ To establish a connection to ROS, set the IP address and port of the ROS TCP server in Unity under the menu item Robotics -> ROS Settings

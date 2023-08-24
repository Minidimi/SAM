# Unity Project for SAM Visualization
This repository contains the Unity project for receiving ROS messages from the SAM robot and visualizing the SPIRIT scene.
## Requirements
+ PC with Windows and Unity (Tested with Windows 10 and Unity 2021.3.9f1)
+ Hololens 2
+ Visual Studio (Tested with Visual Studio 2019)
+ [Setup of the Mixed Reality Toolkit in Unity](https://learn.microsoft.com/en-us/training/paths/beginner-hololens-2-tutorials/)
+ [Setup of Unity Robotics Hub in Unity and the on the ROS server](https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/setup.md)
## Build and Deployment
To deploy the application on the hololens follow these steps:
+ Make sure the Hololens is connected to the same network as the PC used for deployment
+ To establish a connection to ROS, set the IP address and port of the ROS TCP server in Unity under the menu item `Robotics` &rarr; `ROS Settings`
+ Build the Unity project under `File` &rarr; `Build Settings` &rarr; `Universal Windows Platform` &rarr; `Build`
+ In the `Build` folder, select the generated `.sln` file in Visual Studio
+ Specify the Hololens IP adress under the Visual Studio menu item `Debugging` &rarr; `Debugging Properties` &rarr; `Configuration Properties` &rarr; `Debugging` &rarr; `Device name`
+ Make sure the Visual Studio solution is set to `Release` and `ARM64` next to the launch button. When these are set, press the green launch button to launch the application on the Hololens.
## Starting the ROS Server
+ Navigate to the catkin workspace of the ROS TCP Endpoint.
+ Run the script `devel/setup.bash`
+ Set the environment variables for `ROS_IP` and `ROS_MASTER_URI`
+ Run `roslaunch ros_tcp_endpoint endpoint.launch` to launch the endpoint
## Starting the Hololens
+ First, make sure the Hololens is in the same network as the ROS endpoint.
+ After the application has first been built as mentioned earlier, the application can be started from `all apps`, even if it is not connected to the development PC
+ To receive ROS messages, it is important that the ROS endpoint has the same IP address specified in the Unity project during the latest build.
## Remote Display of the Hololens View
+ To remotely display the view of the Hololens, enter the Hololens IP address in a browser
+ If a warning is shown, choose `advanced`, then `continue despite risk`
+ Enter the username and password for the Hololens
+ Go to `Views` &rarr; `Mixed Reality Capture`
+ Choose `Live Preview`
## Add or Remove Models
To add a new model, convert it to a `.obj` file and add it into the scene. To move it through translations from ROS, add the `RosManipulator` Script. Here, make sure the origin coordinate system on the ROS side correctly corresponds to the position of the object. If this is not the case, the object can be added as the child of an empty GameObject corresponding to the coordinate system in ROS. The GameObject `sam_scaler` under `Robot Box` &rarr; `Bounding Area` in the hierarchy represents the origin of the coordinate system. As such, all GameObjects of the SAM scene should be put underneath this in the scene hierarchy and then positioned in relation to the other objects. To receive the transformations from ROS, enter the topic name in the `RosManipulator` field of the corresponding GameObject. If the new object should be hidden depending on the chosen mode, the `ModeController` GameObject in the scene can be used.

To delete an object, simply remove it from the Unity scene. Here, it is important to consider hierarchies to not accidentally delete child objects.
